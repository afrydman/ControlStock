namespace Central.Tesoreria
{
    partial class nuevaCuenta
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
            this.label3 = new System.Windows.Forms.Label();
            this.txtdescripcion = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioTarjeta = new System.Windows.Forms.RadioButton();
            this.radioOtra = new System.Windows.Forms.RadioButton();
            this.radioCartera = new System.Windows.Forms.RadioButton();
            this.radioBanco = new System.Windows.Forms.RadioButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.GroupTipoCuentaBancaria = new System.Windows.Forms.GroupBox();
            this.radioCuentaCorriente = new System.Windows.Forms.RadioButton();
            this.radioCajaAhorro = new System.Windows.Forms.RadioButton();
            this.groupBanco = new System.Windows.Forms.GroupBox();
            this.cmbBancos = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtsucursal = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtsaldo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtlimite = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTitular = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCBU = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtcuenta = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.GroupTipoCuentaBancaria.SuspendLayout();
            this.groupBanco.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Descripcion";
            // 
            // txtdescripcion
            // 
            this.txtdescripcion.Location = new System.Drawing.Point(141, 12);
            this.txtdescripcion.Name = "txtdescripcion";
            this.txtdescripcion.Size = new System.Drawing.Size(122, 20);
            this.txtdescripcion.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(21, 52);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(438, 243);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(430, 217);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Tipo Cuenta";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioTarjeta);
            this.groupBox2.Controls.Add(this.radioOtra);
            this.groupBox2.Controls.Add(this.radioCartera);
            this.groupBox2.Controls.Add(this.radioBanco);
            this.groupBox2.Location = new System.Drawing.Point(6, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(136, 163);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tipo de cuenta";
            // 
            // radioTarjeta
            // 
            this.radioTarjeta.AutoSize = true;
            this.radioTarjeta.Enabled = false;
            this.radioTarjeta.Location = new System.Drawing.Point(33, 98);
            this.radioTarjeta.Name = "radioTarjeta";
            this.radioTarjeta.Size = new System.Drawing.Size(58, 17);
            this.radioTarjeta.TabIndex = 2;
            this.radioTarjeta.TabStop = true;
            this.radioTarjeta.Text = "Tarjeta";
            this.radioTarjeta.UseVisualStyleBackColor = true;
            // 
            // radioOtra
            // 
            this.radioOtra.AutoSize = true;
            this.radioOtra.Location = new System.Drawing.Point(32, 130);
            this.radioOtra.Name = "radioOtra";
            this.radioOtra.Size = new System.Drawing.Size(45, 17);
            this.radioOtra.TabIndex = 3;
            this.radioOtra.TabStop = true;
            this.radioOtra.Text = "Otra";
            this.radioOtra.UseVisualStyleBackColor = true;
            // 
            // radioCartera
            // 
            this.radioCartera.AutoSize = true;
            this.radioCartera.Enabled = false;
            this.radioCartera.Location = new System.Drawing.Point(32, 64);
            this.radioCartera.Name = "radioCartera";
            this.radioCartera.Size = new System.Drawing.Size(59, 17);
            this.radioCartera.TabIndex = 1;
            this.radioCartera.TabStop = true;
            this.radioCartera.Text = "Cartera";
            this.radioCartera.UseVisualStyleBackColor = true;
            // 
            // radioBanco
            // 
            this.radioBanco.AutoSize = true;
            this.radioBanco.Location = new System.Drawing.Point(32, 33);
            this.radioBanco.Name = "radioBanco";
            this.radioBanco.Size = new System.Drawing.Size(56, 17);
            this.radioBanco.TabIndex = 0;
            this.radioBanco.TabStop = true;
            this.radioBanco.Text = "Banco";
            this.radioBanco.UseVisualStyleBackColor = true;
            this.radioBanco.CheckedChanged += new System.EventHandler(this.radioBanco_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.GroupTipoCuentaBancaria);
            this.tabPage2.Controls.Add(this.groupBanco);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(430, 217);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Datos Banco";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // GroupTipoCuentaBancaria
            // 
            this.GroupTipoCuentaBancaria.Controls.Add(this.radioCuentaCorriente);
            this.GroupTipoCuentaBancaria.Controls.Add(this.radioCajaAhorro);
            this.GroupTipoCuentaBancaria.Enabled = false;
            this.GroupTipoCuentaBancaria.Location = new System.Drawing.Point(23, 115);
            this.GroupTipoCuentaBancaria.Name = "GroupTipoCuentaBancaria";
            this.GroupTipoCuentaBancaria.Size = new System.Drawing.Size(159, 81);
            this.GroupTipoCuentaBancaria.TabIndex = 5;
            this.GroupTipoCuentaBancaria.TabStop = false;
            this.GroupTipoCuentaBancaria.Text = "Tipo cuenta bancaria";
            // 
            // radioCuentaCorriente
            // 
            this.radioCuentaCorriente.AutoSize = true;
            this.radioCuentaCorriente.Location = new System.Drawing.Point(6, 42);
            this.radioCuentaCorriente.Name = "radioCuentaCorriente";
            this.radioCuentaCorriente.Size = new System.Drawing.Size(104, 17);
            this.radioCuentaCorriente.TabIndex = 7;
            this.radioCuentaCorriente.TabStop = true;
            this.radioCuentaCorriente.Text = "Cuenta Corriente";
            this.radioCuentaCorriente.UseVisualStyleBackColor = true;
            // 
            // radioCajaAhorro
            // 
            this.radioCajaAhorro.AutoSize = true;
            this.radioCajaAhorro.Location = new System.Drawing.Point(6, 19);
            this.radioCajaAhorro.Name = "radioCajaAhorro";
            this.radioCajaAhorro.Size = new System.Drawing.Size(94, 17);
            this.radioCajaAhorro.TabIndex = 6;
            this.radioCajaAhorro.TabStop = true;
            this.radioCajaAhorro.Text = "Caja de ahorro";
            this.radioCajaAhorro.UseVisualStyleBackColor = true;
            // 
            // groupBanco
            // 
            this.groupBanco.Controls.Add(this.cmbBancos);
            this.groupBanco.Controls.Add(this.label2);
            this.groupBanco.Controls.Add(this.txtsucursal);
            this.groupBanco.Enabled = false;
            this.groupBanco.Location = new System.Drawing.Point(23, 11);
            this.groupBanco.Name = "groupBanco";
            this.groupBanco.Size = new System.Drawing.Size(401, 98);
            this.groupBanco.TabIndex = 4;
            this.groupBanco.TabStop = false;
            this.groupBanco.Text = "Seleccione el banco";
            // 
            // cmbBancos
            // 
            this.cmbBancos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBancos.FormattingEnabled = true;
            this.cmbBancos.Location = new System.Drawing.Point(6, 31);
            this.cmbBancos.Name = "cmbBancos";
            this.cmbBancos.Size = new System.Drawing.Size(376, 21);
            this.cmbBancos.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Sucursal";
            // 
            // txtsucursal
            // 
            this.txtsucursal.Location = new System.Drawing.Point(149, 58);
            this.txtsucursal.Name = "txtsucursal";
            this.txtsucursal.Size = new System.Drawing.Size(233, 20);
            this.txtsucursal.TabIndex = 5;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox4);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(430, 217);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Datos Cuenta";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.txtsaldo);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.txtlimite);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.txtTitular);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.txtCBU);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.txtcuenta);
            this.groupBox4.Location = new System.Drawing.Point(6, 16);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(406, 186);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Datos de cuenta";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Saldo actual";
            // 
            // txtsaldo
            // 
            this.txtsaldo.Location = new System.Drawing.Point(291, 111);
            this.txtsaldo.Name = "txtsaldo";
            this.txtsaldo.Size = new System.Drawing.Size(100, 20);
            this.txtsaldo.TabIndex = 11;
            this.txtsaldo.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 140);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Limite descubierto";
            // 
            // txtlimite
            // 
            this.txtlimite.Location = new System.Drawing.Point(291, 137);
            this.txtlimite.Name = "txtlimite";
            this.txtlimite.Size = new System.Drawing.Size(100, 20);
            this.txtlimite.TabIndex = 12;
            this.txtlimite.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Titular";
            // 
            // txtTitular
            // 
            this.txtTitular.Location = new System.Drawing.Point(123, 33);
            this.txtTitular.Name = "txtTitular";
            this.txtTitular.Size = new System.Drawing.Size(268, 20);
            this.txtTitular.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "CBU";
            // 
            // txtCBU
            // 
            this.txtCBU.Location = new System.Drawing.Point(123, 85);
            this.txtCBU.Name = "txtCBU";
            this.txtCBU.Size = new System.Drawing.Size(268, 20);
            this.txtCBU.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "N cuenta";
            // 
            // txtcuenta
            // 
            this.txtcuenta.Location = new System.Drawing.Point(123, 59);
            this.txtcuenta.Name = "txtcuenta";
            this.txtcuenta.Size = new System.Drawing.Size(268, 20);
            this.txtcuenta.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(380, 313);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "Generar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // nuevaCuenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 361);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtdescripcion);
            this.Name = "nuevaCuenta";
            this.Text = "Nueva Cuenta";
            this.Load += new System.EventHandler(this.nuevaCuenta_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.GroupTipoCuentaBancaria.ResumeLayout(false);
            this.GroupTipoCuentaBancaria.PerformLayout();
            this.groupBanco.ResumeLayout(false);
            this.groupBanco.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtdescripcion;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioCartera;
        private System.Windows.Forms.RadioButton radioBanco;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox GroupTipoCuentaBancaria;
        private System.Windows.Forms.RadioButton radioCuentaCorriente;
        private System.Windows.Forms.RadioButton radioCajaAhorro;
        private System.Windows.Forms.GroupBox groupBanco;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtsucursal;
        private System.Windows.Forms.RadioButton radioOtra;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtsaldo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtlimite;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTitular;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCBU;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtcuenta;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbBancos;
        private System.Windows.Forms.RadioButton radioTarjeta;
    }
}