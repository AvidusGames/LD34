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
        private FloatRect bounds;
        private float padding;
        private int ticks;

        enum ButtonState {
            Hovering,
            Pressed,
            Normal,
        }

        private ButtonState state = ButtonState.Normal;

        public Button(string text, Vector2f pos, GameState gameState):base(gameState, pos)
		{
            graphics = new RectangleShape(new Vector2f(32, 32));
            graphics.FillColor = Color.Transparent;
            graphics.OutlineColor = Color.White;
            graphics.OutlineThickness = 5.0f;
            padding = 5.0f;
            graphics.Position = pos;
            
            label = new Label("", pos, gameState);
            SetText(text);
        }

        public void SetPadding(float _padding)
        {
            padding = _padding;
            SetText(null);
        }

        public void SetText(string text)
        {
            if(text != null)
                label.SetText(text);

            float tmpPadding = padding;
            if(state == ButtonState.Hovering)
            {
                tmpPadding *= 10;
            }

            float width = label.GetBounds().Width + tmpPadding * 2;
            float height = label.GetBounds().Height + tmpPadding * 2;

            graphics.Size = new Vector2f(width, height);
            label.Position = new Vector2f(Position.X + tmpPadding - width/2, Position.Y - height / 2);
            bounds = graphics.GetLocalBounds();
            Update();
        }

        public void SetTextColor(Color color)
        {
            label.SetColor(color);
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
            throw new NotImplementedException();
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

        public override void Update()
        {
            label.Update();
            if(bounds != null && bounds.Contains(Input.mouseX, Input.mouseY))
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
            graphics.Position = new Vector2f(Position.X - graphics.Size.X/2, Position.Y - graphics.Size.Y/2);

            if(state == ButtonState.Pressed && ticks % 30 == 0)
            {
                SetOutlineColor(graphics.OutlineColor == Color.Cyan ? outlineColor : Color.Cyan);
                if(++ticks >= 120)
                {
                    // call function
                    Dispose();
                }
            }

        }

        private void HandleEvent(ButtonState _state)
        {
            switch(_state)
            {
                case ButtonState.Hovering:
                    SetText(null);
                    break;
                case ButtonState.Pressed:
                    ticks = 0;
                    // play audio effect
                    break;
                case ButtonState.Normal:
                    //SetText(null);
                    break;
            }
            state = _state;
        }
    }
}
