using GameCore.Objects;
using System;
using SFML.Graphics;
using SFML.System;
using GameCore.States;
using GameCore.Core;

namespace LD34.Menu
{
    class InputDialog : GameObject
    {
        private RectangleShape graphics;
        private Button ok;
        private Button cancel;
        private Color outlineColor;
        private Color actionColor;
        private FloatRect bounds;

        public InputDialog(Vector2f pos, GameState gameState) : base(gameState, pos)
        {
            graphics = new RectangleShape(new Vector2f(128, 64));
            graphics.FillColor = Color.Black;
            graphics.OutlineColor = Color.White;
            graphics.Position = pos;

            ok = new Button(ButtonHandler, "Ok", new Vector2f(pos.X + 64, pos.Y + 48), gameState);
            ok.SetActionCommand("ok");
            ok.SetAnimationFactor(1.0f);
            ok.SetActionDelay(.5f);
            ok.SetSize(18);

            cancel = new Button(ButtonHandler, "Cancel", new Vector2f(pos.X + 5, pos.Y + 48), gameState);
            cancel.SetActionCommand("cancel");
            cancel.SetAnimationFactor(1.0f);
            cancel.SetActionDelay(.5f);
            cancel.SetSize(18);
        }

        public void ButtonHandler(string actionCommand, bool perform)
        {
            switch (actionCommand)
            {
                case "ok":
                    if (perform)
                    {
                    }
                    break;
            }
        }

        public override void Dispose()
        {

        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            graphics.Draw(target, states);
            ok.Draw(target, states);
            cancel.Draw(target, states);
        }

        public override void FixedUpdate()
        {
            throw new NotImplementedException();
        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }

        public void UpdatePos()
        {
            graphics.Position = Position;
            ok.Position = new Vector2f(Position.X + 64, Position.Y + 48);
            ok.UpdatePos();
            cancel.Position = new Vector2f(Position.X + 5, Position.Y + 48);
            cancel.UpdatePos();
        }

        public override void Update()
        {
            ok.Update();
            cancel.Update();
        }
        /**
        private void UpdatePosition()
        {
            graphics.Position = new Vector2f(Position.X - graphics.Size.X / 2, Position.Y - graphics.Size.Y / 2);
            label.Update();
        }
    */
    }
}
