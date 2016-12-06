namespace RpgGame.Forms
{
    partial class CharacterSelectionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.characterSelectListBox = new System.Windows.Forms.ListBox();
            this.characterSelectButton = new System.Windows.Forms.Button();
            this.characterCreateButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.characterCreateListBox = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.characterStats = new System.Windows.Forms.Label();
            this.characterImageBox = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.characterImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // characterSelectListBox
            // 
            this.characterSelectListBox.FormattingEnabled = true;
            this.characterSelectListBox.Location = new System.Drawing.Point(6, 48);
            this.characterSelectListBox.Name = "characterSelectListBox";
            this.characterSelectListBox.Size = new System.Drawing.Size(103, 173);
            this.characterSelectListBox.TabIndex = 0;
            // 
            // characterSelectButton
            // 
            this.characterSelectButton.Location = new System.Drawing.Point(6, 19);
            this.characterSelectButton.Name = "characterSelectButton";
            this.characterSelectButton.Size = new System.Drawing.Size(103, 23);
            this.characterSelectButton.TabIndex = 1;
            this.characterSelectButton.Text = "Select character";
            this.characterSelectButton.UseVisualStyleBackColor = true;
            this.characterSelectButton.Click += new System.EventHandler(this.characterSelectButton_Click);
            // 
            // characterCreateButton
            // 
            this.characterCreateButton.Location = new System.Drawing.Point(6, 19);
            this.characterCreateButton.Name = "characterCreateButton";
            this.characterCreateButton.Size = new System.Drawing.Size(107, 23);
            this.characterCreateButton.TabIndex = 2;
            this.characterCreateButton.Text = "Create character";
            this.characterCreateButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.characterSelectButton);
            this.groupBox1.Controls.Add(this.characterSelectListBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(117, 236);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Character selection";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.characterCreateListBox);
            this.groupBox2.Controls.Add(this.characterCreateButton);
            this.groupBox2.Location = new System.Drawing.Point(135, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(122, 236);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Character creation";
            // 
            // characterCreateListBox
            // 
            this.characterCreateListBox.FormattingEnabled = true;
            this.characterCreateListBox.Location = new System.Drawing.Point(6, 48);
            this.characterCreateListBox.Name = "characterCreateListBox";
            this.characterCreateListBox.Size = new System.Drawing.Size(107, 173);
            this.characterCreateListBox.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.characterStats);
            this.groupBox3.Location = new System.Drawing.Point(263, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(122, 236);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Character stats";
            // 
            // characterStats
            // 
            this.characterStats.AutoSize = true;
            this.characterStats.Location = new System.Drawing.Point(6, 16);
            this.characterStats.Name = "characterStats";
            this.characterStats.Size = new System.Drawing.Size(38, 91);
            this.characterStats.TabIndex = 0;
            this.characterStats.Text = "Stat 1:\r\nStat 2:\r\nStat 3:\r\nStat 4:\r\nStat 5:\r\nStat 6:\r\nStat 7:\r\n";
            // 
            // characterImageBox
            // 
            this.characterImageBox.Location = new System.Drawing.Point(391, 12);
            this.characterImageBox.Name = "characterImageBox";
            this.characterImageBox.Size = new System.Drawing.Size(236, 236);
            this.characterImageBox.TabIndex = 6;
            this.characterImageBox.TabStop = false;
            // 
            // CharacterSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 258);
            this.Controls.Add(this.characterImageBox);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "CharacterSelectionForm";
            this.Text = "CharacterSelection";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.characterImageBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox characterSelectListBox;
        private System.Windows.Forms.Button characterSelectButton;
        private System.Windows.Forms.Button characterCreateButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox characterCreateListBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label characterStats;
        private System.Windows.Forms.PictureBox characterImageBox;
    }
}