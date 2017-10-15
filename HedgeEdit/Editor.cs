using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;

namespace HedgeEdit
{
    public class Editor : IRenderable
    {
        public static List<IRenderable> Objects = new List<IRenderable>();
        public static bool IsMovingCamera;
        public static Editor Instance;
        public Font Font;
        public List<EditingMode> Modes = new List<EditingMode>();
        public EditingMode Mode;
        public S2HDSetData.SetObject SelectedObject;
        public S2HDSetData SetData;
        public Dictionary<AniGroup, int[]> SetTextures = new Dictionary<AniGroup, int[]>();
        public Dictionary<S2HDSetData.SetObject, AniGroup> SetAniLink = new Dictionary<S2HDSetData.SetObject, AniGroup>();
        public List<S2HDSetData.SetObject> LastSelectedObjects = new List<S2HDSetData.SetObject>();
        public List<int> LastX = new List<int>();
        public List<int> LastY = new List<int>();
        public float XOffset = 0f;
        public float YOffset = 0f;
        public float TextY = -40f;
        public float Scale = 0f;
        public float lastScroll = 0f;
        public Point prevMousePos = Point.Empty;
        public int FrameCount = 0;
        public int FramesPerSecond = 0;
        public int ModeIndex = 0;
        public DateTime LastTime;
        public SS16.Keyboard Keyboard = new SS16.Keyboard();

        public Editor()
        {
            Modes.Add(new EditingModeTransform());
            Modes.Add(new EditingModeAdd());
            Mode = Modes[1];
            Instance = this;
            LastTime = DateTime.Now;
        }

        public void Init(S2HDSetData setData)
        {
            SetData = setData;
            foreach (var setobj in setData.Objects)
            {
                string path = MainFrm.GetFullPathFromSonicOrcaPath(setobj.Key + "\\ANIGROUP.anigroup.xml");
                if (File.Exists(path))
                {
                    var ani = new AniGroup();
                    ani.Load(path);
                    int[] textures = new int[ani.Textures.Count];
                    for (int i = 0; i < textures.Length; ++i)
                    {
                        textures[i] = Viewport.LoadTexture(MainFrm.GetFullPathFromSonicOrcaPath(setobj.Key + ani.Textures[i] + ".png"));
                    }
                    SetTextures.Add(ani, textures);
                    SetAniLink.Add(setobj, ani);
                }
            }
            Keyboard.KeyPressed += KeyPressed;
        }

        public void Draw(float x, float y, float xCam, float yCam, float scale)
        {
            if ((DateTime.Now - LastTime).TotalMilliseconds > 1000)
            {
                FramesPerSecond = FrameCount;
                FrameCount = 0;
                LastTime = DateTime.Now;
            }
            TextY = -40;
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
                XOffset += (mouseDifference.X * 1.5f) / (scale + Scale);
                YOffset += (mouseDifference.Y * 1.5f) / (scale + Scale);
                XOffset = (float)(Math.Round(XOffset / 2d) * 2d);
                YOffset = (float)(Math.Round(YOffset / 2d) * 2d);

                // Snap cursor to center of viewport
                Cursor.Position =
                    Viewport.VP.PointToScreen(new Point(Viewport.VP.Width / 2, Viewport.VP.Height / 2));
            }
            prevMousePos = Cursor.Position;
            var vpCursorPos = Viewport.VP.PointToClient(Cursor.Position);
            float X = ((vpCursorPos.X * 2) + x) / (scale + Scale) - XOffset;
            float Y = ((vpCursorPos.Y * 2) + y) / (scale + Scale) - YOffset;
            foreach (var obj in Objects)
            {
                // Render
                obj.Draw(x + XOffset, y + YOffset, XOffset, YOffset, scale + Scale);
                // Mouse
                //obj.Mouse(X, Y, scale + Scale, mouseState);
            }
            string text = "FPS: " + FramesPerSecond;
            Font.Draw(text.ToUpper(), 10, TextY += 50, 1);
            text = "Editing Mode: " + Mode;
            Font.Draw(text.ToUpper(), 10, TextY += 50, 1);
            Mode?.Mouse(X, Y, scale + Scale, mouseState);
            Mode?.Draw(x + XOffset, y + YOffset, XOffset, YOffset, scale + Scale);
            Keyboard.Update();
            FrameCount++;
        }

        public void AddLevelObject(IRenderable obj)
        {
            Objects.Add(obj);
        }

        public void Mouse(float x, float y, float scale, MouseState mouseState)
        {
        }

        public void KeyPressed(byte key)
        {
            if (key == 0x9)
            {
                ModeIndex++;
                if (ModeIndex == Modes.Count)
                    ModeIndex = 0;
                Mode = Modes[ModeIndex];
            }
        }

        public class EditingMode : IRenderable
        {
            public string Name;

            public EditingMode(string name)
            {
                Name = name;
            }

            public virtual void Draw(float x, float y, float xCam, float yCam, float scale)
            {
            }

            public virtual void Mouse(float x, float y, float scale, MouseState mouseState)
            {
            }

            public override string ToString()
            {
                return Name;
            }
        }

        public class EditingModeTransform : EditingMode
        {
            public float? Dx = 0;
            public float? Dy = 0;

            public EditingModeTransform() : base("Object Movement")
            {

            }

            public override void Draw(float x, float y, float xCam, float yCam, float scale)
            {
                if (Instance.SelectedObject != null)
                {
                    string s = "Selected Object: " + Instance.SelectedObject.Name;
                    Instance.Font.Draw(s.ToUpper(), 10, Instance.TextY += 50, 1);
                }
            }

            public override void Mouse(float x, float y, float scale, MouseState mouseState)
            {
                if (Dx != null && Dy != null && !mouseState.IsButtonDown(MouseButton.Left))
                {
                    Dx = Dy = null;
                }
                else if (Instance.SelectedObject != null && Dx != null && Dy != null)
                {
                    var obj = Instance.SelectedObject;
                    obj.X = (int)(x + Dx);
                    obj.Y = (int)(y + Dy);
                    MainFrm.Instance.UpdatePos(obj.X, obj.Y);
                }
                else
                {
                    for (int i = Instance.SetData.Objects.Count - 1; i >= 0; --i)
                    {
                        var obj = Instance.SetData.Objects[i];
                        if (Instance.SetAniLink.ContainsKey(obj))
                        {
                            var ani = Instance.SetAniLink[obj];
                            var ani2 = ani.Animations[0];
                            var frame = ani2.Frames[0];
                            int texture = Instance.SetTextures[ani][frame.Texture];
                            float xx = (obj.X - (frame.Width / 2));
                            float yy = (obj.Y - (frame.Height / 2));

                            if (x >= xx && x <= xx + frame.Width && y >= yy && y <= yy + frame.Height && mouseState.IsButtonDown(MouseButton.Left))
                            {
                                Instance.SelectedObject = obj;
                                if (Dx == null && Dy == null)
                                {
                                    Dx = obj.X - x;
                                    Dy = obj.Y - y;
                                    MainFrm.Instance.FillSetObjectDetails(obj);
                                }
                                break;
                            }

                        }
                    }
                }
            }
        }

        public class EditingModeAdd : EditingMode
        {
            public S2HDSetData.SetObject SetObjectTemplate = null;

            public EditingModeAdd() : base("Add Object")
            {

            }

            public override void Draw(float x, float y, float xCam, float yCam, float scale)
            {
                string s = "Object Type: " + (SetObjectTemplate != null ? SetObjectTemplate.Key.Substring(SetObjectTemplate.Key.LastIndexOf('/') + 1) : "NULL");
                Instance.Font.Draw(s.ToUpper(), 10, Instance.TextY += 50, 1);
            }

            int i = 1000;

            public override void Mouse(float x, float y, float scale, MouseState mouseState)
            {
                if (mouseState.IsButtonDown(MouseButton.Left) && SetObjectTemplate != null)
                {
                    var sobj = new S2HDSetData.SetObject();
                    sobj.Key = SetObjectTemplate.Key;
                    sobj.GUID = Guid.NewGuid().ToString();
                    sobj.Name = $"{SetObjectTemplate.Key.Substring(SetObjectTemplate.Key.LastIndexOf('/') + 1)} {i++}";
                    sobj.X = (int)x;
                    sobj.Y = (int)y;
                    if (SetObjectTemplate.ExtraData.Count > 0)
                    {
                        foreach (var pair in SetObjectTemplate.ExtraData)
                        {
                            sobj.ExtraData.Add(pair.Key, pair.Value);
                        }
                    }
                    Instance.SetData.Objects.Add(sobj);
                    Instance.SetAniLink.Add(sobj, Instance.SetAniLink.FirstOrDefault(t => t.Key.Key == sobj.Key).Value);
                }else if (mouseState.IsButtonDown(MouseButton.Middle))
                {
                    for (int i = 0; i < Instance.SetData.Objects.Count; ++i)
                    {
                        var obj = Instance.SetData.Objects[i];
                        if (Instance.SetAniLink.ContainsKey(obj))
                        {
                            var ani = Instance.SetAniLink[obj];
                            var ani2 = ani.Animations[0];
                            var frame = ani2.Frames[0];
                            int texture = Instance.SetTextures[ani][frame.Texture];
                            float xx = (obj.X - (frame.Width / 2));
                            float yy = (obj.Y - (frame.Height / 2));

                            if (x >= xx && x <= xx + frame.Width && y >= yy && y <= yy + frame.Height)
                            {
                                SetObjectTemplate = obj;
                                break;
                            }

                        }
                    }
                }
            }
        }

    }
}
