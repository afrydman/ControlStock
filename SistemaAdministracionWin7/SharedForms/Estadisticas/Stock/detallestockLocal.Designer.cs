namespace SharedForms.Estadisticas.Stock
{
    partial class detallestockLocal
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
            this.components = new System.ComponentModel.Container();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pickerHasta = new System.Windows.Forms.DateTimePicker();
            this.pickerDesde = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkPuntoDeControl = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lbltalle = new System.Windows.Forms.Label();
            this.txtTalle = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbColores = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtinterno = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.cmbProveedor = new System.Windows.Forms.ComboBox();
            this.cmbArticulo = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabla = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.cmbLocales = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPuntoControl = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).BeginInit();
            this.SuspendLayout();
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Codigo";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Talle";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Color";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Articulo";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 150;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Proveedor";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 150;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Cantidad";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(911, 453);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 18);
            this.label1.TabIndex = 40;
            this.label1.Text = "Total";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(1023, 453);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(19, 18);
            this.lblTotal.TabIndex = 39;
            this.lblTotal.Text = "0";
            // 
            // Descripcion
            // 
            this.Descripcion.HeaderText = "Descripcion";
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.ReadOnly = true;
            this.Descripcion.Width = 150;
            // 
            // pickerHasta
            // 
            this.pickerHasta.Location = new System.Drawing.Point(438, 56);
            this.pickerHasta.Name = "pickerHasta";
            this.pickerHasta.Size = new System.Drawing.Size(200, 20);
            this.pickerHasta.TabIndex = 33;
            // 
            // pickerDesde
            // 
            this.pickerDesde.Location = new System.Drawing.Point(92, 56);
            this.pickerDesde.Name = "pickerDesde";
            this.pickerDesde.Size = new System.Drawing.Size(200, 20);
            this.pickerDesde.TabIndex = 35;
            this.pickerDesde.ValueChanged += new System.EventHandler(this.pickerDesde_ValueChanged_2);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "Fecha Desde";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.txtPuntoControl);
            this.groupBox2.Controls.Add(this.cmbLocales);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.checkPuntoDeControl);
            this.groupBox2.Controls.Add(this.pickerHasta);
            this.groupBox2.Controls.Add(this.pickerDesde);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(286, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(654, 135);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            // 
            // checkPuntoDeControl
            // 
            this.checkPuntoDeControl.AutoSize = true;
            this.checkPuntoDeControl.Location = new System.Drawing.Point(18, 85);
            this.checkPuntoDeControl.Name = "checkPuntoDeControl";
            this.checkPuntoDeControl.Size = new System.Drawing.Size(260, 17);
            this.checkPuntoDeControl.TabIndex = 36;
            this.checkPuntoDeControl.Text = "Punto de control mas reciente dentro del intervalo";
            this.checkPuntoDeControl.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(364, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Fecha Hasta";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.lbltalle);
            this.groupBox1.Controls.Add(this.txtTalle);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbColores);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.txtinterno);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.cmbProveedor);
            this.groupBox1.Controls.Add(this.cmbArticulo);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1049, 160);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label7.Location = new System.Drawing.Point(116, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 44;
            this.label7.Text = "?";
            this.toolTip1.SetToolTip(this.label7, "Ingrese 0  o * y luego Enter para buscar");
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(955, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 127);
            this.button1.TabIndex = 6;
            this.button1.Text = "Ver";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbltalle
            // 
            this.lbltalle.AutoSize = true;
            this.lbltalle.Location = new System.Drawing.Point(2, 129);
            this.lbltalle.Name = "lbltalle";
            this.lbltalle.Size = new System.Drawing.Size(30, 13);
            this.lbltalle.TabIndex = 31;
            this.lbltalle.Text = "Talle";
            // 
            // txtTalle
            // 
            this.txtTalle.Location = new System.Drawing.Point(136, 126);
            this.txtTalle.MaxLength = 2;
            this.txtTalle.Name = "txtTalle";
            this.txtTalle.Size = new System.Drawing.Size(128, 20);
            this.txtTalle.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "Color";
            // 
            // cmbColores
            // 
            this.cmbColores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColores.FormattingEnabled = true;
            this.cmbColores.Location = new System.Drawing.Point(83, 99);
            this.cmbColores.Name = "cmbColores";
            this.cmbColores.Size = new System.Drawing.Size(181, 21);
            this.cmbColores.TabIndex = 4;
            this.cmbColores.SelectedIndexChanged += new System.EventHandler(this.cmbColores_SelectedIndexChanged_1);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(2, 22);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(76, 13);
            this.label18.TabIndex = 22;
            this.label18.Text = "Codigo Interno";
            // 
            // txtinterno
            // 
            this.txtinterno.Location = new System.Drawing.Point(136, 19);
            this.txtinterno.MaxLength = 12;
            this.txtinterno.Name = "txtinterno";
            this.txtinterno.Size = new System.Drawing.Size(128, 20);
            this.txtinterno.TabIndex = 1;
            this.txtinterno.TextChanged += new System.EventHandler(this.txtinterno_TextChanged_1);
            this.txtinterno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtinterno_KeyPress);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(3, 75);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(42, 13);
            this.label16.TabIndex = 20;
            this.label16.Text = "Articulo";
            // 
            // cmbProveedor
            // 
            this.cmbProveedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProveedor.FormattingEnabled = true;
            this.cmbProveedor.Location = new System.Drawing.Point(83, 48);
            this.cmbProveedor.Name = "cmbProveedor";
            this.cmbProveedor.Size = new System.Drawing.Size(181, 21);
            this.cmbProveedor.TabIndex = 2;
            this.cmbProveedor.SelectedIndexChanged += new System.EventHandler(this.cmbProveedor_SelectedIndexChanged);
            // 
            // cmbArticulo
            // 
            this.cmbArticulo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbArticulo.FormattingEnabled = true;
            this.cmbArticulo.Location = new System.Drawing.Point(83, 72);
            this.cmbArticulo.Name = "cmbArticulo";
            this.cmbArticulo.Size = new System.Drawing.Size(181, 21);
            this.cmbArticulo.TabIndex = 3;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 48);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(56, 13);
            this.label15.TabIndex = 17;
            this.label15.Text = "Proveedor";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Fecha";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 150;
            // 
            // tabla
            // 
            this.tabla.AllowUserToAddRows = false;
            this.tabla.AllowUserToDeleteRows = false;
            this.tabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tabla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Descripcion,
            this.Column7,
            this.Column5,
            this.Column6,
            this.Column3,
            this.Column4,
            this.Column2});
            this.tabla.Location = new System.Drawing.Point(12, 178);
            this.tabla.Name = "tabla";
            this.tabla.ReadOnly = true;
            this.tabla.Size = new System.Drawing.Size(1049, 257);
            this.tabla.TabIndex = 38;
            this.tabla.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tabla_CellContentClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(145, 13);
            this.label5.TabIndex = 37;
            this.label5.Text = "Punto de control  encontrado";
            // 
            // cmbLocales
            // 
            this.cmbLocales.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocales.FormattingEnabled = true;
            this.cmbLocales.Location = new System.Drawing.Point(457, 19);
            this.cmbLocales.Name = "cmbLocales";
            this.cmbLocales.Size = new System.Drawing.Size(181, 21);
            this.cmbLocales.TabIndex = 38;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(410, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 39;
            this.label6.Text = "Local";
            // 
            // txtPuntoControl
            // 
            this.txtPuntoControl.Location = new System.Drawing.Point(166, 108);
            this.txtPuntoControl.MaxLength = 2;
            this.txtPuntoControl.Name = "txtPuntoControl";
            this.txtPuntoControl.ReadOnly = true;
            this.txtPuntoControl.Size = new System.Drawing.Size(160, 20);
            this.txtPuntoControl.TabIndex = 40;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(332, 107);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(89, 24);
            this.button2.TabIndex = 41;
            this.button2.Text = "Ver";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // detallestockLocal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 489);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabla);
            this.Name = "detallestockLocal";
            this.Text = "Stock Detallado x Local";
            this.Load += new System.EventHandler(this.detallestockLocal_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DateTimePicker pickerHasta;
        private System.Windows.Forms.DateTimePicker pickerDesde;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbltalle;
        private System.Windows.Forms.TextBox txtTalle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbColores;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtinterno;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cmbProveedor;
        private System.Windows.Forms.ComboBox cmbArticulo;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridView tabla;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox checkPuntoDeControl;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtPuntoControl;
        private System.Windows.Forms.ComboBox cmbLocales;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;

    }
}