using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Oracle.ManagedDataAccess.Client;

namespace ElectronicTech
{
    public partial class RegistrarFORM : Form
    {
        string conect = "Data Source=localhost:1521/xe;User Id=prueba;Password=Yuniel1120;";
        public RegistrarFORM()
        {
            InitializeComponent();

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void btn_enviar_Click(object sender, EventArgs e)
        {
            string idEmpleado = textBoxID.Text;
            if (int.TryParse(idEmpleado, out int NumeroAgregar))
            {
                string sueldo = textBoxSueldo.Text;
               if (int.TryParse(sueldo, out int NumeroSueldo))
               {
                    string nombre = textBoxNombre.Text;
                string apellido = textBoxApellido.Text;
                string direccion = textBoxDireccion.Text;
                string email = textBoxEmail.Text;
                string telefono = textBoxTelefono.Text;
                string cargo = textBoxCargo.Text;
                
                // Crear la cadena de conexión
                string cadenaConexion = "Data Source=localhost:1521/xe;User Id=prueba;Password=Yuniel1120;";

                // Crear la consulta de inserción
                string consulta = @"INSERT INTO empleados (id_empleado, nombre, apellido, direccion, email, telefono, cargo, sueldo) 
                            VALUES (:idEmpleado, :nombre, :apellido, :direccion, :email, :telefono, :cargo, :sueldo)";

                    // Crear la conexión y el comando
                    using (OracleConnection conexion = new OracleConnection(cadenaConexion))
                    using (OracleCommand comando = new OracleCommand(consulta, conexion))
                    {
                        // Agregar parámetros
                        comando.Parameters.Add(new OracleParameter("idEmpleado", NumeroAgregar));
                        comando.Parameters.Add(new OracleParameter("nombre", nombre));
                        comando.Parameters.Add(new OracleParameter("apellido", apellido));
                        comando.Parameters.Add(new OracleParameter("direccion", direccion));
                        comando.Parameters.Add(new OracleParameter("email", email));
                        comando.Parameters.Add(new OracleParameter("telefono", telefono));
                        comando.Parameters.Add(new OracleParameter("cargo", cargo));
                        comando.Parameters.Add(new OracleParameter("sueldo", NumeroSueldo));

                        try
                        {
                            // Abrir la conexión
                            conexion.Open();

                            // Ejecutar la consulta de inserción
                            int filasInsertadas = comando.ExecuteNonQuery();

                            // Verificar si se insertaron filas
                            if (filasInsertadas > 0)
                            {
                                MessageBox.Show("Empleado insertado correctamente.");
                            }
                            else
                            {
                                MessageBox.Show("No se pudo insertar el empleado.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al insertar el empleado: " + ex.Message);
                        }
                    } }
            }
        }
    }
}
