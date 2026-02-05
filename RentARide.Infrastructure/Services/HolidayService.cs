using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RentARide.Application.Common.Interfaces;

namespace RentARide.Infrastructure.Services
{
    public class HolidayService : IHolidayService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HolidayService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> IsHolidayAsync(DateTime date, string countryCode = "DE")
        {
            var year = date.Year;
            var client = _httpClientFactory.CreateClient("NagerDate");
            
            try 
            {
                
                var response = await client.GetAsync($"https://date.nager.at/api/v3/PublicHolidays/{year}/{countryCode}");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var holidays = JsonConvert.DeserializeObject<List<PublicHolidayDto>>(content);
                    
                    if (holidays != null)
                    {
                        return holidays.Exists(h => h.Date.Date == date.Date);
                    }
                }
            }
            catch
            {
                
                
            }

            return false;
        }

        private class PublicHolidayDto
        {
            public DateTime Date { get; set; }
            public string Name { get; set; } = string.Empty;
        }
    }
}
