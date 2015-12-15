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
using LD34.Menu;
using System.Collections.Generic;
using LD34.States;

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
		private Tweener playerTweener, towerTweener, bhousesTweener, fhousesTweener;
        private Tweener[] ScoreLabelsTweener;

        private InputDialog dialog;

        private Vector2f towerTargetVec, bhousesTargetVec, fhousesTargetVec;
        private Vector2f[] ScoreLabelsTargetVec;
        private Picture towers, bhouses, fhouses;

        protected List<ScoreLabel> ScoreLabels { get; private set; }

		private const float StartTime = 30;
		private Vector2f playerTargetVec;
		private bool gameStoped = false;

		public MainState(Game game) : base(game)
		{
            LeaderboardHandler.HighscoreRequestAsync(HandleHighscore);
            ScoreLabels = new List<ScoreLabel>();

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

            towers = new Picture(Assets.Textures.ID.Towers, new Vector2f(), this);
            bhouses = new Picture(Assets.Textures.ID.BHouses, new Vector2f(), this);
            fhouses = new Picture(Assets.Textures.ID.FHouses, new Vector2f(), this);

            towerTargetVec = new Vector2f(0f, -11400f);
            towers.Position = towerTargetVec;
            towers.SetCentered(false);

            bhousesTargetVec = new Vector2f(0f, 0f);
            bhouses.Position = bhousesTargetVec;
            bhouses.SetCentered(false);

            fhousesTargetVec = new Vector2f(0f, 0f);
            fhouses.Position = fhousesTargetVec;
            fhouses.SetCentered(false);

            playerTweener = new Tweener();
            towerTweener = new Tweener();
            bhousesTweener = new Tweener();
            fhousesTweener = new Tweener();



			if (leafHandler.PlayerStandLeaf.LeftLeaf)
			{

				playerTargetVec = new Vector2f(leafHandler.PlayerStandLeaf.Position.X, leafHandler.PlayerStandLeaf.Position.Y - 150);

				player.SetDirection(Player.Direction.Left);
			}
			else
			{

				playerTargetVec = new Vector2f(leafHandler.PlayerStandLeaf.Position.X, leafHandler.PlayerStandLeaf.Position.Y - 150);

				player.SetDirection(Player.Direction.Right);
			}

            Game.PlayMusic(Assets.Musics.ID.Game);
        }

        public Player GetPlayer()
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
					//GameObjects.Add(tmpGameObject);
					break;
                case nameof(ScoreLabel):
                    tmpGameObject = new ScoreLabel("", new Vector2f(0, 0), this);
                    ScoreLabels.Add((ScoreLabel)tmpGameObject);
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
			foreach (ScoreLabel label in ScoreLabels)
			{
				label.Dispose();
		}
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
			if (!gameStoped)
			{
				base.Update();
				//player.MoveToLeaf(leafHandler.PlayerStandLeaf);

				timer -= Game.TimeBetweenFrames.AsSeconds();

				timerText.DisplayedString = $"Timer: " + Math.Round(timer);
				scoreText.DisplayedString = $"Score: {player.Score}";

				towers.Update();

				towerTweener.Move(towers, towerTargetVec);
				bhousesTweener.Move(bhouses, bhousesTargetVec);
				fhousesTweener.Move(fhouses, fhousesTargetVec);

				if (ScoreLabelsTweener != null)
				{
					for (int i = 0; i < ScoreLabelsTweener.Length; i++)
					{
						if (ScoreLabelsTweener[i] == null) break;
						ScoreLabelsTweener[i].Move(ScoreLabels[i], ScoreLabelsTargetVec[i]);
						ScoreLabels[i].UpdatePosition();
					}
				}

				ClimbTree();
				MovePlayer();
				player.Update();

				if (timer <= 0)
				{
					Console.WriteLine("Called function!");
					DisplayInputDialog();
					gameStoped = true;
				}
			}


			if (dialog != null) dialog.Update();


		}

		private void MovePlayer()
		{
			if (leafHandler.PlayerStandLeaf.LeftLeaf)
			{
				playerTargetVec = new Vector2f(leafHandler.PlayerStandLeaf.Position.X, leafHandler.PlayerStandLeaf.Position.Y - 145);
				player.MoveTo(playerTargetVec);
				player.SetDirection(Player.Direction.Left);
			}
			else
			{
				playerTargetVec = new Vector2f(leafHandler.PlayerStandLeaf.Position.X, leafHandler.PlayerStandLeaf.Position.Y - 150);
				player.MoveTo(playerTargetVec);
				player.SetDirection(Player.Direction.Right);
			}




			//if (player.Jumping)
			//{
			//	player.Jump(leafHandler.PlayerStandLeaf);
			//}

			towers.Update();
            bhouses.Update();
            fhouses.Update();
			player.Update();
		}

		public override void FixedUpdate()
		{
			base.FixedUpdate();

		}

		public override void Draw(RenderTarget target, RenderStates states)
		{
			towers.Draw(target, states);
			bhouses.Draw(target, states);
			fhouses.Draw(target, states);
			base.Draw(target, states);
			target.Draw(player);
			target.Draw(timerText);
			target.Draw(scoreText);

			foreach (ScoreLabel label in ScoreLabels)
			{
				label.Draw(target, states);
			}
			if (dialog != null) target.Draw(dialog);
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

        private void HandleHighscore(Highscore[] scores)
        {
            Tweener[] _ScoreLabelsTweener = new Tweener[scores.Length];
            Vector2f[] _ScoreLabelsTargetVec = new Vector2f[scores.Length];
            for (int i = 0; i < scores.Length; i++)
            {
                _ScoreLabelsTweener[i] = new Tweener();
                ScoreLabel title = (ScoreLabel)AddGameObject(nameof(ScoreLabel));
                _ScoreLabelsTargetVec[i] = new Vector2f(Game.Window.Size.X / 2, -((scores[i].Score-3) * 100));
                title.Position = new Vector2f(Game.Window.Size.X / 2, -((scores[i].Score-3) * 100));
                title.SetSize(18);
                title.SetFont(Assets.Fonts.ID.Default);
                title.SetText(scores[i].Username);
            }
            ScoreLabelsTargetVec = _ScoreLabelsTargetVec;
            ScoreLabelsTweener = _ScoreLabelsTweener;
        }

        private void UpdatePositions(bool down)
		{
            if(down)
			{
                towerTargetVec = new Vector2f(0, towerTargetVec.Y + 50);
                bhousesTargetVec = new Vector2f(0, bhousesTargetVec.Y + 75);
                fhousesTargetVec = new Vector2f(0, fhousesTargetVec.Y + 100);
                for(int i = 0; i < ScoreLabels.Count; i++)
                {
                    Vector2f labelPos = ScoreLabels[i].Position;
                    ScoreLabelsTargetVec[i] = new Vector2f(labelPos.X, labelPos.Y + 100);
                }
            }else
            {
                towerTargetVec = new Vector2f(0, towerTargetVec.Y - 50);
                bhousesTargetVec = new Vector2f(0, bhousesTargetVec.Y - 75);
                fhousesTargetVec = new Vector2f(0, fhousesTargetVec.Y - 100);
                for(int i = 0; i < ScoreLabels.Count; i++)
                {
                    Vector2f labelPos = ScoreLabels[i].Position;
                    ScoreLabelsTargetVec[i] = new Vector2f(labelPos.X, labelPos.Y - 100);
                }
            }
        }

        public void DisplayInputDialog()
        {
			dialog = new InputDialog(new Vector2f(Game.Window.Size.X / 2 - 64, 100), this);
            dialog.Position = new Vector2f(Game.Window.Size.X / 2 - 64, 100);
            dialog.UpdatePos();
        }

        private void ClimbTree()
		{
			if (Input.GetKeyPressed(Keyboard.Key.Left))
			{
                Game.PlaySound(Assets.Sounds.ID.Jump);
                // TODO:: call this function in the function which handles if player elevates down or up a leaf.
                if (leafHandler.NextLeaf.LeftLeaf)
				{



					player.Jumping = true;
					UpdatePositions(true);
					StartClimb();
				}

				else if (leafHandler.PlayerStandLeaf.LeftLeaf != true)
				{
					DisplayInputDialog();
                    Console.WriteLine("Called function!");
					gameStoped = true;
					//int fallsteps = leafHandler.Fall();
					//               player.Score -= fallsteps;
					//for (int i = 0; i < fallsteps; i++)
					//{
					//	UpdatePositions(false);
					//}
					//               player.Jumping = false;
                }             
			}


			else if (Input.GetKeyPressed(Keyboard.Key.Right))
			{
                Game.PlaySound(Assets.Sounds.ID.Jump);
                // TODO:: call this function in the function which handles if player elevates down or up a leaf.


                if (!leafHandler.NextLeaf.LeftLeaf)
                {
					//if (leafHandler.PlayerStandLeaf.LeftLeaf)
					//{
					//	playerTargetVec = new Vector2f(leafHandler.PlayerStandLeaf.Position.X, leafHandler.PlayerStandLeaf.Position.Y - 145);

					//	player.SetDirection(Player.Direction.Left);
					//}
					//else
					//{
					//	playerTargetVec = new Vector2f(leafHandler.PlayerStandLeaf.Position.X, leafHandler.PlayerStandLeaf.Position.Y - 150);

					//	player.SetDirection(Player.Direction.Right);
					//}

					UpdatePositions(true);
					player.Jumping = true;
					StartClimb();
				}

                else if (leafHandler.PlayerStandLeaf.LeftLeaf != false)
                {
                    Console.WriteLine("Called function!");
					gameStoped = true;
                    DisplayInputDialog();
                }
            }
		}

		private void StartClimb()
		{
			leafHandler.Climb();
			player.Score++;
		}
	}
}