using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MMG.Managers.Sprites
{

    public class MMGSprite : MMGObject
    {
        protected Rectangle _spriteBox;
        protected Texture2D _texture;
        protected MMGSpriteAnimation _currentAnimation;
        protected List<MMGSpriteAnimation> _animations;
        protected bool _isAnimated;
        protected int _forceFrame;

        public Color color;
        public float depth;

        public MMGSprite() : base()
        {
            color = Color.White;

            depth = 0;
        }


        public void loadGraphic(Texture2D spriteImage)
        {
            _texture = spriteImage;
            _spriteBox = new Rectangle(0, 0, _texture.Width, _texture.Height);
            _isAnimated = false;

            this.width = _texture.Width;
            this.height = _texture.Height;
        }

        public void loadGraphic(Texture2D spriteImage, int Width, int Height, int defaultFrame)
        {
            _texture = spriteImage;

            //_texture. = SurfaceFormat.

            this.width = Width;
            this.height = Height;

            _spriteBox = new Rectangle(0, 0, Width, Height);
            _isAnimated = true;
            _forceFrame = defaultFrame;
        }

        public void addAnimation(string Name, int[] Frames, float FrameLength, bool Looped)
        {
            if(_animations == null)
                _animations = new List<MMGSpriteAnimation>();

            _animations.Add(new MMGSpriteAnimation(Name, Frames, FrameLength, Looped));
        }

        public void play(string Name, GameTime gameTime, bool forceStart)
        {
            if (!_isAnimated)
                return;

            if (_currentAnimation != null)
            {
                if (!forceStart && _currentAnimation.name.Equals(Name))
                    return;
            }
            foreach(MMGSpriteAnimation s in _animations)
            {
                if (s.name.Equals(Name))
                {
                    _currentAnimation = s;
                    s.play(gameTime);
                    break;
                }
            }
        }

        public override void draw(GameTime time, SpriteBatch spriteBatch)
        {
            base.draw(time, spriteBatch);

            if (_currentAnimation == null)
            {
                _spriteBox.X = (_spriteBox.Width * _forceFrame) % _texture.Width;
                _spriteBox.Y = _spriteBox.Height * ((_spriteBox.Width * _forceFrame) / _texture.Width);

                spriteBatch.Draw(_texture, Position, _spriteBox, color, rotation, orgin, scale, SpriteEffects.None, depth);
            }
            else 
            {
                int f = _currentAnimation.getCurrentFrame(time);

                _spriteBox.X = (_spriteBox.Width * f) % _texture.Width;
                _spriteBox.Y = _spriteBox.Height * ((_spriteBox.Width * f) / _texture.Width);

                spriteBatch.Draw(_texture, Position, _spriteBox, color, rotation, orgin, scale, SpriteEffects.None, depth);

                if (false)
                {
                    foreach (Vector2 v in getPoints())
                    {
                        spriteBatch.Draw(_texture,
                            new Rectangle((int)v.X, (int    )v.Y, 4, 4),
                            Color.Black);

                    }
                }
                            
            }
        }

    }
}