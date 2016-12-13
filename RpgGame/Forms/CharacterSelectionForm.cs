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
<<<<<<< HEAD
using RpgGame.Core.System;
using RpgGame.Data;
using RpgGame.Data.Data;
using RpgGame.Models;
using RpgGame.Utilities;
using RpgGame.Utilities.Utilities;


=======
using RpgGame.Data;
using RpgGame.Models;
using RpgGame.Models.Characters;
using RpgGame.Systems;
using RpgGame.Utilities;
>>>>>>> origin/master
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
<<<<<<< HEAD
            var context = new RpgGameContext();
            var unit = new UnitOfWork(context);
            var mappingService = new MappingService(unit);

            var schedulingSystem = new SchedulingSystem();
            var commandSystem = new CommandSystem(schedulingSystem, mappingService);
            var objectManager = new ObjectManager(schedulingSystem, mappingService);
            var graphicsManager = new GraphicsManager(mappingService);

            Engine engine = new Engine(commandSystem, graphicsManager, objectManager);
            engine.Run();
            
=======
            var db = new TempDatabase();
            var graphicsManager = new GraphicsManager(db);

            var schedulingSystem = new SchedulingSystem();
            var commandSystem = new CommandSystem(db, schedulingSystem);
            Engine engine = new Engine(commandSystem, graphicsManager);

            
            //this.Hide();
            
            engine.Run();
>>>>>>> origin/master
        }

        private void CharacterSelectionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(1);
        }

        private void CharacterSelectionForm_Load(object sender, EventArgs e)
        {
            LoadCharacterClasses();
            //LoadExistingCharacters();
        }

        private void LoadCharacterClasses()
        {
            classSelectListBox.Items.Add("Mage");
            classSelectListBox.Items.Add("Warrior");
            classSelectListBox.Items.Add("Shaman");
        }

        private void LoadExistingCharacters()
        {
            var characters = context.Characters.Where(e => e.Health > 0);
            foreach (var character in characters)
            {
                characterSelectListBox.Items.Add(character.Name);
            }
        }

        private void characterCreateButton_Click(object sender, EventArgs e)
        {
            //Player newPlayer = new Player()
            //{

            //};
            //TODO:Add character to db
        }

        private void classSelectListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (classSelectListBox.SelectedItem.ToString() == "Mage")
            {
                characterImageBox.Image = null;
                characterImageBox.ImageLocation = Constants.MagePicturePath;
            }
            else if (classSelectListBox.SelectedItem.ToString() == "Shaman")
            {
                characterImageBox.Image = null;
                characterImageBox.ImageLocation = Constants.ShamanPicturePath;
            }
            else
            {
                characterImageBox.Image = null;
                characterImageBox.ImageLocation = Constants.WarriorPicturePath;
            }
<<<<<<< HEAD

            Player pl = new Player();
            characterStats.Text =
                //"Name: " + pl.Name + Environment.NewLine +
                "Minimal attack: " + pl.MinAttack + Environment.NewLine +
                "Maximal attack: " + pl.MaxAttack + Environment.NewLine +
                "Awareness: " + pl.Awareness + Environment.NewLine +
                "Health: " + pl.Health + Environment.NewLine +
                "Speed: " + pl.Speed + Environment.NewLine +
                "Minimal defence: " + pl.MinDefence + Environment.NewLine +
                "Maximal defence: " + pl.MaxDefence + Environment.NewLine +
                "Maximal health: " + pl.MaxHealth + Environment.NewLine;


=======
>>>>>>> origin/master
        }
    }
}
