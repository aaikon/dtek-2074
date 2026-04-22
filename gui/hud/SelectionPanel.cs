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
}
