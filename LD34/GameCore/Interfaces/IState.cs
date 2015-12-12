using SFML.Graphics;
using System;

namespace GameCore.Interfaces
{
	public interface IState : IUpdatable, Drawable, IDisposable { }
}