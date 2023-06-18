using Newtonsoft.Json.Linq;
using RazorEngineCore;

namespace Destinationosh.Services;

public interface IBlockConverter
{
    string Name { get; }
    string Convert(string json);

    public static IRazorEngine RazorEngine { get; }

    static IBlockConverter()
    {
        RazorEngine = new RazorEngine();
    }

    public static string? AlignmentStyle(string? alignment)
    {
        if(alignment == null)
        {
            return null;
        } 
        var jObject = JObject.Parse(alignment);
        var direction = jObject["tunes"]["alignment"]["alignment"].Value<string>();
        switch (direction)
        {
            case "left":
               return "text-left";
            case "center":
               return "text-center";
            case "right": 
               return "text-right";
            default:
                return null;
        }
    }
}
