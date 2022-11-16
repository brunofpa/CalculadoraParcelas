using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CalculadoraParcelas.Windows.Core.ViewModels
{
    public partial class CalculatorViewModel : ObservableObject
    {
        [ObservableProperty]
        private float _value;

        [ObservableProperty]
        private int _parcelaCount;

        [ObservableProperty]
        private float _result;

        public CalculatorViewModel()
        {
            Value = 1;
            ParcelaCount = 1;
        }

        [RelayCommand]
        private void Erase()
        {
            Result = 0;
        }

        [RelayCommand]
        public void Calculate()
        {
            Result = Value / ParcelaCount;
        }
    }
}
