// This code requires the Nuget package Microsoft.AspNet.WebApi.Client to be installed.
// Instructions for doing this in Visual Studio:
// Tools -> Nuget Package Manager -> Package Manager Console
// Install-Package Microsoft.AspNet.WebApi.Client

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CallRequestResponseService
{
    public static class AMLRequest
    {
        
        public static async Task<String> InvokeRequestResponseService(String text)
        {
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {
                    Inputs = new Dictionary<string, List<Dictionary<string, string>>>() {
                        {
                            "input1",
                            new List<Dictionary<string, string>>(){new Dictionary<string, string>(){
                                            {
                                                "Col1", "26151" // exempel: 26151
                                            },
                                            {
                                                "Col2", text // Exempel: socc colomb colomb colomb colomb beat beat chil chil chil chil world world world cup cup cup qualif qualif qualif qualif halftim south south americ americ match sunday scor faustin asprill st minut minut jorg bermudez ivan zamoran penalt attend group stand tabulat play won draw lost goal goal point ...
                                            },
                                }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };

                const string apiKey = "M8bSopR28ftIcsuDLYebhRN+P+DXM5qGJyOjs/hkcDfi2iXdOAnEpkGeXReq39ddrcQ5Xr7DjFgVaDQen24fsg=="; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/2084af0c83aa4beeaf5be937354ef4ea/services/eddb57b09d6c43e68f8dbdc33b813ab1/execute?api-version=2.0&format=swagger");

                // WARNING: The 'await' statement below can result in a deadlock
                // if you are calling this code from the UI thread of an ASP.Net application.
                // One way to address this would be to call ConfigureAwait(false)
                // so that the execution does not attempt to resume on the original context.
                // For instance, replace code such as:
                //      result = await DoSomeTask()
                // with the following:
                //      result = await DoSomeTask().ConfigureAwait(false)

                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);
                Console.WriteLine();
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Result: {0}", result);
                    System.Diagnostics.Debug.WriteLine("Result: {0}", result);
                    return result;
                }
                else
                {
                    Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp,
                    // which are useful for debugging the failure
                    Console.WriteLine(response.Headers.ToString());
                    System.Diagnostics.Debug.WriteLine(response.Headers.ToString());
                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                    System.Diagnostics.Debug.WriteLine(responseContent);
                    return responseContent;
                }
            }
        }
    }
}
