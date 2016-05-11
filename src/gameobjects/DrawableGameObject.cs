using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameProject
{
	public abstract class DrawableGameObject : GameObject, IDrawable
	{
		
		public Texture2D Texture { get; private set;}

		public DrawableGameObject(Texture2D texture)
		{
			Texture = texture;	
		}

		public virtual void Draw(SpriteBatch spriteBatch, GameTime time, Camera camera)
		{
			Vector2? convertedCoordinates = camera.ToScreenCoordinants(this);
			if (convertedCoordinates == null) return;

			Vector2 location = (Vector2) convertedCoordinates;
			Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
			Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);

			spriteBatch.Draw(Texture, location, sourceRectangle, Color.White, Angle, origin, 1.0f, SpriteEffects.None, 1);

		}	
	}
}

