using GameCore.Interfaces;
using GameCore.Objects;
using System.Collections.Generic;
using System;

namespace GameCore.Handlers
{
	class CollisionHandler : IFixedUpdatable
	{
		private List<GameObject> gameObjects = new List<GameObject>();

		public CollisionHandler(List<GameObject> entityObjects)
		{
			gameObjects = entityObjects;
			//this.GameObjects = entityObjects;
			//UpdateGameObjectsList();
		}

		//private void UpdateGameObjectsList()
		//{
		//	//Kanske måste optimera senare
		//	gameObjects.Clear();
		//	foreach (GameObject gameObject in gameObjects)
		//	{
		//		gameObjects.Add(gameObject);
		//	}
		//}

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

		public void FixedUpdate()
		{
			CheckCollision();
		}
	}
}
