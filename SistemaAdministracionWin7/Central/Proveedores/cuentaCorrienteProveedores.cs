using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.Repositories.ComprasProveedoresRepository;
using Repository.Repositories.NotasRepository;
using Repository.Repositories.OrdenPagoRepository;
using Repository.Repositories.ProveedorRepository;
using Services;
using Services.ComprasProveedorService;
using Services.NotaService;
using Services.NotaService.Cliente.Credito;
using Services.OrdenPagoService;
using Services.ProveedorService;

namespace Central.Proveedores
{
    public partial class cuentaCorrienteProveedores : Form
    {
        public cuentaCorrienteProveedores()
        {
            InitializeComponent();
        }

        private void cuentaCorrienteClientes_Load(object sender, EventArgs e)
        {
            cargarProveedores();
            
            
        }

        private void cargarProveedores()
        {

            cmbProveedores.DataSource = new ProveedorService(new ProveedorRepository()).GetAll(true);
            cmbProveedores.DisplayMember = "razonSocial";

        }
        public void refresh2()
        {
            limpiarTabla();
            cargarTabla(((proveedorData)cmbProveedores.SelectedItem).ID,true,true);
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

        private void cargarTabla(Guid idProveedor,bool ocultarDeuda,bool ocultarAnuladas)
        {
            var notaDebitoProveedoresService = new NotaService(new NotaDebitoProveedoresRepository(), new NotaDebitoProveedoresDetalleRepository());
            var notaCreditoProveedoresService = new NotaService(new NotaCreditoProveedoresRepository(), new NotaCreditoProveedoresDetalleRepository());
            var comprasProveedoresService = new ComprasProveedorService(new ComprasProveedoresRepository(),
                new CompraProveedoresDetalleRepository());
            var OrdenPagoService = new OrdenPagoService(new OrdenPagoRepository(), new OrdenPagoDetalleRepository());


            List<ComprasProveedoresData> compras = comprasProveedoresService.GetbyProveedor(idProveedor);
            List<NotaData> notasCredito = notaCreditoProveedoresService.GetByTercero(idProveedor, false);
            List<NotaData> notasDebito = notaDebitoProveedoresService.GetByTercero(idProveedor, false);
            List<OrdenPagoData> opagos = OrdenPagoService.GetbyProveedor(idProveedor);


           


            foreach (var item in compras)
            {
                item.TipoDocumento = TipoDocumento.Compra;
                AddRow(ocultarAnuladas, item.TipoDocumento, item.ID, item.Date, item.Enable, item.Monto, item.NumeroCompleto);
            }
            foreach (var item in notasCredito)
            {
                item.TipoDocumento = TipoDocumento.NotaCredito;
                AddRow(ocultarAnuladas, item.TipoDocumento, item.ID, item.Date, item.Enable, item.Monto, item.NumeroCompleto);
            }
            foreach (var item in notasDebito)
            {
                item.TipoDocumento = TipoDocumento.NotaDebito;
                AddRow(ocultarAnuladas, item.TipoDocumento, item.ID, item.Date, item.Enable, item.Monto, item.NumeroCompleto);
            }
            foreach (var item in opagos)
            {
                item.TipoDocumento = TipoDocumento.Pago;
                AddRow(ocultarAnuladas, item.TipoDocumento, item.ID, item.Date, item.Enable, item.Monto, item.NumeroCompleto);
            }



            todo.Sort((x, y) => DateTime.Compare(x.Date, y.Date));


            decimal aux = 0;
            int fila;
            foreach (DocumentoGeneralData<ChildData> item in todo)
            {//tipo id fecha desc debe haber subt anul
                if (!(ocultarAnuladas && !item.Enable))
                {
                    tabla.Rows.Add();

                    fila = tabla.RowCount - 1;
                    tabla[0, fila].Value = item.TipoDocumento;
                    tabla[1, fila].Value = item.ID;
                    tabla[2, fila].Value = HelperService.convertToFechaConFormato(item.Date);

                    tabla[7, fila].Value = !item.Enable ? "Anulada" : "No Anulada";
                    switch (item.TipoDocumento)
                    {
                        case TipoDocumento.NotaCredito:
                            tabla[3, fila].Value = "Nota Credito: " + item.NumeroCompleto;
                            tabla[5, fila].Value = item.Monto.ToString();

                            break;
                        case TipoDocumento.NotaDebito:
                            tabla[3, fila].Value = "Nota Debito: " + item.NumeroCompleto;
                            tabla[4, fila].Value = item.Monto.ToString();
                            break;
                        case TipoDocumento.Pago:
                            tabla[3, fila].Value = "O.Pago: " + item.NumeroCompleto;
                            tabla[5, fila].Value = item.Monto.ToString();
                            break;
                        case TipoDocumento.Compra:
                            if (HelperService.esCliente(GrupoCliente.Slipak))
                            {
                                tabla[3, fila].Value = item.Description;
                            }
                            else if (HelperService.esCliente(GrupoCliente.Balarino))
                            {
                                tabla[3, fila].Value = "Compra: " + item.NumeroCompleto + " Obs:" + item.Description;
                            }
                            else
                            {
                                tabla[3, fila].Value = "Compra: " + item.NumeroCompleto;
                            }

                            tabla[4, fila].Value = item.Monto.ToString();
                            break;
                        default:
                            break;
                    }

                    aux = calcularSubtotal();
                    tabla[6, fila].Value =  aux.ToString();
                    if (chckOcultar.Checked && aux == 0)
                    {
                        tabla.Rows.Clear();
                    }
                }


            }


            tabla.Sort(Column1, ListSortDirection.Ascending);
            decimal aux = 0;
            int fila;
            foreach (DataGridViewRow item in tabla.Rows)
            {//tipo id fecha desc debe haber subt anul

                aux = calcularSubtotal(item.Index);
                tabla[6, item.Index].Value = aux.ToString();
                int max = item.Index;
                if (ocultarDeudas && aux == 0)
                {
                    for (int i = 0; i <= max; i++)
                    {
                        tabla.Rows.RemoveAt(0);
                    }
                }
            }


            //las pongo en orden y voy agregandolas a la tabla y calculando el subtotal, teniendo en cuenta si el cheque ya esta marcado 
            //como cobrado, vencido o aguardando por ser cobrado ( verde,rojo,amarillo)
            
            





        }

        private decimal calcularSubtotal()
        {
            decimal subotal = 0;
            foreach (DataGridViewRow row in tabla.Rows)
            {
                if (tabla[7, row.Index].Value.ToString() == "No Anulada")
                {
                    if (tabla[3, row.Index].Value.ToString().ToLower().StartsWith("nota c") || tabla[3, row.Index].Value.ToString().ToLower().StartsWith("o"))
                    {
                        
                        subotal += HelperService.ConvertToDecimalSeguro(tabla[5, row.Index].Value.ToString());
                    }
                    else
                    {
                        subotal -= HelperService.ConvertToDecimalSeguro(tabla[4, row.Index].Value.ToString());
                    }
                }
            }
            return subotal;
        }

        private void tabla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var comprasProveedoresService = new ComprasProveedorService(new ComprasProveedoresRepository(),
                      new CompraProveedoresDetalleRepository());
            if (tabla.SelectedCells.Count==1)
            {
                Guid id = new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[1].Value.ToString());
                switch ((TipoDocumento)tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value)
                {
                    case TipoDocumento.NotaCredito:
                        padre.AbrirForm(new Central.Tesoreria.mostrarNota(id, true, true), this.MdiParent, true);
                        break;
                    case TipoDocumento.NotaDebito:
                        padre.AbrirForm(new Central.Tesoreria.mostrarNota(id, false, true), this.MdiParent, true);
                        break;
                    case TipoDocumento.Pago:
                        padre.AbrirForm(new Central.Proveedores.verPago(id),this.MdiParent,true);
                        break;
                    case TipoDocumento.Compra:
                        ComprasProveedoresData c = comprasProveedoresService.GetByID(id);
                        padre.AbrirForm(new mostrarComprasCompleto(c), this.MdiParent, true);
                        break;
                    default:
                        break;
                }
                
            }
            else
            {
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            limpiarTabla();
            cargarTabla(((proveedorData)cmbProveedores.SelectedItem).ID,chckOcultar.Checked,chkAuladas.Checked);
        }




    }
}
