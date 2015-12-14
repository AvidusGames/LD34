using GameCore.Core;
using GameCore.Interfaces;
using GameCore.Objects;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Tween
{
	public class Tweener
	{
		private bool moving;
		public int Speed { get; set; }
		private float elapsed;
		private GameObject start;
		private Vector2f end;
		private Vector2f direction;
		private float distance;
		private Vector2f endVec;
		private Vector2f startVec;

		public Tweener()
		{

		}

		public void StartMoveTowards(GameObject start , Vector2f end)
		{
			if (start.Position.X == end.X)
				return;

			this.start = start;
			this.end = end;

			startVec = this.start.Position;
			endVec = end;
			Speed = 1000;
			elapsed = Game.TimeBetweenFrames.AsSeconds();

			distance = Distance(startVec, endVec);
			direction = Normalize(endVec - startVec);
			moving = true;

		}

		private Vector2f Normalize(Vector2f a)
		{
			return new Vector2f(a.X / Length(a), a.Y/Length(a));
		}

		private float Length(Vector2f a)
		{
			return (float)Math.Sqrt((float)a.X * a.X + a.Y * a.Y);
		}

		private float Distance(Vector2f startVec, Vector2f endVec)
		{
			return Length(endVec - startVec);
		}

		public bool Move(GameObject start, Vector2f end)
		{
			startVec = start.Position;
			endVec = end;
			Speed = 1000;
			elapsed = Game.TimeBetweenFrames.AsSeconds();

			distance = Distance(startVec, endVec);
			direction = Normalize(endVec - startVec);
			moving = true;

			if (start.Position == end)
				return moving;

			this.start = start;
			this.end = end;



			if (moving)
			{
				start.Position += direction * Speed * elapsed;
				//Console.WriteLine(start.ToString() + start);
				if (Distance(startVec, start.Position) >= distance)
				{
					start.Position = endVec;
					moving = false;
					return moving;
				}
			}

			return moving;
		}
	}
}