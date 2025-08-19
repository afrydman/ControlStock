namespace Preparador
{
    partial class Migrar
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
            this.cmbDesde = new System.Windows.Forms.ComboBox();
            this.cmbHacia = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tablaDif = new System.Windows.Forms.DataGridView();
            this.columnaDesde = new System.Windows.Forms.DataGridView();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button2 = new System.Windows.Forms.Button();
            this.columnaHasta = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button3 = new System.Windows.Forms.Button();
            this.txtError = new System.Windows.Forms.TextBox();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.tablaDif)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.columnaDesde)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.columnaHasta)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbDesde
            // 
            this.cmbDesde.FormattingEnabled = true;
            this.cmbDesde.Location = new System.Drawing.Point(36, 40);
            this.cmbDesde.Name = "cmbDesde";
            this.cmbDesde.Size = new System.Drawing.Size(121, 21);
            this.cmbDesde.TabIndex = 0;
            // 
            // cmbHacia
            // 
            this.cmbHacia.FormattingEnabled = true;
            this.cmbHacia.Location = new System.Drawing.Point(218, 40);
            this.cmbHacia.Name = "cmbHacia";
            this.cmbHacia.Size = new System.Drawing.Size(121, 21);
            this.cmbHacia.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Desde";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(215, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hacia";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(36, 84);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(303, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Verificar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tablaDif
            // 
            this.tablaDif.AllowUserToAddRows = false;
            this.tablaDif.AllowUserToDeleteRows = false;
            this.tablaDif.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tablaDif.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column8});
            this.tablaDif.Location = new System.Drawing.Point(36, 124);
            this.tablaDif.Name = "tablaDif";
            this.tablaDif.Size = new System.Drawing.Size(467, 477);
            this.tablaDif.TabIndex = 5;
            // 
            // columnaDesde
            // 
            this.columnaDesde.AllowUserToAddRows = false;
            this.columnaDesde.AllowUserToDeleteRows = false;
            this.columnaDesde.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.columnaDesde.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column6,
            this.Column7});
            this.columnaDesde.Location = new System.Drawing.Point(564, 124);
            this.columnaDesde.Name = "columnaDesde";
            this.columnaDesde.ReadOnly = true;
            this.columnaDesde.Size = new System.Drawing.Size(308, 189);
            this.columnaDesde.TabIndex = 7;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Nombre";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Tipo";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 80;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "null";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 50;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(509, 192);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(49, 121);
            this.button2.TabIndex = 9;
            this.button2.Text = "=>";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // columnaHasta
            // 
            this.columnaHasta.AllowUserToAddRows = false;
            this.columnaHasta.AllowUserToDeleteRows = false;
            this.columnaHasta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.columnaHasta.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.columnaHasta.Location = new System.Drawing.Point(898, 124);
            this.columnaHasta.Name = "columnaHasta";
            this.columnaHasta.ReadOnly = true;
            this.columnaHasta.Size = new System.Drawing.Size(300, 189);
            this.columnaHasta.TabIndex = 10;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Nombre";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Tipo";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 80;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "null";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 50;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(302, 604);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(201, 49);
            this.button3.TabIndex = 11;
            this.button3.Text = "Migrar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtError
            // 
            this.txtError.Location = new System.Drawing.Point(509, 345);
            this.txtError.Multiline = true;
            this.txtError.Name = "txtError";
            this.txtError.Size = new System.Drawing.Size(382, 273);
            this.txtError.TabIndex = 12;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Desde";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Hasta";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Mismas Columns";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Migrar";
            this.Column4.Name = "Column4";
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column4.Width = 50;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Ok";
            this.Column8.Name = "Column8";
            this.Column8.Width = 50;
            // 
            // Migrar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1210, 665);
            this.Controls.Add(this.txtError);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.columnaHasta);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.columnaDesde);
            this.Controls.Add(this.tablaDif);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbHacia);
            this.Controls.Add(this.cmbDesde);
            this.Name = "Migrar";
            this.Text = "Migrar";
            this.Load += new System.EventHandler(this.Migrar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tablaDif)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.columnaDesde)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.columnaHasta)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbDesde;
        private System.Windows.Forms.ComboBox cmbHacia;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView tablaDif;
        private System.Windows.Forms.DataGridView columnaDesde;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridView columnaHasta;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtError;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
    }
}