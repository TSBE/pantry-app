﻿using Pantry.Mobile.Core.ViewModels;

namespace Pantry.Mobile.Views;

public abstract class BasePage<TViewModel>(TViewModel viewModel) : BasePage(viewModel)
    where TViewModel : BaseViewModel
{
    public new TViewModel BindingContext => (TViewModel)base.BindingContext;
}

public abstract class BasePage : ContentPage
{
    protected BasePage(object? viewModel = null)
    {
        BindingContext = viewModel;
        Padding = 12;

        if (string.IsNullOrWhiteSpace(Title))
        {
            Title = GetType().Name;
        }
    }
}
