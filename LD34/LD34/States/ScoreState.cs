﻿using GameCore.States;
using System;
using GameCore.Objects;
using GameCore.Core;
using SFML.Graphics;
using LD34.Menu;
using SFML.System;
using LD34.Handlers;

namespace LD34.States
{
    public class ScoreState : GameState
    {
        public ScoreState(Game game) : base(game)
        {
            LeaderboardHandler.HighscoreRequestAsync(HandleHighscore);

            Button backButton = (Button)AddGameObject(nameof(Button));
            backButton.SetActionCommand("back");
            backButton.SetActionDelay(.5f);
            backButton.Position = new Vector2f(Game.Window.Size.X / 2, 450);
            backButton.SetSize(18);
            backButton.SetText("Back");

            Label title = (Label)AddGameObject(nameof(Label));
            title.Position = new Vector2f(Game.Window.Size.X / 2, 10);
            title.SetSize(48);
            title.SetFont(Assets.Fonts.ID.Header);
            title.SetText("Scoreboard");

            Game.PlayMusic(Assets.Musics.ID.Menu);
        }

        private void HandleHighscore(Highscore[] scores)
        {
            string[,] table = new string[scores.Length+1, 2];
            table[0, 0] = "Username";
            table[0, 1] = "Score";
            for (int i = 0; i < scores.Length; i++)
            {
                table[i+1,0] = scores[i].Username;
                table[i+1,1] = scores[i].Score.ToString();
            }

            Table title = (Table)AddGameObject(nameof(Table));
            title.Position = new Vector2f(Game.Window.Size.X / 2, 100);
            title.SetSize(18);
            title.SetFont(Assets.Fonts.ID.Default);
            title.SetData(table);
        }

        public override Entity AddEntity(string type)
        {
            throw new NotImplementedException();
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);
        }

        public override GameObject AddGameObject(string type)
        {
            GameObject tmpGameObject = null;
            switch (type)
            {
                case nameof(Button):
                    tmpGameObject = new Button(ButtonHandler, "", new Vector2f(0, 0), this);
                    GameObjects.Add(tmpGameObject);
                    break;
                case nameof(Label):
                    tmpGameObject = new Label("", new Vector2f(0, 0), this);
                    GameObjects.Add(tmpGameObject);
                    break;
                case nameof(Table):
                    tmpGameObject = new Table(null, new Vector2f(0, 0), this);
                    GameObjects.Add(tmpGameObject);
                    break;
                default:
                    throw new Exception("GameObject not found in this State");
            }
            return tmpGameObject;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public void ButtonHandler(string actionCommand, bool perform)
        {
            switch (actionCommand)
            {
                case "back":
                    if (perform)
                    {
                        Game.ChangeState(new MenuState(Game));
                    }
                    break;
            }
        }
    }
}
