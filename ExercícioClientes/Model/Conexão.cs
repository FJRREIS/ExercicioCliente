using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace ExercícioClientes.Model
{
    public class Conexão
    {
        private string _path = null;
        private string _fileType;
        private Hashtable _htConexão = new Hashtable();
        private SqlConnection _connection = new SqlConnection();

        public SqlConnection Connection { get => _connection;  }

        //Seta as Keys possíveis para comparação com a hastable
        private enum Tags { SV, BD, US, SN };

        /// <summary>
        /// Conecta ao banco de dados
        /// </summary>
        public void Conectar()
        {
            //Caso não tenha sido atribuído um caminho, ou caso não haja valor atríbuído à hashtable (caso de input errado no arquivo), não é feita tentativa de conexão
            if (_path == null || _htConexão.Count == 0)
                return;
            
            //array auxiliar para armazenar os dados da conexão extraídos da hashtable _htConexão
            string[] connData = new string[4];
            
            //extrai os valores da hashtable _htConexão, uma key em cada iteração
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    //armazena na variável auxiliar tag o valor da tag armazenado na enum Tags
                    string tag = ((Tags)i).ToString();

                    //Unboxing do valor da hashtable em uma lista auxiliar
                    List<char> auxChar = (List<char>)_htConexão[tag];

                    //joga uma exceção caso a Key não exista na hashtable ou caso seu valor seja nulo
                    if (!_htConexão.ContainsKey(tag) || auxChar.Count == 0)
                    {
                        throw new System.ArgumentException("Erro ao criar conexão. O Arquivo de conexão contém tags faltando ou nulas.");
                    }

                    //atribui à array auxiliar o valor correspondente à key [tag] em _htConexão
                    foreach (char c in auxChar)
                    {
                        connData[i] += c;
                    }
                }
            }
            catch (Exception e)
            {
                //mostra o erro pego, e sai do método sem tentar fazer a conexão
                MessageBox.Show(e.Message, "Erro.");
                return;
            }

            //cria a connection string e a seta para a SqlConnection _connection;
            Connection.ConnectionString = "Data Source=" + connData[0] + ";Initial Catalog=" + connData[1] + ";User Id=" + connData[2] + ";Password=" + connData[3];

            //tenta abrir conexão usando os dados recuperados da hastable
            try
            {
                Connection.Open();
                MessageBox.Show("Conexão com o banco de dados realizada com sucesso!\n \nServidor: " + connData[0] + "\nBanco de dados: " + connData[1] + "\nUsuário: "+ connData[2], "Conexão bem sucedida", MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show("Não foi possível se conectar ao banco de dados: " + e.Message, "Erro.");
            }
        }

        /// <summary>
        /// Fecha a conexão com o banco de dados
        /// </summary>
        public void FecharConexão()
        {
            Connection.Close();
        }

        /// <summary>
        /// Metódo privado para fazer validações na criação de instância
        /// </summary>
        /// <param name="_path"></param>
        /// <param name="_fileType"></param>
        /// <returns></returns>
        private static Conexão CriarInstancia(string filePath, string fileType)
        {
            //variaveis auxiliares para armazenar os valores das tags de conexão e valor de retorno do método
            string value = null;
            List<char> valueChars = new List<char>();
            Conexão aux = new Conexão();

            if (fileType == ".txt")
            {
                using (StreamReader SR = File.OpenText(filePath))
                {
                    //variaveis auxiliares
                    string line;
                    string tag = null;
                    string key = null;

                    //intera cada linha do texto contido no arquivo de conexão
                    while ((line = SR.ReadLine()) != null)
                    {
                        line.TrimEnd(' ');
                        //busca os primeiros caracteres '[' e ']' e atribui à variável 'tag' o valor entre eles em uppercase
                        //busca o primeiro caracter ':' e atribui o que vier depois, desconsiderando espaços e branco à direita, à variável 'value'
                        //caso esses caracteres não sejam encontrados, ocorre um erro IndexOutOfRange e a execução vai para a próxima linha do texto
                        //atribui cada valor de 'value' à um item da List<char> 'valueChars'
                        try
                        {
                            tag = line.Substring(line.IndexOf('['), line.IndexOf(']') + 1).ToUpper();
                            value = line.Substring(line.IndexOf(':'));
                            //formata 'value' para retirar ':' e os espaços após ':'
                            value = value.Substring(1).TrimStart(' ');
                            valueChars.AddRange(value);
                        }
                        catch
                        {
                            continue;
                        }
                        //se nenhum erro ocorrer, significa que o formato tag/valor estava correto no texto, e é atribuido um valor à variável 'key'
                        //      para ser utilizado na hashtable, de acordo com o valor armazenado em 'tag'
                        //caso o nome da tag não esteja correto ou não referencie um valor da conexão, 'key' permanece null
                        switch (tag)
                        {
                            case "[SERVIDOR]":
                                key = "SV";
                                break;
                            case "[BD]":
                                key = "BD";
                                break;
                            case "[USUARIO]":
                            case "[USUÁRIO]":
                                key = "US";
                                break;
                            case "[SENHA]":
                                key = "SN";
                                break;
                            default:
                                key = null;
                                break;
                        }
                        try
                        {
                            //verifica se 'key' é null: caso seja, significa que o nome da tag não está correto ou não referencia um valor da conexão
                            //      e nesse caso nenhum código é executado e é a próxima linha é lida
                            if (key != null)
                            {
                                //instancia um novo objeto auxiliar List<char> e o inicia com os valores de 'valueChar' que será acessível apenas pela hashtable
                                //adiciona um par KeyValue(string, List<char>) à Hashtable '_htConexão'
                                List<char> auxChar = new List<char>(valueChars);
                                aux._htConexão.Add(key, auxChar);
                            }
                        }
                        //Caso haja tentativa de adicionar à hastable uma key já existente, ocorrerá uma exceção
                        catch
                        {
                            //Mostra a mensagem de erro da exceção
                            MessageBox.Show("Arquivo de conexão contém tags duplicadas.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return null;
                        }
                        //limpa os valores de 'valueChars' para a próxima linha lida
                        valueChars.Clear();
                    }
                }
            }

            else if (fileType == ".xml")
            {
                //carrega o arquivo XML em uma instancia XmlDocument xmlDoc
                XmlDocument xmlDoc = new XmlDocument();
                using (StreamReader SR = File.OpenText(filePath))
                {
                    xmlDoc.Load(SR);
                }

                //busca as tags de conexão dentro de xmlDoc, fazendo uma iteração para cada tag
                for (int i = 0; i < 4; i++)
                {
                    try
                    {
                        //varifica em cada iteração se há tags duplicadas, e joga uma exceção caso haja
                        Exception e = new System.InvalidOperationException("Arquivo de conexão contém tags duplicadas.");
                        switch (i)
                        {
                            case 0:
                                if (xmlDoc.GetElementsByTagName("servidor").Count > 1)
                                    throw e;
                                value = xmlDoc.GetElementsByTagName("servidor")[0].InnerText;
                                break;
                            case 1:
                                if (xmlDoc.GetElementsByTagName("bd").Count > 1)
                                    throw e;
                                value = xmlDoc.GetElementsByTagName("bd")[0].InnerText;
                                break;
                            case 2:
                                if (xmlDoc.GetElementsByTagName("usuario").Count > 1)
                                    throw e;
                                value = xmlDoc.GetElementsByTagName("usuario")[0].InnerText;
                                break;
                            case 3:
                                if (xmlDoc.GetElementsByTagName("senha").Count > 1)
                                    throw e;
                                value = xmlDoc.GetElementsByTagName("senha")[0].InnerText;
                                break;
                        }
                        //adiciona os caracteres de value como itens de valueChars, ou não adiciona nada, caso value seja null
                        valueChars.AddRange(value);
                    }
                    catch (InvalidOperationException e)
                    {
                        //Mostra a mensagem de erro da exceção
                        MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                    catch
                    {
                        //caso não exista tag name com os nomes especificados nos cases, ocorerrá um erro ao tentar acessar o elemento não-existente no XML
                        //nesse caso, nada é atribuído à value e, consequentemente, valueChars é vazio
                    }

                    //instancia um novo objeto auxiliar List<char> e o inicia com os valores de valueChar que será acessível apenas pela hashtable
                    //adiciona um par KeyValue(string, List<char>) à Hashtable _htConexão, sendo a Key um valor contido na enum Tags correspondente à iteração
                    List<char> auxChar = new List<char>(valueChars);
                    aux._htConexão.Add(((Tags)i).ToString(), auxChar);


                    //limpa valueChars e value para a próxima iteração
                    valueChars.Clear();
                    value = null;
                }

            }

            else
            {
                MessageBox.Show("Extensão inválida. Somente arquivos *.txt e *.xml são aceitos como dados de conexão.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            return aux;
        }

        /// <summary>
        ///Construtor da classe que recebe como parâmetro o path do arquivo com as informações da conexão
        /// </summary>
        /// <param name="filePath">String contendo caminho do arquivo .txt ou .xml</param>
        public Conexão(string filePath)
        {
            //Verifica se o arquivo existe, case não exista, não atribui nenhum valor às propriedades internas
            if (File.Exists(filePath))
            {
                //armazena os valores de path e extensão
                _path = filePath;
                _fileType = Path.GetExtension(filePath);

                //chama o método interno para recuperar os dados do arquivo espeficicado em filePath e os armazena na hashtable _htConexão, caso não haja nenhum erro
                Conexão aux = null;
                aux = CriarInstancia(_path, _fileType);
                if (aux != null)
                    _htConexão = aux._htConexão;
            }
        }

        /// <summary>
        /// Construtor padrao, para instanciação somente nos métodos privados
        /// </summary>
        private Conexão()
        {

        }
    }
}