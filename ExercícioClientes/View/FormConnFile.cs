using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExercícioClientes.Control;

namespace ExercícioClientes.View
{
    public partial class FormConnFile : Form
    {
        public FormConnFile()
        {
            InitializeComponent();

            //seta a pasta inicial como a Desktop e mostra os arquivos de conexão, caso existam
            txtFolder.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            txtFolder.SelectionStart = txtFolder.Text.Length;
            Controller.BuscarArquivos(txtFolder.Text, lboxFiles);
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            Controller.BuscarPasta(txtFolder, lboxFiles);
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            Controller.Conectar(txtFolder, lboxFiles);
        }
    }
}
