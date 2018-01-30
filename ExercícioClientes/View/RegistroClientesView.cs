using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExercícioClientes.Model;

namespace ExercícioClientes.View
{
    public class RegistroClientesView
    {
        //variaveis para referenciar os objetos no Form
        public ToolStripMenuItem menuItemPesquisar;
        public ToolStripMenuItem menuItemInserir;
        public TextBox txtNome;
        public TextBox txtSobrenome;
        public MaskedTextBox txtDataNasc;
        public ComboBox cboxSexo;
        public MaskedTextBox txtCEP;
        public TextBox txtEndNum;
        public TextBox txtEndComplemento;
        public TextBox txtBairro;
        public TextBox txtCidade;
        public ComboBox cboxEstado;
        public TextBox txtLogradouro;
        public Button btnOperacao;
        public Button btnProximo;
        public Button btnUltimo;
        public Button btnPrimeiro;
        public Button btnAnterior;
        public Label lblExcluido;
        public Label lblIndicePesquisa;

        ///<summary>
        ///Pega os valores digitados para pesquisa ou inserção e os atribui à um objeto Cliente
        ///</summary>
        public Cliente ViewToCliente()
        {
            Cliente cliView = new Cliente();
            cliView.Nome = txtNome.Text;
            cliView.Sobrenome = txtSobrenome.Text;

            //valida se a data é válida, e retorna null caso não seja
            if (txtDataNasc.Text.Trim(' ') != "/  /" && txtDataNasc.Text.Trim(' ') != "/" && txtDataNasc.Text.Trim(' ') != "")
            {
                try
                {
                    cliView.DataDeNascimento = DateTime.Parse(txtDataNasc.Text);
                    if (cliView.DataDeNascimento.Year <= 1753)
                        throw new ArgumentOutOfRangeException();
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Formato de data inválido. O ano deve ser maior do que 1753. \nEx: \"27/04/1990\"");
                    return null;
                }
                catch
                {
                    MessageBox.Show("Formato de data inválido. O campo \"Data de nascimento\" aceita apenas datas no formato DD/MM/AAAA. \nEx: \"27/04/1990\"");
                    return null;
                }
            }

            if (cboxSexo.SelectedItem!=null)
                cliView.Sexo = cboxSexo.SelectedItem.ToString();

            //se o CEP contem apenas "-" (está virtualmente vazio), é passado "" à propriedade CEP de cliView
            if (txtCEP.Text.Trim(' ') == "-" || txtCEP.Text.Trim(' ') == "")
                cliView.CEP = "";
            else
                cliView.CEP = txtCEP.Text;

            //Valida se o número é válido, e retorna null caso não seja
            if (txtEndNum.Text != "")
            {
                try
                {
                    int numAux = Int32.Parse(txtEndNum.Text);
                    //verifica se o Numero inputado não é negativo
                    if (numAux < 0)
                    {
                        MessageBox.Show("Formato de número inválido. O campo \"Número\" aceita apenas números positivos. \nEx.: \"500\", \"725\"...");
                        return null;
                    }
                    cliView.Numero = numAux;
                }
                catch
                {
                    MessageBox.Show("Formato de número inválido. O campo \"Número\" aceita apenas dígitos numéricos como caracters. \nEx.: \"500\", \"725\"...");
                    return null;
                }
            }
            cliView.Complemento = txtEndComplemento.Text;
            cliView.Logradouro = txtLogradouro.Text;
            cliView.Bairro = txtBairro.Text;
            cliView.Cidade = txtCidade.Text;
            cliView.Estado = cboxEstado.SelectedItem.ToString();

            return cliView;    
        }

        /// <summary>
        /// Recebe um objeto Cliente como parametro e usa suas propriedades para popular os objetos do Form
        /// </summary>
        public void ClienteToView(Cliente cliView)
        {         
            txtNome.Text = cliView.Nome;
            txtSobrenome.Text = cliView.Sobrenome;
            txtDataNasc.Text = cliView.DataDeNascimento.ToShortDateString();
            cboxSexo.SelectedItem = cliView.Sexo;
            cboxEstado.SelectedItem = cliView.Estado;
            txtCEP.Text = cliView.CEP;
            txtEndNum.Text = cliView.Numero.ToString();
            txtEndComplemento.Text = cliView.Complemento;
            txtBairro.Text = cliView.Bairro;
            txtCidade.Text = cliView.Cidade;
            txtLogradouro.Text = cliView.Logradouro;
        }

        /// <summary>
        /// Limpa todos os valores dos objetos referentes às propriedades dos clientes na View e desabilita os botões de navegação
        /// </summary>
        public void Clear()
        {
            txtNome.Text = "";
            txtSobrenome.Text = "";
            txtDataNasc.Text = "";
            cboxSexo.SelectedIndex = 0;
            txtCEP.Text = "";
            txtEndNum.Text = "";
            txtEndComplemento.Text = "";
            txtBairro.Text = "";
            txtCidade.Text = "";
            cboxEstado.SelectedIndex = 0;
            txtLogradouro.Text = "";
            btnAnterior.Enabled = false;
            btnPrimeiro.Enabled = false;
            btnProximo.Enabled = false;
            btnUltimo.Enabled = false;
            lblIndicePesquisa.Visible = false;
        }

        /// <summary>
        /// Desabilita os campos editaveis e o botão de operação
        /// </summary>
        public void Disable()
        {
            txtNome.Enabled = false;
            txtSobrenome.Enabled = false;
            txtDataNasc.Enabled = false;
            cboxSexo.Enabled = false;
            txtCEP.Enabled = false;
            txtEndNum.Enabled = false;
            txtEndComplemento.Enabled = false;
            btnOperacao.Enabled = false;
            lblExcluido.Visible = true;
        }
        
        /// <summary>
        /// habilita os campos editaveis e o botao de operação
        /// </summary>
        public void Enable()
        {
            txtNome.Enabled = true;
            txtSobrenome.Enabled = true;
            txtDataNasc.Enabled = true;
            cboxSexo.Enabled = true;
            txtCEP.Enabled = true;
            txtEndNum.Enabled = true;
            txtEndComplemento.Enabled = true;
            btnOperacao.Enabled = true;
            lblExcluido.Visible = false;
        }
    }
}
