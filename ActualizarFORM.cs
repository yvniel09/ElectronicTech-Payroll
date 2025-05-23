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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;


namespace ElectronicTech
{
    public partial class ActualizarFORM : Form
    {
        public ActualizarFORM()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_enviar_Click(object sender, EventArgs e)
        {
            string idEmpleado = textBoxID.Text;
              if (int.TryParse(idEmpleado, out int NumeroAgregar))
              {
                string sueldo = TextBoxSueldo.Text;
                string nombre = textBoxNombre.Text;
                    string apellido = textBoxApellido.Text;
                    string direccion = textBoxDireccion.Text;
                    string email = textBoxEmail.Text;
                    string telefono = textBoxTelefono.Text;
                    string cargo = TextBoxCargo.Text;
                   


                    // Crear la cadena de conexión
                    string cadenaConexion = "Data Source=localhost:1521/xe;User Id=prueba;Password=Yuniel1120;";

                // Crear la consulta de actualización
                string consulta = @"UPDATE empleados 
SET nombre = :nombre,
    apellido = :apellido,
    direccion = :direccion,
    email = :email,
    sueldo = :sueldo,
    cargo = :cargo
WHERE id_empleado = :idEmpleado;";

                // Crear la conexión y el comando
                using (OracleConnection conexion = new OracleConnection(cadenaConexion))
                using (OracleCommand comando = new OracleCommand(consulta, conexion))
                {
                    // Agregar parámetros
                    comando.Parameters.Add(new OracleParameter("nombre", nombre));
                    comando.Parameters.Add(new OracleParameter("apellido", apellido));
                    comando.Parameters.Add(new OracleParameter("direccion", direccion));
                    comando.Parameters.Add(new OracleParameter("email", email));
                    comando.Parameters.Add(new OracleParameter("telefono", telefono));
                    comando.Parameters.Add(new OracleParameter("idEmpleado", NumeroAgregar));
                    comando.Parameters.Add(new OracleParameter("sueldo", sueldo));
                    comando.Parameters.Add(new OracleParameter("cargo", cargo));
                        try
                        {
                            // Abrir la conexión
                            conexion.Open();

                            // Ejecutar la consulta de actualización
                            int filasActualizadas = comando.ExecuteNonQuery();

                            // Verificar si se actualizaron filas
                            if (filasActualizadas > 0)
                            {
                                MessageBox.Show("Empleado actualizado correctamente.");
                            }
                            else
                            {
                                MessageBox.Show("No se pudo actualizar el empleado.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al actualizar el empleado: " + ex.Message);
                        }     }
                }
              }
        }
    }




