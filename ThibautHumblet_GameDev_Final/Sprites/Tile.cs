using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThibautHumblet_GameDev_Final.Interfaces;

namespace ThibautHumblet_GameDev_Final.Sprites
{
    public enum PlatformTypes
    {
        Dangerous,
        Safe,
    }

    public class Tile : Sprite, IMoveable
    {
        public Vector2 Velocity { get; set; }

        public PlatformTypes PlatformType;

        public Tile(Texture2D texture)
          : base(texture)
        {
            PlatformType = PlatformTypes.Safe;
        }

        public override void Update(GameTime gameTime)
        {
            Position += Velocity;

            if (Rectangle.Right < 0)
                IsRemoved = false;
        }
    }
}
