using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HedgeEdit
{
    public class ViewPortMap : IRenderable
    {

        public Map Map;
        public TileSet TileSet;
        public int[] Textures;

        public ViewPortMap(Map map, TileSet tileSet)
        {
            Map = map;
            TileSet = tileSet;
            Textures = new int[tileSet.Textures.Count];
            for (int i = 0; i < Textures.Length; ++i)
            {
                Textures[i] = Viewport.LoadTexture("TILESET" + tileSet.Textures[i] + ".png");
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
                        int texture = Textures[tile.Frames[0].Texture];
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
        }
    }
}
