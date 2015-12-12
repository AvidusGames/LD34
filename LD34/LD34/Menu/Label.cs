using GameCore.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using GameCore.States;

namespace LD34.Menu
{
    class Label : GameObject
    {
        private Text graphics;
        private bool centered;
        private FloatRect bounds;

        public Label(string text, Vector2f pos, GameState gameState) : base(gameState, pos)
		{
            graphics = new Text(text, gameState.Game.GetFont(GameCore.Core.Fonts.ID.Default), 12);
            graphics.Position = pos;
            graphics.Color = Color.White;
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
        }

        public void SetFont(GameCore.Core.Fonts.ID id)
        {
            graphics.Font = GameState.Game.GetFont(id);
            SetText(null);
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
            throw new NotImplementedException();
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
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

        public override void Update() {
            if(centered)
            {
                graphics.Position = new Vector2f(Position.X - bounds.Width / 2, Position.Y - bounds.Height / 2);
            }
            else
            {
                graphics.Position = Position;
            }
        }
    }
}
