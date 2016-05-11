using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameProject
{
	public abstract class Component
	{

		public float X {get; set;}
		public float Y {get; set;}

		public Component()
		{
			X = 0; Y = 0;
		}

		public abstract void Draw(SpriteBatch spriteBatch, Vector2 parentLocation);
	}
}

