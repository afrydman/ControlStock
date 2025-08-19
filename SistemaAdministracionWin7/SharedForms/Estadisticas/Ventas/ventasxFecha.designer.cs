namespace SharedForms.Estadisticas.Ventas
{
    partial class ventasxFecha
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
            this.pickerDesde = new System.Windows.Forms.DateTimePicker();
            this.tablaTodas = new System.Windows.Forms.DataGridView();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.factura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FormaPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.anulada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbLocales = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbClientes = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.pickerHasta = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grupoPares = new System.Windows.Forms.GroupBox();
            this.txtSalidaCambio = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEntradaCambio = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tablaPagos = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtParesVendidos = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.tablaTodas)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.grupoPares.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablaPagos)).BeginInit();
            this.SuspendLayout();
            // 
            // pickerDesde
            // 
            this.pickerDesde.Location = new System.Drawing.Point(83, 18);
            this.pickerDesde.Name = "pickerDesde";
            this.pickerDesde.Size = new System.Drawing.Size(200, 20);
            this.pickerDesde.TabIndex = 0;
            this.pickerDesde.ValueChanged += new System.EventHandler(this.picker_ValueChanged);
            // 
            // tablaTodas
            // 
            this.tablaTodas.AllowUserToAddRows = false;
            this.tablaTodas.AllowUserToDeleteRows = false;
            this.tablaTodas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tablaTodas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Fecha,
            this.factura,
            this.FormaPago,
            this.Monto,
            this.anulada,
            this.ida,
            this.Column2,
            this.Column3});
            this.tablaTodas.Location = new System.Drawing.Point(12, 134);
            this.tablaTodas.Name = "tablaTodas";
            this.tablaTodas.ReadOnly = true;
            this.tablaTodas.Size = new System.Drawing.Size(631, 539);
            this.tablaTodas.TabIndex = 17;
            this.tablaTodas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tablaTodas_CellContentClick);
            this.tablaTodas.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tablaTodas_CellContentDoubleClick);
            this.tablaTodas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tablaTodas_CellContentDoubleClick);
            // 
            // Fecha
            // 
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            // 
            // factura
            // 
            this.factura.HeaderText = "Numero de Factura";
            this.factura.Name = "factura";
            this.factura.ReadOnly = true;
            this.factura.Width = 60;
            // 
            // FormaPago
            // 
            this.FormaPago.HeaderText = "Forma de Pago";
            this.FormaPago.Name = "FormaPago";
            this.FormaPago.ReadOnly = true;
            // 
            // Monto
            // 
            this.Monto.HeaderText = "Monto";
            this.Monto.Name = "Monto";
            this.Monto.ReadOnly = true;
            this.Monto.Width = 60;
            // 
            // anulada
            // 
            this.anulada.HeaderText = "Anulada";
            this.anulada.Name = "anulada";
            this.anulada.ReadOnly = true;
            this.anulada.Width = 60;
            // 
            // ida
            // 
            this.ida.HeaderText = "Venta/Cambio";
            this.ida.Name = "ida";
            this.ida.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "id";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Visible = false;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Cliente";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // cmbLocales
            // 
            this.cmbLocales.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocales.FormattingEnabled = true;
            this.cmbLocales.Location = new System.Drawing.Point(83, 54);
            this.cmbLocales.Name = "cmbLocales";
            this.cmbLocales.Size = new System.Drawing.Size(200, 21);
            this.cmbLocales.TabIndex = 18;
            this.cmbLocales.SelectedIndexChanged += new System.EventHandler(this.cmbLocales_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cmbClientes);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.pickerHasta);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.pickerDesde);
            this.groupBox2.Controls.Add(this.cmbLocales);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(631, 116);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(369, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Cliente";
            // 
            // cmbClientes
            // 
            this.cmbClientes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClientes.FormattingEnabled = true;
            this.cmbClientes.Location = new System.Drawing.Point(417, 55);
            this.cmbClientes.Name = "cmbClientes";
            this.cmbClientes.Size = new System.Drawing.Size(208, 21);
            this.cmbClientes.TabIndex = 24;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(542, 88);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(83, 23);
            this.button3.TabIndex = 23;
            this.button3.Text = "Ver";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(340, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Fecha Hasta";
            // 
            // pickerHasta
            // 
            this.pickerHasta.Location = new System.Drawing.Point(417, 19);
            this.pickerHasta.Name = "pickerHasta";
            this.pickerHasta.Size = new System.Drawing.Size(208, 20);
            this.pickerHasta.TabIndex = 21;
            this.pickerHasta.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Fecha Desde";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Local";
            // 
            // grupoPares
            // 
            this.grupoPares.Controls.Add(this.txtSalidaCambio);
            this.grupoPares.Controls.Add(this.label7);
            this.grupoPares.Controls.Add(this.label4);
            this.grupoPares.Controls.Add(this.txtEntradaCambio);
            this.grupoPares.Controls.Add(this.label3);
            this.grupoPares.Controls.Add(this.tablaPagos);
            this.grupoPares.Controls.Add(this.txtParesVendidos);
            this.grupoPares.Location = new System.Drawing.Point(649, 14);
            this.grupoPares.Name = "grupoPares";
            this.grupoPares.Size = new System.Drawing.Size(313, 438);
            this.grupoPares.TabIndex = 20;
            this.grupoPares.TabStop = false;
            this.grupoPares.Text = "Valores";
            // 
            // txtSalidaCambio
            // 
            this.txtSalidaCambio.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSalidaCambio.Location = new System.Drawing.Point(255, 137);
            this.txtSalidaCambio.Name = "txtSalidaCambio";
            this.txtSalidaCambio.Size = new System.Drawing.Size(52, 29);
            this.txtSalidaCambio.TabIndex = 30;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(17, 137);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(216, 24);
            this.label7.TabIndex = 29;
            this.label7.Text = "Unidades Salida Cambio";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(230, 24);
            this.label4.TabIndex = 28;
            this.label4.Text = "Unidades Entrada Cambio";
            // 
            // txtEntradaCambio
            // 
            this.txtEntradaCambio.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEntradaCambio.Location = new System.Drawing.Point(255, 75);
            this.txtEntradaCambio.Name = "txtEntradaCambio";
            this.txtEntradaCambio.Size = new System.Drawing.Size(52, 29);
            this.txtEntradaCambio.TabIndex = 27;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(176, 24);
            this.label3.TabIndex = 26;
            this.label3.Text = "Unidades Vendidos";
            // 
            // tablaPagos
            // 
            this.tablaPagos.AllowUserToAddRows = false;
            this.tablaPagos.AllowUserToDeleteRows = false;
            this.tablaPagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tablaPagos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column4});
            this.tablaPagos.Location = new System.Drawing.Point(6, 205);
            this.tablaPagos.Name = "tablaPagos";
            this.tablaPagos.ReadOnly = true;
            this.tablaPagos.Size = new System.Drawing.Size(301, 219);
            this.tablaPagos.TabIndex = 1;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Forma Pago";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 150;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Total";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 80;
            // 
            // txtParesVendidos
            // 
            this.txtParesVendidos.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtParesVendidos.Location = new System.Drawing.Point(255, 16);
            this.txtParesVendidos.Name = "txtParesVendidos";
            this.txtParesVendidos.Size = new System.Drawing.Size(52, 29);
            this.txtParesVendidos.TabIndex = 0;
            // 
            // ventasxFecha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(967, 685);
            this.Controls.Add(this.grupoPares);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.tablaTodas);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ventasxFecha";
            this.Text = "Ventas X Fecha";
            this.Load += new System.EventHandler(this.ventasxFecha_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tablaTodas)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grupoPares.ResumeLayout(false);
            this.grupoPares.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablaPagos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker pickerDesde;
        private System.Windows.Forms.DataGridView tablaTodas;
        private System.Windows.Forms.ComboBox cmbLocales;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn factura;
        private System.Windows.Forms.DataGridViewTextBoxColumn FormaPago;
        private System.Windows.Forms.DataGridViewTextBoxColumn Monto;
        private System.Windows.Forms.DataGridViewTextBoxColumn anulada;
        private System.Windows.Forms.DataGridViewTextBoxColumn ida;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker pickerHasta;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbClientes;
        private System.Windows.Forms.GroupBox grupoPares;
        private System.Windows.Forms.TextBox txtSalidaCambio;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEntradaCambio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView tablaPagos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.TextBox txtParesVendidos;
    }
}