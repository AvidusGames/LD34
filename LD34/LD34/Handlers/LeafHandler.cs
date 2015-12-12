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
		private Random rand = new Random();

		public Leaf CurentLeaf { get; private set; }

		public LeafHandler(GameState gameState)
		{
			this.gameState = gameState;

			InitLeafs();
		}

		private void InitLeafs()
		{
			for (int i = 0; i < NumberOfLeafsOnBranch; i++)
			{
				int diceRoll = rand.Next(0, 100);
				Leaf tmpLeaf = (Leaf)gameState.AddGameObject(nameof(Leaf));
				if (diceRoll > 75)
				{
					//Create a left leaf
					tmpLeaf.Position = new SFML.System.Vector2f(158, i * 120 + 70);
					tmpLeaf.LeaftLeaf = true;
				}
				//Create a right leaf
				else
				{
					tmpLeaf.Position = new SFML.System.Vector2f(480, i * 120 + 70);
					tmpLeaf.LeaftLeaf = false;
				}

				leafs.Add(tmpLeaf);

			}
			CurentLeaf = leafs.LastOrDefault();
		}

		/// <summary>
		/// Moves you to next level in tree.
		/// </summary>
		internal void Climb()
		{
			for (int i = 0; i < leafs.Count; i++)
			{
				if (leafs[i].LeaftLeaf)
				{
					leafs[i].LeaftLeaf = false;
					leafs[i].Position = new SFML.System.Vector2f(480, i * 120 + 70);
				}
				else
				{
					leafs[i].Position = new SFML.System.Vector2f(158, i * 120 + 70);
					leafs[i].LeaftLeaf = true;
				}
			}
		}
	}
}
