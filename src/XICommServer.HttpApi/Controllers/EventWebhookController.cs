using Cotur.Abp.ApiKeyAuthorization.ApiKeys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SendGrid.Helpers.EventWebhook;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using XICommServer.MailEventLogs;
using XICommServer.Models.Mail;
using XICommServer.WebHookConfigurations;

namespace XICommServer.Controllers
{
    // [Authorize]
    [Area("Event")]
    [ControllerName("Event")]
    [Route("api/event")]
    [ShowInSwagger]
    public class EventWebhookController : XICommServerController
    {

        private readonly IWebHookConfigurationRepository _configurationRepository;


        private readonly IMailEventLogRepository _mailEventLogRepository;

        public EventWebhookController(IWebHookConfigurationRepository configurationRepository, IMailEventLogRepository mailEventLogRepository)
        {
            _configurationRepository = configurationRepository;
            _mailEventLogRepository= mailEventLogRepository;
        }

        /// <summary>
        /// POST : Event webhook handler
        /// </summary>
        /// <returns></returns>
        [Route("/events")]
        [HttpPost]
        public async Task<IActionResult> Events()
        {
            var response = new BaseResponse();
            var config = await _configurationRepository.FirstOrDefaultAsync(x => x.IsDefault == true);
            if(config == null)
            {
                response.Status = false;
                response.StatusMessage = "Webhoos not configured in the comm server please contact administrator";
                return StatusCode(StatusCodes.Status406NotAcceptable, response);
            }
            //Webhook signature verification --- if you dont need this please comment
            if (!IsValidSignature(Request,config.ApiSignatureVerificationKey))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "signazture validation failed");
            }
            IEnumerable<Event> events = await EventParser.ParseAsync(Request.Body);
            foreach (var eventInfo in events)
            {
                var log = new MailEventLog();
                log.TLS = eventInfo.TLS;
                log.Category = JsonConvert.SerializeObject(eventInfo.Category);
                log.SmtpId= eventInfo.SmtpId;
                log.SendGridMessageId= eventInfo.SendGridMessageId;
                log.SendGridEventId= eventInfo.SendGridEventId;
                log.EventType = eventInfo.EventType;
                log.MarketingCampainId= eventInfo.MarketingCampainId;
                log.MarketingCampainName = eventInfo.MarketingCampainName;
                
                switch (eventInfo.EventType)
                {
                    case EventType.SpamReport:
                        if(config.ListenSpamReport)
                        {
                            await SendWebhookEventToClient(eventInfo, config.ClientWebhookUrl, config.ApiSignatureVerificationKey, log);
                        }
                        break;
                    case EventType.Bounce:
                        if (config.ListenBounce)
                        {
                            await SendWebhookEventToClient(eventInfo, config.ClientWebhookUrl, config.ApiSignatureVerificationKey, log);
                        }
                        break;
                    case EventType.Processed:
                        if (config.ListenSpamReport)
                        {
                            await SendWebhookEventToClient(eventInfo, config.ClientWebhookUrl, config.ApiSignatureVerificationKey,log);
                        }
                        break;
                    case EventType.Delivered:
                        if (config.ListenDelivered)
                        {
                            await SendWebhookEventToClient(eventInfo, config.ClientWebhookUrl, config.ApiSignatureVerificationKey, log);
                        }
                        break;
                    case EventType.Deferred:
                        if (config.ListenDeferred)
                        {
                            await SendWebhookEventToClient(eventInfo, config.ClientWebhookUrl, config.ApiSignatureVerificationKey, log);
                        }
                        break;
                    case EventType.Click:
                        if (config.ListenClick)
                        {
                            await SendWebhookEventToClient(eventInfo, config.ClientWebhookUrl, config.ApiSignatureVerificationKey, log);
                        }
                        break;
                    case EventType.Dropped:
                        if (config.ListenDropped)
                        {
                            await SendWebhookEventToClient(eventInfo, config.ClientWebhookUrl, config.ApiSignatureVerificationKey, log);
                        }
                        break;
                    case EventType.GroupResubscribe:
                        if (config.ListenGroupResubscribe)
                        {
                            await SendWebhookEventToClient(eventInfo, config.ClientWebhookUrl, config.ApiSignatureVerificationKey, log);
                        }
                        break;
                    case EventType.GroupUnsubscribe:
                        if (config.ListenGroupUnsubscribe)
                        {
                            await SendWebhookEventToClient(eventInfo, config.ClientWebhookUrl, config.ApiSignatureVerificationKey, log);
                        }
                        break;
                    case EventType.Open:
                        if (config.ListenOpen)
                        {
                            await SendWebhookEventToClient(eventInfo, config.ClientWebhookUrl, config.ApiSignatureVerificationKey, log);
                        }
                        break;
                    case EventType.Unsubscribe:
                        if (config.ListenUnsubscribe)
                        {
                            await SendWebhookEventToClient(eventInfo, config.ClientWebhookUrl, config.ApiSignatureVerificationKey, log);
                        }
                        break;
                    default:
                        break;
                }

               
            }
            return Ok();
        }
        private async Task SendWebhookEventToClient(Event _event,string apiEndpointUrl,string _apiKey, MailEventLog log)
        {
            string jsonData = JsonConvert.SerializeObject(_event);
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("XI-API-Secrect", _apiKey);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(apiEndpointUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    log.IsLogSynced=true;
                    await _mailEventLogRepository.InsertAsync(log,true);
                    Console.WriteLine("Request was successful. Response status code: " + response.StatusCode);
                }
                else
                {
                    log.IsLogSynced = false;
                    await _mailEventLogRepository.InsertAsync(log, true);
                    Console.WriteLine("Request failed. Response status code: " + response.StatusCode);
                }
            }
        }
        public static bool IsValidSignature(HttpRequest request,string apiKey)
        {
            var publicKey = apiKey;
            string requestBody;
            using (var reader = new StreamReader(request.Body))
            {
                requestBody = reader.ReadToEnd();
            }

            var validator = new RequestValidator();
            var ecPublicKey = validator.ConvertPublicKeyToECDSA(publicKey);

            return validator.VerifySignature(
                ecPublicKey,
                requestBody,
                request.Headers[RequestValidator.SIGNATURE_HEADER],
                request.Headers[RequestValidator.TIMESTAMP_HEADER]
            );
        }
    }
}
