using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RLNET;
using RogueSharp.Random;
using RpgGame.Core;
using RpgGame.Data;
using RpgGame.Data.Models;
using RpgGame.Systems;

namespace RpgGame.Forms
{
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
            if (String.IsNullOrEmpty(usernameTextBox.Text) || String.IsNullOrEmpty(passwordTextBox.Text))
            {
                MessageBox.Show("Fields can't be empty");
            }

            var select =
                context.Users.FirstOrDefault(
                    u => u.Username == usernameTextBox.Text && u.Password == passwordTextBox.Text); //current user

            if (select == null)
            {
                MessageBox.Show("No such user!");
            }
            else
            {
                User updatedUser = context.Users.FirstOrDefault(u => u.Username == usernameTextBox.Text && u.Password == passwordTextBox.Text);
                updatedUser.LastLoginDate = DateTime.Now;
                context.Entry(select).CurrentValues.SetValues(updatedUser); // change the date of las login in the database
                context.SaveChanges();


                Engine engine = new Engine();
                engine.Run(); // run the game
            }
        }

        private void LoginForm_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(1);
        }
    }
}

