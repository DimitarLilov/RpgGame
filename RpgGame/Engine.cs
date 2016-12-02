namespace RpgGame
{
    using Systems;
    using Core;
    using RLNET;

    public class Engine
    {
        private static int screenWidth = 100;
        private static int screenHeight = 70;

        private static int mapWidth = 80;
        private static int mapHeight = 48;
        private static RLConsole mapConsole;

        private static int messageWidth = 80;
        private static int messageHeight = 11;
        private static RLConsole messageConsole;

        private static int statWidth = 20;
        private static int statHeight = 70;
        private static RLConsole statConsole;

        private static int inventoryWidth = 80;
        private static int inventoryHeight = 11;
        private static RLConsole inventoryConsole;

        private static RLRootConsole rootConsole;

        private static bool renderRequired = true;

        public static DungeonMap DungeonMap { get; private set; }

        public static CommandSystem CommandSystem { get; private set; }

        public static MessageLog MessageLog { get; private set; }

        public static Player Player { get; set; }

        public void Run()
        {
            string fontFileName = "../../Resources/terminal8x8.png";

            string consoleTitle = "Rpg Game";

            Player = new Player();
            CommandSystem = new CommandSystem();
            MessageLog = new MessageLog();

            rootConsole = new RLRootConsole(fontFileName, screenWidth, screenHeight, 8, 8, 1f, consoleTitle);
            MapGenerator mapGenerator = new MapGenerator(mapWidth, mapHeight);
            DungeonMap = mapGenerator.CreateMap();

            DungeonMap.UpdatePlayerFieldOfView();

            mapConsole = new RLConsole(mapWidth, mapHeight);
            messageConsole = new RLConsole(messageWidth, messageHeight);
            statConsole = new RLConsole(statWidth, statHeight);
            inventoryConsole = new RLConsole(inventoryWidth, inventoryHeight);

            rootConsole.Update += OnRootConsoleUpdate;

            rootConsole.Render += OnRootConsoleRender;

            rootConsole.Run();
        }

        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            bool didPlayerAct = false;
            RLKeyPress keyPress = rootConsole.Keyboard.GetKeyPress();

            if (keyPress != null)
            {
                if (keyPress.Key == RLKey.Up)
                {
                    didPlayerAct = CommandSystem.MovePlayer(Direction.Up);
                }
                else if (keyPress.Key == RLKey.Down)
                {
                    didPlayerAct = CommandSystem.MovePlayer(Direction.Down);
                }
                else if (keyPress.Key == RLKey.Left)
                {
                    didPlayerAct = CommandSystem.MovePlayer(Direction.Left);
                }
                else if (keyPress.Key == RLKey.Right)
                {
                    didPlayerAct = CommandSystem.MovePlayer(Direction.Right);
                }
                else if (keyPress.Key == RLKey.Escape)
                {
                    rootConsole.Close();
                }
            }

            if (didPlayerAct)
            {
                renderRequired = true;
            }
        }

        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            if (renderRequired)
            {
                renderRequired = false;
            }

            DungeonMap.Draw(mapConsole);
            Player.Draw(mapConsole, DungeonMap);
            Player.DrawStats(statConsole);

            messageConsole.SetBackColor(0, 0, messageWidth, messageHeight, Swatch.DbDeepWater);
            messageConsole.Print(1, 0, "Messages", Colors.TextHeading);

            inventoryConsole.SetBackColor(0, 0, inventoryWidth, inventoryHeight, Swatch.DbWood);
            inventoryConsole.Print(1, 1, "Inventory", Colors.TextHeading);

            RLConsole.Blit(mapConsole, 0, 0, mapWidth, mapHeight, rootConsole, 0, inventoryHeight);
            RLConsole.Blit(statConsole, 0, 0, statWidth, statHeight, rootConsole, mapWidth, 0);
            RLConsole.Blit(messageConsole, 0, 0, messageWidth, messageHeight, rootConsole, 0, screenHeight - messageHeight);
            RLConsole.Blit(inventoryConsole, 0, 0, inventoryWidth, inventoryHeight, rootConsole, 0, 0);
            rootConsole.Draw();
        }
    }
}