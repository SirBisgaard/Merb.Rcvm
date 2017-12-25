using System;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Merb.Rcvm.SkatService.Domain
{
    public class SkatDmrVehicleRepository
    {
        private readonly HttpClient _client;

        public SkatDmrVehicleRepository()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://www.nummerplade.net/soeg/")
            };
        }

        public async Task<Vehicle> GetVehicleFromRegistrationNumber(string registrationNumber)
        {
            var content = await _client.GetAsync($"?regnr={registrationNumber}");

            return await GetVehicleFromHtml(content);
        }

        public async Task<Vehicle> GetVehicleFromVin(string vin)
        {
            var content = await _client.GetAsync($"?stelnr={vin}");

            return await GetVehicleFromHtml(content);
        }

        private async Task<Vehicle> GetVehicleFromHtml(HttpResponseMessage content)
        {
            var document = new HtmlDocument();
            document.Load(await content.Content.ReadAsStreamAsync());

            var vehicle = new Vehicle
            {
                VinNumber = GetValue(document, "//td[@id='stelnr']"),
                RegistrationNumber = GetValue(document, "//td[@id='regnr']"),
                Brand = GetValue(document, "//td[@id='maerke']"),
                Model = GetValue(document, "//td[@id='model']"),
                Variant = GetValue(document, "//td[@id='variant']"),
            };

            return vehicle;
        }

        private static string GetValue(HtmlDocument document, string xpath)
        {
            return document.DocumentNode.SelectSingleNode(xpath)?.InnerText ?? string.Empty;
        }
    }
}
