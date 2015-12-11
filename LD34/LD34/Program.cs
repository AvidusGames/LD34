using GameCore.Core;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD34
{
    class Program
    {
        static void Main(string[] args)
        {
			RenderWindow window = new RenderWindow(new SFML.Window.VideoMode(200, 300), "Test");
			Game game = new Game(window);
			game.Start();
        }
    }
}
