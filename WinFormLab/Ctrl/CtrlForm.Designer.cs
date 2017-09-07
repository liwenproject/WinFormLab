namespace WinFormLab.Ctrl
{
    partial class CtrlForm
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
            this.cbNearMonth = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cbNearMonth
            // 
            this.cbNearMonth.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbNearMonth.FormattingEnabled = true;
            this.cbNearMonth.Location = new System.Drawing.Point(29, 13);
            this.cbNearMonth.Name = "cbNearMonth";
            this.cbNearMonth.Size = new System.Drawing.Size(121, 28);
            this.cbNearMonth.TabIndex = 0;
            // 
            // CtrlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 261);
            this.Controls.Add(this.cbNearMonth);
            this.Name = "CtrlForm";
            this.Text = "CtrlForm";
            this.Load += new System.EventHandler(this.CtrlForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbNearMonth;
    }
}