using ReactiveUI;
using ReactiveWpfApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace ReactiveWpfApp.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        public MainWindowViewModel()
        {
            productViewModels = this
                        .WhenAnyValue(x => x.SearchText)
                        .Throttle(TimeSpan.FromMilliseconds(500))
                        .Select(term => term?.Trim())
                        .DistinctUntilChanged()
                        //.Where(term => !string.IsNullOrWhiteSpace(term))
                        .SelectMany(GetProductViewModels)
                        .ObserveOn(RxApp.MainThreadScheduler)
                        .ToProperty(this, x => x.ProductViewModels);

            _ = productViewModels.ThrownExceptions.Subscribe(error =>
            {
                /* Handle errors here */
            });
        }

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set => this.RaiseAndSetIfChanged(ref searchText, value);
        }

        private readonly ObservableAsPropertyHelper<IEnumerable<ProductViewModel>> productViewModels;
        public IEnumerable<ProductViewModel> ProductViewModels => productViewModels.Value;


        public async Task<IEnumerable<ProductViewModel>> GetProductViewModels(string term)
        {
            List<ProductViewModel> list = new List<ProductViewModel>();

            await Task.Run(new Action(() =>
            {
                for (int i = 0; i < 10000; i++)
                {
                    list.Add(new ProductViewModel(new Product { Id = i, Name = i.ToString(), Count = 1 }));
                }
            })).ConfigureAwait(false);

            return string.IsNullOrEmpty(term) ? list : list.Where(f => f.Name.Contains(term));
        }
    }
}
