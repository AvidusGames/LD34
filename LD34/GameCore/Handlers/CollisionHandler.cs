using GameCore.Interfaces;
using GameCore.Objects;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Handlers
{
	class CollisionHandler : IUpdatable
	{
		private List<GameObject> gameObjects;

		public CollisionHandler(List<GameObject> gameObjects)
		{
			this.gameObjects = gameObjects;
		}

		public void Update()
		{
			CheckCollision();
		}

		private void CheckCollision()
		{
			for (int i = 0; i < gameObjects.Count; i++)
			{
				GameObject checker = gameObjects[i];

				for (int j = 0; j < gameObjects.Count; j++)
				{
					//Om det är samma object som vi kollar, hoppa.
					if (i == j)
						continue;
					GameObject target = gameObjects[j];
					if (checker.Bounds.Intersects(target.Bounds))
					{
						checker.OnCollision(target);
					}
				}
			}
		}
	}
}
