namespace Assign
{
    partial class Gant_chart
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
            this.Deallocation = new System.Windows.Forms.GroupBox();
            this.de_allo_btn = new System.Windows.Forms.Button();
            this.Pid_label = new System.Windows.Forms.Label();
            this.Type_combo = new System.Windows.Forms.ComboBox();
            this.Deallocate_btn = new System.Windows.Forms.Button();
            this.Pid_txt = new System.Windows.Forms.TextBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.Allocation = new System.Windows.Forms.GroupBox();
            this.seg_txt = new System.Windows.Forms.TextBox();
            this.Num_label = new System.Windows.Forms.Label();
            this.Add_btn = new System.Windows.Forms.Button();
            this.seg_box = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.allo_btn = new System.Windows.Forms.Button();
            this.seg_table = new System.Windows.Forms.TableLayoutPanel();
            this.Deallocation.SuspendLayout();
            this.Allocation.SuspendLayout();
            this.seg_box.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(108, 2223);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 1;
            // 
            // Deallocation
            // 
            this.Deallocation.BackColor = System.Drawing.SystemColors.Highlight;
            this.Deallocation.Controls.Add(this.de_allo_btn);
            this.Deallocation.Controls.Add(this.Pid_label);
            this.Deallocation.Controls.Add(this.Type_combo);
            this.Deallocation.Controls.Add(this.Deallocate_btn);
            this.Deallocation.Controls.Add(this.Pid_txt);
            this.Deallocation.Location = new System.Drawing.Point(12, 12);
            this.Deallocation.Name = "Deallocation";
            this.Deallocation.Size = new System.Drawing.Size(301, 143);
            this.Deallocation.TabIndex = 2;
            this.Deallocation.TabStop = false;
            this.Deallocation.Text = "Deallocation";
            this.Deallocation.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // de_allo_btn
            // 
            this.de_allo_btn.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.de_allo_btn.Location = new System.Drawing.Point(64, 109);
            this.de_allo_btn.Name = "de_allo_btn";
            this.de_allo_btn.Size = new System.Drawing.Size(102, 28);
            this.de_allo_btn.TabIndex = 5;
            this.de_allo_btn.Text = "Reset";
            this.de_allo_btn.UseVisualStyleBackColor = false;
            // 
            // Pid_label
            // 
            this.Pid_label.AutoSize = true;
            this.Pid_label.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Pid_label.Location = new System.Drawing.Point(6, 68);
            this.Pid_label.Name = "Pid_label";
            this.Pid_label.Size = new System.Drawing.Size(29, 13);
            this.Pid_label.TabIndex = 4;
            this.Pid_label.Text = "#Pid";
            // 
            // Type_combo
            // 
            this.Type_combo.FormattingEnabled = true;
            this.Type_combo.Items.AddRange(new object[] {
            "New Process",
            "Old Process"});
            this.Type_combo.Location = new System.Drawing.Point(6, 19);
            this.Type_combo.Name = "Type_combo";
            this.Type_combo.Size = new System.Drawing.Size(121, 21);
            this.Type_combo.TabIndex = 3;
            this.Type_combo.Text = "Process_type";
            // 
            // Deallocate_btn
            // 
            this.Deallocate_btn.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Deallocate_btn.Location = new System.Drawing.Point(184, 109);
            this.Deallocate_btn.Name = "Deallocate_btn";
            this.Deallocate_btn.Size = new System.Drawing.Size(102, 28);
            this.Deallocate_btn.TabIndex = 3;
            this.Deallocate_btn.Text = "De_Allocate";
            this.Deallocate_btn.UseVisualStyleBackColor = false;
            this.Deallocate_btn.Click += new System.EventHandler(this.Deallocate_btn_Click);
            // 
            // Pid_txt
            // 
            this.Pid_txt.Location = new System.Drawing.Point(64, 65);
            this.Pid_txt.Name = "Pid_txt";
            this.Pid_txt.Size = new System.Drawing.Size(107, 20);
            this.Pid_txt.TabIndex = 0;
            // 
            // Allocation
            // 
            this.Allocation.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Allocation.Controls.Add(this.seg_txt);
            this.Allocation.Controls.Add(this.Num_label);
            this.Allocation.Controls.Add(this.Add_btn);
            this.Allocation.Location = new System.Drawing.Point(12, 177);
            this.Allocation.Name = "Allocation";
            this.Allocation.Size = new System.Drawing.Size(301, 158);
            this.Allocation.TabIndex = 3;
            this.Allocation.TabStop = false;
            this.Allocation.Text = "Allocation";
            // 
            // seg_txt
            // 
            this.seg_txt.Location = new System.Drawing.Point(64, 36);
            this.seg_txt.Name = "seg_txt";
            this.seg_txt.Size = new System.Drawing.Size(107, 20);
            this.seg_txt.TabIndex = 6;
            // 
            // Num_label
            // 
            this.Num_label.AutoSize = true;
            this.Num_label.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Num_label.Location = new System.Drawing.Point(6, 39);
            this.Num_label.Name = "Num_label";
            this.Num_label.Size = new System.Drawing.Size(52, 13);
            this.Num_label.TabIndex = 5;
            this.Num_label.Text = "Num_Seg";
            // 
            // Add_btn
            // 
            this.Add_btn.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Add_btn.Location = new System.Drawing.Point(184, 124);
            this.Add_btn.Name = "Add_btn";
            this.Add_btn.Size = new System.Drawing.Size(102, 28);
            this.Add_btn.TabIndex = 4;
            this.Add_btn.Text = "Add new process";
            this.Add_btn.UseVisualStyleBackColor = false;
            this.Add_btn.Click += new System.EventHandler(this.Add_btn_Click);
            // 
            // seg_box
            // 
            this.seg_box.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.seg_box.Controls.Add(this.comboBox1);
            this.seg_box.Controls.Add(this.label2);
            this.seg_box.Controls.Add(this.allo_btn);
            this.seg_box.Controls.Add(this.seg_table);
            this.seg_box.Location = new System.Drawing.Point(12, 356);
            this.seg_box.Name = "seg_box";
            this.seg_box.Size = new System.Drawing.Size(301, 259);
            this.seg_box.TabIndex = 0;
            this.seg_box.TabStop = false;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "First Fit",
            "Best Fit"});
            this.comboBox1.Location = new System.Drawing.Point(6, 224);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label2.Location = new System.Drawing.Point(6, 208);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Allocation Method :";
            // 
            // allo_btn
            // 
            this.allo_btn.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.allo_btn.Location = new System.Drawing.Point(188, 219);
            this.allo_btn.Name = "allo_btn";
            this.allo_btn.Size = new System.Drawing.Size(98, 28);
            this.allo_btn.TabIndex = 6;
            this.allo_btn.Text = "Allocate";
            this.allo_btn.UseVisualStyleBackColor = false;
            this.allo_btn.Click += new System.EventHandler(this.allo_btn_Click);
            // 
            // seg_table
            // 
            this.seg_table.AutoScroll = true;
            this.seg_table.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.seg_table.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.seg_table.ColumnCount = 1;
            this.seg_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.77049F));
            this.seg_table.Location = new System.Drawing.Point(6, 16);
            this.seg_table.Name = "seg_table";
            this.seg_table.RowCount = 1;
            this.seg_table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.seg_table.Size = new System.Drawing.Size(280, 181);
            this.seg_table.TabIndex = 5;
            // 
            // Gant_chart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(581, 435);
            this.Controls.Add(this.seg_box);
            this.Controls.Add(this.Allocation);
            this.Controls.Add(this.Deallocation);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Gant_chart";
            this.Text = "Gant_chart";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Gant_chart_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Gant_chart_Paint);
            this.Deallocation.ResumeLayout(false);
            this.Deallocation.PerformLayout();
            this.Allocation.ResumeLayout(false);
            this.Allocation.PerformLayout();
            this.seg_box.ResumeLayout(false);
            this.seg_box.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox Deallocation;
        private System.Windows.Forms.ComboBox Type_combo;
        private System.Windows.Forms.Button Deallocate_btn;
        private System.Windows.Forms.TextBox Pid_txt;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label Pid_label;
        private System.Windows.Forms.GroupBox Allocation;
        private System.Windows.Forms.TextBox seg_txt;
        private System.Windows.Forms.Label Num_label;
        private System.Windows.Forms.Button Add_btn;
        private System.Windows.Forms.GroupBox seg_box;
        private System.Windows.Forms.Button allo_btn;

       

        private System.Windows.Forms.TableLayoutPanel seg_table;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button de_allo_btn;
    }
}