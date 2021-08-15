using Godot;
using System;

[Tool]
public class QueueContainer : Container
{
    private void CustomFitChildInRect()
    {
        var childs = this.GetChildren();
        for (int i = 0; i < childs.Count; i++)
        {
            var child = (Control)childs[i];
            var size = child.RectMinSize;
            var w = size.x;
            var h = size.y;
            var itemPadding = h * 0.05f;
            var offsetTop = RectSize.y - h - i * (h * 0.5f + itemPadding) + itemPadding;
            var offsetLeft = i % 2 != 0 ? w / 2 + itemPadding : 0;

            FitChildInRect(child, new Rect2(
                offsetLeft, offsetTop,
                w, h
            ));
        }
    }

    public override void _Notification(int what)
    {
        if (what == NotificationSortChildren)
        {
            CustomFitChildInRect();
        }
    }
}
