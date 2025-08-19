using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DTO;
using DTO.BusinessEntities;
using Repository.ColoresRepository;
using Repository.Repositories.ComprasProveedoresRepository;
using Repository.Repositories.PersonalRepository;
using Repository.Repositories.ProductoRepository;
using Repository.Repositories.ProveedorRepository;
using Repository.Repositories.StockRepository;
using Services;
using Services.ColorService;
using Services.ComprasProveedorService;
using Services.PersonalService;
using Services.ProductoService;
using Services.ProveedorService;
using Services.StockService;
using Services.TributoService;
using SharedForms;
using SharedForms.Ventas;

namespace Central.Proveedores
{
    public partial class comprasAProveedores : ventaBase, IreceptorArticulo
    {




        public comprasAProveedores()
        {
            InitializeComponent();
        }
        private void CargarTributos()
        {
            var tributoService = new TributoService();

            cmbTributo.DataSource = tributoService.GetAll();
            cmbTributo.DisplayMember = "Description";

        }

        private void comprasAProveedores_Load(object sender, EventArgs e)
        {


            SetControls(txtNeto, new TextBox(), txtUnidadTotal, txtIva, txtTotal, new TextBox(), txtTributos, txtObs,

                 txtTributoAlicuota, txtTributoImporte, txtTributoBase, radioAlicuota, cmbTributo,
                tabla, tablaTributos, new DataGridView(),
                cmbClase,new ComboBox(), cmbVendedor, new ComboBox(), cmbProveedores,
                fechaFactura, fechaContable);

            CargarTributos();
            cargarNumero();
            cargarProveedores();
            cargarColores();
            cargarClase();
            cargarVendedores();

            if (HelperService.talleUnico)
            {
                txtTalle.Text = "0";
                txtTalle.ReadOnly = true;
            }
            if (HelperService.haymts)
            {

                lbltalle.Text = "Mts";
                
                tabla.Columns[3].HeaderText = "Mts";
                tabla.Columns[3].ReadOnly = true;
            }
            else
            {
                tabla.Columns[8].Visible = false;
            }
            if (HelperService.talleUnico)
            {
                lbltalle.Visible = false;
                txtTalle.Visible = false;
                tabla.Columns[3].Visible = false;
            }
            

        }

        private void cargarVendedores()
        {


            var personalService = new PersonalService(new PersonalRepository());

            List<PersonalData> vendedores = personalService.GetAll();

            cmbVendedor.DataSource = vendedores;
            cmbVendedor.DisplayMember = "nombrecontacto";
        }

        private void cargarNumero()
        {
            var comprasProveedoresService = new ComprasProveedorService(new ComprasProveedoresRepository(),
                   new CompraProveedoresDetalleRepository());
            lblnum.Text = comprasProveedoresService.GetNextNumberAvailable(HelperService.IDLocal, HelperService.Prefix, true);
        }

        private void cargarColores()
        {
            ColorData c = new ColorData();
            if (cmbColores.DataSource != null && cmbColores.SelectedIndex > -1)
            {
                c = (ColorData) cmbColores.SelectedItem;
            }

            var colorService = new ColorService(new ColorRepository());

            cmbColores.DataSource = colorService.GetAll(true);
            cmbColores.DisplayMember = "Description";

            if (c.ID!= Guid.Empty)
            {
                cmbColores.SelectedItem =
                    ((List<ColorData>)cmbColores.DataSource).Find(d => d.Description.ToLower().StartsWith(c.Description));
            }

        }

        private void cargarProveedores()
        {
            List<ProveedorData> pvs = new ProveedorService(new ProveedorRepository()).GetAll(true);

            cmbProveedores.DataSource = pvs;
            cmbProveedores.DisplayMember = "razonSocial";
        }

        private void cmbProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabla.Rows.Count > 0)
            {
                DialogResult dg = MessageBox.Show("Si continua, se borraran todos los articulos previamente cargados. \n Desea continuar?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (dg == DialogResult.OK)
                {
                    if (cmbProveedores.SelectedIndex > -1)
                    {
                        cargarArticulos((ProveedorData)cmbProveedores.SelectedItem);
                        limpiarTablaTotal();
                    }
                }

            }
            else
            {
                if (cmbProveedores.SelectedIndex > -1)
                {
                    cargarArticulos((ProveedorData)cmbProveedores.SelectedItem);
                    limpiarTablaTotal();
                }
            }

        }



        private void cargarArticulos(ProveedorData proveedorData)
        {
            var productoService = new ProductoService(new ProductoRepository());
            ProductoData c = new ProductoData();
            if (cmbArticulos.DataSource != null && cmbArticulos.SelectedIndex > -1)
            {
                c = (ProductoData)cmbArticulos.SelectedItem;
            }
            List<ProductoData> aux = productoService.GetbyProveedor(proveedorData.ID);

            cmbArticulos.DataSource = aux;
            cmbArticulos.DisplayMember = "Show";



       

            if (c.ID != Guid.Empty)
            {
                cmbArticulos.SelectedItem =
                    ((List<ProductoData>)cmbArticulos.DataSource).Find(d => d.Description.ToLower().StartsWith(c.Description));
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private bool valido()
        {

            int talle = -1;
            int cant = -1;
            try
            {
                talle = Convert.ToInt32(txtTalle.Text);
                cant = Convert.ToInt32(txtCantidad.Text);
                HelperService.ConvertToDecimalSeguro(txtprecio.Text);
                HelperService.ConvertToDecimalSeguro(txtExtra.Text);
            }
            catch (Exception)
            {

                MessageBox.Show("Revise los parametros", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }


            if (txtCantidad.Text == "" || txtTalle.Text == "")
            {
                return false;
            }



            bool valido = true;
            if (talle < 0 || talle > 252)
            {
                valido = false;
            }
            if (cant < 0)
            {
                valido = false;
            }


            return valido;
        }



        private void button2_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void limpiar()
        {

            txtCantidad.Text = "";
            txtTalle.Text = "";
            txtTotal.Text = "";
            txtTributos.Text = "";
            txtSubtotal.Text = "";
            
            txtdescuento.Text = "0";

            tablaTributos.Rows.Clear();
            tablaTributos.ClearSelection();
            txtTributoAlicuota.Text = "0";
            txtTributoBase.Text = "0";
            txtTributoImporte.Text = "0";
            //cmbArticulos.DataSource = null;
            //cmbColores.SelectedIndex = -1;
            //cmbProveedores.SelectedIndex = -1;
        }

        private void txtTalle_Leave(object sender, EventArgs e)
        {



        }

        private void getStock()
        {

            var stockService = new StockService(new StockRepository());

            if (txtTalle.Text != "")
            {

                StockData s = new StockData();
                s.Color = (ColorData)cmbColores.SelectedItem;
                s.Local.ID = HelperService.IDLocal;
                s.Producto.ID = ((ProductoData)cmbArticulos.SelectedItem).ID;
                s.Talle = Convert.ToInt32(txtTalle.Text);
                decimal st = stockService.GetStockTotal(s.Producto.ID, s.Color.ID, s.Talle);


            }
            else
            {
                MessageBox.Show("Ingrese un talle valido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTalle.Focus();
            }
        }

        private void txtTalle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cmbColores_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        private void limpiarTablaTotal()
        {
            tabla.Rows.Clear();

            tabla.ClearSelection();
        }


        private void button3_Click_1(object sender, EventArgs e)
        {
            getStock();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var stockService = new StockService(new StockRepository());

            StockData s;
            if (validarDetalle("", cmbArticulos.SelectedIndex, cmbColores.SelectedIndex, txtTalle.Text, HelperService.ConvertToDecimalSeguro(txtprecio.Text,2).ToString(),txtCantidad.Text))
            {
                string auxCodigo = stockService.GetCodigoBarraDinamico(((ProductoData)cmbArticulos.SelectedItem),
               ((ColorData)cmbColores.SelectedItem), txtTalle.Text);
                
                decimal alicuotapercent = 0;

                if (HelperService.esCliente(GrupoCliente.Slipak))
                {
                    alicuotapercent = 21;
                }
                s = stockService.obtenerProducto(auxCodigo);
                if (!HelperService.validarCodigo(s.Codigo))
                {
                    s = new stockDummyData(auxCodigo);
                }
                
                

                AgregarArticulo(s, tabla, HelperService.ConvertToDecimalSeguro(txtprecio.Text).ToString(), HelperService.ConvertToDecimalSeguro(txtExtra.Text).ToString(),  HelperService.ConvertToDecimalSeguro(txtCantidad.Text,3), alicuotapercent);




                CalcularTotales();
                limpiartxt();
            }
            if (HelperService.talleUnico)
            {
                txtTalle.Text = "0";
                txtTalle.ReadOnly = true;
            }

            cargarColores();
            cargarArticulos((ProveedorData)cmbProveedores.SelectedItem);
        }

        

        private void limpiartxt()
        {
            txtTalle.Text = "0";
            txtprecio.Text = "0";
            txtCantidad.Text = "0";
            txtExtra.Text = "0";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var comprasProveedoresService = new ComprasProveedorService(new ComprasProveedoresRepository(),
                      new CompraProveedoresDetalleRepository());
            DialogResult dg = MessageBox.Show("Desea confirmar la operacion?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            bool task;
            if (dg == DialogResult.OK)
            {
                if (validoTodo())
                {
                    ComprasProveedoresData r = cargoCompra();



                    task = comprasProveedoresService.Insert(r);

                    
                    txtObs.Text = "";
                    txtTotal.Text = "0";

                    if (task)
                    {
                        MessageBox.Show("Operacion completada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        limpiarTablaTotal();
                        limpiar();
                        cargarNumero();
                    }
                    else
                    {
                        MessageBox.Show("Ocurrio un error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show("Revise los parametros", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (HelperService.talleUnico)
                {
                    txtTalle.Text = "0";
                    txtTalle.ReadOnly = true;
                }
            }

        }

        private ComprasProveedoresData cargoCompra()
        {
            ComprasProveedoresData r = new ComprasProveedoresData();


            switch (cmbClase.Text.ToLower())
            {
                case "a":
                    r.ClaseDocumento = ClaseDocumento.A;
                    break;

                case "b":
                    r.ClaseDocumento = ClaseDocumento.B;
                    break;

                case "c":
                    r.ClaseDocumento = ClaseDocumento.C;
                    break;
            }
            r.Date = fechaContable.Value;
            r.FechaFactura = fechaFactura.Value;
            r.Local.ID = HelperService.IDLocal;
            r.Vendedor.ID = ((PersonalData)cmbVendedor.SelectedItem).ID;
            r.Proveedor = ((ProveedorData)cmbProveedores.SelectedItem);
            r.ID = Guid.NewGuid();
            r.Description = txtObs.Text;
            r.Prefix = HelperService.Prefix;
            r.Numero = Convert.ToInt32(lblnum.Text.Split('-')[1]);

            r.Monto = HelperService.ConvertToDecimalSeguro(txtTotal.Text);
            r.IVA = HelperService.ConvertToDecimalSeguro(txtTributos.Text);
            
            r.Descuento = HelperService.ConvertToDecimalSeguro(txtdescuento.Text);
            ComprasProveedoresdetalleData rd;
            List<ComprasProveedoresdetalleData> rds = new List<ComprasProveedoresdetalleData>();


            foreach (DataGridViewRow row in tabla.Rows)
            {

                rd = new ComprasProveedoresdetalleData();
                
                rd.Codigo = tabla[0, row.Index].Value.ToString();
                rd.FatherID = r.ID;
                rd.Cantidad = HelperService.ConvertToDecimalSeguro(tabla[7, row.Index].Value.ToString());
                rd.PrecioExtra = HelperService.ConvertToDecimalSeguro(tabla[5, row.Index].Value.ToString());
                rd.PrecioUnidad = HelperService.ConvertToDecimalSeguro(tabla[4, row.Index].Value.ToString());
                rd.Alicuota = HelperService.ConvertToDecimalSeguro(tabla[10, row.Index].Value.ToString());
                rd.Bonificacion = HelperService.ConvertToDecimalSeguro(tabla[6, row.Index].Value.ToString());
                rds.Add(rd);

            }

            r.Tributos = cargoTributos();
            
            r.Children = rds;
            r.Enable = true;
            return r;
        }

        private bool validoTodo()
        {
            if (tabla.Rows.Count == 0)//que haya articulos
            {

                MessageBox.Show("Debe de ingresar al menos un articulo", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (txtTotal.Text == "")//que haya articulos
            {

                MessageBox.Show("Debe de ingresar el Monto de la operacion", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTotal.Focus();
                return false;
            }


            try
            {
                HelperService.ConvertToDecimalSeguro(txtTotal.Text);
            }
            catch (Exception)
            {

                return false;
            }

            //hay tributos, entonces valido que los que sean porcentual se hayan realizado con el neto final
            foreach (DataGridViewRow r in tablaTributos.Rows)
            {

                var baseTributo = HelperService.ConvertToDecimalSeguro(r.Cells[2].Value);
                if (baseTributo > 0 && baseTributo != HelperService.ConvertToDecimalSeguro(txtNeto.Text))
                {
                    MessageBox.Show(string.Format("El tributo {0} tiene un neto diferente al de la venta.Debe de borrarlo y volverlo a ingresar.", r.Cells[1].Value), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            return true;

        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperService.VerificoTextBoxNumerico(txtCantidad, e);
            
        }

        private void txtTotal_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtprecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperService.VerificoTextBoxNumerico(txtprecio, e);
           

        }

        private void txtExtra_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperService.VerificoTextBoxNumerico(txtExtra, e);
            
        }


        private void cmbClase_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            SeteoAlicuotas(tabla,cmbClase.Text);
            
            foreach (DataGridViewRow row in tabla.Rows)
            {
                calcularCantidad(row.Index, tabla);
            }

            CalcularTotales();
        }

        private void tabla_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (tabla.Rows.Count > 0)
            {
                calcularCantidad(e.RowIndex, tabla);

                CalcularTotales();
            }


        }

        private void txtTalle_Enter(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                txtTalle.SelectAll();
            });
        }

        private void txtCantidad_Enter(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                txtCantidad.SelectAll();
            });
        }

        private void txtprecio_Enter(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                txtprecio.SelectAll();
            });
        }

        private void txtExtra_Enter(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                txtExtra.SelectAll();
            });
        }

        private void tabla_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {


            CalcularTotales();
        }



        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void txtIIBB_Enter(object sender, EventArgs e)
        {
           
        }

        private void txtIIBBProv_Enter(object sender, EventArgs e)
        {
           
        }

       

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            cargarProveedores();
            cargarColores();
        }

        private void cmbArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        public void selectProducto(Guid idproducto)
        {
            var productoService = new ProductoService(new ProductoRepository());
            ProductoData p = productoService.GetByID(idproducto);
            
            cmbArticulos.SelectedIndex = cmbArticulos.FindStringExact(p.Show);
            txtCantidad.Focus();
        }

        private void comprasAProveedores_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F3") //f12
            {
                padreBase.AbrirForm(new BuscarArticulo(this,(ProveedorData)cmbProveedores.SelectedItem), this.MdiParent, false, FormStartPosition.CenterScreen);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            padreBase.AbrirForm(new BuscarArticulo(this, (ProveedorData)cmbProveedores.SelectedItem), this.MdiParent, false, FormStartPosition.CenterScreen);
        }

        private void txtdescuento_TextChanged(object sender, EventArgs e)
        {
            if (txtdescuento.Text == "" || HelperService.ConvertToDecimalSeguro(txtdescuento.Text) > 100)
            {
                txtdescuento.Text = "0";
            }
            CalcularTotales();
        }

        private void txtIIBBProv_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtIIBB_TextChanged(object sender, EventArgs e)
        {

        }

      
       

    

     

        private void tabla_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 12)
            {
                tabla[7, e.RowIndex].Value = (HelperService.ConvertToDecimalSeguro(tabla[7, e.RowIndex].Value) * -1).ToString();
            }
            if (e.RowIndex > 0)
                ActualizarRow(e.RowIndex, e.ColumnIndex);
        }

        private void ActualizarRow(int RowIndex, int p2)
        {
            calcularCantidad(RowIndex, tabla);
            CalcularTotales();
        }

        private void tabla_CellEndEdit_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (!IsDigitsOnly(tabla[7, e.RowIndex].Value.ToString()))
                {
                    tabla[7, e.RowIndex].Value = "1";
                }
                if (!IsDigitsOnly(tabla[6, e.RowIndex].Value.ToString()))
                {
                    tabla[6, e.RowIndex].Value = "0";
                }
                tabla[12, e.RowIndex].Value = !(HelperService.ConvertToDecimalSeguro(tabla[7, e.RowIndex].Value) > 0);

                ActualizarRow(e.RowIndex, e.ColumnIndex);
            }
        }

        private void txtExtra_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            HelperService.VerificoTextBoxNumerico(txtExtra, e);
        }

        private void tabla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (ValidoTributo(txtTributoImporte.Text))
            {
                AgregoTributo();
                txtTributoImporte.Text = "";


                CalcularTotales();
            }
        }

        private void radioAlicuota_CheckedChanged(object sender, EventArgs e)
        {
            TributoRadioButton();
        }

        private void radioImporte_CheckedChanged(object sender, EventArgs e)
        {
            TributoRadioButton();
        }

        private void txtTributoAlicuota_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperService.VerificoTextBoxNumerico(txtTributoAlicuota, e);
        }

        private void txtTributoImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperService.VerificoTextBoxNumerico(txtTributoImporte, e);
        }

        private void txtTributoAlicuota_TextChanged(object sender, EventArgs e)
        {
            CalcularTributoImporte();
        }

        private void tablaTributos_RowsRemoved_1(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            //txtRecargos.Text = calcularTributo(tablaTributos).ToString(); // todo! ver esto
            CalcularTotales();
        }
    }
}
