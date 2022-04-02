using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingPizza.UI.ComponentsLibrary
{
    public static class LocalStorage
    {
        public static ValueTask<T> GetAsync<T>(IJSRuntime jSRuntime, string key) =>
            jSRuntime.InvokeAsync<T>("blazorLocalStorage.get", key);

        public static ValueTask SetAsync(IJSRuntime jSRuntime, string key, object value) =>
            jSRuntime.InvokeVoidAsync("blazorLocalStorage.set", key, value);

        public static ValueTask DeleteAsync(IJSRuntime jSRuntime, string key) =>
            jSRuntime.InvokeVoidAsync("blazorLocalStorage.delete", key);

    }
}
