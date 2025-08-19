namespace SharedForms.Estadisticas.RetirosIngresos
{
    partial class retiros_ingresosxfecha<T> 
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
            this.tablaRetiros = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbLocales = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.hasta = new System.Windows.Forms.DateTimePicker();
            this.desde = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.txtFinal = new System.Windows.Forms.TextBox();
            this.lblCaja = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tablaRetiros)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tablaRetiros
            // 
            this.tablaRetiros.AllowUserToAddRows = false;
            this.tablaRetiros.AllowUserToDeleteRows = false;
            this.tablaRetiros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tablaRetiros.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.Column2,
            this.tipo,
            this.monto,
            this.desc,
            this.Column1});
            this.tablaRetiros.Location = new System.Drawing.Point(12, 130);
            this.tablaRetiros.Name = "tablaRetiros";
            this.tablaRetiros.ReadOnly = true;
            this.tablaRetiros.Size = new System.Drawing.Size(834, 366);
            this.tablaRetiros.TabIndex = 1;
            this.tablaRetiros.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tablaRetiros_CellContentClick);
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Fecha";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 150;
            // 
            // tipo
            // 
            this.tipo.HeaderText = "Tipo Retiro";
            this.tipo.Name = "tipo";
            this.tipo.ReadOnly = true;
            this.tipo.Width = 150;
            // 
            // monto
            // 
            this.monto.HeaderText = "Monto";
            this.monto.Name = "monto";
            this.monto.ReadOnly = true;
            // 
            // desc
            // 
            this.desc.HeaderText = "Descripcion";
            this.desc.Name = "desc";
            this.desc.ReadOnly = true;
            this.desc.Width = 250;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Anulada";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbTipo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbLocales);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.hasta);
            this.groupBox1.Controls.Add(this.desde);
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(590, 118);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(320, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Tipo";
            // 
            // cmbTipo
            // 
            this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Location = new System.Drawing.Point(404, 51);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(161, 21);
            this.cmbTipo.TabIndex = 25;
            this.cmbTipo.SelectedIndexChanged += new System.EventHandler(this.cmbTipo_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Local";
            // 
            // cmbLocales
            // 
            this.cmbLocales.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocales.FormattingEnabled = true;
            this.cmbLocales.Location = new System.Drawing.Point(81, 54);
            this.cmbLocales.Name = "cmbLocales";
            this.cmbLocales.Size = new System.Drawing.Size(168, 21);
            this.cmbLocales.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(291, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Fecha Hasta";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Fecha Desde";
            // 
            // hasta
            // 
            this.hasta.Location = new System.Drawing.Point(365, 19);
            this.hasta.Name = "hasta";
            this.hasta.Size = new System.Drawing.Size(200, 20);
            this.hasta.TabIndex = 7;
            this.hasta.ValueChanged += new System.EventHandler(this.hasta_ValueChanged_1);
            // 
            // desde
            // 
            this.desde.Location = new System.Drawing.Point(81, 19);
            this.desde.Name = "desde";
            this.desde.Size = new System.Drawing.Size(168, 20);
            this.desde.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(608, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(238, 107);
            this.button1.TabIndex = 22;
            this.button1.Text = "Ver";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtFinal
            // 
            this.txtFinal.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Bold);
            this.txtFinal.Location = new System.Drawing.Point(702, 502);
            this.txtFinal.Name = "txtFinal";
            this.txtFinal.ReadOnly = true;
            this.txtFinal.Size = new System.Drawing.Size(144, 30);
            this.txtFinal.TabIndex = 26;
            // 
            // lblCaja
            // 
            this.lblCaja.AutoSize = true;
            this.lblCaja.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblCaja.Location = new System.Drawing.Point(585, 508);
            this.lblCaja.Name = "lblCaja";
            this.lblCaja.Size = new System.Drawing.Size(57, 24);
            this.lblCaja.TabIndex = 25;
            this.lblCaja.Text = "Total";
            // 
            // retiros_ingresosxfecha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 543);
            this.Controls.Add(this.txtFinal);
            this.Controls.Add(this.lblCaja);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tablaRetiros);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(606, 270);
            this.Name = "retiros_ingresosxfecha";
            this.Text = "Retiros X Fecha";
            this.Load += new System.EventHandler(this.retirosxfecha_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tablaRetiros)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView tablaRetiros;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker hasta;
        private System.Windows.Forms.DateTimePicker desde;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbLocales;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.TextBox txtFinal;
        private System.Windows.Forms.Label lblCaja;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn monto;
        private System.Windows.Forms.DataGridViewTextBoxColumn desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    }
}