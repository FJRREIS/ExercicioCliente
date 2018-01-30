using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using ExercícioClientes.Model;
using ExercícioClientes.View;

namespace ExercícioClientes.Control
{
    public class Controller
    {
        /// <summary>
        /// Seleciona a pasta e mostra os arquivos
        /// </summary>
        public static void BuscarPasta(TextBox folder, ListBox files)
        {
            //cria um novo FolderBrowseDialog e armazena o resultado na variavel result
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            DialogResult result = folderDialog.ShowDialog();

            //se foi selecionada uma pasta, coloca seu caminho na textbox 'folder'
            if (result == DialogResult.OK)
            {
                folder.Text = folderDialog.SelectedPath;
                //seta o cursor da textbox para o final do texto
                folder.SelectionStart = folder.Text.Length;
                BuscarArquivos(folder.Text, files);
            }
        }

        /// <summary>
        /// Verifica se há arquivos conexao.txt ou conexao.xml no endereço indicado e, se encontrados, adiciona os arquivos como items da listbox indicada
        /// </summary>
        /// <param name="filePath">String com caminho para pasta onde será feita a busca</param>
        /// <param name="fileBox">Listbox onde serão adicionados os arquivos encontrados</param>
        public static void BuscarArquivos(string filePath, ListBox fileBox)
        {
            //limpa a listbox, para o caso de haver buscas anteriores
            fileBox.Items.Clear();

            //verifica se existe arquivo de conexão na pasta passada em filePath e os adiciona na listbox passada como parametro
            if (File.Exists(filePath + @"\conexao.txt"))
            {
                fileBox.Items.Add("conexao.txt");
            }
            if (File.Exists(filePath + @"\conexao.xml"))
            {
                fileBox.Items.Add("conexao.xml");
            }
        }

        /// <summary>
        /// Tenta criar uma conexão com o banco de dados usando os dados do arquivo(item) selecionado na listbox informada
        /// </summary>
        /// <param name="path">String com o path do folder onde está o arquivo</param>
        /// <param name="file">Listbox onde devem estar listados os arquivos de conexão</param>
        public static void Conectar(TextBox path, ListBox file)
        {
            //verificar se há algum item(arquivo) selecionado na Listbox passada como parametro
            if(file.SelectedItem == null)
            {
                MessageBox.Show("Nenhum arquivo selecionado. \nPor favor selecione um arquivo para iniciar a conexão.", "Erro", MessageBoxButtons.OK ,MessageBoxIcon.Error);
                return;
            }

            //cria uma instância de conexão para se conectar ao banco, caso haja erros serão mostrados ao chamar o método Conexão.Conectar()
            Conexão _conexão = new Conexão(path.Text + @"\" + file.SelectedItem.ToString());
            _conexão.Conectar();

            //Esconde o form principal e chama o form de registro de clientes sem finalizar o método
            Form aux = (Form)file.Parent;
            aux.Hide();
            FormClientes formClientes = new FormClientes(_conexão.Connection);
            DialogResult cli = formClientes.ShowDialog();

            //Caso o usuario feche a janela de registro de clientes, a aplicação se encerra
            if (cli == DialogResult.Cancel)
            {
                Application.Exit();
            }
            //Caso o usuário clique em desconectar, o form de conexão é mostrado novamente
            if (cli == DialogResult.No)
            {
                _conexão.FecharConexão();
                aux.Show();
            }
        }

        /// <summary>
        /// Utiliza uma instancia de RegistroClientes para realizar as operações de pesquisa e atualiza a View através de uma instancia da classe RegistroClientesViews
        /// </summary>
        public static void PesquisarCliente(RegistroClientes registro, RegistroClientesView regView)
        {

            registro.ListaClientes.Clear();

            //caso a data e/ou numero inputados sejam inválidos, ViewToCliente retornará null e mostrará uma mensagem. Nesse caso, a pesquisa não é realizada
            if (regView.ViewToCliente() == null)
                return;

            //adiciona os valores inputados nos campos editáveis para uma instância de Cliente e a adiciona como propriedade da instancia de RegistroClientes passada como parametro
            registro.ListaClientes.Add(regView.ViewToCliente());

            registro.Pesquisar();

            //se nenhum resultado foi retornado, nada é feito
            if (!registro.UltimaPesquisa)
                return;

            //mostra o primeiro resultado e o indica como o primeiro na propriedade indexClienteNaPesquisa, de RegistroClientes
            regView.ClienteToView(registro.ListaClientes[0]);
            registro.indexClienteNaPesquisa = 1;
            registro.ListaClientes[0].Saved = true;

            //se mais de 1 resultado foi retornado, os botões Proximo e Ultimo são habilitados
            if (registro.numClientesNaPesquisa > 1)
            {
                regView.btnProximo.Enabled = true;
                regView.btnUltimo.Enabled = true;
            }

            //atualiza os estados do btnOperção e do menuItemPesquisar
            regView.btnOperacao.Text = "Atualizar";
            regView.btnOperacao.Enabled = false;
            regView.menuItemPesquisar.Checked = false;

            //atualiza a label que indica o indice da pesquisa
            AtualizarIndice(registro, regView);

            //muda o foco para a textbox que contém Nome
            regView.txtNome.Focus();
            regView.txtNome.Select(0,0);
        }

        /// <summary>
        /// Mostra o próximo registro em uma pesquisa com mais de um resultado
        /// </summary>
        public static void Proximo(RegistroClientes registro, RegistroClientesView regView)
        {
            //caso haja alterações não salvas, o usuário é avisado e escolhe continuar sem salvar ou não
            if (!registro.ListaClientes[registro.indexClienteNaPesquisa - 1].Saved)
            {
                DialogResult ContinuarSemSalvar = MessageBox.Show("Há alterações não salvas no registro atual. Deseja continuar sem salvar?", "Dados não salvos!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ContinuarSemSalvar == DialogResult.No)
                    return;
            }

            //caso o indice auxiliar de controle dos resultados da pesquisa já esteja na posição final, nada é feito e uma mensagem é mostrada
            //caso não haja registros o suficiente, nada é feito e uma mensagem é mostrada
            if (registro.ListaClientes.Count == registro.indexClienteNaPesquisa || registro.ListaClientes.Count <= 1)
            {
                MessageBox.Show("Não há um próximo registro para ser acessado.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //atualiza o indice de navegação de registro.ListaClientes
            registro.indexClienteNaPesquisa++;

            //limpa a tela(e desabilita os botoes)
            regView.Clear();
            regView.Enable();
            //atualiza a label que indica o indice da pesquisa
            AtualizarIndice(registro, regView);
            //exibe o proximo resultado da pesquisa
            //caso o proximo registro tenha disabled como true, desabilita os controles e não chama nenhum dado do registro
            if (registro.ListaClientes[registro.indexClienteNaPesquisa-1].Disabled)
                regView.Disable();
            else
                regView.ClienteToView(registro.ListaClientes[registro.indexClienteNaPesquisa-1]);

            registro.ListaClientes[registro.indexClienteNaPesquisa-1].Saved = true;
            regView.btnOperacao.Enabled = false;

            //habilita os botoes Anterior e Primeiro
            regView.btnAnterior.Enabled = true;
            regView.btnPrimeiro.Enabled = true;

            //Caso, ao acessar o proximo registro, nao se chegue ao ultimo, os botoes Proximo e Ultimo sao habilitados
            if (registro.numClientesNaPesquisa != registro.indexClienteNaPesquisa)
            {
                regView.btnProximo.Enabled = true;
                regView.btnUltimo.Enabled = true;
            }  
        }

        /// <summary>
        /// Mostra o último registro em uma pesquisa com mais de um resultado
        /// </summary>
        public static void Ultimo(RegistroClientes registro, RegistroClientesView regView)
        {
            //caso haja alterações não salvas, o usuário é avisado e escolhe continuar sem salvar ou não
            if (!registro.ListaClientes[registro.indexClienteNaPesquisa - 1].Saved)
            {
                DialogResult ContinuarSemSalvar = MessageBox.Show("Há alterações não salvas no registro atual. Deseja continuar sem salvar?", "Dados não salvos!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ContinuarSemSalvar == DialogResult.No)
                    return;
            }

            //caso não haja múltiplos registros a serem mostrados, nada é feito
            if (registro.ListaClientes.Count <= 1)
            {
                MessageBox.Show("Operação inválida: função último funciona apenas para múltiplos registros.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //seta o indice de navegação do registro para a ultima posição
            registro.indexClienteNaPesquisa = registro.ListaClientes.Count;

            //limpa a tela(e desabilita os botoes)
            regView.Clear();
            regView.Enable();
            //atualiza a label que indica o indice da pesquisa
            AtualizarIndice(registro, regView);
            //exibe o último resultado da pesquisa
            //caso o ultimo registro tenha disabled como true, desabilita os controles e não chama nenhum dado do registro
            if (registro.ListaClientes[registro.indexClienteNaPesquisa-1].Disabled)
                regView.Disable();
            else
                regView.ClienteToView(registro.ListaClientes[registro.indexClienteNaPesquisa - 1]);

            registro.ListaClientes[registro.indexClienteNaPesquisa - 1].Saved = true;
            regView.btnOperacao.Enabled = false;

            //habilita os botoes Anterior e Primeiro
            regView.btnAnterior.Enabled = true;
            regView.btnPrimeiro.Enabled = true;
        }

        /// <summary>
        /// Mostra o registro anterior em uma pesquisa com mais de um resultado
        /// </summary>
        public static void Anterior(RegistroClientes registro, RegistroClientesView regView)
        {
            //caso haja alterações não salvas, o usuário é avisado e escolhe continuar sem salvar ou não
            if (!registro.ListaClientes[registro.indexClienteNaPesquisa - 1].Saved)
            {
                DialogResult ContinuarSemSalvar = MessageBox.Show("Há alterações não salvas no registro atual. Deseja continuar sem salvar?", "Dados não salvos!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ContinuarSemSalvar == DialogResult.No)
                    return;
            }

            //caso o indice auxiliar de controle dos resultados da pesquisa já esteja na posição inicial, nada é feito e uma mensagem é mostrada
            //caso não haja registros o suficiente, nada é feito e uma mensagem é mostrada
            if (registro.indexClienteNaPesquisa == 1 || registro.ListaClientes.Count <= 1)
            {
                MessageBox.Show("Não há um registro anterior para ser acessado.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //atualiza o indice de navegação de registro.ListaClientes
            registro.indexClienteNaPesquisa--;

            //limpa a tela(e desabilita os botoes)
            regView.Clear();
            regView.Enable();
            //atualiza a label que indica o indice da pesquisa
            AtualizarIndice(registro, regView);
            //exibe o proximo resultado da pesquisa
            //caso o registro anterior tenha disabled como true, desabilita os controles e não chama nenhum dado do registro
            if (registro.ListaClientes[registro.indexClienteNaPesquisa - 1].Disabled)
                regView.Disable();
            else
                regView.ClienteToView(registro.ListaClientes[registro.indexClienteNaPesquisa-1]);
            registro.ListaClientes[registro.indexClienteNaPesquisa - 1].Saved = true;
            regView.btnOperacao.Enabled = false;

            //habilita os botoes Próximo e Último
            regView.btnProximo.Enabled = true;
            regView.btnUltimo.Enabled = true;

            //Caso, ao acessar registro anterior, nao se chegue ao primeiro, os botoes Anterior e Primeiro sao habilitados
            if ((registro.indexClienteNaPesquisa-1) != 0 )
            {
                regView.btnAnterior.Enabled = true;
                regView.btnPrimeiro.Enabled = true;
            }
        }

        /// <summary>
        /// Mostra o primeiro registro em uma pesquisa com mais de um resultado
        /// </summary>
        public static void Primeiro(RegistroClientes registro, RegistroClientesView regView)
        {
            //caso haja alterações não salvas, o usuário é avisado e escolhe continuar sem salvar ou não
            if (!registro.ListaClientes[registro.indexClienteNaPesquisa - 1].Saved)
            {
                DialogResult ContinuarSemSalvar = MessageBox.Show("Há alterações não salvas no registro atual. Deseja continuar sem salvar?", "Dados não salvos!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ContinuarSemSalvar == DialogResult.No)
                    return;
            }

            //caso não haja múltiplos registros a serem mostrados, nada é feito
            if (registro.ListaClientes.Count <= 1)
            {
                MessageBox.Show("Operação inválida: função primeiro funciona apenas para múltiplos registros.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //atualiza o indice de navegação de registro.ListaClientes
            registro.indexClienteNaPesquisa = 1;

            //limpa a tela(e desabilita os botoes)
            regView.Clear();
            regView.Enable();
            //atualiza a label que indica o indice da pesquisa
            AtualizarIndice(registro, regView);
            //exibe o primeiro resultado da pesquisa
            //caso o registro anterior tenha disabled como true, desabilita os controles e não chama nenhum dado do registro
            if (registro.ListaClientes[registro.indexClienteNaPesquisa - 1].Disabled)
                regView.Disable();
            else
                regView.ClienteToView(registro.ListaClientes[registro.indexClienteNaPesquisa - 1]);

            registro.ListaClientes[registro.indexClienteNaPesquisa - 1].Saved = true;
            regView.btnOperacao.Enabled = false;

            //habilita os botoes Proximo e Ultimo
            regView.btnProximo.Enabled = true;
            regView.btnUltimo.Enabled = true;
        }

        /// <summary>
        /// Muda a view para o modo de pesquisa
        /// </summary>
        public static void ModoPesquisa(RegistroClientes registro, RegistroClientesView regView)
        {
            //caso o modo Pesquisa já esteja selecionado, nada é feito
            if (regView.menuItemPesquisar.Checked)
                return;

            //caso haja dados não salvos, confirma se o usuario deseja continuar sem salvar e perder as alterações feitas
            if(registro.numClientesNaPesquisa>0)
            {
                if (!registro.ListaClientes[registro.indexClienteNaPesquisa-1].Saved)
                {
                    DialogResult ContinuarSemSalvar = MessageBox.Show("Há alterações não salvas no registro atual. Deseja continuar sem salvar?", "Dados não salvos!", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                    if (ContinuarSemSalvar == DialogResult.No)
                        return;
                }
            }

            //limpa os dados armazenados na instancia 'registro'
            registro.ListaClientes.Clear();
            registro.indexClienteNaPesquisa = 0;
            registro.numClientesNaPesquisa = 0;

            //seta a View para o modo pesquisar
            regView.Clear();
            regView.Enable();
            regView.menuItemInserir.Checked = false;
            regView.menuItemPesquisar.Checked = true;
            regView.btnOperacao.Text = "Pesquisar";
            regView.btnOperacao.Enabled = true;

        }

        /// <summary>
        /// Muda a view para o modo de insersão
        /// </summary>
        public static void ModoInserir(RegistroClientes registro, RegistroClientesView regView)
        {
            //caso o modo Inserir já esteja selecionado, nada é feito
            if (regView.menuItemInserir.Checked)
                return;

            //caso haja dados não salvos, confirma se o usuario deseja continuar sem salvar e perder as alterações feitas
            if (registro.numClientesNaPesquisa > 0)
            {
                if (!registro.ListaClientes[registro.indexClienteNaPesquisa - 1].Saved)
                {
                    DialogResult ContinuarSemSalvar = MessageBox.Show("Há alterações não salvas no registro atual. Deseja continuar sem salvar?", "Dados não salvos!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (ContinuarSemSalvar == DialogResult.No)
                        return;
                }
            }

            //limpa os dados armazenados na instancia 'registro'
            registro.ListaClientes.Clear();

            //adiciona uma instancia Cliente à ListaClientes (registro a ser salvo, caso alguma alteração -dados inputados- seja feita)
            Cliente novoCliente = new Cliente();
            registro.ListaClientes.Add(novoCliente);
            registro.indexClienteNaPesquisa = 1;
            registro.numClientesNaPesquisa = 1;

            //seta a View para o modo Inserir
            regView.Clear();
            regView.Enable();
            registro.ListaClientes[0].Saved = true;
            regView.menuItemInserir.Checked = true;
            regView.menuItemPesquisar.Checked = false;
            regView.btnOperacao.Text = "Inserir";
            regView.btnOperacao.Enabled = true;
        }

        /// <summary>
        /// Atualiza os registros alterados para o banco de dados
        /// </summary>
        public static void AtualizarCliente(RegistroClientes registro, RegistroClientesView regView)
        {
            //se o registro já estiver salvo, nada é feito
            if (registro.ListaClientes[registro.indexClienteNaPesquisa - 1].Saved)
                return;

            //variavel auxiliar para armazenar o registro do Cliente com as informações alteradas
            Cliente aux = new Cliente();
            aux = regView.ViewToCliente();
            
            //caso a data e/ou numero inputados sejam inválidos, ViewToCliente retornará null e mostrará uma mensagem. Nesse caso, a atualização não é realizada
            if (aux == null)
                return;

            //Passa ID para aux, para nao ocorrer conflitos com o proprio registro na validação de dados, logo abaixo
            aux.Id = registro.ListaClientes[registro.indexClienteNaPesquisa - 1].Id;

            //se algum dado estiver inválido, nada é feito e ValidaDados mostrará uma mensagem de erro
            if (!ValidaDados(aux, registro.Connection))
                return;

            //salva o registro de Cliente na tela com a informações atualizadas na variavel aux
            //salva as alterações no banco de dados e marca o registro como salvo
            registro.ListaClientes[registro.indexClienteNaPesquisa - 1] = aux;
            registro.Atualizar();
            registro.ListaClientes[registro.indexClienteNaPesquisa - 1].Saved = true;

            //desabilita o botao Atualizar
            regView.btnOperacao.Enabled = false;

            MessageBox.Show("Registro atualizado com sucesso.", "Dados atualizados", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        /// <summary>
        /// Insere um novo cliente no banco de dados
        /// </summary>
        public static void InserirCliente(RegistroClientes registro, RegistroClientesView regView)
        {
            //caso não haja nenhuma instancia de Cliente ou haja mais de uma em registro para ser inserida, nada é feito
            if(registro.ListaClientes.Count != 1)
            {
                MessageBox.Show("Impossível inserir zero ou múltiplos registros.", "Insersão inválida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //caso o Id não seja zero(inserindo um item já existente), nada é feito
            if(registro.ListaClientes[0].Id != 0)
            {
                MessageBox.Show("Impossível inserir o item. Somente registros novos podem ser inseridos.", "Insersão inválida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //caso a data e/ou numero inputados sejam inválidos, ViewToCliente retornará null e mostrará uma mensagem. Nesse caso, a insersão não é realizada
            if (regView.ViewToCliente() == null)
                return;

            //armazena as informações inputadas no form no registro de clientes para ser validado
            registro.ListaClientes[0] = regView.ViewToCliente();
            //marca saved como false, pois ViewToCliente nao altera o valor padrão dessa propriedade, que é true
            registro.ListaClientes[0].Saved = false;

            //se algum dado estiver inválido, nada é feito e ValidaDados mostrará uma mensagem de erro
            if (!ValidaDados(registro.ListaClientes[0], registro.Connection))
                return;

            //insere o novo registro no banco de dados e mostra uma mensagem confirmando a operação
            registro.Inserir();
            MessageBox.Show("Registro inserido com sucesso.", "Dados inseridos", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //marca o menu inserir como unchecked, para poder chamar o método ModoInserir dando bypass nessa validação
            //marca o registro como salvo
            registro.ListaClientes[0].Saved = true;
            regView.menuItemInserir.Checked = false;
            ModoInserir(registro, regView);
        }

        /// <summary>
        /// Exclui do banco de dados um registro presente em um resultado de pesquisa
        /// </summary>
        public static void ExcluirCliente(RegistroClientes registro, RegistroClientesView regView)
        {
            //verifica se há ao menos um registro para ser excluído, e não faz nada caso não haja
            if (registro.ListaClientes.Count < 1)
            {
                MessageBox.Show("Não há registro para ser excluído.", "Exclusão inválida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //caso haja um registro na tela que possa ser excluído, é verificado se seu Id é zero (item sendo inserido, não existe no banco de dados)
            if (registro.ListaClientes[registro.indexClienteNaPesquisa-1].Id == 0)
            {
                MessageBox.Show("Registro não existe no banco de dados.", "Exclusão inválida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //confirma com o usuário se ele deseja mesmo excluir o registro sendo exibido na tela
            DialogResult drExcluir = MessageBox.Show("Deseja excluir o registro atual do banco de dados?", "Excluir registro", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            //caso o usuário responda não, nada é feito
            if (drExcluir == DialogResult.No)
                return;

            //exclui do banco de dados o registro exibido
            registro.Excluir();

            //salva o estado dos botões de navegação, que são desabilitados no método regView.Clear() chamado logo abaixo
            bool bAnterior = regView.btnAnterior.Enabled;
            bool bPrimeiro = regView.btnPrimeiro.Enabled;
            bool bProximo = regView.btnProximo.Enabled;
            bool bUltimo = regView.btnUltimo.Enabled;

            //limpa e disabilita a tela no registro excluido
            regView.Clear();
            regView.Disable();
            //atualiza a label que indica o indice da pesquisa, pois mesmo excluído do banco, o registro continua na pesquisa
            AtualizarIndice(registro, regView);

            //volta os botões de navegação para seus estados anteriores
            regView.btnAnterior.Enabled = bAnterior;
            regView.btnPrimeiro.Enabled = bPrimeiro;
            regView.btnProximo.Enabled = bProximo;
            regView.btnUltimo.Enabled = bUltimo;

            //marca o registro atual como salvo e desabilitado, e seta seu Id para zero(não existente no banco de dados)
            //marca como salvo pois, ao excluir, pode ser que houvessem mudanças não salvas. Nesse caso as alterações são ignoradas, e a propriedade saved é setada como true novamente
            registro.ListaClientes[registro.indexClienteNaPesquisa - 1].Saved = true;
            registro.ListaClientes[registro.indexClienteNaPesquisa - 1].Disabled = true;
            registro.ListaClientes[registro.indexClienteNaPesquisa - 1].Id = 0;

            //mostra uma mensagem da operação bem sucedida
            MessageBox.Show("Registro excluído com sucesso.", "Dados excluídos", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Altera os controles necessários se algum dado em registros for alterado
        /// </summary>
        public static void DadoAlterado(RegistroClientes registro, RegistroClientesView regView)
        {
            //se o indice contador de registros de clientes é 0 (não há clientes na tela), nada é feito
            if (registro.indexClienteNaPesquisa == 0)
                return;

            //verifica se está em modo de navegação de pesquisa
            if (regView.btnOperacao.Text == "Atualizar")
            {
                //habilita o botão Atualizar e marca o registro como não salvo
                regView.btnOperacao.Enabled = true;
                registro.ListaClientes[registro.indexClienteNaPesquisa - 1].Saved = false;
            }
            //verifica se está em modo Inserir
            else if (regView.btnOperacao.Text == "Inserir")
            {
                //marca o registro a ser inserido como não salvo
                registro.ListaClientes[registro.indexClienteNaPesquisa - 1].Saved = false;
            }

            //alterações no modo pesquisa nao sao consideradas
        }

        /// <summary>
        /// Valida os dados inputados pelo usuário no modo inserir e no modo atualizar. Retorna false se houver dados inválidos, e true se todos os dados forem válidos
        /// </summary>
        public static bool ValidaDados(Cliente valCli, SqlConnection auxConn)
        {
            //valida a propriedade NOME
            if(valCli.Nome == "" || !valCli.Nome.All(char.IsLetter))
            {
                MessageBox.Show("\"Nome\" não pode conter espaços, números ou estar vazio.", "Campo inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //Valida a propriedade sobrenome
            if (valCli.Sobrenome == "" || !valCli.Nome.All(char.IsLetter))
            {
                MessageBox.Show("\"Nome\" não pode conter espaços, números ou estar vazio.", "Campo inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //com nome e sobrenome válidos, verifica se existe algum registro com ambos os campos idênticos
            SqlCommand valComm = new SqlCommand("SELECT * FROM Clientes WHERE nome='"+valCli.Nome+"' AND sobrenome='"+valCli.Sobrenome+"' AND IDCli!="+valCli.Id, auxConn);
            using(SqlDataReader auxReader = valComm.ExecuteReader()) 
            {
                //caso a query realizada retorne algum resultado, siginifica que já existe um registro com mesmo Nome e Sobrenome
                if (auxReader.HasRows)
                {
                    MessageBox.Show("Já existe um registro que contém mesmo \"Nome\" e \"Sobrenome\".", "Registro com identificação duplicada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            //Como a data é recebida como string, e a propriedade DataDeNascimento da classe Cliente é do tipo datetime, a validação é feita na classe RegistroClientesView
            //Valida apenas para checar se data não está vazia (não inicializada)
            DateTime dtAux = new DateTime();
            if(valCli.DataDeNascimento.CompareTo(dtAux) == 0)
            {
                MessageBox.Show("\"Data de nascimento\" não pode estar vazia.", "Campo inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            
            //valida a propriedade sexo
            if (valCli.Sexo == "")
            {
                MessageBox.Show("\"Sexo\" não pode estar vazio.", "Campo inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if(valCli.Sexo != "Masculino" && valCli.Sexo != "Feminino")
            {
                MessageBox.Show("\"Sexo\" inválido. Selecione uma opção disponível na lista.", "Campo inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //valida se CEP está vazio
            if(valCli.CEP.Trim(' ') == "-" || valCli.CEP.Trim(' ') == "")
            {
                MessageBox.Show("\"CEP\" não pode estar vazio.", "Campo inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Como numero do endereço é recebido como string e convertido para int a validação é feita na classe RegistroClientesView
            //Valida apenas para checar se Numero não está vazio(é igual a zero)
            if(valCli.Numero == 0)
            {
                MessageBox.Show("\"Número\" não pode estar vazio ou ser igual a zero.", "Campo inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //caso não ocorra nenhum erro, retorna true
            return true;
        }

        /// <summary>
        /// Valida o CEP na tabela Enderecos quando o campo CEP perde o foco no form
        /// </summary>
        public static bool ValidaCEP(RegistroClientes registro, RegistroClientesView regView)
        {
            //verifica se o CEP está vazio, e se estiver, apenas deixa os campos de endereço vazios
            if (regView.txtCEP.Text.Trim(' ') == "-" || regView.txtCEP.Text.Trim(' ') == "")
            {
                regView.txtLogradouro.Text = "";
                regView.txtBairro.Text = "";
                regView.txtCidade.Text = "";
                regView.cboxEstado.SelectedItem ="";
                return true;
            }

            //valida a propriedade CEP
            if (regView.txtCEP.Text.Length != 9)
            {
                MessageBox.Show("Formato de CEP inválido. O campo \"CEP\" deve conter 8 dígitos numéricos. \nEx.: \"18090-555\", \"17564-001\"...", "CEP inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                regView.txtLogradouro.Text = "";
                regView.txtBairro.Text = "";
                regView.txtCidade.Text = "";
                regView.cboxEstado.SelectedItem = "";
                return false;
            }

            //usa uma string auxiliar para retirar o hifen do CEP
            string auxCEP = regView.txtCEP.Text.Substring(0, 5) + regView.txtCEP.Text.Substring(6);

            //verifica se há somente números no CEP. Caso isso seja falso, mostra uma mensagem
            if (!auxCEP.All(char.IsDigit))
            {
                MessageBox.Show("Formato de CEP inválido. O campo \"CEP\" aceita apenas dígitos numéricos como caracters. \nEx.: \"18090-555\", \"17564-001\"...", "CEP inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                regView.txtLogradouro.Text = "";
                regView.txtBairro.Text = "";
                regView.txtCidade.Text = "";
                regView.cboxEstado.SelectedItem = "";
                return false;
            }

            SqlCommand valComm = new SqlCommand ("SELECT * FROM Enderecos WHERE CEP='" + auxCEP + "'",registro.Connection);
            using (SqlDataReader auxReader = valComm.ExecuteReader())
            {
                //caso a query realizada não retorne resultado, siginifica que o CEP não existe na tabela Enderecos
                if (!auxReader.HasRows)
                {
                    MessageBox.Show("CEP não existe.", "CEP inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    regView.txtLogradouro.Text = "";
                    regView.txtBairro.Text = "";
                    regView.txtCidade.Text = "";
                    regView.cboxEstado.SelectedItem = "";
                    return false;
                }
                //lê o endereço do CEP válido e o armazena no registro de Cliente exibido na tela, assim como altera os textos de endereço no form
                auxReader.Read();
                
                //passa o endereço para a tela
                regView.txtLogradouro.Text = auxReader.GetFieldValue<string>(1);
                regView.txtBairro.Text = auxReader.GetFieldValue<string>(2);
                regView.txtCidade.Text = auxReader.GetFieldValue<string>(3);
                regView.cboxEstado.SelectedItem = auxReader.GetFieldValue<string>(4);
            }

            return true;
        }

        /// <summary>
        /// Atualiza os estados presentes na tabela Enderecos para o combobox cboxEstado no form
        /// </summary>
        public static void BuscarEstados(RegistroClientes registro, RegistroClientesView regView)
        {

            //limpa os items na cboxEstado
            regView.cboxEstado.Items.Clear();

            //busca todos os Estados e os adiciona à cboxEstado de regView
            SqlCommand estComm = new SqlCommand("SELECT estado FROM Enderecos", registro.Connection);
            using (SqlDataReader auxReader = estComm.ExecuteReader())
            {
                regView.cboxEstado.Items.Add("");
                while (auxReader.Read())
                {
                    if (!regView.cboxEstado.Items.Contains(auxReader.GetValue(0)))
                        regView.cboxEstado.Items.Add(auxReader.GetValue(0));
                }
            }
        }

        /// <summary>
        /// Atualiza a label que mostra o indice de uma pesquisa realizada, caso haja resultados na pesquisa
        /// </summary>
        public static void AtualizarIndice(RegistroClientes registro, RegistroClientesView regView)
        {
            //verifica se registro contém pelo menos um indice para mostrar na pesquisa, e não faz nada caso não tenha
            if (registro.numClientesNaPesquisa == 0)
                return;

            regView.lblIndicePesquisa.Text = registro.indexClienteNaPesquisa + " de " + registro.numClientesNaPesquisa;
            regView.lblIndicePesquisa.Visible = true;
        }
    }
}
