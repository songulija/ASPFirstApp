﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace App.Repository.ApiClient
{
    //this contains code to invoke api endpoints
    public class WebApiExecuter : IWebApiExecuter
    {
        private readonly string baseUrl;
        private readonly HttpClient httpClient;
        //Constructor takes baseUrl and httpClient.
        public WebApiExecuter(string baseUrl, HttpClient httpClient)
        {
            this.baseUrl = baseUrl;
            this.httpClient = httpClient;

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        //create for different methods that corresponds to CRUD

        public async Task<T> InvokeGet<T>(string uri)
        {
            return await httpClient.GetFromJsonAsync<T>(GetUrl(uri));
        }

        public async Task<T> InvokePost<T>(string uri, T obj)
        {
            var response = await httpClient.PostAsJsonAsync(GetUrl(uri), obj);
            await HandleError(response);

            return await response.Content.ReadFromJsonAsync<T>();
        }

        

        public async Task InvokePut<T>(string uri, T obj)
        {
            var response = await httpClient.PutAsJsonAsync(GetUrl(uri), obj);
            response.EnsureSuccessStatusCode();
        }

        public async Task InvokeDelete(string uri)
        {
            var response = await httpClient.DeleteAsync(GetUrl(uri));
            response.EnsureSuccessStatusCode();
        }

        private string GetUrl(string uri)
        {
            return $"{baseUrl}/{uri}";
        }


        private static async Task HandleError(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(error);
            }
        }
    }
}
