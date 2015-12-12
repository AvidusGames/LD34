using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Core
{
	public static class Input
	{
		private static bool[] key;

		static Input()
		{
			key = new bool[256];
		}

		public static void InitEvents(Window window)
		{
			window.KeyPressed += Window_KeyPressed;
			window.KeyReleased += Window_KeyReleased;
		}

		public static bool GetKey(int code)
		{
			return key[code];
		}

		private static void Window_KeyReleased(object sender, KeyEventArgs e)
		{
			key[(int)e.Code] = false;
		}

		private static void Window_KeyPressed(object sender, KeyEventArgs e)
		{
			key[(int)e.Code] = true;
		}
	}
}