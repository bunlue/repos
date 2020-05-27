using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovieWA.Client.Helpers
{
    public static class NaviationManagerExtensions
    {
        public static Dictionary<string, string> getQueryStrings(
            this NavigationManager navigationManager, string url)
        {
            if (string.IsNullOrWhiteSpace(url) || !url.Contains("?") || url.Substring(url.Length -1) == "?")
            {
                return null;
            }

            // https://domain.com?key1=value1&key2=value2

            var queryStrings = url.Split(new string[] { "?" }, StringSplitOptions.None)[1];
            Dictionary<string, string> dicQueryString =
                queryStrings.Split('&')
                .ToDictionary(c => c.Split('=')[0],
                c => Uri.UnescapeDataString(c.Split('=')[1]));
            //Console.WriteLine($"I'm here.{dicQueryString["intheaters"]} ");
            return dicQueryString;
        }
    }
}
