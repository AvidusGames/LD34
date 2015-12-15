using GameCore.Core;
using SFML.Graphics;
using System;
using LD34.Handlers;
using LD34.States;
using SFML.System;

namespace LD34
{
    class Program
    {
        static void Main(string[] args)
        {
			RenderWindow window = new RenderWindow(new SFML.Window.VideoMode(800, 600), "Ludum Dare 34");
			window.SetFramerateLimit(240);/**

            LeaderboardHandler leaderboard = new LeaderboardHandler();

            Random rand = new Random();
            string username = Console.ReadLine();
            leaderboard.PutScoreAsync(username, rand.Next(0, 100));
            leaderboard.HighscoreRequestAsync(HandleHighscore);
    */
            LeaderboardHandler.Init();
            Game game = new Game(window);
			game.Start(Init);
        }

        public static void Init(Game game)
        {
            try
            {
                game.LoadFont(Assets.Fonts.ID.Default, "Assets/Fonts/ARCADECLASSIC.TTF");
                game.LoadFont(Assets.Fonts.ID.Header, "Assets/Fonts/crackman.ttf");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: Could not load font resources ({0})!", ex.GetBaseException());
                return;
            }

            try
            {
                game.LoadSound(Assets.Sounds.ID.Button, "Assets/SFX/button.wav");
                game.LoadSound(Assets.Sounds.ID.Lose, "Assets/SFX/lose.wav");
                game.LoadSound(Assets.Sounds.ID.Jump, "Assets/SFX/jump1.wav");
                game.LoadMusic(Assets.Musics.ID.Game, "Assets/Music/gameLoop.wav");
                game.LoadMusic(Assets.Musics.ID.Menu, "Assets/Music/menuLoop.wav");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: Could not load audio resources ({0})!", ex.GetBaseException());
                return;
            }

            try
            {
                game.LoadTexture(Assets.Textures.ID.Menu, "Assets/Textures/foreground_beginning.png");
                game.LoadTexture(Assets.Textures.ID.Towers, "Assets/Textures/background_towers.png");
                game.LoadTexture(Assets.Textures.ID.BHouses, "Assets/Textures/background-houses.png");
                game.LoadTexture(Assets.Textures.ID.FHouses, "Assets/Textures/foreground_houses.png");
                game.LoadTexture(Assets.Textures.ID.Tree, "Assets/Textures/foreground_tree.png");
                game.LoadTexture(Assets.Textures.ID.Help, "Assets/Textures/logo.png");
				game.LoadTexture(Assets.Textures.ID.SFML, "Assets/Textures/SFML.png");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: Could not load sound resources ({0})!", ex.GetBaseException());
                return;
            }

            try
            {
                game.LoadAnimation(Assets.Animations.ID.Jump, "Assets/Animations/Jump/jump_*.png", 30);         
                game.LoadAnimation(Assets.Animations.ID.Walk, "Assets/Animations/Walk/walk_*.png", 30);
                game.LoadAnimationSpritesheet(Assets.Animations.ID.Leaf, "Assets/Animations/Leaf/leaf.png", new Vector2i(3, 1), new Vector2i(16, 16), 240);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: Could not load animation resources ({0})!", ex.GetBaseException());
                return;
            }
            game.ChangeState(new MenuState(game));
        }
    }
}
