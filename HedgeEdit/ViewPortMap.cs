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
        public int FontTextureID;
        public int[] TileTextures;

        public ViewPortMap(Map map, TileSet tileSet)
        {
            Map = map;
            TileSet = tileSet;
            FontTextureID = Viewport.LoadTexture(Editor.Instance.Font.Textures[1]);
            TileTextures = new int[tileSet.Textures.Count];
            for (int i = 0; i < TileTextures.Length; ++i)
            {
                TileTextures[i] = Viewport.LoadTexture("SONICORCA/LEVELS/EHZ/TILESET" + tileSet.Textures[i] + ".png");
            }
        }

        public void Draw(float x, float y, float xCam, float yCam, float scale)
        {
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
                        // Draws the Tile.
                        Viewport.DrawTexturedRect(xx * scale, yy * scale, 64 * scale, 64 * scale, tile.Frames[0].X, tile.Frames[0].Y, 64, 64, flipX, flipY, texture);
                    }
                    ii++;
                }
            }
            
            for (int i = 0; i < Editor.Instance.SetData.Objects.Count; ++i)
            {
                var obj = Editor.Instance.SetData.Objects[i];
                if (Editor.Instance.SetAniLink.ContainsKey(obj))
                {
                    var ani = Editor.Instance.SetAniLink[obj];
                    var ani2 = ani.Animations[0];
                    bool fliph = false;
                    bool flipv = false;

                    ObjectRenderAttitudes.Update(ref obj, ref ani, ref ani2, ref fliph, ref flipv);

                    var frame = ani2.Frames[0];
                    int texture = Editor.Instance.SetTextures[ani][frame.Texture];
                    float xx = (obj.X - frame.Width / 2 + x) * scale;
                    float yy = (obj.Y - frame.Height / 2 + y) * scale;

                    // Prevents Rending Tiles out of view
                    if (xx / scale + frame.Width + xCam < xCam || yy / scale + frame.Height + yCam < yCam ||
                        xx / scale - xCam > (Viewport.VP.Width * 2 / scale) - xCam || yy / scale - yCam > (Viewport.VP.Height * 2 / scale) - yCam)
                        continue;


                    Viewport.DrawTexturedRect(xx, yy, frame.Width * scale, frame.Height * scale,
                        frame.X, frame.Y, frame.Width, frame.Height, fliph, flipv, texture);
                    
                    // Extra Frames
                    if (obj.Key.Contains("MONITOR"))
                    {
                        string contents = (string)obj.ExtraData["Contents"];
                        var prevFrame = ani2.Frames[0];
                        xx = (obj.X - prevFrame.Width / 2 + x) * scale;
                        yy = (obj.Y - prevFrame.Height / 2 + y) * scale;
                        // 6 = Life

                            frame = ani.Animations[7].Frames[0];
                        if (contents == "Life")
                            frame = ani.Animations[5].Frames[0];
                        if (contents == "Robotnik")
                            frame = ani.Animations[7].Frames[0];
                        if (contents == "Ring")
                            frame = ani.Animations[8].Frames[0];
                        if (contents == "SpeedShoes")
                            frame = ani.Animations[9].Frames[0];
                        if (contents == "Barrier")
                            frame = ani.Animations[11].Frames[0];
                        if (contents == "Invincibility")
                            frame = ani.Animations[12].Frames[0];
                        if (contents == "Swap")
                            frame = ani.Animations[13].Frames[0];
                        if (contents == "Random")
                            frame = ani.Animations[14].Frames[0];

                        xx = (obj.X + 4 - frame.Width / 2 + x) * scale;
                        yy = (obj.Y - 10 - frame.Height / 2 + y) * scale;
                        texture = Editor.Instance.SetTextures[ani][frame.Texture];
                        Viewport.DrawTexturedRect(xx, yy, frame.Width * scale, frame.Height * scale,
                            frame.X, frame.Y, frame.Width, frame.Height, fliph, flipv, texture);
                        frame = prevFrame;
                        xx = (obj.X - frame.Width / 2 + x) * scale;
                        yy = (obj.Y - frame.Height / 2 + y) * scale;
                    }
                    
                    // Border
                    GL.BindTexture(TextureTarget.Texture2D, 0);
                    Viewport.DrawTexturedRect(xx, yy, frame.Width * scale, 2);
                    Viewport.DrawTexturedRect(xx, yy + frame.Height * scale, frame.Width * scale, 2);
                    Viewport.DrawTexturedRect(xx, yy, 2, frame.Height * scale);
                    Viewport.DrawTexturedRect(xx + frame.Width * scale, yy, 2, frame.Height * scale);

                }
            }
        }

        public void Mouse(float x, float y, float scale, MouseState mouseState)
        {
        }
    }
}
