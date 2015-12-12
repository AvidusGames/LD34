using GameCore.Interfaces;
using GameCore.Objects;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Core
{
	public class Game
	{
		private IState currentState;
		private Clock clock;

        public delegate void Init(Game game);

		public static RenderWindow Window{ get; private set; }

		public static Time TimeBetweenFrames { get; private set; }

		public Game(RenderWindow window)
		{
			Window = window;
			clock = new Clock();

			Window.Closed += Window_Closed;
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			Window.Close();
		}

		public void Start(Init init)
		{
			Input.InitEvents(Window);
            init(this);
			Loop();
		}

		private void Loop()
		{
			while (Window.IsOpen)
			{
                if (currentState == null)
                {
                    Console.WriteLine("ERROR: Current state is not set!");
                    continue;
                }
                Window.DispatchEvents();

				Draw();
				Update();

				TimeBetweenFrames = clock.Restart();
			}
		}

		public void ChangeState(IState state)
		{
			if (currentState != null)
				currentState.Dispose();
			currentState = state;
		}

		private void Draw()
		{
            Window.Clear();
			Window.Draw(currentState);
			Window.Display();
		}

		private void Update()
		{

            currentState.Update();
		}
	}
}
