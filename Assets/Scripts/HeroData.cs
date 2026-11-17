using System;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class HeroData
{
    public string heroName;
    public string heroDescription;
    public int heroHealth;
    public int heroAttack;
    public int heroDamage;
    public int heroMagic;
    public int heroProtection;
    public string heroCountry;

    public List<string> completedBattles = new List<string>();

    public static HeroData instance;

    public Sprite heroImage;

   
}
