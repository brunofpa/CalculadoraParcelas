using CalculadoraParcelas.Windows.Core.ViewModels;

namespace CalculadoraParcelas.Blazor.Pages
{
    public partial class Index
    {
        private CalculatorViewModel? Vm;

        protected override void OnInitialized()
        {
            Vm = new CalculatorViewModel();
            Vm.PropertyChanged += (s, e) => StateHasChanged();
        }
    }
}
