namespace BlazorWebAssembly.Pages
{
    using BlazorBootstrap;
    using Microsoft.AspNetCore.Components;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    public partial class MyComponent
    {
        private ElementReference textInput;

        [Parameter]
        public int CurrentValue { get; set; }

        [Parameter]
        public int Value { get; set; } = 1;

        [Parameter]
        public EventCallback<int> CurrentValueChanged { get; set; }

        private string? message;

        private async Task Increment()
        {
            message = string.Empty;

            CurrentValue+=Value;

            await CurrentValueChanged.InvokeAsync(CurrentValue);
        }

        private async Task Decrement()
        {
            if (CurrentValue == 0) {
                message = "Próba ustawienia wartości poniżej zera";
                return;
            };            
            CurrentValue -= Value;

            await CurrentValueChanged.InvokeAsync(CurrentValue);
        }

        [Parameter]
        public RenderFragment? ChildComponent { get; set; }
    }
}
