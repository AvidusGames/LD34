using System;
using GameCore.Objects;
using SFML.Graphics;
using GameCore.States;
using SFML.System;

namespace LD34.Objects
{
	class Leaf : GameObject
	{
		private RectangleShape graphics = new RectangleShape(new Vector2f(160, 16));


		public bool LeftLeaf { get; set; }
		public Leaf Parent { get; set; }
		public Leaf Child { get; set; }

		public Leaf(GameState gameState, Vector2f pos ) :base(gameState, pos)
		{
			graphics.FillColor = Color.Green;
			LeftLeaf = true;
		}

		public Leaf(GameState gameState) : base(gameState, new Vector2f(0, 0))
		{
			graphics.FillColor = Color.Green;
		}

		public override void Dispose()
		{
			throw new NotImplementedException();
		}

		public override void Draw(RenderTarget target, RenderStates states)
		{
			target.Draw(graphics);
		}

		public override void FixedUpdate()
		{
			throw new NotImplementedException();
		}

		public override void Update()
		{
			graphics.Position = Position;
		}
	}
}