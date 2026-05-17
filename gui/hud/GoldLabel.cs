using Godot;
using System;

public partial class GoldLabel : RichTextLabel
{
  public override void _Ready()
  {
    GamestateController.Instance.GoldChanged += (newGold) => Text = $"[img]res://assets/sGoldIcon.png[/img] {newGold}";
  }
}
