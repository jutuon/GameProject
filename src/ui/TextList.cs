using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameProject
{
	/// <summary>
	/// List of TextObjects
	/// </summary>
	public class TextList : Component
	{
		
		private List<TextObject> texts;
		private SpriteFont font;

		private int lineWidth;

		/// <summary>
		/// Gets or sets the width of the line in all texts.
		/// </summary>
		/// <value>The width of the line.</value>
		public int LineWidth { 
			get
			{
				return lineWidth;
			}
			set
			{
				foreach (var item in texts) {
					item.LineWidth = value;
				}
				lineWidth = value;
				//TODO: update text list when linewidth is set
			}
		}

		private int height;
		/// <summary>
		/// Gets the height of text list in pixels.
		/// </summary>
		/// <value>The height in pixels</value>
		public override int Height
		{
			get
			{
				return height;
			}
		}

		//TODO: implement width calculation in TextList
		private int width;
		/// <summary>
		/// Gets the width of text list in pixels.
		/// </summary>
		/// <value>The width in pixels</value>
		public override int Width
		{
			get
			{
				if (lineWidth <= 0) return width;

				return lineWidth;
			}
		}


		public TextList(SpriteFont font)
		{
			texts = new List<TextObject>();
			this.font = font;
			lineWidth = 0;
			height = 0;
			width = 0;
		}

		/// <summary>
		/// Creates and adds new TextObject to the list.
		/// </summary>
		/// <param name="text">Created TextObject.</param>
		public TextObject Add(String text) {
			TextObject textObject = new TextObject(font, text);
			textObject.LineWidth = LineWidth;

			textObject.Position += new Vector2(0, Height);

			Console.WriteLine(textObject.Position);

			height += textObject.Height;

			texts.Add(textObject);

			return textObject;
		}

		/// <summary>
		/// Removes all current texts in the list
		/// </summary>
		public void Clear() {
			texts.Clear();
			height = 0;
		}

		public override void Draw(SpriteBatch spriteBatch, Vector2 parentLocation)
		{
			Vector2 location = Position + parentLocation;

			foreach (var item in texts)
			{
				item.Draw(spriteBatch, location);
			}
		}
	}
}

