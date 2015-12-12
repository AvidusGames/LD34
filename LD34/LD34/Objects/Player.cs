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

		public Player(Leaf root, GameState gameState):base(gameState, root.Position)
		{
            graphics = new RectangleShape(new Vector2f(32, 64));
            graphics.FillColor = Color.White;
		}

		public override void Draw(RenderTarget target, RenderStates states)
		{
			target.Draw(graphics);
		}

		public override void Update()
		{
			 //graphics.Position = Position + graphics.Size;
		}

		public override void FixedUpdate()
		{
			throw new NotImplementedException();
		}

		public override void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
