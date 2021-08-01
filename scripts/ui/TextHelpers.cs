public static class TextHelpers
{
    public static string GetIconBBCode(string icon, int size)
    {
        return GetIconBBCode(icon, size, size);
    }
    public static string GetIconBBCode(string icon, int width = 22, int height = 22)
    {
        var path = $"res://assets/graphics/icons/Icon.{icon}.png";
        var img = $"[img={width}x{height}]{path}[/img]";
        var icon_font = "res://prefabs/fonts/dynamic_fonts/IconFont.tres";
        var with_offset = $"[font={icon_font}]{img}[/font]";
        return with_offset;
    }
}