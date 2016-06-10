using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameProject
{
	public class DrawableBasicGameObject : BasicGameObject
	{
		public Texture2D Texture { get; private set;}
		public bool Visible { get; set;}

		public float DrawingAngle { get; set;}

		public DrawableBasicGameObject(Texture2D texture)
		{
			Texture = texture;
			Visible = true;
			DrawingAngle = 0;
		}

		public virtual void Draw(SpriteBatch spriteBatch, ScreenCoordinateInfo coordinates)
		{
			if (!coordinates.OnScreen) return;

			Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
			Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);

			spriteBatch.Draw(Texture, coordinates.ScreenCoords, sourceRectangle, Color.White, DrawingAngle, origin, 1.0f, SpriteEffects.None, 1);

		}
			
	}
}

