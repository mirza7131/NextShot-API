using Microsoft.Extensions.Configuration;
using SMSSender.DTO;
using System.Net;
using System.Text.Json;
using System.Text;
using System.Xml;

namespace SMSSender
{
    public class SMS
    {
        #region Class Fields & Propertities

        private readonly bool isDevelopment = false;

        #endregion

        public SMS(IConfiguration config)
        {
            isDevelopment = bool.Parse(config.GetSection("IsDevelopment").Value?.ToString() ?? "true");
        }

        public SendSMSDto? SendSMS(SendSMSDto sms)
        {
            try
            {
                //if (!isDevelopment)
                if (true)
                {
                    return SendSMSJazz(sms);
                }

                return null;
                
            }
            catch (Exception)
            {
                return sms;
            }
        }

        private SendSMSDto SendSMSJazz(SendSMSDto sms)
        {
          
            sms.Sender = "03018482714";
            sms.Mask = sms.Mask == null ? "HISDU" : sms.Mask;
            sms = sendQuickMessageJazz(sms);
            return sms;
        }
        private SendSMSDto sendQuickMessageJazz(SendSMSDto sms)
        {

            try
            {
                var task = sendQuickMessageJazzAsync(sms);
                task.Wait(); // Wait for the task to complete (blocking call)

                return sms;

                //string url = @"https://connect.jazzcmt.com/sendsms_url.html?Username=03018482714&Password=Jazz@123&From=" + sms.Mask + "&To=" + sms.Receiver + "&Message=" + sms.Body + "&Identifier=123456&UniqueId=123456789&ProductId=123456789&Channel=123456789&TransactionId=123456789";
                //var res = sendRequestJazz(url);
                ////sms.MessageId = res.Replace("Successful:", "");
                //sms.StatusResponse = res;
                //sms.Status = res.Contains("Successfully") ? "Sent" : "Error";
                //sms.SentDate = DateTime.UtcNow.AddHours(5);
                ////SaveSMSHistory(sms);
                //return sms;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        private async Task<string> sendQuickMessageJazzAsync(SendSMSDto sms)
        {
            try
            {
                sms.Message = sms.Body;
                sms.mobileNumber = sms.Receiver;
                var serializedParam = JsonSerializer.Serialize(sms);
                var stringContent = new StringContent(serializedParam, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("XApiKey", "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXp");
                var httpResponse = await client.PostAsync("https://sms.pshealthpunjab.gov.pk/api/SMSServicesAPI/SentSMS", stringContent);
                var content = await httpResponse.Content.ReadAsStringAsync();
                SendSMSDto jsonDeserialize = JsonSerializer.Deserialize<SendSMSDto>(content);
                return content;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public string sendRequestJazz(string url)
        {
            string response = null;
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                var client = new WebClient();
                Uri smsUri = new Uri(url);
                response = client.DownloadString(smsUri);
                XmlDocument xmldoc = new XmlDocument();
                //xmldoc.LoadXml(response);
                //XmlNodeList responseType = xmldoc.GetElementsByTagName("response_to_browser");
                //XmlNodeList data = xmldoc.GetElementsByTagName("response_id");
                //XmlNodeList text = xmldoc.GetElementsByTagName("response_text");
                //if (responseType.Equals("Error"))
                //{
                //    return null;
                //}
                //string responseId = data[0].InnerText;
                //string responseText = response;
                //return responseText.Replace("Successful:", "");
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        //public void SaveSMSHistory(SMSViewModel model)
        //{
        //    using (var db = new UnitOfWork<AppSmsHistory>())
        //    {
        //        AppSmsHistory his = new AppSmsHistory();
        //        his.Sender = model.Sender;
        //        his.Receiver = model.Receiver;
        //        his.Mask = model.Mask;
        //        his.SentDate = model.SentDate;
        //        his.Note = model.Note;
        //        his.SmssessionId = model.SmssessionId;
        //        his.MessageId = model.MessageId;
        //        his.Fkid = model.Fkid;
        //        his.Body = model.Body;
        //        his.CreatedBy = model.CreatedBy;
        //        his.SentDate = model.SentDate;
        //        his.Status = model.Status;
        //        his.StatusResponse = model.StatusResponse;
        //        db.Repository.Insert(his);
        //        db.Save();
        //    }
        //}

    }
}