using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.ClienteRepository;
using Repository.Repositories.PedidoRepository;
using Repository.Repositories.StockRepository;
using Repository.Repositories.VentaRepository;
using Services;
using Services.ClienteService;
using Services.PedidoService;
using Services.StockService;
using Services.VentaService;
using SharedForms.Ventas;

namespace SharedForms.Stock
{
    public partial class estadoPedido : Form
    {
        public estadoPedido()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            limpiarTablas();
            loadPedido();
        }

        private void limpiarTablas()
        {
            tabla.Rows.Clear();
            tabla.ClearSelection();

            tabla2.Rows.Clear();
            tabla2.ClearSelection();
        }

        private void loadPedido()
        {
            var stockService = new StockService(new StockRepository());
            var pedidoService = new PedidoService(new PedidoRepository(), new PedidoDetalleRepository());
            List<pedidoData> pedidos = pedidoService.GetAll();
            var ventaService = new VentaService(new VentaRepository(), new VentaDetalleRepository());
            pedidos = pedidos.FindAll(delegate(pedidoData x)
            {
               
                return ((x.Cliente.ID == ((ClienteData)cmbClientes.SelectedItem).ID) && (x.Date >= dateDesde.Value) && (x.Date <= dateHasta.Value));
            });

            List<ventaData> ventas = ventaService.getbyCliente(((ClienteData)cmbClientes.SelectedItem).ID);


           ventas = ventas.FindAll(delegate(ventaData x)
           {
               return ((x.Cliente.ID == ((ClienteData)cmbClientes.SelectedItem).ID) && (x.Date >= dateDesde.Value) && (x.Date <= dateHasta.Value));
            
           });

           List<auxilarVentaPedido> auxList = new List<auxilarVentaPedido>();

           //esto se q es feo
        

            //la tupla prmero pedido dps venta
           Dictionary<string, Tuple<decimal, decimal>> detalle = new Dictionary<string, Tuple<decimal, decimal>>();
          

           foreach (ventaData v in ventas)
           {
               auxilarVentaPedido aux = new auxilarVentaPedido();
               aux.ID = v.ID;
               aux.Date = v.Date;
               aux.prefix = v.prefix;
               aux.Numero = v.Numero;
               aux.Enable = v.Enable;
               aux.esventa = true;
               auxList.Add(aux);
               if (!v.Enable)
               {



                   foreach (ventaDetalleData detall in v.Children)
                   {
                       if (!detalle.ContainsKey(detall.codigo))
                       {
                           Tuple<decimal, decimal> aux2 = new Tuple<decimal, decimal>(0, 0); ;
                           aux2.Item2 = detall.cantidad;
                           detalle.Add(detall.codigo, aux2);
                       }
                       else
                       {
                           detalle[detall.codigo].Item2 += detall.cantidad;
                       }

                   }
               }
               
               
           }
           foreach (pedidoData v in pedidos)
           {
               auxilarVentaPedido aux = new auxilarVentaPedido();
               aux.ID = v.ID;
               aux.Date = v.Date;
               aux.prefix = v.prefix;
               aux.Numero = v.Numero;
               aux.Enable = v.Enable;
               aux.esventa = false;
               auxList.Add(aux);

               if (!v.Enable)
               {


                   foreach (pedidoDetalleData detall in v.Children)
                   {

                       if (!detalle.ContainsKey(detall.codigo))
                       {
                           tuple<decimal, decimal> aux2 = new tuple<decimal, decimal>(0, 0);
                           aux2.Item1 = detall.cantidad;
                           detalle.Add(detall.codigo, aux2);
                       }
                       else
                       {
                           detalle[detall.codigo].Item1 += detall.cantidad;
                       }
                   }
               }

           }


           auxList.Sort(delegate(auxilarVentaPedido x, auxilarVentaPedido y)
            {
                return DateTime.Compare(x.Date,y.Date);
                
            });



           foreach (auxilarVentaPedido item in auxList)
           {
               tabla.Rows.Add();
               int fila;
               fila = tabla.RowCount - 1;
               tabla[0, fila].Value = item.ID;
               tabla[1, fila].Value = item.esventa?"Venta":"Pedido";
               tabla[2, fila].Value = item.NumeroCompleto;
               tabla[3, fila].Value =  HelperService.convertToFechaHoraConFormato(item.Date);
               tabla[4, fila].Value = item.Enable?"Anulada":"No Anulada";
           }



           foreach (string key in detalle.Keys)
           {

               tabla2.Rows.Add();
               int fila;
               fila = tabla2.RowCount - 1;

               StockData s = stockService.obtenerProducto(key);
               
               
               tabla2[1, fila].Value = key;
               tabla2[2, fila].Value = s.producto.proveedor.RazonSocial;
               tabla2[3, fila].Value = s.producto.show;
               tabla2[4, fila].Value = s.color.Description;
               tabla2[5, fila].Value = detalle[key].Item1.ToString();
               tabla2[6, fila].Value = detalle[key].Item2.ToString();
               tabla2[7, fila].Value = s.stock;
               if (Convert.ToInt32(tabla2[6, fila].Value) == Convert.ToInt32(tabla2[5, fila].Value))
               {
                   tabla2[8, fila].Value = "Completo";
               }else
	{
        if (Convert.ToInt32(tabla2[6, fila].Value) > Convert.ToInt32(tabla2[5, fila].Value))
        {
            tabla2[8, fila].Value = "Completo (excedente)";
        }
        else
        {
            tabla2[8, fila].Value = "Incompleto";
        }
	} 


           }
          

        }

        private void estadoPedido_Load(object sender, EventArgs e)
        {
            cargarClientes();
            dateDesde.Value = dateDesde.Value.AddMonths(-1);
        }

        private void cargarClientes()
        {
            var ClienteService = new ClienteService(new ClienteRepository());
            cmbClientes.DisplayMember = "razonSocial";
            cmbClientes.DataSource = ClienteService.GetAll(true); ;
            

        }

        private void tabla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            showdaobject();
        }

        private void showdaobject()
        {
            var pedidoService = new PedidoService(new PedidoRepository(), new PedidoDetalleRepository());
            if (tabla.SelectedCells.Count > 0)
            {
                var ventaService = new VentaService(new VentaRepository(), new VentaDetalleRepository());
                if (tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[1].Value.ToString() == "Venta")
                {
                    ventaData v = ventaService.GetByID(new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString()));

                    mostrarVenta(v);
                }
                else
                {//pedido
                    pedidoData v = pedidoService.GetByID(new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString()));

                    mostrarPedido(v);
                }

            }
        }

        private void mostrarVenta(ventaData v)
        {
            padreBase.AbrirForm(new mostrarVentaMayor(v), this.MdiParent, true);
            
        }

        private void mostrarPedido(pedidoData v)
        {
            padreBase.AbrirForm(new mostrarpedido(v), this.MdiParent, true);
            
        }
    }
}
