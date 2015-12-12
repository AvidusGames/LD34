using GameCore.Core;
using GameCore.States;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LD34.Objects;

namespace LD34
{
    class Program
    {
        static void Main(string[] args)
        {
			RenderWindow window = new RenderWindow(new SFML.Window.VideoMode(800, 600), "Test");
			window.SetFramerateLimit(240);

			Game game = new Game(window);
			game.Start(Init);
        }

        public static void Init(Game game)
        {
			game.ChangeState(new MainState(game));

            //try
            //{
            //    game.LoadTexture(GameCore.Core.Textures.ID.Background, "Assets/Textures/bg.png");
            //    game.LoadTexture(GameCore.Core.Textures.ID.Player, "Assets/Textures/Player.png");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("ERROR: Could not load resources ({0})!", ex.GetBaseException());
            //    return;
            //}
        }
    }
}
