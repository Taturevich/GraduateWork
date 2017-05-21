using System;

namespace Yandex.Speller.Api.DataContract
{
	/// <summary>
	/// Settings Yandex.Speller
	/// <see href="https://tech.yandex.ru/speller/doc/dg/reference/speller-options-docpage/" />.
	/// </summary>
	[Flags]
	public enum Options
	{
		/// <summary>
		/// No specific options.
		/// </summary>
		Default = 0x0,

		/// <summary>
		/// Ignore words written in capital letters, such as "VPK".
		/// </summary>
		IgnoreUppercase = 0x1,

		/// <summary>
		/// Ignore words with numbers, such as "avp17h4534."
		/// </summary>
		IgnoreDigits = 0x2,

		/// <summary>
		/// Ignore Internet addresses, email addresses and filenames.
		/// </summary>
		IgnoreUrls = 0x4,

		/// <summary>
		/// Highlight repetitions of words, consecutive. For example, "I flew to Cyprus."
		/// </summary>
		FindRepeatWords = 0x8,

		/// <summary>
		/// Ignore words, written in Latin, for example, "madrid".
		/// </summary>
		IgnoreLatin = 0x10,

		/// <summary>
		/// Just check the text, without giving options for replacement.
		/// </summary>
		NoSuggest = 0x20,

		/// <summary>
		/// Flag words, written in Latin, as errors.
		/// </summary>
		FlagLatin = 0x80,

		/// <summary>
		/// Do not use a dictionary environment (context) during the scan. This is useful in cases where the service is
		/// transmitted to the input of a list of individual words.
		/// </summary>
		ByWords = 0x100,

		/// <summary>
		/// Ignore the incorrect use of uppercase / lowercase letters, for example, in the word "moscow".
		/// </summary>
		IgnoreCapitalization = 0x200,

		/// <summary>
		/// Ignore Roman numerals ("I, II, III, ...").
		/// </summary>
		IgnoreRomanNumerals = 0x800
	}
}