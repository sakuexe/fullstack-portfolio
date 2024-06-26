using System.Text.RegularExpressions;

namespace fullstack_portfolio.Utils;

public static class SvgUtils
{
    public static string? SanitizeSvg(string path)
    {
        string svgCode;
        try {
            svgCode = File.ReadAllText(path);
        } catch (Exception e) {
            Console.WriteLine(e.Message);
            return null;
        }
        if (svgCode == null)
            return null;
        svgCode = RemoveFill(svgCode);
        svgCode = AddFullWidthHeight(svgCode);
        svgCode = RemoveInlineFill(svgCode);
        return svgCode;
    }

    public static string GetSvgPath(string link)
    {
        string brand = LinkUtils.GetBrand(link);
        return $"wwwroot/icons/{brand}.svg";
    }

    public static string RemoveFill(string svg)
    {
        // matches all fill properties and the values in them
        // also include the optional trailing whitespace
        string findFillPattern = @"(fill=\""#?\w*""\s?)";
        Regex rg = new Regex(findFillPattern);
        svg = rg.Replace(svg, "");
        return svg;
    }

    public static string AddFullWidthHeight(string svg)
    {
        // matches width and height properties, first group is the name
        // second group is the actual numeric value
        string FindWidthAndHeightPattern = @"(width|height)=""\d+\S+""";
        // builds a new property using the first match group
        // as the name
        string replacePattern = "$1=\"100%\"";
        Regex rg = new Regex(FindWidthAndHeightPattern);
        return rg.Replace(svg, replacePattern);
    }

    public static string RemoveInlineFill(string svg)
    {
        string findFillPattern = @"fill:#?\w+;\s";
        Regex rg = new Regex(findFillPattern);
        svg = rg.Replace(svg, "");
        return svg;
    }
}
