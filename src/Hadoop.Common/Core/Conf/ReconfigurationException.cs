using System;


namespace Org.Apache.Hadoop.Conf
{
	/// <summary>
	/// Exception indicating that configuration property cannot be changed
	/// at run time.
	/// </summary>
	[System.Serializable]
	public class ReconfigurationException : Exception
	{
		private const long serialVersionUID = 1L;

		private string property;

		private string newVal;

		private string oldVal;

		/// <summary>Construct the exception message.</summary>
		private static string ConstructMessage(string property, string newVal, string oldVal
			)
		{
			string message = "Could not change property " + property;
			if (oldVal != null)
			{
				message += " from \'" + oldVal;
			}
			if (newVal != null)
			{
				message += "\' to \'" + newVal + "\'";
			}
			return message;
		}

		/// <summary>
		/// Create a new instance of
		/// <see cref="ReconfigurationException"/>
		/// .
		/// </summary>
		public ReconfigurationException()
			: base("Could not change configuration.")
		{
			this.property = null;
			this.newVal = null;
			this.oldVal = null;
		}

		/// <summary>
		/// Create a new instance of
		/// <see cref="ReconfigurationException"/>
		/// .
		/// </summary>
		public ReconfigurationException(string property, string newVal, string oldVal, Exception
			 cause)
			: base(ConstructMessage(property, newVal, oldVal), cause)
		{
			this.property = property;
			this.newVal = newVal;
			this.oldVal = oldVal;
		}

		/// <summary>
		/// Create a new instance of
		/// <see cref="ReconfigurationException"/>
		/// .
		/// </summary>
		public ReconfigurationException(string property, string newVal, string oldVal)
			: base(ConstructMessage(property, newVal, oldVal))
		{
			this.property = property;
			this.newVal = newVal;
			this.oldVal = oldVal;
		}

		/// <summary>Get property that cannot be changed.</summary>
		public virtual string GetProperty()
		{
			return property;
		}

		/// <summary>Get value to which property was supposed to be changed.</summary>
		public virtual string GetNewValue()
		{
			return newVal;
		}

		/// <summary>Get old value of property that cannot be changed.</summary>
		public virtual string GetOldValue()
		{
			return oldVal;
		}
	}
}
