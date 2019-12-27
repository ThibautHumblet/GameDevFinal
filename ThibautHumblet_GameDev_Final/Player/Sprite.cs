using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThibautHumblet_GameDev_Final.UserInterface;

namespace ThibautHumblet_GameDev_Final.Player
{
    public class Sprite : SpriteManager
    {
        private bool _onGround;

        private bool _jumping;

        Input _input;

        public Sprite(Texture2D texture)
          : base(texture)
        {
        }

        public override void Update(GameTime gameTime, Input input)
        {
            _input = input;

            if (_input.Keydown(Keys.Left))
                _velocity.X = -2f;
            else if (_input.Keydown(Keys.Right))
                _velocity.X = 2f;
            else
                _velocity.X = 0;
            if (_input.Keypress(Keys.Space) || input.Keypress(Keys.Up))
                _jumping = true;
        }

        public override void OnCollide(SpriteManager sprite)
        {
            var test = sprite.Centre - (this.Centre);

            var onTop = this.WillIntersectTop(sprite);
            var onLeft = this.WillIntersectLeft(sprite);
            var onRight = this.WillIntersectRight(sprite);
            var onBotton = this.WillIntersectBottom(sprite);

            if (onTop)
            {
                _onGround = true;
                _velocity.Y = sprite.Rectangle.Top - this.Rectangle.Bottom;
                //this.Position = new Vector2(this.Position.X, sprite.Rectangle.Top - this.Origin.Y);
                //this._velocity.Y = 0;
            }
            else if (onLeft && _velocity.X > 0)
            {
                _velocity.X = 0;
            }
            else if (onRight && _velocity.X < 0)
            {
                _velocity.X = 0;
            }
            else if (onBotton)
            {
                _velocity.Y = 1;
            }
        }

        public override void ApplyPhysics(GameTime gameTime)
        {
            if (!_onGround)
                _velocity.Y += 0.2f;

            if (_onGround && _jumping)
            {
                _velocity.Y = -5f;
            }

            _onGround = false;
            _jumping = false;

            Position += _velocity;
        }
    }
}
