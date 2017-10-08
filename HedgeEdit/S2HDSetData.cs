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
                var def2Elem = def1Elem.Elements().First();
                var setObj = new SetObject();
                setObj.Group = def2Elem.Name.LocalName;
                setObj.Key = def2Elem.Element("Key").Value;
                setObj.UID = def2Elem.Element("Uid").Value;
                setObj.Name = def2Elem.Element("Name").Value;
                var posElem = def2Elem.Element("Position");
                setObj.X = int.Parse(posElem.Attribute("X").Value);
                setObj.Y = int.Parse(posElem.Attribute("Y").Value);
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
        }

    }
}
