using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using ExercícioClientes.Model;
using ExercícioClientes.Control;

namespace ExercícioClientes.View
{
    public partial class FormClientes : Form
    {
        //instancia RegistroClientes e RegistroClientesView para usar como paramentros ao chamar os métodos da Controller
        RegistroClientes regClientes;
        RegistroClientesView regView;

        public FormClientes(SqlConnection conexao)
        {
            InitializeComponent();

            //inicializa as veriaveis que serão usadas como parametros para os métodos da Controller
            regClientes = new RegistroClientes(conexao);
            regView = new RegistroClientesView();

            //as referencias aos objetos do Form sao passadas à regView
            regView.txtNome = this.txtNome;
            regView.txtSobrenome = this.txtSobrenome;
            regView.txtDataNasc = this.txtDataNasc;
            regView.cboxSexo = this.cboxSexo;
            regView.txtCEP = this.txtCEP;
            regView.txtEndNum = this.txtNumero;
            regView.txtEndComplemento = this.txtComplemento;
            regView.txtBairro = this.txtBairro;
            regView.txtCidade = this.txtCidade;
            regView.txtLogradouro = this.txtLogradouro;
            regView.cboxEstado = this.cboxEstado;
            regView.btnOperacao = this.btnOperacao;
            regView.btnAnterior = this.btnAnterior;
            regView.btnPrimeiro = this.btnPrimeiro;
            regView.btnProximo = this.btnProximo;
            regView.btnUltimo = this.btnUltimo;
            regView.menuItemInserir = this.MenuItemInserir;
            regView.menuItemPesquisar = this.MenuItemPesquisar;
            regView.lblExcluido = this.lblExcluído;
            regView.lblIndicePesquisa = this.lblIndicePesquisa;

            //seta as opções disponiveis na combobox cboxEstado
            Controller.BuscarEstados(regClientes, regView);

            //seta o modo Inserir como tela inicial
            Controller.ModoInserir(regClientes, regView);
        }

        /// <summary>
        /// Fecha a conexão e volta para a tela de seleção de arquivos de conexão
        /// </summary>
        private void MenuItemDesconectar_Click(object sender, EventArgs e)
        {
            if(regClientes.ListaClientes.Count > 0)
            {
                if (!regClientes.ListaClientes[regClientes.indexClienteNaPesquisa - 1].Saved)
                {
                    DialogResult ContinuarSemSalvar = MessageBox.Show("Há alterações não salvas no registro atual. Deseja continuar sem salvar?", "Dados não salvos!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (ContinuarSemSalvar == DialogResult.No)
                        return;
                }
            }
            this.Close();
            this.DialogResult = DialogResult.No;       
        }

        private void MenuItemConInfo_Click(object sender, EventArgs e)
        {
            //recupera o usuário da connection string
            int iUser = regClientes.Connection.ConnectionString.IndexOf("User Id=") + 8;
            int iPassword = regClientes.Connection.ConnectionString.LastIndexOf(';');
            string user = regClientes.Connection.ConnectionString.Substring(iUser, iPassword-iUser);
            //mostra as informações da conexão
            MessageBox.Show("Servidor: " + regClientes.Connection.DataSource + "\nBanco de dados: " + regClientes.Connection.Database + "\nUsuário: " + user, "Informações da conexão", MessageBoxButtons.OK);
        }

        private void btnOperacao_Click(object sender, EventArgs e)
        {
            if (btnOperacao.Text == "Pesquisar")
                Controller.PesquisarCliente(regClientes, regView);
            else if (btnOperacao.Text == "Atualizar")
                Controller.AtualizarCliente(regClientes, regView);
            else if (btnOperacao.Text == "Inserir")
                Controller.InserirCliente(regClientes, regView);
        }

        private void MenuItemPesquisar_Click(object sender, EventArgs e)
        {
            Controller.ModoPesquisa(regClientes,regView);            
        }

        private void MenuItemInserir_Click(object sender, EventArgs e)
        {
            Controller.ModoInserir(regClientes, regView);
        }

        private void MenuItemExcluir_Click(object sender, EventArgs e)
        {
            Controller.ExcluirCliente(regClientes, regView);
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            Controller.Proximo(regClientes, regView);
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            Controller.Anterior(regClientes, regView);
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            Controller.Ultimo(regClientes, regView);
        }

        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            Controller.Primeiro(regClientes, regView);
        }

        private void txtGeneric_TextChanged(object sender, EventArgs e)
        {
            Controller.DadoAlterado(regClientes, regView);
        }

        private void txtCEP_Leave(object sender, EventArgs e)
        {
            if (!Controller.ValidaCEP(regClientes,regView))
                txtCEP.Focus();
        }
    }
}
