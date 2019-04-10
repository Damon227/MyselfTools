// ***********************************************************************
// Solution         : KC.Foundation
// Project          : KC.Foundation.AspNetCore
// File             : SchemaFilter.cs
// Updated          : 2017-10-25 4:53 PM
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace IdentityDemo.Swagger
{
    public class SchemaFilter : ISchemaFilter
    {
        #region ISchemaFilter Members

        public void Apply(Schema model, SchemaFilterContext context)
        {
        }

        #endregion
    }
}