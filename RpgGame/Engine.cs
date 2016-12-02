using RLNET;

namespace RpgGame
{
    public class Engine
    {
        private static readonly int screenWidth = 100;
        private static readonly int screenHeight = 70;

        private static RLRootConsole rootConsole;

        public void Run()
        {

            string fontFileName = "../../Resources/terminal8x8.png";

            string consoleTitle = "Rpg Game";

            rootConsole = new RLRootConsole(fontFileName, screenWidth, screenHeight,
              8, 8, 1f, consoleTitle);

            rootConsole.Update += OnRootConsoleUpdate;

            rootConsole.Render += OnRootConsoleRender;

            rootConsole.Run();
        }

        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            rootConsole.Print(10, 10, "It worked!", RLColor.White);
        }

        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            rootConsole.Draw();
        }
    }
}