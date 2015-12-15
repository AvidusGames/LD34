using GameCore.States;
using System;
using GameCore.Objects;
using GameCore.Core;
using SFML.Graphics;
using LD34.Menu;
using SFML.System;
using LD34.Objects;
using LD34.Handlers;

namespace LD34.States
{
    public class MenuState : GameState
    {
        public MenuState(Game game) : base(game)
        {
            Picture picture = (Picture)AddGameObject(nameof(Picture));
            picture.SetCentered(false);
            picture.SetTexture(Assets.Textures.ID.Menu);
            picture.Position = new Vector2f(0, 0);

            Button playButton = (Button) AddGameObject(nameof(Button));
            playButton.SetActionCommand("play");
            playButton.Position = new Vector2f(Game.Window.Size.X/2, 150);
            playButton.SetSize(18);
            playButton.SetText("Play Game");

            Button helpButton = (Button)AddGameObject(nameof(Button));
            helpButton.SetActionCommand("help");
            helpButton.SetActionDelay(.5f);
            helpButton.Position = new Vector2f(Game.Window.Size.X / 2, 250);
            helpButton.SetSize(18);
            helpButton.SetText("Help");

            Button scoreButton = (Button)AddGameObject(nameof(Button));
            scoreButton.SetActionCommand("score");
            scoreButton.SetActionDelay(.5f);
            scoreButton.SetActionColor(Color.Magenta);
            scoreButton.SetOutlineColor(new Color(255, 165, 0));
            scoreButton.Position = new Vector2f(Game.Window.Size.X / 2, 350);
            scoreButton.SetSize(18);
            scoreButton.SetText("Scoreboard");

            Button quitButton = (Button)AddGameObject(nameof(Button));
            quitButton.SetActionCommand("quit");
            quitButton.SetActionDelay(.5f);
            quitButton.Position = new Vector2f(Game.Window.Size.X / 2, 450);
            quitButton.SetSize(18);
            quitButton.SetText("Quit Game");

            Label title = (Label)AddGameObject(nameof(Label));
            title.Position = new Vector2f(Game.Window.Size.X / 2, 10);
            title.SetSize(48);
            title.SetFont(Assets.Fonts.ID.Header);
            title.SetText("Avidus Games");

            Game.PlayMusic(Assets.Musics.ID.Menu);
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
                    tmpGameObject = new Button(ButtonHandler, "",new Vector2f(0, 0), this);
                    GameObjects.Add(tmpGameObject);
                    break;
                case nameof(Label):
                    tmpGameObject = new Label("", new Vector2f(0, 0), this);
                    GameObjects.Add(tmpGameObject);
                    break;
                case nameof(Picture):
                    tmpGameObject = new Picture(0, new Vector2f(0, 0), this);
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
            switch(actionCommand)
            {
                case "quit":
                    if(perform)
                    {
                        Game.StopMusic(false);
                        Game.ChangeState(null);
                    }
                    break;
                case "play":
                    if (perform)
                    {
                        Game.ChangeState(new MainState(Game));
                    }
                    else
                    {
                        Game.StopMusic(true);
                    }
                    break;
                case "score":
					//LeaderboardHandler.HighscoreRequestAsync(HandleHighscore);
					if (perform)
                    {
                        Game.ChangeState(new ScoreState(Game));
                    }
                    break;
                case "help":
                    if (perform)
                    {
                        Game.ChangeState(new HelpState(Game));
                    }
                    break;
            }
        }
    }
}
