using Godot;
using System.Collections.Generic;
using System.IO;
using System.Text;

public partial class CreatePartScreen : Control
{
	TButton BackButton;
	TButton SaveButton;
	MechPartsList MechPartsList;
	MechPartEditor MechPartEditor;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		MechPartsList = GetNodeOrNull<MechPartsList>("PanelContainer/VBoxContainer/ScreenContent/MechPartsListMargin");
		MechPartEditor = GetNodeOrNull<MechPartEditor>("PanelContainer/VBoxContainer/ScreenContent/MechPartViewerMargin");
		SaveButton = GetNodeOrNull<TButton>("PanelContainer/VBoxContainer/BottomLayout/SaveButton");
		BackButton = GetNodeOrNull<TButton>("PanelContainer/VBoxContainer/BottomLayout/BackButton");


		if (BackButton != null)
			BackButton.Button.Pressed += Back_Pressed;
		if (SaveButton != null)
			SaveButton.Button.Pressed += Button_Pressed;
		if (MechPartsList != null)
		{
			MechPartsList.PartSelected += OnPartSelected;
			MechPartEditor.OnPartChanged += OnPartChanged;
		}
		LoadParts();
	}

	private void Back_Pressed()
	{
		Error error = GetTree().ChangeSceneToFile("res://control.tscn");
	}

	private void Button_Pressed()
	{
		SaveParts();
		Back_Pressed();
	}

	private void LoadParts()
	{
		string appdataPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);

		if (File.Exists(appdataPath + "\\Tyrant\\tyrantSave.save"))
		{
			byte[] fileInfo = File.ReadAllBytes(appdataPath + "\\Tyrant\\tyrantSave.save");
			string fileParsed = System.Text.Encoding.Default.GetString(fileInfo);

			Godot.Collections.Array<Godot.Collections.Dictionary<string, string>> partsCollection = (Godot.Collections.Array<Godot.Collections.Dictionary<string, string>>)JSON.ParseString(fileParsed);
			foreach (Godot.Collections.Dictionary<string, string> part in partsCollection)
			{
				MechPart newPart = new(part);
				MechPartsList.AddPart(newPart);
			}
		}
		else
		{
			MechPartsList.AddPart(new MechPart());
			OnPartSelected(new MechPart());
		}

		MechPartsList.UpdateList();
	}

	public void SaveParts()
	{
		string appdataPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
		if (!Directory.Exists(appdataPath + "\\Tyrant"))
			_ = Directory.CreateDirectory(appdataPath + "\\Tyrant");

		FileStream fs = File.Open(appdataPath + "\\Tyrant\\tyrantSave.save", FileMode.Create);
		Godot.Collections.Array<Godot.Collections.Dictionary<string, string>> PartsCollection = new();
		foreach (KeyValuePair<int, MechPart> part in MechPartsList.parts)
		{
			Godot.Collections.Dictionary<string, string> keyValuePairs = part.Value.toVariantCompatible();
			PartsCollection.Add(keyValuePairs);

		}
		byte[] info = new UTF8Encoding(true).GetBytes(PartsCollection.ToString());
		fs.Write(info, 0, info.Length);

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
