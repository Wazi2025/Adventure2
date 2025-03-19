namespace Adventure2;

class Program
{

    class Room
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Item { get; set; }
        public Dictionary<string, Room> Exits { get; set; }

        public Room(string name, string description, string item)
        {
            Name = name;
            Description = description;
            Item = item;
            Exits = new Dictionary<string, Room>();
        }

        // Adds an exit from this room to another
        public void AddExit(string direction, Room targetRoom)
        {
            Exits[direction.ToLower()] = targetRoom;
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
            }
            else
            {
                Console.WriteLine("You can't go that way.");
                Console.WriteLine(CurrentRoom.Name);
                Console.WriteLine(CurrentRoom.Description);
                Console.WriteLine(CurrentRoom.Item);
            }
        }
    }

    static void Main(string[] args)
    {

        // Example setup:
        Room bridge = new Room("Bridge", "The control panels blink in a rhythmic pattern. You're on the bridge of your ship.", "You see a keycard.");
        Room dockingBay = new Room("Docking Bay", "You're in the docking bay. There's a shuttle here.", "");
        Room storageRoom = new Room("Storage Room", "Crates and boxes fill this storage room. It's dimly lit.", "");

        // Connecting rooms (bi-directional for this example):
        dockingBay.AddExit("north", storageRoom);
        storageRoom.AddExit("south", dockingBay);

        storageRoom.AddExit("west", bridge);
        bridge.AddExit("east", storageRoom);


        // Initialize Player:
        Player player = new Player(bridge);
        while (true)
        {
            //Console.WriteLine(player.CurrentRoom.Description);
            Console.WriteLine("Which way?");
            string input = Console.ReadLine().Trim().ToLower();

            if (input == "quit" || input == "exit") break;

            player.Move(input);
        }

    }

}