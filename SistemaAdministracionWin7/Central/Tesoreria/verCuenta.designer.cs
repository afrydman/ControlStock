namespace Central.Tesoreria
{
    partial class verCuenta
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
            this.groupBanco = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbtipoBanco = new System.Windows.Forms.ComboBox();
            this.cmbBancos = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtsucursal = new System.Windows.Forms.TextBox();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
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
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBanco.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBanco
            // 
            this.groupBanco.Controls.Add(this.label10);
            this.groupBanco.Controls.Add(this.label9);
            this.groupBanco.Controls.Add(this.cmbtipoBanco);
            this.groupBanco.Controls.Add(this.cmbBancos);
            this.groupBanco.Controls.Add(this.label2);
            this.groupBanco.Controls.Add(this.txtsucursal);
            this.groupBanco.Enabled = false;
            this.groupBanco.Location = new System.Drawing.Point(23, 71);
            this.groupBanco.Name = "groupBanco";
            this.groupBanco.Size = new System.Drawing.Size(406, 103);
            this.groupBanco.TabIndex = 5;
            this.groupBanco.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 13);
            this.label10.TabIndex = 8;
            this.label10.Text = "Banco";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 69);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Tipo";
            // 
            // cmbtipoBanco
            // 
            this.cmbtipoBanco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbtipoBanco.FormattingEnabled = true;
            this.cmbtipoBanco.Items.AddRange(new object[] {
            "-",
            "Cuenta Corriente",
            "Caja Ahorro"});
            this.cmbtipoBanco.Location = new System.Drawing.Point(149, 66);
            this.cmbtipoBanco.Name = "cmbtipoBanco";
            this.cmbtipoBanco.Size = new System.Drawing.Size(233, 21);
            this.cmbtipoBanco.TabIndex = 6;
            // 
            // cmbBancos
            // 
            this.cmbBancos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBancos.FormattingEnabled = true;
            this.cmbBancos.Location = new System.Drawing.Point(149, 13);
            this.cmbBancos.Name = "cmbBancos";
            this.cmbBancos.Size = new System.Drawing.Size(233, 21);
            this.cmbBancos.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Sucursal";
            // 
            // txtsucursal
            // 
            this.txtsucursal.Location = new System.Drawing.Point(149, 40);
            this.txtsucursal.Name = "txtsucursal";
            this.txtsucursal.Size = new System.Drawing.Size(233, 20);
            this.txtsucursal.TabIndex = 5;
            // 
            // cmbTipo
            // 
            this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipo.Enabled = false;
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Items.AddRange(new object[] {
            "Banco",
            "Otra"});
            this.cmbTipo.Location = new System.Drawing.Point(172, 47);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(233, 21);
            this.cmbTipo.TabIndex = 6;
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
            this.groupBox4.Enabled = false;
            this.groupBox4.Location = new System.Drawing.Point(23, 183);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(406, 186);
            this.groupBox4.TabIndex = 8;
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
            // txtDescripcion
            // 
            this.txtDescripcion.Enabled = false;
            this.txtDescripcion.Location = new System.Drawing.Point(172, 18);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(233, 20);
            this.txtDescripcion.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(26, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Descripcion";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(26, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Tipo";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(23, 387);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "Anular";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(309, 387);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "Ver Movimientos";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // verCuenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 432);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.cmbTipo);
            this.Controls.Add(this.groupBanco);
            this.Name = "verCuenta";
            this.Text = "Cuenta";
            this.Load += new System.EventHandler(this.verCuenta_Load);
            this.groupBanco.ResumeLayout(false);
            this.groupBanco.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBanco;
        private System.Windows.Forms.ComboBox cmbBancos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtsucursal;
        private System.Windows.Forms.ComboBox cmbTipo;
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
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbtipoBanco;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
    }
}