using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DTO;
using DTO.BusinessEntities;
using ObjectDumper;
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
    public partial class MostrarcomprasAProveedores : ventaBase, IreceptorArticulo, IVentaBaseSeteable
    {



        private ComprasProveedoresData _compra = null;
        public MostrarcomprasAProveedores()
        {
            InitializeComponent();
        }
        public MostrarcomprasAProveedores(ComprasProveedoresData c)
        {
            _compra = c;
            InitializeComponent();
        }

        private void comprasAProveedores_Load(object sender, EventArgs e)
        {

            SetControls(txtNeto, new TextBox(), txtUnidadTotal, txtIva, txtTotal, new TextBox(), txtTributos,txtObs,

                txtTributoAlicuota, txtTributoImporte, txtTributoBase, radioAlicuota, cmbTributo,
                tabla,tablaTributos, new DataGridView(), 
                cmbClase, new ComboBox(), cmbVendedor, new ComboBox(), cmbProveedores,
                fechaFactura, fechaContable);

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


            try
            {
                if (_compra != null)
                    cargarCompra(_compra);
            }
            catch (Exception ee)
            {
                HelperService.writeLog(ee.ToString(), true, true);
                HelperService.writeLog(
                               ObjectDumperExtensions.DumpToString(_compra, "MostrarCompra"), true, true);

            }
           

          
        }

        private void cargarCompra(ComprasProveedoresData _compra)
        {
            var comprasProveedoresService = new ComprasProveedorService(new ComprasProveedoresRepository(),
                   new CompraProveedoresDetalleRepository());

            var stockService = new StockService(new StockRepository());
            cmbClase.Text = _compra.ClaseDocumento.ToString();
            txtObs.Text = _compra.Description;
            txtTotal.Text = _compra.Monto.ToString();


            lblnum.Text = _compra.NumeroCompleto;
            fechaContable.Value = _compra.Date;
            fechaFactura.Value = _compra.FechaFactura;

            txtdescuento.Text = _compra.Descuento.ToString();
            var proveedorService = new ProveedorService(new ProveedorRepository());
            bool setcmb = false;
            StockData s;
            foreach (ComprasProveedoresdetalleData item in _compra.Children)
            {
                s = stockService.obtenerProducto(item.Codigo);
                if (!HelperService.validarCodigo(s.Codigo))
                {
                    s = new stockDummyData(item.Codigo);

                }
                else
                {
                    if (!setcmb)
                    {

                        ProveedorData pp = proveedorService.GetByID(s.Producto.Proveedor.ID);
                        cmbProveedores.Text = pp.RazonSocial;
                        setcmb = true;
                    }
                }

                AgregarArticulo(s, tabla, item.PrecioUnidad.ToString(), item.PrecioExtra.ToString(), item.Cantidad, item.Alicuota,item.Bonificacion);


                calcularCantidad(tabla.RowCount - 1, tabla);
                
                
            }

            //Tributos.

            var tributoService = new TributoService();
            TributoData tr;
            foreach (TributoNexoData tributo in _compra.Tributos)
            {

                tr = tributoService.GetByID(tributo.Tributo.ID);
                AgregarTributoATabla(tr, tributo.Base,tributo.Alicuota,tributo.Importe, tablaTributos);


            }
            txtTributos.Text = calcularTributo(tablaTributos).ToString();
            CalcularTotales();

            if (!_compra.Enable)
            {
                button1.Enabled = false;
            }

            CalcularTotales();

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
            //txtIIBB.Text = "0";
            //txtIIBBProv.Text = "0";
            txtdescuento.Text = "0";
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

        private void CalcularTotales()
        {
            txtNeto.Text = CalcularSubTotal(tabla).ToString();
            txtUnidadTotal.Text = CalcularCantidadTotal(tabla);
            txtIva.Text = calcularIva(tabla).ToString();
            txtTotal.Text = CalcularTotal(tabla, tablaTributos,HelperService.ConvertToDecimalSeguro(txtdescuento.Text)).ToString();
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


                    task = comprasProveedoresService.Disable(_compra.ID,false);//anular !

                    limpiarTablaTotal();
                    txtObs.Text = "";
                    txtTotal.Text = "0";

                    if (task)
                    {
                        MessageBox.Show("Operacion completada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                rd.Alicuota = HelperService.ConvertToDecimalSeguro(tabla[6, row.Index].Value.ToString());
                rds.Add(rd);

            }
            foreach (DataGridViewRow f in tablaTributos.Rows)
            {

                TributoNexoData tNexo = new TributoNexoData();
                TributoData tributo = new TributoData();
                tributo.ID = new Guid(f.Cells[0].Value.ToString());
                tNexo.Importe = HelperService.ConvertToDecimalSeguro(f.Cells[2].Value.ToString());
                tNexo.Tributo = tributo;

                r.Tributos.Add(tNexo);
            }


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

        private void label6_Click(object sender, EventArgs e)
        {

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

        private void txtIIBB_Leave(object sender, EventArgs e)
        {
            CalcularTotales();
        }

        private void txtIIBBProv_Leave(object sender, EventArgs e)
        {
            CalcularTotales();
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

      
        private bool ValidoTributo(string valor)
        {
            if (valor == "" || valor == "." || valor == ",")
            {
                MessageBox.Show("Debe de ingresar un Valor", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (HelperService.ConvertToDecimalSeguro(valor) <= 0)
            {
                MessageBox.Show("Debe de ingresar un valor mayor a cero", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
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

        private void tabla_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
    }
}
