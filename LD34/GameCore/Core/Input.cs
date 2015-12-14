using SFML.System;
using SFML.Window;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace GameCore.Core
{
	public static class Input
	{
		private static bool[] key;
		private static bool[] keyPressed;
        private static int mouseButton;
        private static StringBuilder sb;

        public static int mouseX, mouseY;

		static Input()
		{
			key = new bool[256];
			keyPressed = new bool[256];
		}
		public static void Reset()
		{
			for (int i = 0; i < keyPressed.Length; i++)
			{
				keyPressed[i] = false;
			}
		}

		public static void InitEvents(Window window)
		{
            sb = new StringBuilder();
            mouseButton = -1;
            mouseX = mouseY = -1;
			window.KeyPressed += Window_KeyPressed;
			window.KeyReleased += Window_KeyReleased;
            window.MouseButtonPressed += Window_MousePressed;
            window.MouseButtonReleased += Window_MouseReleased;
            window.MouseMoved += Window_MouseMoved;
            window.TextEntered += Window_TextEntered;
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
			keyPressed[(int)e.Code] = true;
        }

        private static void Window_MouseReleased(object sender, MouseButtonEventArgs e)
        {
            mouseButton = -1;
        }

        private static void Window_MousePressed(object sender, MouseButtonEventArgs e)
        {
            mouseButton = (int) e.Button;
        }

        private static void Window_MouseMoved(object sender, MouseMoveEventArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;
        }

        private static void Window_TextEntered(object sender, TextEventArgs e)
        {
            //sb.Remove(sb.Length - 1, 1);
            if(GetKeyPressed(Keyboard.Key.BackSpace) && sb.Length!=0)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            else
            {
                if(isAlphaNumeric(e.Unicode))
                {
                    foreach(var c in e.Unicode)
                    {
                        sb.Append(c);
                    }
                }
            }            
        }

        public static Boolean isAlphaNumeric(string strToCheck)
        {
            Regex rg = new Regex(@"\w+");
            return rg.IsMatch(strToCheck);
        }

        public static void ClearText()
        {
            sb.Clear();
        }

        public static string GetText()
        {
            Console.WriteLine(sb);
            return sb.ToString();
        }

        public static bool GetKeyPressed(Keyboard.Key code)
		{
			return keyPressed[(int) code];
		}

        public static bool GetMousePressed(Mouse.Button code)
        {
            return mouseButton == (int) code;
        }
    }
}