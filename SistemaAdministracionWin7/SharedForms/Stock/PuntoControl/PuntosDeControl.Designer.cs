namespace SharedForms.Estadisticas.Stock.PuntoControl
{
    partial class PuntosDeControl
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkAnulados = new System.Windows.Forms.CheckBox();
            this.generadoHasta = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.generadoDesde = new System.Windows.Forms.DateTimePicker();
            this.label16 = new System.Windows.Forms.Label();
            this.checkGenerado = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.tabla = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.checkAnulados);
            this.groupBox4.Controls.Add(this.generadoHasta);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.generadoDesde);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.checkGenerado);
            this.groupBox4.Controls.Add(this.button3);
            this.groupBox4.Location = new System.Drawing.Point(3, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(545, 108);
            this.groupBox4.TabIndex = 90;
            this.groupBox4.TabStop = false;
            // 
            // checkAnulados
            // 
            this.checkAnulados.AutoSize = true;
            this.checkAnulados.Checked = true;
            this.checkAnulados.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkAnulados.Location = new System.Drawing.Point(6, 42);
            this.checkAnulados.Name = "checkAnulados";
            this.checkAnulados.Size = new System.Drawing.Size(107, 17);
            this.checkAnulados.TabIndex = 106;
            this.checkAnulados.Text = "Ocultar Anulados";
            this.checkAnulados.UseVisualStyleBackColor = true;
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
            this.button3.Location = new System.Drawing.Point(445, 64);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(94, 38);
            this.button3.TabIndex = 86;
            this.button3.Text = "Filtrar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
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
            this.Column4});
            this.tabla.Location = new System.Drawing.Point(3, 126);
            this.tabla.Name = "tabla";
            this.tabla.ReadOnly = true;
            this.tabla.Size = new System.Drawing.Size(545, 339);
            this.tabla.TabIndex = 91;
            this.tabla.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tabla_CellContentDoubleClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(454, 471);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 38);
            this.button1.TabIndex = 92;
            this.button1.Text = "Ver";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.Column3.HeaderText = "Local";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Estado";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // PuntosDeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 521);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabla);
            this.Controls.Add(this.groupBox4);
            this.Name = "PuntosDeControl";
            this.Text = "Puntos De Control";
            this.Load += new System.EventHandler(this.PuntosDeControl_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox checkAnulados;
        private System.Windows.Forms.DateTimePicker generadoHasta;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DateTimePicker generadoDesde;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox checkGenerado;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridView tabla;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}