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
        public List<string> Test = new List<string>();

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
                foreach (var elem in def1Elem.Elements())
                {
                    if (!Test.Contains(elem.Name.ToString()))
                    {
                        Test.Add(elem.Name.ToString());
                    }
                }
                var setObj = new SetObject();
                setObj.Group = commonElem.Name.LocalName;
                setObj.Key = commonElem.Element("Key").Value;
                setObj.UID = commonElem.Element("Uid").Value;
                setObj.Name = commonElem.Element("Name").Value;
                if (commonElem.Element("Layer") != null)
                    setObj.Layer = commonElem.Element("Layer").Value;
                var posElem = commonElem.Element("Position");
                setObj.X = int.Parse(posElem.Attribute("X").Value);
                setObj.Y = int.Parse(posElem.Attribute("Y").Value);
                if (def1Elem.Element("Behaviour") != null)
                {
                    var behElem = def1Elem.Element("Behaviour");
                    foreach (var elem in behElem.Elements())
                    {
                        if (elem.Elements().Count() > 0)
                        {
                            setObj.ExtraData.Add(elem.Name.ToString(), elem);
                        }
                        else
                        {
                            setObj.ExtraData.Add(elem.Name.ToString(), elem.Value);
                        }
                    }
                }

                Objects.Add(setObj);
            }
        }

        public override void Save(Stream fileStream)
        {
            var xml = new XDocument();
            var binding = new XElement("Binding");
            var defs = new XElement("Definitions");
            defs.SetAttributeValue("DefaultLayer", LayerID);

            foreach (var sobj in Objects)
            {
                var def = new XElement("Definition");
                var common = new XElement("Common");
                var key = new XElement("Key", sobj.Key);
                var uid = new XElement("Uid", sobj.UID);
                var name = new XElement("Name", sobj.Name);
                var layer = new XElement("Layer", sobj.Layer);
                var pos = new XElement("Position");
                pos.SetAttributeValue("X", sobj.X);
                pos.SetAttributeValue("Y", sobj.Y);
                common.Add(key);
                common.Add(uid);
                common.Add(name);
                if (sobj.Layer != null)
                    common.Add(layer);
                common.Add(pos);
                if (sobj.ExtraData.Count > 0)
                {
                    var beh = new XElement("Behaviour");
                    foreach (var val in sobj.ExtraData)
                    {
                        if (val.Value is string str)
                        {
                            var elem = new XElement(val.Key, str);
                            beh.Add(elem);
                        }else
                        {
                            beh.Add(val.Value);
                        }
                    }
                    def.Add(beh);
                }
                def.Add(common);
                defs.Add(def);
            }
            binding.Add(defs);
            xml.Add(binding);
            xml.Save(fileStream);
        }

        public class SetObject
        {
            public string Type;
            public string Group; // Could be a group or common definitions
            public string UID;
            public string Key;
            public string Layer;
            public string Name;
            public int X;
            public int Y;
            public Dictionary<string, object> ExtraData = new Dictionary<string, object>();
        }

    }
}
