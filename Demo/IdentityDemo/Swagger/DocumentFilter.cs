// ***********************************************************************
// Solution         : KC.Foundation
// Project          : KC.Foundation.AspNetCore
// File             : DocumentFilter.cs
// Updated          : 2017-10-25 5:19 PM
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System.Collections.Generic;
using System.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace IdentityDemo.Swagger
{
    public class DocumentFilter : IDocumentFilter
    {
        #region IDocumentFilter Members

        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            //make operations alphabetic
            List<KeyValuePair<string, PathItem>> paths = swaggerDoc.Paths.OrderBy(e => e.Key).ToList();
            swaggerDoc.Paths = paths.ToDictionary(e => e.Key, e => e.Value);
        }

        #endregion
    }
}