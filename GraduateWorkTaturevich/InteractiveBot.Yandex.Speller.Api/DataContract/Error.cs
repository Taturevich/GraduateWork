using System.Collections.Generic;
using RestSharp.Deserializers;

namespace Yandex.Speller.Api.DataContract
{
	/// <summary>
	/// The data model for represent error.
	/// </summary>
	public class Error
	{
		/// <summary>
		/// Error code.
		/// <see href="https://tech.yandex.ru/speller/doc/dg/reference/error-codes-docpage/" />.
		/// </summary>
		public ErrorCode Code
		{
			get;
			set;
		}

		/// <summary>
		/// Column number (starting from 0).
		/// </summary>
		public int Col
		{
			get;
			set;
		}

		/// <summary>
		/// Length of the misspelled word.
		/// </summary>
		public int Len
		{
			get;
			set;
		}

		/// <summary>
		/// Position of the misspelled word (counting from 0).
		/// </summary>
		public int Pos
		{
			get;
			set;
		}

		/// <summary>
		/// Line number (counting from 0).
		/// </summary>
		public int Row
		{
			get;
			set;
		}

		/// <summary>
		/// Tip (may be several or may not be available).
		/// </summary>
		[DeserializeAs(Name = "s")]
		public List<string> Steer
		{
			get;
			set;
		}

		/// <summary>
		/// The original word.
		/// </summary>
		public string Word
		{
			get;
			set;
		}
	}
}