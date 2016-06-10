using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameProject
{
	public class Background : DrawableBasicGameObject
	{

		public bool IsStatic { get; set;}

		public Background(Texture2D texture) : base(texture)
		{
			IsStatic = false;
		}


		public override void Draw(SpriteBatch spriteBatch, ScreenCoordinateInfo coordiantes)
		{
			Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
			Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);

			if (IsStatic)
			{
				float scale = coordiantes.Camera.WindowWidth * 1.0f / Texture.Width;
				Vector2 location1 = new Vector2(coordiantes.Camera.WindowWidth / 2, coordiantes.Camera.WindowHeight / 2);
				spriteBatch.Draw(Texture, location1, sourceRectangle, Color.White, DrawingAngle, origin, scale, SpriteEffects.None, 1);

				return;
			}

			//TODO: precalculate infinite background drawing positions, save coordinates to ScreenCoordinateInfo?
			float distanceX = coordiantes.Camera.Position.X - Position.X;
			float distanceY = coordiantes.Camera.Position.Y - Position.Y;

			int picturesX = (int) distanceX / Texture.Width;
			int picturesY = (int) distanceY / Texture.Height;

			Vector2 imageWorldCoordinates = new Vector2(picturesX * Texture.Width, picturesY * Texture.Height);

			Vector2 baseLocation = coordiantes.ToScreenCoordinants(imageWorldCoordinates);

			for (int row = -2 ; row <= 2; row++)
			{
				for (int colum = -2 ; colum <= 2; colum++)
				{
					Vector2 location = baseLocation + new Vector2(row * Texture.Width, colum * Texture.Height);
					spriteBatch.Draw(Texture, location, sourceRectangle, Color.White, DrawingAngle, origin, 1.0f, SpriteEffects.None, 1);
				}
			}



		}
	}
}

