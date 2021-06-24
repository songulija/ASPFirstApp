﻿using System.Threading.Tasks;

namespace App.Repository.ApiClient
{
    //its extracted interface
    public interface IWebApiExecuter
    {
        Task InvokeDelete<T>(string uri);
        Task<T> InvokeGet<T>(string uri);
        Task<T> InvokePost<T>(string uri, T obj);
        Task InvokePut<T>(string uri, T obj);
    }
}