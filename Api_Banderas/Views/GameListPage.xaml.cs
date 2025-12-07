using RawGames.Services;
using RawGames.ViewModel;

namespace RawGames.Views;

public partial class GameListPage : ContentPage
{
    public GameListPage()
    {
        InitializeComponent();
        BindingContext = new CountryListViewModel(new CountryApiService());
    }
}