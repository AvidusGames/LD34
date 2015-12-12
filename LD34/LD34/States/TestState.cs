using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using GameCore.Handlers;
using GameCore.Core;
using GameCore.Objects;
using GameCore.States;
using LD34.Objects;

namespace LD34.States
{
	public class TestState : GameState
	{
		private GameObjectPool<TestGameObject> testPool = new GameObjectPool<TestGameObject>(() => new TestGameObject(new SFML.System.Vector2f(0,0)), 100);

		public TestState(Game game) : base(game)
		{
			AddEntity(nameof(TestGameObject));
		}

		public override void Update()
		{
			base.Update();
		}

		public override void Dispose()
		{
			throw new NotImplementedException();
		}

		public override void Draw(RenderTarget target, RenderStates states)
		{
			base.Draw(target, states);
		}

		public override GameObject AddEntity(string type)
		{
			GameObject tmpGameObject = null;
			switch (type)
			{
				case nameof(TestGameObject):
					tmpGameObject = testPool.Release();
					break;

				default:
					break;
			}
			EntityObjects.Add(tmpGameObject);
			return tmpGameObject;
		}
	}
}
