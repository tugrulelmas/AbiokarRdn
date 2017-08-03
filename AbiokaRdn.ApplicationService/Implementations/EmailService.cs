﻿using AbiokaRdn.ApplicationService.Abstractions;
using AbiokaRdn.ApplicationService.Messaging;
using AbiokaRdn.Domain.Repositories;
using AbiokaRdn.Infrastructure.Common.ApplicationSettings;
using AbiokaRdn.Infrastructure.Common.Exceptions;
using AbiokaRdn.Infrastructure.Common.Helper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AbiokaRdn.ApplicationService.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly string apiKey;
        private readonly string domain;
        private readonly string from;
        private readonly ICurrentContext currentContext;
        private readonly IExceptionLogRepository exceptionLogRepository;
        private readonly IHttpClient httpClient;

        public EmailService(IConfigurationManager configurationManager, ICurrentContext currentContext, IExceptionLogRepository exceptionLogRepository, IHttpClient httpClient) {
            var mailgunApiValues = configurationManager.ReadAppSetting("MailgunApiValues").Split(',');
            apiKey = mailgunApiValues[0];
            domain = mailgunApiValues[1];
            from = mailgunApiValues[2];

            this.currentContext = currentContext;
            this.exceptionLogRepository = exceptionLogRepository;
            this.httpClient = httpClient;
        }

        public async Task SendAsync(EmailRequest emailRequest) {
            var form = new Dictionary<string, string>();
            form["from"] = emailRequest.From ?? from;
            form["to"] = emailRequest.To;
            if (!string.IsNullOrEmpty(emailRequest.Cc)) {
                form["cc"] = emailRequest.Cc;
            }
            if (!string.IsNullOrEmpty(emailRequest.Bcc)) {
                form["bcc"] = emailRequest.Bcc;
            }
            form["subject"] = emailRequest.Subject;
            form["html"] = emailRequest.Body;

            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(($"https://api.mailgun.net/v3/{domain}/messages")));
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", $"api:{apiKey}".EncodeWithBase64());
            request.Content = new FormUrlEncodedContent(form);

            var response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                var exceptionLog = new ExceptionLog("MailChimpSubscriber", $"To: {emailRequest.To}, Subject: {emailRequest.Subject}", "Mailgun", "Mailgun", content, currentContext.Current.Principal?.Id ?? Guid.Empty, currentContext.Current.IP);
                exceptionLogRepository.Add(exceptionLog);
            }
        }
    }
}
