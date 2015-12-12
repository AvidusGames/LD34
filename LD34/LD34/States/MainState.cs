using GameCore.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameCore.Objects;
using GameCore.Handlers;
using GameCore.Core;

namespace LD34.Objects
{
	class MainState : GameState
	{
		private GameObjectPool<Leaf> LeafPool;

		public MainState(Game game): base(game)
		{
			InitPools();
			AddEntity(nameof(Branch));
		}

		private void InitPools()
		{
			LeafPool = new GameObjectPool<Leaf>(() => new Leaf(this), 10);
		}

		public override Entity AddGameObject(string type)
		{
			throw new NotImplementedException();
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
	}
}
