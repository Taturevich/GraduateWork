using Yandex.Speller.Api.DataContract;

namespace Yandex.Speller.Api.Native
{
	/// <summary>
	/// Spell checking interface through Yandex (native API).
	/// </summary>
	public interface IYandexNativeSpeller
	{
		/// <summary>
		/// Checks spelling in this passage of text.
		/// <see href="https://tech.yandex.ru/speller/doc/dg/reference/checkText-docpage/" />
		/// </summary>
		SpellResult CheckText(string text, string lang, int options, string format);

		/// <summary>
		/// Checks spelling in specified text fragments. For each fragment returns a separate array error prompts.
		/// <see href="https://tech.yandex.ru/speller/doc/dg/reference/checkTexts-docpage/" />.
		/// </summary>
		SpellResult[] CheckTexts(string[] text, string lang, int options, string format);
	}
}