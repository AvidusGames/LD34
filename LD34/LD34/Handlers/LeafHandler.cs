using GameCore.Objects;
using GameCore.States;
using LD34.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD34.Handlers
{
	class LeafHandler
	{
		private GameState gameState;
		public LeafHandler(GameState gameState)
		{
			this.gameState = gameState;

			gameState.AddGameObject(nameof(Leaf)).Position = new SFML.System.Vector2f(250, 50);
			gameState.AddGameObject(nameof(Leaf)).Position = new SFML.System.Vector2f(250, 100);
			gameState.AddGameObject(nameof(Leaf)).Position = new SFML.System.Vector2f(250, 150);
			gameState.AddGameObject(nameof(Leaf)).Position = new SFML.System.Vector2f(250, 210);
		}
	}
}
