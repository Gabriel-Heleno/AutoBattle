using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
    public class Character
    {
        private string[] predefinedNames = new string[] { 
            "Gabriel",
            "Miguel",
            "Rafael",
            "Uriel",
            "Ana",
            "Helena",
            "Joana",
            "Rita"
        };
        public string playerName { get; set; }
        public float health;
        public float baseDamage;
        public float damageMultiplier { get; set; }
        public GridBox currentBox;
        public Character target { get; set; } 
        public Character(CharacterClass characterClass, bool isEnemy)
        {
            var rand = new Random();
            health = rand.Next(80, 120);
            baseDamage = rand.Next(10, 30);

            string baseName = (isEnemy)? "Evil " : "Allied ";
            playerName = baseName + predefinedNames[rand.Next(0, predefinedNames.Length)];
        }


        public bool TakeDamage(float amount)
        {
            if((health -= baseDamage) <= 0)
            {
                Die();
                return true;
            }
            return false;
        }

        public void Die()
        {
            //TODO >> maybe kill him?
        }

        public void WalkTO(bool CanWalk)
        {

        }

        public void StartTurn(Grid battlefield)
        {

            if (CheckCloseTargets(battlefield)) 
            {
                Attack(target);
                

                return;
            }
            else
            {   // if there is no target close enough, calculates in wich direction this character should move to be closer to a possible target
                if(this.currentBox.xIndex > target.currentBox.xIndex)
                {
                    if ((battlefield.grids.Exists(x => x.Index == currentBox.Index - 1)))
                    {
                        currentBox.ocupied = false;
                        battlefield.grids[currentBox.Index] = currentBox;
                        currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index - 1));
                        currentBox.ocupied = true;
                        battlefield.grids[currentBox.Index] = currentBox;
                        Console.WriteLine($"Player {playerName} walked left\n");
                        battlefield.drawBattlefield(5, 5);

                        return;
                    }
                } else if(currentBox.xIndex < target.currentBox.xIndex)
                {
                    currentBox.ocupied = false;
                    battlefield.grids[currentBox.Index] = currentBox;
                    currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index + 1));
                    currentBox.ocupied = true;
                    return;
                    battlefield.grids[currentBox.Index] = currentBox;
                    Console.WriteLine($"Player {playerName} walked right\n");
                    battlefield.drawBattlefield(5, 5);
                }

                if (this.currentBox.yIndex > target.currentBox.yIndex)
                {
                    battlefield.drawBattlefield(5, 5);
                    this.currentBox.ocupied = false;
                    battlefield.grids[currentBox.Index] = currentBox;
                    this.currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index - battlefield.xLenght));
                    this.currentBox.ocupied = true;
                    battlefield.grids[currentBox.Index] = currentBox;
                    Console.WriteLine($"Player {playerName} walked up\n");
                    return;
                }
                else if(this.currentBox.yIndex < target.currentBox.yIndex)
                {
                    this.currentBox.ocupied = true;
                    battlefield.grids[currentBox.Index] = this.currentBox;
                    this.currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index + battlefield.xLenght));
                    this.currentBox.ocupied = false;
                    battlefield.grids[currentBox.Index] = currentBox;
                    Console.WriteLine($"Player {playerName} walked down\n");
                    battlefield.drawBattlefield(5, 5);

                    return;
                }
            }
        }

        // Check in x and y directions if there is any character close enough to be a target.
        bool CheckCloseTargets(Grid battlefield)
        {
            bool left = (battlefield.grids.Find(x => x.Index == currentBox.Index - 1).ocupied);
            bool right = (battlefield.grids.Find(x => x.Index == currentBox.Index + 1).ocupied);
            bool up = (battlefield.grids.Find(x => x.Index == currentBox.Index + battlefield.xLenght).ocupied);
            bool down = (battlefield.grids.Find(x => x.Index == currentBox.Index - battlefield.xLenght).ocupied);

            if (left & right & up & down) 
            {
                return true;
            }
            return false; 
        }

        public void Attack (Character target)
        {
            var rand = new Random();
            target.TakeDamage(rand.Next(0, (int)baseDamage));
            Console.WriteLine($"Player {playerName} is attacking the player {this.target.playerName} and did {baseDamage} damage\n");
        }
    }
}
