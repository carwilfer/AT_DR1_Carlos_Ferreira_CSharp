using System;
using System.Collections.Generic;
using System.Text;

namespace ATCSharpAniversario.Dominio
{
    public class Pessoa
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataDeCadastro { get; set; }

        public Pessoa()
        {
            DataNascimento = DateTime.Now;
        }

        public Pessoa(string nome, string sobreNome)
        {
            Nome = nome;
            SobreNome = sobreNome;
        }

        public Pessoa(string nome, string sobreNome, string cpf, DateTime dataNascimento)
        {
            Nome = nome;
            SobreNome = sobreNome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
        }

        public string NomeCompleto()
        {
            return $"{Nome} {SobreNome}";
        }

    }
}
