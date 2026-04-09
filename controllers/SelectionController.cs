using Game.component;
using Godot;
using Godot.Collections;
using System.Collections.Generic;

namespace Game.controller
{
    public partial class SelectionController : Node
    {
        public static SelectionController Instance { get; private set; } = null!;

        [Signal]
        public delegate void SelectionChangedEventHandler(Array<SelectableComponent> selected);
        [Signal]
        public delegate void HoverChangedEventHandler(SelectableComponent? hovered);

        public SelectableComponent? Hovered { get; private set; }

        public List<SelectableComponent> Selected = new();

        private Vector2? dragStart;
        public Rect2 DragRect;
        public bool IsDragging { get; private set; }

        public override void _Ready()
        {
            Instance = this;
        }

        public override void _UnhandledInput(InputEvent @event)
        {
            if (@event is InputEventMouseButton mouseButton)
            {
                if (mouseButton.IsAction("select"))
                {
                    if (mouseButton.Pressed)
                    {
                        dragStart = mouseButton.GlobalPosition;
                        IsDragging = false;
                    }
                    else
                    {
                        if (IsDragging) 
                            FinalizeDragSelection();
                        else if (Hovered == null)
                            ClearSelection();
                        
                        dragStart = null;
                        IsDragging = false;
                        DragRect = default;
                    }
                }
            }

            if (@event is InputEventMouseMotion mouseMotion && dragStart.HasValue)
            {
                var delta = mouseMotion.GlobalPosition - dragStart.Value;
                if (!IsDragging && delta.Length() > 8f)
                    IsDragging = true;

                if (IsDragging)
                {
                    DragRect = new Rect2(dragStart.Value, mouseMotion.GlobalPosition - dragStart.Value).Abs();
                }
            }

            if (@event.IsActionPressed("select") && !IsDragging && Hovered == null)
                ClearSelection();
        }

        private void FinalizeDragSelection()
        {
            var found = new Array<SelectableComponent>();

            foreach (var node in GetTree().GetNodesInGroup("selectable"))
            {
                if (node is not SelectableComponent selectable) continue;
                if (DragRect.HasPoint(selectable.GetGlobalTransformWithCanvas().Origin))
                    found.Add(selectable);
            }

            if (found.Count > 0)
                SetSelection(found);
            else
                ClearSelection();
        }

        public void SetHover(SelectableComponent target)
        {
            if (Hovered == target) return;
            Hovered?.SetHovered(false);
            Hovered = target;
            Hovered.SetHovered(true);
            EmitSignal(SignalName.HoverChanged, target);
        }

        public void ClearHover(SelectableComponent caller)
        {
            if (Hovered != caller) return;
            Hovered.SetHovered(false);
            Hovered = null;
            EmitSignal(SignalName.HoverChanged, default(SelectableComponent));
        }

        public void SetSelection(SelectableComponent target)
        {
            ClearSelectionInternal();
            Selected.Add(target);
            target.SetSelected(true);
            EmitSignal(SignalName.SelectionChanged, ToArray());
        }

        public void SetSelection(Array<SelectableComponent> targets)
        {
            ClearSelectionInternal();
            foreach (var target in targets)
            {
                Selected.Add(target);
                target.SetSelected(true);
            }
            EmitSignal(SignalName.SelectionChanged, ToArray());
        }

        public void AddToSelection(SelectableComponent target)
        {
            if (Selected.Contains(target)) return;
            Selected.Add(target);
            target.SetSelected(true);
            EmitSignal(SignalName.SelectionChanged, ToArray());
        }

        public void ClearSelection()
        {
            ClearSelectionInternal();
            EmitSignal(SignalName.SelectionChanged, ToArray());
        }

        private void ClearSelectionInternal()
        {
            foreach (var s in Selected) s.SetSelected(false);
            Selected.Clear();
        }

        private Array<SelectableComponent> ToArray() => new(Selected);
    }
}
