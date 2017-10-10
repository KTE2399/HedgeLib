using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HedgeEdit
{
    public class Editor : IRenderable
    {
        public static List<IRenderable> Objects = new List<IRenderable>();
        public static bool IsMovingCamera;
        public static Editor Instance;
        public S2HDSetData.SetObject SelectedObject;
        public S2HDSetData.SetObject LastSelectedObject;
        public int LastX;
        public int LastY;
        public float XOffset = 0f;
        public float YOffset = 0f;
        public float Scale = 0f;
        public float lastScroll = 0f;
        public Point prevMousePos = Point.Empty;

        public Editor()
        {
            Instance = this;
        }

        public void Draw(float x, float y, float xCam, float yCam, float scale)
        {

            // Update camera transform
            var mouseState = OpenTK.Input.Mouse.GetState();
            Scale -= (lastScroll - mouseState.WheelPrecise) * 0.05f;
            lastScroll = mouseState.WheelPrecise;
            if (IsMovingCamera && mouseState.RightButton == OpenTK.Input.ButtonState.Pressed)
            {
                var vpMousePos = Viewport.VP.PointToClient(Cursor.Position);
                float screenX = (float)vpMousePos.X / Viewport.VP.Size.Width;
                float screenY = (float)vpMousePos.Y / Viewport.VP.Size.Height;

                // Rotation
                var mouseDifference = new Point(
                    Cursor.Position.X - prevMousePos.X,
                    Cursor.Position.Y - prevMousePos.Y);

                // IDK
                XOffset += mouseDifference.X * 1f;
                YOffset += mouseDifference.Y * 1f;
                XOffset = (float)(Math.Round(XOffset / 2d) * 2d);
                YOffset = (float)(Math.Round(YOffset / 2d) * 2d);

                // Snap cursor to center of viewport
                Cursor.Position =
                    Viewport.VP.PointToScreen(new Point(Viewport.VP.Width / 2, Viewport.VP.Height / 2));
            }

            prevMousePos = Cursor.Position;
            var vpCursorPos = Viewport.VP.PointToClient(Cursor.Position);
            foreach (var obj in Objects)
            {
                // Render
                obj.Draw(x + XOffset, y + YOffset, XOffset, YOffset, scale + Scale);
                // Mouse
                obj.Mouse((((vpCursorPos.X * 2) + x) / (scale + Scale)) - XOffset,
                          ((vpCursorPos.Y * 2) + y) / (scale + Scale) - YOffset, scale + Scale, mouseState);
            }
        }

        public void AddLevelObject(IRenderable obj)
        {
            Objects.Add(obj);
        }

        public void Mouse(float x, float y, float scale, MouseState mouseState)
        {
        }
    }
}
