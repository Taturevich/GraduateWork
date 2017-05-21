using System;
using System.Collections.Generic;
using RestSharp;
using Yandex.Speller.Api.DataContract;

namespace Yandex.Speller.Api.Native
{
	/// <summary>
	/// Implementing an interface <see cref="IYandexNativeSpeller" />.
	/// </summary>
	public class YandexNativeSpeller : IYandexNativeSpeller
	{
		private readonly IRestClient _restClient;

		/// <summary />
		protected YandexNativeSpeller(IRestClient restClient)
		{
			_restClient = restClient;
		}

		/// <summary>
		/// Checks spelling in this passage of text.
		/// <see href="https://tech.yandex.ru/speller/doc/dg/reference/checkText-docpage/" />
		/// </summary>
		public SpellResult CheckText(string text, string lang, int options, string format)
		{
			if (text == null) throw new ArgumentNullException("text");
			if (lang == null) throw new ArgumentNullException("lang");
			if (format == null) throw new ArgumentNullException("format");

			IRestRequest request = new RestRequest("checkText", Method.POST);

			request.AddParameter("text", text);
			if (!string.IsNullOrEmpty(lang))
				request.AddParameter("lang", lang);
			if (!string.IsNullOrEmpty(format))
				request.AddParameter("format", format);
			request.AddParameter("options", options);
			IRestResponse<List<Error>> response = _restClient.Execute<List<Error>>(request);
			if (!ReferenceEquals(response.ErrorException, null)) throw response.ErrorException;
			return new SpellResult {Errors = response.Data};
		}

		/// <summary>
		/// Checks spelling in specified text fragments. For each fragment returns a separate array error prompts.
		/// <see href="https://tech.yandex.ru/speller/doc/dg/reference/checkTexts-docpage/" />.
		/// </summary>
		public SpellResult[] CheckTexts(string[] text, string lang, int options, string format)
		{
			if (text == null) throw new ArgumentNullException("text");
			if (lang == null) throw new ArgumentNullException("lang");
			if (format == null) throw new ArgumentNullException("format");

			IRestRequest request = new RestRequest("checkTexts", Method.POST);

			foreach (string s in text)
			{
				request.AddParameter("text", s);
			}

			if (!string.IsNullOrEmpty(lang))
				request.AddParameter("lang", lang);
			if (!string.IsNullOrEmpty(format))
				request.AddParameter("format", format);
			request.AddParameter("options", options);

			IRestResponse<List<List<Error>>> response = _restClient.Execute<List<List<Error>>>(request);
			if (!ReferenceEquals(response.ErrorException, null)) throw response.ErrorException;

			var results = new SpellResult[text.Length];
			for (int i = 0; i < results.Length; i++)
			{
				results[i] = new SpellResult {Errors = response.Data[i]};
				results[i].Errors.ForEach(x => x.Row = i);
			}
			return results;
		}

		/// <summary>
		/// Creating Yandex.Speller client.
		/// </summary>
		public static IYandexNativeSpeller CreateYandexNativeSpeller()
		{
			return new YandexNativeSpeller(new RestClient("http://speller.yandex.net/services/spellservice.json/"));
		}

		/// <summary>
		/// Creating Yandex.Speller client.
		/// </summary>
		public static IYandexNativeSpeller CreateYandexNativeSpeller(string url)
		{
			return new YandexNativeSpeller(new RestClient(url));
		}
	}
}