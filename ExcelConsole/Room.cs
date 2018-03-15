// ***********************************************************************
// Solution         : MyselfTools
// Project          : ExcelConsole
// File             : Contract.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2017 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************


using System;

namespace ExcelConsole
{
    internal class Room
    {
        public string ActorId { get; set; }

        public string RoomId { get; set; }

        public string ApartmentId { get; set; }

        public string Floor { get; set; }

        public string RoomNumber { get; set; }

        public long Price { get; set; }

        public DateTimeOffset CreateTime { get; set; }
    }
}