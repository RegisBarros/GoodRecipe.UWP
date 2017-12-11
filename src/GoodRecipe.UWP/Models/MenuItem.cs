using System;
using Windows.UI.Xaml.Controls;

namespace GoodRecipe.UWP.Models
{
    public class MenuItem
    {
        public string Title { get; set; }

        public Symbol Icon { get; set; }

        public Type NavigateTo { get; set; }
    }
}
