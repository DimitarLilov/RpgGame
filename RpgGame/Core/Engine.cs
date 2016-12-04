namespace RpgGame
{
    using Core;
    using RLNET;
    using RpgGame.Enums;
    using RpgGame.Models;
    using RpgGame.Systems;
    using RpgGame.Utilities;

    public class Engine
    {
        private RLRootConsole rootConsole;

        private RLConsole mapConsole;
        private RLConsole messageConsole;
        private RLConsole statConsole;
        private RLConsole inventoryConsole;

        private bool renderRequired;

        public Engine(CommandSystem commandSystem, GraphicsManager graphicsManager)
        {
            this.CommandSystem = commandSystem;
            this.GraphicsManager = graphicsManager;

            this.renderRequired = true;
            this.Initialize();
        }

        public CommandSystem CommandSystem { get; }

        public GraphicsManager GraphicsManager { get; }


        public void Run()
        {
            this.rootConsole.Run();
        }

        public void Exit()
        {
            this.rootConsole.Close();
        }

        private void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            bool didPlayerAct = false;
            RLKeyPress keyPress = this.rootConsole.Keyboard.GetKeyPress();

            if (this.CommandSystem.IsPlayerTurn)
            {
                if (keyPress != null)
                {
                    if (keyPress.Key == RLKey.Up)
                    {
                        didPlayerAct = this.CommandSystem.MovePlayer(Direction.Up);
                    }
                    else if (keyPress.Key == RLKey.Down)
                    {
                        didPlayerAct = this.CommandSystem.MovePlayer(Direction.Down);
                    }
                    else if (keyPress.Key == RLKey.Left)
                    {
                        didPlayerAct = this.CommandSystem.MovePlayer(Direction.Left);
                    }
                    else if (keyPress.Key == RLKey.Right)
                    {
                        didPlayerAct = this.CommandSystem.MovePlayer(Direction.Right);
                    }
                    else if (keyPress.Key == RLKey.Escape)
                    {
                        this.Exit();
                    }
                }

                if (!didPlayerAct)
                {
                    return;
                }

                this.renderRequired = true;
                this.CommandSystem.EndPlayerTurn();
            }
            else
            {
                this.CommandSystem.ActivateMonsters();
                this.renderRequired = true;
            }
        }

        private void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            if (!this.renderRequired)
            {
                return;
            }

            this.mapConsole.Clear();
            this.statConsole.Clear();
            this.messageConsole.Clear();

            this.GraphicsManager.Draw(this.mapConsole, this.statConsole, this.messageConsole);

            RLConsole.Blit(this.mapConsole, 0, 0, this.mapConsole.Width, this.mapConsole.Height, this.rootConsole, 0,
                this.inventoryConsole.Height);
            RLConsole.Blit(this.statConsole, 0, 0, this.statConsole.Width, this.statConsole.Height, this.rootConsole,
                this.mapConsole.Width, 0);
            RLConsole.Blit(this.messageConsole, 0, 0, this.messageConsole.Width, this.messageConsole.Height,
                this.rootConsole, 0, this.rootConsole.Height - this.messageConsole.Height);
            RLConsole.Blit(this.inventoryConsole, 0, 0, this.inventoryConsole.Width, this.inventoryConsole.Height,
                this.rootConsole, 0, 0);

            this.rootConsole.Draw();

            this.renderRequired = false;
        }

        private void Initialize()
        {
            this.SetConsoleScreen();
            this.SetConsolePanels();

            var player = new Player("Duci");
            this.CommandSystem.CreateMainModels(player);

            this.rootConsole.Update += this.OnRootConsoleUpdate;
            this.rootConsole.Render += this.OnRootConsoleRender;
        }

        private void SetConsolePanels()
        {
            this.mapConsole = new RLConsole(Constants.MapWidth, Constants.MapHeight);
            this.messageConsole = new RLConsole(Constants.MessageWidth, Constants.MessageHeight);
            this.statConsole = new RLConsole(Constants.StatWidth, Constants.StatHeight);
            this.inventoryConsole = new RLConsole(Constants.InventoryWidth, Constants.InventoryHeight);

            this.mapConsole.SetBackColor(0, 0, this.mapConsole.Width, this.mapConsole.Height, Colors.FloorBackground);
            this.inventoryConsole.SetBackColor(0, 0, this.inventoryConsole.Width, this.inventoryConsole.Height,
                Swatch.DbWood);

            this.inventoryConsole.Print(1, 1, "Inventory", Colors.TextHeading);
        }

        private void SetConsoleScreen()
        {
            this.rootConsole = new RLRootConsole(Constants.FontFilePath, Constants.ScreenWidth, Constants.ScreenHeight,
                8, 8, 1f, Constants.GameTitle);
        }
    }
}