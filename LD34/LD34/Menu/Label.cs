using GameCore.Objects;
using System;
using SFML.Graphics;
using SFML.System;
using GameCore.States;

namespace LD34.Menu
{
    class Label : GameObject
    {
        private Text graphics;
        private RectangleShape background;
        private bool centered;
        private Color backgroundColor;
        private FloatRect bounds;

        public Label(string text, Vector2f pos, GameState gameState) : base(gameState, pos)
		{
            graphics = new Text(text, gameState.Game.GetFont(Assets.Fonts.ID.Default), 12);
            graphics.Position = pos;
            graphics.Color = Color.White;
            background = new RectangleShape();
            background.FillColor = Color.Transparent;
            bounds = GetBounds();
            centered = true;
        }

        public void SetCentered(bool _centered)
        {
            centered = _centered;
        }

        public void SetText(string text)
        {
            if(text != null)
                graphics.DisplayedString = text;

            bounds = GetBounds();
            Update();

            if (background.FillColor != Color.Transparent)
            {
                float width = bounds.Width + 5 * 2;
                float height = bounds.Height + 5 * 2;

                background.Size = new Vector2f(width, height);
                graphics.Position = new Vector2f(Position.X + 5 - width / 2, Position.Y - height / 2);
            }
        }

        public void SetFont(Assets.Fonts.ID id)
        {
            graphics.Font = GameState.Game.GetFont(id);
            SetText(null);
        }

        public void SetBackgroundColor(Color color)
        {
            backgroundColor = color;
            background.FillColor = color;
        }

        public void SetColor(Color color)
        {
            graphics.Color = color;
        }

        public void SetSize(uint size)
        {
            graphics.CharacterSize = size;
            SetText(null);
        }

        public FloatRect GetBounds()
        {
            return graphics.GetLocalBounds();
        }

        public override void Dispose()
        {
			graphics.Dispose();
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            background.Draw(target, states);
            graphics.Draw(target, states);
        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }

        public override void FixedUpdate()
        {
            throw new NotImplementedException();
        }

        public void UpdatePos(Vector2f pos)
        {
            Position = pos;
        }

        public void UpdateGraphicsPos(Vector2f pos)
        {
            graphics.Position = pos;
        }

        public override void Update() {
            if(centered)
            {
                graphics.Position = new Vector2f(Position.X - bounds.Width / 2, Position.Y - bounds.Height / 2);
            }
            else
            {
                graphics.Position = Position;
            }
            if(background.FillColor != Color.Transparent)
            {
                background.Position = new Vector2f(Position.X - background.Size.X / 2, Position.Y - background.Size.Y / 2);
            }
        }
	}
}
