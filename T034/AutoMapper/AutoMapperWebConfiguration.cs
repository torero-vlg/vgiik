﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using AutoMapper;
using T034.Api.Entity;

namespace T034.AutoMapper
{
    public static class AutoMapperWebConfiguration
    {
        public static List<T> StringToCollection<T>(string ids) where T : Entity, new()
        {
            return string.IsNullOrEmpty(ids) ? null : new List<T>(ids.Split(new string[] { "," }, StringSplitOptions.None).Select(n => new T { Id = Convert.ToInt32(n) }));
        }

        public static string IdsToString<T>(ICollection<T> collection) where T : Entity
        {
            return collection == null || !collection.Any() ? "" : collection.Select(n => n.Id.ToString()).Aggregate((i, j) => i.ToString() + "," + j.ToString());
        }

        public static void Configure(HttpServerUtility server)
        {
            var profiles = Assembly.GetAssembly(typeof(AutoMapperWebConfiguration)).GetTypes().Where(t => typeof(Profile).IsAssignableFrom(t));
            foreach (var profile in profiles)
            {
                //TODO убрать этот костыль
                if (profile.GetConstructors().Count() > 1)
                    Mapper.AddProfile(Activator.CreateInstance(profile, server) as Profile);
                else
                    Mapper.AddProfile(Activator.CreateInstance(profile) as Profile);
            }
        }

        public static string GetSafeHtml(string htmlInputTxt)
        {
            var sb = new StringBuilder(HttpUtility.HtmlEncode(htmlInputTxt));

            sb.Replace("&lt;script&gt;", "");
            sb.Replace("&lt;/script&gt;", "");
            return sb.ToString();
        }
    }
}