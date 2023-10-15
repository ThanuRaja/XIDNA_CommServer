using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scriban;
using SendGrid;
using SendGrid.Helpers.Mail.Model;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XICommServer.Models.Mail;
using XICommServer.SendGridKeys;
using Volo.Abp.Domain.Repositories;
using XICommServer.MailEvents;

namespace XICommServer.Controllers
{
   // [Authorize]
    [Area("Mail")]
    [ControllerName("Mail")]
    [Route("api/mail")]
    [ShowInSwagger]
    public class MailController : XICommServerController
    {
        private  SendGridClient _sendGridClient { get; set; }

        private readonly ISendGridKeyRepository _sendGridKeyRepository;
        private readonly IMailEventRepository _mailEventRepository;
        public MailController(ISendGridKeyRepository sendGridKeyRepository, IMailEventRepository mailEventRepository)
        {
            _sendGridKeyRepository= sendGridKeyRepository;
            _mailEventRepository= mailEventRepository;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendAsync(SendEmailRequest request)
        {
            var response = new BaseResponse();
            var apiKey =await _sendGridKeyRepository.FirstOrDefaultAsync(x => x.IsDefault == true);
            if(apiKey==null)
            {
                response.Status = false;
                response.StatusMessage = "API Key not setted for this client/application, Please contact Administrator";
                return StatusCode(200, response);
            }
            _sendGridClient = new SendGridClient(apiKey.APIKey);
            try
            {
                var msg = new SendGridMessage
                {
                    From = new EmailAddress(apiKey.EmailAddress,apiKey.DisplayName),
                    Subject = request.Subject,
                    PlainTextContent = request.PlainTextContent,
                    HtmlContent = request.HtmlContent
                };
                foreach (var toEmail in request.ToEmails)
                {
                    msg.AddTo(new EmailAddress(toEmail));
                }
                if (request.CcEmails != null && request.CcEmails.Count > 0)
                {
                    foreach (var ccEmail in request.CcEmails)
                    {
                        msg.AddCc(new EmailAddress(ccEmail));
                    }
                }
                if (request.BccEmails != null)
                {
                    foreach (var bccEmail in request.BccEmails)
                    {
                        msg.AddBcc(new EmailAddress(bccEmail));
                    }
                }

                if (request.Attachments != null)
                {
                    foreach (var attachment in request.Attachments)
                    {
                        var bytes = Convert.FromBase64String(attachment.Content);
                        msg.AddAttachment(attachment.FileName, Convert.ToBase64String(bytes));
                    }
                }
                if (!string.IsNullOrEmpty(request.TemplateId))
                {
                    msg.SetTemplateId(request.TemplateId);
                }
                var resp = await _sendGridClient.SendEmailAsync(msg); 
                if (resp.StatusCode != System.Net.HttpStatusCode.OK && resp.StatusCode != System.Net.HttpStatusCode.Accepted)
                {
                    response.StatusMessage=$"Failed to send email. Status code: {resp.StatusCode}";
                }
                else
                {
                    var responseContent =  resp.DeserializeResponseHeaders();   //X-Message-Id

                    if (resp.Headers.TryGetValues("X-Message-Id", out var messageIdValues))
                    {
                        var messageId = messageIdValues.FirstOrDefault();
                        await _mailEventRepository.InsertAsync(new MailEvent()
                        {
                            CreatedAt = DateTime.UtcNow,
                            SGMessageId = messageId,
                            IsSuccess = true,
                        },true);
                        response.Data = new
                        {
                            X_Message_Id = messageId
                        };
                        response.Status = true;
                        response.StatusMessage = "Mail Send Successfully";
                    }
                    else
                    {
                        await _mailEventRepository.InsertAsync(new MailEvent()
                        {
                            CreatedAt = DateTime.UtcNow,
                            SGMessageId = null,
                            IsSuccess = false,
                        });
                        response.StatusMessage = "X-Message-Id header not found in the response.";
                    }
                }
                return StatusCode(200, response);
            }
            catch(Exception ex)
            {
                return  StatusCode(500, ex.Message);
            }
        }
    }
}
