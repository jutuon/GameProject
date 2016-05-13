using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameProject
{

	//TODO:center according the window content width and heigth

	public enum WindowAlignmentX
	{
		Left, Center, Right
	}

	public enum WindowAlignmentY
	{
		Top, Center, Bottom
	}

	public class InGameWindow
	{
		private List<Component> list;
		public Vector2 Position { get; set;}

		public bool IsVisible { get; set;}
		public WindowAlignmentX AlignmentX { get; set;}
		public WindowAlignmentY AlignmentY { get; set;}

		public Texture2D Background { get; set;}

		private GameWindow window;

		public InGameWindow(GameWindow window)
		{
			Position = new Vector2(0, 0);

			list = new List<Component>();
			IsVisible = true;
			this.window = window;
			AlignmentX = WindowAlignmentX.Left;
			AlignmentY = WindowAlignmentY.Top;
		}

		public Component Add(Component c) {list.Add(c); return c;}
		public void Remove(Component c) {list.Remove(c);}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (!IsVisible) return;

			Vector2 location = CalculatePositionedCoordinates();

			//draw background
			if (Background != null)
			{
				Vector2 origin = new Vector2(Background.Width / 2, Background.Height / 2);
				Rectangle sourceRectangle = new Rectangle(0, 0, Background.Width, Background.Height);

				spriteBatch.Draw(Background, location, sourceRectangle, Color.White, 0, origin, 1.0f, SpriteEffects.None, 1);
			}
			//draw window content
			foreach (Component item in list)
			{
				item.Draw(spriteBatch, location);
			}
				
		}
			
		private Vector2 CalculatePositionedCoordinates()
		{
			float x = Position.X;
			if (AlignmentX == WindowAlignmentX.Right) x += window.ClientBounds.Width;
			else if (AlignmentX == WindowAlignmentX.Center) x += window.ClientBounds.Width/2;

			float y = Position.Y;
			if (AlignmentY == WindowAlignmentY.Bottom) x += window.ClientBounds.Height;
			else if (AlignmentY == WindowAlignmentY.Center) x += window.ClientBounds.Height/2;

			return new Vector2(x, y);
		}

	}
}

