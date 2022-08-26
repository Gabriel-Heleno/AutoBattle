using System;
using static AutoBattle.Types;

namespace AutoBattle {
    public class Character {
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
        public int health;
        public int baseDamage;
        public int damageMultiplier { get; set; }
        public bool isEnemy { get; set; }
        public GridBox currentBox;
        public CharacterClasses characterClass;
        public Character target { get; set; }
        public Character(CharacterClasses characterClass, bool isEnemy) {
            this.characterClass = characterClass;
            var rand = new Random();
            //add +1 to include the max
            health = rand.Next(characterClass.minHealth, characterClass.maxHealth + 1);
            damageMultiplier = rand.Next(characterClass.minDamageMultiplier, characterClass.maxDamageMultiplier + 1);
            baseDamage = characterClass.baseDamage;

            this.isEnemy = isEnemy;
            string baseName = (isEnemy) ? "Evil " : "Allied ";
            playerName = baseName + characterClass.name + " " + predefinedNames[rand.Next(0, predefinedNames.Length)];
        }


        public bool TakeDamage(int amount) {
            health -= amount;
            if (health <= 0) {
                Die();
                return true;
            }
            return false;
        }

        public void Die() {
            Console.WriteLine($"{playerName} has died!\n");
        }

        public void WalkTO(bool CanWalk) {

        }

        public void StartTurn(Grid battlefield) {

            if (CheckCloseTargets(battlefield)) {
                Attack(target);


                return;
            } else {   // if there is no target close enough, calculates in wich direction this character should move to be closer to a possible target
                if (currentBox.xIndex > target.currentBox.xIndex) {
                    if ((battlefield.grids.Exists(x => x.index == currentBox.index - 1))) {
                        currentBox.character = null;
                        battlefield.grids[currentBox.index] = currentBox;
                        currentBox = (battlefield.grids.Find(x => x.index == currentBox.index - 1));
                        currentBox.character = this;
                        battlefield.grids[currentBox.index] = currentBox;
                        Console.WriteLine($"{playerName}(HP:{health}) walked left\n");
                        battlefield.drawBattlefield(5, 5);

                        return;
                    }
                } else if (currentBox.xIndex < target.currentBox.xIndex) {
                    currentBox.character = null;
                    battlefield.grids[currentBox.index] = currentBox;
                    currentBox = (battlefield.grids.Find(x => x.index == currentBox.index + 1));
                    currentBox.character = this;
                    battlefield.grids[currentBox.index] = currentBox;
                    Console.WriteLine($"{playerName}(HP:{health}) walked right\n");
                    battlefield.drawBattlefield(5, 5);
                    return;
                }

                if (currentBox.yIndex > target.currentBox.yIndex) {
                    currentBox.character = null;
                    battlefield.grids[currentBox.index] = currentBox;
                    currentBox = (battlefield.grids.Find(x => x.index == currentBox.index - battlefield.xLenght));
                    currentBox.character = this;
                    battlefield.grids[currentBox.index] = currentBox;
                    Console.WriteLine($"{playerName}(HP:{health}) walked up\n");
                    battlefield.drawBattlefield(5, 5);
                    return;
                } else if (currentBox.yIndex < target.currentBox.yIndex) {
                    currentBox.character = null;
                    battlefield.grids[currentBox.index] = this.currentBox;
                    currentBox = (battlefield.grids.Find(x => x.index == currentBox.index + battlefield.xLenght));
                    currentBox.character = this;
                    battlefield.grids[currentBox.index] = currentBox;
                    Console.WriteLine($"{playerName}(HP:{health}) walked down\n");
                    battlefield.drawBattlefield(5, 5);

                    return;
                }
            }
        }

        // Check in x and y directions if there is any character close enough to be a target.

        bool CheckCloseTargets(Grid battlefield) {
            for(int i = 1; i <= characterClass.range; i++) {
                Character left = (battlefield.grids.Find(x => x.index == currentBox.index - i).character);
                Character right = (battlefield.grids.Find(x => x.index == currentBox.index + i).character);
                Character up = (battlefield.grids.Find(x => x.index == currentBox.index + (battlefield.xLenght * i)).character);
                Character down = (battlefield.grids.Find(x => x.index == currentBox.index - (battlefield.xLenght * i)).character);
                if(left == target || right == target || up == target || down == target) {
                    return true;
                }
            }
            return false;
        }

        public void Attack(Character target) {
            if (health > 0) {
                var rand = new Random();
                int inflictDamage = rand.Next(1, (int)(baseDamage * damageMultiplier));
                Console.WriteLine($"{playerName} is attacking the {this.target.playerName}(HP:{target.health}) and did {inflictDamage} damage\n");
                bool died = target.TakeDamage(inflictDamage);
                if (!died) {
                    Console.WriteLine($"{this.target.playerName} has now {this.target.health} points of health\n");
                }
            }
        }
    }
}
