﻿using HedgeEdit.Properties;
using HedgeLib.Models;
using OpenTK;
//using OpenTK.Graphics.ES30;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace HedgeEdit
{
    public static class Viewport
    {
        // Variables/Constants
        public static List<ViewportObject> Objects = new List<ViewportObject>();
        public static Dictionary<string, int> Textures = new Dictionary<string, int>();
        public static Model DefaultCube;

        public static Vector3 CameraPos = Vector3.Zero, CameraRot = new Vector3(-90, 0, 0);
        public static float FOV = 40.0f, NearDistance = 0.1f, FarDistance = 1000f;
        public static bool IsMovingCamera = false;

        private static GLControl vp = null;
        private static Point prevMousePos = Point.Empty;
        private static Vector3 camUp = new Vector3(0, 1, 0),
            camForward = new Vector3(0, 0, -1);

        private static float camSpeed = normalSpeed;
        private const float normalSpeed = 1, fastSpeed = 4;

        // Methods
        public static void Init(GLControl viewport)
        {
            vp = viewport;
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            // Load the shaders
            //Shaders.LoadAll();
        }

        public static void Resize(int width, int height)
        {
            GL.Viewport(0, 0, width, height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, width, height, 0, -16, -16);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        public static void Render()
        {
            if (vp == null)
                throw new Exception("Cannot render viewport - viewport not yet initialized!");

            // Clear the background color
            GL.ClearColor(0, 0, 0, 1);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Start using our "Default" program and bind our VAO
            //int defaultID = Shaders.ShaderPrograms["ColorTest"]; // TODO: Make this more efficient
            //GL.UseProgram(defaultID);

            // Update camera transform
            var mouseState = Mouse.GetState();
            if (IsMovingCamera && mouseState.RightButton == OpenTK.Input.ButtonState.Pressed)
            {
                var vpMousePos = vp.PointToClient(Cursor.Position);
                float screenX = (float)vpMousePos.X / vp.Size.Width;
                float screenY = (float)vpMousePos.Y / vp.Size.Height;

                // Rotation
                var mouseDifference = new Point(
                    Cursor.Position.X - prevMousePos.X,
                    Cursor.Position.Y - prevMousePos.Y);

                CameraRot.X += mouseDifference.X * 0.1f;
                CameraRot.Y -= mouseDifference.Y * 0.1f;

                // Position
                var keyState = Keyboard.GetState();
                camSpeed = (keyState.IsKeyDown(Key.ShiftLeft) ||
                    keyState.IsKeyDown(Key.ShiftRight)) ? fastSpeed : normalSpeed;

                if (keyState.IsKeyDown(Key.W))
                {
                    CameraPos += camSpeed * camForward;
                }
                else if (keyState.IsKeyDown(Key.S))
                {
                    CameraPos -= camSpeed * camForward;
                }

                if (keyState.IsKeyDown(Key.A))
                {
                    CameraPos -= Vector3.Normalize(
                        Vector3.Cross(camForward, camUp)) * camSpeed;
                }
                else if (keyState.IsKeyDown(Key.D))
                {
                    CameraPos += Vector3.Normalize(
                        Vector3.Cross(camForward, camUp)) * camSpeed;
                }

                // Snap cursor to center of viewport
                Cursor.Position =
                    vp.PointToScreen(new Point(vp.Width / 2, vp.Height / 2));
            }

            // Update Transforms
            float x = MathHelper.DegreesToRadians(CameraRot.X);
            float y = MathHelper.DegreesToRadians(CameraRot.Y);
            float yCos = (float)Math.Cos(y);

            var front = new Vector3()
            {
                X = (float)Math.Cos(x) * yCos,
                Y = (float)Math.Sin(y),
                Z = (float)Math.Sin(x) * yCos
            };

            camForward = Vector3.Normalize(front);

            var view = Matrix4.LookAt(CameraPos,
                CameraPos + camForward, camUp);

            prevMousePos = Cursor.Position;

            foreach (var obj in Objects)
            {
                obj.Draw();
            }

            // Swap our buffers
            vp.SwapBuffers();
        }

        public static void DrawTexturedRect(float x, float y, float width, float height, int texture)
        {
            GL.BindTexture(TextureTarget.Texture2D, texture);
            DrawTexturedRect(x, y, width, height);
        }
        
        public static void DrawTexturedRect(float x, float y, float width, float height)
        {
            float w = (1f / vp.Width) * width;
            float h = (1f / vp.Height) * height;
            float x2 = (x / vp.Width) - 1f;
            float y2 = ((y + height) / vp.Height) - 1f;
            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0, 1); GL.Vertex2(x2, -y2);
            GL.TexCoord2(1, 1); GL.Vertex2(x2 + w, -y2);
            GL.TexCoord2(1, 0); GL.Vertex2(x2 + w, -y2 + h);
            GL.TexCoord2(0, 0); GL.Vertex2(x2, -y2 + h);

            GL.End();
        }

        public static void DrawTexturedRect(float x, float y, float width, float height, float xCrop, float yCrop, float wCrop, float hCrop, int texture)
        {
            GL.BindTexture(TextureTarget.Texture2D, texture);
            DrawTexturedRect(x, y, width, height, xCrop, yCrop, wCrop, hCrop);
        }

        public static void DrawTexturedRect(float x, float y, float width, float height, float xCrop, float yCrop, float wCrop, float hCrop)
        {

            float texW, texH;
            GL.GetTexLevelParameter(TextureTarget.Texture2D, 0, GetTextureParameter.TextureWidth, out texW);
            GL.GetTexLevelParameter(TextureTarget.Texture2D, 0, GetTextureParameter.TextureHeight, out texH);
            float w = (1f / vp.Width) * width;
            float h = (1f / vp.Height) * height;
            float x2 = (x / vp.Width) - 1f;
            float y2 = ((y + height) / vp.Height) - 1f;
            float xCrop2 = (xCrop / texW);
            float yCrop2 = (yCrop / texH);
            float wCrop2 = (wCrop / texW);
            float hCrop2 = (hCrop / texH);
            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(xCrop2, yCrop2 + hCrop2); GL.Vertex2(x2, -y2);
            GL.TexCoord2(xCrop2 + wCrop2, yCrop2 + hCrop2); GL.Vertex2(x2 + w, -y2);
            GL.TexCoord2(xCrop2 + wCrop2, yCrop2); GL.Vertex2(x2 + w, -y2 + h);
            GL.TexCoord2(xCrop2, yCrop2); GL.Vertex2(x2, -y2 + h);

            GL.End();
        }

        public static int LoadTexture(string file)
        {
            return LoadTexture(new Bitmap(file), file);
        }

        public static int LoadTexture(Bitmap bitmap, string file = null)
        {
            if (Textures.ContainsKey(file))
            {
                return Textures[file];
            }
            int tex;
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            GL.GenTextures(1, out tex);
            GL.BindTexture(TextureTarget.Texture2D, tex);

            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            bitmap.UnlockBits(data);


            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            return tex;
        }

        public static void AddObject(ViewportObject obj)
        {
            Objects.Add(obj);
        }

        public static void Clear()
        {
            Objects.Clear();
        }
    }
}