using SFML.Graphics;
using SFML.System;
using System;

namespace GameCore.Core
{
    public static class Spritesheet
    {
        public static Sprite[] LoadSprites(Texture spritesheet, int cols, int rows, int width, int height)
        {
            Sprite[] sprites = new Sprite[cols*rows];
            for(int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    sprites[x + y] = new Sprite(spritesheet);
                    sprites[x + y].TextureRect = new IntRect(x*width, y*height, width, height);
                }
            }
            return sprites;
        }

        public static Sprite[] LoadSprites(Texture spritesheet, Vector2i count, Vector2i size)
        {
            int cols = count.X;
            int rows = count.Y;
            int width = size.X;
            int height = size.Y;

            Sprite[] sprites = new Sprite[cols * rows];
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    sprites[x + y] = new Sprite(spritesheet);
                    sprites[x + y].TextureRect = new IntRect(x * width, y * height, width, height);
                    sprites[x + y].Origin = new Vector2f(width, height);
                }
            }
            return sprites;
        }
    }
}
