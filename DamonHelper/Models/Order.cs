// ***********************************************************************
// Solution         : MyselfTools
// Project          : DamonHelper
// File             : Order.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;

namespace DamonHelper.Models
{
    public class Order
    {
        public string ApartmentId { get; set; }
        public string TenantId { get; set; }
        public string OrderId { get; set; }
        public string ApartmentName { get; set; }
        public string PayeeDraweeRealName { get; set; }
        public string RoomNumber { get; set; }
        public string OrderState { get; set; }
        public long Amount { get; set; }
        public long PropertyManagementAmount { get; set; }
        public long PenaltyAmount { get; set; }
        public long TotalAmount => Amount + PropertyManagementAmount;
        public long PaidAmount { get; set; }
        public DateTimeOffset OrderStartTime { get; set; }
        public DateTimeOffset OrderEndTime { get; set; }
        public DateTimeOffset PaymentTime { get; set; }
    }
}