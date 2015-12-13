using System;
using GameCore.Objects;
using SFML.Graphics;
using GameCore.States;
using SFML.System;
using GameCore.Tween;
using GameCore.Core;

namespace LD34.Objects
{
	class Leaf : GameObject
	{
        private Animation graphics;
        private Tweener leafTween = new Tweener();
		private bool moving;
		private Vector2f targetPos;
        private bool leftleaf;

		public bool LeftLeaf { get
            {
                return leftleaf;
            }
            set
            {
                leftleaf = value;
                if (!LeftLeaf)
                {
                    graphics.SetScale(new Vector2f(-5f, 5f));
                }else
                {
                    graphics.SetScale(new Vector2f(5f, 5f));
                }
            }
        }

		public Leaf(GameState gameState, Vector2f pos ) :base(gameState, pos)
		{
			moving = false;
		}

		public Leaf(GameState gameState) : base(gameState, new Vector2f(0, 0))
		{
            graphics = new Animation(gameState.Game.GetAnimation(Assets.Animations.ID.Leaf));

			LeftLeaf = false;
			moving = false;
			//graphics.SetFlipped();
		}

		public override void Dispose()
		{
			// graphics.Dispose();
		}

		public override void Draw(RenderTarget target, RenderStates states)
		{
            Sprite currentFrame = graphics.GetImage();
            target.Draw(currentFrame, states);
        }

		public override void FixedUpdate()
		{
			throw new NotImplementedException();
		}

		public override void Update()
		{
			graphics.Update();
			Sprite currentFrame = graphics.GetImage();
			if (moving)
			{
				//moving = leafTween.Move(this, targetPos);
				//Console.WriteLine(Position);
			}

			currentFrame.Position = Position;
		}

		public override void Reset()
		{
			Position = new Vector2f(0, 0);
		}

		internal void MoveOneStepDown()
		{
			targetPos = Position += new Vector2f(0, 120);
            moving = true;
		}

		internal void MoveOneStepUp()
		{
			targetPos = Position -= new Vector2f(0, 120);
			moving = true;
		}
	}
}