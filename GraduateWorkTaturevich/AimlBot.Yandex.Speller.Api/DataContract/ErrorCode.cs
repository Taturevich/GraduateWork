namespace Yandex.Speller.Api.DataContract
{
	/// <summary>
	/// Error codes.
	/// <see href="https://tech.yandex.ru/speller/doc/dg/reference/error-codes-docpage/" />.
	/// </summary>
	public enum ErrorCode
	{
		/// <summary>
		/// There is no word in the dictionary.
		/// </summary>
		ErrorUnknownWord = 1,

		/// <summary>
		/// Repeating words.
		/// </summary>
		ErrorRepeatWord = 2,

		/// <summary>
		/// Incorrect use of uppercase and lowercase letters.
		/// </summary>
		ErrorCapitalization = 3,

		/// <summary>
		/// The text contains too many errors. In this case, the application can send text Yandex.Speller remain untested in
		/// the following query.
		/// </summary>
		ErrorTooManyErrors = 4
	}
}