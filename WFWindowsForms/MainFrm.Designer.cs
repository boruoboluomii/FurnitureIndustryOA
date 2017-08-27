namespace WFWindowsForms
{
    partial class MainFrm
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
            this.btnStart = new System.Windows.Forms.Button();
            this.btnContinue = new System.Windows.Forms.Button();
            this.txtBookMarkName = new System.Windows.Forms.TextBox();
            this.txtMoney = new System.Windows.Forms.TextBox();
            this.btnStartStatWF = new System.Windows.Forms.Button();
            this.txtAppId = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(91, 55);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "启动";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnContinue
            // 
            this.btnContinue.Location = new System.Drawing.Point(91, 162);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(75, 23);
            this.btnContinue.TabIndex = 0;
            this.btnContinue.Text = "继续";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // txtBookMarkName
            // 
            this.txtBookMarkName.Location = new System.Drawing.Point(214, 56);
            this.txtBookMarkName.Name = "txtBookMarkName";
            this.txtBookMarkName.Size = new System.Drawing.Size(168, 21);
            this.txtBookMarkName.TabIndex = 1;
            this.txtBookMarkName.Text = "book";
            // 
            // txtMoney
            // 
            this.txtMoney.Location = new System.Drawing.Point(214, 164);
            this.txtMoney.Name = "txtMoney";
            this.txtMoney.Size = new System.Drawing.Size(168, 21);
            this.txtMoney.TabIndex = 1;
            this.txtMoney.Text = "120";
            // 
            // btnStartStatWF
            // 
            this.btnStartStatWF.Location = new System.Drawing.Point(103, 242);
            this.btnStartStatWF.Name = "btnStartStatWF";
            this.btnStartStatWF.Size = new System.Drawing.Size(75, 23);
            this.btnStartStatWF.TabIndex = 2;
            this.btnStartStatWF.Text = "启动状态工作流";
            this.btnStartStatWF.UseVisualStyleBackColor = true;
            this.btnStartStatWF.Click += new System.EventHandler(this.btnStartStatWF_Click);
            // 
            // txtAppId
            // 
            this.txtAppId.Location = new System.Drawing.Point(249, 243);
            this.txtAppId.Name = "txtAppId";
            this.txtAppId.Size = new System.Drawing.Size(322, 21);
            this.txtAppId.TabIndex = 3;
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 478);
            this.Controls.Add(this.txtAppId);
            this.Controls.Add(this.btnStartStatWF);
            this.Controls.Add(this.txtMoney);
            this.Controls.Add(this.txtBookMarkName);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.btnStart);
            this.Name = "MainFrm";
            this.Text = "MainFrm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.TextBox txtBookMarkName;
        private System.Windows.Forms.TextBox txtMoney;
        private System.Windows.Forms.Button btnStartStatWF;
        private System.Windows.Forms.TextBox txtAppId;
    }
}