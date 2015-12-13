using SFML.Graphics;
using System;

namespace GameCore.Core
{
    public class Spritesheet
    {

        private Sprite[] sprites;

        public Spritesheet(Texture texture, int cols, int rows, int width, int height)
        {
            sprites = new Sprite[cols*rows];
            for(int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    sprites[x + y] = new Sprite(texture);
                    sprites[x + y].TextureRect = new IntRect(x*width, y*height, width, height);
                }
            }
        }

        public Sprite[] GetSprites()
        {
            return sprites;
        }
    }
}
