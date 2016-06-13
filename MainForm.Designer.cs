namespace FallMinLibTools
{
    partial class MainForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtLibFileName = new System.Windows.Forms.TextBox();
            this.btnComplie = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "LibFile:";
            // 
            // txtLibFileName
            // 
            this.txtLibFileName.AllowDrop = true;
            this.txtLibFileName.Location = new System.Drawing.Point(71, 10);
            this.txtLibFileName.Name = "txtLibFileName";
            this.txtLibFileName.Size = new System.Drawing.Size(208, 21);
            this.txtLibFileName.TabIndex = 1;
            this.txtLibFileName.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtLibFileName_DragDrop);
            this.txtLibFileName.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtLibFileName_DragEnter);
            // 
            // btnComplie
            // 
            this.btnComplie.Location = new System.Drawing.Point(285, 9);
            this.btnComplie.Name = "btnComplie";
            this.btnComplie.Size = new System.Drawing.Size(75, 23);
            this.btnComplie.TabIndex = 2;
            this.btnComplie.Text = "&Complie";
            this.btnComplie.UseVisualStyleBackColor = true;
            this.btnComplie.Click += new System.EventHandler(this.btnComplie_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 38);
            this.Controls.Add(this.btnComplie);
            this.Controls.Add(this.txtLibFileName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FallMin Lib Tools";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLibFileName;
        private System.Windows.Forms.Button btnComplie;
    }
}

