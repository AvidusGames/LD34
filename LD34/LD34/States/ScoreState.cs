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
    public class ScoreState : GameState
    {
        public ScoreState(Game game) : base(game)
        {
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
