using System;
using static AutoBattle.Character;
using static AutoBattle.Grid;
using System.Collections.Generic;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle {
    class Program {
        static void Main(string[] args) {
            Grid grid = new Grid(5, 5);
            CharacterClass playerCharacterClass;
            GridBox PlayerCurrentLocation;
            GridBox EnemyCurrentLocation;
            Character PlayerCharacter;
            Character EnemyCharacter;
            List<Character> AllPlayers = new List<Character>();
            int currentTurn = 0;
            int numberOfPossibleTiles = grid.grids.Count;
            GetPlayerChoice();

            void GetPlayerChoice() {
                //asks for the player to choose between for possible classes via console.
                Console.WriteLine("Choose Between One of this Classes:\n");
                Console.WriteLine("[1] Paladin, [2] Warrior, [3] Cleric, [4] Archer");
                //store the player choice in a variable
                string choice = Console.ReadLine();

                //verify if user has choosen a class
                if (!choice.Equals(null)) {
                    CreatePlayerCharacter(Int32.Parse(choice));
                } else {
                    GetPlayerChoice();
                }
            }

            void CreatePlayerCharacter(int classIndex) {

                CharacterClass characterClass = (CharacterClass)classIndex;
                Console.WriteLine($"Player Class Choice: {characterClass}");
                PlayerCharacter = new Character(characterClass, false);

                CreateEnemyCharacter();

            }

            void CreateEnemyCharacter() {
                //randomly choose the enemy class and set up vital variables
                var rand = new Random();
                int randomInteger = rand.Next(1, 4);
                CharacterClass enemyClass = (CharacterClass)randomInteger;
                Console.WriteLine($"Enemy Class Choice: {enemyClass}");
                EnemyCharacter = new Character(enemyClass, true);
                StartGame();

            }

            void StartGame() {
                //populates the character variables and targets
                EnemyCharacter.target = PlayerCharacter;
                PlayerCharacter.target = EnemyCharacter;
                AllPlayers.Add(PlayerCharacter);
                AllPlayers.Add(EnemyCharacter);
                AlocatePlayers();
                StartTurn();

            }

            void StartTurn() {

                if (currentTurn == 0) {
                    //AllPlayers.Sort();  
                }

                foreach (Character character in AllPlayers) {
                    character.StartTurn(grid);
                }

                currentTurn++;
                HandleTurn();
            }

            void HandleTurn() {
                if (PlayerCharacter.health == 0) {
                    return;
                } else if (EnemyCharacter.health == 0) {
                    Console.Write(Environment.NewLine + Environment.NewLine);

                    // endgame?

                    Console.Write(Environment.NewLine + Environment.NewLine);

                    return;
                } else {
                    Console.Write(Environment.NewLine + Environment.NewLine);
                    Console.WriteLine("Click on any key to start the next turn...\n");
                    Console.Write(Environment.NewLine + Environment.NewLine);

                    ConsoleKeyInfo key = Console.ReadKey();
                    StartTurn();
                }
            }

            int GetRandomInt(int min, int max) {
                var rand = new Random();
                int index = rand.Next(min, max);
                return index;
            }

            void AlocatePlayers() {
                AlocatePlayerCharacter();

            }

            void AlocatePlayerCharacter() {
                int random = 0;
                GridBox RandomLocation = (grid.grids.ElementAt(random));
                Console.Write($"{random}\n");
                if (!RandomLocation.ocupied) {
                    GridBox PlayerCurrentLocation = RandomLocation;
                    RandomLocation.ocupied = true;
                    grid.grids[random] = RandomLocation;
                    PlayerCharacter.currentBox = grid.grids[random];
                    AlocateEnemyCharacter();
                } else {
                    AlocatePlayerCharacter();
                }
            }

            void AlocateEnemyCharacter() {
                int random = 24;
                GridBox RandomLocation = (grid.grids.ElementAt(random));
                Console.Write($"{random}\n");
                if (!RandomLocation.ocupied) {
                    EnemyCurrentLocation = RandomLocation;
                    RandomLocation.ocupied = true;
                    grid.grids[random] = RandomLocation;
                    EnemyCharacter.currentBox = grid.grids[random];
                    grid.drawBattlefield(5, 5);
                } else {
                    AlocateEnemyCharacter();
                }


            }

        }
    }
}
