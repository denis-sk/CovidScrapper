using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using CleanArchitecture.Domain.Entities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CleanArchitecture.WebScrapper
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Load default configuration
                var config = Configuration.Default.WithDefaultLoader();
                // Create a new browsing context
                var context = BrowsingContext.New(config);
                // This is where the HTTP request happens, returns <IDocument> that // we can query later
                var document = await context.OpenAsync("https://www.worldometers.info/coronavirus/");
                // Get statistics table
                var statistics = document.QuerySelectorAll("#main_table_countries_today");

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                var stopWatch = Stopwatch.StartNew();

                //await ScrapAsync(statistics);
                //Console.WriteLine(stopWatch.Elapsed);
                //stopWatch.Restart();
                Scrap(statistics);
                Console.WriteLine(stopWatch.Elapsed);
                await Task.Delay(30000, stoppingToken);
            }
        }
        private void Scrap(IHtmlCollection<IElement> tableBody)
        {
            foreach (var table in tableBody)
            {
                var body = table.QuerySelectorAll("tbody").FirstOrDefault();
                if (body != null)
                {
                    var lines = body.QuerySelectorAll("tr");
                    foreach (var line in lines)
                    {
                        var collumns = line.QuerySelectorAll("td");
                        var coronaCountry = new CoronaCountry();
                        int x;
                        coronaCountry.Title = collumns[1].Text().Trim();
                        if (int.TryParse(collumns[2].Text().Trim().Replace(",", ""), out x)) { coronaCountry.TotalCases = x; }
                        if (int.TryParse(collumns[3].Text().Trim().Replace(",", ""), out x)) { coronaCountry.NewCases = x; }
                        if (int.TryParse(collumns[4].Text().Trim().Replace(",", ""), out x)) { coronaCountry.TotalDeaths = x; }
                        if (int.TryParse(collumns[5].Text().Trim().Replace(",", ""), out x)) { coronaCountry.NewDeaths = x; }
                        if (int.TryParse(collumns[6].Text().Trim().Replace(",", ""), out x)) { coronaCountry.TotalRecovered = x; }
                       

                        SendAsync(coronaCountry);
                    }
                }
            }
            Console.WriteLine("Scrap done");
        }
        private async Task ScrapAsync(IHtmlCollection<IElement> table)
        {
            Parallel.ForEach(table, ParseTable);
            Console.WriteLine("async Scrap done");
        }

        private void ParseTable(IElement table)
        {
            var body = table.QuerySelectorAll("tbody").FirstOrDefault();
            if (body != null)
            {
                var lines = body.QuerySelectorAll("tr");
                Parallel.ForEach(lines, ParseCountry);
            }
        }

        private void ParseCountry(IElement country)
        {
            var collumns = country.QuerySelectorAll("td");
            var coronaCountry = new CoronaCountry();
           
            int x;
            coronaCountry.Title = collumns[1].Text().Trim();
            if (int.TryParse(collumns[2].Text().Trim().Replace(",", ""), out x)) { coronaCountry.TotalCases = x; }
            if (int.TryParse(collumns[3].Text().Trim().Replace(",", ""), out x)) { coronaCountry.NewCases = x; }
            if (int.TryParse(collumns[4].Text().Trim().Replace(",", ""), out x)) { coronaCountry.TotalDeaths = x; }
            if (int.TryParse(collumns[5].Text().Trim().Replace(",", ""), out x)) { coronaCountry.NewDeaths = x; }
            if (int.TryParse(collumns[6].Text().Trim().Replace(",", ""), out x)) { coronaCountry.TotalRecovered = x; }
           
            SendAsync(coronaCountry);
        }

        private async void SendAsync(CoronaCountry model)
        {
            var url = new Uri("https://localhost:44312/api/CoronaCountries");
            using (var client = new WebClient())
            {
                client.Headers["content-type"] = "application/json";

                var reqString = Encoding.Default.GetBytes(JsonConvert.SerializeObject(model, Formatting.Indented));
                client.UploadDataAsync(url, "post", reqString);
            }
        }
    }
}
