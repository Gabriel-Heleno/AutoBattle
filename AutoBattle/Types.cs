using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle
{
    public class Types
    {

        public struct CharacterClassSpecific
        {
            CharacterClass CharacterClass;
            float hpModifier;
            float ClassDamage;
            CharacterSkills[] skills;

        }

        public struct GridBox
        {
            public int xIndex;
            public int yIndex;
            public Character character;
            public int index;

            public GridBox(int x, int y, int index)
            {
                xIndex = x;
                yIndex = y;
                this.character = null;
                this.index = index;
            }

        }

        public struct CharacterSkills
        {
            string Name;
            float damage;
            float damageMultiplier;
        }

        public enum CharacterClass : uint
        {
            Paladin = 1,
            Warrior = 2,
            Cleric = 3,
            Archer = 4
        }

    }
}
