using Godot;
using System;
using Godot.Collections;

[GlobalClass]
public partial class DynamicCamera : Camera2D
{
  [Export]
  private Array<Node2D> targets = new();
  [Export]
  private float padding = 0f;
  [Export]
  private float minZoom;
  [Export]
  private float maxZoom;

  private Vector2? zoomOverride;

  public override void _Process(double delta)
  {
    if (targets.Count == 0) return;
    var bounds = GetBounds();
    var targetPosition = bounds.GetCenter();

    if (zoomOverride.HasValue)
    {
      var manualZoom = zoomOverride.Value.X;
      var requiredZoom = GetZoom(bounds).X;
      if (requiredZoom < manualZoom)
        zoomOverride = null;
    }

    var targetzoom = zoomOverride ?? GetZoom(bounds);
    Position = Position.Lerp(targetPosition, 0.1f);
    Zoom = Zoom.Lerp(targetzoom, 0.1f);
  }

  public override void _UnhandledInput(InputEvent @event)
  {
    if (@event.IsActionPressed("zoom_in") || @event.IsActionPressed("zoom_out"))
    {
      zoomOverride ??= GetZoom(GetBounds());

      bool zoomingIn = @event.IsActionPressed("zoom_in");
      var factor = zoomingIn ? 1.1f : 0.9f;
      var rawZoom = zoomOverride.Value.X * factor;
      var snapped = SnapPixelPerfect(Mathf.Clamp(rawZoom, minZoom, maxZoom), zoomingIn);
      zoomOverride = Vector2.One * snapped;

      GetViewport().SetInputAsHandled();
    }
  }

  private Rect2 GetBounds()
  {
    Rect2 bounds = new Rect2(targets[0].GlobalPosition, Vector2.Zero);
    foreach (var target in targets)
      bounds = bounds.Expand(target.GlobalPosition);
    return bounds.Grow(padding);
  }

  private Vector2 GetZoom(Rect2 bounds)
  {
    var viewport = GetViewportRect().Size;
    var zoomX = viewport.X / bounds.Size.X;
    var zoomY = viewport.Y / bounds.Size.Y;
    var zoom = Mathf.Clamp(Mathf.Min(zoomX, zoomY), minZoom, maxZoom);
    return Vector2.One * SnapPixelPerfect(zoom, false);
  }

  private float SnapPixelPerfect(float zoom, bool zoomingIn)
  {
    if (zoom >= 1f)
      return zoomingIn ? Mathf.Ceil(zoom) : Mathf.Floor(zoom);
    else
      return 1f / (zoomingIn ? Mathf.Floor(1f / zoom) : Mathf.Ceil(1f / zoom));
  }
}