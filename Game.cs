using System;
using System.Diagnostics;
using System.Media;

namespace DungeonExplorer
{
    // Manages the core game logic.
    internal class Game
    {
        // Represents the player, tracking their name, health, and inventory.
        private Player player;
        // A list of Room objects representing the dungeon.
        private List<Room> rooms;
        // Tracks the index of the current room in the rooms list.
        private int currentRoomIndex;

        // Initialises the game by creating the player and rooms.
        public Game()
        {
            // Call ResetGame to initialise the game state.
            ResetGame();
        }
        
        // Resets the game state to its initial values.
        private void ResetGame()
        {
            // Initialise the player with a name and health.
            player = new Player("Adventurer", 100);

            // Initialise the rooms list with three Room objects.
            rooms = new List<Room>
    {
                new Room("Dungeon Entrance",
                         "You are at the entrance of the dungeon.",
                         "A rusty key lies on the ground.",
                         "rusty key"),
                new Room("Abandoned Armoury",
                         "You enter an abandoned armoury.",
                         "An iron sword rests on a table.",
                         "iron sword"),
                new Room("Ranger's Rest",
                         "You find yourself in a ranger's resting place.",
                         "A longbow leans against the wall.",
                         "longbow")
    };

            // Start in the first room by resetting the room index.
            currentRoomIndex = 0;
        }

        // Asks the player if they want to replay the game.
        private bool AskToReplay()
        {
            Console.WriteLine("Would you like to play again? (y/n): ");
            string input = Console.ReadLine().ToLower();

            // If the user chooses to replay, reset the game state.
            if (input == "y")
            {
                ResetGame();
                return true;
            }
            // If the user chooses not to replay, exit the program.
            else
            {
                Console.WriteLine("Farewell, Adventurer.");
                Environment.Exit(0);
                return false;
            }
        }
        
        // Begin the game and handle the main game loop.
        public void Start()
        {
            // Controls the outer game loop for replay functionality.
            bool playing = true;
            while(playing)
            {
                // Display a welcome message and instructions.
                Console.WriteLine("The Dungeon awaits, Adventurer.");
                Console.WriteLine("Type 'look' to view the room, 'status' to check your health and inventory, 'pickup' to pick up an item, 'north' to go north, 'south' to go south, or 'exit' to quit.");
            
                // Controls the inner game loop, for current game session.
                bool inGame = true;
                while (inGame)
                {
                    // Display the current room's name.
                    Console.WriteLine($"\nYou are in the {rooms[currentRoomIndex].Name}.");
                    // Prompt the player for input.
                    Console.Write("What would you like to do? ");
                    // Read input and convert to lowercase.
                    string input = Console.ReadLine().ToLower();

                    // Process the player's input.
                    switch (input)
                    {
                        case "look":
                            // Display the current room's description. 
                            Console.WriteLine(rooms[currentRoomIndex].GetDescription());
                            break;

                        case "status":
                            // Display the player's health and inventory.
                            Console.WriteLine($"Health: {player.Health}, Inventory: {player.InventoryContents()}");
                            break;
                    
                        case "pickup":
                            // Attempt to pick up an item in the current room.
                            string item = rooms[currentRoomIndex].GetItem();
                            if (item != null)
                            {
                                player.PickUpItem(item);
                                // Add the item to the player's inventory.
                                Console.WriteLine($"You picked up the {item}.");
                                // Remove the item from the room.
                                rooms[currentRoomIndex].RemoveItem();
                            }
                            else
                            {
                                Console.WriteLine("There is nothing to pick up here.");
                            }
                            break;

                        case "north":
                            // Move to the room to the north, if possible.
                            if (currentRoomIndex < rooms.Count - 1)
                            {
                                // Increment the room index.
                                currentRoomIndex++;
                                Console.WriteLine("You move north.");
                            }
                            else
                            {
                                Console.WriteLine("You cannot go further north.");
                            }
                            break;

                        case "south":
                            // Move to the room to the north, if possible.
                            if (currentRoomIndex > 0)
                            {
                                currentRoomIndex--;
                                // Decrement the room index.
                                Console.WriteLine("You move south.");
                            }
                            else
                            {
                                Console.WriteLine("You cannot go further south.");
                            }
                            break;

                        case "exit":
                            // Exit the current game session.
                            inGame = false;
                            // Ask if the player wants to replay.
                            playing = AskToReplay();
                            break;
                        

                        default:
                            // Handle invalid commands.
                            Console.WriteLine("Invalid command. Try 'look', 'status', 'pickup', 'north', 'south', or 'exit'.");
                            break;
                    }
                }
            }
        }
    }
}