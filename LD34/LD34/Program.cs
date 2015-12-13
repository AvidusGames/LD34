﻿using GameCore.Core;
using SFML.Graphics;
using System;
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
                game.LoadSound(Assets.Sounds.ID.Jump, "Assets/SFX/jump1.wav");
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
