using GameCore.Interfaces;
using GameCore.Objects;
using SFML.Audio;
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
		private Clock betweenFramesClock = new Clock();
		private int fixedUpdateTimer = 0;
		private const int TimeBetweenFixedUpdate = 16;
        private TextureHolder textures;
        private SoundHolder sounds;
        private FontHolder fonts;
        //private Sprite background;

        public delegate void Init(Game game);

		public static RenderWindow Window{ get; private set; }

		public static Time TimeBetweenFrames { get; private set; }

		public Game(RenderWindow window)
		{
			Window = window;

			Window.Closed += Window_Closed;
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			Window.Close();
		}

		public void Start(Init init)
		{
			Input.InitEvents(Window);
            sounds = new SoundHolder();
            textures = new TextureHolder();
            fonts = new FontHolder();
            init(this);
			//background = new Sprite(textures.Get(Textures.ID.Background));
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
				Input.Reset();
                Window.DispatchEvents();

				Draw();
				if (fixedUpdateTimer >= TimeBetweenFixedUpdate)
				{
					currentState.FixedUpdate();
					fixedUpdateTimer = 0;
				}
				Update();

				TimeBetweenFrames = betweenFramesClock.Restart();
				fixedUpdateTimer += TimeBetweenFrames.AsMilliseconds();
			}
		}

        public void LoadTexture(Textures.ID id, string filename)
        {
            textures.Load(id, filename);
        }

        public Texture GetTexture(Textures.ID id)
        {
            return textures.Get(id);
        }

        public void LoadSound(Sounds.ID id, string filename)
        {
            sounds.Load(id, filename);
        }

        public SoundBuffer GetSound(Sounds.ID id)
        {
            return sounds.Get(id);
        }

        public void LoadFont(Fonts.ID id, string filename)
        {
            fonts.Load(id, filename);
        }

        public Font GetFont(Fonts.ID id)
        {
            return fonts.Get(id);
        }

        public void ChangeState(IState state)
		{
			if (currentState != null)
				currentState.Dispose();

            if (state == null)
            {
                Window.Close();
            }else
            {
                currentState = state;
            }
		}

		private void Draw()
		{
            Window.Clear();
            //Window.Draw(background);
			Window.Draw(currentState);
			Window.Display();
		}

		private void Update()
		{

            currentState.Update();
		}
	}
}
