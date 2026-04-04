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

        private readonly List<SelectableComponent> _selected = new();
        public IReadOnlyList<SelectableComponent> Selected => _selected;

        public override void _Ready()
        {
            Instance = this;
        }

        public override void _UnhandledInput(InputEvent @event)
        {
            if (!@event.IsActionPressed("select")) return;
            if (Hovered == null) ClearSelection();
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
            _selected.Add(target);
            target.SetSelected(true);
            EmitSignal(SignalName.SelectionChanged, ToArray());
        }

        public void AddToSelection(SelectableComponent target)
        {
            if (_selected.Contains(target)) return;
            _selected.Add(target);
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
            foreach (var s in _selected) s.SetSelected(false);
            _selected.Clear();
        }

        private Array<SelectableComponent> ToArray() => new(_selected);
    }
}
