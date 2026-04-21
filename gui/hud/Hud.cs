using Godot;
using Godot.Collections;
using Game.component;
using Game.controller;
using Game.mutation;
using System.Linq;

public partial class Hud : CanvasLayer
{
    [Export]
    private AnimationPlayer animationPlayer;
    [Export]
    private SelectionPanel selectionPanel;

    private HealthComponent? trackedHealth;

    private MutationComponent? trackedMutations;

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

        if (selected.Count == 0)
        {
            animationPlayer.Play("selection_out");
            return;
        }

        animationPlayer.Play("selection_in");

        Array<Node> targets = [];
        foreach (var s in selected)
        {
            if (s.Target != null)
                targets.Add(s.Target);
        }
        selectionPanel.SetTracked(targets);

        /*
        else if (selected.Count == 1)
        {
            animationPlayer.Play("selection_in");

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

            trackedMutations = target.GetNodeOrNull<MutationComponent>("MutationComponent");

            if (trackedMutations != null)
            {
                trackedMutations.MutationsChanged += selectionPanel.OnMutationsChanged;
                selectionPanel.OnMutationsChanged(trackedMutations.Mutations);
            }
        }
        else
        {

        }
        */
    }

    private void OnSingleSelected(SelectableComponent selected)
    {
        var entityDataComponent = selected.GetNodeOrNull<EntityDataComponent>("EntityDataComponent");
        var healthComponent = selected.GetNodeOrNull<HealthComponent>("HealthComponent");

        selectionPanel.SetEntityData(entityDataComponent?.Data);

        if (healthComponent != null)
        {
            healthComponent.HealthChanged += selectionPanel.OnHealthChanged;
            selectionPanel.OnHealthChanged(healthComponent.Health, healthComponent.MaxHealth);
        }
    }

    private void OnMultipleSelected(Array<SelectableComponent> selected)
    {
        foreach (var s in selected)
        {
            var entityDataComponent = s.GetNodeOrNull<EntityDataComponent>("EntityDataComponent");
            var healthComponent = s.GetNodeOrNull<HealthComponent>("HealthComponent");

            selectionPanel.SetEntityData(entityDataComponent?.Data);

            if (healthComponent != null)
            {
                healthComponent.HealthChanged += selectionPanel.OnHealthChanged;
                selectionPanel.OnHealthChanged(healthComponent.Health, healthComponent.MaxHealth);
            }
        }
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
