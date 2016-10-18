using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Mail;

namespace PersonalScheduler.Notifiers
{

    class EmailNotifier : INotifier
    {
        private string[] Scopes = { GmailService.Scope.GmailSend, GmailService.Scope.GmailReadonly };
        private string ApplicationName = "PersonalSchelduer";

        private GmailService service;
        private string mail;

        public EmailNotifier()
        {
            UserCredential credential;
            using (var stream =
                new FileStream("Notifiers/client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = ".credentials/gmail-dotnet-user.json";

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });


            mail = service.Users.GetProfile("me").Execute().EmailAddress;


        }
        private string Base64UrlEncode(string input)
        {
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(inputBytes)
              .Replace('+', '-')
              .Replace('/', '_')
              .Replace("=", "");
        }
        public void Notify(ScheduledEvent ev)
        {
            string text = String.Join(Environment.NewLine, "It's time for " + ev.Name);
            if (ev.Description != null)
            {
                text = String.Join(Environment.NewLine, text, "Description:", ev.Description);
            }
            if (ev.Description != null)
            {
                text = String.Join(Environment.NewLine, text, "Place:", ev.Place);
            }
            var msg = new AE.Net.Mail.MailMessage
            {
                Subject = "Notification",
                Body = text,
                From = new MailAddress(mail)
            };
            msg.To.Add(new MailAddress(mail));
            msg.ReplyTo.Add(msg.From);
            var msgStr = new StringWriter();
            msg.Save(msgStr);

            var gmail = service;
            var result = gmail.Users.Messages.Send(new Message
            {
                Raw = Base64UrlEncode(msgStr.ToString())
            }, "me").Execute();

        }
    }
}



