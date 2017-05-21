using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BusinessLogic.BotConfiguration.CommandAnalyzer
{
    public class CommandParser
    {
        public void Parse()
        {
            var document = XDocument.Load("aiml/Default.aiml");
            var query = (from lv in document.Descendants("category")
                select new
                {
                    Category = lv.Name,
                    Pattern  = lv.Descendants("pattern").Select(x => x.Value).ToList(),
                    Template = lv.Descendants("template").Select(x => x.Value).ToList()
                }).ToList();        
        }
    }
}
