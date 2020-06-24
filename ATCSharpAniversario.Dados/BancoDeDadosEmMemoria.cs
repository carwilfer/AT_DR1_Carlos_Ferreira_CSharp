using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using ATCSharpAniversario.Dominio;


namespace ATCSharpAniversario.Dados
{
    public class BancoDeDadosEmMemoria : BancoDeDados
    {
        private static List<Pessoa> pessoasCadastrada = new List<Pessoa>();

        public override void Salvar(Pessoa pessoa)
        {
            //var pessoasEncontrada = pessoasCadastrada.Find(x => x == pessoa);

            bool aniversarianteJaExiste = false;
            foreach (var aniversarianteNaLista in pessoasCadastrada)
            {
                if (aniversarianteNaLista == pessoa)
                {
                    aniversarianteJaExiste = true;
                }
            }
            if (aniversarianteJaExiste == false)
            {
                pessoasCadastrada.Add(pessoa);
            }
        }

        public override IEnumerable<Pessoa> BuscarTodosOsAniversariantes()
        {
            return pessoasCadastrada;
        }
        public override IEnumerable<Pessoa> BuscarTodosOsAniversariantes(string nome)
        {
            return pessoasCadastrada
                .Where(gente => gente.Nome.Contains(nome, StringComparison.InvariantCultureIgnoreCase));
        }
        public override IEnumerable<Pessoa> BuscarTodosOsAniversariantes(DateTime dataNascimento)
        {
            return pessoasCadastrada
                .Where(gente => gente.DataNascimento.Date == dataNascimento);
        }
        //public static IEnumerable<Pessoa> BuscarTodosOsAniversariantes(DateTime dataDeCadastro)
        //{
        //return pessoasCadastrada
        //.Where(gente => gente.DataDeCadastro.Date == dataDeCadastro);
        //}

        public override Pessoa BuscarAniversariantesPelo(string cpf)
        {
            return pessoasCadastrada.Find(gente => gente.Cpf == cpf);
        }
        public override void Excluir(Pessoa pessoa)
        {
            pessoasCadastrada.Remove(pessoa);
        }
        public override void Editar(Pessoa pessoa)
        {
            throw new NotImplementedException();
        }
    }
}
