using RpgGame.Forms;
using System.Windows.Forms;

namespace RpgGame
{
    public class Program
    {
        public static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartGameForm());

            //Engine engine = new Engine();
            //engine.Run();
        }
    }
}
