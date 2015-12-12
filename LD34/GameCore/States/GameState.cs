using GameCore.Interfaces;
using System.Collections.Generic;
using SFML.Graphics;
using GameCore.Core;
using GameCore.Handlers;
using GameCore.Objects;

namespace GameCore.States
{
	public abstract class GameState : IState
	{
		public Game Game { get; private set; }

		protected List<GameObject> GameObjects { get; private set; }

		private CollisionHandler collisionHandler;

		public GameState(Game game)
		{
			Game = game;
			GameObjects = new List<GameObject>();

			Init();
		}

		private void Init()
		{
			GameObjects = new List<GameObject>();
			collisionHandler = new CollisionHandler(GameObjects);
		}
		/// <summary>
		/// Lägger till ett GameObject i statet.
		/// </summary>
		/// <param name="type">vilket GameObject</param>
		/// <returns>referens till GameObjectet som laddes till</returns>
		public abstract GameObject AddGameObject(string type);

		public virtual void Draw(RenderTarget target, RenderStates states)
		{
			foreach (GameObject gameObject in GameObjects)
			{
				target.Draw(gameObject);
			}
		}

		virtual public void Update()
		{
			collisionHandler.Update();

			UpdateGameObjects();

			for (int i = 0; i < GameObjects.Count; i++)
			{
				if (GameObjects[i].Destroyed)
				{
					GameObjects.RemoveAt(i);
					i--;
				}
			}
		}


		private void UpdateGameObjects()
		{
			foreach (GameObject gameObject in GameObjects)
			{
				gameObject.Update();
			}
		}

		abstract public void Dispose();
	}
}
