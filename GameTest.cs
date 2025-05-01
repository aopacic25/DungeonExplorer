using System;
using System.Diagnostics;

namespace DungeonExplorer
{
    public static class GameTest
    {
        public static void RunTests()
        {
            Console.WriteLine("Running tests...");
            TestPlayerInitialisation();
            TestInventoryManagement();
            TestCombatMechanics();
            TestRoomNavigation();
        }


        private static void TestPlayerInitialisation()
        {
            var player = new Player("TestHero", 100);
            Debug.Assert(player.Health == 100, "Player health should initialise to 100.");
            Debug.Assert(player.Name == "TestHero", "Player name should match constructor.");
            Console.WriteLine("Player initialisation passed.");
        }

        private static void TestInventoryManagement()
        {
            var player = new Player("TestHero", 100);
            var potion = new Potion("Health Potion", 20);
            var sword = new Weapon("Iron Sword", 10);

            player.Inventory.Add(potion);
            player.Inventory.Add(sword);

            Debug.Assert(player.Inventory.GetCount(potion) == 1, "Potion should be in inventory.");
            Debug.Assert(player.Inventory.GetWeapons().Count() == 1, "Weapon should be detected.");

            player.Inventory.Remove(potion);
            Debug.Assert(player.Inventory.GetCount(potion) == 0, "Potion should be removed.");
            Console.WriteLine("Inventory management passed."); 
        }

        private static void TestCombatMechanics()
        {
            var player = new Player("TestHero", 100);
            var monster = new Monster("Goblin", 30, 5);

            monster.Attack(player);
            Debug.Assert(player.Health == 95, "Player should take 5 damage.");

            player.Inventory.Add(new Weapon("Sword", 10));
            var weapon = player.Inventory.GetWeapons().First();
            monster.TakeDamage(weapon.Damage);
            Debug.Assert(monster.Health == 20, "Monster should take 10 damage.");
            Console.WriteLine("Combat mechanics passed.");
        }

        private static void TestRoomNavigation()
        {
            var gameMap = new GameMap();
            var room1 = new Room("Room 1", "Test room 1");
            var room2 = new Room("Room 2", "Test room 2");

            gameMap.AddRoom(room1);
            gameMap.AddRoom(room2);
            gameMap.ConnectRooms(room1, "north", room2);

            gameMap.SetCurrentRoom(room1);
            var nextRoom = gameMap.GetAdjacentRoom(room1, "north");
            Debug.Assert(nextRoom == room2, "Room navigation should work.");
            Console.WriteLine("Room navigation passed.");
        }
    }
}