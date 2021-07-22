using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MonitoringAssignment
{
    public class Monitor
    {
        public async Task MonitorUrls(IEnumerable<string> urls)
        {
            List<Task<(string, bool)>> responseTasks = new();
            Parallel.ForEach(urls, url =>
            {
                var responseTask = OpenLinkAndShowIfIsOk(url);
                responseTasks.Add(responseTask);
            });
            var resultTask = Task.WhenAll(responseTasks);
            var reponses = await resultTask;
            PrintOutReponses(reponses);
        }
        public void PrintOutReponses(IEnumerable<(string Url,bool IsSuccessful)> responses){
            foreach (var response in responses)
            {
                Console.WriteLine(response.Url + "   " + (response.IsSuccessful ? "success" : "error"));
            }
        }
        public async Task<(string, bool)> OpenLinkAndShowIfIsOk(string url)
        {

            HttpClient client = new HttpClient();
            try
            {
                await client.GetAsync(url,HttpCompletionOption.ResponseHeadersRead);
                return (url, true);
            }
            catch
            {
                return (url, false);
            }

        }
    }

}