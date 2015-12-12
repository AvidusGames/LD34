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
            sound.SoundBuffer = GameState.Game.GetSound(GameCore.Core.Sounds.ID.Jump);
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
                if (Position.X + 2 > Game.Window.Size.X)
                {
                    Position = new Vector2f(Game.Window.Size.X, Position.Y);
                }
                else
                {
                    Move(2, 0);
                }
            }
            if (Input.GetKey((int)Keyboard.Key.Left))
            {
                if (Position.X - 2 < 0)
                {
                    Position = new Vector2f(0, Position.Y);
                }
                else
                {
                    Move(-2, 0);
                }
            }
            if (Input.GetKey((int)Keyboard.Key.Down))
            {
                if (Position.Y + 2 > Game.Window.Size.Y)
                {
                    Position = new Vector2f(Position.X, Game.Window.Size.Y);
                }
                else
                {
                    Move(0, 2);
                }
            }
            if (Input.GetKey((int)Keyboard.Key.Up))
            {
                if (Position.X - 2 < 0)
                {
                    Position = new Vector2f(Position.X, 0);
                }
                else
                {
                    Move(0, -2);
                }
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

		public override void Reset()
		{
			throw new NotImplementedException();
		}
	}
}
