using HedgeLib;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;
using System.Windows.Forms;

namespace HedgeEdit
{
    public class ViewPortMap : IRenderable
    {

        public Map Map;
        public TileSet TileSet;
        public S2HDSetData SetData;
        public Font Font;
        public int FontTextureID;
        public int[] TileTextures;
        public Dictionary<AniGroup, int[]> SetTextures = new Dictionary<AniGroup, int[]>();
        public float? Dx = 0;
        public float? Dy = 0;


        public Dictionary<S2HDSetData.SetObject, AniGroup> SetAniLink = new Dictionary<S2HDSetData.SetObject, AniGroup>();

        public ViewPortMap(Map map, TileSet tileSet, S2HDSetData setData, Font font)
        {
            Map = map;
            TileSet = tileSet;
            SetData = setData;
            Font = font;
            FontTextureID = Viewport.LoadTexture(font.Textures[1]);
            TileTextures = new int[tileSet.Textures.Count];
            for (int i = 0; i < TileTextures.Length; ++i)
            {
                TileTextures[i] = Viewport.LoadTexture("SONICORCA/LEVELS/EHZ/TILESET" + tileSet.Textures[i] + ".png");
            }
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
        }

        public void Draw(float x, float y, float xCam, float yCam, float scale)
        {
            int count = 0;
            foreach (var layer in Map.Layers)
            {
                int ii = 0;
                foreach (string[] rows in layer.Rows)
                {
                    for (int i = 0; i < rows.Length; ++i)
                    {
                        // The ID of the tile
                        string id = rows[i];
                        // Remove the flipping from the ID
                        if (id[0] == 'h' || id[0] == 'v')
                            id = id.Substring(1);
                        // Check if the ID is null, No need to render theses
                        if (rows[i] == "0")
                            continue;
                        // Checks if the Tile exist
                        if (!TileSet.Tiles.ContainsKey(id))
                            continue;
                        var tile = TileSet.Tiles[id];
                        bool flipX = rows[i][0] == 'h';
                        bool flipY = rows[i][0] == 'v';
                        // The X and Y position of the tile without Scale
                        float xx = i * 64 + x;
                        float yy = ii * 64 + y;
                        // The OpenGL Texture ID
                        int texture = TileTextures[tile.Frames[0].Texture];
                        // Prevents Rending Tiles out of view
                        if (xx + 64 + xCam < xCam || yy + 64 + yCam < yCam ||
                            xx - xCam > (Viewport.VP.Width * 2 / scale) - xCam || yy - yCam > (Viewport.VP.Height * 2 / scale) - yCam)
                            continue;
                        count++;
                        // Draws the Tile.
                        Viewport.DrawTexturedRect(xx * scale, yy * scale, 64 * scale, 64 * scale, tile.Frames[0].X, tile.Frames[0].Y, 64, 64, flipX, flipY, texture);
                    }
                    ii++;
                }
            }

            GL.BindTexture(TextureTarget.Texture2D, FontTextureID);
            Font.Draw($"RENDERING: {count} TILES", 10, 10, 1);
            count = 0;
            GL.BindTexture(TextureTarget.Texture2D, 0);
            for (int i = 0; i < SetData.Objects.Count; ++i)
            {
                var obj = SetData.Objects[i];
                if (SetAniLink.ContainsKey(obj))
                {
                    var ani = SetAniLink[obj];
                    var ani2 = ani.Animations[0];
                    bool fliph = false;
                    bool flipv = false;
                    // TODO: Mode to another method
                    // Rotated Spring
                    if (obj.Key.Contains("SPRING"))
                    {
                        if (obj.ExtraData.ContainsKey("Direction"))
                        {
                            string dir = obj.ExtraData["Direction"];
                            if (dir == "UpRight")
                            {
                                if (obj.ExtraData.ContainsKey("Strength"))
                                    ani2 = ani.Animations[4];
                                else
                                    ani2 = ani.Animations[6];
                            }
                            else if (dir == "UpLeft")
                            {
                                if (obj.ExtraData.ContainsKey("Strength"))
                                    ani2 = ani.Animations[4];
                                else
                                    ani2 = ani.Animations[6];
                                fliph = true;
                            }
                            else if (dir == "Down")
                            {
                                if (obj.ExtraData.ContainsKey("Strength"))
                                    ani2 = ani.Animations[0];
                                else
                                    ani2 = ani.Animations[2];
                                flipv = true;
                            }
                            else if (dir == "Down")
                            {
                                if (obj.ExtraData.ContainsKey("Strength"))
                                    ani2 = ani.Animations[0];
                                else
                                    ani2 = ani.Animations[2];
                            }
                        }
                        else if(obj.ExtraData.ContainsKey("Strength"))
                        {
                            if (obj.ExtraData.ContainsKey("Strength"))
                                ani2 = ani.Animations[0];
                            else
                                ani2 = ani.Animations[2];
                        }
                    }
                    if (obj.Key.Contains("SPIKES"))
                    {
                        if (obj.ExtraData.ContainsKey("Direction"))
                        {
                            string dir = obj.ExtraData["Direction"];
                            if (dir == "Down")
                            {
                                flipv = true;
                            }
                        }
                    }
                    var frame = ani2.Frames[0];
                    int texture = SetTextures[ani][frame.Texture];
                    float xx = (obj.X - frame.Width / 2 + x) * scale;
                    float yy = (obj.Y - frame.Height / 2 + y) * scale;

                    count++;

                    Viewport.DrawTexturedRect(xx, yy, frame.Width * scale, frame.Height * scale,
                        frame.X, frame.Y, frame.Width, frame.Height, fliph, flipv, texture);

                    // Border
                    GL.BindTexture(TextureTarget.Texture2D, 0);
                    Viewport.DrawTexturedRect(xx, yy, frame.Width * scale, 2);
                    Viewport.DrawTexturedRect(xx, yy + frame.Height * scale, frame.Width * scale, 2);
                    Viewport.DrawTexturedRect(xx, yy, 2, frame.Height * scale);
                    Viewport.DrawTexturedRect(xx + frame.Width * scale, yy, 2, frame.Height * scale);
                }
            }
            GL.BindTexture(TextureTarget.Texture2D, FontTextureID);
            Font.Draw($"RENDERING: {count} OBJECTS", 10, 60, 1);
            if (Editor.Instance.SelectedObject != null)
            {
                string s = "Selected Object: " + Editor.Instance.SelectedObject.Name;
                Font.Draw(s.ToUpper(), 10, 110, 1);
            }
        }

        public void Mouse(float x, float y, float scale, MouseState mouseState)
        {
            if (Dx != null && Dy != null && !mouseState.IsButtonDown(MouseButton.Left))
            {
                Dx = Dy = null;
            }
            else if (Editor.Instance.SelectedObject != null && Dx != null && Dy != null)
            {
                var obj = Editor.Instance.SelectedObject;
                obj.X = (int)(x + Dx);
                obj.Y = (int)(y + Dy);
                MainFrm.Instance.UpdatePos(obj.X, obj.Y);
            }
            else
            {
                for (int i = 0; i < SetData.Objects.Count; ++i)
                {
                    var obj = SetData.Objects[i];
                    if (SetAniLink.ContainsKey(obj))
                    {
                        var ani = SetAniLink[obj];
                        var ani2 = ani.Animations[0];
                        var frame = ani2.Frames[0];
                        int texture = SetTextures[ani][frame.Texture];
                        float xx = (obj.X - (frame.Width / 2));
                        float yy = (obj.Y - (frame.Height / 2));

                        if (x >= xx && x <= xx + frame.Width && y >= yy && y <= yy + frame.Height && mouseState.IsButtonDown(MouseButton.Left))
                        {
                            //Viewport.DrawTexturedRect(0, 0, 100, 100);
                            //MessageBox.Show(obj.Name);
                            Editor.Instance.SelectedObject = obj;
                            if (Dx == null && Dy == null)
                            {
                                Dx = obj.X - x;
                                Dy = obj.Y - y;
                            }
                            break;
                        }

                    }
                }
            }
        }
    }
}
