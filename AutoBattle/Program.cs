using System;
using System.Collections.Generic;
using System.Linq;
using static AutoBattle.Types;
using System.IO;
using System.Text.Json;

namespace AutoBattle {
    class Program {
        static void Main(string[] args) {
            Grid grid = new Grid(5, 5);
            List<CharacterClasses> classes;
            PopulateClassesArray();
            GridBox playerCurrentLocation;
            GridBox enemyCurrentLocation;
            Character playerCharacter;
            Character enemyCharacter;
            List<Character> allPlayers = new List<Character>();
            int currentTurn = 0;
            int numberOfPossibleTiles = grid.grids.Count;
            GetPlayerChoice();

            void GetPlayerChoice() {
                //asks for the player to choose between for possible classes via console.
                Console.WriteLine("Choose Between One of this Classes:\n");
                string classesOptions = "";
                int count = 0;
                foreach(CharacterClasses charClass in classes) {
                    classesOptions += "[" + count + "] " + charClass.name;
                    count++;
                }
                Console.WriteLine(classesOptions);
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

                CharacterClasses characterClass = classes[classIndex];
                Console.WriteLine($"Player Class Choice: {characterClass.name}");
                playerCharacter = new Character(characterClass, false);

                CreateEnemyCharacter();

            }

            void CreateEnemyCharacter() {
                //randomly choose the enemy class and set up vital variables
                var rand = new Random();
                int randomInteger = rand.Next(1, 4);
                CharacterClasses enemyClass = classes[randomInteger];
                Console.WriteLine($"Enemy Class Choice: {enemyClass.name}");
                enemyCharacter = new Character(enemyClass, true);
                StartGame();

            }

            void StartGame() {
                //populates the character variables and targets
                enemyCharacter.target = playerCharacter;
                playerCharacter.target = enemyCharacter;
                allPlayers.Add(playerCharacter);
                allPlayers.Add(enemyCharacter);
                AlocatePlayers();
                StartTurn();

            }

            void StartTurn() {

                if (currentTurn == 0) {
                    //sort players in an random order
                    var rand = new Random();
                    allPlayers = allPlayers.OrderBy(a => rand.Next()).ToList();
                }

                foreach (Character character in allPlayers) {
                    character.StartTurn(grid);
                }

                currentTurn++;
                HandleTurn();
            }

            void HandleTurn() {
                if (playerCharacter.health <= 0) {
                    Console.Write(Environment.NewLine + Environment.NewLine);
                    Console.Write("\"Hahahah, loser!\" - Evil Lord");
                    Console.Write(Environment.NewLine + Environment.NewLine);

                    return;
                } else if (enemyCharacter.health <= 0) {
                    // endgame
                    Console.Write(Environment.NewLine + Environment.NewLine);
                    Console.Write("Congrats, you win!");
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

            void AlocatePlayers() {
                AlocatePlayerCharacter();
                AlocateEnemyCharacter();
                grid.drawBattlefield(5, 5);
            }

            void AlocatePlayerCharacter() {
                var rand = new Random();
                int random = rand.Next(0,25);
                GridBox randomLocation = (grid.grids.ElementAt(random));
                Console.Write($"{random}\n");
                if (randomLocation.character == null) {
                    playerCurrentLocation = randomLocation;
                    randomLocation.character = playerCharacter;
                    grid.grids[random] = randomLocation;
                    playerCharacter.currentBox = grid.grids[random];
                    //AlocateEnemyCharacter();
                } else {
                    AlocatePlayerCharacter();
                }
            }

            void AlocateEnemyCharacter() {
                var rand = new Random();
                int random = rand.Next(0, 25);
                GridBox randomLocation = (grid.grids.ElementAt(random));
                Console.Write($"{random}\n");
                if (randomLocation.character == null) {
                    enemyCurrentLocation = randomLocation;
                    randomLocation.character = enemyCharacter;
                    grid.grids[random] = randomLocation;
                    enemyCharacter.currentBox = grid.grids[random];
                } else {
                    AlocateEnemyCharacter();
                }


            }
            void PopulateClassesArray() {
                string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, @"classes.json");
                using (StreamReader reader = new StreamReader(path)) {
                    String json = reader.ReadToEnd();
                    json = json.Replace(@"\r\n","");

                    CharacterClassesArray classesArray = JsonSerializer.Deserialize < CharacterClassesArray >(json);
                    classes = classesArray.classes;
                }
            }

        }
    }
}
