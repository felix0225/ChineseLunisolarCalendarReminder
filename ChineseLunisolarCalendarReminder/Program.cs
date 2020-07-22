using System;
using System.Collections.Generic;
using System.Net.Http;

namespace ChineseLunisolarCalendarReminder
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime dt = DateTime.Now.AddDays(1);
            System.Globalization.TaiwanLunisolarCalendar tlc = new System.Globalization.TaiwanLunisolarCalendar();
            var lDay = tlc.GetDayOfMonth(dt);
            //判斷農歷初一，十五
            if (lDay == 1 || lDay == 15)
            {
                const string token = "改成要發送LineNotify的Token";
                var client = new HttpClient { BaseAddress = new Uri("https://notify-api.line.me/api/notify") };
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                var kvDict = new Dictionary<string, string> { { "message", "改成要發送的訊息內容" } };

                var content = new FormUrlEncodedContent(kvDict);

                var response = client.PostAsync("", content).Result;
                response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
        }
    }
}
