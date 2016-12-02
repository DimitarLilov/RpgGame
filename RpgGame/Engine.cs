namespace RpgGame
{
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

        public void Run()
        {
            string fontFileName = "../../Resources/terminal8x8.png";

            string consoleTitle = "Rpg Game";

            rootConsole = new RLRootConsole(fontFileName, screenWidth, screenHeight, 8, 8, 1f, consoleTitle);

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
            messageConsole.SetBackColor(0, 0, messageWidth, messageHeight, RLColor.Gray);
            messageConsole.Print(1, 0, "Messages", RLColor.White);

            inventoryConsole.SetBackColor(0, 0, inventoryWidth, inventoryHeight, RLColor.Brown);
            inventoryConsole.Print(1, 1, "Inventory", RLColor.White);
        }

        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            RLConsole.Blit(mapConsole, 0, 0, mapWidth, mapHeight, rootConsole, 0, inventoryHeight);
            RLConsole.Blit(statConsole, 0, 0, statWidth, statHeight, rootConsole, mapWidth, 0);
            RLConsole.Blit(messageConsole, 0, 0, messageWidth, messageHeight, rootConsole, 0, screenHeight - messageHeight);
            RLConsole.Blit(inventoryConsole, 0, 0, inventoryWidth, inventoryHeight, rootConsole, 0, 0);
            rootConsole.Draw();
        }
    }
}