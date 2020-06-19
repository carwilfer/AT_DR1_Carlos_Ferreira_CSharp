using System;
using System.Collections.Generic;
using ATCSharpAniversario.Dominio;


namespace ATCSharpAniversario.Dados
{
    public class BancoDeDadosSql : BancoDeDados
    {
        public override Pessoa BuscarAniversariantesPelo(string cpf)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Pessoa> BuscarTodosOsAniversariantes()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Pessoa> BuscarTodosOsAniversariantes(string nome)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Pessoa> BuscarTodosOsAniversariantes(DateTime dataNascimento)
        {
            throw new NotImplementedException();
        }

        public override void Excluir(Pessoa pessoa)
        {
            throw new NotImplementedException();
        }

        public override void Salvar(Pessoa pessoa)
        {
            throw new NotImplementedException();
        }

        public override void Editar(Pessoa pessoa)
        {
            throw new NotImplementedException();
        }
    }
}
