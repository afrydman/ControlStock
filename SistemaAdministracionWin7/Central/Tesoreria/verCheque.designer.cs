namespace Central.Tesoreria
{
    partial class verCheque
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
            this.ingreso = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbBancos = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioDiferido = new System.Windows.Forms.RadioButton();
            this.radioComun = new System.Windows.Forms.RadioButton();
            this.cobro = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbOrigen = new System.Windows.Forms.ComboBox();
            this.lblInterno = new System.Windows.Forms.Label();
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tabla = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.entregasVarias = new System.Windows.Forms.GroupBox();
            this.txtObsOut = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.fechaOut = new System.Windows.Forms.DateTimePicker();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).BeginInit();
            this.entregasVarias.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ingreso);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cmbBancos);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.cmbOrigen);
            this.groupBox1.Controls.Add(this.lblInterno);
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
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(657, 278);
            this.groupBox1.TabIndex = 50;
            this.groupBox1.TabStop = false;
            // 
            // ingreso
            // 
            this.ingreso.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.ingreso.Location = new System.Drawing.Point(522, 22);
            this.ingreso.Name = "ingreso";
            this.ingreso.Size = new System.Drawing.Size(100, 20);
            this.ingreso.TabIndex = 78;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(428, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 79;
            this.label6.Text = "Fecha Ingreso";
            // 
            // cmbBancos
            // 
            this.cmbBancos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBancos.FormattingEnabled = true;
            this.cmbBancos.Location = new System.Drawing.Point(99, 132);
            this.cmbBancos.Name = "cmbBancos";
            this.cmbBancos.Size = new System.Drawing.Size(224, 21);
            this.cmbBancos.TabIndex = 77;
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
            this.groupBox2.Location = new System.Drawing.Point(226, 71);
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
            this.radioDiferido.TabIndex = 76;
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
            this.radioComun.TabIndex = 75;
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
            this.cobro.TabIndex = 73;
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
            this.cmbOrigen.TabIndex = 69;
            this.cmbOrigen.SelectedIndexChanged += new System.EventHandler(this.cmbOrigen_SelectedIndexChanged);
            // 
            // lblInterno
            // 
            this.lblInterno.AutoSize = true;
            this.lblInterno.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblInterno.Location = new System.Drawing.Point(97, 43);
            this.lblInterno.Name = "lblInterno";
            this.lblInterno.Size = new System.Drawing.Size(170, 20);
            this.lblInterno.TabIndex = 67;
            this.lblInterno.Text = "ERROR CONEXION";
            // 
            // txtObs
            // 
            this.txtObs.Location = new System.Drawing.Point(101, 196);
            this.txtObs.Multiline = true;
            this.txtObs.Name = "txtObs";
            this.txtObs.Size = new System.Drawing.Size(422, 69);
            this.txtObs.TabIndex = 65;
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
            this.label1.Location = new System.Drawing.Point(7, 48);
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
            this.emision.Size = new System.Drawing.Size(100, 20);
            this.emision.TabIndex = 3;
            // 
            // txtmonto
            // 
            this.txtmonto.Location = new System.Drawing.Point(521, 166);
            this.txtmonto.Name = "txtmonto";
            this.txtmonto.Size = new System.Drawing.Size(100, 20);
            this.txtmonto.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(372, 169);
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
            this.txtnroCheque.Location = new System.Drawing.Point(415, 131);
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tabla);
            this.groupBox3.Location = new System.Drawing.Point(12, 295);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(657, 187);
            this.groupBox3.TabIndex = 52;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Movimientos";
            // 
            // tabla
            // 
            this.tabla.AllowUserToAddRows = false;
            this.tabla.AllowUserToDeleteRows = false;
            this.tabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tabla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.tabla.Location = new System.Drawing.Point(14, 19);
            this.tabla.Name = "tabla";
            this.tabla.ReadOnly = true;
            this.tabla.Size = new System.Drawing.Size(558, 146);
            this.tabla.TabIndex = 52;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Fecha";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Operacion";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 400;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(40, 175);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(151, 23);
            this.button2.TabIndex = 81;
            this.button2.Text = "Marcar como Rechazado";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(351, 175);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 23);
            this.button1.TabIndex = 80;
            this.button1.Text = "Marcar como Anulado";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(197, 175);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(143, 23);
            this.button3.TabIndex = 82;
            this.button3.Text = "Marcar como Entregado";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // entregasVarias
            // 
            this.entregasVarias.Controls.Add(this.txtObsOut);
            this.entregasVarias.Controls.Add(this.label8);
            this.entregasVarias.Controls.Add(this.label7);
            this.entregasVarias.Controls.Add(this.fechaOut);
            this.entregasVarias.Controls.Add(this.button2);
            this.entregasVarias.Controls.Add(this.button3);
            this.entregasVarias.Controls.Add(this.button1);
            this.entregasVarias.Location = new System.Drawing.Point(675, 12);
            this.entregasVarias.Name = "entregasVarias";
            this.entregasVarias.Size = new System.Drawing.Size(477, 220);
            this.entregasVarias.TabIndex = 83;
            this.entregasVarias.TabStop = false;
            // 
            // txtObsOut
            // 
            this.txtObsOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObsOut.Location = new System.Drawing.Point(100, 83);
            this.txtObsOut.Multiline = true;
            this.txtObsOut.Name = "txtObsOut";
            this.txtObsOut.Size = new System.Drawing.Size(355, 69);
            this.txtObsOut.TabIndex = 85;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 86);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 13);
            this.label8.TabIndex = 86;
            this.label8.Text = "Observaciones";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(47, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 84;
            this.label7.Text = "Fecha";
            // 
            // fechaOut
            // 
            this.fechaOut.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fechaOut.Location = new System.Drawing.Point(100, 40);
            this.fechaOut.Name = "fechaOut";
            this.fechaOut.Size = new System.Drawing.Size(100, 20);
            this.fechaOut.TabIndex = 83;
            // 
            // verCheque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1166, 557);
            this.Controls.Add(this.entregasVarias);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "verCheque";
            this.Text = "Ver Cheque";
            this.Load += new System.EventHandler(this.nuevoCheque_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).EndInit();
            this.entregasVarias.ResumeLayout(false);
            this.entregasVarias.PerformLayout();
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
        private System.Windows.Forms.Label lblInterno;
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
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView tabla;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DateTimePicker ingreso;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox entregasVarias;
        private System.Windows.Forms.TextBox txtObsOut;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker fechaOut;
    }
}