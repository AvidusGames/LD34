using GameCore.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameCore.Objects;
using GameCore.Handlers;
using GameCore.Core;
using LD34.Handlers;

namespace LD34.Objects
{
	class MainState : GameState
	{
		private GameObjectPool<Leaf> LeafPool;
		private LeafHandler leafHandler;

		public MainState(Game game): base(game)
		{

			InitPools();
			AddGameObject(nameof(Player)).Position = new SFML.System.Vector2f(230, 500);
			leafHandler = new LeafHandler(this);
			AddEntity(nameof(Branch));
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
				case nameof(Leaf):
					tmpGameObject = LeafPool.Release();
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
			throw new NotImplementedException();
		}

		public override Entity AddEntity(string type)
		{
			Entity tmpEnitiy = null;
			switch (type)
			{
				case nameof(Branch):
					tmpEnitiy = new Branch();
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
		}
	}
}
