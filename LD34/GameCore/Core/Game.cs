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

        private MusicFader fader;
        private Enum currentMusic;

        private AnimationHolder animations;
        private TextureHolder textures;
        private SoundHolder sounds;
        private MusicHolder musics;
        private FontHolder fonts;

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
            fader = new MusicFader();
            animations = new AnimationHolder();
            textures = new TextureHolder();
            sounds = new SoundHolder();
            musics = new MusicHolder();
            fonts = new FontHolder();
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

        public void LoadMusic(Enum id, string filename)
        {
            musics.Load(id, filename);
        }

        public Music GetMusic(Enum id)
        {
            return musics.Get(id);
        }

        public void PlayMusic(Enum id)
        {
            Music music;
            if (currentMusic == null)
            {
                currentMusic = id;
                music = GetMusic(currentMusic);
                music.Volume = 33f;
                music.Play();
                return;
            }

            music = GetMusic(currentMusic);

            if (id == currentMusic && music.Status == SoundStatus.Playing)
            {
                return;
            }

            if (fader.fading || music.Status == SoundStatus.Stopped)
            {
                currentMusic = id;
                music = GetMusic(currentMusic);
                music.Volume = 33f;
                music.Play();
            }
        }

        public void StopMusic(bool fadeIn)
        {
            if (fader.fading) return;

            Music music = GetMusic(currentMusic);
            if (music.Status == SoundStatus.Playing)
            {
                if(fadeIn)
                {
                    fader.BeginFade(music, 1f);
                }
                else
                {
                    music.Stop();
                }
            }
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
			Window.Draw(currentState);
			Window.Display();
		}

		private void Update()
		{
            fader.Update();
            currentState.Update();
		}

        public void PlaySound(Enum id)
        {
            Sound sound = new Sound(GetSound(id));
            sound.Play();
        }
    }

    internal class MusicFader
    {
        internal Music music;
        internal float initialVolume;
        internal float currentVolume;
        internal float speed;
        internal bool fading;

        internal void BeginFade(Music _music, float _speed)
        {
            speed = 1.0f /_speed;
            music = _music;
            initialVolume = currentVolume = _music.Volume;
            fading = true;
        }

        internal void Update()
        {
            if(fading)
            {
                float elapsed = Game.TimeBetweenFrames.AsSeconds();
                currentVolume -= initialVolume * speed * elapsed;
                if(currentVolume <= 0.0)
                {
                    fading = false;
                    music.Stop();
                }else
                {
                    music.Volume = currentVolume;
                } 
            }
        }
    }
}
