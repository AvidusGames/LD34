using SFML.Graphics;
using System;

namespace GameCore.Core
{
    class Animation
    {
        private Sprite[] frames;
        private int currentFrame;
        private int numFrames;

        private int tick;
        private int delay;

        public Animation(Sprite[] _frames, int _delay)
        {
            frames = _frames;
            numFrames = _frames.Length;
            delay = _delay;
        }

        public void SetDelay(int _delay)
        {
            delay = _delay;
        }

        public void SetFrame(int _frame)
        {
            currentFrame = _frame;
        }

        public void Update()
        {
            if (++tick == delay)
            {
                currentFrame++;
                tick = 0;
            }
            if (currentFrame == numFrames)
            {
                currentFrame = 0;
            }
        }

        public int GetFrame()
        {
            return currentFrame;
        }

        public int getCount()
        {
            return tick;
        }

        public Sprite GetImage()
        {
            return frames[currentFrame];
        }
    }
}
