using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThibautHumblet_GameDev_Final.Animations;
using ThibautHumblet_GameDev_Final.Sounds;
using ThibautHumblet_GameDev_Final.UserInterface;

namespace ThibautHumblet_GameDev_Final.Sprites
{
    public class Player: Sprite
    {
        Input _input;

        private bool _isOnGround = false;

        private bool _jumping = false;

        public static bool Dead = false;

        public Vector2 Velocity;
        public float Parallaxscroll;

        public bool WorldShift;

        bool isCollidingLeft = false, isCollidingRight = false;

        public Player(Input input, Dictionary<string, Animation> animations) : base(animations)
        {
            _input = input;
        }

        public override void Update(GameTime gameTime)
        {
            if (Game1.mainMenu && !Dead && _input.Keypress(Keys.Enter))
                Game1.mainMenu = false;
            if (!Game1.mainMenu && _input.Keypress(Keys.Escape))
                Game1.mainMenu = true;

            if (!Game1.mainMenu)
            {
                if (Velocity.Y >= 0)
                    _jumping = false;
                if (!Dead)
                {
                    if (_isOnGround)
                    {
                        if (_input.Keypress(Keys.Space) || _input.Keypress(Keys.Up))
                        {
                            Velocity.Y = -12f;
                            _jumping = true;
                            Sound.jump.Play();
                        }
                    }
                    else
                    {
                        Velocity.Y += 0.50f;
                    }
                    if (_input.Keydown(Keys.Left))
                    {
                        _position.X -= 6;
                        if (!isCollidingLeft)
                            Game1.AchtergrondPositie.X--;
                    }
                    else if (_input.Keydown(Keys.Right))
                    {
                        _position.X += 6;
                        if (!isCollidingRight)
                            Game1.AchtergrondPositie.X++;
                    }

                    if (_input.Keypress(Keys.RightControl))
                    {
                        if (WorldShift == false)
                            WorldShift = true;
                        else
                            WorldShift = false;
                    }

                }
                SetAnimation();

                _animationManager.Update(gameTime);

                _isOnGround = false;
            }
        }

        public void ApplyVelocity(GameTime gameTime)
        {
            if (!Game1.mainMenu)
            {
                this.Y += Velocity.Y;
                this.X += Velocity.X;
            }
        }

        private void SetAnimation()
        {
            if (!Dead)
            {
                if (Velocity.Y < 0)
                {
                    _animationManager.Play(_animations["JumpStart"]);
                }
                else if (Velocity.Y > 0)
                {
                    _animationManager.Play(_animations["JumpEnd"]);
                }
                else if (_input.Keydown(Keys.Right))
                {
                    _animationManager.Play(_animations["Walk"]);
                }
                else if (_input.Keydown(Keys.Left))
                {
                    _animationManager.Play(_animations["Walk"]);
                }
                else
                {
                    _animationManager.Play(_animations["Idle"]);
                }
            } else
            {
                _animationManager.Play(_animations["Dead"]);
                if (AnimationManager.DonePlaying)
                Game1.mainMenu = true;
            }
        }

        public override void OnCollide(Sprite sprite)
        {
            var test = sprite.Centre - (this.Centre);// + new Vector2(10, 25));

            var rotation = (float)Math.Atan2(test.Y, test.X);

            var rotation2 = Math.Abs(MathHelper.ToDegrees(rotation));

            bool onLeft = false;
            bool onRight = false;
            bool onTop = false;
            bool onBotton = false;

            int index = 0;

            for (int i = -45; i <= 315; i += 90)
            {
                if (rotation2 >= i && rotation2 < i + 90)
                {
                    switch (index)
                    {
                        case 0:

                            onLeft = true;

                            break;

                        case 1:

                            onTop = true;

                            break;

                        case 2:

                            onRight = true;

                            break;

                        case 3:

                            onBotton = true;

                            break;

                        default:
                            break;
                    }
                }
                index++;
            }

            switch (sprite)
            {
                case Tile platform:

                    if (onLeft)
                    {
                        this.X = platform.Rectangle.Left - this.Rectangle.Width;
                        isCollidingRight = true;
                    }
                    else
                        isCollidingRight = false;

                    if (onTop)
                    {
                        if (!_jumping)
                        {
                            this._position.Y = platform.Rectangle.Top - this.Rectangle.Height;
                            Velocity.Y = 0;
                            _isOnGround = true;
                        }
                    }

                    if (onRight)
                    {
                        this.X = platform.Rectangle.Right;
                        isCollidingLeft = true;
                    }
                    else
                        isCollidingLeft = false;

                    if (onBotton)
                    {
                        _isOnGround = false;
                        this.Y = platform.Rectangle.Bottom + this.Rectangle.Height;
                    }

                    if (platform.TileType == TileTypes.Spike)
                    {
                        Dead = true;
                    }
                    break;

                default:
                    throw new Exception("Unexpected sprite: " + sprite.ToString());
            }
        }
    }

}
