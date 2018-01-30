using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExercícioClientes.Model
{
    public class Cliente
    {
        //propriedades de cliente
        private int _id;
        private string _nome;
        private string _sobrenome;
        private DateTime _dataDeNascimento = new DateTime();
        private string _logradouro;
        private int _numero;
        private string _complemento;
        private string _bairro;
        private string _cidade;
        private string _cEP;
        private string _sexo;
        private string _estado;
        private bool _saved = true;
        private bool _disabled = false;

        //encapsulamento
        public int Id { get => _id; set => _id = value; }
        public string Nome { get => _nome; set => _nome = value; }
        public string Sobrenome { get => _sobrenome; set => _sobrenome = value; }
        public DateTime DataDeNascimento { get => _dataDeNascimento; set => _dataDeNascimento = value; }
        public string Logradouro { get => _logradouro; set => _logradouro = value; }
        public int Numero { get => _numero; set => _numero = value; }
        public string Complemento { get => _complemento; set => _complemento = value; }
        public string Bairro { get => _bairro; set => _bairro = value; }
        public string Cidade { get => _cidade; set => _cidade = value; }
        public string CEP { get => _cEP; set => _cEP = value; }
        public string Sexo { get => _sexo; set => _sexo = value; }
        public string Estado { get => _estado; set => _estado = value; }
        public bool Saved { get => _saved; set => _saved = value; }
        public bool Disabled { get => _disabled; set => _disabled = value; }
    }
}
