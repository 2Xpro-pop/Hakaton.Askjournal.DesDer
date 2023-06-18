namespace Destinationosh.Models;
public class NavNode
{
    public Guid Id
    {
        get; set;
    }
    public string KgName
    {
        get; set;
    }
    public string RuName
    {
        get; set;
    }
    public string EnName
    {
        get; set;
    }
    public string SubPath { get; set; } = null!;
    public bool IsHide
    {
        get; set;
    } = false;
    public NavNode? this[string subPath]
    {
        get
        {
            if (subPath == null)
                return this;
            return Children.FirstOrDefault(x => string.Equals(x.SubPath, subPath, StringComparison.OrdinalIgnoreCase));
        }
    }
    public IList<NavNode> Children { get; set; } = new List<NavNode>();

    public NavNode? TryFind(Guid guid)
    {
        if (Id == guid)
            return this;
        foreach (var child in Children)
        {
            var result = child.TryFind(guid);
            if (result != null)
                return result;
        }
        return null;
    }

    public string GetPathTo(Guid guid)
    {
        var result = "";
        if (Id == guid)
        {
            return SubPath;
        }
        foreach (var child in Children)
        {
            var path = child.GetPathTo(guid);
            if (!string.IsNullOrWhiteSpace(path))
            {
                return SubPath + "/" + path;
            }
        }
        return result;
    }

    public bool Remove(NavNode node)
    {
        if (Children.Remove(node))
        {
            return true;
        }
        else
        {
            foreach (var child in Children)
            {
                if (child.Remove(node))
                    return true;
            }
        }
        return false;
    }
}