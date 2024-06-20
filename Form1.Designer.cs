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
            this.labelFirstSel = new System.Windows.Forms.Label();
            this.buttonUndo = new System.Windows.Forms.Button();
            this.checkBoxDrawLegalMoves = new System.Windows.Forms.CheckBox();
            this.labelCountLegalMoves = new System.Windows.Forms.Label();
            this.checkBoxOpponentDraw = new System.Windows.Forms.CheckBox();
            this.buttonBestMove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // playerIsSheep
            // 
            this.playerIsSheep.AutoSize = true;
            this.playerIsSheep.Checked = true;
            this.playerIsSheep.CheckState = System.Windows.Forms.CheckState.Checked;
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
            this.textUserDepth.Text = "3";
            // 
            // labelFirstSel
            // 
            this.labelFirstSel.AutoSize = true;
            this.labelFirstSel.Location = new System.Drawing.Point(692, 208);
            this.labelFirstSel.Name = "labelFirstSel";
            this.labelFirstSel.Size = new System.Drawing.Size(0, 13);
            this.labelFirstSel.TabIndex = 3;
            this.labelFirstSel.UseMnemonic = false;
            // 
            // buttonUndo
            // 
            this.buttonUndo.Location = new System.Drawing.Point(692, 225);
            this.buttonUndo.Name = "buttonUndo";
            this.buttonUndo.Size = new System.Drawing.Size(75, 23);
            this.buttonUndo.TabIndex = 4;
            this.buttonUndo.Text = "Undo";
            this.buttonUndo.UseVisualStyleBackColor = true;
            this.buttonUndo.Click += new System.EventHandler(this.buttonUndo_Click);
            // 
            // checkBoxDrawLegalMoves
            // 
            this.checkBoxDrawLegalMoves.AutoSize = true;
            this.checkBoxDrawLegalMoves.Location = new System.Drawing.Point(688, 293);
            this.checkBoxDrawLegalMoves.Name = "checkBoxDrawLegalMoves";
            this.checkBoxDrawLegalMoves.Size = new System.Drawing.Size(115, 17);
            this.checkBoxDrawLegalMoves.TabIndex = 5;
            this.checkBoxDrawLegalMoves.Text = "Draw Legal Moves";
            this.checkBoxDrawLegalMoves.UseVisualStyleBackColor = true;
            this.checkBoxDrawLegalMoves.CheckedChanged += new System.EventHandler(this.checkBoxDrawLegalMoves_CheckedChanged);
            // 
            // labelCountLegalMoves
            // 
            this.labelCountLegalMoves.AutoSize = true;
            this.labelCountLegalMoves.Location = new System.Drawing.Point(692, 400);
            this.labelCountLegalMoves.Name = "labelCountLegalMoves";
            this.labelCountLegalMoves.Size = new System.Drawing.Size(70, 13);
            this.labelCountLegalMoves.TabIndex = 6;
            this.labelCountLegalMoves.Text = "Legal moves:";
            // 
            // checkBoxOpponentDraw
            // 
            this.checkBoxOpponentDraw.AutoSize = true;
            this.checkBoxOpponentDraw.Checked = true;
            this.checkBoxOpponentDraw.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxOpponentDraw.Location = new System.Drawing.Point(688, 434);
            this.checkBoxOpponentDraw.Name = "checkBoxOpponentDraw";
            this.checkBoxOpponentDraw.Size = new System.Drawing.Size(99, 17);
            this.checkBoxOpponentDraw.TabIndex = 7;
            this.checkBoxOpponentDraw.Text = "Draw opponent";
            this.checkBoxOpponentDraw.UseVisualStyleBackColor = true;
            this.checkBoxOpponentDraw.CheckedChanged += new System.EventHandler(this.checkBoxOpponentDraw_CheckedChanged);
            // 
            // buttonBestMove
            // 
            this.buttonBestMove.Location = new System.Drawing.Point(695, 482);
            this.buttonBestMove.Name = "buttonBestMove";
            this.buttonBestMove.Size = new System.Drawing.Size(75, 23);
            this.buttonBestMove.TabIndex = 8;
            this.buttonBestMove.Text = "Best Move";
            this.buttonBestMove.UseVisualStyleBackColor = true;
            this.buttonBestMove.Click += new System.EventHandler(this.buttonBestMove_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 561);
            this.Controls.Add(this.buttonBestMove);
            this.Controls.Add(this.checkBoxOpponentDraw);
            this.Controls.Add(this.labelCountLegalMoves);
            this.Controls.Add(this.checkBoxDrawLegalMoves);
            this.Controls.Add(this.buttonUndo);
            this.Controls.Add(this.labelFirstSel);
            this.Controls.Add(this.textUserDepth);
            this.Controls.Add(this.buttonNewGame);
            this.Controls.Add(this.playerIsSheep);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(600, 600);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox playerIsSheep;
        private System.Windows.Forms.Button buttonNewGame;
        private System.Windows.Forms.TextBox textUserDepth;
        private System.Windows.Forms.Label labelFirstSel;
        private System.Windows.Forms.Button buttonUndo;
        private System.Windows.Forms.CheckBox checkBoxDrawLegalMoves;
        private System.Windows.Forms.Label labelCountLegalMoves;
        private System.Windows.Forms.CheckBox checkBoxOpponentDraw;
        private System.Windows.Forms.Button buttonBestMove;
    }
}

