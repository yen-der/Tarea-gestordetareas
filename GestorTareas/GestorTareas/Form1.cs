using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestorTareas
{
    public partial class Form1 : Form

    {
        List<Tarea> listaTareas = new List<Tarea>();

        public Form1()
        {
            GroupBox gbDatosTarea = new GroupBox();
            gbDatosTarea.Text = "Datos de la Tarea";
            Tarea nuevaTarea = new Tarea();
            nuevaTarea.Nombre = "Revisar código";
            nuevaTarea.Estado = "En Proceso";
            InitializeComponent();
        }
        private void LimpiarCampos()
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            txtDescripcion.Clear();
            txtLugar.Clear();
            cmbEstado.SelectedIndex = -1;
            dtpFecha.Value = DateTime.Now;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void lblDescripcion_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void ActualizarGrid()
        {
            dgvTareas.DataSource = null;
            dgvTareas.DataSource = listaTareas;
        }
        private void dgvTareas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtCodigo.Text = dgvTareas.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtNombre.Text = dgvTareas.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtDescripcion.Text = dgvTareas.Rows[e.RowIndex].Cells[2].Value.ToString();
                dtpFecha.Value = (DateTime)dgvTareas.Rows[e.RowIndex].Cells[3].Value;
                txtLugar.Text = dgvTareas.Rows[e.RowIndex].Cells[4].Value.ToString();
                cmbEstado.SelectedItem = dgvTareas.Rows[e.RowIndex].Cells[5].Value.ToString();

            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Validación de campos obligatorios
            if (string.IsNullOrWhiteSpace(txtCodigo.Text) ||
                string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtDescripcion.Text) ||
                string.IsNullOrWhiteSpace(txtLugar.Text) ||
                cmbEstado.SelectedItem == null)
            {
                MessageBox.Show("Por favor completa todos los campos antes de agregar la tarea.");
                return;
            }

            // Verificación de código duplicado
            bool codigoExiste = listaTareas.Any(t => t.Codigo == txtCodigo.Text);
            if (codigoExiste)
            {
                MessageBox.Show("Ya existe una tarea con ese código. Usa uno diferente.");
                return;
            }

            // Si todo está bien, se crea y agrega la tarea
            Tarea nueva = new Tarea()
            {
                Codigo = txtCodigo.Text,
                Nombre = txtNombre.Text,
                Descripcion = txtDescripcion.Text,
                Fecha = dtpFecha.Value,
                Lugar = txtLugar.Text,
                Estado = cmbEstado.SelectedItem.ToString()
            };

            listaTareas.Add(nueva);
            ActualizarGrid();
            MessageBox.Show("Tarea agregada correctamente.");

            // Limpieza opcional de campos
            LimpiarCampos ();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvTareas.SelectedRows.Count > 0)
            {
                int index = dgvTareas.SelectedRows[0].Index;
                listaTareas[index].Codigo = txtCodigo.Text;
                listaTareas[index].Nombre = txtNombre.Text;
                listaTareas[index].Descripcion = txtDescripcion.Text;
                listaTareas[index].Fecha = dtpFecha.Value;
                listaTareas[index].Lugar = txtLugar.Text;
                listaTareas[index].Estado = cmbEstado.SelectedItem.ToString();

                ActualizarGrid();
                MessageBox.Show("Tarea editada correctamente.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvTareas.SelectedRows.Count > 0)
            {
                int index = dgvTareas.SelectedRows[0].Index;
                listaTareas.RemoveAt(index);
                ActualizarGrid();
                MessageBox.Show("Tarea eliminada correctamente.");
                LimpiarCampos();
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBuscarCodigo_Click(object sender, EventArgs e)
        {
            string codigoBuscado = txtBuscarCodigo.Text.Trim();

            var resultado = listaTareas.FirstOrDefault(t => t.Codigo == codigoBuscado);

            if (resultado != null)
            {
                dgvTareas.DataSource = null;
                dgvTareas.DataSource = new List<Tarea> { resultado };
            }
            else
            {
                MessageBox.Show("No se encontró ninguna tarea con ese código.");
            }
        }

        private void btnBuscarEstado_Click(object sender, EventArgs e)
        {
            if (cmbBuscarEstado.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un estado para buscar.");
                return;
            }

            string estadoBuscado = cmbBuscarEstado.SelectedItem.ToString();

            var resultados = listaTareas.Where(t => t.Estado == estadoBuscado).ToList();

            if (resultados.Any())
            {
                dgvTareas.DataSource = null;
                dgvTareas.DataSource = resultados;
            }
            else
            {
                MessageBox.Show("No se encontraron tareas con ese estado.");
            }
        }

        private void btnBuscarFechas_Click(object sender, EventArgs e)
        {
            DateTime desde = dtpDesde.Value.Date;
            DateTime hasta = dtpHasta.Value.Date;

            var resultados = listaTareas.Where(t => t.Fecha.Date >= desde && t.Fecha.Date <= hasta).ToList();

            if (resultados.Any())
            {
                dgvTareas.DataSource = null;
                dgvTareas.DataSource = resultados;
            }
            else
            {
                MessageBox.Show("No se encontraron tareas en ese rango de fechas.");
            }
        }
    }
}
