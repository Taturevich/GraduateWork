using System;

namespace Yandex.Speller.Api.DataContract
{
	/// <summary>
	/// Supported languages.
	/// </summary>
	[Flags]
	public enum Lang
	{
		/// <summary>
		/// Russian
		/// </summary>
		Ru = 0x1,

		/// <summary>
		/// Ukrainian
		/// </summary>
		Uk = 0x2,

		/// <summary>
		/// English
		/// </summary>
		En = 0x4
	}
}