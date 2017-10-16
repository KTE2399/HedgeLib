using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HedgeEdit
{
    public class ObjectRenderAttitudes
    {

        public static void Update(ref S2HDSetData.SetObject sobj, ref AniGroup anigroup, ref AniGroup.Animation ani, ref bool fliph, ref bool flipv, ref bool rot)
        {
            if (sobj.Key.Contains("SPRING"))
            {
                if (sobj.ExtraData.ContainsKey("Direction"))
                {
                    string dir = (string)sobj.ExtraData["Direction"];
                    if (dir == "UpRight")
                    {
                        if (sobj.ExtraData.ContainsKey("Strength"))
                            ani = anigroup.Animations[4];
                        else
                            ani = anigroup.Animations[6];
                    }
                    else if (dir == "UpLeft")
                    {
                        if (sobj.ExtraData.ContainsKey("Strength"))
                            ani = anigroup.Animations[4];
                        else
                            ani = anigroup.Animations[6];
                        fliph = true;
                    }
                    else if (dir == "Down")
                    {
                        if (sobj.ExtraData.ContainsKey("Strength"))
                            ani = anigroup.Animations[0];
                        else
                            ani = anigroup.Animations[2];
                        flipv = true;
                    }
                    else if (dir == "Down")
                    {
                        if (sobj.ExtraData.ContainsKey("Strength"))
                            ani = anigroup.Animations[0];
                        else
                            ani = anigroup.Animations[2];
                    }
                }
                else
                {
                    rot = true;
                }
                if (!sobj.ExtraData.ContainsKey("Strength"))
                {
                    ani = anigroup.Animations[2];
                }
            }else
            {
                if (sobj.ExtraData.ContainsKey("Direction"))
                {
                    string dir = (string)sobj.ExtraData["Direction"];
                    if (dir == "Down")
                    {
                        flipv = true;
                    }
                }
            }
        }

    }
}
