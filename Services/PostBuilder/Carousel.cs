using System.Text;
using Newtonsoft.Json.Linq;
using RazorEngineCore;

namespace Destinationosh.Services;

public class Carousel : IBlockConverter
{
    const string template = @"

<div class=""post-block @(Model.Stretched ? ""stretched"" : """")"">
    <div class=""post-block-content"">
        <div id=""@Model.Id"" class=""carousel slide"" data-bs-ride=""carousel"">
            <div class=""carousel-indicators"">
                @for (int i = 0; i < Model.Items.Length; i++)
                {
                    var active = (i == 0 ? ""active"" : """");
                    <button type=""button"" data-bs-target=""@Model.Id"" data-bs-slide-to=""@i"" class=""@active"" />
                }
            </div>
            <div class=""carousel-inner"">
                @for (int i = 0; i < Model.Items.Length; i++)
                {
                    var item = Model.Items[i];
                    var active = (i == 0 ? ""active"" : """");
                    <div class=""carousel-item @active"">
                        <img src=""@item.Url"" class=""d-block"" alt=""@item.Caption.Header"" />
                        <div class=""carousel-caption"">
                            <h5>@item.Caption.Header</h5>
                            <p>@item.Caption.Text</p>
                        </div>
                        @if (!string.IsNullOrWhiteSpace(item.Caption.Button.Text))
                        {
                            <div class=""side-btn"">
                                <a class=""btn  btn-dark"" href=""@item.Caption.Button.Url"">@item.Caption.Button.Text</a>
                            </div>
                        }
                    </div>
                }
            </div>
            <button type=""button"" class=""carousel-control-prev"" data-bs-slide=""prev"" data-bs-target=""#@Model.Id""><span
                    class=""carousel-control-prev-icon""></span></button>
            <button type=""button"" class=""carousel-control-next"" data-bs-slide=""next"" data-bs-target=""#@Model.Id""><span
                    class=""carousel-control-next-icon""></span></button>
        </div>
    </div>
</div>
    ";
    private static readonly IRazorEngineCompiledTemplate _razorTemplate;

    static Carousel()
    {
        _razorTemplate = IBlockConverter.RazorEngine.Compile(template);
    }

    public string Name => nameof(Carousel);

    public string Convert(string json)
    {
        var jObject = JObject.Parse(json);

        var viewModel = jObject["data"]?.ToObject<CarouseViewModel>() ?? throw new ArgumentException("Invalid json");
        viewModel.Id = "id"+Guid.NewGuid().ToString();

        return _razorTemplate.Run(viewModel);;
    }
    public class CarouseViewModel
    {
        public CarouselItem[] Items { get; set; }
        public string Id { get; set;} 
        public bool Stretched { get; set; }
    }

    public class CarouselItem
    {
        public string Url { get; set; }
        public Caption Caption { get; set; } 
    }

    public class Caption
    {
        public string Header { get; set; }
        public string Text { get; set; }
        public Button Button { get; set; }
    }

    public class Button
    {
        public string? Url { get; set; }
        public string? Text { get; set; }
    }

}
