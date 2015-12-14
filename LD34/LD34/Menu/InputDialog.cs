using GameCore.Objects;
using System;
using SFML.Graphics;
using SFML.System;
using GameCore.States;
using GameCore.Core;
using SFML.Window;
using System.Collections.Generic;
using System.Text;
using LD34.Handlers;
using LD34.Objects;
using LD34.States;

namespace LD34.Menu
{
    class InputDialog : GameObject
    {
        private RectangleShape graphics;
        private Button ok;
        private Button cancel;
        private Label label;
        private bool acceptInput;
        private string text;

        public InputDialog(Vector2f pos, GameState gameState) : base(gameState, pos)
        {
            graphics = new RectangleShape(new Vector2f(256, 64));
            graphics.FillColor = Color.Black;
            graphics.OutlineColor = Color.White;
            graphics.OutlineThickness = 5.0f;
            graphics.Position = pos;

            ok = new Button(ButtonHandler, "Ok", new Vector2f(pos.X + 200, pos.Y + 48), gameState);
            ok.SetActionCommand("ok");
            ok.SetAnimationFactor(1.0f);
            ok.SetActionDelay(.5f);
            ok.SetSize(18);

            label = new Label("paytowin", new Vector2f(pos.X + 128, pos.Y + 12), gameState);
            label.SetBackgroundColor(new Color(161, 161, 161));
            label.SetSize(18);

            cancel = new Button(ButtonHandler, "Cancel", new Vector2f(pos.X + 48, pos.Y + 48), gameState);
            cancel.SetActionCommand("cancel");
            cancel.SetAnimationFactor(1.0f);
            cancel.SetActionDelay(.5f);
            cancel.SetSize(18);

            Input.ClearText();
            acceptInput = true;
        }

        public void ButtonHandler(string actionCommand, bool perform)
        {
            switch (actionCommand)
            {
                case "ok":
                    acceptInput = false;                   
                    if (perform)
                    {
                        LeaderboardHandler.PutScoreAsync(text, ((MainState) GameState).GetPlayer().Score);
                        GameState.Game.ChangeState(new ScoreState(GameState.Game));
                    }
                    break;
                case "cancel":
                    if (perform)
                    {
                        GameState.Game.ChangeState(new ScoreState(GameState.Game));
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
            label.Draw(target, states);
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
            ok.Position = new Vector2f(Position.X + 227, Position.Y + 48);
            ok.UpdatePos();
            cancel.Position = new Vector2f(Position.X + 48, Position.Y + 48);
            cancel.UpdatePos();
            label.UpdatePos(new Vector2f(Position.X + 128, Position.Y + 12));
        }

        public override void Update()
        {
            ok.Update();
            cancel.Update();
            label.Update();

            if(acceptInput)
            {
                text = Input.GetText();
                label.SetText(text);
            }
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
