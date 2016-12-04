namespace RpgGame
{
    using System;
    using Core;
    using RLNET;
    using RogueSharp.Random;
    using RpgGame.Enums;
    using RpgGame.Models;
    using RpgGame.Models.Map;
    using RpgGame.Utilities;

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

        public static SchedulingSystem SchedulingSystem { get; private set; }

        public static IRandom Random { get; private set; }

        public static Player Player { get; set; }

        public static void Exit()
        {
            rootConsole.Close();
        }

        public void Run()
        {
            int seed = (int)DateTime.UtcNow.Ticks;
            Random = new DotNetRandom(seed);

            string fontFileName = "../../Resources/terminal8x8.png";

            string consoleTitle = "Rpg Game";

            Player = new Player("Duci");
            CommandSystem = new CommandSystem();
            SchedulingSystem = new SchedulingSystem();
            SchedulingSystem.Add(Player);

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
            if (CommandSystem.IsPlayerTurn)
            {
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
                        Exit();
                    }
                }

                if (didPlayerAct)
                {
                    renderRequired = true;
                    CommandSystem.EndPlayerTurn();
                }
            }
            else
            {
                CommandSystem.ActivateMonsters();
                renderRequired = true;
            }
        }

        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            if (renderRequired)
            {
                mapConsole.Clear();
                statConsole.Clear();
                messageConsole.Clear();

                DungeonMap.Draw(mapConsole, statConsole);
                Player.Draw(mapConsole, DungeonMap);
                MessageLog.Draw(messageConsole);
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
                renderRequired = false;
            }
        }
    }
}