using Godot;

public partial class StatLabel : MarginContainer
{
	public Label StatName;
	public Label StatValue;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		StatName = GetNodeOrNull<Label>("HBoxContainer/StatName");
		StatValue = GetNodeOrNull<Label>("HBoxContainer/StatValue");
	}

	public void SetStat(string name, string value)
	{
		if (StatName != null)
			StatName.Text = name;
		if (StatValue != null)
			StatValue.Text = value;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
