using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HedgeEdit
{
    public class S2HDSetData : FileBase
    {

        public int LayerID = 0;
        public List<SetObject> Objects = new List<SetObject>();

        public override void Load(Stream fileStream)
        {
            var xml = XDocument.Load(fileStream);
            var tiles = xml.Root.Element("Definitions");
            if (tiles.Attribute("DefaultLayer") != null)
            {
                LayerID = int.Parse(tiles.Attribute("DefaultLayer").Value);
            }

            foreach (var def1Elem in tiles.Elements("Definition"))
            {
                var commonElem = def1Elem.Element("Common");
                var setObj = new SetObject();
                setObj.Group = commonElem.Name.LocalName;
                setObj.Key = commonElem.Element("Key").Value;
                setObj.UID = commonElem.Element("Uid").Value;
                setObj.Name = commonElem.Element("Name").Value;
                var posElem = commonElem.Element("Position");
                setObj.X = int.Parse(posElem.Attribute("X").Value);
                setObj.Y = int.Parse(posElem.Attribute("Y").Value);
                if (def1Elem.Element("Behaviour") != null)
                {
                    var behElem = def1Elem.Element("Behaviour");
                    foreach (var elem in behElem.Elements())
                    {
                        setObj.ExtraData.Add(elem.Name.ToString(), elem.Value);
                    }
                }

                Objects.Add(setObj);
            }
        }

        public class SetObject
        {
            public string Type;
            public string Group; // Could be a group or common definitions
            public string UID;
            public string Key;
            public string Name;
            public int X;
            public int Y;
            public Dictionary<string, string> ExtraData = new Dictionary<string, string>();
        }

    }
}
