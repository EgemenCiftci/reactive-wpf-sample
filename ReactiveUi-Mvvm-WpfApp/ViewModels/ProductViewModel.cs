using ReactiveUI;
using ReactiveUi_Mvvm_WpfApp.Models;

namespace ReactiveUi_Mvvm_WpfApp.ViewModels;

public class ProductViewModel(Product metadata) : ReactiveObject
{
    public int Id => metadata.Id;

    public string Name => metadata.Name;

    public int Count => metadata.Count;
}
