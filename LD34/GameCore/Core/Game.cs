using GameCore.Interfaces;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using System;

namespace GameCore.Core
{
	public class Game
	{
		private IState currentState;
		private Clock betweenFramesClock = new Clock();
		private int fixedUpdateTimer = 0;
		private const int TimeBetweenFixedUpdate = 16;
        private AnimationHolder animations;
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
            animations = new AnimationHolder();
            textures = new TextureHolder();
            sounds = new SoundHolder();
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

        public void LoadTexture(Enum id, string filename)
        {
            textures.Load(id, filename);
        }

        public Texture GetTexture(Enum id)
        {
            return textures.Get(id);
        }

        public void LoadAnimation(Enum id, string filebase)
        {
            animations.Load(id, filebase);
        }

        public void LoadAnimation(Enum id, string filebase, int delay)
        {
            animations.Load(id, filebase, delay);
        }

        public void LoadAnimationSpritesheet(Enum id, string filename, Vector2i count, Vector2i size)
        {
            animations.LoadSpritesheet(id, filename, count, size);
        }

        public void LoadAnimationSpritesheet(Enum id, string filename, Vector2i count, Vector2i size, int delay)
        {
            animations.LoadSpritesheet(id, filename, count, size, delay);
        }

        public Animation GetAnimation(Enum id)
        {
            return animations.Get(id);
        }

        public void LoadSound(Enum id, string filename)
        {
            sounds.Load(id, filename);
        }

        public SoundBuffer GetSound(Enum id)
        {
            return sounds.Get(id);
        }

        public void LoadFont(Enum id, string filename)
        {
            fonts.Load(id, filename);
        }

        public Font GetFont(Enum id)
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
