﻿using System;
using System.ComponentModel.DataAnnotations;
using VueStore.Repository.Interfaces;

namespace VueStore.Repository.Models
{
    public class Item : IBaseItem
    {
        [Key]
        public int Id { get; set; }
        public string ItemName { get; set; }
        public int Cost { get; set; }
    }
}
