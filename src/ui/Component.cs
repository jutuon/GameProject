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

	public interface IComponent
	{
		/// <summary>
		/// Gets the parent of the component.
		/// </summary>
		/// <value>The parent.</value>
		IComponent Parent { get;}

		/// <summary>
		/// Gets or sets the position on the screen.
		/// </summary>
		/// <value>The position.</value>
		Vector2 PositionOffset { get; set;}

		/// <summary>
		/// The final drawing position.
		/// </summary>
		Vector2 DrawingPosition {get;}

		/// <summary>
		/// Gets or sets the drawing position set by component parent.
		/// </summary>
		/// <value>The drawing position from parent.</value>
		Vector2 DrawingPositionFromParent { get; set;}

		/// <summary>
		/// Gets or sets components x-axis alignment.
		/// </summary>
		/// <value>The x-axis alignment.</value>
		ComponentAlignmentX AlignmentX { get; set;}

		/// <summary>
		/// Gets or sets the y-axis alignment.
		/// </summary>
		/// <value>The y-axis alignment.</value>
		ComponentAlignmentY AlignmentY { get; set; }

		/// <summary>
		/// Gets the height of component in pixels.
		/// </summary>
		/// <value>The height in pixels.</value>
		uint Width {get;}

		/// <summary>
		/// Gets the width of component in pixels.
		/// </summary>
		/// <value>The width of component in pixels.</value>
		uint Height {get;}

		/// <summary>
		/// Calculates the drawing position for component.
		/// Component alignment and drawingPositionFromParent defines the
		/// final drawing position
		/// </summary>
		void CalculateDrawingPosition();

		/// <summary>
		/// Draw the Component with specified spriteBatch.
		/// </summary>
		/// <param name="spriteBatch">Sprite batch.</param>
		void Draw(SpriteBatch spriteBatch);
		void Update();
	}


	/// <summary>
	/// Base class for user interface components
	/// </summary>
	public abstract class Component : IComponent
	{
		public IComponent Parent {get; private set;}

		private Vector2 positionOffset;
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


		public Vector2 DrawingPosition { get; protected set;}

		private Vector2 drawingPositionFromParent;
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


		public abstract uint Height { get;}
		public abstract uint Width { get;}


		private ComponentAlignmentX alignmentX;
		public ComponentAlignmentX AlignmentX 
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

		private ComponentAlignmentY alignmentY;
		public ComponentAlignmentY AlignmentY {
			get {
				return alignmentY;
			}
			set {
				alignmentY = value;
				CalculateDrawingPosition();
			}
		}

		public Component(IComponent parent)
		{
			Parent = parent;
			alignmentX = ComponentAlignmentX.Left;
			alignmentY = ComponentAlignmentY.Top;
			positionOffset = Vector2.Zero;
			DrawingPosition = Vector2.Zero;
			drawingPositionFromParent = Vector2.Zero;
		}
			
		public virtual void CalculateDrawingPosition()
		{
			float x = 0;
			float y = 0;

			if (AlignmentX == ComponentAlignmentX.Center) x = -(Width / 2);
			else if (AlignmentX == ComponentAlignmentX.Right) x = -Width;

			if (AlignmentY == ComponentAlignmentY.Center) y = -(Height / 2);
			else if (AlignmentY == ComponentAlignmentY.Bottom) y = -Height;

			DrawingPosition = new Vector2(x, y) + PositionOffset + DrawingPositionFromParent;
		}


		public abstract void Draw(SpriteBatch spriteBatch);

		public virtual void Update() {}
	}


	/// <summary>
	/// List of components
	/// </summary>
	public abstract class ComponentList<T> : IComponent where T : IComponent
	{
		protected List<T> list;

		public IComponent Parent { get; private set;}

		private Vector2 positionOffset;
		public Vector2 PositionOffset
		{
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
			
		public Vector2 DrawingPosition { get; private set;}

		protected Vector2 drawingPositionFromParent;
		public Vector2 DrawingPositionFromParent
		{
			get {
				return drawingPositionFromParent;
			}
			set {
				drawingPositionFromParent = value;
				CalculateDrawingPosition();
			}
		}
			
		/// <summary>
		/// Gets the height of component list in pixels.
		/// </summary>
		/// <value>The height in pixels</value>
		public uint Height { get; private set;}


		/// <summary>
		/// Gets the width of component list in pixels.
		/// </summary>
		/// <value>The width in pixels</value>
		public uint Width
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
		public ComponentAlignmentX AlignmentX {
			get
			{
				return alignmentX;
			}
			set
			{
				alignmentX = value;
				foreach (var item in list) item.AlignmentX = value;
			}
		}

		private ComponentAlignmentY alignmentY;
		public ComponentAlignmentY AlignmentY {
			get
			{
				return alignmentY;
			}
			set 
			{
				alignmentY = value;
				foreach (var item in list) item.AlignmentY = value;
			}
		}

		public ComponentList(IComponent c)
		{
			this.list = new List<T>();
			Parent = c;

			Height = 0;
			alignmentX = ComponentAlignmentX.Left;
			alignmentY = ComponentAlignmentY.Top;
			positionOffset = Vector2.Zero;
			DrawingPosition = Vector2.Zero;
			drawingPositionFromParent = Vector2.Zero;
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

			SetComponentDrawingPositionFromParent(component, Height + component.Height);

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
		public virtual void Draw(SpriteBatch spriteBatch)
		{
			foreach (var item in list) item.Draw(spriteBatch);
		}

		/// <summary>
		/// Calculates the drawing positions for all components in the list.
		/// </summary>
		public void CalculateDrawingPosition()
		{
			DrawingPosition = DrawingPositionFromParent + PositionOffset;

			uint oldHeight = Height;
			Height = 0;

			if (list.Count == 1) 
			{
				SetComponentDrawingPositionFromParent(list[0]);
				return;
			}
				
			for (int i = 0; i < list.Count; i++)
			{
				T item = list[i];
				SetComponentDrawingPositionFromParent(item, oldHeight);
			}
		}

		private void SetComponentDrawingPositionFromParent(T component, uint listHeight = 0)
		{
			if (component.AlignmentY == ComponentAlignmentY.Top) component.DrawingPositionFromParent = new Vector2(0, Height) + DrawingPosition;
			else if (component.AlignmentY == ComponentAlignmentY.Center) {
				float centerValue = 0;
				if (list.Count > 1) {
					float objectHeight = listHeight*1.0f/list.Count;
					centerValue = objectHeight / 2 * (list.Count - 1);
				}
				component.DrawingPositionFromParent = new Vector2(0, Height - centerValue) + DrawingPosition;
			} 
			else component.DrawingPositionFromParent = new Vector2(0, -Height) + DrawingPosition;

			Height += component.Height;
		}

		public virtual void Update() {}
	}
}

