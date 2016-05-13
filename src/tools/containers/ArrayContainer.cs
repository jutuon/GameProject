using System;

namespace GameProject
{
	public abstract class ArrayContainer<ContentType>
	{
		private ContentType[] array;

		public ArrayContainer(int size)
		{
			array = new ContentType[size];
		}

		protected ContentType this[int i]
		{
			get 
			{
				return array[i];
			}
			set 
			{
				array[i] = value;
			}
		}

	}
}

