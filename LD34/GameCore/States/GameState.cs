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
		protected List<Entity> EntityObjects { get; private set; }

		private CollisionHandler collisionHandler;

		public GameState(Game game)
		{
			Game = game;

			Init();
		}

		private void Init()
		{
			GameObjects = new List<GameObject>();
			EntityObjects = new List<Entity>();
			collisionHandler = new CollisionHandler(GameObjects);
		}
		/// <summary>
		/// Lägger till ett GameObject i statet.
		/// </summary>
		/// <param name="type">vilket GameObject</param>
		/// <returns>referens till GameObjectet som laddes till</returns>
		public abstract Entity AddGameObject(string type);

		/// <summary>
		/// Lägger till en Entity i statet.
		/// </summary>
		/// <param name="type">vilken Entity</param>
		/// <returns>referens till Entityn som laddes till</returns>
		public abstract Entity AddEntity(string type);

		public virtual void Draw(RenderTarget target, RenderStates states)
		{
			foreach (GameObject gameObject in GameObjects)
			{
				target.Draw(gameObject);
			}

			foreach (Entity entity in EntityObjects)
			{
				target.Draw(entity);
			}
		}

		virtual public void Update()
		{


			UpdateGameObjects();

			RemoveGameObjects();
		}

		private void RemoveGameObjects()
		{
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

			foreach (Entity entity in EntityObjects)
			{
				entity.Update();
			}
		}

		abstract public void Dispose();

		public virtual void FixedUpdate() => collisionHandler.FixedUpdate();
	}
}
