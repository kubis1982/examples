namespace BlazorWebAssembly.Pages
{
    using System.Threading.Tasks;

    public partial class MyComponent
    {
        private int currentValue;
        private string? message;

        private void Increment()
        {
            message = string.Empty;
            currentValue++;
        }

        private void Decrement()
        {
            if (currentValue == 0) {
                message = "Próba ustawienia wartości poniżej zera";
                return;
            };            
            currentValue--;
        }

        protected override Task OnInitializedAsync()
        {
            currentValue += 10;

            return base.OnInitializedAsync();
        }
    }
}
