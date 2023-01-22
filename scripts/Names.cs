using System;
using System.Collections.Generic;

public class Names
{
    public static List<string> names = new();
    public int unamedIndex = 0;
    public Names()
    {

        AddName("Petit Jésus");
        AddName("L'enfant de la PGW");
        AddName("Satan");
        AddName("Dante From Devil May Cry Series™");
        AddName("Flynn");
        AddName("Joker (Persona 5)");
        AddName("Joker (Batman)");
        AddName("Berserk from the Berserk Series™");
        AddName("Jotaro");
        AddName("Dio");
        AddName("Joseph (Costume Tequilla)");
        AddName("Vile Jaeger");
        AddName("Vitality Colossus");
        AddName("Panzer Command Unit");
        AddName("Stalker Jaeger");
        AddName("Cobra Scouter");
        AddName("Discovery");
        AddName("Elysium");
        AddName("Claymore");
        AddName("Ravage Drone");
        AddName("Mist Gun System");
        AddName("Jumbo Scouter");
        AddName("Phoenix");
        AddName("Warfare Gun System");
        AddName("Hallowed Jaeger");
        AddName("Blossom");
        AddName("Leviathan");
        AddName("Void Stalker");
        AddName("Dreadnought");
        AddName("Chrono Decimator");
        AddName("Drill Panzer");
        AddName("99 Problems");
        AddName("Everbody's Problem");
        AddName("Traffic Light");
        AddName("Stop Sign 自身 Buster MKIX");
        AddName("Please don't hurt me I'm just a kid MKXCIX");
        AddName("Antoine Daniel");
        AddName("Cocolocatron");
        AddName("MissJirachi MK CC");
    }

    public static void AddName(string name)
    {
        if (!names.Contains(name))
            names.Add(name);
    }

    public string GetRandomName()
    {
        if (names.Count != 0)
        {

            Random random = new();
            int nameIndex = random.Next(0, names.Count);
            string name = names[nameIndex];
            //  GD.Print("Taking name: " + names[nameIndex] + " from the list : " + names.Count);
            names.RemoveAt(nameIndex);
            return name;
        }
        else
        {
            unamedIndex++;
            return "Unamed " + unamedIndex;
        }
    }
}
