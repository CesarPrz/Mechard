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
    public Godot.Collections.Dictionary<string, string> toVariantCompatible()
    {
        return new Godot.Collections.Dictionary<string, string>()
        {
            {"IT" , IT.ToString()},
            {"AM" , IT.ToString()},
            {"PS" , IT.ToString()},
            {"PN" , IT.ToString()},
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
