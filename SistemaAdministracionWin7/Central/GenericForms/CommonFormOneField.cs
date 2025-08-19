using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DTO.BusinessEntities;
using Services;

namespace Central.GenericForms
{
    public partial class CommonFormOneField<T> : Form where T : GenericObject, new()
    {
        private IGenericService<T> repo;
        private Guid? idItem;
        private bool isInsert;
        private string _myname;
        public CommonFormOneField(IGenericService<T> _repo, string frmName)
        {
            repo = _repo;

            _myname = frmName;
            InitializeComponent();
        }

        private void CommonFormOneField_Load(object sender, EventArgs e)
        {

            SetUI();
            setToInsert();
            loadTable("");
            this.Text = _myname;

        }

        private void setToInsert(bool isInsert=true,Guid? id=null,string name="")
        {
            if (!isInsert)
            {
                button1.Text = "Editar";
                idItem = id;
                txtNombre.Text = name;
            }
            else
            {
                button1.Text = "Agregar";
                idItem = new Guid();
                txtNombre.Text = "";
            }

        }

        private void SetUI()
        {
            Type typeT = typeof (T);


            if (typeT==typeof(BancoData))
            {
                this.Text = "Bancos";
            }
        }

        private void loadTable(string search)
        {
            ClearTable();
            
            List<T> items = repo.GetAll(false);

            

            if (!string.IsNullOrEmpty(search))
            {
                items = items.FindAll(x => x.Description.ToLower().Contains(search.ToLower()));
            }

            foreach (T t in items)
            {
                tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;
                //id nombre Codigo enable
                tabla[0, fila].Value = t.ID;
                tabla[1, fila].Value = t.Description;
                tabla[2, fila].Value = t.Enable ? "Habilitado" : "DesHabilitado";
                if (!t.Enable)
                    tabla.Rows[fila].DefaultCellStyle.BackColor = Color.Pink;
            }
        }

        private void ClearTable()
        {
            tabla.Rows.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidationText())
            {
                if (HelperService.MessageBoxHelper.confirmOperation() == DialogResult.OK)
                {
                    T theObject = LoadObject();
                    bool task = false;

                    if (EmptyItem(idItem))
                    {//insert
                       task= repo.Insert(theObject);

                        if (task){
                            HelperService.MessageBoxHelper.InsertOk("");

                        }
                        else
                            HelperService.MessageBoxHelper.InsertError("");
                    }
                    else
                    {//update
                        task = repo.Update(theObject);
                        if (task)
                            HelperService.MessageBoxHelper.UpdateOk("");
                        else
                            HelperService.MessageBoxHelper.UpdateError("");
                    }

                    if(task)
                        setToInsert();

                    loadTable("");
                }

                
            }
        }

        private T LoadObject()
        {

            var item = new T();
            item.Description = txtNombre.Text;
            item.ID = (Guid) (EmptyItem(idItem)? Guid.NewGuid():idItem);
            item.Enable = true;



            return item;
        }

        private bool EmptyItem(Guid? idItem)
        {
            return idItem == null || idItem == Guid.Empty;

        }

        private bool ValidationText()
        {

            if (txtNombre.Text == "")
            {
                if (EmptyItem(idItem))
                {
                    HelperService.MessageBoxHelper.InsertError(", el campo no puede ser vacio");    
                }
                else
                {
                    HelperService.MessageBoxHelper.UpdateError(", el campo no puede ser vacio");    
                }
                
                return false;
            }

            return true;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            
                loadTable(txtNombre.Text);
            
        }

        private void tabla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((tabla.SelectedCells.Count > 0))
            {
                setToInsert(false, new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString()), tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[1].Value.ToString());    
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            setToInsert();
            loadTable("");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((tabla.SelectedCells.Count > 0))
            {
                bool task = false;
                if (HelperService.MessageBoxHelper.confirmOperation() == DialogResult.OK)
                {
                    var item =
                        repo.GetByID(new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString()));


                    if (item.Enable)
                    {
                        task = repo.Disable(item);

                        if (task)
                            HelperService.MessageBoxHelper.DisableOk("");
                        else
                        {
                            HelperService.MessageBoxHelper.DisableError("");

                        }
                    }
                    else
                    {
                        task = repo.Enable(item);

                        if (task)
                            HelperService.MessageBoxHelper.EnableeOk("");
                        else
                        {
                            HelperService.MessageBoxHelper.EnableError("");

                        }
                    }

                    loadTable("");
                }
            }
        }
    }
}
