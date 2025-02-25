﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThibautHumblet_GameDev_Final.UserInterface;

namespace ThibautHumblet_GameDev_Final.Animations
{
    public class AnimationManager
    {
        private Animation _animation;

        private float _timer;

        private bool _updated;

        public static bool DonePlaying;

        public int FrameWidth
        {
            get
            {
                return _animation.FrameWidth;
            }
        }

        public int FrameHeight
        {
            get
            {
                return _animation.FrameHeight;
            }
        }

        public Vector2 Position { get; set; }

        public float Layer { get; set; }

        public AnimationManager(Animation animation)
        {
            _animation = animation;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!_updated)
                throw new Exception("Need to call 'Update' first");

            _updated = false;

            spriteBatch.Draw(_animation.Texture,
                             Position,
                             new Rectangle(_animation.CurrentFrame * _animation.FrameWidth,
                                           0,
                                           _animation.FrameWidth,
                                           _animation.FrameHeight),
                             Color.White,
                             0f,
                             new Vector2(0, 0),
                             1f,
                             SpriteEffects.None,
                             Layer);
        }

        public void Play(Animation animation)
        {
            if (_animation == animation)
                return;

            _animation = animation;

            _animation.CurrentFrame = 0;

            _timer = 0;
        }

        public void Stop()
        {
            _timer = 0f;

            _animation.CurrentFrame = 0;
        }

        public void Update(GameTime gameTime)
        {
            DonePlaying = false;
            _updated = true;

            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > _animation.FrameSpeed)
            {
                _timer = 0f;

                _animation.CurrentFrame++;

                if (_animation.CurrentFrame >= _animation.FrameCount)
                {
                    DonePlaying = true;
                    _animation.CurrentFrame = 0;
                }
            }
        }
    }
}
