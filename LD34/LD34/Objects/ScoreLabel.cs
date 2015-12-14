using GameCore.Objects;
using System;
using SFML.Graphics;
using SFML.System;
using GameCore.States;
using GameCore.Core;
using LD34.Menu;

namespace LD34.Objects
{
    class ScoreLabel : GameObject
    {
        private RectangleShape graphics;
        private Label label;
        private Color fillColor;
        private FloatRect bounds;
        private float padding;

        public int Score { get; set; }

        public ScoreLabel(string text, Vector2f pos, GameState gameState) : base(gameState, pos)
        {
            graphics = new RectangleShape(new Vector2f(32, 32));
            graphics.OutlineColor = Color.Black;
            graphics.OutlineThickness = 3.0f;
            SetFillColor(Color.Magenta);
            padding = 5.0f;
            graphics.Position = pos;

            label = new Label("", pos, gameState);
            label.SetSize(24);
            label.SetCentered(false);
            SetText(text);
        }

        public void SetPadding(float _padding)
        {
            padding = _padding;
            SetText(null);
        }

        public void SetFont(Assets.Fonts.ID id)
        {
            label.SetFont(id);
            SetText(null);
        }

        public void SetSize(uint size)
        {
            label.SetSize(size);
            SetText(null);
        }

        public void SetText(string text)
        {
            if (text != null)
                label.SetText(text);

            float width = label.GetBounds().Width + padding * 2;
            float height = label.GetBounds().Height + padding * 2;

            graphics.Size = new Vector2f(width, height);
            label.Position = new Vector2f(Position.X + padding - width / 2, Position.Y - height / 2);
            graphics.Position = new Vector2f(Position.X - graphics.Size.X / 2, Position.Y - graphics.Size.Y / 2);
            label.Update();
            bounds = new FloatRect(graphics.Position.X, graphics.Position.Y, width, height);
        }

        public void SetTextColor(Color color)
        {
            label.SetColor(color);
        }

        public void SetFillColor(Color color)
        {
            fillColor = color;
            graphics.FillColor = color;
        }

        public override void Dispose()
        {
            graphics.Dispose();
            label.Dispose();
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            graphics.Draw(target, states);
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

        public override void Update()
        {
        }

        public void UpdatePosition()
        {
            SetText(null);
        }
    }
}
