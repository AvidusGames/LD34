using GameCore.States;
using System;
using GameCore.Objects;
using GameCore.Core;
using SFML.Graphics;
using LD34.Menu;
using SFML.System;

namespace LD34.States
{
    public class MenuState : GameState
    {

        public MenuState(Game game) : base(game)
        {
            AddGameObject(nameof(Button));
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
                    tmpGameObject = new Button("Play Game",new Vector2f(100, 100), this);
                    GameObjects.Add(tmpGameObject);
                    break;
                case nameof(Label):
                    //tmpGameObject = new Player(leafHandler.CurentLeaf, this);
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
    }
}
