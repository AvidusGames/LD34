using GameCore.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using GameCore.States;

namespace LD34.Objects
{
	class Player : GameObject
	{
		private RectangleShape graphics = new RectangleShape(new SFML.System.Vector2f(32, 64));

		public Player(GameState gameState):base(gameState, new SFML.System.Vector2f(0,0))
		{
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

		 
	}
}
