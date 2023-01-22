using Godot;
using System.Collections.Generic;

public partial class MechPartsList : MarginContainer
{
	ItemList itemsList;
	public Dictionary<int, MechPart> parts = new();
	public delegate void PartSelectedDelegate(MechPart part);
	public event PartSelectedDelegate PartSelected;
	public TButton AddPartButton;
	public TButton RemovePartButton;
	int currentPartIndex = 0;


	public void AddPart(MechPart part)
	{
		if (itemsList != null)
		{
			int id = itemsList.AddItem(part.Name);
			itemsList.Select(id);
			currentPartIndex = id;

			parts.Add(id, part);
			GD.Print("Added part with name + " + part.Name + " and id " + id);
		}
	}

	public void EditPart(MechPart part)
	{
		parts[currentPartIndex] = part;
		UpdateList();
		itemsList.Select(currentPartIndex);
	}

	public void UpdateList()
	{
		itemsList.Clear();
		foreach (KeyValuePair<int, MechPart> part in parts)
		{
			Texture2D icon = new();
			switch (part.Value.PartType)
			{
				case MechPartType.MAIN_FRAME:
					icon = ResourceLoader.Load<Texture2D>("res://ui/Icones_Tyrant/Tyrant_Icons_KR.jpg");
					break;
				case MechPartType.WEAPON:
					icon = ResourceLoader.Load<Texture2D>("res://ui/Icones_Tyrant/Tyrant_Icons_SA.jpg");
					break;
				case MechPartType.UTILITY:
					icon = ResourceLoader.Load<Texture2D>("res://ui/Icones_Tyrant/Tyrant_Icons_SU.jpg");
					break;
				case MechPartType.SENSORS:
					icon = ResourceLoader.Load<Texture2D>("res://ui/Icones_Tyrant/Tyrant_Icons_SS.jpg");
					break;
				case MechPartType.MOVEMENT:
					icon = ResourceLoader.Load<Texture2D>("res://ui/Icones_Tyrant/Tyrant_Icons_SL.jpg");
					break;
			}
			_ = itemsList.AddItem(part.Value.Name, icon);
		}
		itemsList.Select(currentPartIndex);

	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		itemsList = GetNodeOrNull<ItemList>("MechPartsPanel/VBoxContainer/ItemList");
		AddPartButton = GetNodeOrNull<TButton>("MechPartsPanel/VBoxContainer/HBoxContainer/AddPartButton");
		RemovePartButton = GetNodeOrNull<TButton>("MechPartsPanel/VBoxContainer/HBoxContainer/RemovePartButton");
		if (AddPartButton != null && AddPartButton.Button != null)
			AddPartButton.Button.Pressed += OnAddPartPressed;
		if (RemovePartButton != null && RemovePartButton.Button != null)
			RemovePartButton.Button.Pressed += OnRemovePressed;
		if (itemsList != null)
		{
			itemsList.ItemClicked += OnItemClicked;
		}

	}

	private void OnRemovePressed()
	{
		_ = parts.Remove(currentPartIndex);
		currentPartIndex = 0;
		UpdateList();
		PartSelected?.Invoke(parts[currentPartIndex]);
	}

	private void OnAddPartPressed()
	{
		MechPart part = new();
		AddPart(part);
		PartSelected?.Invoke(part);
	}

	private void OnItemClicked(long index, Vector2 atPosition, long mouseButtonIndex)
	{
		currentPartIndex = (int)index;
		if (PartSelected != null)
			PartSelected.Invoke(parts[(int)index]);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
