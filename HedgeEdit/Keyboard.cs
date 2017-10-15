using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SS16
{
    public class Keyboard
    {

        public delegate void KeyEvent(byte key);
        public event KeyEvent KeyPressed;
        public event KeyEvent KeyDown;
        public event KeyEvent KeyReleased;

        public static bool[] PressedKeys = new bool[0xC1];

        public void Update()
        {
            for (int i = 0x9; i < PressedKeys.Length; ++i)
            {
                short state = GetKeyState(i);
                bool pressed = (state & 0x80) != 0;
                if (!PressedKeys[i] && pressed)
                {
                    PressedKeys[i] = pressed;
                    KeyPressed.Invoke((byte)i);
                }
                else if (PressedKeys[i] && !pressed)
                {
                    PressedKeys[i] = pressed;
                    //KeyReleased.Invoke((byte)i);
                }
                if (pressed)
                {
                    //KeyDown.Invoke((byte)i);
                }

                PressedKeys[i] = pressed;
            }
        }

        [DllImport("User32.dll")]
        public static extern short GetKeyState(int key);


    }
}
