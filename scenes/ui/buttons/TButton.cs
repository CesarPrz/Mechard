using Godot;

[Tool]
public partial class TButton : MarginContainer
{
    [Export]
    public string Text
    {
        get { return GetNode<Button>("PrimaryButton").Text; }
        set
        {
            Button button = GetNodeOrNull<Button>("PrimaryButton");
            if (button != null)
            {
                button.Text = value;
            }
        }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
