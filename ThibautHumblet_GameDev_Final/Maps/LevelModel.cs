using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThibautHumblet_GameDev_Final.ParallaxBackground;
using ThibautHumblet_GameDev_Final.Sprites;

namespace ThibautHumblet_GameDev_Final.Maps
{
    public class LevelModel
    {
        public readonly Player Player;
        public List<Parallax> Parallaxes;

        public LevelModel(Player player)
        {
            Player = player;

            Parallaxes = new List<Parallax>();
        }
    }
}
