namespace SLSShippingApp
{
    partial class QtyUpdate
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtNewQty = new System.Windows.Forms.TextBox();
            this.btnSubmitQty = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(239, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enter New Quantity of the item in the order.";
            // 
            // txtNewQty
            // 
            this.txtNewQty.Location = new System.Drawing.Point(114, 50);
            this.txtNewQty.Name = "txtNewQty";
            this.txtNewQty.Size = new System.Drawing.Size(100, 20);
            this.txtNewQty.TabIndex = 2;
            // 
            // btnSubmitQty
            // 
            this.btnSubmitQty.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSubmitQty.Location = new System.Drawing.Point(111, 79);
            this.btnSubmitQty.Name = "btnSubmitQty";
            this.btnSubmitQty.Size = new System.Drawing.Size(107, 23);
            this.btnSubmitQty.TabIndex = 3;
            this.btnSubmitQty.Text = "Submit New Qty";
            this.btnSubmitQty.UseVisualStyleBackColor = true;
            // 
            // QtyUpdate
            // 
            this.AcceptButton = this.btnSubmitQty;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 110);
            this.Controls.Add(this.btnSubmitQty);
            this.Controls.Add(this.txtNewQty);
            this.Controls.Add(this.label1);
            this.Name = "QtyUpdate";
            this.Text = "New Quantity";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtNewQty;
        private System.Windows.Forms.Button btnSubmitQty;
    }
}