using Yandex.Speller.Api.DataContract;
using Yandex.Speller.Api.Native;

namespace Yandex.Speller.Api
{
	/// <summary>
	/// Implementing an interface <see cref="IYandexSpeller" />.
	/// </summary>
	public class YandexSpeller : IYandexSpeller
	{
		private readonly IYandexNativeSpeller _nativeSpeller;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="nativeSpeller">Custom implementation.</param>
		protected YandexSpeller(IYandexNativeSpeller nativeSpeller)
		{
			_nativeSpeller = nativeSpeller;
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		public YandexSpeller() : this(YandexNativeSpeller.CreateYandexNativeSpeller()) {}

		/// <summary>
		/// Checks spelling in this passage of text.
		/// </summary>
		public SpellResult CheckText(string text, Lang lang, Options options, TextFormat format)
		{
			return _nativeSpeller.CheckText(text, lang.ParamToString(), options.ParamToString(), format.ParamToString());
		}

		/// <summary>
		/// Checks spelling in specified text fragments.
		/// </summary>
		/// <returns>For each fragment returns a separate array error prompts.</returns>
		public SpellResult[] CheckTexts(string[] text, Lang lang, Options options, TextFormat format)
		{
			return _nativeSpeller.CheckTexts(text, lang.ParamToString(), options.ParamToString(), format.ParamToString());
		}
	}
}