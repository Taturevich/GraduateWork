using System.IO;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Xml;

namespace TestBotConnection.Controllers
{
    public class FileController : ApiController
    {
        [HttpGet]
        [Route("api/file")]
        public HttpResponseMessage GetDirectoryFile()
        {
            var pathToUserFile = @"~\aiml\UserDirectory.aiml";
            pathToUserFile = System.Web.Hosting.HostingEnvironment.MapPath(pathToUserFile);
            var xmlDoc = new XmlDocument();
            if (pathToUserFile != null)
            {
                xmlDoc.Load(pathToUserFile);
                return new HttpResponseMessage
                {
                    Content = new StringContent(xmlDoc.OuterXml, Encoding.UTF8, "application/xml")
                };
            }

            return new HttpResponseMessage();
        }

        [HttpPut]
        [Route("api/file")]
        public async void UpdateDirectoryFile()
        {
            var bytes = await Request.Content.ReadAsByteArrayAsync();
            var pathToUserFile = @"~\aiml\UserDirectory.aiml";
            pathToUserFile = System.Web.Hosting.HostingEnvironment.MapPath(pathToUserFile);
            if (pathToUserFile != null)
            {
                File.WriteAllBytes(pathToUserFile, bytes);
            }
        }
    }
}
