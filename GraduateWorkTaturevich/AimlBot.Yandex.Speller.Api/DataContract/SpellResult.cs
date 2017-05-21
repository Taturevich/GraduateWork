using System.Collections.Generic;

namespace Yandex.Speller.Api.DataContract
{
	/// <summary>
	/// <see href="https://tech.yandex.ru/speller/doc/dg/reference/checkText-docpage/" />.
	/// </summary>
	public class SpellResult
	{
		/// <summary>
		/// Found errors.
		/// </summary>
		public List<Error> Errors
		{
			get;
			set;
		}
	}
}