using System;
using Game.component;
using Game.mutation;
using Godot;
using Godot.Collections;


public partial class SelectionPanel : Control
{
    [Export]
    private Label nameLabel;
    [Export]
    private TextureRect portraitRect;
    [Export]
    private TextureProgressBar healthBar;
    [Export]
    private Label healthLabel;
    [Export]
    private GridContainer mutationList;

    private Dictionary<Node, TrackedEntityData> trackedEntities = [];

    public void SetEntityData(EntityData? data)
    {
        nameLabel.Text = data?.Name ?? "";
        portraitRect.Texture = data?.Portrait;
    }

    public void OnHealthChanged(float health, float maxHealth)
    {
        healthBar.MaxValue = maxHealth;
        healthBar.Value = health;

        healthLabel.Text = health + "/" + maxHealth;
    }

    public void OnMutationsChanged(Array<Mutation> mutations)
    {
    }

    private partial class TrackedEntityData(EntityData? entityData, float? health, float? maxHealth) : GodotObject
    {

    }

    public void SetTracked(Array<Node> targets)
    {
        foreach (var t in targets)
        {
            var entityDataComponent = t.GetNodeOrNull<EntityDataComponent>("EntityDataComponent");
            var healthComponent = t.GetNodeOrNull<HealthComponent>("HealthComponent");

            if (healthComponent != null)
            {
                healthComponent.HealthChanged += selectionPanel.OnHealthChanged;
                selectionPanel.OnHealthChanged(healthComponent.Health, healthComponent.MaxHealth);
            }
            trackedEntities.Add(
                t,
                new TrackedEntityData(
                    entityDataComponent?.Data ?? null,
                    healthComponent?.Health ?? null,
                    healthComponent?.MaxHealth ?? null
                )
            );
        }
    }
}
