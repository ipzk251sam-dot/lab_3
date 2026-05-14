using System.Collections.Generic;

public class TagMetadata
{
    public string TagName { get; }
    public string DisplayType { get; }
    public string ClosingType { get; }

    public TagMetadata(string tagName, string displayType, string closingType)
    {
        TagName = tagName; DisplayType = displayType; ClosingType = closingType;
    }
}

public class FlyweightFactory
{
    private Dictionary<string, TagMetadata> _tags = new Dictionary<string, TagMetadata>();

    public TagMetadata GetTagMetadata(string tagName)
    {
        if (!_tags.ContainsKey(tagName))
        {
            string displayType = (tagName == "h1" || tagName == "h2" || tagName == "p" || tagName == "blockquote") ? "block" : "inline";
            _tags[tagName] = new TagMetadata(tagName, displayType, "paired");
        }
        return _tags[tagName];
    }
}

public class LightElementNodeFlyweight : LightNode
{
    private TagMetadata _metadata;
    public List<LightNode> Children { get; set; } = new List<LightNode>();

    public LightElementNodeFlyweight(TagMetadata metadata)
    {
        _metadata = metadata;
    }

    public void Add(LightNode node) => Children.Add(node);

    public override string InnerHTML()
    {
        string html = "";
        foreach (var child in Children) html += child.OuterHTML();
        return html;
    }

    public override string OuterHTML()
    {
        string openTag = $"<{_metadata.TagName}>";
        if (_metadata.ClosingType == "single") return openTag;
        return $"{openTag}{InnerHTML()}</{_metadata.TagName}>";
    }
}