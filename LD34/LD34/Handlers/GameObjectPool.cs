using GameCore.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace GameCore.Handlers
{
	public class GameObjectPool <T> where T : GameObject
	{
		private Stack<T> pool = new Stack<T>();

		public GameObjectPool(Func<T> poolObjectInit, int poolSize)
		{
			for (int i = 0; i < poolSize; i++)
			{
				pool.Push(poolObjectInit());
			}
		}

		public void Acquire(T value)
		{
			pool.Push(value);
		}

		public T Release()
		{
			try
			{
				T tmp = pool.Pop();
				return tmp;
			}
			catch (InvalidOperationException)
			{ 
				throw new PoolEmptyException();
			}
		}
	}

	[Serializable]
	internal class PoolEmptyException : Exception
	{
		private const string message = "The pool is now empty";

		public PoolEmptyException() : base(message)
		{
		}
	}
}
