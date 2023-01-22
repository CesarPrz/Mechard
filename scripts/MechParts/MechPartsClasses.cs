using Godot;
using System;

public class MechPartsClasses
{
}

public enum MechPartTier
{
    TIER1 = 1,
    TIER2 = 2,
    TIER3 = 3,
}

public enum MechPartType
{
    MAIN_FRAME,
    WEAPON,
    UTILITY,
    SENSORS,
    MOVEMENT,
}

public class MechPart
{
    public MechPart()
    {
    }

    public Texture2D GetIcon()
    {
        Texture2D icon = new();
        switch (PartType)
        {
            case MechPartType.MAIN_FRAME:
                return ResourceLoader.Load<Texture2D>("res://ui/Icones_Tyrant/Tyrant_Icons_KR.jpg");
            case MechPartType.WEAPON:
                return ResourceLoader.Load<Texture2D>("res://ui/Icones_Tyrant/Tyrant_Icons_SA.jpg");
            case MechPartType.UTILITY:
                return ResourceLoader.Load<Texture2D>("res://ui/Icones_Tyrant/Tyrant_Icons_SU.jpg");
            case MechPartType.SENSORS:
                return ResourceLoader.Load<Texture2D>("res://ui/Icones_Tyrant/Tyrant_Icons_SS.jpg");
            case MechPartType.MOVEMENT:
                return ResourceLoader.Load<Texture2D>("res://ui/Icones_Tyrant/Tyrant_Icons_SL.jpg");
            default:
                return ResourceLoader.Load<Texture2D>("res://ui/Icones_Tyrant/Tyrant_Icons_KR.jpg");
        }
    }

    public MechPart(Godot.Collections.Dictionary<string, string> data)
    {
        IT = int.Parse(data["IT"]);
        AM = int.Parse(data["AM"]);
        PS = int.Parse(data["PS"]);
        PN = int.Parse(data["PN"]);
        Tier = Enum.Parse<MechPartTier>(data["Tier"]);
        Constructors = Enum.Parse<MechConstructors>(data["Constructor"]);
        PartType = Enum.Parse<MechPartType>(data["Type"]);
    }

    public Godot.Collections.Dictionary<string, string> toVariantCompatible()
    {
        return new Godot.Collections.Dictionary<string, string>()
        {
            {"IT" , IT.ToString()},
            {"AM" , AM.ToString()},
            {"PS" , PS.ToString()},
            {"PN" , PN.ToString()},
            {"Tier", Tier.ToString() },
            {"Type", PartType.ToString() },
            {"Constructor", Constructors.ToString() }
        };
    }

    public string Name { get => Constructors.ToString() + ' ' + PartType.ToString(); }
    public int IT = 0;
    public int AM = 0;
    public int PS = 0;
    public int PN = 0;
    public MechPartTier Tier = MechPartTier.TIER1;
    public MechPartType PartType = MechPartType.MAIN_FRAME;
    public MechConstructors Constructors = MechConstructors.Durandal;
}
