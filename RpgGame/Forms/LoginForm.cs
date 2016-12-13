using System;
using System.Linq;
using System.Windows.Forms;
using RpgGame.Data.Data;
using RpgGame.Models;

namespace RpgGame.Forms
{
    using RpgGame.Core;
    using RpgGame.Data;
    using RpgGame.Models;
   

    public partial class LoginForm : Form
    {
        private RpgGameContext context;

        public LoginForm(RpgGameContext context)
        {
            this.context = context;
            InitializeComponent();
        }

        private void loginButton_Click_1(object sender, EventArgs e)
        {
            UnitOfWork unit = new UnitOfWork(context);
            if (String.IsNullOrEmpty(usernameTextBox.Text) || String.IsNullOrEmpty(passwordTextBox.Text))
            {
                MessageBox.Show("Fields can't be empty");
            }

            var select =
                unit.Users.GetFirst(
                    u => u.Username == usernameTextBox.Text && u.Password == passwordTextBox.Text); //current user

            if (select == null)
            {
                MessageBox.Show("No such user!");
            }
            else
            {
                User updatedUser = unit.Users.GetFirst(u => u.Username == usernameTextBox.Text && u.Password == passwordTextBox.Text);
                updatedUser.LastLoginDate = DateTime.Now;
                //context.Entry(select).CurrentValues.SetValues(updatedUser);
                //context.SaveChanges();

                //var db = new TempDatabase();
                //var graphicsManager = new GraphicsManager(db);

                //var schedulingSystem = new SchedulingSystem();
                //var commandSystem = new CommandSystem(db, schedulingSystem);
                //Engine engine = new Engine(commandSystem, graphicsManager);

                // hide the logging form, so the game cannot be turned on 10 times
                this.Hide();
                //this.Close();
                //engine.Run();
                CharacterSelectionForm csf = new CharacterSelectionForm(context);
                csf.Show();
                
            }
        }

        private void LoginForm_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            StartGameForm startGameForm = new StartGameForm();
            startGameForm.Closed += (s, args) => this.Close();
            startGameForm.Show();
        }
    }
}

