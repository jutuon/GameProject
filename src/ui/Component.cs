using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameProject
{

	public enum ComponentAlignmentX
	{
		Left, Center, Right
	}

	public enum ComponentAlignmentY
	{
		Top, Center, Bottom
	}


	/// <summary>
	/// Base class for user interface components
	/// </summary>
	public abstract class Component
	{

		public Component Parent {get; protected set;}

		/// <summary>
		/// Gets or sets the position on the screen.
		/// </summary>
		/// <value>The position.</value>
		public Vector2 Position {get; set;}

		/// <summary>
		/// Gets the height of component in pixels.
		/// </summary>
		/// <value>The height in pixels.</value>
		public abstract uint Height { get;}

		/// <summary>
		/// Gets the width of component in pixels.
		/// </summary>
		/// <value>The width of component in pixels.</value>
		public abstract uint Width { get;}

		public ComponentAlignmentX AlignmentX { get; set;}
		public ComponentAlignmentY AlignmentY { get; set;}

		public Component(Component parent)
		{
			Parent = parent;
			Position = new Vector2(0,0);
		}

		public virtual void Update()
		{
			
		}

		/// <summary>
		/// Draw the Component with specified spriteBatch and parentLocation.
		/// </summary>
		/// <param name="spriteBatch">Sprite batch.</param>
		/// <param name="parentLocation">Parent location.</param>
		public abstract void Draw(SpriteBatch spriteBatch, Vector2 parentLocation);
	}


	public abstract class ComponentList<T> : Component where T : Component
	{
		protected List<T> list;

		protected uint height;
		/// <summary>
		/// Gets the height of component list in pixels.
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
		/// Gets the width of component list in pixels.
		/// </summary>
		/// <value>The width in pixels</value>
		public override uint Width
		{
			get
			{
				uint maxWidth = 0;
				foreach (var item in list)
				{
					uint width = item.Width;
					if (maxWidth < width) maxWidth = width;
				}
				return maxWidth;
			}
		}

		public ComponentList(Component c) : base(c)
		{
			this.list = new List<T>();
			height = 0;
		}


		/// <summary>
		/// Add the specified component. Height of the added component
		/// is added to the list's height.
		/// </summary>
		/// <param name="component">Component.</param>
		protected T Add(T component) {
			list.Add(component);
			component.Position += new Vector2(0, Height);
			height += component.Height;

			return component;
		}

		/// <summary>
		/// Removes all current components in the list
		/// </summary>
		public virtual void Clear() {
			list.Clear();
		}

		public override void Draw(SpriteBatch spriteBatch, Vector2 parentLocation)
		{
			Vector2 location = Position + parentLocation;

			foreach (var item in list)
			{
				item.Draw(spriteBatch, location);
			}
		}


		protected void RePositionComponents()
		{
			height = 0;
			for (int i = 0; i < list.Count; i++)
			{
				T item = list[i];
				item.Position = new Vector2(0, height);
				height += item.Height;
			}
		}


		public override void Update()
		{
			RePositionComponents();
			base.Update();
		}
	}
}

