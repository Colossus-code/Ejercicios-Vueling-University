﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SimpleLoginRepository.Models
{
    public partial class Users
    {
        public Users()
        {
            Orders = new HashSet<Orders>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }

        public virtual UserPassword UserPassword { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}