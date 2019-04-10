// ***********************************************************************
// Solution         : KC.Foundation
// Project          : KC.Foundation.AspNetCore
// File             : SwaggerGenOptions.cs
// Updated          : 2017-10-25 5:11 PM
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace IdentityDemo.Swagger
{
    public class SwaggerGenOptions
    {

        public static readonly Func<ApiDescription, string> TagSelector = apiDescription => GetGroupName(apiDescription);

        public static Func<string, ApiDescription, bool> GetDocInclusionPredicate(string defaultDocName, string defaultVersion = "1_0")
        {
            return (docName, apiDescription) =>
            {
                if (apiDescription.ControllerAttributes()
                    .Union(apiDescription.ActionAttributes()).OfType<SwaggerIgnoreAttribute>().Any())
                {
                    return false;
                }

                if (string.IsNullOrEmpty(apiDescription.GroupName)) // 当版本号为空时
                {
                    // 必须只能显示在默认文档
                    if (!docName.Equals(defaultDocName, StringComparison.OrdinalIgnoreCase))
                    {
                        return false;
                    }
                }
                else // 如果版本号不为空时
                {
                    // 当文档是默认文档，只显示当前默认版本的 API
                    if (docName.Equals(defaultDocName, StringComparison.OrdinalIgnoreCase))
                    {
                        if (!apiDescription.GroupName.Equals(defaultVersion))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        // 当文档不是默认文档，只显示对应默认版本的 API
                        if (!apiDescription.GroupName.Equals(docName, StringComparison.OrdinalIgnoreCase))
                        {
                            return false;
                        }
                    }
                }


                return true;
            };
        }

        private static string GetGroupName(ApiDescription apiDescription)
        {
            string actionDisplayName = apiDescription.ActionDescriptor.DisplayName;
            // $"{typeof(Startup).Namespace}.Controllers."
            string removePrefix = actionDisplayName.Remove(0, actionDisplayName.IndexOf(".Controllers.", StringComparison.OrdinalIgnoreCase) + ".Controllers.".Length);
            int index = removePrefix.IndexOf("Controller", StringComparison.Ordinal);
            return removePrefix.Substring(0, index);
        }

        private static string GetHttpMethodOrder(string httpMethod)
        {
            string method = httpMethod.ToUpperInvariant();

            switch (method)
            {
                case "GET":
                    return "1";
                case "POST":
                    return "2";
                case "PUT":
                    return "3";
                case "DELETE":
                    return "4";
                case "HEAD":
                    return "5";
                default:
                    return method;
            }
        }
    }
}