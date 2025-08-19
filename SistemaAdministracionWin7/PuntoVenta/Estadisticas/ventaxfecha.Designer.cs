namespace PuntoVenta.Estadisticas
{
    partial class ventaxfecha
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
            this.button3 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.pickerHasta = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.pickerDesde = new System.Windows.Forms.DateTimePicker();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.factura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tablaTodas = new System.Windows.Forms.DataGridView();
            this.FormaPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.anulada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablaTodas)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.pickerHasta);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.pickerDesde);
            this.groupBox2.Location = new System.Drawing.Point(15, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(631, 85);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(542, 56);
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
            // pickerDesde
            // 
            this.pickerDesde.Location = new System.Drawing.Point(83, 18);
            this.pickerDesde.Name = "pickerDesde";
            this.pickerDesde.Size = new System.Drawing.Size(200, 20);
            this.pickerDesde.TabIndex = 0;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Cliente";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Fecha
            // 
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "id";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Visible = false;
            // 
            // factura
            // 
            this.factura.HeaderText = "Numero de Factura";
            this.factura.Name = "factura";
            this.factura.ReadOnly = true;
            this.factura.Width = 60;
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
            this.tablaTodas.Location = new System.Drawing.Point(12, 108);
            this.tablaTodas.Name = "tablaTodas";
            this.tablaTodas.ReadOnly = true;
            this.tablaTodas.Size = new System.Drawing.Size(631, 356);
            this.tablaTodas.TabIndex = 20;
            this.tablaTodas.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tablaTodas_CellContentDoubleClick);
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
            // ventaxfecha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 526);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.tablaTodas);
            this.Name = "ventaxfecha";
            this.Text = "Ventas x Fecha";
            this.Load += new System.EventHandler(this.ventaxfecha_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablaTodas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker pickerHasta;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker pickerDesde;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn factura;
        private System.Windows.Forms.DataGridView tablaTodas;
        private System.Windows.Forms.DataGridViewTextBoxColumn FormaPago;
        private System.Windows.Forms.DataGridViewTextBoxColumn Monto;
        private System.Windows.Forms.DataGridViewTextBoxColumn anulada;
        private System.Windows.Forms.DataGridViewTextBoxColumn ida;
    }
}