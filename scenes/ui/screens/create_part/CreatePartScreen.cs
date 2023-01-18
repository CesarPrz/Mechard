using Godot;
using System.Collections.Generic;
using System.IO;
using System.Text;

public partial class CreatePartScreen : Control
{
	TButton SaveButton;
	MechPartsList MechPartsList;
	MechPartEditor MechPartEditor;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		MechPartsList = GetNodeOrNull<MechPartsList>("PanelContainer/VBoxContainer/ScreenContent/MechPartsListMargin");
		MechPartEditor = GetNodeOrNull<MechPartEditor>("PanelContainer/VBoxContainer/ScreenContent/MechPartViewerMargin");
		SaveButton = GetNodeOrNull<TButton>("PanelContainer/VBoxContainer/BottomLayout/SaveButton");
		if (SaveButton != null)
			SaveButton.Button.Pressed += Button_Pressed;
		if (MechPartsList != null)
		{
			MechPartsList.PartSelected += OnPartSelected;
			MechPartEditor.OnPartChanged += OnPartChanged;
		}
	}

	private void Button_Pressed()
	{
		SaveParts();
	}

	public void SaveParts()
	{
		FileStream fs = File.Open("savegame.save", FileMode.Create);
		foreach (KeyValuePair<int, MechPart> part in MechPartsList.parts)
		{
			Godot.Collections.Dictionary<string, string> keyValuePairs = part.Value.toVariantCompatible();
			byte[] info = new UTF8Encoding(true).GetBytes(keyValuePairs.ToString());
			GD.Print("Writing : " + keyValuePairs.ToString());
			fs.Write(info, 0, info.Length);
		}
		fs.Close();
	}

	private void OnPartChanged(MechPart part)
	{
		MechPartsList.EditPart(part);
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
