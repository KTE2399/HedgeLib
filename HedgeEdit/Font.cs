using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using OpenTK;

namespace HedgeEdit
{
    public class Font : FileBase
    {

        public static Dictionary<string, Font> Fonts = new Dictionary<string, Font>();
        public List<string> Textures = new List<string>();
        public Dictionary<char, FontChar> Characters = new Dictionary<char, FontChar>();

        public string Name;
        public string Shape;
        public string ResKey;

        public int Texture = 0;

        public void LoadFont(string resourceKey)
        {
            string defPath = MainFrm.GetFullPathFromSonicOrcaPath("SONICORCA/FONTS/" + resourceKey + ".font.xml");
            ResKey = resourceKey;
            Load(defPath);
        }

        public void LoadFontTexture(int i)
        {
            string defPath = MainFrm.GetFullPathFromSonicOrcaPath(Textures[i]);
            Texture = Viewport.LoadTexture(defPath);
        }

        public void Draw(string str, float x, float y, float spacing)
        {
            foreach (char c in str)
            {
                if (c == ' ')
                {
                    x += 24;
                    continue;
                }
                var fontChar = Characters[c];
                OpenTK.Graphics.OpenGL.GL.Color3(0.2f, 0.2f, 0.2f);
                Viewport.DrawTexturedRect(x + fontChar.OffsetX + 5, y + fontChar.OffsetY + 5, fontChar.TextureW, fontChar.TextureH, fontChar.TextureX, fontChar.TextureY, fontChar.TextureW, fontChar.TextureH, Texture);
                OpenTK.Graphics.OpenGL.GL.Color3(1f, 1f, 1f);
                Viewport.DrawTexturedRect(x + fontChar.OffsetX, y + fontChar.OffsetY, fontChar.TextureW, fontChar.TextureH, fontChar.TextureX, fontChar.TextureY, fontChar.TextureW, fontChar.TextureH, Texture);
                x += fontChar.TextureW + spacing;
            }
        }

        public override void Load(Stream fileStream)
        {
            var xml = XDocument.Load(fileStream);
            var chardefs = xml.Root.Element("chardefs");
            Name = xml.Root.Element("name").Value;
            Shape = xml.Root.Element("shape").Value;
            xml.Root.Elements("overlay").ToList().ForEach(t => Textures.Add(MainFrm.GetFullPathFromSonicOrcaPath("SONICORCA/FONTS/" + ResKey + t.Value + ".png")));
            foreach (var chardefElem in chardefs.Elements("chardef"))
            {
                var fontChar = new FontChar();
                var rectElem = chardefElem.Element("rect");
                char c = chardefElem.Attribute("char").Value[0];
                fontChar.TextureX = int.Parse(rectElem.Attribute("x").Value);
                fontChar.TextureY = int.Parse(rectElem.Attribute("y").Value);
                fontChar.TextureW = int.Parse(rectElem.Attribute("w").Value) + 4;
                fontChar.TextureH = int.Parse(rectElem.Attribute("h").Value);

                if (chardefElem.Element("offset") != null)
                {
                    fontChar.OffsetX = int.Parse(chardefElem.Element("offset").Attribute("x").Value);
                    fontChar.OffsetY = int.Parse(chardefElem.Element("offset").Attribute("y").Value);
                }
                Characters.Add(c, fontChar);
            }

        }

        public class FontChar
        {
            public int TextureX;
            public int TextureY;
            public int TextureW;
            public int TextureH;
            public int OffsetX;
            public int OffsetY;
        }


    }
}
