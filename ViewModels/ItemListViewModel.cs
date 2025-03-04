using System;
using System.Collections.Generic;
using Rent_A_House_Amin_Mecha.Models;

namespace Rent_A_House_Amin_Mecha.ViewModels
{
    public class ItemListViewModel
    {
        public IEnumerable<Item> Items { get; set; }
        public string? CurrentViewName { get; set; }

        public ItemListViewModel(IEnumerable<Item> items, string? currentViewName)
        {
            Items = items;
            CurrentViewName = currentViewName;
        }
    }
}
