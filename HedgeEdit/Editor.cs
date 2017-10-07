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
        public float XOffset = 0f;
        public float YOffset = 0f;
        public Point prevMousePos = Point.Empty;

        public void Draw(float x, float y, float scale)
        {

            // Update camera transform
            var mouseState = Mouse.GetState();
            if (IsMovingCamera && mouseState.RightButton == OpenTK.Input.ButtonState.Pressed)
            {
                var vpMousePos = Viewport.VP.PointToClient(Cursor.Position);
                float screenX = (float)vpMousePos.X / Viewport.VP.Size.Width;
                float screenY = (float)vpMousePos.Y / Viewport.VP.Size.Height;

                // Rotation
                var mouseDifference = new Point(
                    Cursor.Position.X - prevMousePos.X,
                    Cursor.Position.Y - prevMousePos.Y);

                XOffset += mouseDifference.X * 1f;
                YOffset += mouseDifference.Y * 1f;

                // Snap cursor to center of viewport
                Cursor.Position =
                    Viewport.VP.PointToScreen(new Point(Viewport.VP.Width / 2, Viewport.VP.Height / 2));
            }

            prevMousePos = Cursor.Position;

            foreach (var obj in Objects)
            {
                obj.Draw(x + XOffset, y + YOffset, scale);
            }
        }

        public void AddLevelObject(IRenderable obj)
        {
            Objects.Add(obj);
        }
    }
}
