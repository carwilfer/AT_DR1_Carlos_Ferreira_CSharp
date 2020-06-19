using System;
using System.Collections.Generic;
using ATCSharpAniversario.Dominio;


namespace ATCSharpAniversario.Dados
{
    public abstract class BancoDeDados
    {
        public abstract void Salvar(Pessoa pessoa);
        public abstract void Excluir(Pessoa pessoa);
        public abstract IEnumerable<Pessoa> BuscarTodosOsAniversariantes();
        public abstract IEnumerable<Pessoa> BuscarTodosOsAniversariantes(string nome);
        public abstract IEnumerable<Pessoa> BuscarTodosOsAniversariantes(DateTime dataNascimento);
        public abstract Pessoa BuscarAniversariantesPelo(string cpf);
        public abstract void Editar(Pessoa pessoa);
        /*
        public abstract void DeletaECria(List<Pessoa> listaPessoas);
        public abstract bool PessoaExistente(Pessoa pessoa);
        public abstract Pessoa BuscarPessoaPorId(int id);
        public abstract string RecebeArquivo();
        public abstract void CriarPessoa(Pessoa pessoa);
        */
    }
}
