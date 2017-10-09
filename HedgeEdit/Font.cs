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
        public Dictionary<char, Vector4> Characters = new Dictionary<char, Vector4>();

        public string Name;
        public string Shape;
        public string ResKey;

        public void LoadFont(string resourceKey)
        {
            string defPath = MainFrm.GetFullPathFromSonicOrcaPath("SONICORCA/FONTS/" + resourceKey + ".font.xml");
            ResKey = resourceKey;
            Load(defPath);
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
                var vec = Characters[c];
                Viewport.DrawTexturedRect(x, y, vec.Z, vec.W, vec.X, vec.Y, vec.Z, vec.W);
                x += vec.Z + spacing;
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
                var rect = new Vector4();
                var rectElem = chardefElem.Element("rect");
                char c = chardefElem.Attribute("char").Value[0];
                rect.X = int.Parse(rectElem.Attribute("x").Value);
                rect.Y = int.Parse(rectElem.Attribute("y").Value);
                rect.Z = int.Parse(rectElem.Attribute("w").Value) + 4;
                rect.W = int.Parse(rectElem.Attribute("h").Value);

                if (chardefElem.Attribute("offset") != null)
                {
                    rect.X += int.Parse(chardefElem.Element("offset").Attribute("x").Value);
                    rect.Y += int.Parse(chardefElem.Element("offset").Attribute("y").Value);
                }
                Characters.Add(c, rect);
            }

        }
    }
}
