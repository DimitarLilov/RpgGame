using System.Windows.Forms;
using RpgGame.Core;
using RpgGame.Core.System;
using RpgGame.Data;
using RpgGame.Data.Data;
using RpgGame.Forms;

namespace RpgGame
{
    public class Game
    {
        public static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartGameForm());

            
        }
    }
}
