using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace appGestionPequeñoNegocio
{
    public partial class FormularioMenu : Form
    {
        public FormularioMenu()
        {
            InitializeComponent();
        }

        string codigo;
        string nombre;
        string descripcion;
        decimal precio;
        int stock;

        private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FormularioMenu_Load(object sender, EventArgs e)
        {
            dgvDatos.Columns.Add("Codigo", "Código");
            dgvDatos.Columns.Add("Nombre", "Nombre");
            dgvDatos.Columns.Add("Descripcion", "Descripción");
            dgvDatos.Columns.Add("Precio", "Precio");
            dgvDatos.Columns.Add("Stock", "Stock");
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            try
            {
                // Captura los datos de los TextBox y los guarda en las variables
                codigo = txtNombre.Text;
                nombre = txtNombre.Text;
                descripcion = txtDescripcion.Text;
                precio = decimal.Parse(txtPrecio.Text); // Convierte el texto a decimal
                stock = int.Parse(txtStock.Text); // Convierte el texto a entero

                // Opcional: Puedes agregar aquí validaciones adicionales
                // Por ejemplo, verificar que los TextBox no estén vacíos, etc.

                // Agrega una fila al DataGridView con los datos capturados
                dgvDatos.Rows.Add(codigo, nombre, descripcion, precio, stock);

                // Opcional: Limpia los TextBox después de agregar la fila
                txtNombre.Text = "";
                txtNombre.Text = "";
                txtDescripcion.Text = "";
                txtPrecio.Text = "";
                txtStock.Text = "";
            }
            catch (FormatException ex)
            {
                // Maneja el error si el usuario ingresa datos no válidos (por ejemplo, texto en un campo numérico)
                MessageBox.Show("Error: Ingresa datos válidos en los campos numéricos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Opcional: Puedes registrar el error en un archivo de registro, etc.
            }
            catch (Exception ex)
            {
                // Maneja cualquier otro error inesperado
                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Opcional: Puedes registrar el error en un archivo de registro, etc.
            }
        }

        private void eliminar()
        {
            try
            {
                // Obtiene el código a eliminar del TextBox
                string codigoEliminar = txtCodigoEliminar.Text;

                // Itera a través de las filas del DataGridView
                foreach (DataGridViewRow row in dgvDatos.Rows)
                {
                    // Verifica si el código de la fila coincide con el código a eliminar
                    if (row.Cells["Codigo"].Value != null && row.Cells["Codigo"].Value.ToString() == codigoEliminar)
                    {
                        // Elimina la fila
                        dgvDatos.Rows.Remove(row);

                        // Opcional: Muestra un mensaje de confirmación
                        MessageBox.Show("Producto con código " + codigoEliminar + " eliminado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Limpia el TextBox de código a eliminar.
                        txtCodigoEliminar.Text = "";

                        // Sale del bucle, ya que se encontró y eliminó la fila
                        return;
                    }
                }

                // Si no se encontró el código, muestra un mensaje de error
                MessageBox.Show("No se encontró ningún producto con el código " + codigoEliminar + ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Maneja cualquier error inesperado
                MessageBox.Show("Ocurrió un error al eliminar el producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblApellido_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            eliminar();
        }
    }
}
