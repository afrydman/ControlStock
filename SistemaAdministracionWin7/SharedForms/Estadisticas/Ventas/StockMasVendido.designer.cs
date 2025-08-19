namespace SharedForms.Estadisticas.Ventas
{
    partial class StockMasVendido
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pickerHasta = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.factura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabla2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.tablaTodas)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla2)).BeginInit();
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
            this.Column1,
            this.factura});
            this.tablaTodas.Location = new System.Drawing.Point(12, 134);
            this.tablaTodas.Name = "tablaTodas";
            this.tablaTodas.ReadOnly = true;
            this.tablaTodas.Size = new System.Drawing.Size(311, 539);
            this.tablaTodas.TabIndex = 17;
            this.tablaTodas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tablaTodas_CellContentClick);
            this.tablaTodas.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tablaTodas_CellContentDoubleClick);
            this.tablaTodas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tablaTodas_CellContentDoubleClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.pickerHasta);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.pickerDesde);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(631, 116);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
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
            // Fecha
            // 
            this.Fecha.HeaderText = "Proveedor";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Articulo";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // factura
            // 
            this.factura.HeaderText = "Cantidad";
            this.factura.Name = "factura";
            this.factura.ReadOnly = true;
            this.factura.Width = 60;
            // 
            // tabla2
            // 
            this.tabla2.AllowUserToAddRows = false;
            this.tabla2.AllowUserToDeleteRows = false;
            this.tabla2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tabla2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn3});
            this.tabla2.Location = new System.Drawing.Point(329, 134);
            this.tabla2.Name = "tabla2";
            this.tabla2.ReadOnly = true;
            this.tabla2.Size = new System.Drawing.Size(311, 539);
            this.tabla2.TabIndex = 20;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Proveedor";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Cantidad";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 60;
            // 
            // StockMasVendido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(661, 685);
            this.Controls.Add(this.tabla2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.tablaTodas);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StockMasVendido";
            this.Text = "Stock Mas Vendido";
            this.Load += new System.EventHandler(this.ventasxFecha_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tablaTodas)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker pickerDesde;
        private System.Windows.Forms.DataGridView tablaTodas;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker pickerHasta;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn factura;
        private System.Windows.Forms.DataGridView tabla2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    }
}