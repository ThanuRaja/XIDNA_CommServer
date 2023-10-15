using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XICommServer.Models.Mail
{
    public class SendEmailRequest
    {
        public List<string> ToEmails { get; set; }
        public string Subject { get; set; }
        public string PlainTextContent { get; set; }
        public string HtmlContent { get; set; }
        public List<string> CcEmails { get; set; }
        public List<string> BccEmails { get; set; }

        public List<Attachment> Attachments { get; set; }
        public string TemplateId { get; set; }
    }
    public class Attachment
    {
        public string FileName { get; set; }   //file name
        public string Content { get; set; }    // base64 string content
    }
}
