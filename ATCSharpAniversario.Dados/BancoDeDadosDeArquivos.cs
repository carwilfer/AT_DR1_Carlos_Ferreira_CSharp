using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ATCSharpAniversario.Dominio;
using System.Linq;

namespace ATCSharpAniversario.Dados
{
    public class BancoDeDadosDeArquivos : BancoDeDados
    {
        private static string ObterNomeArquivo()
        {
            var pastaDesktop = Environment.SpecialFolder.Desktop;
            string localDaPastaDesktop = Environment.GetFolderPath(pastaDesktop);
            string nomeDoArquivo = @"\dadosDosAniversariantes.txt";
            return localDaPastaDesktop + nomeDoArquivo;
        }
        public override void Salvar(Pessoa pessoa)
        {
            bool aniversarianteJaExiste = false;
            //foreach (var aniversarianteNaLista in pessoasCadastrada)
            // {
            //     if (aniversarianteNaLista == pessoa)
            //     {
            //         aniversarianteJaExiste = true;
            //         break;
            //     }
            // }
            if (aniversarianteJaExiste == false)
            {
                //pessoasCadastrada.Add(pessoa);

                //string nome = pessoa.Nome;
                //string sobreNome = pessoa.SobreNome;
                //string cpf = pessoa.Cpf;
                //string dataNascimento = pessoa.DataNascimento.ToString();

                string nomeDoArquivo = ObterNomeArquivo();

                string formato = $"{pessoa.Cpf}, {pessoa.Nome}, " +
                    $"{pessoa.SobreNome}, {pessoa.DataNascimento.ToString()};";

                File.AppendAllText(nomeDoArquivo, formato);
            }
        }
        public override IEnumerable<Pessoa> BuscarTodosOsAniversariantes()
        {
            string nomeDoArquivo = ObterNomeArquivo();

            string resultado = File.ReadAllText(nomeDoArquivo);

            string[] pessoaArray = resultado.Split(';');

            List<Pessoa> pessoaList = new List<Pessoa>();

            for (int i = 0; i < pessoaArray.Length - 1; i++)
            {
                string[] dadosDoAniversariante = pessoaArray[i].Split(',');
                string cpf = dadosDoAniversariante[0];
                string nome = dadosDoAniversariante[1];
                string sobreNome = dadosDoAniversariante[2];
                DateTime dataNascimento = Convert.ToDateTime(dadosDoAniversariante[3]);

                Pessoa pessoa = new Pessoa(nome, sobreNome, cpf, dataNascimento);
                pessoaList.Add(pessoa);
            }
            return pessoaList;
        }
        public override void Excluir(Pessoa pessoa)
        {

        }
        public override IEnumerable<Pessoa> BuscarTodosOsAniversariantes(string nome)
        {
            var todasPessoas = BuscarTodosOsAniversariantes();

            List<Pessoa> pessoasEncontradas = new List<Pessoa>();
            foreach (var pessoa in todasPessoas)
            {
                if ((pessoa.Nome + pessoa.SobreNome).ToLower().Contains(nome.ToLower()))
                {
                    pessoasEncontradas.Add(pessoa);
                }
            }

            return pessoasEncontradas;
        }
        public override IEnumerable<Pessoa> BuscarTodosOsAniversariantes(DateTime dataNascimento)
        {
            var todasPessoas = BuscarTodosOsAniversariantes();
            List<Pessoa> pessoasEncontradas = new List<Pessoa>();
            return todasPessoas.ToList()
                .Where(
                    gente => gente.DataNascimento.Day == dataNascimento.Day
                    && gente.DataNascimento.Month == dataNascimento.Month
                );
        }
        public override Pessoa BuscarAniversariantesPelo(string cpf)
        {
            var todasPessoas = BuscarTodosOsAniversariantes();

            List<Pessoa> pessoasEncontradas = new List<Pessoa>();
            foreach (var pessoa in todasPessoas)
            {
                if (pessoa.Cpf == cpf)
                {
                    return pessoa;
                }
            }

            return null;
        }

        public override void Editar(Pessoa pessoaNova)
        {
            var todasPessoas = BuscarTodosOsAniversariantes();
            List<Pessoa> listaNova = new List<Pessoa>();

            foreach (var pessoa in todasPessoas)
            {
                if (pessoaNova.Cpf == pessoa.Cpf)
                {
                    listaNova.Add(pessoaNova);
                } else
                {
                    listaNova.Add(pessoa);
                }
            }

            RecriarArquivo(listaNova);
        }

        public void RecriarArquivo(List<Pessoa> listaNova)
        {
            string nomeDoArquivo = ObterNomeArquivo();
            File.Delete(nomeDoArquivo);
            FileStream arquivo;
            if (!File.Exists(nomeDoArquivo))
            {
                arquivo = File.Create(nomeDoArquivo);
                arquivo.Close();
            }

            foreach (var pessoa in listaNova)
            {
                Salvar(pessoa);
            }
        }
    }
}
