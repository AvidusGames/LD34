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
		private RectangleShape graphics = new RectangleShape(new Vector2f(32, 64));
		private bool leaftLeaf;

		private readonly Vector2f RightPos = new Vector2f(560, 490);
		private readonly Vector2f LeaftPos = new Vector2f(230, 500);

		public bool LeaftLeaf
		{
			get
			{
				return leaftLeaf;
			}

			set
			{
				leaftLeaf = value;
				if (leaftLeaf)
				{
					Position = LeaftPos;
				}
				else
				{
					Position = RightPos;
				}
			}
		}

		public Player(GameState gameState):base(gameState, new Vector2f(230, 500))
		{
			graphics.FillColor = Color.Red;
			Position = LeaftPos;
			graphics.Position = LeaftPos;
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

		internal void Switch()
		{
			if (Position == LeaftPos)
			{
				Position = RightPos;
			}
			else
			{
				Position = LeaftPos;
			}
		}
	}
}
