using System;

namespace GameProject
{
	public class EventVariable<T>
	{
		private T variable;
		public T Value
		{
			get
			{
				return variable;
			} 
			set
			{
				variable = value;
				OnValueChanged(value);
			}
		}

		public delegate void ValueChanged(T value);
		public event ValueChanged OnValueChanged;

		public EventVariable(T initialValue)
		{
			variable = initialValue;
		}

		public EventVariable()
		{
			
		}
	}
}

