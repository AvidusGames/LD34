using GameCore.Objects;
using System;
using SFML.Graphics;
using SFML.System;
using GameCore.States;

namespace LD34.Menu
{
    class Table : GameObject
    {
        private Assets.Fonts.ID font;
        private Color color;
        private Label[,] cells;
        //private FloatRect bounds;
        private bool centered;
        private uint size;
        private string[,] data;

        public Table(string[,] _data, Vector2f pos, GameState gameState) : base(gameState, pos)
        {
            size = 12;
            font = Assets.Fonts.ID.Default;
            color = Color.White;
            //bounds = GetBounds();
            centered = true;
            if(_data != null) SetData(_data);
        }

        public void SetCentered(bool _centered)
        {
            centered = _centered;
        }

        public void SetData(string[,] _data)
        {
            cells = new Label[_data.GetLength(0), _data.GetLength(1)];
            for (int x = 0; x < _data.GetLength(0); x++)
            {
                for (int y = 0; y < _data.GetLength(1); y++)
                {
                    var text = _data[x, y];
                    cells[x, y] = new Label(text, new Vector2f(Position.X, Position.Y), GameState);
                    cells[x, y].SetCentered(false);
                    cells[x, y].SetFont(font);
                    cells[x, y].SetSize(size);
                }
            }

            for (int x = 0; x < _data.GetLength(0); x++)
            {
                for (int y = 0; y < _data.GetLength(1); y++)
                {
                    FloatRect left, top;
                    float xOffset = 0, yOffset = 0;

                    if (x > 0)
                    {
                        left = cells[x - 1, y].GetBounds();
                        xOffset = cells[x - 1, y].Position.X - Position.X;
                    }else
                    {
                        left = new FloatRect();
                    }
                    if (y > 0)
                    {
                        top = cells[x, y - 1].GetBounds();
                        yOffset = cells[x, y - 1].Position.Y - Position.Y;
                    }
                    else
                    {
                        top = new FloatRect();
                    }
                    cells[x, y].Position = new Vector2f(Position.X + left.Width + xOffset, Position.Y + top.Height + yOffset);
                }
            }

            if (_data != null)
            {
                data = _data;
            }
            //bounds = GetBounds();
            //Update();
        }

        public void SetFont(Assets.Fonts.ID id)
        {
            font = id;
            //SetData(null);
        }

        public void SetColor(Color _color)
        {
            color = _color;
            //SetData(null);
        }

        public void SetSize(uint _size)
        {
            size = _size;
            //SetData(null);
        }

        public override void Dispose()
        {
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            //graphics.Draw(target, states);
            if(data != null)
            {
                for (int x = 0; x < cells.GetLength(0); x++)
                {
                    for (int y = 0; y < cells.GetLength(1); y++)
                    {
                        cells[x, y].Draw(target, states);
                    }
                }
            }
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
            if (data != null)
            {
                for (int x = 0; x < cells.GetLength(0); x++)
                {
                    for (int y = 0; y < cells.GetLength(1); y++)
                    {
                        cells[x, y].Update();
                    }
                }
            }
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
