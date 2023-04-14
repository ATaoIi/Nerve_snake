using System.ComponentModel;
using System.Windows.Forms;

namespace SnakeGame
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;
        private System.Windows.Forms.Label scoreLabel;

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
            this.scoreLabel = new System.Windows.Forms.Label();
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(GAME_WIDTH, GAME_HEIGHT + TOP_OFFSET);
            // Set the ClientSize first
            this.Text = "MainForm";
            this.KeyDown += MainForm_KeyDown;
            // 
            // scoreLabel
            // 
            this.scoreLabel.AutoSize = true;
            this.scoreLabel.Location = new System.Drawing.Point(12, 9);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(54, 17);
            this.scoreLabel.TabIndex = 0;
            this.scoreLabel.Text = "grades：0";
            // ...

            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scoreLabel);

            this.attentionLabel = new System.Windows.Forms.Label();
            this.speedLabel = new System.Windows.Forms.Label();

            // attentionLabel
            this.attentionLabel.AutoSize = true;
            this.attentionLabel.Location = new System.Drawing.Point(this.ClientSize.Width - 150, 9);
            this.attentionLabel.Name = "attentionLabel";
            this.attentionLabel.Size = new System.Drawing.Size(89, 17);
            this.attentionLabel.TabIndex = 1;
            this.attentionLabel.Text = "Attention: 0";

            // speedLabel
            this.speedLabel.AutoSize = true;
            this.speedLabel.Location = new System.Drawing.Point(this.ClientSize.Width - 150, 30);
            this.speedLabel.Name = "speedLabel";
            this.speedLabel.Size = new System.Drawing.Size(65, 17);
            this.speedLabel.TabIndex = 2;
            this.speedLabel.Text = "Speed: 0";

            // MainForm
            this.Controls.Add(this.attentionLabel);
            this.Controls.Add(this.speedLabel);
            
            //
            this.Controls.Add(this.scoreLabel);

            this.attentionLabel = new System.Windows.Forms.Label();
            this.speedLabel = new System.Windows.Forms.Label();

            // Create a TableLayoutPanel
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.Dock = DockStyle.Top;
            tableLayoutPanel.ColumnCount = 3;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel.RowCount = 1;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            // Add the scoreLabel to tableLayoutPanel
            tableLayoutPanel.Controls.Add(this.scoreLabel, 0, 0);

            // Add the attentionLabel to tableLayoutPanel
            this.attentionLabel.AutoSize = true;
            tableLayoutPanel.Controls.Add(this.attentionLabel, 1, 0);

            // Add the speedLabel to tableLayoutPanel
            this.speedLabel.AutoSize = true;
            tableLayoutPanel.Controls.Add(this.speedLabel, 2, 0);

            // Add tableLayoutPanel to MainForm
            this.Controls.Add(tableLayoutPanel);
        }
        

        #endregion
    }
}