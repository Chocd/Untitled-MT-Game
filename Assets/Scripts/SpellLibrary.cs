using System.Collections.Generic;
using UnityEngine;

public class SpellLibrary : MonoBehaviour
{
    public Dictionary<string, Spell> spellLibrary = new Dictionary<string, Spell>();

    public void AddSpell(string spellName, int str, int spd, int size)
    {
        bool[,] tempaoe = {{false,false,false},
                           {false,true ,false},
                           {false,false,false}};
        Spell temp = new Spell(str, spd, size, tempaoe, 1f);
        spellLibrary.Add(spellName, temp);
    }

    public void AddSpell(string spellName, int str, int spd, int size, bool[,] aoe)
    {
        Spell temp = new Spell(str, spd, size, aoe, 1f);
        spellLibrary.Add(spellName, temp);
    }
    public Spell getSpell(string spellName)
    {
        Spell temp;
        spellLibrary.TryGetValue(spellName, out temp);
        return temp;
    }
}

public class Spell
{
    //strength of a spell
    public int strength;

    //speed of a spell
    public int speed;

    //size of a spell
    public int size;

    //starting hexes of the spell
    public bool[,] aoe;

    //multiplier of each hex in the spell
    public float aoeMult;

    //mana cost of the spell
    public int manaCost;

    public Spell(int strength, int speed, int size, bool[,] aoe, float aoeMult)
    {
        this.strength = strength;
        this.speed = speed;
        this.size = size;
        this.aoe = aoe;
        this.aoeMult = aoeMult;

        manaCost = strength + speed + size; //should be balanced later
    }

    public float getCastTimeInTensOfMillis()
    {
        return strength + speed + size;
    }
}
