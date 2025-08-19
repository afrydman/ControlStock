//using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using DTO;
using DTO.BusinessEntities;
//using Microsoft.Office.Interop.Excel;
using Repository.ClienteRepository;
using Repository.ColoresRepository;
using Repository.Repositories.FormaPagoRepository;
using Repository.Repositories.ListaPrecioRepository;
using Repository.Repositories.PersonalRepository;
using Repository.Repositories.ProveedorRepository;
using Repository.Repositories.VentaRepository;
using Services;
using Services.ClienteService;
using Services.ColorService;
using Services.FormaPagoService;
using Services.ListaPrecioService;
using Services.PersonalService;
using Services.ProveedorService;
using Services.VentaService;
using TextBox = System.Windows.Forms.TextBox;

namespace SharedForms.Ventas
{
    public class ventaBase : Form, IVentaBaseSeteable
    {
        private TextBox _txtNeto;
        private TextBox _txtSubtotal;
        private TextBox _txtUnidadTotal;
        private TextBox _txtiva;
        private TextBox _txtTotal;
        private TextBox _txtDescuento;
        private TextBox _txtRecargos;
        private TextBox _txtObs;

        private TextBox _txttributoAlicuota;
        private TextBox _txttributoImporte;
        private TextBox _txttributoBase;
        private RadioButton _radioTributoAlicuota;
        private ComboBox _cmbTributo;


        private DataGridView _tabla;
        private DataGridView _tablaTributos;
        private DataGridView _tablaPagos;


        private ComboBox _cmbClase;
        private ComboBox _cmbClientes;
        private ComboBox _cmbVendedor;
        private ComboBox _cmbListaPrecio;
        private DateTimePicker _pickerfecha;
        private DateTimePicker _fechaContable;
        private ComboBox _cmbProveedores;

       

        public void SetControls(TextBox txtNeto, TextBox txtSubtotal, TextBox txtUnidadTotal, TextBox txtIva, TextBox txtTotal, TextBox txtDescuento, TextBox txtRecargo, TextBox txtObs,

            TextBox txttributoAlicuota, TextBox txttributoImporte, TextBox txttributoBase, RadioButton radioTributoAlicuota, ComboBox cmbTributo,

            DataGridView tabla, DataGridView tablaTributos, DataGridView tablaPagos,
            ComboBox cmbClase, ComboBox cmbClientes, ComboBox cmbVendedor, ComboBox cmbListaPrecio, ComboBox cmbProveedores,
            DateTimePicker fechaFactura, DateTimePicker fechaContable)
        {
            _txtNeto = txtNeto;
            _txtSubtotal = txtSubtotal;
            _txtUnidadTotal = txtUnidadTotal;
            _txtiva = txtIva;
            _txtTotal = txtTotal;
            _txtDescuento = txtDescuento;
            _txtRecargos = txtRecargo;
            _txtObs = txtObs;


            _txttributoAlicuota = txttributoAlicuota;
            _txttributoImporte = txttributoImporte;
            _txttributoBase = txttributoBase;
            _radioTributoAlicuota = radioTributoAlicuota;
            _cmbTributo = cmbTributo;

            _tabla = tabla;
            _tablaTributos = tablaTributos;
            _tablaPagos = tablaPagos;

            _cmbClase = cmbClase;
            _cmbClientes = cmbClientes;
            _cmbVendedor = cmbVendedor;
            _cmbListaPrecio = cmbListaPrecio;
            _cmbProveedores = cmbProveedores;


            _pickerfecha = fechaFactura;
            _fechaContable = fechaContable;
        }
        private void ventaBase_Load(object sender, EventArgs e)
        {

         
        }

        public ventaBase()
        {
            InitializeComponent();
        }

        

        public void agregarPago(Guid idformapago, string descripcion, int cuota, string lote, string cupon, string recargo, string importe, string ImporteRecargo)
        {


            decimal grlRecargo = 0;
            _tablaPagos.Rows.Add();
            int fila;
            fila = _tablaPagos.RowCount - 1;
            //Codigo nombre  color talle subtotal
            _tablaPagos[0, fila].Value = idformapago;
            _tablaPagos[1, fila].Value = descripcion;//+  "-" +cuota.ToString() +" Cuotas";
            _tablaPagos[2, fila].Value = cuota;
            _tablaPagos[3, fila].Value = recargo;
            _tablaPagos[4, fila].Value = lote;
            _tablaPagos[5, fila].Value = cupon;
            _tablaPagos[6, fila].Value = importe;
            _tablaPagos[7, fila].Value = ImporteRecargo;




            CalcularTotales();
        }

        public bool ValidoTributo(string valor)
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

        public bool IsDigitsOnly(string str)
        {
            double ignoreMe;
            return double.TryParse(HelperService.replace_decimal_separator(str), out ignoreMe);
        }

        public virtual bool validarVenta()
        {
            if (_tabla.Rows.Count == 0)//que haya articulos
            {

                MessageBox.Show("Debe de ingresar al menos un articulo", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (_cmbClientes.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un cliente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (_cmbVendedor.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un vendedor", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (_tablaPagos.Rows.Count == 0)//que haya alguna forma de pago
            {
                MessageBox.Show("Seleccione una forma de pago", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }



            if (!esCambio(_tabla))
            {
                decimal aux = 0;
                //bool ccpago = false;
                foreach (DataGridViewRow row in _tablaPagos.Rows)
                {
                    aux += HelperService.ConvertToDecimalSeguro(_tablaPagos[7, row.Index].Value);
                    //ccpago = new Guid (tablapagos[0, row.Index].Value.ToString()) == HelperService.IDCC;
                }

                if (HelperService.ConvertToDecimalSeguro(_txtTotal.Text) != aux)
                {
                    MessageBox.Show("La suma de formas de pago debe de coincidir con el importe Total", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }


            //hay tributos, entonces valido que los que sean porcentual se hayan realizado con el neto final
            foreach (DataGridViewRow r in _tablaTributos.Rows)
            {

                var baseTributo = HelperService.ConvertToDecimalSeguro(r.Cells[2].Value);
                if (baseTributo > 0 && baseTributo != HelperService.ConvertToDecimalSeguro(_txtNeto.Text))
                {
                    MessageBox.Show(string.Format("El tributo {0} tiene un neto diferente al de la venta.Debe de borrarlo y volverlo a ingresar.", r.Cells[1].Value), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            return true;


        }

        public virtual void AgregoTributo()
        {

            AgregarTributoATabla((TributoData)_cmbTributo.SelectedItem, HelperService.ConvertToDecimalSeguro(_txttributoBase.Text), HelperService.ConvertToDecimalSeguro(_txttributoAlicuota.Text), HelperService.ConvertToDecimalSeguro(_txttributoImporte.Text), _tablaTributos);
            LimpioControlesTributo();
        }

        private void LimpioControlesTributo()
        {
            _txttributoAlicuota.Text = "0";
            _txttributoImporte.Text = "0";
        }

        public void AgregarTributoATabla(TributoData tributo, decimal baseImporte, decimal alicuota, decimal importe, DataGridView tablaT)
        {
            tablaT.Rows.Add();



            int fila;
            fila = tablaT.RowCount - 1;

            tablaT[0, fila].Value = tributo.ID;
            tablaT[1, fila].Value = tributo.Description;
            if (alicuota != 0)
            {
                tablaT[2, fila].Value = baseImporte.ToString();
                tablaT[3, fila].Value = alicuota.ToString();
            }
            tablaT[4, fila].Value = importe.ToString();


        }
        public string CalcularCantidadTotal(DataGridView tabla)//para el textbox de Cantidad SubTotal / pares
        {
            decimal cantidad = 0;


            foreach (DataGridViewRow r in tabla.Rows)
            {
                cantidad += HelperService.ConvertToDecimalSeguro(r.Cells[8].Value);
            }

            return HelperService.haymts ? cantidad.ToString() : Convert.ToInt32(cantidad).ToString();

        }

        public void TributoRadioButton()
        {
            _txttributoAlicuota.ReadOnly = !_radioTributoAlicuota.Checked;
            _txttributoImporte.ReadOnly = _radioTributoAlicuota.Checked;
            _txttributoAlicuota.Text = "0";
            _txttributoImporte.Text = "0";
        }

        public virtual void AgregarArticulo(StockData s, DataGridView tabla, string txtprecio, string txtprecioExtra = "0", decimal cantidad = 1, decimal alicuot = 0, decimal bonificacion = 0)
        {
            decimal precio = HelperService.ConvertToDecimalSeguro(txtprecio);
            decimal extra = HelperService.ConvertToDecimalSeguro(txtprecioExtra);

            tabla.Rows.Add();
            int fila;
            fila = tabla.RowCount - 1;

            //Codigo
            tabla[0, fila].Value = s.Codigo;

            /// Descripcion, color y talle/mts
            if ((s.GetType() == typeof(StockData)))
            {
                tabla[1, fila].Value = s.Producto.Show;
                tabla[2, fila].Value = s.Color.Description;
                tabla[3, fila].Value = HelperService.haymts ? s.Metros : s.Talle;
            }
            else
            {
                tabla[1, fila].Value = "Sin descripcion";
                tabla[2, fila].Value = "Sin descripcion";
                tabla[3, fila].Value = s.Codigo.Substring(10, 2);
            }

            //$Unitario
            tabla[4, fila].Value = txtprecio;


            //$Extra
            tabla[5, fila].Value = extra;



            //Bonificacion
            tabla[6, fila].Value = bonificacion;


            //Cantidad
            tabla[7, fila].Value = cantidad;

            //Cantidad Total
            decimal auxMts = 1;
            auxMts = HelperService.haymts ? s.Metros : 1;
            tabla[8, fila].Value = HelperService.ConvertToDecimalSeguro(auxMts * cantidad);


            //SubTotal
            decimal subtotal = (precio + extra) * auxMts * cantidad;
            decimal bonificacionPorcentual = bonificacion;
            decimal Bonificacion = HelperService.ConvertToDecimalSeguro((subtotal * bonificacionPorcentual) / 100);
            decimal subtotalConBonif = HelperService.ConvertToDecimalSeguro(subtotal - Bonificacion);
            tabla[9, fila].Value = subtotalConBonif;


            //Alicuota
            tabla[10, fila].Value = alicuot.ToString();


            //SubTotal c/Iva
            tabla[11, fila].Value = HelperService.ConvertToDecimalSeguro((((alicuot * subtotalConBonif) / 100) + subtotalConBonif)).ToString();

            //Cambio
            tabla[12, fila].Value = false;

            tabla.ClearSelection();


            //calcularCantidad(fila, tabla);
        }

        //para Notas
        public virtual void AgregarArticulo(string Descripcion, DataGridView tabla, string txtprecio, string txtprecioExtra = "0", decimal cantidad = 1, decimal alicuot = 0, decimal bonificacion = 0)
        {
            decimal precio = HelperService.ConvertToDecimalSeguro(txtprecio);
            decimal extra = HelperService.ConvertToDecimalSeguro(txtprecioExtra);

            tabla.Rows.Add();
            int fila;
            fila = tabla.RowCount - 1;

            //Codigo
            tabla[0, fila].Value = "";

            /// Descripcion, color y talle/mts
            //Codigo
            tabla[0, fila].Value = "";

            /// Descripcion, color y talle/mts

            tabla[1, fila].Value = Descripcion;
            tabla[2, fila].Value = "";
            tabla[3, fila].Value = 1;



            //$Unitario
            tabla[4, fila].Value = txtprecio;


            //$Extra
            tabla[5, fila].Value = extra;



            //Bonificacion
            tabla[6, fila].Value = bonificacion;


            //Cantidad
            tabla[7, fila].Value = cantidad;

            //Cantidad Total
            decimal auxMts = 1;
            //auxMts = HelperService.haymts ? s.Metros : 1;
            tabla[8, fila].Value = HelperService.ConvertToDecimalSeguro(auxMts * cantidad);


            //SubTotal
            decimal subtotal = (precio + extra) * auxMts * cantidad;
            decimal bonificacionPorcentual = bonificacion;
            decimal Bonificacion = HelperService.ConvertToDecimalSeguro((subtotal * bonificacionPorcentual) / 100);
            decimal subtotalConBonif = HelperService.ConvertToDecimalSeguro(subtotal - Bonificacion);
            tabla[9, fila].Value = subtotalConBonif;


            //Alicuota
            tabla[10, fila].Value = alicuot.ToString();


            //SubTotal c/Iva
            tabla[11, fila].Value = HelperService.ConvertToDecimalSeguro((((alicuot * subtotalConBonif) / 100) + subtotalConBonif)).ToString();

            //Cambio
            tabla[12, fila].Value = false;

            tabla.ClearSelection();


            //calcularCantidad(fila, tabla);
        }

        public virtual void calcularCantidad(int rowIndex, DataGridView tabla)//Cuando se edita la row.
        {
            decimal precio = HelperService.ConvertToDecimalSeguro(tabla[4, rowIndex].Value.ToString());
            decimal extra = HelperService.ConvertToDecimalSeguro(tabla[5, rowIndex].Value.ToString());

            //Cantidad Total
            decimal auxMts = HelperService.haymts ? HelperService.ConvertToDecimalSeguro(tabla[3, rowIndex].Value.ToString()) : 1;
            decimal cantidad = HelperService.ConvertToDecimalSeguro(tabla[7, rowIndex].Value.ToString());
            decimal cantidadTotal = auxMts * cantidad;


            tabla[8, rowIndex].Value = cantidadTotal;


            //Subtotal
            decimal subtotal = (precio + extra) * auxMts * cantidad;
            decimal bonificacionPorcentual = HelperService.ConvertToDecimalSeguro(tabla[6, rowIndex].Value);
            if (bonificacionPorcentual > 100)
            {

                bonificacionPorcentual = 0;
                tabla[6, rowIndex].Value = 0;
            }
            decimal Bonificacion = HelperService.ConvertToDecimalSeguro((subtotal * bonificacionPorcentual) / 100);
            decimal subtotalConBonif = HelperService.ConvertToDecimalSeguro(subtotal - Bonificacion);
            tabla[9, rowIndex].Value = subtotalConBonif;




            //SubTotal c/Iva
            decimal alic = HelperService.ConvertToDecimalSeguro(tabla[10, rowIndex].Value.ToString());
            tabla[11, rowIndex].Value = HelperService.ConvertToDecimalSeguro(((((alic * subtotalConBonif) / 100)) + subtotalConBonif)).ToString();


        }

        public virtual void SeteoAlicuotas(DataGridView tabla, string clase)//Cuando se cambia el combo de clase de factura ->b o c
        {
            if (clase.ToLower() != ClaseDocumento.A.ToString().ToLower())
            {
                foreach (DataGridViewRow row in tabla.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        row.Cells[10].Value = "0";
                    }
                }
                tabla.Columns[10].ReadOnly = true;
            }
            else
                tabla.Columns[10].ReadOnly = false;
        }


        public decimal CalcularSubTotal(DataGridView tabla)//para txt neto SubTotal.
        {
            decimal Subtotal = 0;
            foreach (DataGridViewRow row in tabla.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    Subtotal += HelperService.ConvertToDecimalSeguro(row.Cells[9].Value);
                }
            }

            return HelperService.ConvertToDecimalSeguro(Subtotal);
        }

        public decimal CalcularTotal(DataGridView tabla, DataGridView tributos, decimal descuento = 0)
        {
            decimal iva = calcularIva(tabla);

            decimal subtotal = CalcularSubTotal(tabla);

            decimal neto = CalcularDescuento(subtotal, descuento);

            decimal tributosValue = calcularTributo(tributos);

            decimal total = HelperService.ConvertToDecimalSeguro(iva + neto + tributosValue);

            return total;

        }

        public List<TributoNexoData> cargoTributos()
        {
            List<TributoNexoData> aux = new List<TributoNexoData>();

            foreach (DataGridViewRow f in _tablaTributos.Rows)
            {

                TributoNexoData tNexo = new TributoNexoData();
                TributoData tributo = new TributoData();
                tributo.ID = new Guid(f.Cells[0].Value.ToString());
                tNexo.Base = HelperService.ConvertToDecimalSeguro(f.Cells[2].Value);
                tNexo.Alicuota = HelperService.ConvertToDecimalSeguro(f.Cells[3].Value);
                tNexo.Importe = HelperService.ConvertToDecimalSeguro(f.Cells[4].Value);
                tNexo.Tributo = tributo;

                aux.Add(tNexo);
            }


            return aux;

        }

        public void CalcularTotales()
        {
            _txtUnidadTotal.Text = CalcularCantidadTotal(_tabla);

            _txtSubtotal.Text = CalcularSubTotal(_tabla).ToString();
            _txtNeto.Text = CalcularDescuento(HelperService.ConvertToDecimalSeguro(_txtSubtotal.Text), HelperService.ConvertToDecimalSeguro(_txtDescuento.Text)).ToString();
            _txttributoBase.Text = CalcularDescuento(HelperService.ConvertToDecimalSeguro(_txtSubtotal.Text), HelperService.ConvertToDecimalSeguro(_txtDescuento.Text)).ToString();
            _txtiva.Text = calcularIva(_tabla).ToString();
            _txtRecargos.Text = calcularTributo(_tablaTributos).ToString();

            _txtTotal.Text = CalcularTotal(_tabla, _tablaTributos, HelperService.ConvertToDecimalSeguro(_txtDescuento.Text)).ToString();

        }

        private decimal CalcularDescuento(decimal subtotal, decimal descuentoPorcentual)
        {
            decimal Bonificacion = HelperService.ConvertToDecimalSeguro((subtotal * descuentoPorcentual) / 100);
            return HelperService.ConvertToDecimalSeguro(subtotal - Bonificacion);

        }







        public virtual decimal calcularIva(DataGridView tabla)
        {
            decimal Iva = 0;

            foreach (DataGridViewRow row in tabla.Rows)
            {
                decimal subtotalConBonif = HelperService.ConvertToDecimalSeguro(tabla[9, row.Index].Value);
                decimal alicuota = HelperService.ConvertToDecimalSeguro(tabla[10, row.Index].Value.ToString());
                Iva += (alicuota * subtotalConBonif) / 100;
            }

            return HelperService.ConvertToDecimalSeguro(Iva);
        }

        public void CalcularTributoImporte()
        {
            decimal baseT = HelperService.ConvertToDecimalSeguro(_txttributoBase.Text);
            if (string.IsNullOrEmpty(_txttributoAlicuota.Text))
                _txttributoAlicuota.Text = "0";
            decimal alicuota = HelperService.ConvertToDecimalSeguro(_txttributoAlicuota.Text);

            if (alicuota >= 100 || alicuota <= 0)
                return;

            _txttributoImporte.Text = HelperService.ConvertToDecimalSeguro(((((alicuota * baseT) / 100)))).ToString();

        }

        public virtual decimal calcularTributo(DataGridView tabla)
        {
            decimal tributo = 0;

            foreach (DataGridViewRow row in tabla.Rows)
            {

                tributo += HelperService.ConvertToDecimalSeguro(tabla[4, row.Index].Value.ToString());
            }

            return HelperService.ConvertToDecimalSeguro(tributo);
        }



        public virtual void cargarClase()
        {
            List<string> clases = new List<string>();
            clases.Add("A");
            clases.Add("B");
            clases.Add("C");

            _cmbClase.DataSource = clases;
            _cmbClase.SelectedIndex = 2;

            if (HelperService.esCliente(GrupoCliente.Slipak))
            {
                _cmbClase.SelectedIndex = 0;
            }



        }
        public virtual void cargarFormasPago(ComboBox cmbFormaPago)
        {
            var formaPagoService = new FormaPagoService(new FormaPagoRepository(), new FormaPagoCuotasRepository());
            cmbFormaPago.DataSource = formaPagoService.GetAll();
            cmbFormaPago.DisplayMember = "Description";



        }
        public virtual void cargarClientes()
        {
            var ClienteService = new ClienteService(new ClienteRepository());
            _cmbClientes.DataSource = ClienteService.GetAll(true);
            _cmbClientes.DisplayMember = "razonSocial";


        }
        public virtual void cargarListaPrecios()
        {
            var listaPrecioService = new ListaPrecioService(new ListaPrecioRepository());
            _cmbListaPrecio.DataSource = listaPrecioService.GetAll();
            _cmbListaPrecio.DisplayMember = "Description";

            _cmbListaPrecio.SelectedIndex = 0;
        }

        public virtual void cargarColores(ComboBox cmbColores)
        {
            var colorService = new ColorService(new ColorRepository());
            cmbColores.DataSource = colorService.GetAll(true);
            cmbColores.DisplayMember = "Description";
        }
        public virtual void cagarProveedores(ComboBox cmbProveedor)
        {
            cmbProveedor.DataSource = new ProveedorService(new ProveedorRepository()).GetAll(true);
            cmbProveedor.DisplayMember = "razonSocial";
        }
        public virtual void cargarVendedores()
        {

            var personalService = new PersonalService(new PersonalRepository());

            List<PersonalData> vendedores = personalService.GetAll();

            _cmbVendedor.DataSource = vendedores;
            _cmbVendedor.DisplayMember = "nombrecontacto";




        }

        public virtual string obtenerNumeroFactura()
        {
            var ventaService = new VentaService(new VentaRepository(), new VentaDetalleRepository());
            return ventaService.GetNextNumberAvailable(HelperService.IDLocal, HelperService.Prefix, true);

        }

        public void calcularRecargo(DataGridView tablapagos, TextBox txtRecargos)
        {
            decimal grlRecargo = 0;
            foreach (DataGridViewRow row in tablapagos.Rows)
            {
                grlRecargo += HelperService.ConvertToDecimalSeguro(tablapagos[7, row.Index].Value) - HelperService.ConvertToDecimalSeguro(tablapagos[6, row.Index].Value, 2);

            }

            txtRecargos.Text = grlRecargo.ToString();


        }




        public virtual bool validarDetalle(string txtinterno, int cmbArticuloIndex, int cmbColoresIndex, string txtPongotalle, string txtprecio, string cantidad = "1")
        {


            if (string.IsNullOrEmpty(cantidad) || HelperService.ConvertToDecimalSeguro(cantidad) < 0)
            {
                MessageBox.Show("Debe de ingresar una Cantidad mayor a cero", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (txtprecio == "" || txtprecio == "." || txtprecio == ",")
            {
                MessageBox.Show("Debe de ingresar un precio", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (HelperService.ConvertToDecimalSeguro(txtprecio) <= 0)
            {
                MessageBox.Show("Debe de ingresar un precio mayor a cero", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (HelperService.validarCodigo(txtinterno))
            {
                return true;
            }

            if (cmbArticuloIndex == -1)
            {
                MessageBox.Show("Debe de seleccionar un articulo", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (cmbColoresIndex == -1)
            {
                MessageBox.Show("Debe de seleccionar un color", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (!HelperService.haymts && txtPongotalle == "")
            {
                MessageBox.Show("Debe de seleccionar un talle", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }


            return true;
        }


        public virtual void actualizarVentas()
        {

            foreach (Form hijo in this.MdiParent.MdiChildren)
            {
                if (hijo.GetType() == typeof(Ventas))
                {

                    ((Ventas)hijo).refresh2();
                }

            }
        }

        public virtual VentaData cargarVenta()
        {
            VentaData nuevaVenta = new VentaData();
            nuevaVenta.Date = _pickerfecha.Value;
            nuevaVenta.IVA = HelperService.ConvertToDecimalSeguro(_txtiva);


            List<PagoData> formas = new List<PagoData>();
            var ventaService = new VentaService(new VentaRepository(), new VentaDetalleRepository());
            PagoData nforma;
            foreach (DataGridViewRow f in _tablaPagos.Rows)
            {
                nforma = new PagoData();
                FormaPagoData ff = new FormaPagoData();
                ff.ID = new Guid(_tablaPagos[0, f.Index].Value.ToString());
                nforma.FormaPago = ff;
                nforma.Recargo = HelperService.ConvertToDecimalSeguro(_tablaPagos[3, f.Index].Value.ToString());
                nforma.Importe = HelperService.ConvertToDecimalSeguro(_tablaPagos[6, f.Index].Value.ToString());
                nforma.Lote = _tablaPagos[4, f.Index].Value.ToString();
                nforma.Cupon = _tablaPagos[5, f.Index].Value.ToString();
                nforma.Cuotas = Convert.ToInt32(_tablaPagos[2, f.Index].Value.ToString());
                formas.Add(nforma);
            }

            TributoNexoData tNexo;
            TributoData tributo;
            nuevaVenta.Tributos = cargoTributos();

            nuevaVenta.Pagos = formas;

            if (_txtDescuento.Text == "" || _txtDescuento.Text == "." || _txtDescuento.Text == ",")
                _txtDescuento.Text = "0";
            nuevaVenta.Descuento = HelperService.ConvertToDecimalSeguro(_txtDescuento.Text);
            nuevaVenta.Local.ID = HelperService.IDLocal;
            nuevaVenta.Vendedor = (PersonalData)_cmbVendedor.SelectedItem;
            nuevaVenta.Numero = Convert.ToInt32(ventaService.GetNextNumberAvailable(HelperService.IDLocal, HelperService.Prefix, false));
            nuevaVenta.Prefix = HelperService.Prefix;
            nuevaVenta.Cliente = (ClienteData)_cmbClientes.SelectedItem;

            nuevaVenta.Monto = HelperService.ConvertToDecimalSeguro(_txtTotal.Text);

            switch (_cmbClase.Text.ToLower())
            {
                case "a":
                    nuevaVenta.ClaseDocumento = ClaseDocumento.A;
                    break;

                case "b":
                    nuevaVenta.ClaseDocumento = ClaseDocumento.B;
                    break;

                case "c":
                    nuevaVenta.ClaseDocumento = ClaseDocumento.C;
                    break;
            }
            nuevaVenta.Cambio = esCambio(_tabla);
            nuevaVenta.ID = Guid.NewGuid();
            nuevaVenta.Description = _txtObs.Text;
            nuevaVenta.Enable = true;
            int cantFilas = _tabla.Rows.Count - 1;
            int fila;
            VentaDetalleData detalle;

            List<VentaDetalleData> ds = new List<VentaDetalleData>();

            foreach (DataGridViewRow row in _tabla.Rows)
            {
                detalle = new VentaDetalleData();

                detalle.FatherID = nuevaVenta.ID;
                detalle.Alicuota = HelperService.ConvertToDecimalSeguro(_tabla[10, row.Index].Value);
                detalle.PrecioUnidad = HelperService.ConvertToDecimalSeguro(_tabla[4, row.Index].Value);


                detalle.Cantidad = HelperService.ConvertToDecimalSeguro(_tabla[7, row.Index].Value.ToString());
                detalle.Bonificacion = HelperService.ConvertToDecimalSeguro(_tabla[6, row.Index].Value.ToString());
                detalle.Codigo = _tabla[0, row.Index].Value.ToString();

                ds.Add(detalle);
            }

            nuevaVenta.Children = ds;


            return nuevaVenta;


        }
        public virtual bool esCambio(DataGridView tabla)
        {
            foreach (DataGridViewRow row in tabla.Rows)
            {
                if (Convert.ToBoolean(tabla[12, row.Index].Value) == true)
                {
                    return true;
                }
            }
            return false;

        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ventaBase
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "ventaBase";
            this.Load += new System.EventHandler(this.ventaBase_Load);
            this.ResumeLayout(false);

        }





        internal virtual void agregarArticulo(StockData s, int p1, int p2, string p3, string p4)
        {
            throw new NotImplementedException();
        }

        protected void tablaContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 12)
            {
                _tabla[7, e.RowIndex].Value = (HelperService.ConvertToDecimalSeguro(_tabla[7, e.RowIndex].Value) * -1).ToString();
            }
            if (e.RowIndex > 0)
                ActualizarRow(e.RowIndex);
        }

        public void ActualizarRow(int RowIndex)
        {
            calcularCantidad(RowIndex, _tabla);
            CalcularTotales();
        }




        protected void tablaCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                if (!IsDigitsOnly(_tabla[7, e.RowIndex].Value.ToString()))
                {
                    _tabla[7, e.RowIndex].Value = "1";
                }
                if (!IsDigitsOnly(_tabla[6, e.RowIndex].Value.ToString()))
                {
                    _tabla[6, e.RowIndex].Value = "0";
                }
                _tabla[12, e.RowIndex].Value = !(HelperService.ConvertToDecimalSeguro(_tabla[7, e.RowIndex].Value) > 0);

                ActualizarRow(e.RowIndex);
            }
        }

        protected void tablakeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        protected void tablaRowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CalcularTotales();
            if (_tabla.Rows.Count == 0)
            {
                _cmbListaPrecio.Enabled = true;
            }
        }
    }
}
