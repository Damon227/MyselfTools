// ***********************************************************************
// Solution         : KC.Foundation
// Project          : KC.Foundation.AspNetCore
// File             : OperationFilter.cs
// Updated          : 2017-10-25 11:43 AM
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace IdentityDemo.Swagger
{
    public class OperationFilter : IOperationFilter
    {
        #region IOperationFilter Members

        public void Apply(Operation operation, OperationFilterContext context)
        {
            IList<AuthorizeAttribute> authAttributes = context.ApiDescription.ControllerAttributes()
                .Union(context.ApiDescription.ActionAttributes()).OfType<AuthorizeAttribute>().ToList();

            //if (authAttributes.Any())
            //{
            //    operation.Summary = "[Authorize] " + operation.Summary;

            //    operation.Responses.AddIfNotExist("401", new Response
            //    {
            //        Description = "Unauthorized",
            //        Examples = new Dictionary<string, KolibreResponse> { { "", KolibreResponse.BuildUnauthorizedResponse() } },
            //        Schema = new Schema { Ref = "#/definitions/KC.Foundation.AspNet.Model.KolibreResponse" }
            //    });

            //    operation.Responses.AddIfNotExist("403", new Response
            //    {
            //        Description = "Forbidden",
            //        Examples = new Dictionary<string, KolibreResponse> { { "", KolibreResponse.BuildForbiddenResponse() } },
            //        Schema = new Schema { Ref = "#/definitions/KC.Foundation.AspNet.Model.KolibreResponse" }
            //    });
            //}

            //operation.Responses.AddIfNotExist("400", new Response
            //{
            //    Description = "BadRequest",
            //    Examples = new Dictionary<string, KolibreResponse> { { "", KolibreResponse.BuildBadRequestResponse() } },
            //    Schema = new Schema { Ref = "#/definitions/KC.Foundation.AspNet.Model.KolibreResponse" }
            //});

            //operation.Responses.AddIfNotExist("500", new Response
            //{
            //    Description = "Internal Server Error",
            //    Examples = new Dictionary<string, KolibreResponse> { { "", KolibreResponse.BuildInternalServerErrorResponse() } },
            //    Schema = new Schema { Ref = "#/definitions/KC.Foundation.AspNet.Model.KolibreResponse" }
            //});

            operation.Produces.Clear();
            operation.Produces.Add("application/json");
        }

        #endregion
    }
}