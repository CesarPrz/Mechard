using Godot;

public partial class MechViewer : MarginContainer
{
	public Mech CurrentMech;
	Label MechName;
	PartName Kernel;
	PartName SA;
	PartName SU;
	PartName SS;
	PartName SL;

	StatLabel IT;
	StatLabel PS;
	StatLabel PN;
	StatLabel AM;

	StatLabel Wins;
	StatLabel Losses;
	StatLabel AverageTurns;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Name
		MechName = GetNodeOrNull<Label>("Panel/ScrollContainer/Content/NameMargin/Name");
		// Parts 
		Kernel = GetNodeOrNull<PartName>("Panel/ScrollContainer/Content/Parts/Kernel");
		SA = GetNodeOrNull<PartName>("Panel/ScrollContainer/Content/Parts/SA");
		SU = GetNodeOrNull<PartName>("Panel/ScrollContainer/Content/Parts/SU");
		SS = GetNodeOrNull<PartName>("Panel/ScrollContainer/Content/Parts/SS");
		SL = GetNodeOrNull<PartName>("Panel/ScrollContainer/Content/Parts/SL");
		//Stats
		IT = GetNodeOrNull<StatLabel>("Panel/ScrollContainer/Content/Stats/IT");
		PS = GetNodeOrNull<StatLabel>("Panel/ScrollContainer/Content/Stats/PS");
		PN = GetNodeOrNull<StatLabel>("Panel/ScrollContainer/Content/Stats/PN");
		AM = GetNodeOrNull<StatLabel>("Panel/ScrollContainer/Content/Stats/AM");

		Wins = GetNodeOrNull<StatLabel>("Panel/ScrollContainer/Content/BattleResults/Wins");
		Losses = GetNodeOrNull<StatLabel>("Panel/ScrollContainer/Content/BattleResults/Losses");
		AverageTurns = GetNodeOrNull<StatLabel>("Panel/ScrollContainer/Content/BattleResults/AverageTurns");

	}

	public void SetMech(Mech mech)
	{
		CurrentMech = mech;
		MechName.Text = mech.name;

		Kernel?.SetPart(mech.Kernel);
		SA?.SetPart(mech.SA);
		SU?.SetPart(mech.SU);
		SS?.SetPart(mech.SS);
		SL?.SetPart(mech.SL);

		IT.SetStat("IT", mech.IT.ToString());
		PS.SetStat("PS", mech.PS.ToString());
		PN.SetStat("PN", mech.PN.ToString());
		AM.SetStat("AM", mech.AM.ToString());
		Wins.SetStat("Wins", mech.wins.ToString());
		Losses.SetStat("Losses", mech.losses.ToString());
		AverageTurns.SetStat("Average Turns", mech.averageTurns.ToString());
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
