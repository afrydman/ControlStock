using DTO.BusinessEntities;
using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SharedForms.TransferFiles
{
    public partial class Transfers: Form
    {
        public Transfers()
        {
            InitializeComponent();
        }

        private void Transfers_Load(object sender, EventArgs e)
        {


            CargarFromDB();

           
        }
    private void CargarFromDB()
        {
            var service = new TransferService();

           
            var data = service.GetAll();

            foreach (FileTransferData item in data)
            {

                tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;

                //Codigo
                tabla[0, fila].Value = item.ID;

                tabla[1, fila].Value = item.Description;
                tabla[2, fila].Value = item.Date.ToShortDateString();
                tabla[3, fila].Value = item.Completed ? "Completado" : "Pendiente";
                tabla[4, fila].Value = item.LocalFileName;
                tabla[5, fila].Value = item.FromLocalID;//Pone el local rata
                tabla[6, fila].Value = item.ToLocalID;//Pone el local rata


            }




            tabla.ClearSelection();
        }
    
    }
}
