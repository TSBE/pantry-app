﻿using Pantry.Mobile.Core.ViewModels;

namespace Pantry.Mobile.Views;

public partial class HouseholdPage : ContentPage
{
    public HouseholdPage(HouseholdViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
