using System;
using System.Collections.Generic;
using System.Text.Json;

public class CharacterClasses {
    public string name { get; set; }
    public int minHealth { get; set; }
    public int maxHealth { get; set; }
    public int minDamageMultiplier { get; set; }
    public int maxDamageMultiplier { get; set; }
    public int baseDamage { get; set; }
    public int range { get; set; }
    public CharacterClasses() {

    }
}
public class CharacterClassesArray {
    public List<CharacterClasses> classes { get; set; }
}
