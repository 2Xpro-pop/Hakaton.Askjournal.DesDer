using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesDer.Models;

public class Post
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    public string? Ru { get; set; }
    public string? En { get; set; }
    public string? Kg { get; set; }
    public string? RuHeader { get; set; }
    public string? EnHeader { get; set; }
    public string? KgHeader { get; set; }
    public string? RuDescription { get; set; }
    public string? EnDescription { get; set; }
    public string? KgDescription { get; set; }
    public string UserId { get; set; }
    public virtual User? User { get; set; }
    public string? PathGuid { get; set; }

}
