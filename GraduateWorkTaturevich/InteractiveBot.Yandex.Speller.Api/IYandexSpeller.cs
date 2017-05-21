using Yandex.Speller.Api.DataContract;

namespace Yandex.Speller.Api
{
	/// <summary>
	/// Spell checking interface through Yandex (User friendly).
	/// </summary>
	public interface IYandexSpeller
	{
		/// <summary>
		/// Checks spelling in this passage of text.
		/// </summary>
		SpellResult CheckText(string text, Lang lang, Options options, TextFormat format);

		/// <summary>
		/// Checks spelling in specified text fragments.
		/// </summary>
		/// <returns>For each fragment returns a separate array error prompts.</returns>
		SpellResult[] CheckTexts(string[] text, Lang lang, Options options, TextFormat format);
	}
}