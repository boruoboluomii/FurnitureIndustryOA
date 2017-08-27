namespace WFWinFrmDemoo
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStartWF = new System.Windows.Forms.Button();
            this.txtBookMarkName = new System.Windows.Forms.TextBox();
            this.btnContinue = new System.Windows.Forms.Button();
            this.txtMoney = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnStartWF
            // 
            this.btnStartWF.Location = new System.Drawing.Point(73, 53);
            this.btnStartWF.Name = "btnStartWF";
            this.btnStartWF.Size = new System.Drawing.Size(75, 23);
            this.btnStartWF.TabIndex = 0;
            this.btnStartWF.Text = "启动工作流";
            this.btnStartWF.UseVisualStyleBackColor = true;
            this.btnStartWF.Click += new System.EventHandler(this.btnStartWF_Click);
            // 
            // txtBookMarkName
            // 
            this.txtBookMarkName.Location = new System.Drawing.Point(220, 55);
            this.txtBookMarkName.Name = "txtBookMarkName";
            this.txtBookMarkName.Size = new System.Drawing.Size(246, 21);
            this.txtBookMarkName.TabIndex = 1;
            // 
            // btnContinue
            // 
            this.btnContinue.Location = new System.Drawing.Point(73, 157);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(75, 23);
            this.btnContinue.TabIndex = 2;
            this.btnContinue.Text = "继续书签执行";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // txtMoney
            // 
            this.txtMoney.Location = new System.Drawing.Point(220, 157);
            this.txtMoney.Name = "txtMoney";
            this.txtMoney.Size = new System.Drawing.Size(255, 21);
            this.txtMoney.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 440);
            this.Controls.Add(this.txtMoney);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.txtBookMarkName);
            this.Controls.Add(this.btnStartWF);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartWF;
        private System.Windows.Forms.TextBox txtBookMarkName;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.TextBox txtMoney;
    }
}

