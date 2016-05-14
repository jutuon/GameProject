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
		public override int Height
		{
			get
			{
				Vector2 textSize = Font.MeasureString(text);
				return (int)textSize.Y * lines;
			}
		}

		/// <summary>
		/// Gets the width of the text in pixels.
		/// </summary>
		/// <value>The width of the text in pixels.</value>
		public override int Width
		{
			get
			{
				Vector2 textSize = Font.MeasureString(text);
				if (LineWidth <= 0) return (int) textSize.X;
				return LineWidth;
			}
		}

		/// <summary>
		/// Gets or sets the line width for text wrapping.
		/// </summary>
		/// <value>The width of the line in pixels. Value 0 or negative value
		/// disables text wrapping </value>
		public int LineWidth { get; set;}
		//TODO: too small line width makes endless loop



		private String wrappedText;
		private int lines = 1; //count of lines in wrapped text

		public TextObject(SpriteFont font, String text = "") : base()
		{
			this.text = text;
			wrappedText = text;
			this.font = font;
			LineWidth = 0;
			CalculateTextWrapping();
		}

		public override void Draw(SpriteBatch spriteBatch, Vector2 parentLocation)
		{
			Vector2 location = Position + parentLocation;
			spriteBatch.DrawString(Font, wrappedText, location, Color.White);
		}

		private void CalculateTextWrapping()
		{
			if (LineWidth <= 0)
			{
				wrappedText = text;
				return;
			}

			//TODO: text wrapping by words
			Vector2 textSize = Font.MeasureString(text);

			float textX = textSize.X;

			if (textX > LineWidth) {
				int position = (int) textX / LineWidth;

				int newLine = text.Length / position;

				StringBuilder sb = new StringBuilder(text);

				lines = 1;
				for (int i = newLine; i < sb.Length; i+=newLine)
				{
					sb.Insert(i, '\n');
					lines++;
				}

				wrappedText = sb.ToString();
				return;
			}


			wrappedText = text;

		}
	}
}

