﻿namespace BrickBreaker
{
    partial class GameScreen
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.livesLabel = new System.Windows.Forms.Label();
            this.scoreLabel = new System.Windows.Forms.Label();
            this.pauseLabel = new System.Windows.Forms.Label();
            this.exitButton = new System.Windows.Forms.Button();
            this.resumeButton = new System.Windows.Forms.Button();
            this.pauseLivesLabel = new System.Windows.Forms.Label();
            this.pauseScoreLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // gameTimer
            // 
            this.gameTimer.Enabled = true;
            this.gameTimer.Interval = 1;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // livesLabel
            // 
            this.livesLabel.BackColor = System.Drawing.Color.Transparent;
            this.livesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.livesLabel.ForeColor = System.Drawing.Color.Black;
            this.livesLabel.Location = new System.Drawing.Point(3, 0);
            this.livesLabel.Name = "livesLabel";
            this.livesLabel.Size = new System.Drawing.Size(116, 34);
            this.livesLabel.TabIndex = 0;
            this.livesLabel.Text = "1";
            this.livesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // scoreLabel
            // 
            this.scoreLabel.BackColor = System.Drawing.Color.Transparent;
            this.scoreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scoreLabel.ForeColor = System.Drawing.Color.Black;
            this.scoreLabel.Location = new System.Drawing.Point(484, 0);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(116, 34);
            this.scoreLabel.TabIndex = 1;
            this.scoreLabel.Text = "Score: 1";
            this.scoreLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pauseLabel
            // 
            this.pauseLabel.BackColor = System.Drawing.Color.Gray;
            this.pauseLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pauseLabel.Location = new System.Drawing.Point(186, 203);
            this.pauseLabel.Name = "pauseLabel";
            this.pauseLabel.Size = new System.Drawing.Size(219, 277);
            this.pauseLabel.TabIndex = 3;
            this.pauseLabel.Text = "Paused";
            this.pauseLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.pauseLabel.Visible = false;
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(220, 417);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(152, 42);
            this.exitButton.TabIndex = 4;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Visible = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // resumeButton
            // 
            this.resumeButton.Location = new System.Drawing.Point(220, 369);
            this.resumeButton.Name = "resumeButton";
            this.resumeButton.Size = new System.Drawing.Size(152, 42);
            this.resumeButton.TabIndex = 5;
            this.resumeButton.Text = "Resume";
            this.resumeButton.UseVisualStyleBackColor = true;
            this.resumeButton.Visible = false;
            this.resumeButton.Click += new System.EventHandler(this.resumeButton_Click);
            // 
            // pauseLivesLabel
            // 
            this.pauseLivesLabel.BackColor = System.Drawing.Color.DimGray;
            this.pauseLivesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pauseLivesLabel.ForeColor = System.Drawing.Color.Black;
            this.pauseLivesLabel.Location = new System.Drawing.Point(237, 252);
            this.pauseLivesLabel.Name = "pauseLivesLabel";
            this.pauseLivesLabel.Size = new System.Drawing.Size(116, 34);
            this.pauseLivesLabel.TabIndex = 6;
            this.pauseLivesLabel.Text = "Score: 1";
            this.pauseLivesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.pauseLivesLabel.Visible = false;
            // 
            // pauseScoreLabel
            // 
            this.pauseScoreLabel.BackColor = System.Drawing.Color.DimGray;
            this.pauseScoreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pauseScoreLabel.ForeColor = System.Drawing.Color.Black;
            this.pauseScoreLabel.Location = new System.Drawing.Point(237, 304);
            this.pauseScoreLabel.Name = "pauseScoreLabel";
            this.pauseScoreLabel.Size = new System.Drawing.Size(116, 34);
            this.pauseScoreLabel.TabIndex = 7;
            this.pauseScoreLabel.Text = "Score: 1";
            this.pauseScoreLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.pauseScoreLabel.Visible = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(237, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 34);
            this.label1.TabIndex = 8;
            this.label1.Text = "Pause";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // GameScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImage = global::BrickBreaker.Properties.Resources.Background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pauseScoreLabel);
            this.Controls.Add(this.pauseLivesLabel);
            this.Controls.Add(this.resumeButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.pauseLabel);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.livesLabel);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "GameScreen";
            this.Size = new System.Drawing.Size(600, 700);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GameScreen_Paint);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GameScreen_KeyUp);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GameScreen_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GameScreen_MouseMove);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.GameScreen_PreviewKeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Label livesLabel;
        private System.Windows.Forms.Label scoreLabel;
        private System.Windows.Forms.Label pauseLabel;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button resumeButton;
        private System.Windows.Forms.Label pauseLivesLabel;
        private System.Windows.Forms.Label pauseScoreLabel;
        private System.Windows.Forms.Label label1;
    }
}
