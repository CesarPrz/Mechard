using Godot;
using System.Collections.Generic;

public partial class MechPartsList : MarginContainer
{
	ItemList itemsList;
	public Dictionary<int, MechPart> parts = new();
	public delegate void PartSelectedDelegate(MechPart part);
	public event PartSelectedDelegate PartSelected;


	public void AddPart(MechPart part)
	{
		int id = itemsList.AddItem(part.Name);
		parts.Add(id, part);
		GD.Print("Added part with name + " + part.Name + " and id " + id);
	}



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		itemsList = GetNodeOrNull<ItemList>("MechPartsPanel/ItemList");

		if (itemsList != null)
			itemsList.ItemClicked += OnItemClicked;
	}

	private void OnItemClicked(long index, Vector2 atPosition, long mouseButtonIndex)
	{
		if (PartSelected != null)
			PartSelected.Invoke(parts[(int)index]);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
