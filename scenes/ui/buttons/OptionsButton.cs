using Godot;
using System;

public enum MechConstructors
{
	Jishin,
	Durandal,
	Sgfrd,
	Minamoto
}

public enum OptionsButtonType
{
	Tier,
	Contructor,
	PartType
}

[Tool]
public partial class OptionsButton : Control
{
	OptionButton optionButton;
	Label label;
	[Export]
	string Label;
	[Export]
	OptionsButtonType type = OptionsButtonType.Tier;

	public delegate void OnOptionSelectedDelegate(string option, OptionsButtonType type);
	public event OnOptionSelectedDelegate OnOptionSelected;

	string[] currentOptions;
	public void SetType(OptionsButtonType newType)
	{
		switch (newType)
		{
			case OptionsButtonType.Tier:
				SetOptions(Enum.GetNames<MechPartTier>());
				break;
			case OptionsButtonType.Contructor:
				SetOptions(Enum.GetNames<MechConstructors>());
				break;
			case OptionsButtonType.PartType:
				SetOptions(Enum.GetNames<MechPartType>());
				break;
		}
	}

	public void SetLabel(string newLabel)
	{
		if (label != null)
		{
			label.Text = newLabel;
		}
	}

	public void SetSelected(string option)
	{
		int i = 0;
		foreach (string currentOption in currentOptions)
		{
			if (currentOption == option)
			{
				optionButton.Select(i);
			}
			i++;
		}

	}

	public void SetOptions(string[] options)
	{
		if (optionButton != null)
		{
			currentOptions = options;

			optionButton.Clear();
			foreach (string option in options)
			{
				optionButton.AddItem(option);
			}
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		optionButton = GetNodeOrNull<OptionButton>("MarginContainer2/OptionButton");
		label = GetNodeOrNull<Label>("LabelMargin/Label");
		if (optionButton != null)
		{
			optionButton.ItemSelected += OnItemSelected;
		}
		SetType(type);
		SetLabel(Label);
	}

	private void OnItemSelected(long index)
	{
		if (OnOptionSelected != null)
		{
			OnOptionSelected.Invoke(currentOptions[index], type);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
