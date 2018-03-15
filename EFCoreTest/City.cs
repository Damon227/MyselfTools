// ***********************************************************************
// Solution         : MyselfTools
// Project          : EFCoreTest
// File             : City.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreTest
{
    [Table("KC.Fengniaowu.Talos.Cities", Schema = "dbo")]
    public class City
    {
        [Key]
        [Required]
        public string CityId { get; set; }

        [Required]
        public string ActorId { get; set; }

        [Required]
        public string CityName { get; set; }

        [Required]
        public bool Enabled { get; set; }

        [Required]
        public DateTimeOffset CreateTime { get; set; }

        [Required]
        public DateTimeOffset UpdateTime { get; set; }

        public string Data { get; set; }
    }
}