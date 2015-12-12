using GameCore.Core;
using GameCore.States;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LD34.Objects;
using LD34.Handlers;
using LD34.States;

namespace LD34
{
    class Program
    {
        static void Main(string[] args)
        {
			RenderWindow window = new RenderWindow(new SFML.Window.VideoMode(800, 600), "Test");
			window.SetFramerateLimit(240);/**
            LeaderboardHandler leaderboard = new LeaderboardHandler();

            Random rand = new Random();
            string username = Console.ReadLine();
            leaderboard.PutScoreAsync(username, rand.Next(0, 100));
            leaderboard.HighscoreRequestAsync(HandleHighscore);
    */
			Game game = new Game(window);
			game.Start(Init);
        }

        private static void HandleHighscore(Highscore[] scores)
        {
            foreach(Highscore score in scores)
            {
                Console.WriteLine(score.Username + ":" + score.Score);
            }
        }

        public static void Init(Game game)
        {
            try
            {
                game.LoadFont(GameCore.Core.Fonts.ID.Default, "Assets/Fonts/ARCADECLASSIC.TTF");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: Could not load resources ({0})!", ex.GetBaseException());
                return;
            }

            try
            {
                game.LoadSound(GameCore.Core.Sounds.ID.Jump, "Assets/SFX/jump1.wav");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: Could not load resources ({0})!", ex.GetBaseException());
                return;
            }

            try
            {
                game.LoadTexture(GameCore.Core.Textures.ID.Background, "Assets/Textures/bg.png");
                game.LoadTexture(GameCore.Core.Textures.ID.Player, "Assets/Textures/Player.png");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: Could not load resources ({0})!", ex.GetBaseException());
                return;
            }
            game.ChangeState(new MainState(game));
        }
    }
}
