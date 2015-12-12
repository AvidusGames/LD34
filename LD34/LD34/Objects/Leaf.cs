using System;
using GameCore.Objects;
using SFML.Graphics;
using GameCore.States;
using SFML.System;

namespace LD34.Objects
{
	class Leaf : GameObject
	{
		private RectangleShape graphics = new RectangleShape(new Vector2f(64, 32));

		public Leaf(GameState gameState, Vector2f pos ) :base(gameState, pos)
		{
			graphics.FillColor = Color.Green;
		}

		public Leaf(GameState gameState) : base(gameState, new Vector2f(0, 0))
		{

		}

		public override void Dispose()
		{
			throw new NotImplementedException();
		}

		public override void Draw(RenderTarget target, RenderStates states)
		{
			throw new NotImplementedException();
		}

		public override void FixedUpdate()
		{
			throw new NotImplementedException();
		}

		public override void Update()
		{
			throw new NotImplementedException();
		}
	}
}