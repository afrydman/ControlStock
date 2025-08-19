namespace SharedForms.Estadisticas.Ventas
{
    partial class comisiones
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
            this.label1 = new System.Windows.Forms.Label();
            this.tablaProductos = new System.Windows.Forms.DataGridView();
            this.aa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtComisionTotal = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnCalcularComision = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtcomisionPorcentaje = new System.Windows.Forms.TextBox();
            this.tablaPagos = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tabla = new System.Windows.Forms.DataGridView();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.factura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FormaPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbVendedores = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.pickerHasta = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pickerDesde = new System.Windows.Forms.DateTimePicker();
            this.cmbLocales = new System.Windows.Forms.ComboBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablaProductos)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablaPagos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.tablaProductos);
            this.groupBox2.Location = new System.Drawing.Point(18, 338);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1246, 221);
            this.groupBox2.TabIndex = 99;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalle";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(943, 427);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 100;
            this.label1.Text = "*: Venta con descuento";
            // 
            // tablaProductos
            // 
            this.tablaProductos.AllowUserToAddRows = false;
            this.tablaProductos.AllowUserToDeleteRows = false;
            this.tablaProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tablaProductos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.aa,
            this.Column6,
            this.Column7,
            this.Column8,
            this.dataGridViewTextBoxColumn3,
            this.Column3,
            this.Column5,
            this.dataGridViewTextBoxColumn4});
            this.tablaProductos.Location = new System.Drawing.Point(6, 19);
            this.tablaProductos.Name = "tablaProductos";
            this.tablaProductos.ReadOnly = true;
            this.tablaProductos.Size = new System.Drawing.Size(1219, 180);
            this.tablaProductos.TabIndex = 99;
            // 
            // aa
            // 
            this.aa.HeaderText = "N Factura";
            this.aa.Name = "aa";
            this.aa.ReadOnly = true;
            this.aa.Width = 150;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Codigo";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 150;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Proveedor";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 160;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Descripcion";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 350;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Color";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Talle";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 30;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Cantidad";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 50;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Precio";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtComisionTotal);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.btnCalcularComision);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtcomisionPorcentaje);
            this.groupBox1.Controls.Add(this.tablaPagos);
            this.groupBox1.Controls.Add(this.tabla);
            this.groupBox1.Location = new System.Drawing.Point(18, 120);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1246, 198);
            this.groupBox1.TabIndex = 98;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ventas";
            // 
            // txtComisionTotal
            // 
            this.txtComisionTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtComisionTotal.Location = new System.Drawing.Point(1115, 114);
            this.txtComisionTotal.Name = "txtComisionTotal";
            this.txtComisionTotal.ReadOnly = true;
            this.txtComisionTotal.Size = new System.Drawing.Size(100, 29);
            this.txtComisionTotal.TabIndex = 24;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1043, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Total";
            // 
            // btnCalcularComision
            // 
            this.btnCalcularComision.Location = new System.Drawing.Point(1046, 66);
            this.btnCalcularComision.Name = "btnCalcularComision";
            this.btnCalcularComision.Size = new System.Drawing.Size(169, 31);
            this.btnCalcularComision.TabIndex = 0;
            this.btnCalcularComision.Text = "Calcular";
            this.btnCalcularComision.UseVisualStyleBackColor = true;
            this.btnCalcularComision.Click += new System.EventHandler(this.btnCalcularComision_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1043, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Comision (%)";
            // 
            // txtcomisionPorcentaje
            // 
            this.txtcomisionPorcentaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcomisionPorcentaje.Location = new System.Drawing.Point(1115, 19);
            this.txtcomisionPorcentaje.Name = "txtcomisionPorcentaje";
            this.txtcomisionPorcentaje.Size = new System.Drawing.Size(100, 29);
            this.txtcomisionPorcentaje.TabIndex = 21;
            this.txtcomisionPorcentaje.TextChanged += new System.EventHandler(this.txtcomisionPorcentaje_TextChanged);
            this.txtcomisionPorcentaje.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtcomisionPorcentaje_KeyPress);
            // 
            // tablaPagos
            // 
            this.tablaPagos.AllowUserToAddRows = false;
            this.tablaPagos.AllowUserToDeleteRows = false;
            this.tablaPagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tablaPagos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.Column4,
            this.Column9});
            this.tablaPagos.Location = new System.Drawing.Point(685, 19);
            this.tablaPagos.Name = "tablaPagos";
            this.tablaPagos.Size = new System.Drawing.Size(338, 151);
            this.tablaPagos.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Forma Pago";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Total";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 80;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Calcular";
            this.Column9.Name = "Column9";
            this.Column9.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column9.Width = 50;
            // 
            // tabla
            // 
            this.tabla.AllowUserToAddRows = false;
            this.tabla.AllowUserToDeleteRows = false;
            this.tabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tabla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Fecha,
            this.factura,
            this.FormaPago,
            this.Monto,
            this.ida,
            this.Column1,
            this.Column2});
            this.tabla.Location = new System.Drawing.Point(6, 19);
            this.tabla.Name = "tabla";
            this.tabla.ReadOnly = true;
            this.tabla.Size = new System.Drawing.Size(673, 151);
            this.tabla.TabIndex = 1;
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
            // 
            // ida
            // 
            this.ida.HeaderText = "id";
            this.ida.Name = "ida";
            this.ida.ReadOnly = true;
            this.ida.Visible = false;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Cliente";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Venta/Cambio";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.cmbVendedores);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.pickerHasta);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.pickerDesde);
            this.groupBox3.Controls.Add(this.cmbLocales);
            this.groupBox3.Location = new System.Drawing.Point(18, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(825, 92);
            this.groupBox3.TabIndex = 100;
            this.groupBox3.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(355, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Vendedor";
            // 
            // cmbVendedores
            // 
            this.cmbVendedores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVendedores.FormattingEnabled = true;
            this.cmbVendedores.Location = new System.Drawing.Point(417, 55);
            this.cmbVendedores.Name = "cmbVendedores";
            this.cmbVendedores.Size = new System.Drawing.Size(208, 21);
            this.cmbVendedores.TabIndex = 24;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(681, 19);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(127, 57);
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Local";
            // 
            // pickerDesde
            // 
            this.pickerDesde.Location = new System.Drawing.Point(83, 18);
            this.pickerDesde.Name = "pickerDesde";
            this.pickerDesde.Size = new System.Drawing.Size(200, 20);
            this.pickerDesde.TabIndex = 0;
            // 
            // cmbLocales
            // 
            this.cmbLocales.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocales.FormattingEnabled = true;
            this.cmbLocales.Location = new System.Drawing.Point(83, 54);
            this.cmbLocales.Name = "cmbLocales";
            this.cmbLocales.Size = new System.Drawing.Size(200, 21);
            this.cmbLocales.TabIndex = 18;
            // 
            // comisiones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1276, 582);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "comisiones";
            this.Text = "Comisiones";
            this.Load += new System.EventHandler(this.comisiones_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablaProductos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablaPagos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView tablaProductos;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView tablaPagos;
        private System.Windows.Forms.DataGridView tabla;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbVendedores;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker pickerHasta;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker pickerDesde;
        private System.Windows.Forms.ComboBox cmbLocales;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn factura;
        private System.Windows.Forms.DataGridViewTextBoxColumn FormaPago;
        private System.Windows.Forms.DataGridViewTextBoxColumn Monto;
        private System.Windows.Forms.DataGridViewTextBoxColumn ida;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Button btnCalcularComision;
        private System.Windows.Forms.TextBox txtComisionTotal;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtcomisionPorcentaje;
        private System.Windows.Forms.DataGridViewTextBoxColumn aa;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column9;
    }
}