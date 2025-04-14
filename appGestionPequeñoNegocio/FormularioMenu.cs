using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using pryGestionInventario;

namespace appGestionPequeñoNegocio
{
    public partial class FormularioMenu : Form
    {
        public FormularioMenu()
        {
            InitializeComponent();
        }

        String codigo;
        String nombre;
        String descripcion;
        String categoria;
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
            dgvDatos.Columns.Add("Categoria", "Categoría");
            dgvDatos.Columns.Add("Stock", "Stock");

            clsConexionBD objConectarBD = new clsConexionBD();
            objConectarBD.ConectarBD();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            try
            {
                codigo = txtCodigo.Text;
                nombre = txtNombre.Text;
                descripcion = txtDescripcion.Text;
                precio = decimal.Parse(txtPrecio.Text);
                categoria = txtCategoria.Text; 
                stock = int.Parse(txtStock.Text); 

                dgvDatos.Rows.Add(codigo, nombre, descripcion, precio, categoria, stock);

                txtNombre.Text = "";
                txtCodigo.Text = "";
                txtDescripcion.Text = "";
                txtPrecio.Text = "";
                txtCategoria.Text = "";
                txtStock.Text = "";
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Error: Ingresa datos válidos en los campos numéricos. "+ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminar()
        {
            try
            {
                String codigoEliminar = txtCodigoEliminar.Text;

                foreach (DataGridViewRow row in dgvDatos.Rows)
                {
                    if (row.Cells["Codigo"].Value != null && row.Cells["Codigo"].Value.ToString() == codigoEliminar)
                    {
                        dgvDatos.Rows.Remove(row);

                        MessageBox.Show("Producto con código " + codigoEliminar + " eliminado.", "Eliminación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCodigoEliminar.Text = "";
                        return;
                    }
                }

                MessageBox.Show("No se encontró ningún producto con el código " + codigoEliminar + ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al eliminar el producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void eliminar(String codigoEliminar)
        {
            try
            {
                foreach (DataGridViewRow row in dgvDatos.Rows)
                {
                    if (row.Cells["Codigo"].Value != null && row.Cells["Codigo"].Value.ToString() == codigoEliminar)
                    {
                        dgvDatos.Rows.Remove(row);

                        MessageBox.Show("Producto con código " + codigoEliminar + " eliminado.", "Eliminación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
                        txtCodigoEliminar.Text = "";

                        return;
                    }
                }

                MessageBox.Show("No se encontró ningún producto con el código " + codigoEliminar + ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al eliminar el producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lblApellido_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodigoEliminar.Text)) { 
                DialogResult resultado1 = MessageBox.Show("¿Estás seguro de que deseas eliminar este elemento?",
                                                        "Confirmar eliminación",
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Warning);

                if (resultado1 == DialogResult.Yes)
                {
                    DialogResult resulto2 = MessageBox.Show("¿Estás seguro de que deseas eliminarlo definitivamente?",
                                                            "Confirmar eliminación final",
                                                            MessageBoxButtons.YesNo,
                                                            MessageBoxIcon.Warning);
                    if (resulto2 == DialogResult.Yes)
                    {
                        eliminar();
                    }
                    else
                    {
                        MessageBox.Show("Eliminación cancelada.", "Operación cancelada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Eliminación cancelada.", "Operación cancelada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un código de elemento para eliminar.");
            }
        }
        private void modificar()
        {
            if(!string.IsNullOrEmpty(txtModificar.Text))
            {
                try
                {
                    foreach (DataGridViewRow fila in dgvDatos.Rows)
                    {
                        if (fila.IsNewRow) continue;

                        if (fila.Cells[0].Value != null && fila.Cells[0].Value.ToString() == txtModificar.Text)
                        {
                            txtCodigo.Text = txtModificar.Text;
                            txtNombre.Text = fila.Cells[1].Value?.ToString();
                            txtDescripcion.Text = fila.Cells[2].Value?.ToString();
                            txtPrecio.Text = fila.Cells[3].Value?.ToString();
                            txtCategoria.Text = fila.Cells[4].Value?.ToString();
                            txtStock.Text = fila.Cells[5].Value?.ToString();
                            break;
                        }
                    }
                    eliminar(txtModificar.Text);
                    MessageBox.Show("Proceda a modificar los datos del producto y luego haga clic en cargar. De lo contrario, se perderan los datos.", "Información para modificar");
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Error: Ingresa un codigo válido. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor ingrese un código de producto existente.");
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            modificar();
        }
    }
}
