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
using Oracle.ManagedDataAccess.Client;

namespace ElectronicTech
{
    public partial class empleadosFORM : Form
    {
        private OracleConnection conexion;

        public empleadosFORM()
        {
            InitializeComponent();
            string cadenaConexion = "Data Source=localhost:1521/xe;User Id=prueba;Password=Yuniel1120;";
            conexion = new OracleConnection(cadenaConexion);
        }

        private void empleadosFORM_Load(object sender, EventArgs e)
        {
            CargarDatosEmpleados();
        }

        private void CargarDatosEmpleados()
        {
            string consulta = "SELECT * FROM empleados";

            OracleDataAdapter adaptador = new OracleDataAdapter(consulta, conexion);
            DataTable dt = new DataTable();

            try
            {
                conexion.Open();
                adaptador.Fill(dt);
                dataGridViewEmpleados.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        private void btn_patra_Click(object sender, EventArgs e)
        {
            formPrincipal Form2 = new formPrincipal();
            Form2.Show();
            this.Close();
        }
    }
}