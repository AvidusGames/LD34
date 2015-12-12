using GameCore.Objects;
using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using GameCore.States;

namespace LD34.Menu
{
    class Button : GameObject
    {
        private RectangleShape graphics;
        private Label label;

        public Button(string text, Vector2f pos, GameState gameState):base(gameState, pos)
		{
            graphics = new RectangleShape(new Vector2f(64, 32));
            graphics.FillColor = Color.Transparent;
            graphics.OutlineColor = Color.White;
            graphics.OutlineThickness = 5.0f;
            graphics.Position = pos;

            label = new Label(text, pos, gameState);
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
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

        public override void Update()
        {
            
        }
    }
}
