namespace Central.Tesoreria
{
    partial class Depositocuenta
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblnro = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.radioCheque = new System.Windows.Forms.RadioButton();
            this.radioEfectivo = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbCheques = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtObs = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCajaOrigen = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbCuentaDestino = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtImporte = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblnro);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.radioCheque);
            this.groupBox2.Controls.Add(this.radioEfectivo);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cmbCheques);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtObs);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.dateTimePicker1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cmbCajaOrigen);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cmbCuentaDestino);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.txtImporte);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(371, 321);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            // 
            // lblnro
            // 
            this.lblnro.AutoSize = true;
            this.lblnro.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblnro.Location = new System.Drawing.Point(106, 16);
            this.lblnro.Name = "lblnro";
            this.lblnro.Size = new System.Drawing.Size(170, 20);
            this.lblnro.TabIndex = 40;
            this.lblnro.Text = "ERROR CONEXION";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 13);
            this.label7.TabIndex = 39;
            this.label7.Text = "N";
            // 
            // radioCheque
            // 
            this.radioCheque.AutoSize = true;
            this.radioCheque.Location = new System.Drawing.Point(189, 126);
            this.radioCheque.Name = "radioCheque";
            this.radioCheque.Size = new System.Drawing.Size(62, 17);
            this.radioCheque.TabIndex = 38;
            this.radioCheque.Text = "Cheque";
            this.radioCheque.UseVisualStyleBackColor = true;
            this.radioCheque.CheckedChanged += new System.EventHandler(this.radioCheque_CheckedChanged);
            // 
            // radioEfectivo
            // 
            this.radioEfectivo.AutoSize = true;
            this.radioEfectivo.Checked = true;
            this.radioEfectivo.Location = new System.Drawing.Point(109, 126);
            this.radioEfectivo.Name = "radioEfectivo";
            this.radioEfectivo.Size = new System.Drawing.Size(64, 17);
            this.radioEfectivo.TabIndex = 37;
            this.radioEfectivo.TabStop = true;
            this.radioEfectivo.Text = "Efectivo";
            this.radioEfectivo.UseVisualStyleBackColor = true;
            this.radioEfectivo.CheckedChanged += new System.EventHandler(this.radioEfectivo_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 199);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 35;
            this.label6.Text = "Cheque";
            // 
            // cmbCheques
            // 
            this.cmbCheques.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCheques.Enabled = false;
            this.cmbCheques.FormattingEnabled = true;
            this.cmbCheques.Location = new System.Drawing.Point(109, 196);
            this.cmbCheques.Name = "cmbCheques";
            this.cmbCheques.Size = new System.Drawing.Size(232, 21);
            this.cmbCheques.TabIndex = 36;
            this.cmbCheques.SelectedIndexChanged += new System.EventHandler(this.cmbCheques_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 252);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "Observaciones";
            // 
            // txtObs
            // 
            this.txtObs.Location = new System.Drawing.Point(107, 249);
            this.txtObs.Multiline = true;
            this.txtObs.Name = "txtObs";
            this.txtObs.Size = new System.Drawing.Size(234, 53);
            this.txtObs.TabIndex = 33;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Fecha";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(109, 57);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 31;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 172);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Caja Origen";
            // 
            // cmbCajaOrigen
            // 
            this.cmbCajaOrigen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCajaOrigen.FormattingEnabled = true;
            this.cmbCajaOrigen.Location = new System.Drawing.Point(109, 169);
            this.cmbCajaOrigen.Name = "cmbCajaOrigen";
            this.cmbCajaOrigen.Size = new System.Drawing.Size(232, 21);
            this.cmbCajaOrigen.TabIndex = 30;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Cuenta Destino";
            // 
            // cmbCuentaDestino
            // 
            this.cmbCuentaDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCuentaDestino.FormattingEnabled = true;
            this.cmbCuentaDestino.Location = new System.Drawing.Point(109, 83);
            this.cmbCuentaDestino.Name = "cmbCuentaDestino";
            this.cmbCuentaDestino.Size = new System.Drawing.Size(232, 21);
            this.cmbCuentaDestino.TabIndex = 23;
            this.cmbCuentaDestino.SelectedIndexChanged += new System.EventHandler(this.cmbCuentaDestino_SelectedIndexChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(13, 226);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(42, 13);
            this.label21.TabIndex = 20;
            this.label21.Text = "Importe";
            // 
            // txtImporte
            // 
            this.txtImporte.Location = new System.Drawing.Point(109, 223);
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.Size = new System.Drawing.Size(66, 20);
            this.txtImporte.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 1;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(255, 339);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(128, 22);
            this.button3.TabIndex = 20;
            this.button3.Text = "Generar Deposito";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Depositocuenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 373);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button3);
            this.Name = "Depositocuenta";
            this.Text = "Deposito en Cuenta Bancaria";
            this.Load += new System.EventHandler(this.Depositocuenta_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbCuentaDestino;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtImporte;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCajaOrigen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtObs;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbCheques;
        private System.Windows.Forms.RadioButton radioCheque;
        private System.Windows.Forms.RadioButton radioEfectivo;
        private System.Windows.Forms.Label lblnro;
        private System.Windows.Forms.Label label7;
    }
}