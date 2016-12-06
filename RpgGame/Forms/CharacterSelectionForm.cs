using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RpgGame.Core;
using RpgGame.Data;
using RpgGame.Systems;

namespace RpgGame.Forms
{
    public partial class CharacterSelectionForm : Form
    {
        private RpgGameContext context;
        public CharacterSelectionForm(RpgGameContext context)
        {
            InitializeComponent();
            this.context = context;
        }

        private void characterSelectButton_Click(object sender, EventArgs e)
        {
            var db = new TempDatabase();
            var graphicsManager = new GraphicsManager(db);

            var schedulingSystem = new SchedulingSystem();
            var commandSystem = new CommandSystem(db, schedulingSystem);
            Engine engine = new Engine(commandSystem, graphicsManager);

            
            //this.Hide();
            
            engine.Run();
        }
    }
}
