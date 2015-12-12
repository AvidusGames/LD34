using GameCore.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using GameCore.States;
using SFML.System;

namespace LD34.Objects
{
	class Player : GameObject
	{
        private RectangleShape graphics;

		public enum Side
		{
			Right,
			Left
		}

		public Player(GameState gameState):base(gameState, new Vector2f(200, 580))
		{
            graphics = new RectangleShape(new Vector2f(32, 64));
            graphics.FillColor = Color.Red;
		}

		public override void Draw(RenderTarget target, RenderStates states)
		{
			target.Draw(graphics);
		}

		public override void Update()
		{
			graphics.Position = Position;
		}

		public override void FixedUpdate()
		{
			throw new NotImplementedException();
		}

		public override void Dispose()
		{
			throw new NotImplementedException();
		}

		public override void Reset()
		{
			throw new NotImplementedException();
		}

		internal void MoveTo(Side side)
		{
			switch (side)
			{
				case Side.Right:
					Position = new Vector2f(600, 480);
					break;
				case Side.Left:
					Position = new Vector2f(200, 480);
					break;
				default:
					break;
			}
		}
	}
}
