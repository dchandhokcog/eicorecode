namespace EnvInt.Win32.Controls.Controls
{
    partial class LocationDescriptionUserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocationDescriptionUserControl));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFloor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxEquipment = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxDirection = new System.Windows.Forms.ComboBox();
            this.txtDistance = new System.Windows.Forms.TextBox();
            this.txtElevation = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.label5, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtFloor, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxEquipment, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxDirection, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtDistance, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtElevation, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // txtFloor
            // 
            resources.ApplyResources(this.txtFloor, "txtFloor");
            this.txtFloor.Name = "txtFloor";
            this.txtFloor.TextChanged += new System.EventHandler(this.txtFloor_TextChanged);
            this.txtFloor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFloor_KeyPress);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // comboBoxEquipment
            // 
            resources.ApplyResources(this.comboBoxEquipment, "comboBoxEquipment");
            this.comboBoxEquipment.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxEquipment.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.tableLayoutPanel1.SetColumnSpan(this.comboBoxEquipment, 2);
            this.comboBoxEquipment.FormattingEnabled = true;
            this.comboBoxEquipment.Name = "comboBoxEquipment";
            this.comboBoxEquipment.TextChanged += new System.EventHandler(this.comboBoxEquipment_TextChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // comboBoxDirection
            // 
            resources.ApplyResources(this.comboBoxDirection, "comboBoxDirection");
            this.comboBoxDirection.FormattingEnabled = true;
            this.comboBoxDirection.Name = "comboBoxDirection";
            this.comboBoxDirection.SelectedIndexChanged += new System.EventHandler(this.comboBoxDirection_DataChanged);
            this.comboBoxDirection.TextUpdate += new System.EventHandler(this.comboBoxDirection_DataChanged);
            // 
            // txtDistance
            // 
            resources.ApplyResources(this.txtDistance, "txtDistance");
            this.txtDistance.Name = "txtDistance";
            this.txtDistance.TextChanged += new System.EventHandler(this.txtDistance_TextChanged);
            this.txtDistance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDistance_KeyPress);
            // 
            // txtElevation
            // 
            resources.ApplyResources(this.txtElevation, "txtElevation");
            this.txtElevation.Name = "txtElevation";
            this.txtElevation.TextChanged += new System.EventHandler(this.txtElevation_TextChanged);
            this.txtElevation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.floatvalidation_KeyPress);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // LocationDescriptionUserControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "LocationDescriptionUserControl";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFloor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtElevation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxEquipment;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxDirection;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDistance;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}
