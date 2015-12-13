using GameCore.Objects;
using System;
using SFML.Graphics;
using SFML.System;
using GameCore.States;

namespace LD34.Menu
{
    class Picture : GameObject
    {
        private Sprite graphics;
        private FloatRect bounds;
        private Vector2f scale;
        private bool centered;

        public Picture(Assets.Textures.ID texture, Vector2f pos, GameState gameState) : base(gameState, pos)
        {
            graphics = new Sprite(gameState.Game.GetTexture(texture));
            graphics.Position = pos;
            bounds = GetBounds();
            scale = new Vector2f(1f, 1f);
            centered = true;
        }

        public void SetCentered(bool _centered)
        {
            centered = _centered;
        }

        public void SetTexture(Assets.Textures.ID texture)
        {
            graphics = new Sprite(GameState.Game.GetTexture(texture));
            graphics.Scale = scale;
            bounds = GetBounds();

            Update();
        }

        public void SetScale(Vector2f _scale)
        {
            scale = _scale;
            graphics.Scale = _scale;
        }

        public FloatRect GetBounds()
        {
            return graphics.GetLocalBounds();
        }

        public override void Dispose()
        {
            graphics.Dispose();
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            graphics.Draw(target, states);
        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }

        public override void FixedUpdate()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            if (centered)
            {
                graphics.Position = new Vector2f(Position.X - bounds.Width / (2.0f / scale.X), Position.Y - bounds.Height / (2.0f / scale.Y));
            }
            else
            {
                graphics.Position = Position;
            }
        }
    }
}
