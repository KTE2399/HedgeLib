using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HedgeEdit
{
    public interface IRenderable
    {
        void Draw(float x, float y, float xCam, float yCam, float scale);
        void Mouse(float x, float y, float scale, MouseState mouseState);
    }
}
