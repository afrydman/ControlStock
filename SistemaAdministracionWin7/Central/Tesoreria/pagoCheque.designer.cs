namespace Central.Tesoreria
{
    partial class pagoCheque
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
            this.label4 = new System.Windows.Forms.Label();
            this.fechaCobro = new System.Windows.Forms.DateTimePicker();
            this.lblnro = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbCheques = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtObs = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.fecha = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCuenta = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtImporte = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.fechaCobro);
            this.groupBox2.Controls.Add(this.lblnro);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cmbCheques);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtObs);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.fecha);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cmbCuenta);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.txtImporte);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(371, 325);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 42;
            this.label4.Text = "Fecha Cobro";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // fechaCobro
            // 
            this.fechaCobro.Enabled = false;
            this.fechaCobro.Location = new System.Drawing.Point(109, 148);
            this.fechaCobro.Name = "fechaCobro";
            this.fechaCobro.Size = new System.Drawing.Size(200, 20);
            this.fechaCobro.TabIndex = 41;
            this.fechaCobro.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 112);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 35;
            this.label6.Text = "Cheque";
            // 
            // cmbCheques
            // 
            this.cmbCheques.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCheques.FormattingEnabled = true;
            this.cmbCheques.Location = new System.Drawing.Point(109, 104);
            this.cmbCheques.Name = "cmbCheques";
            this.cmbCheques.Size = new System.Drawing.Size(232, 21);
            this.cmbCheques.TabIndex = 36;
            this.cmbCheques.SelectedIndexChanged += new System.EventHandler(this.cmbCheques_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 258);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "Observaciones";
            // 
            // txtObs
            // 
            this.txtObs.Location = new System.Drawing.Point(109, 255);
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
            // fecha
            // 
            this.fecha.Location = new System.Drawing.Point(109, 57);
            this.fecha.Name = "fecha";
            this.fecha.Size = new System.Drawing.Size(200, 20);
            this.fecha.TabIndex = 31;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Cuenta";
            // 
            // cmbCuenta
            // 
            this.cmbCuenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCuenta.Enabled = false;
            this.cmbCuenta.FormattingEnabled = true;
            this.cmbCuenta.Location = new System.Drawing.Point(109, 174);
            this.cmbCuenta.Name = "cmbCuenta";
            this.cmbCuenta.Size = new System.Drawing.Size(232, 21);
            this.cmbCuenta.TabIndex = 30;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(15, 204);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(42, 13);
            this.label21.TabIndex = 20;
            this.label21.Text = "Importe";
            // 
            // txtImporte
            // 
            this.txtImporte.Enabled = false;
            this.txtImporte.Location = new System.Drawing.Point(111, 201);
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
            this.button3.Location = new System.Drawing.Point(255, 343);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(128, 22);
            this.button3.TabIndex = 30;
            this.button3.Text = "Generar Pago";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // pagoCheque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 378);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.groupBox2);
            this.Name = "pagoCheque";
            this.Text = "Pago de Cheque";
            this.Load += new System.EventHandler(this.pagoCheque_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblnro;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbCheques;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtObs;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker fecha;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCuenta;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtImporte;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker fechaCobro;
        private System.Windows.Forms.Button button3;
    }
}