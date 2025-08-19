using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;

using DTO.BusinessEntities;
using Repository.ColoresRepository;
using Repository.Repositories.ProductoRepository;
using Repository.Repositories.ProveedorRepository;
using Services.ColorService;
using Services.ProductoService;
using Services.ProveedorService;

namespace Central
{

    
   

    public partial class printLabel : Form
    {




        Stack<object> labels = new  Stack<object>();

        public printLabel()
        {
            InitializeComponent();
        }
        
       

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (dg == DialogResult.OK)
            {
                foreach (DataGridViewRow f in tabla.Rows)
                {

                    for (int i = 0; i < Convert.ToInt32(tabla[7, f.Index].Value.ToString()); i++)
                    {

                        factoryLabel(
                            tabla[3, f.Index].Value.ToString(),
                            tabla[5, f.Index].Value.ToString(),
                            tabla[1, f.Index].Value.ToString(),
                            tabla[0, f.Index].Value.ToString(),
                            tabla[4, f.Index].Value.ToString(),
                            tabla[6, f.Index].Value.ToString());

                    }

                }




                Printing();
            }
        }






        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {


            printyeah((List<labelString>)labels.Pop(),ev);
           
            
            
            ev.HasMorePages = labels.Count>0;
          
                
        }

        private void printyeah(List<labelString> p,PrintPageEventArgs ev)
        {
            foreach (labelString item in p)
            {
                ev.Graphics.DrawString(item.text, item.font, Brushes.Black, item.xmargin, item.ymargin, new StringFormat());
   
            }
            
        }

        // Print the file. 
        public void Printing()
        {
            try
            {
                    PaperSize paperSize = new PaperSize("My Envelope", 275, 98);
                    PrintDocument pd = new PrintDocument();

                    pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                    
                    // Print the document.
                    pd.PrinterSettings.PrinterName = "ZEBRA TLP 2742";
                    pd.DefaultPageSettings.PaperSize = paperSize;
                    pd.DefaultPageSettings.Margins = new Margins(60, 40, 20, 20);
                    pd.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void printLabel_Load(object sender, EventArgs e)
        {
            cagarProveedores();
            cargarColores();
        }
       
        private void cargarColores()
        {
            cmbColores.DisplayMember = "Description";
            var colorService = new ColorService(new ColorRepository());
            cmbColores.DataSource = colorService.GetAll(true);
            
        }

        private void cagarProveedores()
        {
            cmbProveedor.DisplayMember = "razonSocial";
            cmbProveedor.DataSource = new ProveedorService(new ProveedorRepository()).GetAll(false); ;
            
        }

        private void cargarArticulos(ProveedorData proveedorData)
        {
            var productoService = new ProductoService(new ProductoRepository());
            cmbArticulo.DisplayMember = "Show";
            cmbArticulo.DataSource = productoService.GetbyProveedor(proveedorData.ID);
            
        }

        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProveedor.SelectedIndex > -1)
            {
                cargarArticulos((ProveedorData)cmbProveedor.SelectedItem);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {


            tabla.Rows.Add();
            int fila;
            fila = tabla.RowCount - 1;


            tabla[0, fila].Value = ((ProductoData)cmbArticulo.SelectedItem).CodigoInterno + ((ColorData)cmbColores.SelectedItem).Codigo + Convert.ToInt32(txtTalle.Text).ToString("00");
            tabla[1, fila].Value = ((ProveedorData)cmbProveedor.SelectedItem).RazonSocial;
            tabla[2, fila].Value = ((ProductoData)cmbArticulo.SelectedItem).Show;
            tabla[3, fila].Value = ((ProductoData)cmbArticulo.SelectedItem).Description;
            tabla[4, fila].Value = ((ProductoData)cmbArticulo.SelectedItem).CodigoProveedor;
            tabla[5, fila].Value = ((ColorData)cmbColores.SelectedItem).Description;
            tabla[6, fila].Value = Convert.ToInt32(txtTalle.Text).ToString("00");
            tabla[7, fila].Value = txtCantidad.Text;



            
        }


        private void factoryLabel(string strdescripcion, string strcolor, string strproveedor, string strcodigoBarras, string strarticuloCodigo, string strtalle)
        {

            float x_offset = 5;
            float y_offset = 0;
            float defaultline = 13;

            Font descriptionFont = new Font("Arial", 10, FontStyle.Bold);
            Font colorFont = new Font("Arial", 12, FontStyle.Bold);
            Font proveedorFont = new Font("Arial", 10);
            Font barCodeFont = new Font("Free 3 of 9", 30);
            Font codeFont = new Font("Consolas", 10);
            Font articuleFont = new Font("Arial", 10);
            Font sizeFont = new Font("Arial", 25, FontStyle.Bold);


            labelString descripcion = new labelString(strdescripcion.ToUpper(), x_offset, y_offset, descriptionFont);
            labelString color = new labelString(strcolor.ToUpper(), x_offset, descripcion.ymargin + defaultline, colorFont);
            labelString proveedor = new labelString(strproveedor.ToUpper(), x_offset, color.ymargin + defaultline+4, proveedorFont);

            labelString codigoBarras = new labelString(strcodigoBarras, x_offset + 10, proveedor.ymargin + defaultline + 5, barCodeFont);
            labelString nroCodigoBarras = new labelString(strcodigoBarras, x_offset + 30, codigoBarras.ymargin + (defaultline * 2) + 5, codeFont);

            labelString codigoArt = new labelString(strarticuloCodigo, x_offset + 176, color.ymargin + defaultline, articuleFont);

            labelString talle = new labelString(strtalle, x_offset + 225, color.ymargin, sizeFont);

            List<labelString> label = new List<labelString>();

            label.Add(descripcion);
            label.Add(color);
            label.Add(proveedor);
            label.Add(codigoBarras);
            label.Add(nroCodigoBarras);
            label.Add(codigoArt);
            label.Add(talle);

            labels.Push(label);
        
        
        }

        private void tabla_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
           

            
        }

        private void calcularTotal()
        {
          
        }

        private void tabla_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            
        }
    }

    public class labelString
    {

        public labelString(string t, float x, float y, Font f)
        {
            _text = t;
            _xmargin = x;
            _ymargin = y;
            _f = f;

        }
        private string _text { get; set; }
        public string text
        {
            get { return _text; }
            set { _text = value; }

        }

        private float _ymargin { get; set; }
        public float ymargin
        {
            get { return _ymargin; }
            set { _ymargin = value; }

        }

        private float _xmargin { get; set; }
        public float xmargin
        {
            get { return _xmargin; }
            set { _xmargin = value; }

        }

        private Font _f { get; set; }
        public Font font
        {
            get { return _f; }
            set { _f = value; }

        }
    }
}
