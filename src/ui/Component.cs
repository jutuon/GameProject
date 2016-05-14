using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameProject
{
	/// <summary>
	/// Base class for user interface components
	/// </summary>
	public abstract class Component
	{

		/// <summary>
		/// Gets or sets the position on the screen.
		/// </summary>
		/// <value>The position.</value>
		public Vector2 Position {get; set;}

		/// <summary>
		/// Gets the height of component in pixels.
		/// </summary>
		/// <value>The height in pixels.</value>
		public abstract int Height { get;}

		/// <summary>
		/// Gets the width of component in pixels.
		/// </summary>
		/// <value>The width of component in pixels.</value>
		public abstract int Width { get;}

		public Component()
		{
			Position = new Vector2(0,0);
		}

		/// <summary>
		/// Draw the Component with specified spriteBatch and parentLocation.
		/// </summary>
		/// <param name="spriteBatch">Sprite batch.</param>
		/// <param name="parentLocation">Parent location.</param>
		public abstract void Draw(SpriteBatch spriteBatch, Vector2 parentLocation);
	}
}

