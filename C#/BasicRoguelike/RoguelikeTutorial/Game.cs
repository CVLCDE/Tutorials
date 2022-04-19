using RLNET;
using RoguelikeTutorial.Core;
using RoguelikeTutorial.Systems;

namespace RogueSharpTutorial
{
    public class Game
    {
        // Defines the dimmensions of the game screen. Dimmensions represent the number of tiles to use.
        private static readonly int _screenHeight = 70;
        private static readonly int _screenWidth = 100;
        private static RLRootConsole _rootConsole;

        // The map console takes up most of the screen and is where the map will be drawn
        private static readonly int _mapWidth = 80;
        private static readonly int _mapHeight = 48;
        private static RLConsole _mapConsole;

        // Below the map console is the message console which displays attack rolls and other information
        private static readonly int _messageWidth = 80;
        private static readonly int _messageHeight = 11;
        private static RLConsole _messageConsole;

        // The stat console is to the right of the map and display player and monster stats
        private static readonly int _statWidth = 20;
        private static readonly int _statHeight = 70;
        private static RLConsole _statConsole;

        // Above the map is the inventory console which shows the players equipment, abilities, and items
        private static readonly int _inventoryWidth = 80;
        private static readonly int _inventoryHeight = 11;
        private static RLConsole _inventoryConsole;

        public static DungeonMap DungeonMap { get; private set; }

        public static void Main()
        {
            // Bitmap
            string fontFileName = "terminal8x8.png";

            // Game Window Title
            string consoleTitle = "RogueSharp Tutorial";

            // Define an RLRootConsole with specified settings
            _rootConsole = new RLRootConsole(fontFileName, _screenWidth, _screenHeight, 8, 8, 1f, consoleTitle);

            // Initialize the sub consoles that we will Blit to the root console
            _mapConsole = new RLConsole(_mapWidth, _mapHeight);
            _messageConsole = new RLConsole(_messageWidth, _messageHeight);
            _statConsole = new RLConsole(_statWidth, _statHeight);
            _inventoryConsole = new RLConsole(_inventoryWidth, _inventoryHeight);

            // Handler for Update event
            _rootConsole.Update += OnRootConsoleUpdate;

            // Handler for Render event
            _rootConsole.Render += OnRootConsoleRender;


            MapGenerator mapGenerator = new MapGenerator(_mapWidth, _mapHeight);
            DungeonMap = mapGenerator.CreateMap();

            // Start the RLNET game loop
            _rootConsole.Run(); 
        }

        // Event handler for RLNET's Update event
        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            _rootConsole.Print(10, 10, "Wxrking!", RLColor.White);

            // Map Console Settings
            _mapConsole.SetBackColor(0, 0, _mapWidth, _mapHeight, Colors.FloorBackground);

            // Message Console Settings
            _messageConsole.SetBackColor(0, 0, _messageWidth, _messageHeight, Palette.DbDeepWater);
            _messageConsole.Print(1, 1, "Messages", Colors.TextHeading);

            // Stat Console Settings
            _statConsole.SetBackColor(0, 0, _statWidth, _statHeight, Palette.DbOldStone);
            _statConsole.Print(1, 1, "Stats", Colors.TextHeading);

            // Inventory Console Settings
            _inventoryConsole.SetBackColor(0, 0, _inventoryWidth, _inventoryHeight, Palette.DbWood);
            _inventoryConsole.Print(1, 1, "Inventory", Colors.TextHeading);
        }

        // Event Handler for RLNET's Render event
        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            // Blit the sub consoles to the root console in the correct locations
            RLConsole.Blit(_mapConsole, 0, 0, _mapWidth, _mapHeight,
              _rootConsole, 0, _inventoryHeight);
            RLConsole.Blit(_statConsole, 0, 0, _statWidth, _statHeight,
              _rootConsole, _mapWidth, 0);
            RLConsole.Blit(_messageConsole, 0, 0, _messageWidth, _messageHeight,
              _rootConsole, 0, _screenHeight - _messageHeight);
            RLConsole.Blit(_inventoryConsole, 0, 0, _inventoryWidth, _inventoryHeight,
              _rootConsole, 0, 0);

            // Tell RLNET to draw the console that we set
            _rootConsole.Draw();

            DungeonMap.Draw(_mapConsole);
        }
    }
}
