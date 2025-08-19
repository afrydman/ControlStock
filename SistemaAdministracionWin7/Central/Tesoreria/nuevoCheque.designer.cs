namespace Central.Tesoreria
{
    partial class nuevoCheque
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
            this.txtInterno = new System.Windows.Forms.TextBox();
            this.cmbBancos = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioDiferido = new System.Windows.Forms.RadioButton();
            this.radioComun = new System.Windows.Forms.RadioButton();
            this.cobro = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbOrigen = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtObs = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.emision = new System.Windows.Forms.DateTimePicker();
            this.txtmonto = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTitularCuenta = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtnroCheque = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.ingreso = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ingreso);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtInterno);
            this.groupBox1.Controls.Add(this.cmbBancos);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.cmbOrigen);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.txtObs);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.emision);
            this.groupBox1.Controls.Add(this.txtmonto);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtTitularCuenta);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtnroCheque);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(657, 300);
            this.groupBox1.TabIndex = 50;
            this.groupBox1.TabStop = false;
            // 
            // txtInterno
            // 
            this.txtInterno.Location = new System.Drawing.Point(99, 62);
            this.txtInterno.Name = "txtInterno";
            this.txtInterno.Size = new System.Drawing.Size(122, 20);
            this.txtInterno.TabIndex = 77;
            // 
            // cmbBancos
            // 
            this.cmbBancos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBancos.FormattingEnabled = true;
            this.cmbBancos.Location = new System.Drawing.Point(99, 132);
            this.cmbBancos.Name = "cmbBancos";
            this.cmbBancos.Size = new System.Drawing.Size(224, 21);
            this.cmbBancos.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 76;
            this.label9.Text = "Origen";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioDiferido);
            this.groupBox2.Controls.Add(this.radioComun);
            this.groupBox2.Controls.Add(this.cobro);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(256, 73);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(395, 53);
            this.groupBox2.TabIndex = 75;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tipo";
            // 
            // radioDiferido
            // 
            this.radioDiferido.AutoSize = true;
            this.radioDiferido.Location = new System.Drawing.Point(99, 28);
            this.radioDiferido.Name = "radioDiferido";
            this.radioDiferido.Size = new System.Drawing.Size(61, 17);
            this.radioDiferido.TabIndex = 4;
            this.radioDiferido.TabStop = true;
            this.radioDiferido.Text = "Diferido";
            this.radioDiferido.UseVisualStyleBackColor = true;
            // 
            // radioComun
            // 
            this.radioComun.AutoSize = true;
            this.radioComun.Checked = true;
            this.radioComun.Location = new System.Drawing.Point(15, 28);
            this.radioComun.Name = "radioComun";
            this.radioComun.Size = new System.Drawing.Size(58, 17);
            this.radioComun.TabIndex = 3;
            this.radioComun.TabStop = true;
            this.radioComun.Text = "Comun";
            this.radioComun.UseVisualStyleBackColor = true;
            this.radioComun.CheckedChanged += new System.EventHandler(this.radioComun_CheckedChanged);
            // 
            // cobro
            // 
            this.cobro.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.cobro.Location = new System.Drawing.Point(272, 28);
            this.cobro.Name = "cobro";
            this.cobro.Size = new System.Drawing.Size(100, 20);
            this.cobro.TabIndex = 5;
            this.cobro.ValueChanged += new System.EventHandler(this.cobro_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(198, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 74;
            this.label4.Text = "Fecha Cobro";
            // 
            // cmbOrigen
            // 
            this.cmbOrigen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrigen.FormattingEnabled = true;
            this.cmbOrigen.Location = new System.Drawing.Point(99, 19);
            this.cmbOrigen.Name = "cmbOrigen";
            this.cmbOrigen.Size = new System.Drawing.Size(224, 21);
            this.cmbOrigen.TabIndex = 1;
            this.cmbOrigen.SelectedIndexChanged += new System.EventHandler(this.cmbOrigen_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(576, 247);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 33);
            this.button1.TabIndex = 11;
            this.button1.Text = "Generar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtObs
            // 
            this.txtObs.Location = new System.Drawing.Point(101, 196);
            this.txtObs.Multiline = true;
            this.txtObs.Name = "txtObs";
            this.txtObs.Size = new System.Drawing.Size(422, 84);
            this.txtObs.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 199);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 66;
            this.label5.Text = "Observaciones";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 64;
            this.label1.Text = "N°  interno";
            // 
            // emision
            // 
            this.emision.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.emision.Location = new System.Drawing.Point(101, 99);
            this.emision.Name = "emision";
            this.emision.Size = new System.Drawing.Size(120, 20);
            this.emision.TabIndex = 2;
            // 
            // txtmonto
            // 
            this.txtmonto.Location = new System.Drawing.Point(444, 167);
            this.txtmonto.Name = "txtmonto";
            this.txtmonto.Size = new System.Drawing.Size(100, 20);
            this.txtmonto.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(379, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 55;
            this.label2.Text = "Monto";
            // 
            // txtTitularCuenta
            // 
            this.txtTitularCuenta.Location = new System.Drawing.Point(101, 170);
            this.txtTitularCuenta.Name = "txtTitularCuenta";
            this.txtTitularCuenta.Size = new System.Drawing.Size(243, 20);
            this.txtTitularCuenta.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 53;
            this.label3.Text = "Titular Cuenta";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 138);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 13);
            this.label11.TabIndex = 50;
            this.label11.Text = "Banco Emisor";
            // 
            // txtnroCheque
            // 
            this.txtnroCheque.Location = new System.Drawing.Point(444, 132);
            this.txtnroCheque.Name = "txtnroCheque";
            this.txtnroCheque.Size = new System.Drawing.Size(207, 20);
            this.txtnroCheque.TabIndex = 7;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(350, 135);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 13);
            this.label13.TabIndex = 48;
            this.label13.Text = "N° Cheque";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 102);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(76, 13);
            this.label14.TabIndex = 47;
            this.label14.Text = "Fecha Emision";
            // 
            // ingreso
            // 
            this.ingreso.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.ingreso.Location = new System.Drawing.Point(528, 22);
            this.ingreso.Name = "ingreso";
            this.ingreso.Size = new System.Drawing.Size(120, 20);
            this.ingreso.TabIndex = 78;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(434, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 79;
            this.label6.Text = "Fecha Ingreso";
            // 
            // nuevoCheque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 323);
            this.Controls.Add(this.groupBox1);
            this.Name = "nuevoCheque";
            this.Text = "Nuevo Cheque";
            this.Load += new System.EventHandler(this.nuevoCheque_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbBancos;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioDiferido;
        private System.Windows.Forms.RadioButton radioComun;
        private System.Windows.Forms.DateTimePicker cobro;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbOrigen;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtObs;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker emision;
        private System.Windows.Forms.TextBox txtmonto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTitularCuenta;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtnroCheque;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtInterno;
        private System.Windows.Forms.DateTimePicker ingreso;
        private System.Windows.Forms.Label label6;
    }
}