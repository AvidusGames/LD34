using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using GameCore.Core;
using GameCore.Objects;
using SFML.Window;
using SFML.Audio;
using GameCore.States;

namespace LD34.Objects
{
	public class TestGameObject : GameObject
	{
		private Vector2f position = new Vector2f(0, 0);
        private Sprite graphics;

		public TestGameObject(GameState gameState, Vector2f pos):base(gameState, pos)
		{
			Bounds = new FloatRect(Position.X, Position.Y, 32, 32);
            graphics = new Sprite(GameState.Game.GetTexture(GameCore.Core.Textures.ID.Player));

            var sound = new Sound();
            sound.SoundBuffer = GameState.Game.GetSounds(GameCore.Core.Sounds.ID.Jump);
            sound.Play();

        }

		public override void Draw(RenderTarget target, RenderStates states)
		{
			target.Draw(graphics);
		}

		public override void OnCollision(GameObject target)
		{
			target.Destroyed = true;
		}

		public override void Update()
		{
			if (Input.GetKey((int)Keyboard.Key.Right))
			{
				Move(2, 0);
			}
			graphics.Position = Position;
		}

		public override void Dispose()
		{
			throw new NotImplementedException();
		}

		public override void FixedUpdate()
		{
			throw new NotImplementedException();
		}
	}
}
