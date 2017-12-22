﻿using GoodRecipe.UWP.Abstracts;
using GoodRecipe.UWP.Models;
using GoodRecipe.UWP.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Controls;

namespace GoodRecipe.UWP.ViewModels
{
    public class NavigationViewModel : NotifyableClass
    {
        public ObservableCollection<MenuItem> MenuItems { get; set; }

        private MenuItem _selectedMenuItem;

        public MenuItem SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set { Set(ref _selectedMenuItem, value); }
        }

        public void Initialize()
        {
            MenuItems = new ObservableCollection<MenuItem>(GetMenuItems());
            SelectedMenuItem = MenuItems.FirstOrDefault();
        }

        private List<MenuItem> GetMenuItems()
        {
            var menuItems = new List<MenuItem>();
            menuItems.Add(new MenuItem() { Title = "Home", Icon = Symbol.Home, NavigateTo = typeof(HomeView) });
            menuItems.Add(new MenuItem() { Title = "My Food", Icon = Symbol.OutlineStar, NavigateTo = typeof(MyFoodView) });
            menuItems.Add(new MenuItem() { Title = "Search", Icon = Symbol.Map, NavigateTo = typeof(SearchView) });

            return menuItems;
        }
    }
}
