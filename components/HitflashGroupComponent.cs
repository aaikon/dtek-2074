using Godot;
using System;

namespace Game.component
{
  [GlobalClass]
  public partial class HitflashGroupComponent : CanvasGroup
  {
    [Export]
    private HealthComponent healthComponent;

    private ShaderMaterial shaderMaterial => Material as ShaderMaterial;

    public override void _Ready()
    {
      healthComponent.DamageTaken += OnDamageTaken;
    }

    private async void OnDamageTaken()
    {
      shaderMaterial.SetShaderParameter("hitflash_on", true);
      await ToSignal(GetTree().CreateTimer(0.2f), SceneTreeTimer.SignalName.Timeout);
      shaderMaterial.SetShaderParameter("hitflash_on", false);
    }
  }
}