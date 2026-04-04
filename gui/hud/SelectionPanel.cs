using Godot;

public partial class SelectionPanel : Control
{
    [Export]
    private TextureRect portraitRect;
    [Export]
    private Label nameLabel;
    [Export]
    private ProgressBar healthBar;

    public void SetPortraitRectTexture(Texture2D texture)
    {
        portraitRect.Texture = texture;
    }

    public void SetNameLabelText(string name)
    {
        nameLabel.Text = name;
    }

    public void OnHealthChanged(float health, float maxHealth)
    {
        healthBar.MaxValue = maxHealth;
        healthBar.Value = health;
    }
}
