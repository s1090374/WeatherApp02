﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class ApiService
    {
        public static async Task<Root> GetWeather(double latitude, double longitude)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(string.Format("http://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&appid={b227c256394afb304ce519255d60469e}"));
            return JsonConvert.DeserializeObject<Root>(response);
        }
        public static async Task<Root> GetWeatherByCity(string city)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(string.Format("https://api.openweathermap.org/data/2.5/forecast?q={taichung}&units={metric}&lang={zh_tw}&appid={b227c256394afb304ce519255d60469e}"));
            return JsonConvert.DeserializeObject<Root>(response);
        }
    }
}
