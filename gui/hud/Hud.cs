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
        UnsubscribeFromCurrent();

        if (selected.Count == 0) {
            selectionPanel.Hide();
            return;
        }

        var target = selected[0].Target;

        var entityDataComponent = target.GetNodeOrNull<EntityDataComponent>("EntityDataComponent");

        selectionPanel.SetEntityData(entityDataComponent?.Data);

        trackedHealth = target.GetNodeOrNull<HealthComponent>("HealthComponent");

        if (trackedHealth != null)
        {
            trackedHealth.HealthChanged += selectionPanel.OnHealthChanged;
            selectionPanel.OnHealthChanged(trackedHealth.Health, trackedHealth.MaxHealth);
        } 
        else
        {
            selectionPanel.OnHealthChanged(0f, 0f);
        }

        selectionPanel.Show();
    }

    private void OnHoverChanged(SelectableComponent? hovered)
    {

    }

    private void UnsubscribeFromCurrent()
    {
        if (trackedHealth != null)
        {
            trackedHealth.HealthChanged -= selectionPanel.OnHealthChanged;
            trackedHealth = null;
        }
    }
}
