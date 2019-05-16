using ReactiveUI;
using System.Reactive;

namespace ReactiveWpfApp.Models
{
    public class ProductViewModel : ReactiveObject
    {
        private readonly Product _metadata;

        public ProductViewModel(Product metadata)
        {
            _metadata = metadata;
            OpenPage = ReactiveCommand.Create(() => { });
        }

        public int Id => _metadata.Id;

        public string Name => _metadata.Name;

        public int Count => _metadata.Count;

        public ReactiveCommand<Unit, Unit> OpenPage { get; }
    }
}
