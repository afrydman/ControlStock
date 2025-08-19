using System;
using SharedForms;

namespace Preparador
{
    public partial class padre : padreBase
    {
        public padre()
        {
            InitializeComponent();
        }

        private void crearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new Crear(), this);
        }

        private void padre_Load(object sender, EventArgs e)
        {

        }

        private void migrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new Migrar(), this);
        }
    }
}
