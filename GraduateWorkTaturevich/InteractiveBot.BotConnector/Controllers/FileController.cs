using System.IO;
using System.Web.Http;

namespace TestBotConnection.Controllers
{
    public class FileController : ApiController
    {
        [HttpGet]
        [Route("api/file")]
        public byte[] GetDirectoryFile()
        {
            var pathToUserFile = @"~\aiml\UserDirectory.xml";
            pathToUserFile = System.Web.Hosting.HostingEnvironment.MapPath(pathToUserFile);
            return File.ReadAllBytes(pathToUserFile);
        }

        [HttpPut]
        [Route("api/file")]
        public async void UpdateDirectoryFile()
        {
            var bytes = await Request.Content.ReadAsByteArrayAsync();
            var pathToUserFile = @"~\aiml\UserDirectory.xml";
            pathToUserFile = System.Web.Hosting.HostingEnvironment.MapPath(pathToUserFile);
            if (pathToUserFile != null)
            {
                File.WriteAllBytes(pathToUserFile, bytes);
            }
        }
    }
}
