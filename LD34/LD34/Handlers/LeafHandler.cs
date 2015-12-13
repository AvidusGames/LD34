using GameCore.States;
using GameCore.Tween;
using LD34.Objects;
using SFML.System;
using System;
using System.Collections.Generic;

namespace LD34.Handlers
{
	class LeafHandler
	{
		private GameState gameState;
		private List<Leaf> leafs = new List<Leaf>();

		private const int NumberOfLeafsOnBranch = 5;

		private Random rand = new Random();
		private const int MaxNumberOfLeftLeavesAtRow = 3;
		private int numberOfLeftLeavesAtRow;

		private Tweener leafTweener = new Tweener();
		private int nextIndex = 3;

		public Leaf NextLeaf { get; private set; }
		public Leaf Top { get; private set; }
		//public Vector2f StartPos { get; private set; }

		public Leaf PlayerStandLeaf { get; private set; }

		public LeafHandler(GameState gameState)
		{
			this.gameState = gameState;

			InitLeafs();
		}

		private void InitLeafs()
		{
			for (int i = NumberOfLeafsOnBranch - 1; i >= 0; i--)
			{
				int diceRoll = rand.Next(0, 100);

				Leaf tmpLeaf = (Leaf)gameState.AddGameObject(nameof(Leaf));
				LeftOrRightLeafRand(tmpLeaf, i);

				leafs.Add(tmpLeaf);
			}
			NextLeaf = leafs[nextIndex];
			PlayerStandLeaf = leafs[nextIndex - 1];
            Console.WriteLine("Curent Leaf pos:" + NextLeaf.Position);
			
			//for (int i = 0; i < leafs.Count; i++)
			//{
			//	if (i > 0)
			//		leafs[i].Parent = leafs[i - 1];
			//	else
			//		leafs[i].Parent = null;

			//	if (i < NumberOfLeafsOnBranch - 1)
			//		leafs[i].Child = leafs[i + 1];
			//	else
			//		leafs[i].Child = null;
			//}

			//CurentLeaf = leafs.LastOrDefault();
			//Top = leafs[0];

			//BottomLeaf.Child = Top;
			//Top.Parent = BottomLeaf;
		}

		/// <summary>
		/// Moves you to next level in tree.
		/// </summary>
		internal void Climb()
		{
			ChangeLeaf();

			//for (int i = 0; i < leafs.Count; i++)
			//{
			//	LeftOrRightLeafRand(leafs[i], i);
			//}
		}
		/// <summary>
		/// a leafe.
		/// </summary>
		/// <param name="leaf">the leaf that shuld get a rand pos</param>
		/// <param name="idx">wich index is leaf in the list</param>
		private void LeftOrRightLeafRand(Leaf leaf, int idx)
		{
			int diceRoll = rand.Next(0, 100);

			if (diceRoll >= 50 && numberOfLeftLeavesAtRow <= MaxNumberOfLeftLeavesAtRow)
			{
				numberOfLeftLeavesAtRow++;
                leaf.Position = new Vector2f(320, idx * 120 + 70);
				leaf.LeftLeaf = true;
			}
			else
			{
				leaf.Position = new Vector2f(480, idx * 120 + 70);
				leaf.LeftLeaf = false;
				numberOfLeftLeavesAtRow = 0;
			}
		}

		private void LeftOrRightLeafRand(Leaf leaf)
		{
			//TODO:: make it more even
			int diceRoll = rand.Next(0, 100);

			if (diceRoll >= 50)
			{
				leaf.Position = new Vector2f(320, 70);
				leaf.LeftLeaf = true;
			}
			else
		{
				leaf.Position = new Vector2f(480, 70);
				leaf.LeftLeaf = false;
		}
		}

		private void ChangeLeaf()
		{

			for (int i = 0; i < leafs.Count; i++)
			{
				leafs[i].MoveOneStepDown();
			}

			//leafs[0].Destroyed = true;
			//leafs.RemoveAt(0);

			if (PlayerStandLeaf == leafs[leafs.Count - 2])
			{
				Leaf tmpLeaf = (Leaf)gameState.AddGameObject(nameof(Leaf));
				LeftOrRightLeafRand(tmpLeaf);
				leafs.Add(tmpLeaf);
			}

			nextIndex++;
			NextLeaf = leafs[nextIndex];
			Console.WriteLine(nextIndex);

			PlayerStandLeaf = leafs[nextIndex - 1];





		}

		internal int Fall()
		{
			//PlayerStandLeaf = null;
			int fallAMount = 0;
			bool stopedFalling = false;

			for (int i = leafs.Count - 1; i >= 0; i--)
			{
				//fallAMount++;
				if (!stopedFalling)
					nextIndex--;

				leafs[i].MoveOneStepUp();

				if (leafs[i].LeftLeaf != PlayerStandLeaf.LeftLeaf)
				{
					stopedFalling = true;
					PlayerStandLeaf = leafs[i];
				}
			}
			Console.WriteLine(nextIndex);
			return fallAMount;
		}
	}
}
