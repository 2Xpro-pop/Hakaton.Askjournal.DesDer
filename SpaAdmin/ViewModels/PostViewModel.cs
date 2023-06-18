using Destinationosh.Models;

namespace Destinationosh.SpaAdmin.ViewModels;

public class PostViewModel
{
    public string Route { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string EnHeader { get; set; }
    public string RuHeader { get; set; }
    public string KgHeader { get; set; }
    public string DescriptionRu { get; set; }
    public string DescriptionEn { get; set; }
    public string DescriptionKg { get; set; }

    public static implicit operator PostViewModel(Post post)
    {
        return new PostViewModel()
        {
            Route = post.PathGuid,
            Name = post.Name,
            EnHeader = post.EnHeader,
            RuHeader = post.RuHeader,
            KgHeader = post.KgHeader,
            DescriptionRu = post.RuDescription,
            DescriptionEn = post.EnDescription,
            DescriptionKg = post.KgDescription,
        };
    } 
}