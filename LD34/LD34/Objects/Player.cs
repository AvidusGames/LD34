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
using GameCore.Tween;

namespace LD34.Objects
{
	class Player : GameObject
	{
        private Animation currentAnim;
		private Animation jumpingAnim;
		private Animation idleAnim;
		public int Score { get; set; }
		public bool Jumping { get; internal set; }

		private Tweener playerTweener;
		private Vector2f targetVec;
		private bool moving;

		public enum Side
		{
			Right,
			Left
		}

		public Player(GameState gameState):base(gameState, new Vector2f(200, 580))
		{
			Position = new Vector2f(200, 480);
			idleAnim = gameState.Game.GetAnimation(Assets.Animations.ID.Walk);
			idleAnim.SetFrame(0);
			jumpingAnim = gameState.Game.GetAnimation(Assets.Animations.ID.Jump);
			currentAnim = idleAnim;

            currentAnim.SetScale(new Vector2f(0.25f, 0.25f));
			currentAnim.SetFrame(3);
			playerTweener = new Tweener();


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
			if (moving)
			{
				moving = playerTweener.Move(this, targetVec);

			}

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

		public void Jump(Leaf leaf)
		{
			targetVec = new Vector2f(leaf.Position.X, leaf.Position.Y - 50);
			Jumping = playerTweener.Move(this, targetVec);

			Console.WriteLine(Jumping);
		}

		public void MoveToLeaf(Leaf leaf)
		{

			if (leaf.LeftLeaf)
			{
				targetVec = new Vector2f(leaf.Position.X - 80, leaf.Position.Y - 100);
			}

			else
			{
				targetVec = new Vector2f(leaf.Position.X, leaf.Position.Y - 100);
			}

			moving = true;
			Console.WriteLine("Player pos: " + Position);
			//För att updatatera postionen på grafiken

			Update();
		}

		public void MoveTo(Vector2f targetVector)
		{
			targetVec = targetVector;
			if (playerTweener.Move(this, targetVector))
			{
				currentAnim = jumpingAnim;
				currentAnim.SetScale(new Vector2f(0.25f, 0.25f));
			}
			else
			{
				currentAnim = idleAnim;
				currentAnim.SetScale(new Vector2f(0.25f, 0.25f));
			}
			
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
