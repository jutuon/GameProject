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

		/// <summary>
		/// Gets or sets the parent of the component.
		/// </summary>
		/// <value>The parent.</value>
		public Component Parent {get; protected set;}

		private Vector2 positionOffset;
		/// <summary>
		/// Gets or sets the position on the screen.
		/// </summary>
		/// <value>The position.</value>
		public Vector2 PositionOffset {
			get
			{
				return positionOffset;
			}
			set
			{
				positionOffset = value;
				CalculateDrawingPosition();
			}
		}

		/// <summary>
		/// The final drawing position.
		/// </summary>
		protected Vector2 drawingPosition;

		protected Vector2 drawingPositionFromParent;
		/// <summary>
		/// Gets or sets the drawing position set by component parent.
		/// </summary>
		/// <value>The drawing position from parent.</value>
		public Vector2 DrawingPositionFromParent
		{
			get
			{
				return drawingPositionFromParent;
			}
			set
			{
				drawingPositionFromParent = value;
				CalculateDrawingPosition();
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


		protected ComponentAlignmentX alignmentX;
		/// <summary>
		/// Gets or sets components x-axis alignment.
		/// </summary>
		/// <value>The x-axis alignment.</value>
		public virtual ComponentAlignmentX AlignmentX 
		{ 
			get
			{
				return alignmentX;
			}
			set
			{
				alignmentX = value;
				CalculateDrawingPosition();
			}
		}

		protected ComponentAlignmentY alignmentY;
		/// <summary>
		/// Gets or sets the y-axis alignment.
		/// </summary>
		/// <value>The y-axis alignment.</value>
		public virtual ComponentAlignmentY AlignmentY {
			get {
				return alignmentY;
			}
			set {
				alignmentY = value;
				CalculateDrawingPosition();
			}
		}

		public Component(Component parent)
		{
			Parent = parent;
			positionOffset = new Vector2(0,0);
			drawingPosition = new Vector2(0,0);
			alignmentX = ComponentAlignmentX.Left;
			alignmentY = ComponentAlignmentY.Top;
		}

		public virtual void Update()
		{
			
		}


		/// <summary>
		/// Calculates the drawing position for component.
		/// Component alignment and drawingPositionFromParent defines the
		/// final drawing position
		/// </summary>
		public virtual void CalculateDrawingPosition()
		{
			float x = 0;
			float y = 0;

			if (AlignmentX == ComponentAlignmentX.Center) x = -(Width / 2);
			else if (AlignmentX == ComponentAlignmentX.Right) x = -Width;

			if (AlignmentY == ComponentAlignmentY.Center) y = -(Height / 2);
			else if (AlignmentY == ComponentAlignmentY.Bottom) y = -Height;

			drawingPosition = new Vector2(x, y) + PositionOffset + drawingPositionFromParent;
		}

		/// <summary>
		/// Draw the Component with specified spriteBatch.
		/// </summary>
		/// <param name="spriteBatch">Sprite batch.</param>
		public abstract void Draw(SpriteBatch spriteBatch);

	}


	/// <summary>
	/// List of components
	/// </summary>
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
			
		public override ComponentAlignmentX AlignmentX {
			get
			{
				return alignmentX;
			}
			set
			{
				foreach (var item in list) item.AlignmentX = value;
				alignmentX = value;
				CalculateDrawingPosition();
			}
		}
			
		public override ComponentAlignmentY AlignmentY {
			get
			{
				return alignmentY;
			}
			set 
			{
				foreach (var item in list) item.AlignmentY = value;
				alignmentY = value;
				CalculateDrawingPosition();
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
		public T Add(T component) {
			list.Add(component);
			component.AlignmentX = AlignmentX;
			component.AlignmentY = AlignmentY;

			SetComponentDrawingPositionFromParent(component, drawingPositionFromParent + PositionOffset);

			return component;
		}

		/// <summary>
		/// Removes all current components in the list
		/// </summary>
		public virtual void Clear() {
			list.Clear();
		}

		/// <summary>
		/// Remove the specified component from list.
		/// </summary>
		/// <param name="c">Component</param>
		public void Remove(T c) {list.Remove(c);}

		/// <summary>
		/// Draw the Components in the list with specified spriteBatch.
		/// </summary>
		/// <param name="spriteBatch">Spritebatch.</param>
		public override void Draw(SpriteBatch spriteBatch)
		{
			foreach (var item in list) item.Draw(spriteBatch);
		}

		/// <summary>
		/// Calculates the drawing positions for all components in the list.
		/// </summary>
		public override void CalculateDrawingPosition()
		{
			height = 0;
			Vector2 location = PositionOffset + drawingPositionFromParent;
			for (int i = 0; i < list.Count; i++)
			{
				T item = list[i];
				SetComponentDrawingPositionFromParent(item, location);
			}
		}

		private void SetComponentDrawingPositionFromParent(T component, Vector2 location)
		{
			if (component.AlignmentY == ComponentAlignmentY.Top) component.DrawingPositionFromParent = new Vector2(0, height) + location;
			else component.DrawingPositionFromParent = new Vector2(0, -height) + location;
			height += component.Height;
		}
	}
}

