using Microsoft.AspNetCore.Components;

namespace BlazorConfig.Components.Navbar
{
    public interface ILinkItem
    {
        public RenderFragment? Content { get; set; }
    }
}


