using System.Net.Mail;

namespace Common.Email
{
    public class InternetMessage : MailMessage
    {
        public InternetMessage()
        {
        }

        public InternetMessage(string from, string to) : base(from, to)
        {
        }

        public InternetMessage(string from, string to, string subject, string body) : base(from, to, subject, body)
        {
        }

        public InternetMessage(MailAddress from, MailAddress to) : base(from, to)
        {
        }
    }
}