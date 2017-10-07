using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace HedgeEdit
{
    public class TileSet : FileBase
    {

        public List<string> Textures = new List<string>();
        public Tile[] Tiles = new Tile[4096];

        public override void Load(Stream fileStream)
        {
            var xml = XDocument.Load(fileStream);
            var textures = xml.Root.Element("textures");
            var tiles = xml.Root.Element("tiles");

            foreach (var texElem in textures.Elements("texture"))
            {
                Textures.Add(texElem.Value);
            }

            foreach (var tileElem in tiles.Elements("tile"))
            {
                var tile = new Tile();
                int id = int.Parse(tileElem.Attribute("id").Value);
                if (tileElem.Attribute("opaque") != null)
                {
                    tile.Opaque = bool.Parse(tileElem.Attribute("opaque").Value);
                }

                if (tileElem.Attribute("texture") != null)
                {
                    var frame = new Frame();
                    frame.Texture = int.Parse(tileElem.Attribute("texture").Value);
                    frame.X = int.Parse(tileElem.Attribute("x").Value);
                    frame.Y = int.Parse(tileElem.Attribute("y").Value);
                    tile.Frames.Add(frame);
                }else
                {
                    foreach (var frameElem in tileElem.Elements("frame"))
                    {
                        var frame = new Frame();
                        frame.Texture = int.Parse(frameElem.Attribute("texture").Value);
                        frame.X = int.Parse(frameElem.Attribute("x").Value);
                        frame.Y = int.Parse(frameElem.Attribute("y").Value);
                        tile.Frames.Add(frame);
                    }
                }

                if (tileElem.Attribute("delay") != null)
                {
                    tile.Delay = int.Parse(tileElem.Attribute("delay").Value);
                }
                Tiles[id] = tile;
            }
        }


        public class Tile
        {
            public bool Opaque;
            public int Delay;
            public List<Frame> Frames = new List<Frame>();
        }

        public class Frame
        {
            public int Texture;
            public int X;
            public int Y;
        }

    }
}
