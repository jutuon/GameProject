using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameProject
{
	public abstract class Component
	{

		public Vector2 Position {get; set;}

		public Component()
		{
			Position = new Vector2(0,0);
		}

		public abstract void Draw(SpriteBatch spriteBatch, Vector2 parentLocation);
	}
}

