using Godot;
using System;
using Game.controller;

public partial class SelectionBox : Control
{

    [Export]
    private Color outline = new Color(0.2f, 0.6f, 1f, 0.8f);
    [Export]
    private Color fill = new Color(0.2f, 0.6f, 1f, 0.15f);

    public override void _Process(double delta)
    {
        QueueRedraw();
    }

    public override void _Draw()
    {
        if (!SelectionController.Instance.IsDragging) return;
        var rect = SelectionController.Instance.DragRect;
        DrawRect(rect, fill, filled: true);
        DrawRect(rect, outline, filled: false, width: 1f);
    }
}
