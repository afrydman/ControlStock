using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Central.Proveedores;
using DTO.BusinessEntities;
using Repository.ClienteRepository;
using Repository.Repositories.ComprasProveedoresRepository;
using Repository.Repositories.NotasRepository;
using Repository.Repositories.OrdenPagoRepository;
using Repository.Repositories.ProveedorRepository;
using Repository.Repositories.ReciboRepository;
using Repository.Repositories.VentaRepository;
using Services;
using Services.ClienteService;
using Services.ComprasProveedorService;
using Services.NotaService;
using Services.OrdenPagoService;
using Services.ProveedorService;
using Services.ReciboService;
using Services.VentaService;
using SharedForms.Ventas;

namespace Central.GenericForms
{
    public partial class cuentaCorriente<T> : Form where T:PersonaData
    {
        public cuentaCorriente()
        {
            InitializeComponent();
        }

        private void cuentaCorrienteClientes_Load(object sender, EventArgs e)
        {
            cargarTerceros();
        }

        private void cargarTerceros()
        {


            if (typeof (T) != typeof (ClienteData))
            {
                cmbTercero.DataSource = new ProveedorService(new ProveedorRepository()).GetAll(false);
                lblTercero.Text = "Proveedores";
            }
            else
            {
                cmbTercero.DataSource = new ClienteService(new ClienteRepository()).GetAll(false);
                lblTercero.Text = "Clientes";
            }


            cmbTercero.DisplayMember = "razonSocial";

        }
        public void refresh2()
        {
            limpiarTabla();
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void limpiarTabla()
        {
            tabla.Rows.Clear();
            tabla.DataSource = null;
            tabla.ClearSelection();
        }

        private void cargarTabla(Guid idTercero,bool ocultarDeudas,bool ocultarAnuladas)
        {

            if (typeof (T) != typeof (ClienteData))
            {

                var notaDebitoProveedoresService = new NotaService(new NotaDebitoProveedoresRepository(),new NotaDebitoProveedoresDetalleRepository(), new TributoNotaDebitoProveedoresRepository());
                var notaCreditoProveedoresService = new NotaService(new NotaCreditoProveedoresRepository(),new NotaCreditoProveedoresDetalleRepository(), new TributoNotaCreditoProveedoresRepository());
                var comprasProveedoresService = new ComprasProveedorService(new ComprasProveedoresRepository(),
                    new CompraProveedoresDetalleRepository());
                var OrdenPagoService = new OrdenPagoService(new OrdenPagoRepository(), new OrdenPagoDetalleRepository());


                List<ComprasProveedoresData> compras = comprasProveedoresService.GetbyProveedor(idTercero, ocultarAnuladas);
                List<NotaData> notasCreditoProveedor = notaCreditoProveedoresService.GetByTercero(idTercero,false, ocultarAnuladas);
                List<NotaData> notasDebitoProveedor = notaDebitoProveedoresService.GetByTercero(idTercero, false, ocultarAnuladas);
                List<OrdenPagoData> opagos = OrdenPagoService.GetbyProveedor(idTercero, ocultarAnuladas);



                foreach (var item in compras)
                {
                    
                    AddRow(ocultarAnuladas, TipoDocumento.Compra, item.ID, item.Date, item.Enable, item.Monto, item.NumeroCompleto,item.Description);
                }
                foreach (var item in notasCreditoProveedor)
                {
                    
                    AddRow(ocultarAnuladas, TipoDocumento.NotaCredito, item.ID, item.Date, item.Enable, item.Monto, item.NumeroCompleto, item.Description);
                }
                foreach (var item in notasDebitoProveedor)
                {
                    
                    AddRow(ocultarAnuladas, TipoDocumento.NotaDebito, item.ID, item.Date, item.Enable, item.Monto, item.NumeroCompleto, item.Description);
                }
                foreach (var item in opagos)
                {
                    
                    AddRow(ocultarAnuladas,TipoDocumento.Pago, item.ID, item.Date, item.Enable, item.Monto, item.NumeroCompleto, item.Description);
                }

            }
            else
            {
                var notaCreditoClienteService = new NotaService(new NotaCreditoClienteRepository(), new NotaCreditoClienteDetalleRepository(),new TributoNotaCreditoClientesRepository());
                var notaDebitoClienteService = new NotaService(new NotaDebitoClienteRepository(), new NotaDebitoClienteDetalleRepository(), new TributoNotaDebitoClientesRepository());
                var ventaService = new VentaService(new VentaRepository(), new VentaDetalleRepository());
                var reciboService = new ReciboService(new ReciboRepository(), new ReciboDetalleRepository());


                List<VentaData> ventas = ventaService.getCuentaCorrientebyCliente(idTercero, ocultarAnuladas);

                List<NotaData> notasCreditoCliente = notaCreditoClienteService.GetByTercero(idTercero, false, ocultarAnuladas);

                List<NotaData> notasDebitoCliente = notaDebitoClienteService.GetByTercero(idTercero, false, ocultarAnuladas);

                List<ReciboData> recibos = reciboService.GetbyCliente(idTercero, ocultarAnuladas);


                foreach (var item in ventas)
                {
                    
                    AddRow(ocultarAnuladas, TipoDocumento.Venta, item.ID, item.Date, item.Enable, item.Monto, item.NumeroCompleto, item.Description);


                }
                foreach (var item in notasCreditoCliente)
                {
                    
                    AddRow(ocultarAnuladas, TipoDocumento.NotaCredito, item.ID, item.Date, item.Enable, item.Monto, item.NumeroCompleto, item.Description);
                }
                foreach (var item in notasDebitoCliente)
                {

                    AddRow(ocultarAnuladas, TipoDocumento.NotaDebito, item.ID, item.Date, item.Enable, item.Monto, item.NumeroCompleto, item.Description);
                }
                foreach (var item in recibos)
                {

                    AddRow(ocultarAnuladas, TipoDocumento.Recibo, item.ID, item.Date, item.Enable, item.Monto, item.NumeroCompleto, item.Description);
                }

            }


            tabla.Sort(Column1,ListSortDirection.Ascending);
            decimal aux = 0;
            int fila;
            foreach (DataGridViewRow item in tabla.Rows)
            {//tipo id fecha desc debe haber subt anul

                if (item.Index>=0)
                {
                    
               
                aux = calcularSubtotal(item.Index);
                tabla[6, item.Index].Value = aux.ToString();
                int max = item.Index;
                if (ocultarDeudas && aux==0)
                {
                    for (int i = 0; i <= max; i++)
                    {
                        tabla.Rows.RemoveAt(0);    
                    }
                }
                }
            }

        }

        private void AddRow(bool ocultarAnuladas,TipoDocumento unTipo,Guid id,DateTime aDate,bool enable, decimal Monto, string NumeroCompleto,string Description)
        {
            
            if (!(ocultarAnuladas && !enable))
            {
                tabla.Rows.Add();
                int fila = tabla.RowCount - 1;
                tabla[0, fila].Value = unTipo;
                tabla[1, fila].Value = id;
                tabla[2, fila].Value = aDate;

                tabla[7, fila].Value = !enable ? "Anulada" : "No Anulada";
                if (!enable)
                    tabla.Rows[fila].DefaultCellStyle.BackColor = Color.Pink;
                switch (unTipo)
                {
                    case TipoDocumento.NotaCredito:
                        
                        if (HelperService.esCliente(GrupoCliente.Slipak))
                        {
                            tabla[3, fila].Value = "Nota Credito: " + NumeroCompleto + " - Obs: " + Description;
                            tabla[5, fila].Value = Monto.ToString();
                        }
                        else
                        {
                            tabla[3, fila].Value = "Nota Credito: " + NumeroCompleto;
                            tabla[5, fila].Value = Monto.ToString();    
                        }
                break;
                    case TipoDocumento.NotaDebito:

                        if (HelperService.esCliente(GrupoCliente.Slipak))
                        {
                        tabla[3, fila].Value = "Nota Debito: " + NumeroCompleto  + " - Obs: " + Description;
                        tabla[4, fila].Value = Monto.ToString();
                        }
                        else { 
                        tabla[3, fila].Value = "Nota Debito: " + NumeroCompleto;
                        tabla[4, fila].Value = Monto.ToString();
                        }
                        break;
                    case TipoDocumento.Recibo:
                        tabla[3, fila].Value = "Recibo: " + NumeroCompleto;
                        tabla[5, fila].Value = Monto.ToString();
                        break;
                    case TipoDocumento.Venta:
                       
                        if (HelperService.esCliente(GrupoCliente.Slipak))
                        {
                            tabla[3, fila].Value = "Venta: " + NumeroCompleto + " - Obs: " + Description;
                            tabla[4, fila].Value = Monto.ToString();
                        }
                        else
                        {
                            tabla[3, fila].Value = "Venta: " + NumeroCompleto;
                            tabla[4, fila].Value = Monto.ToString();
                        }
                        break;
                    case TipoDocumento.Pago:
                        tabla[3, fila].Value = "O.Pago: " + NumeroCompleto;
                        tabla[5, fila].Value = Monto.ToString();
                        break;
                    case TipoDocumento.Compra:
                        if (HelperService.esCliente(GrupoCliente.Slipak))
                        {
                            tabla[3, fila].Value = Description;
                        }
                        else if (HelperService.esCliente(GrupoCliente.Balarino))
                        {
                            tabla[3, fila].Value = "Compra: " + NumeroCompleto + " Obs:" + Description;
                        }
                        else
                        {
                            tabla[3, fila].Value = "Compra: " + NumeroCompleto;
                        }

                        tabla[4, fila].Value = Monto.ToString();
                        break;
                    default:
                        break;
                }
            }
        }


        private decimal calcularSubtotal(int indexMax)
        {
            decimal subotal = 0;
            for (int i = 0; i <= indexMax; i++)
            {
                if (tabla[7, i].Value.ToString() == "No Anulada")
                {
                    if (tabla[3, i].Value.ToString().ToLower().StartsWith("nota c") || tabla[3, i].Value.ToString().ToLower().StartsWith("r") || tabla[3, i].Value.ToString().ToLower().StartsWith("o"))
                    {

                        subotal += HelperService.ConvertToDecimalSeguro(tabla[5,i].Value.ToString());
                    }
                    else
                    {
                        subotal -= HelperService.ConvertToDecimalSeguro(tabla[4, i].Value.ToString());
                    }
                }
            }

            return subotal;
        }

        private void tabla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            limpiarTabla();
            cargarTabla(((PersonaData)cmbTercero.SelectedItem).ID,chckOcultar.Checked,chkAuladas.Checked);
        }

        private void tabla_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var ventaService = new VentaService(new VentaRepository(), new VentaDetalleRepository());

            var comprasProveedoresService = new ComprasProveedorService(new ComprasProveedoresRepository(),
                     new CompraProveedoresDetalleRepository());

            if (tabla.SelectedCells.Count == 1)
            {
                Guid id = new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[1].Value.ToString());
                switch ((TipoDocumento)tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value)
                {
                    case TipoDocumento.NotaCredito:
                        padre.AbrirForm(new Central.GenericForms.Notas.MostrarNota<T>(id, true, typeof(T) != typeof(ClienteData)), this.MdiParent, true);
                        break;
                    case TipoDocumento.NotaDebito:
                        padre.AbrirForm(new Central.GenericForms.Notas.MostrarNota<T>(id, false, typeof(T) != typeof(ClienteData)), this.MdiParent, true);
                        break;
                    case TipoDocumento.Recibo:
                        padre.AbrirForm(new Central.verRecibo(id), this.MdiParent, true);
                        break;
                    case TipoDocumento.Venta:
                        VentaData v = ventaService.GetByID(id);
                        padre.AbrirForm(new mostrarVentaMenor(v), this.MdiParent, true);
                        break;
                    case TipoDocumento.Pago:
                        padre.AbrirForm(new Central.Proveedores.verPago(id), this.MdiParent, true);
                        break;
                    case TipoDocumento.Compra:
                        ComprasProveedoresData c = comprasProveedoresService.GetByID(id);
                        padre.AbrirForm(new MostrarcomprasAProveedores(c), this.MdiParent, true);
                        break;
                    default:
                        break;
                }

            }
            else
            {

            }
        }

        private void tabla_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }




    }
}
