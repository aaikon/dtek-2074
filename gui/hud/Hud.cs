using Godot;
using Godot.Collections;
using Game.component;
using Game.controller;

public partial class Hud : CanvasLayer
{
    [Export]
    private SelectionPanel selectionPanel;

    private HealthComponent? trackedHealth;

    public override void _Ready()
    {
        SelectionController.Instance.SelectionChanged += OnSelectionChanged;
        SelectionController.Instance.HoverChanged += OnHoverChanged;
    }

    public override void _ExitTree()
    {
        SelectionController.Instance.SelectionChanged -= OnSelectionChanged;
        SelectionController.Instance.HoverChanged -= OnHoverChanged;
    }

    private void OnSelectionChanged(Array<SelectableComponent> selected)
    {
        if (trackedHealth != null)
        {
            trackedHealth.HealthChanged -= selectionPanel.OnHealthChanged;
            trackedHealth = null;
        }

        if (selected.Count == 0) {
            Hide();
            return;
        }

        var target = selected[0].Target;

        selectionPanel.SetNameLabelText(target.Name);

        trackedHealth = target.GetNodeOrNull<HealthComponent>("HealthComponent");

        if (trackedHealth != null)
        {
            trackedHealth.HealthChanged += selectionPanel.OnHealthChanged;
            selectionPanel.OnHealthChanged(trackedHealth.Health, trackedHealth.MaxHealth);
            GD.Print($"Health values: {trackedHealth.Health} / {trackedHealth.MaxHealth}");
        }

        Show();
    }

    private void OnHoverChanged(SelectableComponent? hovered)
    {

    }
}
