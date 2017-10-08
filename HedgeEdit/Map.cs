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
    public class Map : FileBase
    {

        public List<Layer> Layers = new List<Layer>();

        public override void Load(Stream fileStream)
        {
            var xml = XDocument.Load(fileStream);
            var tiles = xml.Root.Element("tiles");

            foreach (var layerElem in tiles.Elements("layer"))
            {
                var layer = new Layer();
                var tiles2 = layerElem.Element("tiles");
                layer.Name = layerElem.Attribute("name").Value;
                foreach (var rowElem in tiles2.Elements("row"))
                {
                    string[] arr = StringtoStringArray(rowElem.Value);
                    //for (int i = 0; i < arr.Length; ++i)
                    //{
                    //    if (arr[i][0] == 'h' || arr[i][0] == 'v')
                    //        arr[i] = arr[i].Substring(1);
                    //}
                    layer.Rows.Add(arr);
                }
                Layers.Add(layer);
            }
        }

        public string[] StringtoStringArray(string array)
        {
            List<string> list = new List<string>();
            foreach (string s in array.Split(','))
            {
                list.Add(s);
            }
            return list.ToArray();
        }

        public class Layer
        {
            public string Name;
            public string MinimapColor;
            public List<string[]> Rows = new List<string[]>(); 
        }

    }
}
