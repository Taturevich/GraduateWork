using System;
using System.Linq;
using Yandex.Speller.Api.DataContract;

namespace Yandex.Speller.Api
{
	internal static class YandexSpellerHelper
	{
		public static string ParamToString(this Lang lang)
		{
			return string.Join(",",
				Enum.GetValues(typeof (Lang)).Cast<Lang>().Where(x => lang.HasFlag(x)).Select(v =>
				{
					switch (v)
					{
						case Lang.Ru:
							return "ru";
						case Lang.Uk:
							return "uk";
						case Lang.En:
							return "en";
						default:
							throw new NotSupportedException();
					}
				}));
		}

		public static int ParamToString(this Options options)
		{
			return ((int) options);
		}

		public static string ParamToString(this TextFormat textFormat)
		{
			switch (textFormat)
			{
				case TextFormat.Plain:
					return "plain";
				case TextFormat.Html:
					return "html";
				default:
					throw new NotSupportedException();
			}
		}
	}
}