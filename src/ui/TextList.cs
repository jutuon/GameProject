using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameProject
{
	/// <summary>
	/// List of TextObjects
	/// </summary>
	public class TextList : ComponentList<TextObject>
	{
		

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
				foreach (var item in list) {
					item.PreferredLineWidth = value;
				}
				preferredLineWidth = value;
				RePositionComponents();
			}
		}


		public TextList(Component c, SpriteFont font) : base(c)
		{
			this.font = font;
			preferredLineWidth = 0;

		}

		/// <summary>
		/// Creates and adds new TextObject to the list.
		/// </summary>
		/// <param name="text">Created TextObject.</param>
		public TextObject Add(String text) {
			TextObject textObject = new TextObject(this, font, text);
			textObject.PreferredLineWidth = PreferredLineWidth;

			Add(textObject);

			return textObject;
		}
			
	}
}

