﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SimpleLoginRepository.Models
{
    public partial class Orders
    {
        public int OrderId { get; set; }
        public string OrderProduct { get; set; }
        public string OrderDescription { get; set; }
        public int? CustomerId { get; set; }
        public DateTime OrderDateDeliver { get; set; }

        public virtual Users Customer { get; set; }
    }
}