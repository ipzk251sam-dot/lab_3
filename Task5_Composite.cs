using System.Collections.Generic;
using System.Text;

public abstract class LightNode
{
    public abstract string OuterHTML();
    public abstract string InnerHTML();
}

public class LightTextNode : LightNode
{
    private string _text;
    public LightTextNode(string text) { _text = text; }
    public override string OuterHTML() => _text;
    public override string InnerHTML() => _text;
}

public class LightElementNode : LightNode
{
    public string TagName { get; set; }
    public string DisplayType { get; set; }
    public string ClosingType { get; set; }
    public List<string> CssClasses { get; set; } = new List<string>();
    public List<LightNode> Children { get; set; } = new List<LightNode>();

    public LightElementNode(string tagName, string displayType, string closingType)
    {
        TagName = tagName;
        DisplayType = displayType;
        ClosingType = closingType;
    }

    public void Add(LightNode node) => Children.Add(node);

    public override string InnerHTML()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var child in Children)
        {
            sb.Append(child.OuterHTML());
        }
        return sb.ToString();
    }

    public override string OuterHTML()
    {
        string classes = CssClasses.Count > 0 ? $" class=\"{string.Join(" ", CssClasses)}\"" : "";
        string openTag = $"<{TagName}{classes}>";

        if (ClosingType == "single") return openTag;
        
        return $"{openTag}{InnerHTML()}</{TagName}>";
    }
}