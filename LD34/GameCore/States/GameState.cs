using GameCore.Interfaces;
using System.Collections.Generic;
using SFML.Graphics;
using GameCore.Core;
using GameCore.Handlers;
using GameCore.Objects;
using System;
using System.Threading;

namespace GameCore.States
{
	public abstract class GameState : IState
	{
		public Game Game { get; private set; }

		protected List<GameObject> GameObjects { get; private set; }
		protected List<Entity> EntityObjects { get; private set; }

        protected Semaphore mutex;

		private CollisionHandler collisionHandler;

		public GameState(Game game)
		{
			Game = game;
            mutex = new Semaphore(1, 1);
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
		public abstract GameObject AddGameObject(string type);

		/// <summary>
		/// Lägger till en Entity i statet.
		/// </summary>
		/// <param name="type">vilken Entity</param>
		/// <returns>referens till Entityn som laddes till</returns>
		public abstract Entity AddEntity(string type);

		public virtual void Draw(RenderTarget target, RenderStates states)
		{
            mutex.WaitOne();
			foreach (GameObject gameObject in GameObjects)
			{
				target.Draw(gameObject);
			}

			foreach (Entity entity in EntityObjects)
			{
				target.Draw(entity);
			}
            mutex.Release();
		}

		virtual public void Update()
		{


			UpdateGameObjects();

			RemoveGameObjects();
		}

		protected virtual void RemoveGameObjects()
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
            mutex.WaitOne();
            foreach (GameObject gameObject in GameObjects)
			{
				gameObject.Update();
			}

			foreach (Entity entity in EntityObjects)
			{
				entity.Update();
			}
            mutex.Release();
		}

		public virtual void Dispose()
		{
			foreach (GameObject gameObject in GameObjects)
			{
				gameObject.Dispose();
			}

			foreach (Entity entitiy in EntityObjects)
			{
				entitiy.Dispose();
			}
		}

		public virtual void FixedUpdate() => collisionHandler.FixedUpdate();
	}
}
