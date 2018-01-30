using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ExercícioClientes.Model
{
    public class RegistroClientes
    {
        private SqlConnection _connection;
        public List<Cliente> ListaClientes = new List<Cliente>();
        
        //variáveis auxiliares para os métodos de RegistroClientes
        private bool _ultimaPesquisa;
        private int _indexClienteNaPesquisa = 0;
        private int _numClientesNaPesquisa = 0;
        public Dictionary<string, string> dicPropriedades = new Dictionary<string, string>();

        //encapsulamento
        public bool UltimaPesquisa { get => _ultimaPesquisa; set => _ultimaPesquisa = value; }
        public int numClientesNaPesquisa { get => _numClientesNaPesquisa; set => _numClientesNaPesquisa = value; }
        public int indexClienteNaPesquisa { get => _indexClienteNaPesquisa; set => _indexClienteNaPesquisa = value; }
        public SqlConnection Connection { get => _connection; set => _connection = value; }

        /// <summary>
        /// Verifica quais campos o usuário preencheu para pesquisa, busca no banco de dados usando esses campos como critério de busca e retorna os resultados como objetos Cliente em ListaClientes
        /// </summary>
        public void Pesquisar()
        {
            //veririca se existe apenas 1 item dentro de ListaClientes (para pesquisa)
            //Caso haja mais de 1, a pesquisa não é realizada
            if (ListaClientes.Count != 1)
            {
                //valida a ultima pesquisa como falha
                UltimaPesquisa = false;
                MessageBox.Show("Ocorreu um erro ao realizar a pesquisa: não é possível pesquisar múltiplos items ao mesmo tempo.", "Erro ao pesquisar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //armazena as propriedades da única instância Cliente(com os dados editáveis de busca) em ListaClientes (somente um registro é buscado por vez) como strings para comparação
            dicPropriedades.Clear();
            dicPropriedades.Add("nome", ListaClientes[0].Nome);
            dicPropriedades.Add("sobrenome", ListaClientes[0].Sobrenome);
            DateTime dtAux = new DateTime();
            if (ListaClientes[0].DataDeNascimento.CompareTo(dtAux) > 0)
                dicPropriedades.Add("dataNasc", ListaClientes[0].DataDeNascimento.ToShortDateString());
            dicPropriedades.Add("sexo", ListaClientes[0].Sexo);
            dicPropriedades.Add("CEP", ListaClientes[0].CEP);
            if (ListaClientes[0].Numero != 0)
                dicPropriedades.Add("endNum", ListaClientes[0].Numero.ToString());
            dicPropriedades.Add("endComplemento", ListaClientes[0].Complemento);


            //cria um novo SqlCommand para fazer a query de busca na View ClientesComEndereço, usando a propriedade conexão da classe RegistroClientes
            SqlCommand searchComm = new SqlCommand();
            searchComm.Connection = Connection;
            searchComm.CommandText = @"SELECT * FROM ClientesComEndereço";

            //caso haja alguma propriedade de cliente que não é null ou empty (ou seja, há critério de busca), é adicionada a cláusula WHERE à query
            if (dicPropriedades.Values.Any(p => p != null && p != ""))
            {
                searchComm.CommandText += @" WHERE ";
                int iaux = 0;
                //após a cláusula WHERE ser adicionada, é verificado quais propriedades serão usadas como critério de busca e as adiciona à query
                foreach (KeyValuePair<string, string> p in dicPropriedades)
                {
                    if (p.Value != null && p.Value != "")
                    {
                        if (iaux != 0)
                            searchComm.CommandText += @"AND ";
                        if (p.Key == "endNum")
                            searchComm.CommandText += p.Key + "=" + p.Value + @" ";
                        else if (p.Key == "nome" || p.Key == "sobrenome")
                            searchComm.CommandText += p.Key + " LIKE " + @"'%" + p.Value + @"%'" + @" ";
                        else
                            searchComm.CommandText += p.Key + "=" + @"'" + p.Value + @"'" + @" ";
                        iaux++;
                    }
                }
            }

            //usa um SQLReader para receber o resultado da query de busca
            using (SqlDataReader queryReader = searchComm.ExecuteReader())
            {
                //limpa ListaClientes para armazenar os resultados da busca como instancias de Cliente
                ListaClientes.Clear();
                //lê cada uma das linhas do result da query
                while (queryReader.Read())
                {
                    //armazena os valores das colunas, em ordem, na array auxiliar rowValues
                    object[] rowValues = new object[12];
                    queryReader.GetValues(rowValues);

                    //cria uma nova instancia de Cliente, e armazena os valores da query result, em ordem, como propriedades, fazendo as conversões necessárias
                    Cliente auxCliente = new Cliente();
                    auxCliente.Id = (int)rowValues[0];
                    auxCliente.Nome = rowValues[1].ToString();
                    auxCliente.Sobrenome = rowValues[2].ToString();
                    auxCliente.DataDeNascimento = DateTime.Parse(rowValues[3].ToString());
                    auxCliente.Sexo = rowValues[4].ToString();
                    auxCliente.CEP = rowValues[5].ToString();
                    auxCliente.Logradouro = rowValues[6].ToString();
                    auxCliente.Numero = (int)rowValues[7];
                    auxCliente.Complemento = rowValues[8].ToString();
                    auxCliente.Bairro = rowValues[9].ToString();
                    auxCliente.Cidade = rowValues[10].ToString();
                    auxCliente.Estado = rowValues[11].ToString();

                    //adiciona cada linha da result query à ListaClientes, como instancias de Cliente
                    ListaClientes.Add(auxCliente);
                }
            }

            //armazena a quantidade de resultados da busca
            numClientesNaPesquisa = ListaClientes.Count;

            //mostra quantos resultados foram encontrados na busca
            if (numClientesNaPesquisa == 0)
            {
                //valida a ultima pesquisa como falha
                UltimaPesquisa = false;
                MessageBox.Show("Não foi encontrado nenhum resultado com os critérios definidos.", "Resultado da pesquisa", MessageBoxButtons.OK);
                return;
            }
            else if (numClientesNaPesquisa == 1)
                MessageBox.Show("Foi encontrado 1 resultado para a pesquisa", "Resultado da pesquisa.", MessageBoxButtons.OK);
            else
                MessageBox.Show("Foram encontrados " + numClientesNaPesquisa + " resultados para a pesquisa.", "Resultado da pesquisa", MessageBoxButtons.OK);

            //valida a ultima pesquisa realizada como bem sucedida
            UltimaPesquisa = true;
        }

        /// <summary>
        /// Atualiza um registro de cliente na tabela Clientes
        /// </summary>
        public void Atualizar()
        {
            //cria um novo SqlCommand para fazer o update através da stored procedure UpdateCliente, usando a propriedade conexão da classe
            SqlCommand updateComm = new SqlCommand("UpdateCliente", Connection);
            updateComm.CommandType = System.Data.CommandType.StoredProcedure;

            //atribui as propriedades de Cliente em ListaClientes, usando como controlador de indice a variavel indexClienteNaPesquisa
            updateComm.Parameters.AddWithValue("@ID", ListaClientes[indexClienteNaPesquisa-1].Id);
            updateComm.Parameters.AddWithValue("@nome", ListaClientes[indexClienteNaPesquisa-1].Nome);
            updateComm.Parameters.AddWithValue("@sobrenome", ListaClientes[indexClienteNaPesquisa-1].Sobrenome);
            updateComm.Parameters.AddWithValue("@dataNasc", ListaClientes[indexClienteNaPesquisa-1].DataDeNascimento);
            if(ListaClientes[indexClienteNaPesquisa - 1].Sexo == "Masculino")
                updateComm.Parameters.AddWithValue("@sexo", 'M');
            else if (ListaClientes[indexClienteNaPesquisa - 1].Sexo == "Feminino")
                updateComm.Parameters.AddWithValue("@sexo", 'F');

            //cria uma variavel auxiliar para passar para a query o CEP sem hífen
            string auxCEP = ListaClientes[indexClienteNaPesquisa - 1].CEP.Substring(0, 5) + ListaClientes[indexClienteNaPesquisa - 1].CEP.Substring(6);
            updateComm.Parameters.AddWithValue("@endCEP", auxCEP);
            updateComm.Parameters.AddWithValue("@endNum", ListaClientes[indexClienteNaPesquisa-1].Numero);
            updateComm.Parameters.AddWithValue("@endComplemento", ListaClientes[indexClienteNaPesquisa-1].Complemento);

            //executa o comando update
            updateComm.ExecuteNonQuery();
        }

        /// <summary>
        /// Insere um registro de cliente na tabela Clientes
        /// </summary>
        public void Inserir()
        {
            //verifica se existe apenas 1 item dentro de ListaClientes (para ser inserido)
            //Caso haja mais de 1, a insersão não é realizada
            if (ListaClientes.Count != 1)
                return;

            //cria um novo SqlCommand para fazer o insert, usando a propriedade conexão da classe
            SqlCommand inserirComm = new SqlCommand();
            inserirComm.Connection = Connection;
            inserirComm.CommandText = @"INSERT INTO Clientes VALUES
                                        (@IDCli,
                                        @nome,
                                        @sobrenome,
                                        @datadenasc,
                                        @sexo,
                                        @endCEP,
                                        @endNumero,
                                        @endComplemento)";

            //busca o maior ID na tabela Clientes, e incrementa em 1 para o ID do novo registro do campo de dados
            SqlCommand maxID = new SqlCommand("SELECT MAX(IDCli) FROM Clientes", _connection);
            using (SqlDataReader idReader = maxID.ExecuteReader())
            {
                idReader.Read();
                inserirComm.Parameters.AddWithValue("@IDCli", idReader.GetFieldValue<int>(0) + 1);
            }

            //atribui as propriedades da primeira ocorrencia de Cliente em ListaClientes. 
            //Como somente um registro é inserido por vez, nesse caso haverá somente uma ocorrencia em ListaClientes
            inserirComm.Parameters.AddWithValue("@nome", ListaClientes[0].Nome);
            inserirComm.Parameters.AddWithValue("@sobrenome", ListaClientes[0].Sobrenome);
            inserirComm.Parameters.AddWithValue("@datadenasc", ListaClientes[0].DataDeNascimento);
            //converte sexo de string para char
            if (ListaClientes[0].Sexo == "Masculino")
                inserirComm.Parameters.AddWithValue("@sexo", 'M');
            else if (ListaClientes[0].Sexo == "Feminino")
                inserirComm.Parameters.AddWithValue("@sexo", 'F');
            //retira o hifen do CEP
            string auxCEP = ListaClientes[0].CEP.Substring(0, 5) + ListaClientes[0].CEP.Substring(6);
            inserirComm.Parameters.AddWithValue("@endCEP", auxCEP);
            inserirComm.Parameters.AddWithValue("@endNumero", ListaClientes[0].Numero);
            inserirComm.Parameters.AddWithValue("@endComplemento", ListaClientes[0].Complemento);

            //executa o comando insert
            inserirComm.ExecuteNonQuery();
        }

        /// <summary>
        /// Exclui um registro de cliente na tabela Clientes
        /// </summary>
        public void Excluir()
        {
            //verifica se existe pelo menos 1 item dentro de ListaClientes (para ser excluído)
            //Caso não haja, a exclusão não é realizada
            if (ListaClientes.Count < 1)
                return;

            //cria um novo SqlCommand para fazer o delete, usando a propriedade conexão da classe
            SqlCommand deleteComm = new SqlCommand();
            deleteComm.Connection = Connection;
            deleteComm.CommandText = @"DELETE FROM Clientes WHERE IDCli = @id";

            //atribui à propriedade @id o ID do registro 
            deleteComm.Parameters.AddWithValue("@id", ListaClientes[indexClienteNaPesquisa-1].Id);

            //executa o comando delete
            deleteComm.ExecuteNonQuery();
        }

        /// <summary>
        /// Construtor público para criar uma instancia de RegistroClientes com acesso ao banco de dados
        /// </summary>
        /// <param name="connection">Parametro do tipo SqlConnection</param>
        public RegistroClientes(SqlConnection connection)
        {
            Connection = connection;
        }
    }
}
