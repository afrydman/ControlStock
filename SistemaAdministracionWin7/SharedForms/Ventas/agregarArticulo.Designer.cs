namespace SharedForms.Ventas
{
    partial class agregarArticulo
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.lblinterno = new System.Windows.Forms.Label();
            this.txtinterno = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.cmbProveedor = new System.Windows.Forms.ComboBox();
            this.cmbArticulo = new System.Windows.Forms.ComboBox();
            this.cmbColores = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.txtprecio = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtstocktotal = new System.Windows.Forms.TextBox();
            this.lblTalle = new System.Windows.Forms.Label();
            this.txtPongotalle = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.lblinterno);
            this.groupBox1.Controls.Add(this.txtinterno);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.cmbProveedor);
            this.groupBox1.Controls.Add(this.cmbArticulo);
            this.groupBox1.Controls.Add(this.cmbColores);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtprecio);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtstocktotal);
            this.groupBox1.Controls.Add(this.lblTalle);
            this.groupBox1.Controls.Add(this.txtPongotalle);
            this.groupBox1.Location = new System.Drawing.Point(4, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(441, 227);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(370, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "?";
            this.toolTip1.SetToolTip(this.label1, "Ingrese 0  o * y luego Enter para buscar");
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(4, 192);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(86, 28);
            this.button4.TabIndex = 23;
            this.button4.Text = "Cargar Pedido";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            // 
            // lblinterno
            // 
            this.lblinterno.AutoSize = true;
            this.lblinterno.Location = new System.Drawing.Point(2, 22);
            this.lblinterno.Name = "lblinterno";
            this.lblinterno.Size = new System.Drawing.Size(73, 13);
            this.lblinterno.TabIndex = 22;
            this.lblinterno.Text = "Codigo Barras";
            // 
            // txtinterno
            // 
            this.txtinterno.Location = new System.Drawing.Point(83, 19);
            this.txtinterno.MaxLength = 20;
            this.txtinterno.Name = "txtinterno";
            this.txtinterno.Size = new System.Drawing.Size(281, 20);
            this.txtinterno.TabIndex = 1;
            this.txtinterno.TextChanged += new System.EventHandler(this.txtinterno_TextChanged);
            this.txtinterno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtinterno_KeyPress);
            this.txtinterno.Leave += new System.EventHandler(this.txtinterno_Leave);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(3, 102);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(31, 13);
            this.label17.TabIndex = 21;
            this.label17.Text = "Color";
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
            this.cmbProveedor.Location = new System.Drawing.Point(83, 45);
            this.cmbProveedor.Name = "cmbProveedor";
            this.cmbProveedor.Size = new System.Drawing.Size(352, 21);
            this.cmbProveedor.TabIndex = 2;
            this.cmbProveedor.SelectedIndexChanged += new System.EventHandler(this.cmbProveedor_SelectedIndexChanged);
            // 
            // cmbArticulo
            // 
            this.cmbArticulo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbArticulo.FormattingEnabled = true;
            this.cmbArticulo.Location = new System.Drawing.Point(83, 72);
            this.cmbArticulo.Name = "cmbArticulo";
            this.cmbArticulo.Size = new System.Drawing.Size(352, 21);
            this.cmbArticulo.TabIndex = 3;
            this.cmbArticulo.SelectedIndexChanged += new System.EventHandler(this.cmbArticulo_SelectedIndexChanged);
            // 
            // cmbColores
            // 
            this.cmbColores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColores.FormattingEnabled = true;
            this.cmbColores.Location = new System.Drawing.Point(83, 102);
            this.cmbColores.Name = "cmbColores";
            this.cmbColores.Size = new System.Drawing.Size(181, 21);
            this.cmbColores.TabIndex = 4;
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(335, 192);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 7;
            this.button1.Text = "Agregar (F4)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 155);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(37, 13);
            this.label12.TabIndex = 9;
            this.label12.Text = "Precio";
            // 
            // txtprecio
            // 
            this.txtprecio.Location = new System.Drawing.Point(83, 152);
            this.txtprecio.Name = "txtprecio";
            this.txtprecio.Size = new System.Drawing.Size(110, 20);
            this.txtprecio.TabIndex = 6;
            this.txtprecio.Enter += new System.EventHandler(this.txtprecio_Enter);
            this.txtprecio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtprecio_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(2, 207);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Stock Disponible";
            this.label7.Visible = false;
            // 
            // txtstocktotal
            // 
            this.txtstocktotal.Location = new System.Drawing.Point(154, 204);
            this.txtstocktotal.Name = "txtstocktotal";
            this.txtstocktotal.ReadOnly = true;
            this.txtstocktotal.Size = new System.Drawing.Size(110, 20);
            this.txtstocktotal.TabIndex = 0;
            this.txtstocktotal.TabStop = false;
            this.txtstocktotal.Visible = false;
            // 
            // lblTalle
            // 
            this.lblTalle.AutoSize = true;
            this.lblTalle.Location = new System.Drawing.Point(3, 129);
            this.lblTalle.Name = "lblTalle";
            this.lblTalle.Size = new System.Drawing.Size(30, 13);
            this.lblTalle.TabIndex = 1;
            this.lblTalle.Text = "Talle";
            // 
            // txtPongotalle
            // 
            this.txtPongotalle.Location = new System.Drawing.Point(83, 126);
            this.txtPongotalle.MaxLength = 20;
            this.txtPongotalle.Name = "txtPongotalle";
            this.txtPongotalle.Size = new System.Drawing.Size(110, 20);
            this.txtPongotalle.TabIndex = 5;
            this.txtPongotalle.TextChanged += new System.EventHandler(this.txtPongotalle_TextChanged);
            this.txtPongotalle.Enter += new System.EventHandler(this.txtPongotalle_Enter);
            this.txtPongotalle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPongotalle_KeyPress);
            this.txtPongotalle.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPongotalle_KeyUp);
            // 
            // agregarArticulo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 245);
            this.Controls.Add(this.groupBox1);
            this.KeyPreview = true;
            this.Name = "agregarArticulo";
            this.Text = "Agregar Articulo";
            this.Load += new System.EventHandler(this.agregarArticulo_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.agregarArticulo_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label lblinterno;
        private System.Windows.Forms.TextBox txtinterno;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cmbProveedor;
        private System.Windows.Forms.ComboBox cmbArticulo;
        private System.Windows.Forms.ComboBox cmbColores;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtprecio;
        private System.Windows.Forms.Label lblTalle;
        private System.Windows.Forms.TextBox txtPongotalle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtstocktotal;
    }
}