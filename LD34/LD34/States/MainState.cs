using GameCore.States;
using System;
using GameCore.Objects;
using GameCore.Handlers;
using GameCore.Core;
using LD34.Handlers;
using SFML.Window;
using SFML.System;
using SFML.Graphics;
using GameCore.Tween;

namespace LD34.Objects
{
	class MainState : GameState
	{
		private GameObjectPool<Leaf> LeafPool;
		private LeafHandler leafHandler;
		private Player player;
		private float timer;
		private Text timerText;
		private Text scoreText;
		private Tweener playerTweener;

		private const float StartTime = 30;

		public MainState(Game game) : base(game)
		{

			InitPools();
			leafHandler = new LeafHandler(this);
			player = (Player)AddGameObject(nameof(Player));
			player.MoveToLeaf(leafHandler.PlayerStandLeaf);
			AddEntity(nameof(Branch));

			timer = StartTime;
            timerText = new Text($"Timer: " + Math.Round(timer), new Font(Game.GetFont(Assets.Fonts.ID.Default)));
			timerText.Position = new Vector2f(10, 0);
			scoreText = new Text($"Score: {player.Score}",new Font(Game.GetFont(Assets.Fonts.ID.Default)));
			scoreText.Position = new Vector2f(650, 0);

			playerTweener = new Tweener();

            Game.PlayMusic(Assets.Musics.ID.Game);
        }

        private Player GetPlayer()
        {
            return player;
        }

		private void InitPools()
		{
			LeafPool = new GameObjectPool<Leaf>(() => new Leaf(this), 10);
		}

		public override GameObject AddGameObject(string type)
		{
			GameObject tmpGameObject = null;
			switch (type)
			{
				//TODO:Fix pool
				case nameof(Leaf):
					tmpGameObject = new Leaf(this); //LeafPool.Release();
                    GameObjects.Add(tmpGameObject);
					break;
				case nameof(Player):
					tmpGameObject = new Player(this);
					GameObjects.Add(tmpGameObject);
					break;
					
				default:
					throw new Exception("GameObject not found in this State");
			}
			return tmpGameObject;
		}

		public override void Dispose()
		{
			base.Dispose();
		}

		public override Entity AddEntity(string type)
		{
			Entity tmpEnitiy = null;
			switch (type)
			{
				case nameof(Branch):
					tmpEnitiy = new Branch(Game);
					EntityObjects.Add(tmpEnitiy);
					break;
				default:
					throw new Exception("Entity not found in this State");
			}
			return tmpEnitiy;
		}

		public override void Update()
		{
			base.Update();
			//player.MoveToLeaf(leafHandler.PlayerStandLeaf);

			timer -= Game.TimeBetweenFrames.AsSeconds();

			if (timer<= 0)
			{
				Game.ChangeState(null);
			}

			timerText.DisplayedString = $"Timer: " + Math.Round(timer);
            scoreText.DisplayedString = $"Score: {player.Score}";
            ClimbTree();

            Vector2f playerTargetVec;
            if (leafHandler.PlayerStandLeaf.LeftLeaf)
            {
                playerTargetVec = new Vector2f(leafHandler.PlayerStandLeaf.Position.X - 80, leafHandler.PlayerStandLeaf.Position.Y-100);
            }
            else
            {
                playerTargetVec = new Vector2f(leafHandler.PlayerStandLeaf.Position.X, leafHandler.PlayerStandLeaf.Position.Y-100);
            }

			playerTweener.Move(player, playerTargetVec);
			//Console.WriteLine(player.Position);
		}

		public override void FixedUpdate()
		{
			base.FixedUpdate();

		}

		public override void Draw(RenderTarget target, RenderStates states)
		{
			base.Draw(target, states);
			target.Draw(timerText);
			target.Draw(scoreText);
		}

		protected override void RemoveGameObjects()
		{
			for (int i = 0; i < GameObjects.Count; i++)
			{
				if (GameObjects[i].Destroyed)
				{
					GameObject tmpGameObject = GameObjects[i];
					if (tmpGameObject is Leaf)
					{
						LeafPool.Acquire((Leaf)tmpGameObject);
					}
					GameObjects.RemoveAt(i);
					i--;
				}
			}
		}

		private void ClimbTree()
		{
			if (Input.GetKeyPressed(Keyboard.Key.Left))
			{
				
				if (leafHandler.NextLeaf.LeftLeaf)
				{
					StartClimb();
				}

				else
				{
					Game.ChangeState(null);
				}
			}


			else if (Input.GetKeyPressed(Keyboard.Key.Right))
			{
                if (!leafHandler.NextLeaf.LeftLeaf)
                {
					StartClimb();
				}
				else
                {
                    Game.ChangeState(null);
                }
            }
		}

		private void StartClimb()
		{
			//var tmp = player.Position;

			//playerTweener.StartMoveTowards(player, playerTargetVec);
			//player.MoveToLeaf(leafHandler.PlayerStandLeaf);
			leafHandler.Climb();
			player.Score++;
		}
	}
}