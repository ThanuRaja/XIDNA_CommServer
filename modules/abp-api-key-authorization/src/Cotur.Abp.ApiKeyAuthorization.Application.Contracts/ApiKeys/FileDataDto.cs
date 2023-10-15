using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cotur.Abp.ApiKeyAuthorization.ApiKeys
{
    public class FileDataDto
    {
        public byte[] FileContent { get; set; }
        public string FileContentType { get; set;}
        public string FileName { get; set;}

    }
}
