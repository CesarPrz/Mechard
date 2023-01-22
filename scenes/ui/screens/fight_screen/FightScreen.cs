using Godot;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

public class Mech
{
    public string name = "Unamed Mech";
    public MechPart Kernel;
    public MechPart SA;
    public MechPart SU;
    public MechPart SS;
    public MechPart SL;
    public List<Battle> battles = new();
    public int wins
    {
        get
        {
            int wins = 0;
            foreach (Battle battle in battles)
                if (battle.result == Battle.BattleResult.WIN) wins++;
            return wins;
        }
    }
    public int losses
    {
        get
        {
            int losses = 0;
            foreach (Battle battle in battles)
                if (battle.result == Battle.BattleResult.LOSE) losses++;
            return losses;
        }
    }

    public float averageTurns
    {
        get
        {
            int turnsTotal = 0;
            foreach (Battle battle in battles)
                turnsTotal += battle.turnCount;
            return turnsTotal / battles.Count;
        }
    }

    public Mech()
    {
    }

    public Mech(string _name, MechPart kernel, MechPart sA, MechPart sU, MechPart sS, MechPart sL)
    {
        name = _name;
        Kernel = kernel;
        SA = sA;
        SU = sU;
        SS = sS;
        SL = sL;
        if (kernel == null || sA == null || SL == null || SS == null || SU == null)
            GD.Print("Mech part is null!");
    }

    public int IT
    {
        get => Kernel.IT + SA.IT + SU.IT + SS.IT + SL.IT;
    }

    public int PS
    {
        get => Kernel.PS + SA.PS + SU.PS + SS.PS + SL.PS;
    }

    public int PN
    {
        get => Kernel.PN + SA.PN + SU.PN + SS.PN + SL.PN;
    }

    public int AM
    {
        get => Kernel.AM + SA.AM + SU.AM + SS.AM + SL.AM;
    }


    public void PrintMechInfos()
    {
        GD.Print("---------" + name + "-----------");
        GD.Print(Kernel.Name);
        GD.Print(SA.Name);
        GD.Print(SU.Name);
        GD.Print(SS.Name);
        GD.Print(SL.Name);
        GD.Print("--------------------------------");
    }
}

public class MechAssembler
{
    public List<Mech> mech = new();
    public Names names;
    public void AssembleMechs(List<MechPart> parts)
    {
        mech.Clear();
        names = new();
        foreach (MechPart KernelPart in parts)
        {
            if (KernelPart.PartType == MechPartType.MAIN_FRAME)
            {
                foreach (MechPart WeaponPart in parts)
                {

                    if (WeaponPart.PartType == MechPartType.WEAPON)
                    {
                        foreach (MechPart MovementPart in parts)
                        {
                            if (MovementPart.PartType == MechPartType.MOVEMENT)
                            {
                                foreach (MechPart UtilityPart in parts)
                                {
                                    if (UtilityPart.PartType == MechPartType.UTILITY)
                                    {
                                        foreach (MechPart SensorPart in parts)
                                        {
                                            if (SensorPart.PartType == MechPartType.SENSORS)
                                            {
                                                mech.Add(new Mech(names.GetRandomName(), KernelPart, WeaponPart, UtilityPart, SensorPart, MovementPart));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

    }
}

public class Battle
{
    public int turnCount;
    public enum BattleResult
    {
        WIN,
        LOSE,
        TIE
    }

    public BattleResult result;

    public int GetPlayerDamage(Mech player, Mech opponent)
    {
        int damageReduction = opponent.AM - player.PN;
        if (damageReduction < 0) damageReduction = 0;
        int ps = player.PS - damageReduction;
        if (ps < 0) ps = 0;
        return ps;
    }

    public int GetOpponentDamage(Mech player, Mech opponent)
    {
        int damageReduction = player.AM - opponent.PN;
        if (damageReduction < 0) damageReduction = 0;
        int ps = opponent.PS - damageReduction;
        if (ps < 0) ps = 0;
        return ps;
    }
    public Battle(Mech player, Mech opponent)
    {
        int playerIT = player.IT;
        int opponentIT = opponent.IT;
        //  int playerDamage = 10;
        //  int opponentDamage = 4;
        int playerDamage = GetPlayerDamage(player, opponent);
        int opponentDamage = GetOpponentDamage(player, opponent);
        turnCount = 0;
        while (playerIT > 0 && opponentIT > 0 && turnCount < 25)
        {
            opponentIT -= playerDamage;
            playerIT -= opponentDamage;
            turnCount++;
        }
        if (playerIT > opponentIT)
            result = BattleResult.WIN;
        else
            result = BattleResult.LOSE;
    }
}

public partial class FightScreen : Control
{
    ItemList list;
    List<MechPart> MechPartList = new();
    MechAssembler mechaAssembler = new();
    MechViewer MechViewer;
    OptionButton optionButton;
    TButton backButton;
    private void FightMechs()
    {
        List<Mech> mechs = mechaAssembler.mech;
        foreach (Mech player in mechs)
        {
            int i = 0;
            foreach (Mech opponent in mechs)
            {
                Battle battle = new Battle(player, opponent);
                player.battles.Add(battle);
                i++;
            }
        }
        GD.Print("Battles Done!");
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
                MechPartList.Add(newPart);
            }
        }
    }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        list = GetNodeOrNull<ItemList>("PanelContainer/VBoxContainer/MainContentMargin/HBoxContainer/MechListContainer/MechListPanel/VBoxContainer/MechList");
        MechViewer = GetNodeOrNull<MechViewer>("PanelContainer/VBoxContainer/MainContentMargin/HBoxContainer/VBoxContainer/MechViewer");
        optionButton = GetNodeOrNull<OptionButton>("PanelContainer/VBoxContainer/MainContentMargin/HBoxContainer/MechListContainer/MechListPanel/VBoxContainer/HBoxContainer/MarginContainer2/OptionButton");
        backButton = GetNodeOrNull<TButton>("PanelContainer/VBoxContainer/BottomLayoutMargin/BottomLayout/PrimaryButtonMargin");
        backButton.Button.Pressed += Back_Pressed;
        LoadParts();
        mechaAssembler.AssembleMechs(MechPartList);
        Task unused = Task.Run(() => FightMechs());


        if (optionButton != null)
        {
            optionButton.ItemSelected += SortMechList;
            optionButton.AddItem("Win");
            optionButton.AddItem("IT");
            optionButton.AddItem("PS");
            optionButton.AddItem("PN");
            optionButton.AddItem("AM");
            optionButton.Select(0);
        }

        list.ItemSelected += OnItemSelected;
        UpdateList();
        SortMechList(0);
    }

    private void Back_Pressed()
    {
        Error error = GetTree().ChangeSceneToFile("res://control.tscn");
    }

    private void UpdateList()
    {
        list.Clear();
        if (list != null)
        {
            foreach (Mech mech in mechaAssembler.mech)
            {
                _ = list.AddItem(mech.name);
            }
        }
        list.Select(0);
        OnItemSelected(0);
    }

    private void SortMechList(long index)
    {
        switch (index)
        {
            case 0:
                mechaAssembler.mech.Sort((x, y) => y.wins.CompareTo(x.wins));
                break;
            case 1:
                mechaAssembler.mech.Sort((x, y) => y.IT.CompareTo(x.IT));
                break;
            case 2:
                mechaAssembler.mech.Sort((x, y) => y.PS.CompareTo(x.PS));
                break;
            case 3:
                mechaAssembler.mech.Sort((x, y) => y.PN.CompareTo(x.PN));
                break;
            case 4:
                mechaAssembler.mech.Sort((x, y) => y.AM.CompareTo(x.AM));
                break;
        }
        UpdateList();
    }

    private void OnItemSelected(long index)
    {
        MechViewer.SetMech(mechaAssembler.mech[(int)index]);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
