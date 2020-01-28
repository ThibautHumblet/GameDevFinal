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
    public enum TileTypes
    {
        Crystal,
        Safe,
        Empty,
    }

    public class Tile : Sprite, IMoveable
    {
        public Vector2 Velocity { get; set; }

        public TileTypes TileType;

        public Tile(Texture2D texture)
          : base(texture)
        {
            TileType = TileTypes.Safe;
        }

        public override void Update(GameTime gameTime)
        {
            Position += Velocity;

            if (Rectangle.Right < 0)
                IsRemoved = false;
        }
    }
}
