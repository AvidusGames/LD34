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

		public enum Direction
		{
			Left,
			Right
		}

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
			jumpingAnim = gameState.Game.GetAnimation(Assets.Animations.ID.Jump);

			currentAnim = jumpingAnim;
            currentAnim.SetScale(new Vector2f(0.25f, 0.25f));
			currentAnim.SetFrame(3);
			currentAnim.SetDelay(50);
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
			if (Jumping)
			{
				currentAnim.Update();
			}
			else
			{
				currentAnim.SetFrame(3);
			}

			//ChangeToJumpingSprite();

			Sprite currentFrame = currentAnim.GetImage();
			currentFrame.Position = Position;
			//currentAnim.Update();

			if (Jumping)
			{
				Jumping = playerTweener.Move(this, targetVec);
			}
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
			playerTweener.Move(this, targetVec);
			//ChangeToJumpingSprite();
		}

		public void ChangeToJumpingSprite()
		{
			if (currentAnim != jumpingAnim)
			{
				currentAnim = jumpingAnim;
				currentAnim.SetScale(new Vector2f(0.25f, 0.25f));
			}

		}

		public void ChangeToIdleSprite()
		{
			if (currentAnim != idleAnim)
			{
				currentAnim = idleAnim;
				currentAnim.SetScale(new Vector2f(0.25f, 0.25f));
			}
		}

		public void SetDirection(Direction value)
		{
			if (value == Direction.Left)
			{
				currentAnim.SetScale(new Vector2f(-0.25f, 0.25f));

			}
			else
			{
				currentAnim.SetScale(new Vector2f(0.25f, 0.25f));

			}

		}

		internal bool Fall(Direction value)
		{
			if (value == Direction.Right)
			{
				targetVec = new Vector2f(400, 700);
				moving = true;
				return playerTweener.Move(this, targetVec);
			}
			else
			{
				Jumping = true;
				targetVec = new Vector2f(200, 700);
				return playerTweener.Move(this, targetVec);
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
