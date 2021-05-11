using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
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
                string password = ConfigurationManager.AppSettings["password"];
                string email = ConfigurationManager.AppSettings["email"];
                var basicCredential = new NetworkCredential(email, password);
                using (MailMessage message = new MailMessage())
                {
                    MailAddress fromAddress = new MailAddress(email);

                    smtpClient.Host = "smtp.gmail.com";
                    smtpClient.Port = 587;
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.Credentials = basicCredential;
                    message.From = fromAddress;
                    message.Subject = "Weekly job to run";
                    message.IsBodyHtml = true;

                    StringBuilder bodyTemplate = new StringBuilder();

                    foreach (var job in jobs)
                    {
                        bodyTemplate.Append(job.TaskName + "\r\n");
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
