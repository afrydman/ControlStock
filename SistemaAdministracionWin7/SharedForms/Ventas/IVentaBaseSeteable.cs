using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharedForms.Ventas
{
    public interface IVentaBaseSeteable
    {
         void SetControls(TextBox txtNeto, TextBox txtSubtotal, TextBox txtUnidadTotal, TextBox txtIva,
            TextBox txtTotal, TextBox txtDescuento, TextBox txtRecargo, TextBox txtObs,

            TextBox txttributoAlicuota, TextBox txttributoImporte, TextBox txttributoBase,
            RadioButton radioTributoAlicuota, ComboBox cmbTributo,

            DataGridView tabla, DataGridView tablaTributos, DataGridView tablaPagos,
            ComboBox cmbClase, ComboBox cmbClientes, ComboBox cmbVendedor, ComboBox cmbListaPrecio,
            ComboBox cmbProveedores,
            DateTimePicker fechaFactura, DateTimePicker fechaContable);
    }
}
