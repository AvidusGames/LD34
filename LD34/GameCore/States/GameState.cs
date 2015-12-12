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

		protected List<Entity> EntityObjects { get; private set; }

		private CollisionHandler collisionHandler;

		public GameState(Game game)
		{
			Game = game;
			EntityObjects = new List<Entity>();

			Init();
		}

		private void Init()
		{
			EntityObjects = new List<Entity>();
			collisionHandler = new CollisionHandler(EntityObjects);
		}
		/// <summary>
		/// Lägger till en Entity i statet.
		/// </summary>
		/// <param name="type">vilken Entity</param>
		/// <returns>referens till Entityn som laddes till</returns>
		public abstract Entity AddEntity(string type);

		public virtual void Draw(RenderTarget target, RenderStates states)
		{
			foreach (GameObject gameObject in EntityObjects)
			{
				target.Draw(gameObject);
			}
		}

		virtual public void Update()
		{


			UpdateGameObjects();

			for (int i = 0; i < EntityObjects.Count; i++)
			{
				if (EntityObjects[i].Destroyed)
				{
					EntityObjects.RemoveAt(i);
					i--;
				}
			}
		}


		private void UpdateGameObjects()
		{
			foreach (GameObject gameObject in EntityObjects)
			{
				gameObject.Update();
			}
		}

		abstract public void Dispose();

		public virtual void FixedUpdate() => collisionHandler.FixedUpdate();
	}
}
