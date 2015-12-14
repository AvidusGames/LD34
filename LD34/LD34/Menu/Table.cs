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
        private FloatRect bounds;
        private bool centered;
        private int padding;
        private uint size;
        private string[,] data;

        public Table(string[,] _data, Vector2f pos, GameState gameState) : base(gameState, pos)
        {
            size = 12;
            padding = 15;
            font = Assets.Fonts.ID.Default;
            color = Color.White;
            //bounds = GetBounds();
            centered = true;
            if(_data != null) SetData(_data);
        }

        public void SetPadding(int _padding)
        {
            padding = _padding;
        }

        public void SetCentered(bool _centered)
        {
            centered = _centered;
        }

        public void SetData(string[,] _data)
        {
            /**
            if(_data == null)
            {
                if (data != null)
                {
                    _data = data;
                }else
                {
                    return;
                }
            }else
            {
                data = _data;
            }
    */
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
                        xOffset = 0;
                        for (int k = 0; k < _data.GetLength(1); k++)
                        {
                            FloatRect tmpBounds = cells[x - 1, k].GetBounds();
                            float tmpOffset = (cells[x - 1, k].Position.X + tmpBounds.Width) - Position.X - left.Width;
                            if (tmpOffset > xOffset) xOffset = tmpOffset;
                        }
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
                    cells[x, y].Position = new Vector2f(Position.X + left.Width + xOffset + padding, Position.Y + top.Height + yOffset + padding);
                }
            }

            if(_data != null)
            {
                data = _data;
                bounds = GetBounds();
                Update();
            }
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

        public FloatRect GetBounds()
        {
            FloatRect rect = new FloatRect();
            if (data != null)
            {
                Label width = cells[cells.GetLength(0)-1, 0];
                Label height = cells[0, cells.GetLength(1)-1];
                rect.Width = width.Position.X + width.GetBounds().Width - Position.X;
                rect.Height = width.Position.Y + width.GetBounds().Height - Position.Y;
            }
            return rect;
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
                        if (centered)
                        {
                            Vector2f labelPos = cells[x, y].Position;
                            cells[x, y].UpdateGraphicsPos(new Vector2f(labelPos.X - bounds.Width / 2, labelPos.Y - bounds.Height / 2));
                        }
                    }
                }
            }
        }
    }
}
