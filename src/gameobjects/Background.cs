﻿using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameProject
{
	public class Background : DrawableGameObject
	{

		public bool IsStatic { get; set;}

		public Background(Texture2D texture) : base(texture)
		{
			IsStatic = true;
		}


		public override void Draw(SpriteBatch spriteBatch, GameTime time, Camera camera)
		{
			Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
			Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);

			if (IsStatic)
			{
				float scale = camera.Window.ClientBounds.Width * 1.0f / Texture.Width;
				GameProject.debugText.Text = scale + "";
				Vector2 location1 = new Vector2(camera.Window.ClientBounds.Width / 2, camera.Window.ClientBounds.Height / 2);
				spriteBatch.Draw(Texture, location1, sourceRectangle, Color.White, Angle, origin, scale, SpriteEffects.None, 1);

				return;
			}


			float x = (camera.X - camera.Window.ClientBounds.Width) / Texture.Width;
			float y = (camera.Y - camera.Window.ClientBounds.Height) / Texture.Height;

			//Console.WriteLine(x + ", " + y);

			Vector2? convertedCoordinates = camera.ToScreenCoordinants(x, y);
			if (convertedCoordinates == null) return;

			Vector2 location = (Vector2) convertedCoordinates;



			spriteBatch.Draw(Texture, location, sourceRectangle, Color.White, Angle, origin, 1.0f, SpriteEffects.None, 1);
		}
	}
}

