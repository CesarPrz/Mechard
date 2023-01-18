using Godot;

[Tool]
public partial class TButton : MarginContainer
{
    public Button Button;

    [Export]
    public string Text;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Button = GetNodeOrNull<Button>("PrimaryButton");
        Button.Text = Text;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
