using Godot;

public partial class MainMenu : Control
{
	TButton createParts;
	TButton battleMechs;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		createParts = GetNodeOrNull<TButton>("Row/MarginContainer2/VBoxContainer/CreateParts");
		battleMechs = GetNodeOrNull<TButton>("Row/MarginContainer2/VBoxContainer/FightMechs");

		if (createParts != null && createParts.Button != null)
			createParts.Button.Pressed += CreatePartsPressed;
		if (battleMechs != null && battleMechs.Button != null)
			battleMechs.Button.Pressed += FightMechsPressed;
	}


	private void FightMechsPressed()
	{
		GD.Print("Fight Mechs Pressed!");
		_ = GetTree().ChangeSceneToFile("res://scenes/ui/screens/fight_screen/fight_screen.tscn");
	}

	private void CreatePartsPressed()
	{
		GD.Print("Create Parts Pressed!");
		_ = GetTree().ChangeSceneToFile("res://scenes/ui/screens/create_part/create_part_screen.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
