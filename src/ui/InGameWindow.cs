using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameProject
{

	public class InGameWindow : ComponentList<IComponent>
	{
		public bool IsVisible { get; set;}

		public Texture2D Background { get; set;}
		private GameWindow window;

		private int windowWidth, windowHeight;

		public InGameWindow(GameWindow window) : base(null)
		{
			IsVisible = true;
			this.window = window;
			window.ClientSizeChanged += delegate
			{
				windowWidth = window.ClientBounds.Width;
				windowHeight = window.ClientBounds.Height;
				CalculateWindowDrawingPosition();
			};
		}


		public TextList CreateAndAddTextList(SpriteFont font)
		{
			TextList textList = new TextList(this, font);
			Add(textList);
			return textList;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (!IsVisible) return;

			//draw background
			if (Background != null)
			{
				//TODO:test background drawing in InGameWindow
				Vector2 origin = new Vector2(Background.Width / 2, Background.Height / 2);
				Rectangle sourceRectangle = new Rectangle(0, 0, Background.Width, Background.Height);

				spriteBatch.Draw(Background, DrawingPosition, sourceRectangle, Color.White, 0, origin, 1.0f, SpriteEffects.None, 1);
			}
			//draw window content
			base.Draw(spriteBatch);
		}
			
		private void CalculateWindowDrawingPosition()
		{
			float x = PositionOffset.X;
			if (AlignmentX == ComponentAlignmentX.Right) x += windowWidth;
			else if (AlignmentX == ComponentAlignmentX.Center) x += windowWidth/2;

			float y = PositionOffset.Y;
			if (AlignmentY == ComponentAlignmentY.Bottom) y += windowHeight;
			else if (AlignmentY == ComponentAlignmentY.Center) y += windowHeight/2;

			DrawingPositionFromParent = new Vector2(x, y);
		}

	}
}

