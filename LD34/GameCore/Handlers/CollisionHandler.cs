using GameCore.Interfaces;
using GameCore.Objects;
using System.Collections.Generic;

namespace GameCore.Handlers
{
	class CollisionHandler : IUpdatable
	{
		private List<GameObject> gameObjects = new List<GameObject>();
		private List<Entity> entityObjects;

		public CollisionHandler(List<Entity> entityObjects)
		{
			this.entityObjects = entityObjects;
			UpdateGameObjectsList();
		}

		private void UpdateGameObjectsList()
		{
			//Kanske måste optimera senare
			gameObjects.Clear();
			foreach (GameObject gameObject in entityObjects)
			{
				gameObjects.Add(gameObject);
			}
		}

		public void Update()
		{
			UpdateGameObjectsList();
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
