using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameProject
{
	public class TextObject : Component
	{
		public String Text {get; set;}

		private SpriteFont Font { get; set;}

		public TextObject(SpriteFont font, String text = "") : base()
		{
			Text = text;
			Font = font;
		}

		public override void Draw(SpriteBatch spriteBatch, Vector2 parentLocation)
		{
			Vector2 location = Position + parentLocation;
			spriteBatch.DrawString(Font, Text, location, Color.White);
		}
	}
}

