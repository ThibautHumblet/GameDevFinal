using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThibautHumblet_GameDev_Final.Components;
using ThibautHumblet_GameDev_Final.Sprites;

namespace ThibautHumblet_GameDev_Final.ParallaxBackground
{
    public class Parallax : Component
    {
        private List<Sprite> _sprites;

        private readonly Player _player;

        public Parallax(List<Texture2D> textures, Player player, float scrollingSpeed, bool constantSpeed = false)
        {
            _player = player;

            _sprites = new List<Sprite>();

            for (int i = 0; i < textures.Count; i++)
            {
                var texture = textures[i];

                _sprites.Add(new Sprite(texture)
                {
                    Position = new Vector2(i * texture.Width - Math.Min(i, i + 1), Game1.ScreenHeight - texture.Height),
                });
            }
        }

        public override void Update(GameTime gameTime)
        {
            CheckPosition();
        }

        private void CheckPosition()
        {
            for (int i = 0; i < _sprites.Count; i++)
            {
                var sprite = _sprites[i];

                if (sprite.Rectangle.Right <= 0)
                {
                    var index = i - 1;

                    if (index < 0)
                        index = _sprites.Count - 1;
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var sprite in _sprites)
                sprite.Draw(gameTime, spriteBatch);
        }
    }
}
