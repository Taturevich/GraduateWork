using AIMLbot;
using System;
using System.IO;

namespace BusinessLogic.BotConfiguration
{
    public class BotInitialization
    {
        const string UserId = "CityU.Scm.Ivan";
        private readonly Bot _aimlBot;
        private readonly User _myUser;

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
            Request request = new Request(input, _myUser, _aimlBot);
            Result res = _aimlBot.Chat(request);
            return res.Output;
        }
    }
}
