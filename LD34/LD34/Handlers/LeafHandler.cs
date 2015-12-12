using GameCore.Core;
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
		private List<Leaf> leafs = new List<Leaf>();
		private const int NumberOfLeafsOnBranch = 5;

		public LeafHandler(GameState gameState)
		{
			this.gameState = gameState;

			for (int i = 0; i < NumberOfLeafsOnBranch; i++)
			{
				Leaf tmpLeaf = (Leaf)gameState.AddGameObject(nameof(Leaf));
				if (i % 2 == 0)
					tmpLeaf.Position = new SFML.System.Vector2f(158, i * 120 + 70);
				else
					tmpLeaf.Position = new SFML.System.Vector2f(480, i * 120 + 70);



			}
		}
	}
}
