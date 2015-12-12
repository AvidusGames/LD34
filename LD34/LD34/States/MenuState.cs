using GameCore.States;
using System;
using GameCore.Objects;
using GameCore.Core;
using SFML.Graphics;
using LD34.Menu;
using SFML.System;
using LD34.Objects;

namespace LD34.States
{
    public class MenuState : GameState
    {
        public MenuState(Game game) : base(game)
        {
            Button playButton = (Button) AddGameObject(nameof(Button));
            playButton.SetActionCommand("play");
            playButton.Position = new Vector2f(Game.Window.Size.X/2, 100);
            playButton.SetSize(18);
            playButton.SetText("Play Game");

            Button quitButton = (Button)AddGameObject(nameof(Button));
            quitButton.SetActionCommand("quit");
            quitButton.Position = new Vector2f(Game.Window.Size.X / 2, 200);
            quitButton.SetSize(18);
            quitButton.SetText("Quit Game");

            Label title = (Label)AddGameObject(nameof(Label));
            title.Position = new Vector2f(Game.Window.Size.X / 2, 10);
            title.SetSize(48);
            title.SetFont(GameCore.Core.Fonts.ID.Header);
            title.SetText("Avidus Games");
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

                default:
                    throw new Exception("GameObject not found in this State");
            }
            return tmpGameObject;
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        public void ButtonHandler(string actionCommand)
        {
            switch(actionCommand)
            {
                case "quit":
                    Game.ChangeState(null);
                    break;
                case "play":
                    Game.ChangeState(new MainState(Game));
                    break;
            }
        }
    }
}
