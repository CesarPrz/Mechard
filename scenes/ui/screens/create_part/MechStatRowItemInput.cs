using Godot;

[Tool]
public partial class MechStatRowItemInput : HBoxContainer
{
	[Export]
	public string Label
	{
		get { return GetNode<Label>("LabelMargin/Label").Text; }
		set
		{
			Label label = GetNodeOrNull<Label>("LabelMargin/Label");
			if (label != null)
			{
				label.Text = value;
			}
		}
	}

	public void SetStatValue(string value)
	{
		GetNode<LineEdit>("InputMargin/LineEdit").Text = value;
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
