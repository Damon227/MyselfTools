// ***********************************************************************
// Solution         : KC.Foundation
// Project          : KC.Foundation.AspNetCore
// File             : SwaggerIgnoreAttribute.cs
// Updated          : 2017-10-24 10:54 AM
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;

namespace IdentityDemo.Swagger
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SwaggerIgnoreAttribute : Attribute
    {
        // 也可以使用 [ApiExplorerSettings(IgnoreApi = true)] 或者 IActionModelConvention
        // See more at https://github.com/domaindrivendev/Swashbuckle.AspNetCore#omit-arbitrary-operations
    }
}