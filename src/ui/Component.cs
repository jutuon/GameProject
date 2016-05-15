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

		private Vector2 position;
		/// <summary>
		/// Gets or sets the position on the screen.
		/// </summary>
		/// <value>The position.</value>
		public Vector2 Position {
			get
			{
				return position;
			}
			set
			{
				position = value;
				UpdateDrawingPosition();
			}
		}

		protected Vector2 drawingPosition;
		public Vector2 DrawingPosition {
			get {
				return drawingPosition;
			}
			set {
				drawingPosition = value;
				UpdateDrawingPosition();
			}
		}
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

		private ComponentAlignmentX alignmentX;
		public virtual ComponentAlignmentX AlignmentX 
		{ 
			get
			{
				return alignmentX;
			}
			set
			{
				alignmentX = value;
				UpdateDrawingPosition();
			}
		}

		private ComponentAlignmentY alignmentY;
		public virtual ComponentAlignmentY AlignmentY {
			get {
				return alignmentY;
			}
			set {
				alignmentY = value;
				UpdateDrawingPosition();
			}
		}

		public Component(Component parent)
		{
			Parent = parent;
			Position = new Vector2(0,0);
			drawingPosition = new Vector2(0,0);
			alignmentX = ComponentAlignmentX.Left;
			alignmentY = ComponentAlignmentY.Top;
		}

		public virtual void Update()
		{
			
		}

		protected virtual void UpdateDrawingPosition()
		{
			float x = Position.X + drawingPosition.X;
			float y = Position.Y + drawingPosition.Y;

			if (AlignmentX == ComponentAlignmentX.Center) x = -((Width+x)/2);
			else if (AlignmentX == ComponentAlignmentX.Right) x = -(Width+x);

			if (AlignmentY == ComponentAlignmentY.Center) y = -((Height+y) / 2);
			else if (AlignmentY == ComponentAlignmentY.Bottom) y = -(Height+y);

			drawingPosition = new Vector2(x, y);
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

		private ComponentAlignmentX alignmentX;
		public override ComponentAlignmentX AlignmentX {
			get {
				return alignmentX;
			}
			set {
				alignmentX = value;
				foreach (var item in list) item.AlignmentX = value;
			}
		}

		private ComponentAlignmentY alignmentY;
		public override ComponentAlignmentY AlignmentY {
			get {
				return alignmentY;
			}
			set {
				alignmentY = value;
				foreach (var item in list) item.AlignmentY = value;
			}
		}
			

		public ComponentList(Component c) : base(c)
		{
			alignmentX = ComponentAlignmentX.Left;
			alignmentY = ComponentAlignmentY.Top;
			this.list = new List<T>();
			height = 0;
		}


		/// <summary>
		/// Add the specified component. Height of the added component
		/// is added to the list's height.
		/// </summary>
		/// <param name="component">Component.</param>
		public T Add(T component) {
			list.Add(component);
			component.DrawingPosition += new Vector2(0, Height);
			height += component.Height;

			component.AlignmentX = alignmentX;
			component.AlignmentY = alignmentY;

			return component;
		}

		/// <summary>
		/// Removes all current components in the list
		/// </summary>
		public virtual void Clear() {
			list.Clear();
		}

		public void Remove(T c) {list.Remove(c);}

		public override void Draw(SpriteBatch spriteBatch, Vector2 parentLocation)
		{
			Vector2 location = DrawingPosition + parentLocation;

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
				item.DrawingPosition = new Vector2(0, height);
				height += item.Height;
			}
		}
			

		protected override void UpdateDrawingPosition() {}


		public override void Update()
		{
			RePositionComponents();
			base.Update();
		}
	}
}

