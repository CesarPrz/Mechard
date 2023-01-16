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
    public string Name = "New Part";
    public int IT = 0;
    public int AM = 0;
    public int PS = 0;
    public int PN = 0;
    public MechPartTier Tier = MechPartTier.TIER1;
}
