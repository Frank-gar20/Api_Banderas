
using RawGames.Model;
using RawGames.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RawGames.ViewModel
{
    public class CountryListViewModel : BaseViewModel
    {
        private readonly CountryApiService _apiService;
        private string _searchText = string.Empty;

        public ObservableCollection<Country> Countries { get; } = new();

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    //Funcion asincrona de busqueda
                }
            }
        }

        public ICommand LoadCountriesCommand { get; }
        public ICommand RefreshCommand { get; }

        public ICommand CountriesSelectedCommand { get; }

        public CountryListViewModel(CountryApiService apiService)
        {
            _apiService = apiService;
            Title = "PAISES";

            LoadCountriesCommand = new Command(async () => await LoadCountriesAsync());
            RefreshCommand = new Command(async () => await RefreshCountriesAsync());
            CountriesSelectedCommand = new Command<Country>(OnCountrySelected);

            // Cargar Paises al inicializar
            Task.Run(async () => await LoadCountriesAsync());
        }

        private void OnCountrySelected(Country pais)
        {
            if (pais == null)
                return;

            // Aquí puedes navegar a la página de detalles del Pais
            // Por ahora solo mostramos un mensaje
            Application.Current?.MainPage?.DisplayAlert("Pais Seleccionado", pais.Name.Common, "OK");
        }

        private async Task LoadCountriesAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                Countries.Clear();

                var result = await _apiService.GetAllCountriesAsync();

                if (result != null)
                {
                    foreach (var c in result.OrderBy(x => x.Name.Common))
                    {
                        Countries.Add(c);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar los Paises: {ex.Message}");
                await Application.Current!.MainPage!.DisplayAlert("Error", "No se pudieron cargar los Paises. Verifica la conexion a Internet.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        private async Task RefreshCountriesAsync()
        {
            await LoadCountriesAsync();
        }
    }
}