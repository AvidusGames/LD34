using SFML.Graphics;
using SFML.System;
using System;

namespace GameCore.Core
{
    public class Animation
    {
        private Sprite[] frames;
        private Vector2f scale;
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

        public Animation(Animation animation)
        {
            frames = new Sprite[animation.numFrames];
            for(int i = 0; i < animation.numFrames; i++)
            {
                frames[i] = new Sprite(animation.frames[i].Texture);
                frames[i].TextureRect = animation.frames[i].TextureRect;
                frames[i].Origin = animation.frames[i].Origin;
            }
            numFrames = animation.numFrames;
            delay = animation.delay;
            scale = animation.scale;
        }

        public void SetScale(Vector2f _scale)
        {
            scale = _scale;
            foreach(Sprite frame in frames)
            {
                frame.Scale = _scale;
            }
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
