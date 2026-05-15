using Godot;
using System;

public partial class MainMenu : Node
{
  private Button startButton => GetNode<Button>("Start");
  private Button optionsButton => GetNode<Button>("Options");
  private Button quitButton => GetNode<Button>("Quit");

  public override void _Ready()
  {
    startButton.Pressed += StartButtonPressed;
    optionsButton.Pressed += OptionsButtonPressed;
    quitButton.Pressed += QuitButtonPressed;
  }

  private void StartButtonPressed()
  {
    GetTree().ChangeSceneToFile("main.tscn");
  }

  private void OptionsButtonPressed()
  {

  }

  private void QuitButtonPressed() => GetTree().Quit();
}
