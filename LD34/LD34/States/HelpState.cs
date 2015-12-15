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
    public class HelpState : GameState
    {
        public HelpState(Game game) : base(game)
        {
            //Picture picture = (Picture)AddGameObject(nameof(Picture));
            //picture.SetCentered(false);
            //picture.SetTexture(Assets.Textures.ID.Menu);
            //picture.Position = new Vector2f(0, 0);

            Button backButton = (Button)AddGameObject(nameof(Button));
            backButton.SetActionCommand("back");
            backButton.SetActionDelay(.5f);
            backButton.Position = new Vector2f(Game.Window.Size.X / 2, 450);
            backButton.SetSize(18);
            backButton.SetText("Back");

            Label body = (Label)AddGameObject(nameof(Label));
            body.Position = new Vector2f(Game.Window.Size.X / 2, 100);
            body.SetSize(18);
            body.SetFont(Assets.Fonts.ID.Default);
            body.SetText("The  game  is  about  trying  to  climb  as  high  as  possible\nWhen  you  see  a  leaf  on  the  left  side  of  the  branch  use  the  left  key\nVice  versa  if  a  leaf  is  on  the  right  side");
			body.SetColor(Color.White);

            Picture pic = (Picture)AddGameObject(nameof(Picture));
            pic.Position = new Vector2f(Game.Window.Size.X / 2, 250);
            pic.SetTexture(Assets.Textures.ID.Help);
            pic.SetScale(new Vector2f(0.25f, 0.25f));

            Label body2 = (Label)AddGameObject(nameof(Label));
            body2.Position = new Vector2f(Game.Window.Size.X / 2, 400);
            body2.SetSize(18);
            body2.SetFont(Assets.Fonts.ID.Default);
            body2.SetText("Created  in  72  hours  by  Linus123xbb  and  Cellmon95");
			body2.SetColor(Color.White);

            Label title = (Label)AddGameObject(nameof(Label));
            title.Position = new Vector2f(Game.Window.Size.X / 2, 10);
            title.SetSize(48);
            title.SetFont(Assets.Fonts.ID.Header);
            title.SetText("Help Menu");

			Label madeWithLbl = (Label)AddGameObject(nameof(Label));
			madeWithLbl.SetText("Made With");
			madeWithLbl.SetSize(20);
			madeWithLbl.SetFont(Assets.Fonts.ID.Default);
			madeWithLbl.Position = new Vector2f(400, 500);
			madeWithLbl.SetColor(Color.White);

			Picture sfmlLogo = (Picture)AddGameObject("sfml");
			sfmlLogo.SetTexture(Assets.Textures.ID.SFML);
			sfmlLogo.SetScale(new Vector2f(0.25f, 0.25f));
			sfmlLogo.Position = new Vector2f(400, 550);

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
                case nameof(Picture):
                    tmpGameObject = new Picture(0, new Vector2f(0, 0), this);
                    GameObjects.Add(tmpGameObject);
                    break;
				case "sfml":
					tmpGameObject = new Picture(0, new Vector2f(400, 500), this);
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
