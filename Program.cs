using System.IO.Pipelines;

namespace Adventure2;

class Program
{

    class Room
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string RoomExit { get; set; }
        public string RoomExit2 { get; set; }
        public string Item { get; set; }

        public Dictionary<string, Room> Exits { get; set; }

        public Room(string name, string description, string roomExit, string roomExit2, string item)
        {
            Name = name;
            Description = description;
            RoomExit = roomExit;
            RoomExit2 = roomExit2;
            Item = item;
            Exits = new Dictionary<string, Room>();
        }

        // Adds an exit from this room to another
        public string AddExit(string direction, Room targetRoom)
        {
            Exits[direction.ToLower()] = targetRoom;
            string temp = $"There are exit(s) {direction}";
            return temp;
        }
    }

    class Player
    {
        public Room CurrentRoom { get; set; }

        public Player(Room startRoom)
        {
            CurrentRoom = startRoom;
        }

        public void Move(string direction)
        {
            if (CurrentRoom.Exits.ContainsKey(direction.ToLower()))
            {
                CurrentRoom = CurrentRoom.Exits[direction.ToLower()];
                Console.WriteLine($"You move {direction} and enter {CurrentRoom.Name}.");
                Console.WriteLine(CurrentRoom.Description);
                Console.WriteLine(CurrentRoom.Item);
                Console.WriteLine(CurrentRoom.RoomExit);
                Console.WriteLine(CurrentRoom.RoomExit2);
            }
            else
            {
                Console.WriteLine("You can't go that way.");
                Console.WriteLine(CurrentRoom.Name);
                Console.WriteLine(CurrentRoom.Description);
                Console.WriteLine(CurrentRoom.RoomExit);
                Console.WriteLine(CurrentRoom.RoomExit2);
                Console.WriteLine(CurrentRoom.Item);
            }
        }
    }

    static void Main(string[] args)
    {

        // Example setup:
        Room bridge = new Room("Bridge", "The control panels blink in a rhythmic pattern. You're on the bridge of your ship.", "", "", "You see a keycard.");
        Room dockingBay = new Room("Docking Bay", "You're in the docking bay. There's a shuttle here.", "", "", "");
        Room storageRoom = new Room("Storage Room", "Crates and boxes fill this storage room. It's dimly lit.", "", "", "");
        bool gameStarted = false;

        // Connecting rooms 
        dockingBay.RoomExit = dockingBay.AddExit("south", bridge);
        storageRoom.RoomExit = storageRoom.AddExit("north", bridge);

        //bridge room has two exits
        bridge.RoomExit = bridge.AddExit("south", storageRoom);
        bridge.RoomExit2 = bridge.AddExit("north", dockingBay);

        // Initialize Player:
        Player player = new Player(bridge);
        while (true)
        {
            //describe starting room instead of just "Which way?"
            //the first time. After that Player will supply room descriptions
            if (!gameStarted)
            {
                Console.WriteLine(player.CurrentRoom.Name);
                Console.WriteLine(player.CurrentRoom.Description);
                Console.WriteLine(player.CurrentRoom.Item);
                Console.WriteLine(player.CurrentRoom.RoomExit);
                Console.WriteLine(player.CurrentRoom.RoomExit2);
                gameStarted = true;
            }
            Console.WriteLine("Which way?");
            string input = Console.ReadLine().Trim().ToLower();

            if (input == "quit" || input == "exit") break;

            player.Move(input);
        }

    }

}