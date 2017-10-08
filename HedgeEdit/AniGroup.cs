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
    public class AniGroup : FileBase
    {
        public List<string> Textures = new List<string>();
        public List<Animation> Animations = new List<Animation>();

        public override void Load(Stream fileStream)
        {
            var xml = XDocument.Load(fileStream);
            var textures = xml.Root.Element("textures");
            var animations = xml.Root.Element("animations");

            foreach (var texElem in textures.Elements("texture"))
            {
                Textures.Add(texElem.Value);
            }

            foreach (var animationElem in animations.Elements("animation"))
            {
                var animation = new Animation();
                
                foreach (var frameElem in animationElem.Elements("frame"))
                {
                    var frame = new Frame();
                    frame.Texture = int.Parse(frameElem.Attribute("texture").Value);
                    frame.X = int.Parse(frameElem.Attribute("x").Value);
                    frame.Y = int.Parse(frameElem.Attribute("y").Value);
                    frame.Width = int.Parse(frameElem.Attribute("w").Value);
                    frame.Height = int.Parse(frameElem.Attribute("h").Value);
                    animation.Frames.Add(frame);
                }
                
                if (animationElem.Attribute("delay") != null)
                {
                    animation.Delay = int.Parse(animationElem.Attribute("delay").Value);
                }
                if (animationElem.Attribute("next") != null)
                {
                    animation.Next = int.Parse(animationElem.Attribute("next").Value);
                }
                if (animationElem.Attribute("loop") != null)
                {
                    animation.Loop = int.Parse(animationElem.Attribute("loop").Value);
                }
                    Animations.Add(animation);
            }
        }


        public class Animation
        {
            public int Loop;
            public int Next;
            public int Delay;
            public List<Frame> Frames = new List<Frame>();
        }

        public class Frame
        {
            public int Texture;
            public int X;
            public int Y;
            public int Width;
            public int Height;
        }
    }
}
