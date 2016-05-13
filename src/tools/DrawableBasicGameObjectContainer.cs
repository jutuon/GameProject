using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameProject
{
	public class DrawableBasicGameObjectContainer<T> : GameObjectContainer<T>, IDrawable where T : DrawableBasicGameObject
	{
		public void Draw(SpriteBatch spriteBatch, GameTime time, Camera camera)
		{
			foreach (T item in list)
			{
				item.Draw(spriteBatch, time, camera);
			}
		}
	}
}

