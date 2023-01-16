using Godot;

public partial class MechPartEditor : MarginContainer
{
	MechPart CurrentMechPart = null;
	LineEdit name;
	MechStatRowItemInput IT;
	MechStatRowItemInput AM;
	MechStatRowItemInput PS;
	MechStatRowItemInput PN;

	public void LoadMechPart(MechPart part)
	{
		name.Text = part.Name;
		IT.SetStatValue(part.IT.ToString());
		AM.SetStatValue(part.AM.ToString());
		PS.SetStatValue(part.PS.ToString());
		PN.SetStatValue(part.PN.ToString());
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		name = GetNode<LineEdit>("MechPartViewerPanel/Row/PartName/LineEdit");
		IT = GetNode<MechStatRowItemInput>("MechPartViewerPanel/Row/IT");
		AM = GetNode<MechStatRowItemInput>("MechPartViewerPanel/Row/AM");
		PS = GetNode<MechStatRowItemInput>("MechPartViewerPanel/Row/PS");
		PN = GetNode<MechStatRowItemInput>("MechPartViewerPanel/Row/PN");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
