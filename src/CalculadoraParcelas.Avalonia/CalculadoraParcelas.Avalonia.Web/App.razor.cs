using Avalonia.Web.Blazor;

namespace CalculadoraParcelas.Web
{
    public partial class App
    {
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            WebAppBuilder.Configure<CalculadoraParcelas.App>()
                .SetupWithSingleViewLifetime();
        }
    }
}