using System;
using GameCore.Objects;
using SFML.Graphics;
using GameCore.States;
using SFML.System;
using GameCore.Tween;

namespace LD34.Objects
{
	class Leaf : GameObject
	{
		private RectangleShape graphics = new RectangleShape(new Vector2f(160, 16));
		private Tweener leafTween = new Tweener();
		private bool moving;
		private Vector2f targetPos;

		public bool LeftLeaf { get; set; }

		public Leaf(GameState gameState, Vector2f pos ) :base(gameState, pos)
		{
			graphics.FillColor = Color.Green;
			LeftLeaf = true;

			leafTween.speed = 1000;
		}

		public Leaf(GameState gameState) : base(gameState, new Vector2f(0, 0))
		{
			graphics.FillColor = Color.Green;
		}

		public override void Dispose()
		{
			graphics.Dispose();
			graphics = null;
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
			if (moving)
			{
				moving = leafTween.Move(this, targetPos);
			}
		}

		public override void Reset()
		{
			Position = new Vector2f(0, 0);
		}

		internal void MoveOneStep()
		{
			targetPos = Position += new Vector2f(0, 120);
            moving = true;
		}
	}
}