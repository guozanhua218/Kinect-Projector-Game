using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MMG.Managers.Sprites
{
    public class MMGSpriteAnimation
    {
        public string name;
        public float frameLength;
        public int[] frames;
        public bool loop;

        protected int _frameIndex;
        protected double _startTime;

        public MMGSpriteAnimation(string Name, int[] Frames, float FrameLength, bool Looped)
        {
            name = Name;
            frames = Frames;
            frameLength = FrameLength;
            loop = Looped;

            _startTime = _frameIndex = 0;
        }

        public void play(GameTime gameTime)
        {
            _startTime = gameTime.TotalGameTime.TotalSeconds;
        }

        public int getCurrentFrame(GameTime gameTime)
        {
            double elapsed = gameTime.TotalGameTime.TotalSeconds - _startTime;
            _frameIndex = (int)(elapsed / frameLength);
            if (!loop && _frameIndex >= frames.Length)
                _frameIndex = frames.Length - 1;

            return frames[_frameIndex % frames.Length];
        }
    }
}
