namespace SharedForms.Ventas
{
    partial class agregarFormaPago
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCuotas = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtImporteRecargo = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtImporte = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtRecargo = new System.Windows.Forms.TextBox();
            this.txtcupon = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtlote = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFormaPago = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbCuotas);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.txtImporteRecargo);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.txtImporte);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.txtRecargo);
            this.groupBox1.Controls.Add(this.txtcupon);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtlote);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbFormaPago);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(577, 158);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(455, 119);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 23);
            this.button1.TabIndex = 53;
            this.button1.Text = "Agregar (F3)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 51;
            this.label2.Text = "Cuotas";
            // 
            // cmbCuotas
            // 
            this.cmbCuotas.AutoCompleteCustomSource.AddRange(new string[] {
            "1 CUOTA",
            "2 CUOTAS",
            "3 CUOTAS",
            "4 CUOTAS",
            "5 CUOTAS",
            "6 CUOTAS",
            "7 CUOTAS",
            "8 CUOTAS",
            "9 CUOTAS",
            "10 CUOTAS",
            "11 CUOTAS",
            "12 CUOTAS"});
            this.cmbCuotas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCuotas.FormattingEnabled = true;
            this.cmbCuotas.Items.AddRange(new object[] {
            "-",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.cmbCuotas.Location = new System.Drawing.Point(94, 46);
            this.cmbCuotas.Name = "cmbCuotas";
            this.cmbCuotas.Size = new System.Drawing.Size(99, 21);
            this.cmbCuotas.TabIndex = 52;
            this.cmbCuotas.SelectedIndexChanged += new System.EventHandler(this.cmbCuotas_SelectedIndexChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(364, 85);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(126, 13);
            this.label22.TabIndex = 50;
            this.label22.Text = "Importe con Recargo";
            // 
            // txtImporteRecargo
            // 
            this.txtImporteRecargo.Enabled = false;
            this.txtImporteRecargo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImporteRecargo.Location = new System.Drawing.Point(496, 82);
            this.txtImporteRecargo.Name = "txtImporteRecargo";
            this.txtImporteRecargo.Size = new System.Drawing.Size(66, 20);
            this.txtImporteRecargo.TabIndex = 49;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(212, 85);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(42, 13);
            this.label21.TabIndex = 48;
            this.label21.Text = "Importe";
            // 
            // txtImporte
            // 
            this.txtImporte.Location = new System.Drawing.Point(260, 82);
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.Size = new System.Drawing.Size(66, 20);
            this.txtImporte.TabIndex = 47;
            this.txtImporte.TextChanged += new System.EventHandler(this.txtImporte_TextChanged);
            this.txtImporte.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtImporte_KeyPress);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(23, 85);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(65, 13);
            this.label20.TabIndex = 46;
            this.label20.Text = "Recargo (%)";
            // 
            // txtRecargo
            // 
            this.txtRecargo.Location = new System.Drawing.Point(105, 82);
            this.txtRecargo.Name = "txtRecargo";
            this.txtRecargo.ReadOnly = true;
            this.txtRecargo.Size = new System.Drawing.Size(63, 20);
            this.txtRecargo.TabIndex = 45;
            this.txtRecargo.TabStop = false;
            // 
            // txtcupon
            // 
            this.txtcupon.Location = new System.Drawing.Point(367, 45);
            this.txtcupon.MaxLength = 4;
            this.txtcupon.Name = "txtcupon";
            this.txtcupon.Size = new System.Drawing.Size(45, 20);
            this.txtcupon.TabIndex = 42;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(323, 48);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(38, 13);
            this.label14.TabIndex = 44;
            this.label14.Text = "Cupon";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(227, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(28, 13);
            this.label8.TabIndex = 43;
            this.label8.Text = "Lote";
            // 
            // txtlote
            // 
            this.txtlote.Location = new System.Drawing.Point(261, 45);
            this.txtlote.MaxLength = 4;
            this.txtlote.Name = "txtlote";
            this.txtlote.Size = new System.Drawing.Size(42, 20);
            this.txtlote.TabIndex = 41;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "Forma de pago";
            // 
            // cmbFormaPago
            // 
            this.cmbFormaPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormaPago.FormattingEnabled = true;
            this.cmbFormaPago.Location = new System.Drawing.Point(94, 19);
            this.cmbFormaPago.Name = "cmbFormaPago";
            this.cmbFormaPago.Size = new System.Drawing.Size(232, 21);
            this.cmbFormaPago.TabIndex = 40;
            this.cmbFormaPago.SelectedIndexChanged += new System.EventHandler(this.cmbFormaPago_SelectedIndexChanged);
            // 
            // agregarFormaPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 182);
            this.Controls.Add(this.groupBox1);
            this.KeyPreview = true;
            this.Name = "agregarFormaPago";
            this.Text = "Agregar Forma Pago";
            this.Load += new System.EventHandler(this.agregarFormaPago_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.agregarFormaPago_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCuotas;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtImporteRecargo;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtImporte;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtRecargo;
        private System.Windows.Forms.TextBox txtcupon;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtlote;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbFormaPago;

    }
}