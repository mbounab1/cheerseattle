using System;

using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace todo.Services
{
    public class EmailAlerting
    {
        public void SendEmail(string name, int absenceCount)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("CheerSeattle Operations", "mbounab@cheerseattle.org"));
            email.To.Add(new MailboxAddress("meriem bounab", "mbounab1@outlook.com"));

            email.Subject = "Attendance Update";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "<b>Hello " + name + " you have more than 4 absences ! </b>" + "<b>\nYou have: " + absenceCount + " absences recorded </b>"
            };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 587, false);

                // Note: only needed if the SMTP server requires authentication
                smtp.Authenticate("mbounab@cheerseattle.org", "ndmhrxtfnyknkerp");

                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }
    }
}