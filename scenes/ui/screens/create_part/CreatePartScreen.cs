using Godot;

public partial class CreatePartScreen : Control
{
	MechPartsList MechPartsList;
	MechPartEditor MechPartEditor;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		MechPartsList = GetNodeOrNull<MechPartsList>("PanelContainer/VBoxContainer/ScreenContent/MechPartsListMargin");
		MechPartEditor = GetNodeOrNull<MechPartEditor>("PanelContainer/VBoxContainer/ScreenContent/MechPartViewerMargin");
		if (MechPartsList != null)
		{
			MechPartsList.AddPart(new MechPart());
			MechPartsList.PartSelected += OnPartSelected;
		}
	}

	private void OnPartSelected(MechPart part)
	{
		MechPartEditor.LoadMechPart(part);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
