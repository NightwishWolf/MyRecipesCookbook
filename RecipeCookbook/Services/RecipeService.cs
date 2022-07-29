using Newtonsoft.Json;
using RecipeCookbook.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RecipeCookbook.Services
{
    public class RecipeService
    {
        public HttpClientHandler httpClientHandler = new HttpClientHandler();
        public RecipeService()
        {
            //Make sure httpclient ignores invalid certicates
            this.httpClientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
            this.httpClientHandler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
        }

        //Getter function so everytime a call is run this is reinitialized
        //That way we always grab the latest accesstoken from preferences
        public HttpClient client
        {
            get
            {

                //Initialize httpclient based on handler
                var client = new HttpClient(this.httpClientHandler);

                //Set baseaddress to api adress
                client.BaseAddress = new Uri("https://10.0.2.2:5224");

                //return client
                return client;
            }
        }

        public async Task<List<RecipeItem>> GetAllRecipes()
        {
            //Call backend based on custom apiclient class
            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync("/Recipes");

                if (response.IsSuccessStatusCode)
                {
                    //Read content as normal string
                    var content = await response.Content.ReadAsStringAsync();
                    //Because the response are teams in json format, convert those to actual team objects
                    return JsonConvert.DeserializeObject<List<RecipeItem>>(content);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Fuck");
            }

            //Check if successfull


            return null;

        }
    }
}
