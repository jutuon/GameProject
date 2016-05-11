using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameProject
{
	public interface IDrawable
	{
		void Draw(SpriteBatch spriteBatch, GameTime time, Camera camera);
	}
}

