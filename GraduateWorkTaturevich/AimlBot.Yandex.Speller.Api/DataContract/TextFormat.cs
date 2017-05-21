namespace Yandex.Speller.Api.DataContract
{
	/// <summary>
	/// Checked text format.
	/// </summary>
	public enum TextFormat:uint
	{
		/// <summary>
		/// Text without markup.
		/// </summary>
		Plain=0,

		/// <summary>
		/// HTML-Text.
		/// </summary>
		Html=1
	}
}