using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp;

namespace Healthee.Models
{
    public class MailService
    {
        /// <summary>
        /// Send email via mailgun 
        /// </summary>
        /// <param name="message"></param>
        public static string SendEmail(string recipients, string subject, string body)
        {
            string fromEmail = Properties.Settings.Default.MailGunFromEmail;

            RestClient client = new RestClient();
            client.BaseUrl = "https://api.mailgun.net/v2";
            //client.BaseUrl = "https://api.mailgun.net/v2/sandboxf9b391d59f714d9a80d8b06f92955718.mailgun.org";

            client.Authenticator =
                    new HttpBasicAuthenticator("api",Properties.Settings.Default.MailGunKey);
            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                                    Properties.Settings.Default.MailGunDomain, 
                                    ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", fromEmail);

            string[] Recipients = recipients.Split(';');
            for (int i = 0; i < Recipients.Length; i++)
            {
                request.AddParameter("to", Recipients[i].ToString());
            }
            request.AddParameter("subject", subject);
            request.AddParameter("html", body);
            request.Method = Method.POST;
            var response = client.Execute(request);
            return (string)response.Content;
        }

        /// <summary>
        /// Sends specific patient registration email 
        /// </summary>
        /// <param name="email"></param>
        public static void SendRegistrationEmail(string email)
        {
            string body = Properties.Settings.Default.EmailTemplate;
            body = body.Replace("@greeting", "Good Day!");
            body = body.Replace("@paragraph1", "Thank you for registering with our premier medical system.");
            body = body.Replace("@bold1", "Healthee Medical System");
            body = body.Replace("@paragraph2", "");
            body = body.Replace("@bold2", "");
            body = body.Replace("@paragraph3", "");
            body = body.Replace("@buttontext", "Sign In Now");
            body = body.Replace("@paragraph4", "");
            body = body.Replace("@paragraph5", "");
            body = body.Replace("@footerlink", "");

            // urls 
            body = body.Replace("@buttonurl", "");
            body = body.Replace("@footerurl", "");

            SendEmail(email, "Healthee Registration Comfirmation", body); 
        }
    }
}