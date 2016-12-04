using System;
using System.Linq;
using System.Windows.Forms;

namespace RpgGame.Forms
{
    using RpgGame.Data;
    using RpgGame.Data.Models;

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
                context.Entry(select).CurrentValues.SetValues(updatedUser);
                context.SaveChanges();


                Engine engine = new Engine();
                engine.Run();
            }
        }

        private void LoginForm_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(1);
        }
    }
}

