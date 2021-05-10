using NLog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using Training.Job.DataContracts;

namespace Training.Job.WeeklyScheduler
{
    public static class MailHandler
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public static void SendWeeklyMail(List<TaskResource> jobs)
        {
            using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                var basicCredential = new NetworkCredential("panaintediana91@gmail.com", "Anewlife,22");
                using (MailMessage message = new MailMessage())
                {
                    MailAddress fromAddress = new MailAddress("panaintediana91@gmail.com");

                    smtpClient.Host = "smtp.gmail.com";
                    smtpClient.Port = 587;
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.Credentials = basicCredential;
                    message.From = fromAddress;
                    message.Subject = "Weekly job to run";
                    // Set IsBodyHtml to true means you can send HTML email.
                    message.IsBodyHtml = true;

                    StringBuilder bodyTemplate = new StringBuilder();

                    foreach (var job in jobs)
                    {
                        bodyTemplate.Append("<tr>\r\n");
                        bodyTemplate.Append("<td style=\"font-family: Verdana; padding:2px; font-size: 12px; text-align:left\">" + job.TaskName + "</td>\r\n");
                    }

                    message.Body = bodyTemplate.ToString();

                    message.To.Add("diana.panainte@yopeso.com");

                    try
                    {
                        smtpClient.Send(message);
                    }
                    catch (SmtpException ex)
                    {
                        logger.Debug("Error: Inside catch block of Mail sending");
                        logger.Error("Error msg:" + ex);
                        logger.Error("Stack trace:" + ex.StackTrace);
                        Exception exNew = new Exception("Exception caught in SMTP delivery", ex);
                        throw exNew;
                    }
                }
            }
        }
    }
}
