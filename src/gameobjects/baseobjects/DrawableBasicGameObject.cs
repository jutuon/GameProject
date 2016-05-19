using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameProject
{
	public class DrawableBasicGameObject : BasicGameObject
	{
		public Texture2D Texture { get; private set;}
		public bool Visible { get; set;}

		public DrawableBasicGameObject(Texture2D texture)
		{
			Texture = texture;
			Visible = true;
		}

		public virtual void Draw(SpriteBatch spriteBatch, ScreenCoordinateInfo coordinates)
		{
			Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
			Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);

			spriteBatch.Draw(Texture, coordinates.ScreenCoords, sourceRectangle, Color.White, Angle, origin, 1.0f, SpriteEffects.None, 1);

		}
			
	}
}

