using System;
using System.Linq;

namespace DungeonExplorer
{

    // Main game controller managing game state and player interactions.
    // Handles the core game loop and command processing.
    internal class Game
    {
        // Player character instance
        private Player _player;
        // Dungeon layout
        private GameMap _gameMap;

        // Flag tracking combat state
        private bool _inCombat = false;

        // Initialises game state and creates dungeon layout
        public Game()
        {
            ResetGame();
        }
        
        // Resets all game state to initial conditions
        // Called when starting a new game
        private void ResetGame()
        {
            // Initialises player
            _player = new Player("Adventurer", 100);

            // Sets up game world
            _gameMap = new GameMap();

            // Creates items
            var rustyKey = new Key("Rusty Key");
            var ironSword = new Weapon("Iron Sword", damage: 10);
            var oakBow = new Weapon("Oak Bow", damage: 15);
            var healthPotion = new Potion("Health Potion", heal: 20);

            // Creates monsters
            var goblinScav = new Monster("Goblin Scavenger", health: 30, damage: 5);
            var draugr = new Monster("Draugr", health: 40, damage: 8);

            // Builds rooms
            var entrance = new Room(
                "Dungeon Entrance",
                "You are at the entrance of the dungeon.",
                item: healthPotion
            );

            var armoury = new Room(
                "Abandoned Armoury",
                "You enter an abandoned armoury.",
                item: ironSword,
                monster: goblinScav   
            );

            var rangersRest = new Room(
                "Ranger's Rest",
                "You find yourself in a ranger's resting place.",
                item: oakBow,
                monster: draugr
            );

            // Assembles map
            _gameMap.AddRoom(entrance);
            _gameMap.AddRoom(armoury);
            _gameMap.AddRoom(rangersRest);

            // Bidirectionally connects rooms
            _gameMap.ConnectRooms(entrance, "north", armoury);
            _gameMap.ConnectRooms(armoury, "south", entrance);
            _gameMap.ConnectRooms(armoury, "north", rangersRest);
            _gameMap.ConnectRooms(rangersRest, "south", armoury);

            // Sets starting location
            _gameMap.SetCurrentRoom(entrance);


        }
        // Allows the user to replay the game
        private bool AskToReplay()
        {
            Console.WriteLine("Would you like to play again? (y/n): ");
            string input = Console.ReadLine().ToLower();

            if (input == "y")
            {
                ResetGame();
                return true;
            }
            else
            {
                Console.WriteLine("Farewell, Adventurer.");
                Environment.Exit(0);
                return false;
            }
        }
        
        // Main game loop handling player input and game state updates
        public void Start()
        {
            bool playing = true;
            while(playing)
            {
                Console.WriteLine("The Dungeon awaits, Adventurer.");
            
                bool inGame = true;
                while (inGame)
                {
                    Room currentRoom = _gameMap.CurrentRoom;

                    if (!_inCombat)
                    {
                        Console.WriteLine("\nCommands: look, status, pickup, use, attack, north, south, exit");
                        Console.Write("\nWhat would you like to do? ");
                    }
                    string input = Console.ReadLine().ToLower();

                    switch (input)
                    {   
                        // Gives player information about the current room
                        case "look": 
                            Console.WriteLine($"\n{currentRoom.GetDescription()}");
                            break;
                        // Shows player character's statistics
                        case "status":
                            Console.WriteLine($"\nHealth: {_player.Health}, Inventory: {_player.Inventory.Contents()}");
                            break;
                        // Picks up all the items in the current room
                        case "pickup":
                            if (_inCombat)
                            {
                                Console.WriteLine("\nYou can't pick up items during combat.");
                                break;
                            }
                            Item item = currentRoom.TakeItem();
                            if (item != null)
                            {
                                _player.Inventory.Add(item);
                                Console.WriteLine($"\nYou picked up {item.Name}.");
                            }
                            else
                            {
                                Console.WriteLine("\nThere is nothing to pick up here.");
                            }
                            break;
                        
                        // Allows the player to try and use an item out of combat
                        case "use":
                            Console.WriteLine("\nWhich item? (Type its name): ");
                            string itemName = Console.ReadLine();
                            Item itemToUse = _player.Inventory.GetAllItems()
                                .FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

                            if (itemToUse != null)
                            {
                                if (itemToUse is Potion potion)
                                {
                                    potion.Use(_player);
                                    _player.Inventory.Remove(potion);
                                }
                                else if (itemToUse is Weapon)
                                {
                                    itemToUse.Use(_player);
                                }
                                else
                                {
                                    itemToUse.Use(_player);
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nItem not found in inventory.");
                            }
                            break;
                        // Initiates combat with a monster, if any is present in the room
                        case "attack":
                            Monster monster = currentRoom.GetMonster();
                            if (monster != null)
                            {
                                _inCombat = true;
                                Console.WriteLine($"\n=== COMBAT BEGINS ===");
                                Console.WriteLine($"You face {monster.Name} (Health: {monster.Health})");

                                while (_inCombat && monster.Health > 0 && _player.Health > 0)
                                {
                                    Console.WriteLine($"\nYour Health: {_player.Health} | Enemy Health: {monster.Health}");
                                    Console.Write("Combat command (attack/use potion/flee): ");
                                    string combatCmd = Console.ReadLine().ToLower();

                                    switch (combatCmd)
                                    {
                                        // In-combat attack against a monster
                                        case "attack":
                                            var weapons = _player.Inventory.GetWeapons().ToList();
                                            if (weapons.Count == 0)
                                            {
                                                Console.WriteLine("You punch the enemy for 1 damage.");
                                                monster.TakeDamage(1);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Choose a weapon:");
                                                for (int i = 0; i < weapons.Count; i++)
                                                {
                                                    Console.WriteLine($"{i + 1}. {weapons[i].Name} (Damage: {weapons[i].Damage})");
                                                }

                                                Console.Write("Enter weapon number: ");
                                                if (int.TryParse(Console.ReadLine(), out int weaponChoice) && weaponChoice > 0 && weaponChoice <= weapons.Count)
                                                {
                                                    Weapon chosenWeapon = weapons[weaponChoice - 1];
                                                    Console.WriteLine($"You attack with {chosenWeapon.Name}.");
                                                    monster.TakeDamage(chosenWeapon.Damage);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Invalid choice. Punching instead for 1 damage.");
                                                    monster.TakeDamage(1);
                                                }
                                            }
                                            if (monster.Health > 0)
                                            {
                                                monster.Attack(_player);
                                            }
                                            else
                                            {
                                                Console.WriteLine($"You defeated the {monster.Name}.");
                                                currentRoom.RemoveMonster();
                                                _inCombat = false;
                                            }
                                            break;
                                        // Disengage from combat
                                        case "flee":
                                            Console.WriteLine("You disengage from combat.");
                                            _inCombat = false;
                                            break;
                                        // Lets the player use a potion in-combat
                                        case "use potion":
                                            Console.WriteLine("\nWhich potion? (Type its name):");
                                            string potionName = Console.ReadLine();
                                            Potion potion = _player.Inventory.GetPotions()
                                                .FirstOrDefault(p => p.Name.Equals(potionName, StringComparison.OrdinalIgnoreCase));

                                            if (potion != null)
                                            {
                                                potion.Use(_player);
                                                _player.Inventory.Remove(potion);
                                                if (monster.Health > 0) monster.Attack(_player);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Potion not found.");
                                            }
                                            break;

                                        default:
                                            Console.WriteLine("Invalid combat command!");
                                            break;
                                    }   
                                }

                                if (monster.Health <= 0)
                                {
                                    currentRoom.RemoveMonster();
                                }
                                _inCombat = false;
                            }
                            else
                            {
                                Console.WriteLine("There are no enemies here.");
                            }
                            break;
                        // Allows the player to move back and forth between rooms
                        case "north":
                        case "south":
                            if (_inCombat)
                            {
                                Console.WriteLine("You can't move during combat!");
                                break;
                            }
                            Room nextRoom = _gameMap.GetAdjacentRoom(currentRoom, input);
                            if (nextRoom != null)
                            {
                                _gameMap.SetCurrentRoom(nextRoom);
                                Console.WriteLine($"You move {input}.");
                            }
                            else
                            {
                                Console.WriteLine($"You can't go {input}.");
                            }
                            break;

                        case "exit":
                            inGame = false;
                            playing = AskToReplay();
                            if (playing) ResetGame();
                            break;
                        

                        default:
                            Console.WriteLine("Invalid command. Try look, status, pickup, use, attack, north, south, or exit.");
                            break;
                    }

                    if (_player.Health <= 0)
                    {
                        Console.WriteLine("Game Over!");
                        playing = AskToReplay();
                        if (playing) ResetGame();
                        break;
                    }
                }
            }
        }
    }
}