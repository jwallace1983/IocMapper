﻿namespace Samples.Microsoft.DependencyInjection.WebApiNet.Models
{
    public class WeatherForecast
    {
        public System.DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
