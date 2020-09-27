using System;
using System.ComponentModel.DataAnnotations;
using VueStore.Repository.Interfaces;

namespace VueStore.Repository.Models
{
    public class GroupedItem : IBaseItem
    {
        public string ItemName { get; set; }
        public int Cost { get; set; }
    }
}
