using Godot;

public partial class PartName : HBoxContainer
{
	TextureRect Icon;
	Label Label;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Icon = GetNodeOrNull<TextureRect>("MarginContainer2/TextureRect");
		Label = GetNodeOrNull<Label>("MarginContainer/Label");
	}

	public void SetPart(MechPart part)
	{
		if (Label != null)
		{
			Label.Text = part.Name;
		}
		if (Icon != null)
		{
			Icon.Texture = part.GetIcon();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
