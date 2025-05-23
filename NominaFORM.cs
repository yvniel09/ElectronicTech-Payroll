using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ElectronicTech
{
    public partial class NominaFORM : Form
    {
        private string cadenaConexion = "Data Source=localhost:1521/xe;User Id=prueba;Password=Yuniel1120;";

        public NominaFORM()
        {
            InitializeComponent();

        }

        private void NominaFORM_Load(object sender, EventArgs e)
        {
            CargarDatosNomina();
        }

        private void CargarDatosNomina()
        {
            string consulta = "SELECT * FROM nomina";

            using (OracleConnection conexion = new OracleConnection(cadenaConexion))
            using (OracleCommand comando = new OracleCommand(consulta, conexion))
            {
                try
                {
                    conexion.Open();
                    OracleDataAdapter adaptador = new OracleDataAdapter(comando);
                    DataTable dt = new DataTable();
                    adaptador.Fill(dt);
                    dataGridViewNomina.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los datos de la nómina: " + ex.Message);
                }
            }
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {

            string codigoEmpleado = txtIdEmpleado.Text;

            // Consulta SQL para buscar la información de la nómina por el código de empleado
            string consulta = "SELECT NOMBRE, APELLIDO, SUELDO, CARGO FROM empleados WHERE id_empleado = :codigoEmpleado";

            using (OracleConnection conexion = new OracleConnection(cadenaConexion))
            using (OracleCommand comando = new OracleCommand(consulta, conexion))
            {
                // Agregar parámetros
                comando.Parameters.Add(new OracleParameter("codigoEmpleado", codigoEmpleado));

                try
                {
                    conexion.Open();
                    OracleDataReader reader = comando.ExecuteReader();

                    if (reader.Read())
                    {
                        // Si se encontró información, mostrarla en los TextBox correspondientes
                        txtNombre.Text = reader["NOMBRE"].ToString();
                        txtApellido.Text = reader["APELLIDO"].ToString();
                        txtCargo.Text = reader["CARGO"].ToString();
                        decimal sueldo = reader.GetDecimal(reader.GetOrdinal("SUELDO"));
                        txtSalario.Text = sueldo.ToString("C");
                    }
                    else
                    {
                        MessageBox.Show("No se encontró información de nómina para el código de empleado especificado.");
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar en la nómina: " + ex.Message);
                }
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            formPrincipal Form2 = new formPrincipal();
            Form2.Show();
            this.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            string idEmpleado = txtNombre.Text;
            string nombre = txtNombre.Text;
            string apellido = txtApellido.Text;
            string salario = txtSalario.Text;
            string cargo = txtCargo.Text;
            string horasExtras = txtHorasExtras.Text;
            string horasTrabajadas = txtHorasTrabajadas.Text;
            string totalPago = txtTotalPago.Text;

            // Crear la consulta para insertar un nuevo registro en la tabla "nomina"
            string consulta = @"INSERT INTO nomina (id_empleado, nombre, apellido, sueldo_b, cargo, horas_ex, horas_tb, total_pago) 
                                VALUES (:idNomina, :idEmpleado, :nombre, :apellido, :salario, :cargo, :horasExtras, :horasTrabajadas, :totalPago)";

            using (OracleConnection conexion = new OracleConnection(cadenaConexion))
            using (OracleCommand comando = new OracleCommand(consulta, conexion))
            {
                // Agregar parámetros
               
                comando.Parameters.Add("idEmpleado", OracleDbType.Int32).Value = Convert.ToInt32(idEmpleado);
                comando.Parameters.Add("nombre", OracleDbType.Varchar2).Value = nombre;
                comando.Parameters.Add("apellido", OracleDbType.Varchar2).Value = apellido;
                comando.Parameters.Add("salario", OracleDbType.Decimal).Value = Convert.ToDecimal(salario);
                comando.Parameters.Add("cargo", OracleDbType.Varchar2).Value = cargo;
                comando.Parameters.Add("horasExtras", OracleDbType.Int32).Value = Convert.ToInt32(horasExtras);
                comando.Parameters.Add("horasTrabajadas", OracleDbType.Int32).Value = Convert.ToInt32(horasTrabajadas);
                comando.Parameters.Add("totalPago", OracleDbType.Decimal).Value = Convert.ToDecimal(totalPago);

                try
                {
                    conexion.Open();
                    int filasAfectadas = comando.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Datos de nómina agregados correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("No se pudo agregar los datos de nómina.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar datos de nómina: " + ex.Message);
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
