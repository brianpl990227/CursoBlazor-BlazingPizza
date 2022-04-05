using Microsoft.JSInterop;

namespace BlazingPizza.UI.Client.Extensions
{
    public static class JSRuntimeExtensions
    {
        public static ValueTask<bool> Confirm(this IJSRuntime jSRuntime, string message)
        {
            return jSRuntime.InvokeAsync<bool>("confirm", message);
        }
    }
}
