using Godot;

[GlobalClass]
public partial class RichMenuButton : Button
{
  private RichTextLabel textLabel => GetNode<RichTextLabel>("RichTextLabel");
  private AudioStreamPlayer audioPlayer => GetNode<AudioStreamPlayer>("AudioStreamPlayer");
  private string text;
  private Tween scaleTween;

  public override void _Ready()
  {
    text = textLabel.Text;

    MouseEntered += () =>
    {
      audioPlayer.Play();
      
      textLabel.Text = "[wave]" + text;

      if (scaleTween != null && scaleTween.IsRunning())
      {
        scaleTween.Kill();
      }
      scaleTween = CreateTween().SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Elastic);
      scaleTween.TweenProperty(this, "scale", new Vector2(1.1f, 1.1f), 0.5f);
    };

    MouseExited += () =>
    {
      textLabel.Text = text;

      if (scaleTween != null && scaleTween.IsRunning())
      {
        scaleTween.Kill();
      }
      scaleTween = CreateTween().SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Elastic);
      scaleTween.TweenProperty(this, "scale", Vector2.One, 0.55f);
    };
  }
}
