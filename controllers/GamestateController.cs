using Godot;

public partial class GamestateController : Node
{
  [Signal]
  public delegate void GoldChangedEventHandler(int newGold);

  public static GamestateController Instance { get; private set; } = null!;

  public int Gold { get; private set; } = 0;

  public override void _Ready()
  {
    Instance = this;
  }

  public void SetGold(int newGold)
  {
    Gold = newGold;
    EmitSignal(SignalName.GoldChanged, Gold);
  }

  public void AddGold(int gold)
  {
    Gold += gold;
    EmitSignal(SignalName.GoldChanged, Gold);
  }

  public void UseGold(int gold)
  {
    Gold -= gold;
    EmitSignal(SignalName.GoldChanged, Gold);
  }
}
