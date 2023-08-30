using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Catalog.Extension
{
    [HtmlTargetElement("*", Attributes = "type-button, route-id")]
    public class BotaoTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public BotaoTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        [HtmlAttributeName("type-button")]
        public TypeButton TypeButtonSelect { get; set; }
        [HtmlAttributeName("route-id")]
        public int RouteId { get; set; }

        private string? nameAction;
        private string? nameClass;
        private string? spanIcon;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            switch (TypeButtonSelect)
            {
                case TypeButton.Details:
                    nameAction = "Details";
                    nameClass = "btn btn-info";
                    spanIcon = "fa fa-search";
                    break;
                case TypeButton.Edit:
                    nameAction = "Edit";
                    nameClass = "btn btn-warning";
                    spanIcon = "fa fa-pencil-alt";
                    break;
                case TypeButton.Delete:
                    nameAction = "Delete";
                    nameClass = "btn btn-danger";
                    spanIcon = "fa fa-trash";
                    break;
            }

            var controller = _contextAccessor.HttpContext?.GetRouteData().Values["controller"]?.ToString();

            output.TagName = "a";
            output.Attributes.SetAttribute("href", $"{controller}/{nameAction}/{RouteId}");
            output.Attributes.SetAttribute("class", nameClass);

            var iconSpan = new TagBuilder("span");
            iconSpan.AddCssClass(spanIcon);

            output.Content.AppendHtml(iconSpan);
        }
    }
    public enum TypeButton
    {
        Details = 1,
        Edit,
        Delete
    }
}