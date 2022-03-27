using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace CalculadoraParcelas.Windows.Core.ViewModels
{
    public class CalculatorViewModel : ObservableObject
    {
        private float _value;
        public float Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        private int _parcelaCount;
        public int ParcelaCount
        {
            get => _parcelaCount;
            set => SetProperty(ref _parcelaCount, value);
        }

        private float _result;
        public float Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }

        public ICommand CalculateCommand { get; }
        public ICommand EraseCommand { get; }

        public CalculatorViewModel()
        {
            Value = 1;
            ParcelaCount = 1;

            CalculateCommand = new RelayCommand(Calcular);
            EraseCommand = new RelayCommand(Apagar);
        }

        private void Apagar()
        {
            Result = 0;
        }

        public void Calcular()
        {
            Result = Value / ParcelaCount;
        }
    }
}
