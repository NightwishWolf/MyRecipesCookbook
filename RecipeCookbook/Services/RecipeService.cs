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
            // httpclient ignores invalid certicates
            this.httpClientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
            this.httpClientHandler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
        }

        //Getter function so everytime a call is run this is reinitialized
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
            HttpResponseMessage response;

            try
            {
                response = await client.GetAsync("/Recipes");

                if (response.IsSuccessStatusCode)
                {
                    //Read content as normal string
                    var content = await response.Content.ReadAsStringAsync();
                    //Because the response are recipes in json format, convert those to actual recipe objects
                    return JsonConvert.DeserializeObject<List<RecipeItem>>(content);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Getting recipes didn't work");
            }

            return null;

        }
        public async Task<RecipeItem> GetSingleRecipe(int itemId)
        {
            HttpResponseMessage response;

            try
            {
                response = await client.GetAsync("/Recipes/" + itemId);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<RecipeItem>(content);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Getting single recipe didn't work");
            }
            return null;
        }

        public async Task<RecipeItem> PostRecipe (RecipeItem recipeItem)
        {
            HttpResponseMessage response;

                //Make a JSON from the recipe object
                var json = JsonConvert.SerializeObject(recipeItem);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                response = await client.PostAsync("/Recipes", content);
                //If the post is not successfull a pop-up appears with an error
                if (!response.IsSuccessStatusCode)
                {
                    _ = Application.Current.MainPage.DisplayAlert("Failed", "Something went wrong", "Okay");
                }

            //If it was successfull the response is transformed again, and the content is returned
            var responseJson = await response.Content.ReadAsStringAsync();
            var responseContent = JsonConvert.DeserializeObject<RecipeItem>(responseJson);
            return responseContent;
        }

        public async Task<RecipeItem> EditRecipe(RecipeItem recipeItem)
        {
            HttpResponseMessage response;

            //Make a JSON from the recipe object
            var json = JsonConvert.SerializeObject(recipeItem);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            response = await client.PutAsync($"Recipes/{recipeItem.RecipeId}", content);
            //If the post is not successfull a pop-up appears with an error
            if (!response.IsSuccessStatusCode)
            {
                _ = Application.Current.MainPage.DisplayAlert("Failed", "Something went wrong", "Okay");
            }

            //If it was successfull the response is transformed again, a pop-up for success appears and the content is returned
            var responseJson = await response.Content.ReadAsStringAsync();
            var responseContent = JsonConvert.DeserializeObject<RecipeItem>(responseJson);
            _ = Application.Current.MainPage.DisplayAlert("Succes!", "Recipe was edited", "Okay");
            return responseContent;
        }

        public async Task DeleteRecipe(RecipeItem recipeItem)
        {
            var response = await client.DeleteAsync($"Recipes/{recipeItem.RecipeId}");

            response.EnsureSuccessStatusCode();
        }
    }
}
