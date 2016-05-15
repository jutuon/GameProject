using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Text;

namespace GameProject
{
	/// <summary>
	/// Add text to the screen
	/// </summary>
	public class TextObject : Component
	{

		private String text;

		/// <summary>
		/// Gets or sets the current text. After setting the text,
		/// the text wrapping is calculated.
		/// </summary>
		/// <value>The text.</value>
		public String Text
		{
			get
			{
				return text;
			}
			set
			{
				text = value;
				CalculateTextWrapping();
			}
		}

		private SpriteFont font;

		/// <summary>
		/// Gets or sets the font. After setting the font
		/// the text wrapping is calculated.
		/// </summary>
		/// <value>The font.</value>
		public SpriteFont Font
		{
			get
			{
				return font;
			}
			set
			{
				font = value;
				CalculateTextWrapping();
			}
		}

		/// <summary>
		/// Gets the height of the text in pixels.
		/// </summary>
		/// <value>The height of the text in pixels.</value>
		public override uint Height
		{
			get
			{
				return (uint)Font.MeasureString(wrappedText).Y;
			}
		}

		/// <summary>
		/// Gets the width of the text in pixels.
		/// </summary>
		/// <value>The width of the text in pixels.</value>
		public override uint Width
		{
			get
			{
				return (uint)Font.MeasureString(wrappedText).X;
			}
		}

		private uint preferredLineWidth;
		/// <summary>
		/// Gets or sets the line width for text wrapping as pixels.
		/// If user sets too small value, lines will be one letter width
		/// </summary>
		/// <value>The width of the line in pixels. Value 0 disables text wrapping </value>
		public uint PreferredLineWidth
		{
			get
			{
				return preferredLineWidth;
			} 
			set
			{
				preferredLineWidth = value;
				CalculateTextWrapping();
			}
		}


		private String wrappedText;

		public TextObject(Component parent, SpriteFont font, String text = "") : base(parent)
		{
			this.font = font;
			this.text = text;
			wrappedText = text;

			preferredLineWidth = 0;
			CalculateTextWrapping();
		}

		public override void Draw(SpriteBatch spriteBatch, Vector2 parentLocation)
		{
			Vector2 location = Position + parentLocation;
			spriteBatch.DrawString(Font, wrappedText, location, Color.White);
		}

		private void CalculateTextWrapping()
		{
			wrappedText = text;

			if (PreferredLineWidth == 0) return;

			//TODO: text wrapping by words
			float textWidth = Font.MeasureString(text).X;
			if (textWidth <= PreferredLineWidth) return;

			float positionOfNewLineInPixels = textWidth / PreferredLineWidth;
			int newLinePosition = (int) (text.Length / positionOfNewLineInPixels);

			StringBuilder sb = new StringBuilder(text);

			//if wrapping index is too small lets make lines one letter width
			if (newLinePosition < 1) newLinePosition = 1;

			int nextPosition = newLinePosition + 1;

			for (int i = newLinePosition; i < sb.Length; i+=nextPosition)
			{
				sb.Insert(i, '\n');
			}

			wrappedText = sb.ToString();

			Parent.Update();
		}
	}
}

