using GameCore.Objects;
using System;
using SFML.Graphics;
using SFML.System;
using GameCore.States;

namespace LD34.Menu
{
    class Table : GameObject
    {
        private Font font;
        private Color color;
        //private FloatRect bounds;
        private bool centered;
        private uint size;
        private string[,] data;

        public Table(string[,] _data, Vector2f pos, GameState gameState) : base(gameState, pos)
        {
            size = 12;
            font = gameState.Game.GetFont(Assets.Fonts.ID.Default);
            color = Color.White;
            //bounds = GetBounds();
            centered = true;
            SetData(_data);
        }

        public void SetCentered(bool _centered)
        {
            centered = _centered;
        }

        public void SetData(string[,] _data)
        {
            if (_data != null)
            {
                data = _data;
            }

            //bounds = GetBounds();
            //Update();
        }

        public void SetFont(Assets.Fonts.ID id)
        {
            font = GameState.Game.GetFont(id);
            SetData(null);
        }

        public void SetColor(Color _color)
        {
            color = _color;
            SetData(null);
        }

        public void SetSize(uint _size)
        {
            size = _size;
            SetData(null);
        }

        public override void Dispose()
        {
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            //graphics.Draw(target, states);
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
            /**
            if (centered)
            {
                graphics.Position = new Vector2f(Position.X - bounds.Width / 2, Position.Y - bounds.Height / 2);
            }
            else
            {
                graphics.Position = Position;
            }
    */
        }
    }
}
