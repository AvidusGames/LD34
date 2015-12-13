using GameCore.Objects;
using System;
using SFML.Graphics;
using SFML.System;
using GameCore.States;
using GameCore.Core;

namespace LD34.Menu
{
    class Button : GameObject
    {
        private RectangleShape graphics;
        private Label label;
        private Color outlineColor;
        private Color actionColor;
        private FloatRect bounds;
        private string actionCommand;
        private float padding;
        private float delta;
        private float delay;
        private int ticks;

        public delegate void ButtonHandler(string actionCommand, bool perform);

        enum ButtonState {
            Hovering,
            Pressed,
            Normal,
        }

        private ButtonState state = ButtonState.Normal;
        private ButtonHandler handler;

        public Button(ButtonHandler _handler, string text, Vector2f pos, GameState gameState):base(gameState, pos)
		{
            graphics = new RectangleShape(new Vector2f(32, 32));
            graphics.FillColor = Color.Transparent;
            SetOutlineColor(Color.White);
            SetActionColor(Color.Cyan);
            SetOutlineThickness(5.0f);
            padding = 5.0f;
            graphics.Position = pos;

            handler = _handler;
            label = new Label("", pos, gameState);
            label.SetCentered(false);
            SetText(text);
        }

        public void SetPadding(float _padding)
        {
            padding = _padding;
            SetText(null);
        }

        public void SetFont(Assets.Fonts.ID id)
        {
            label.SetFont(id);
            SetText(null);
        }

        public void SetSize(uint size)
        {
            label.SetSize(size);
            SetText(null);
        }

        public void SetActionCommand(string _actionCommand)
        {
            actionCommand = _actionCommand;
        }

        public void SetActionDelay(int _delay)
        {
            delay = _delay;
        }

        public void SetText(string text)
        {
            if(text != null)
                label.SetText(text);

            float widthPadding = padding;
            if(state == ButtonState.Hovering)
            {
                widthPadding *= 10;
            }

            float width = label.GetBounds().Width + widthPadding * 2;
            float height = label.GetBounds().Height + padding * 2;

            graphics.Size = new Vector2f(width, height);
            label.Position = new Vector2f(Position.X + widthPadding - width/2, Position.Y - height / 2);
            UpdatePosition();
            bounds = new FloatRect(graphics.Position.X, graphics.Position.Y, width, height);
        }

        public void SetTextColor(Color color)
        {
            label.SetColor(color);
        }

        public void SetActionColor(Color color)
        {
            actionColor = color;
        }

        public void SetOutlineColor(Color color)
        {
            outlineColor = color;
            graphics.OutlineColor = color;
        }

        public void SetOutlineThickness(float thickness)
        {
            graphics.OutlineThickness = thickness;
        }

        public override void Dispose()
        {
			graphics.Dispose();
            label.Dispose();
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            graphics.Draw(target, states);
            label.Draw(target, states);
        }

        public override void FixedUpdate()
        {
            throw new NotImplementedException();
        }

		public override void Reset()
		{
			throw new NotImplementedException();
		}

		public override void Update()
        {
            if(state != ButtonState.Pressed)
            {
                if(bounds != null && bounds.Contains((int) Input.mouseX, (int) Input.mouseY))
                {
                    if(Input.GetMousePressed(SFML.Window.Mouse.Button.Left))
                    {
                        if (state != ButtonState.Pressed)
                            HandleEvent(ButtonState.Pressed);
                    } else
                    {
                        if (state != ButtonState.Hovering)
                            HandleEvent(ButtonState.Hovering);
                    }
                }else
                {
                    if (state != ButtonState.Normal)
                        HandleEvent(ButtonState.Normal);
                }                
            }

            if(delta > 0.0f)
            {
                delta -= Game.TimeBetweenFrames.AsSeconds();
            }

            if (state == ButtonState.Pressed && ticks % 30 == 0)
            {
                graphics.OutlineColor = (graphics.OutlineColor == actionColor) ? outlineColor : actionColor;
                
                if(delta <= 0.0f)
                {
                    handler(actionCommand, true);
                }
            }
            ticks++;
        }

        private void UpdatePosition()
        {
            graphics.Position = new Vector2f(Position.X - graphics.Size.X / 2, Position.Y - graphics.Size.Y / 2);
            label.Update();
        }

        private void HandleEvent(ButtonState _state)
        {
            state = _state;
            switch (_state)
            {
                case ButtonState.Hovering:
                    SetText(null);
                    break;
                case ButtonState.Pressed:
                    handler(actionCommand, false);
                    ticks = 0;
                    delta = delay;
                    // play audio effect
                    break;
                case ButtonState.Normal:
                    SetText(null);
                    break;
            }
        }
    }
}
