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

		private uint preferredLineWidth;

		/// <summary>
		/// Gets or sets the preferred width of the line in all texts.
		/// </summary>
		/// <value>The width of the line in pixels</value>
		public uint PreferredLineWidth { 
			get
			{
				return preferredLineWidth;
			}
			set
			{
				foreach (var item in texts) {
					item.PreferredLineWidth = value;
				}
				preferredLineWidth = value;
				RePositionTexts();
			}
		}

		private uint height;
		/// <summary>
		/// Gets the height of text list in pixels.
		/// </summary>
		/// <value>The height in pixels</value>
		public override uint Height
		{
			get
			{
				return height;
			}
		}


		/// <summary>
		/// Gets the width of text list in pixels.
		/// </summary>
		/// <value>The width in pixels</value>
		public override uint Width
		{
			get
			{
				uint maxWidth = 0;
				foreach (var item in texts)
				{
					uint width = item.Width;
					if (maxWidth < width) maxWidth = width;
				}
				return maxWidth;
			}
		}


		public TextList(SpriteFont font)
		{
			texts = new List<TextObject>();
			this.font = font;
			preferredLineWidth = 0;
			height = 0;
		}

		/// <summary>
		/// Creates and adds new TextObject to the list.
		/// </summary>
		/// <param name="text">Created TextObject.</param>
		public TextObject Add(String text) {
			TextObject textObject = new TextObject(font, text);
			textObject.PreferredLineWidth = PreferredLineWidth;

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

		private void RePositionTexts()
		{
			height = 0;
			for (int i = 0; i < texts.Count; i++)
			{
				TextObject item = texts[i];
				item.Position = new Vector2(0, 0);
				height += item.Height;
			}
		}
	}
}

