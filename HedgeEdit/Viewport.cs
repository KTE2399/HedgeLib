using HedgeEdit.Properties;
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
        public static List<IRenderable> Objects = new List<IRenderable>();
        public static Dictionary<string, int> Textures = new Dictionary<string, int>();
        public static Model DefaultCube;

        public static Vector3 CameraPos = Vector3.Zero, CameraRot = new Vector3(-90, 0, 0);
        public static float FOV = 40.0f, NearDistance = 0.1f, FarDistance = 1000f;
        
        public static GLControl VP = null;
        private static Vector3 camUp = new Vector3(0, 1, 0),
            camForward = new Vector3(0, 0, -1);

        private static float camSpeed = normalSpeed;
        private const float normalSpeed = 1, fastSpeed = 4;

        // Methods
        public static void Init(GLControl viewport)
        {
            VP = viewport;
            //GL.Enable(EnableCap.DepthTest);
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
            if (VP == null)
                throw new Exception("Cannot render viewport - viewport not yet initialized!");

            // Clear the background color
            GL.ClearColor(0, 0, 0, 1);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Start using our "Default" program and bind our VAO
            //int defaultID = Shaders.ShaderPrograms["ColorTest"]; // TODO: Make this more efficient
            //GL.UseProgram(defaultID);

            foreach (var obj in Objects)
            {
                obj.Draw(0, 0, 0, 0, 1);
            }

            // Swap our buffers
            VP.SwapBuffers();
        }

        public static void DrawTexturedRect(float x, float y, float width, float height, int texture)
        {
            GL.BindTexture(TextureTarget.Texture2D, texture);
            DrawTexturedRect(x, y, width, height);
        }
        
        public static void DrawTexturedRect(float x, float y, float width, float height)
        {
            float w = (1f / VP.Width) * width;
            float h = (1f / VP.Height) * height;
            float x2 = (x / VP.Width) - 1f;
            float y2 = ((y + height) / VP.Height) - 1f;
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
            double w = (1f / VP.Width) * width;
            double h = (1f / VP.Height) * height;
            double x2 = (x / VP.Width) - 1f;
            double y2 = ((y + height) / VP.Height) - 1f;
            double xCrop2 = (xCrop / texW);
            double yCrop2 = (yCrop / texH);
            double wCrop2 = (wCrop / texW);
            double hCrop2 = (hCrop / texH);
            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(xCrop2, yCrop2 + hCrop2); GL.Vertex2(x2, -y2);
            GL.TexCoord2(xCrop2 + wCrop2, yCrop2 + hCrop2); GL.Vertex2(x2 + w, -y2);
            GL.TexCoord2(xCrop2 + wCrop2, yCrop2); GL.Vertex2(x2 + w, -y2 + h);
            GL.TexCoord2(xCrop2, yCrop2); GL.Vertex2(x2, -y2 + h);

            GL.End();
        }

        public static void DrawTexturedRect(float x, float y, float width, float height, float xCrop, float yCrop, float wCrop, float hCrop, bool flipX, bool flipY, int texture)
        {
            GL.BindTexture(TextureTarget.Texture2D, texture);
            DrawTexturedRect(x, y, width, height, xCrop, yCrop, wCrop, hCrop, flipX, flipY);
        }

        /// <summary>
        /// Draws a Rect with a texture that can be Cropped(Textures) and flipped
        /// </summary>
        /// <param name="x">The X Position of the Rect</param>
        /// <param name="y">The Y Position of the Rect</param>
        /// <param name="width">The Width of the Rect</param>
        /// <param name="height">The Hight of the Rect</param>
        /// <param name="xCrop">The X Position of the Texture</param>
        /// <param name="yCrop">The Y Position of the Texture</param>
        /// <param name="wCrop">The Width of the Texture</param>
        /// <param name="hCrop">The Hight of the Texture</param>
        /// <param name="flipX">Flip the Width of the Texture</param>
        /// <param name="flipY">Flip the hight of the Texture</param>
        public static void DrawTexturedRect(float x, float y, float width, float height, float xCrop, float yCrop, float wCrop, float hCrop, bool flipX, bool flipY)
        {

            float texW, texH;
            GL.GetTexLevelParameter(TextureTarget.Texture2D, 0, GetTextureParameter.TextureWidth, out texW);
            GL.GetTexLevelParameter(TextureTarget.Texture2D, 0, GetTextureParameter.TextureHeight, out texH);
            double w = (1d / (double)VP.Width) * (double)width;
            double h = (1d / (double)VP.Height) * (double)height;
            double x2 = ((double)x / (double)VP.Width) - 1d;
            double y2 = (((double)y + (double)height) / (double)VP.Height) - 1d;
            double xCrop2 = ((double)xCrop / (double)texW);
            double yCrop2 = ((double)yCrop / (double)texH);
            double wCrop2 = ((double)wCrop / (double)texW);
            double hCrop2 = ((double)hCrop / (double)texH);
            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(flipX ? (xCrop2 + wCrop2) : (xCrop2), flipY ? (yCrop2) : (yCrop2 + hCrop2)); GL.Vertex2(x2, -y2);
            GL.TexCoord2(!flipX ? (xCrop2 + wCrop2) : (xCrop2), flipY ? (yCrop2) : (yCrop2 + hCrop2)); GL.Vertex2(x2 + w, -y2);
            GL.TexCoord2(!flipX ? (xCrop2 + wCrop2) : (xCrop2), !flipY ? (yCrop2) : (yCrop2 + hCrop2)); GL.Vertex2(x2 + w, -y2 + h);
            GL.TexCoord2(flipX ? (xCrop2 + wCrop2) : (xCrop2), !flipY ? (yCrop2) : (yCrop2 + hCrop2)); GL.Vertex2(x2, -y2 + h);

            GL.End();
        }

        public static void DrawTexturedRectRot(float x, float y, float width, float height, float xCrop, float yCrop, float wCrop, float hCrop, bool flipX, bool flipY, int texture)
        {
            GL.BindTexture(TextureTarget.Texture2D, texture);
            DrawTexturedRectRot(x, y, width, height, xCrop, yCrop, wCrop, hCrop, flipX, flipY);
        }


        public static void DrawTexturedRectRot(float x, float y, float width, float height, float xCrop, float yCrop, float wCrop, float hCrop, bool flipX, bool flipY)
        {

            float texW, texH;
            GL.GetTexLevelParameter(TextureTarget.Texture2D, 0, GetTextureParameter.TextureWidth, out texW);
            GL.GetTexLevelParameter(TextureTarget.Texture2D, 0, GetTextureParameter.TextureHeight, out texH);
            double w = (1d / (double)VP.Width) * (double)width;
            double h = (1d / (double)VP.Height) * (double)height;
            double x2 = ((double)x / (double)VP.Width) - 1d;
            double y2 = (((double)y + (double)height) / (double)VP.Height) - 1d;
            double xCrop2 = ((double)xCrop / (double)texW);
            double yCrop2 = ((double)yCrop / (double)texH);
            double wCrop2 = ((double)wCrop / (double)texW);
            double hCrop2 = ((double)hCrop / (double)texH);
            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(flipX ? (xCrop2 + wCrop2) : (xCrop2), !flipY ? (yCrop2) : (yCrop2 + hCrop2)); GL.Vertex2(x2 + w, -y2);
            GL.TexCoord2(!flipX ? (xCrop2 + wCrop2) : (xCrop2), !flipY ? (yCrop2) : (yCrop2 + hCrop2)); GL.Vertex2(x2 + w, -y2 + h);
            GL.TexCoord2(!flipX ? (xCrop2 + wCrop2) : (xCrop2), flipY ? (yCrop2) : (yCrop2 + hCrop2)); GL.Vertex2(x2, -y2 + h);
            GL.TexCoord2(flipX ? (xCrop2 + wCrop2) : (xCrop2), flipY ? (yCrop2) : (yCrop2 + hCrop2)); GL.Vertex2(x2, -y2);

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

        public static void AddObject(IRenderable obj)
        {
            Objects.Add(obj);
        }

        public static void Clear()
        {
            Objects.Clear();
        }
    }
}