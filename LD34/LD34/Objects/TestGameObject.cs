using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using GameCore.Core;
using GameCore.Objects;
using SFML.Window;

namespace LD34.Objects
{
	public class TestGameObject : GameObject
	{
		private Vector2f position = new Vector2f(0, 0);
		private RectangleShape graphics = new RectangleShape(new Vector2f(32, 32));

		public TestGameObject()
		{
			Bounds = new FloatRect(Position.X, Position.Y, 32, 32);
			graphics.FillColor = Color.Red;
		}

		public override void Draw(RenderTarget target, RenderStates states)
		{
			target.Draw(graphics);
		}

		public override void OnCollision(GameObject target)
		{
			target.Destroyed = true;
		}

		public override void Update()
		{
			if (Input.GetKey((int)Keyboard.Key.Right))
			{
				Move(2, 0);
			}
			graphics.Position = Position;
		}

		public override void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
