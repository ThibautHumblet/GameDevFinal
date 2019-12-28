﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThibautHumblet_GameDev_Final.Animations;
using ThibautHumblet_GameDev_Final.UserInterface;

namespace ThibautHumblet_GameDev_Final.Sprites
{
    public class Player: Sprite
    {
        Input _input;

        private bool _isOnGround = false;

        private bool _jumping = false;

        public Vector2 Velocity;
        /// <summary>
        /// These are the types of attributes to only change on level-up
        /// </summary>
        

        public Player(Input input, Dictionary<string, Animation> animations) : base(animations)
        {
            _input = input;
        }

        public override void Update(GameTime gameTime)
        {
            if (Velocity.Y >= 0)
                _jumping = false;

            if (_isOnGround)
            {
                if (_input.Keypress(Keys.Space) || _input.Keypress(Keys.Up))
                {
                    Velocity.Y = -12f;
                    _jumping = true;
                }
            }
            else
            {
                Velocity.Y += 0.50f;
            }
            if (_input.Keydown(Keys.Left))
            {
                _position.X -= 3;
            }
            if (_input.Keydown(Keys.Right))
            {
                _position.X += 3;
            }

            SetAnimation();

            _animationManager.Update(gameTime);

            _isOnGround = false;
        }

        public void ApplyVelocity(GameTime gameTime)
        {
            //Position = new Vector2(Position.X, Position.Y + Velocity.Y);
            this.Y += Velocity.Y;
            this.X += Velocity.X;
        }

        private void SetAnimation()
        {
            if (Velocity.Y < 0)
            {
                _animationManager.Play(_animations["Jump"]);
            }
            else if (Velocity.Y > 0)
            {
                _animationManager.Play(_animations["Jump"]);
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
        }

        public override void OnCollide(Sprite sprite)
        {
            {
                var resultP1 = GetMinMax(this.Dots, GetNormals()[1]);
                var resultP2 = GetMinMax(sprite.Dots, GetNormals()[1]);

                var resultQ1 = GetMinMax(this.Dots, GetNormals()[0]);
                var resultQ2 = GetMinMax(sprite.Dots, GetNormals()[0]);

                var resultR1 = GetMinMax(this.Dots, sprite.GetNormals()[1]);
                var resultR2 = GetMinMax(sprite.Dots, sprite.GetNormals()[1]);

                var resultS1 = GetMinMax(this.Dots, sprite.GetNormals()[0]);
                var resultS2 = GetMinMax(sprite.Dots, sprite.GetNormals()[0]);

                float p1Min = resultP1[0];
                float p1Max = resultP1[1];
                float p2Min = resultP2[0];
                float p2Max = resultP2[1];

                float q1Min = resultQ1[0];
                float q1Max = resultQ1[1];
                float q2Min = resultQ2[0];
                float q2Max = resultQ2[1];

                float r1Min = resultR1[0];
                float r1Max = resultR1[1];
                float r2Min = resultR2[0];
                float r2Max = resultR2[1];

                float s1Min = resultS1[0];
                float s1Max = resultS1[1];
                float s2Min = resultS2[0];
                float s2Max = resultS2[1];

                var separate_P = p1Max < p2Min || p2Max < p1Min;
                var separate_Q = q1Max < q2Min || q2Max < q1Min;
                var separate_R = r1Max < r2Min || r2Max < r1Min;
                var separate_S = s1Max < s2Min || s2Max < s1Min;

                var isSeperated = separate_P || separate_Q || separate_R || separate_S;

                if (isSeperated)
                {

                }
                else
                {

                }
            }

            var test = sprite.Centre - (this.Centre);// + new Vector2(10, 25));

            var rotation = (float)Math.Atan2(test.Y, test.X);

            var rotation2 = Math.Abs(MathHelper.ToDegrees(rotation));

            if (rotation2 > 89 || rotation2 < 91)
            {

            }

            if (sprite.Y == 640)
            {

            }

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
                    }

                    if (onRight)
                    {
                        this.X = platform.Rectangle.Right + this.Rectangle.Width;
                    }


                    if (onTop)
                    {
                        if (!_jumping)
                        {
                            this._position.Y = platform.Rectangle.Top - this.Rectangle.Height;
                            Velocity.Y = 0;
                            _isOnGround = true;
                        }
                    }


                    break;

                default:
                    throw new Exception("Unexpected sprite: " + sprite.ToString());
            }
        }

        private List<float> GetMinMax(List<Vector2> dots, Vector2 axis)
        {
            var minProj = Vector2.Dot(dots[1], axis);
            var maxProj = Vector2.Dot(dots[1], axis);
            var minDot = 1;
            var maxDot = 1;

            for (int i = 2; i < dots.Count; i++)
            {
                var currProj = Vector2.Dot(dots[i], axis);

                if (minProj > currProj)
                {
                    minProj = currProj;
                    minDot = i;
                }

                if (currProj > maxProj)
                {
                    maxProj = currProj;
                    maxDot = i;
                }
            }

            return new List<float>()
      {
        minProj,
        maxProj,
      };
        }
    }
}
