using GameCore.Core;
using GameCore.Objects;
using GameCore.States;
using LD34.Objects;
using SFML.System;
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
		private int activeLeaf = NumberOfLeafsOnBranch;

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
				LeftOrRightLeafRand(tmpLeaf, i);

				leafs.Add(tmpLeaf);
			}
			CurentLeaf = leafs[leafs.Count - 1];

			for (int i = 0; i < leafs.Count; i++)
			{
				if (i > 0)
					leafs[i].Child = leafs[i - 1];
				else
					leafs[i].Child = null;

				if (i < NumberOfLeafsOnBranch - 1)
					leafs[i].Parent = leafs[i + 1];
				else
					leafs[i].Parent = null;
			}
		}

		/// <summary>
		/// Moves you to next level in tree.
		/// </summary>
		internal void Climb()
		{
			// Kolla om det lövet spelare står på inte är utanför. Annars gå tillbacks till toppen.
			if (activeLeaf <= 4)
				ChangeLeaf();
			else
			{
				activeLeaf = leafs.Count;
			}

			//for (int i = 0; i < leafs.Count; i++)
			//{
			//	LeftOrRightLeafRand(leafs[i], i);
			//}
		}
		/// <summary>
		/// Randomize a true or false to wich side leaf shuld be on.
		/// </summary>
		/// <param name="leaf">the leaf that shuld get a rand pos</param>
		/// <param name="idx">wich index is leaf in the list</param>
		private void LeftOrRightLeafRand(Leaf leaf, int idx)
		{
			int diceRoll = rand.Next(0, 100);

			if (diceRoll >= 50)
			{
				leaf.Position = new Vector2f(480, idx * 120 + 70);
				leaf.LeaftLeaf = true;
			}
			else
			{
				leaf.Position = new Vector2f(158, idx * 120 + 70);
				leaf.LeaftLeaf = false;
			}
		}

		public Leaf NextLeaf()
		{
			return CurentLeaf.Parent;
		}

		private void ChangeLeaf()
		{
			for (int i = 0; i < leafs.Count - 2; i++)
			{
				leafs[i].Position = new Vector2f(leafs[i + 1].Position.Y, leafs[i].Position.X);
			}

			CurentLeaf = leafs.LastOrDefault(); ;
		}
	}
}
