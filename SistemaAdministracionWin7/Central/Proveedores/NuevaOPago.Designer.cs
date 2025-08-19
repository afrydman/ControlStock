namespace Central.Proveedores
{
    partial class NuevaOPago
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbCajas = new System.Windows.Forms.ComboBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.radioCheque = new System.Windows.Forms.RadioButton();
            this.radioEfectivo = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbCheques = new System.Windows.Forms.ComboBox();
            this.tablapagos = new System.Windows.Forms.DataGridView();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button3 = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.txtImporte = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.fecha = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.lblNro = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbClientes = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablapagos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cmbCajas);
            this.groupBox2.Controls.Add(this.lblTotal);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.radioCheque);
            this.groupBox2.Controls.Add(this.radioEfectivo);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cmbCheques);
            this.groupBox2.Controls.Add(this.tablapagos);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.txtImporte);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 87);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(683, 287);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 29;
            this.label6.Text = "Listado Cajas";
            // 
            // cmbCajas
            // 
            this.cmbCajas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCajas.FormattingEnabled = true;
            this.cmbCajas.Location = new System.Drawing.Point(108, 48);
            this.cmbCajas.Name = "cmbCajas";
            this.cmbCajas.Size = new System.Drawing.Size(232, 21);
            this.cmbCajas.TabIndex = 30;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(512, 258);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(13, 13);
            this.lblTotal.TabIndex = 28;
            this.lblTotal.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(414, 258);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Total";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(346, 75);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 23);
            this.button1.TabIndex = 26;
            this.button1.Text = "Nuevo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // radioCheque
            // 
            this.radioCheque.AutoSize = true;
            this.radioCheque.Location = new System.Drawing.Point(191, 12);
            this.radioCheque.Name = "radioCheque";
            this.radioCheque.Size = new System.Drawing.Size(62, 17);
            this.radioCheque.TabIndex = 25;
            this.radioCheque.Text = "Cheque";
            this.radioCheque.UseVisualStyleBackColor = true;
            this.radioCheque.CheckedChanged += new System.EventHandler(this.radioCheque_CheckedChanged);
            // 
            // radioEfectivo
            // 
            this.radioEfectivo.AutoSize = true;
            this.radioEfectivo.Checked = true;
            this.radioEfectivo.Location = new System.Drawing.Point(110, 12);
            this.radioEfectivo.Name = "radioEfectivo";
            this.radioEfectivo.Size = new System.Drawing.Size(64, 17);
            this.radioEfectivo.TabIndex = 24;
            this.radioEfectivo.TabStop = true;
            this.radioEfectivo.Text = "Efectivo";
            this.radioEfectivo.UseVisualStyleBackColor = true;
            this.radioEfectivo.CheckedChanged += new System.EventHandler(this.radioEfectivo_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Listado Cheques";
            // 
            // cmbCheques
            // 
            this.cmbCheques.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCheques.Enabled = false;
            this.cmbCheques.FormattingEnabled = true;
            this.cmbCheques.Location = new System.Drawing.Point(108, 75);
            this.cmbCheques.Name = "cmbCheques";
            this.cmbCheques.Size = new System.Drawing.Size(232, 21);
            this.cmbCheques.TabIndex = 23;
            this.cmbCheques.SelectedIndexChanged += new System.EventHandler(this.cmbCheques_SelectedIndexChanged);
            // 
            // tablapagos
            // 
            this.tablapagos.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tablapagos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.tablapagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tablapagos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column6,
            this.Column1,
            this.Column7,
            this.Column2});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tablapagos.DefaultCellStyle = dataGridViewCellStyle2;
            this.tablapagos.Location = new System.Drawing.Point(9, 168);
            this.tablapagos.Name = "tablapagos";
            this.tablapagos.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tablapagos.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.tablapagos.Size = new System.Drawing.Size(638, 76);
            this.tablapagos.TabIndex = 21;
            this.tablapagos.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.tablapagos_RowsRemoved);
            this.tablapagos.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.tablapagos_UserDeletedRow);
            this.tablapagos.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.tablapagos_UserDeletingRow);
            // 
            // Column6
            // 
            this.Column6.HeaderText = "idCheque";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Visible = false;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Forma de Pago";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 450;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Importe";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "idCajaEfectivo";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Visible = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(556, 117);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(91, 45);
            this.button3.TabIndex = 20;
            this.button3.Text = "Agregar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(62, 105);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(42, 13);
            this.label21.TabIndex = 20;
            this.label21.Text = "Importe";
            // 
            // txtImporte
            // 
            this.txtImporte.Location = new System.Drawing.Point(110, 102);
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.Size = new System.Drawing.Size(66, 20);
            this.txtImporte.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Forma de pago";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fecha);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblNro);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbClientes);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(683, 69);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            // 
            // fecha
            // 
            this.fecha.Location = new System.Drawing.Point(477, 16);
            this.fecha.Name = "fecha";
            this.fecha.Size = new System.Drawing.Size(200, 20);
            this.fecha.TabIndex = 33;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(434, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Fecha";
            // 
            // lblNro
            // 
            this.lblNro.AutoSize = true;
            this.lblNro.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblNro.Location = new System.Drawing.Point(53, 16);
            this.lblNro.Name = "lblNro";
            this.lblNro.Size = new System.Drawing.Size(170, 20);
            this.lblNro.TabIndex = 30;
            this.lblNro.Text = "ERROR CONEXION";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "N°";
            // 
            // cmbClientes
            // 
            this.cmbClientes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClientes.FormattingEnabled = true;
            this.cmbClientes.Location = new System.Drawing.Point(69, 42);
            this.cmbClientes.Name = "cmbClientes";
            this.cmbClientes.Size = new System.Drawing.Size(271, 21);
            this.cmbClientes.TabIndex = 27;
            this.cmbClientes.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 45);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 13);
            this.label10.TabIndex = 28;
            this.label10.Text = "Proveedor";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(619, 380);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(76, 29);
            this.button2.TabIndex = 29;
            this.button2.Text = "Confirmar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // NuevaOPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(721, 410);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "NuevaOPago";
            this.Text = "Nueva O.Pago";
            this.Load += new System.EventHandler(this.NuevoRecibo_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablapagos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView tablapagos;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtImporte;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbCheques;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblNro;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbClientes;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DateTimePicker fecha;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton radioCheque;
        private System.Windows.Forms.RadioButton radioEfectivo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbCajas;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}