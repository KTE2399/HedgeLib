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
    public class Map : FileBase
    {

        public List<Layer> Layers = new List<Layer>();
        public List<Vector2> Collisions = new List<Vector2>();

        public override void Load(Stream fileStream)
        {
            var xml = XDocument.Load(fileStream);
            var tiles = xml.Root.Element("tiles");
            var collision = xml.Root.Element("collision");

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
            var vectors = collision.Element("vectors");
            foreach (var vectorElem in vectors.Elements("vector"))
            {
                string[] a = StringtoStringArray(vectorElem.Attribute("a").Value);
                string[] b = StringtoStringArray(vectorElem.Attribute("b").Value);
                Collisions.Add(new Vector2(float.Parse(a[0]), float.Parse(a[1])));
                Collisions.Add(new Vector2(float.Parse(b[0]), float.Parse(b[1])));
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
