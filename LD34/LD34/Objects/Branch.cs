using System;
using GameCore.Objects;
using SFML.Graphics;
using GameCore.Core;

namespace LD34.Objects
{
	internal class Branch : Entity
	{
		private const float BranchSize = 160;

        private Sprite graphics;

		public Branch(Game game) :base(new SFML.System.Vector2f(0, 0))
		{
            graphics = new Sprite(game.GetTexture(Assets.Textures.ID.Tree));
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