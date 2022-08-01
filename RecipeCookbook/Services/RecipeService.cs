using Newtonsoft.Json;
using RecipeCookbook.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Forms;

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



        public async Task<bool> GetSingleRecipe(int itemId)
        {
            //Call backend based on custom apiclient class
            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync("/Recipes/" + itemId);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Fuck");
            }

            return false;
        }

        public async Task<RecipeItem> PostRecipe (RecipeItem recipeItem)
        {
            //Call backend based on custom apiclient class
            HttpResponseMessage response;

                //Make a JSON from the user object
                var json = JsonConvert.SerializeObject(recipeItem);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                response = await client.PostAsync("/Recipes", content);
                //If the post is not successfull a pop-up appears with an error
                if (!response.IsSuccessStatusCode)
                {
                    _ = Application.Current.MainPage.DisplayAlert("Failed", "Something went wrong", "Okay");
                }
           

            //If it was successfull the response is transformed again, a pop-up for success appears and the content is returned
            var responseJson = await response.Content.ReadAsStringAsync();
            var responseContent = JsonConvert.DeserializeObject<RecipeItem>(responseJson);
            _ = Application.Current.MainPage.DisplayAlert("Succes!", "Recipe was added", "Okay");
            return responseContent;
        }
    }
}
