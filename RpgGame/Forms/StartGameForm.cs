using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RpgGame.Data;

namespace RpgGame.Forms
{
    public partial class StartGameForm : Form
    {
        private RpgGameContext context;
        public StartGameForm()
        {
            context = new RpgGameContext();
            InitializeComponent();
        }

        private void loginButton_Click_1(object sender, EventArgs e)
        {
            this.Visible = false;
            LoginForm loginForm = new LoginForm(context);
            loginForm.Show();
        }

        private void registerButton_Click_1(object sender, EventArgs e)
        {
            this.Visible = false;
            RegisterForm registerForm = new RegisterForm(context);
            registerForm.Show();
        }

        private void StartGameForm_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(1);
        }

        private void StartGameForm_Load_1(object sender, EventArgs e)
        {
            context.Database.Initialize(true);
        }
    }
}
