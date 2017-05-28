using System.IO;
using AIMLbot;

namespace BusinessLogic.BotConfiguration
{
    public class BotInitialization
    {
        const string UserId = "CityU.Scm.Ivan";
        private readonly Bot _aimlBot;
        private readonly User _myUser;

        private const string RemoteApiBotEndpoint =
            "http://taturevichtestbot.azurewebsites.net/api/file";
        private const string RemoteApiBotFilePath =
            "http://taturevichtestbot.azurewebsites.net/aiml/UserDirectory.aiml";

        public BotInitialization()
        {
            _aimlBot = new Bot();
            _myUser = new User(UserId, _aimlBot);
            Initialize();
        }

        private void Initialize()
        {
            _aimlBot.loadSettings();
            _aimlBot.isAcceptingUserInput = false;
            _aimlBot.loadAIMLFromFiles();
            _aimlBot.isAcceptingUserInput = true;
        }

        // Given an input string, finds a response using AIMLbot lib
        public string GetOutput(string input)
        {
            var request = new Request(input, _myUser, _aimlBot);
            Result res = _aimlBot.Chat(request);
            return res.Output;
        }

        public string GetUserDirectoryAimlFile => Path.Combine(_aimlBot.PathToAIML, "UserDirectory.aiml");

        public string GetUserDirectoryRemoteAimlApi => RemoteApiBotEndpoint;

        public string GetUserDirectoryRemoteAimlFile => RemoteApiBotFilePath;
    }
}
