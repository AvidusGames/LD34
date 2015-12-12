using GameCore.Interfaces;
using SFML.Graphics;
using SFML.System;
using System;

namespace GameCore.Entities
{
    public abstract class Entity : IUpdatable, Drawable, IDisposable
    {

        public Vector2f Position { get; protected set; }

        public Entity(Vector2f pos)
        {
            Position = pos;
        }

        public bool Destroyed { get; set; }

        public abstract void Draw(RenderTarget target, RenderStates states);

        public abstract void Update();

        public abstract void FixedUpdate();

        public abstract void Dispose();
    }
}
