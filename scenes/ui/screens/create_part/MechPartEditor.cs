using Godot;
using System;

public partial class MechPartEditor : MarginContainer
{
	MechPart CurrentMechPart = null;
	Label name;
	MechStatRowItemInput IT;
	MechStatRowItemInput AM;
	MechStatRowItemInput PS;
	MechStatRowItemInput PN;
	OptionsButton partType;
	OptionsButton constructor;
	OptionsButton tier;


	public delegate void OnPartChangedDelegate(MechPart part);
	public event OnPartChangedDelegate OnPartChanged;

	public void LoadMechPart(MechPart part)
	{
		CurrentMechPart = part;
		name.Text = part.Name;
		IT.SetStatValue(part.IT.ToString());
		AM.SetStatValue(part.AM.ToString());
		PS.SetStatValue(part.PS.ToString());
		PN.SetStatValue(part.PN.ToString());
		partType.SetSelected(part.PartType.ToString());
		constructor.SetSelected(part.Constructors.ToString());
		tier.SetSelected(part.Tier.ToString());
	}



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		name = GetNode<Label>("MechPartViewerPanel/Row/PartName/Label");
		IT = GetNode<MechStatRowItemInput>("MechPartViewerPanel/Row/IT");
		AM = GetNode<MechStatRowItemInput>("MechPartViewerPanel/Row/AM");
		PS = GetNode<MechStatRowItemInput>("MechPartViewerPanel/Row/PS");
		PN = GetNode<MechStatRowItemInput>("MechPartViewerPanel/Row/PN");
		partType = GetNode<OptionsButton>("MechPartViewerPanel/Row/Part");
		constructor = GetNode<OptionsButton>("MechPartViewerPanel/Row/Constructor");
		tier = GetNode<OptionsButton>("MechPartViewerPanel/Row/Tier");

		IT.OnValueChanged += IT_OnValueChanged;
		AM.OnValueChanged += AM_OnValueChanged;
		PS.OnValueChanged += PS_OnValueChanged;
		PN.OnValueChanged += PN_OnValueChanged;
		partType.OnOptionSelected += OnOptionButtonChanged;
		constructor.OnOptionSelected += OnOptionButtonChanged;
		tier.OnOptionSelected += OnOptionButtonChanged;
	}

	private void PN_OnValueChanged(string value)
	{
		CurrentMechPart.PN = Convert.ToInt32(value);
		if (OnPartChanged != null)
		{
			OnPartChanged(CurrentMechPart);
		}
	}

	private void PS_OnValueChanged(string value)
	{
		CurrentMechPart.PS = Convert.ToInt32(value);
		if (OnPartChanged != null)
		{
			OnPartChanged(CurrentMechPart);
		}
	}

	private void AM_OnValueChanged(string value)
	{
		CurrentMechPart.AM = Convert.ToInt32(value);
		if (OnPartChanged != null)
		{
			OnPartChanged(CurrentMechPart);
		}
	}

	private void IT_OnValueChanged(string value)
	{
		CurrentMechPart.IT = Convert.ToInt32(value);
		if (OnPartChanged != null)
		{
			OnPartChanged(CurrentMechPart);
		}
	}

	private void OnOptionButtonChanged(string option, OptionsButtonType type)
	{
		switch (type)
		{
			case OptionsButtonType.Tier:
				CurrentMechPart.Tier = Enum.Parse<MechPartTier>(option);
				break;
			case OptionsButtonType.Contructor:
				CurrentMechPart.Constructors = Enum.Parse<MechConstructors>(option);
				break;
			case OptionsButtonType.PartType:
				CurrentMechPart.PartType = Enum.Parse<MechPartType>(option);
				break;
		}
		name.Text = CurrentMechPart.Name;

		if (OnPartChanged != null)
		{
			OnPartChanged.Invoke(CurrentMechPart);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
