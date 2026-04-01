using Godot;
using System;

public partial class StateMachine : GodotObject
{
  private Action currentState;

  public void Update()
  {
    currentState?.Invoke();
  }

  public void SetState(Action state)
  {
    currentState = state;
  }
}
