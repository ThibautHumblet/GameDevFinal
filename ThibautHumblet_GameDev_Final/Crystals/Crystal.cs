using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThibautHumblet_GameDev_Final.Crystals
{
    public class Crystal
    {
        static public bool gotCrystal1, gotCrystal2, gotCrystal3, gotCrystal4;

        public Crystal()
        {

        }

        public static void ResetCrystals()
        {
            gotCrystal1 = false;
            gotCrystal2 = false;
            gotCrystal3 = false;
            gotCrystal4 = false;
        }

        public void Update()
        {
            if (gotCrystal1 && gotCrystal2 && gotCrystal3 && gotCrystal4)
            {
                Game1.Level++;
            }
        }

        public void Draw()
        {

        }
    }
}
