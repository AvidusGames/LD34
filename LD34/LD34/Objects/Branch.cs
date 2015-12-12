using System;
using GameCore.Objects;
using SFML.Graphics;
using GameCore.Core;

namespace LD34.Objects
{
	internal class Branch : Entity
	{
		private const float BranchSize = 160;

		private RectangleShape graphics = new RectangleShape(new SFML.System.Vector2f(BranchSize, 600));

		public Branch() :base(new SFML.System.Vector2f(Game.Window.Size.X / 2 - BranchSize / 2, 0))
		{
			graphics.FillColor = new Color(69, 31, 3);
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
		}
	}
}