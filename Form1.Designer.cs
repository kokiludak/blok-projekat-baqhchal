namespace Baqhchal
{
    partial class Form1
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
            this.playerIsSheep = new System.Windows.Forms.CheckBox();
            this.buttonNewGame = new System.Windows.Forms.Button();
            this.textUserDepth = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // playerIsSheep
            // 
            this.playerIsSheep.AutoSize = true;
            this.playerIsSheep.Location = new System.Drawing.Point(692, 38);
            this.playerIsSheep.Name = "playerIsSheep";
            this.playerIsSheep.Size = new System.Drawing.Size(80, 17);
            this.playerIsSheep.TabIndex = 0;
            this.playerIsSheep.Text = "100% rights";
            this.playerIsSheep.UseVisualStyleBackColor = false;
            // 
            // buttonNewGame
            // 
            this.buttonNewGame.Location = new System.Drawing.Point(692, 101);
            this.buttonNewGame.Name = "buttonNewGame";
            this.buttonNewGame.Size = new System.Drawing.Size(75, 23);
            this.buttonNewGame.TabIndex = 1;
            this.buttonNewGame.Text = "New Game";
            this.buttonNewGame.UseVisualStyleBackColor = true;
            this.buttonNewGame.Click += new System.EventHandler(this.buttonNewGame_Click);
            // 
            // textUserDepth
            // 
            this.textUserDepth.Location = new System.Drawing.Point(688, 154);
            this.textUserDepth.Name = "textUserDepth";
            this.textUserDepth.Size = new System.Drawing.Size(100, 20);
            this.textUserDepth.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 561);
            this.Controls.Add(this.textUserDepth);
            this.Controls.Add(this.buttonNewGame);
            this.Controls.Add(this.playerIsSheep);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(600, 600);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox playerIsSheep;
        private System.Windows.Forms.Button buttonNewGame;
        private System.Windows.Forms.TextBox textUserDepth;
    }
}

