using GameCore.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using GameCore.States;
using SFML.System;
using GameCore.Core;

namespace LD34.Objects
{
	class Player : GameObject
	{
        private Animation currentAnim;
		public int Score { get; set; }

		public enum Side
		{
			Right,
			Left
		}

		public Player(GameState gameState):base(gameState, new Vector2f(200, 580))
		{
			Position = new Vector2f(200, 480);
            currentAnim = gameState.Game.GetAnimation(Assets.Animations.ID.Walk);
            currentAnim.SetScale(new Vector2f(0.25f, 0.25f));
        }

		public Player(GameState gameState, Vector2f pos) : base(gameState, pos)
		{
		}

		public override void Draw(RenderTarget target, RenderStates states)
		{
            Sprite currentFrame = currentAnim.GetImage();
            target.Draw(currentFrame);
		}

		public override void Update()
		{
            currentAnim.Update();
            Sprite currentFrame = currentAnim.GetImage();
			currentFrame.Position = Position;
		}

		public override void FixedUpdate()
		{
			throw new NotImplementedException();
		}

		public override void Dispose()
		{
            //throw new NotImplementedException();
        }

		public override void Reset()
		{
			throw new NotImplementedException();
		}

		public void MoveToLeaf(Leaf leaf)
		{



			Position = new Vector2f(leaf.Position.X + 50, leaf.Position.Y - 70);
			Console.WriteLine("Player pos: " + Position);
			//För att updatatera postionen på grafiken
			Update();
		}

		//internal void MoveTo(Side side)
		//{
		//	switch (side)
		//	{
		//		case Side.Right:
		//			Position = new Vector2f(600, 480);
		//			break;
		//		case Side.Left:
		//			Position = new Vector2f(200, 480);
		//			break;
		//		default:
		//			break;
		//	}
		//}
	}
}
