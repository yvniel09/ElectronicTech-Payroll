using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using Oracle.ManagedDataAccess.Client;

namespace ElectronicTech
{
    public partial class loginFORM : Form
    {
        private string cadenaConexion = "Data Source=localhost:1521/xe;User Id=prueba;Password=Yuniel1120;";
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        public loginFORM()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void loginFORM_Load(object sender, EventArgs e)
        {
            RoundPictureBox(pictureBox1, 10);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RoundPictureBox(PictureBox pictureBox, int cornerRadius)
        {
            // Create a GraphicsPath object to define the shape of the rounded rectangle
            GraphicsPath path = new GraphicsPath();

            // Calculate the corner coordinates
            int x = pictureBox.ClientRectangle.Left;
            int y = pictureBox.ClientRectangle.Top;
            int width = pictureBox.Width;
            int height = pictureBox.Height;
            int diameter = cornerRadius * 2;

            // Add arcs for each corner of the rectangle
            path.AddArc(x, y, diameter, diameter, 180, 90);
            path.AddArc(x + width - diameter, y, diameter, diameter, 270, 90);
            path.AddArc(x + width - diameter, y + height - diameter, diameter, diameter, 0, 90);
            path.AddArc(x, y + height - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            // Apply the rounded rectangle shape to the PictureBox
            pictureBox.Region = new System.Drawing.Region(path);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string usuario = textBoxUsuario.Text;
            string contraseña = textBoxContraseña.Text;

            // Consulta SQL para verificar las credenciales en la tabla de usuarios
            string consulta = "SELECT COUNT(*) FROM usuarios WHERE nombre_usuario = :usuario AND contraseña = :contraseña";

            using (OracleConnection conexion = new OracleConnection(cadenaConexion))
            using (OracleCommand comando = new OracleCommand(consulta, conexion))
            {
                // Agregar parámetros
                comando.Parameters.Add(new OracleParameter("usuario", usuario));
                comando.Parameters.Add(new OracleParameter("contraseña", contraseña));

                try
                {
                    conexion.Open();
                    int count = Convert.ToInt32(comando.ExecuteScalar());

                    if (count > 0)
                    {
                        // Si las credenciales son válidas, mostrar el siguiente formulario
                        formPrincipal Form2 = new formPrincipal();
                        Form2.Show();
                        Hide();
                    }
                    else
                    {
                        MessageBox.Show("Usuario o contraseña incorrectos.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al autenticar: " + ex.Message);
                }
            }
        }
    }
}


