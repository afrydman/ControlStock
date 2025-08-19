namespace SharedForms.Stock
{
    partial class verEnviosStock
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
            this.tabla = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbDestino = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbOrigen = new System.Windows.Forms.ComboBox();
            this.recibidoHastsa = new System.Windows.Forms.DateTimePicker();
            this.recibidoDesde = new System.Windows.Forms.DateTimePicker();
            this.generadoHasta = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.generadoDesde = new System.Windows.Forms.DateTimePicker();
            this.label16 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbFiltroEstado = new System.Windows.Forms.ComboBox();
            this.checkRecibo = new System.Windows.Forms.CheckBox();
            this.checkGenerado = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabla
            // 
            this.tabla.AllowUserToAddRows = false;
            this.tabla.AllowUserToDeleteRows = false;
            this.tabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tabla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column7,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column8});
            this.tabla.Location = new System.Drawing.Point(14, 136);
            this.tabla.Name = "tabla";
            this.tabla.ReadOnly = true;
            this.tabla.Size = new System.Drawing.Size(842, 543);
            this.tabla.TabIndex = 0;
            this.tabla.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tabla_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "id";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Nro";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 150;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Fecha Generacion";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 120;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Origen";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Destino";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Fecha Recibo";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "EstadoCheque";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Cantidad Total";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.cmbDestino);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.cmbOrigen);
            this.groupBox4.Controls.Add(this.recibidoHastsa);
            this.groupBox4.Controls.Add(this.recibidoDesde);
            this.groupBox4.Controls.Add(this.generadoHasta);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.generadoDesde);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.cmbFiltroEstado);
            this.groupBox4.Controls.Add(this.checkRecibo);
            this.groupBox4.Controls.Add(this.checkGenerado);
            this.groupBox4.Controls.Add(this.button3);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Location = new System.Drawing.Point(12, -2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(850, 132);
            this.groupBox4.TabIndex = 89;
            this.groupBox4.TabStop = false;
            this.groupBox4.Enter += new System.EventHandler(this.groupBox4_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(485, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 107;
            this.label4.Text = "Destino";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // cmbDestino
            // 
            this.cmbDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDestino.FormattingEnabled = true;
            this.cmbDestino.Location = new System.Drawing.Point(551, 42);
            this.cmbDestino.Name = "cmbDestino";
            this.cmbDestino.Size = new System.Drawing.Size(293, 21);
            this.cmbDestino.TabIndex = 106;
            this.cmbDestino.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(485, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 105;
            this.label3.Text = "Origen";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // cmbOrigen
            // 
            this.cmbOrigen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrigen.FormattingEnabled = true;
            this.cmbOrigen.Location = new System.Drawing.Point(551, 15);
            this.cmbOrigen.Name = "cmbOrigen";
            this.cmbOrigen.Size = new System.Drawing.Size(293, 21);
            this.cmbOrigen.TabIndex = 104;
            this.cmbOrigen.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // recibidoHastsa
            // 
            this.recibidoHastsa.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.recibidoHastsa.Location = new System.Drawing.Point(328, 49);
            this.recibidoHastsa.Name = "recibidoHastsa";
            this.recibidoHastsa.Size = new System.Drawing.Size(100, 20);
            this.recibidoHastsa.TabIndex = 102;
            // 
            // recibidoDesde
            // 
            this.recibidoDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.recibidoDesde.Location = new System.Drawing.Point(205, 49);
            this.recibidoDesde.Name = "recibidoDesde";
            this.recibidoDesde.Size = new System.Drawing.Size(100, 20);
            this.recibidoDesde.TabIndex = 98;
            // 
            // generadoHasta
            // 
            this.generadoHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.generadoHasta.Location = new System.Drawing.Point(328, 16);
            this.generadoHasta.Name = "generadoHasta";
            this.generadoHasta.Size = new System.Drawing.Size(100, 20);
            this.generadoHasta.TabIndex = 97;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(311, 19);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(12, 13);
            this.label15.TabIndex = 96;
            this.label15.Text = "y";
            // 
            // generadoDesde
            // 
            this.generadoDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.generadoDesde.Location = new System.Drawing.Point(205, 16);
            this.generadoDesde.Name = "generadoDesde";
            this.generadoDesde.Size = new System.Drawing.Size(100, 20);
            this.generadoDesde.TabIndex = 94;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(135, 20);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(64, 13);
            this.label16.TabIndex = 95;
            this.label16.Text = "Fecha entre";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(4, 97);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 13);
            this.label12.TabIndex = 93;
            this.label12.Text = "Estado";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // cmbFiltroEstado
            // 
            this.cmbFiltroEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroEstado.FormattingEnabled = true;
            this.cmbFiltroEstado.Location = new System.Drawing.Point(87, 94);
            this.cmbFiltroEstado.Name = "cmbFiltroEstado";
            this.cmbFiltroEstado.Size = new System.Drawing.Size(200, 21);
            this.cmbFiltroEstado.TabIndex = 92;
            this.cmbFiltroEstado.SelectedIndexChanged += new System.EventHandler(this.cmbFiltroEstado_SelectedIndexChanged);
            // 
            // checkRecibo
            // 
            this.checkRecibo.AutoSize = true;
            this.checkRecibo.Location = new System.Drawing.Point(6, 52);
            this.checkRecibo.Name = "checkRecibo";
            this.checkRecibo.Size = new System.Drawing.Size(93, 17);
            this.checkRecibo.TabIndex = 88;
            this.checkRecibo.Text = "Fecha Recibo";
            this.checkRecibo.UseVisualStyleBackColor = true;
            // 
            // checkGenerado
            // 
            this.checkGenerado.AutoSize = true;
            this.checkGenerado.Location = new System.Drawing.Point(6, 19);
            this.checkGenerado.Name = "checkGenerado";
            this.checkGenerado.Size = new System.Drawing.Size(106, 17);
            this.checkGenerado.TabIndex = 87;
            this.checkGenerado.Text = "Fecha Generado";
            this.checkGenerado.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(757, 89);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(87, 29);
            this.button3.TabIndex = 86;
            this.button3.Text = "Filtrar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(310, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(12, 13);
            this.label8.TabIndex = 81;
            this.label8.Text = "y";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(134, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 13);
            this.label7.TabIndex = 79;
            this.label7.Text = "Fecha entre";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(576, 685);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(117, 43);
            this.button2.TabIndex = 91;
            this.button2.Text = "Ver";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(739, 685);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 43);
            this.button1.TabIndex = 90;
            this.button1.Text = "Anular";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // verEnviosStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 741);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.tabla);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(717, 655);
            this.Name = "verEnviosStock";
            this.Text = "Ver Envios Stock";
            this.Load += new System.EventHandler(this.verEnviosStock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView tabla;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DateTimePicker recibidoHastsa;
        private System.Windows.Forms.DateTimePicker recibidoDesde;
        private System.Windows.Forms.DateTimePicker generadoHasta;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DateTimePicker generadoDesde;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbFiltroEstado;
        private System.Windows.Forms.CheckBox checkRecibo;
        private System.Windows.Forms.CheckBox checkGenerado;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbOrigen;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbDestino;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
    }
}