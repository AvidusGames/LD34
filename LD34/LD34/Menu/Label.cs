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

        public Label(string text, Vector2f pos, GameState gameState) : base(gameState, pos)
		{
            graphics = new Text(text, gameState.Game.GetFont(GameCore.Core.Fonts.ID.Default), 12);
            graphics.Position = pos;
            graphics.Color = Color.White;
        }

        public void SetText(string text)
        {
            graphics.DisplayedString = text;
        }

        public void SetFont(GameCore.Core.Fonts.ID id)
        {
            graphics.Font = GameState.Game.GetFont(id);
        }

        public void SetColor(Color color)
        {
            graphics.Color = color;
        }

        public void SetSize(uint size)
        {
            graphics.CharacterSize = size;
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

        public override void FixedUpdate()
        {
            throw new NotImplementedException();
        }

        public override void Update() {
            graphics.Position = Position;
        }

		public override void Reset()
		{
			throw new NotImplementedException();
		}
	}
}
