namespace AIMLbot.Utils
{
    /// <summary>
    /// Encapsulates all the required methods and attributes for any text transformation.
    /// 
    /// An input string is provided and various methods and attributes can be used to grab
    /// a transformed string.
    /// 
    /// The protected ProcessChange() method is abstract and should be overridden to contain 
    /// the code for transforming the input text into the output text.
    /// </summary>
    abstract public class TextTransformer
    {
        #region Attributes
        /// <summary>
        /// Instance of the input string
        /// </summary>
        protected string inputString;

        /// <summary>
        /// The bot that this transformation is connected with
        /// </summary>
        public Bot bot;

        /// <summary>
        /// The input string to be transformed in some way
        /// </summary>
        public string InputString
        {
            get{return inputString; }
            set{ inputString = value;}
        }

        /// <summary>
        /// The transformed string
        /// </summary>
        public string OutputString
        {
            get{return Transform();}
        }
        #endregion

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="bot">The bot this transformer is a part of</param>
        /// <param name="inputString">The input string to be transformed</param>
        public TextTransformer(Bot bot, string inputString)
        {
            this.bot = bot;
            this.inputString = inputString;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="bot">The bot this transformer is a part of</param>
        public TextTransformer(Bot bot)
        {
            this.bot = bot;
            inputString = string.Empty;
        }

        /// <summary>
        /// Default ctor for used as part of late binding mechanism
        /// </summary>
        public TextTransformer()
        {
            bot = null;
            inputString = string.Empty;
        }

        /// <summary>
        /// Do a transformation on the supplied input string
        /// </summary>
        /// <param name="input">The string to be transformed</param>
        /// <returns>The resulting output</returns>
        public string Transform(string input)
        {
            inputString = input;
            return Transform();
        }

        /// <summary>
        /// Do a transformation on the string found in the InputString attribute
        /// </summary>
        /// <returns>The resulting transformed string</returns>
        public string Transform()
        {
            if (inputString.Length > 0)
            {
                return ProcessChange();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// The method that does the actual processing of the text.
        /// </summary>
        /// <returns>The resulting processed text</returns>
        protected abstract string ProcessChange();
    }
}
