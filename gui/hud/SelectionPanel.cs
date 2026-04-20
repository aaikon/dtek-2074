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

    private Array<TrackedEntityData> trackedEntities = [];

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

    private partial class TrackedEntityData : GodotObject
    {
        public Node entity;
        public float health;
        public float maxHealth;
    }

    public void SetTracked(Array<SelectableComponent> selected)
    {
        foreach (var s in selected)
        {
            var target = s.Target;

            trackedEntities.Add()
        }
    }
}
