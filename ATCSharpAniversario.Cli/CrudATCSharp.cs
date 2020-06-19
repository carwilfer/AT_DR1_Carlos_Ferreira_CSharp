using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATCSharpAniversario.Dados;
using ATCSharpAniversario.Dominio;

namespace ATCSharpAniversario.Cli
{
    class CrudATCSharp
    {
        
        
        public static void EscreverNaTela(string texto)
        {
            Console.WriteLine(texto);
        }
        public static void LimparTela()
        {
            Console.Clear();
        }

        private static string ListaDeAniversarianteHoje(Pessoa pessoa)
        {
            string retorno;
            DateTime aniversarioAnoCorrente = new DateTime(DateTime.Now.Year, pessoa.DataNascimento.Month, pessoa.DataNascimento.Day);
            if (aniversarioAnoCorrente.Date > DateTime.Now.Date)
            {
                TimeSpan ts = aniversarioAnoCorrente.Date - DateTime.Now.Date;
                retorno = string.Format("Faltam {0} dias para seu aniversário.", ts.Days);
            }
            else
            {
                DateTime aniversarioProximoAno = new DateTime(DateTime.Now.Year + 1, pessoa.DataNascimento.Month, pessoa.DataNascimento.Day);
                TimeSpan ts = aniversarioProximoAno.Date - DateTime.Now.Date;
                retorno = string.Format("Faltam {0} dias para seu aniversário.", ts.Days);
            }

            return retorno;
        }
        public static void MenuPrincipal()
        {
            //ListaDeAniversarianteHoje();

            Console.ForegroundColor = ConsoleColor.Red;

            ExibirAniversariantesDoDia();

            EscreverNaTela("Menu do sistema de gerência de Aniversários:");
            EscreverNaTela("Selecione uma operação");
            EscreverNaTela("1 - Adicionar pessoa");
            EscreverNaTela("2 - Pesquisar pessoa");
            EscreverNaTela("3 - Editar pessoa");
            EscreverNaTela("4 - Operação excluir pessoa");
            EscreverNaTela("5 - Sair");

            char operacao = Console.ReadLine().ToCharArray()[0];

            switch (operacao)
            {
                case '1':
                    OperacaoAdicionarPessoa(); break;

                case '2':
                    OperacaoPesquisarPessoa(); break;

                case '3':
                    OperacaoEditarPessoa(); break;

                case '4':
                    OperacaoExcluirPessoa(); break;

                default:
                    EscreverNaTela("Opção inexistente"); 
                    
                    break;
            }
            EscreverNaTela("Até a próxima");
            EscreverNaTela("Pressione qualquer tecla para continuar");
            Console.ReadKey();
            LimparTela();
        }
        private static void OperacaoAdicionarPessoa()
        {
            LimparTela();

            EscreverNaTela("Entre com o Nome:");
            string nome = Console.ReadLine();

            EscreverNaTela("Entre com o Sobrenome:");
            string sobreNome = Console.ReadLine();

            EscreverNaTela("Entre com o CPF:");
            string cpf = Console.ReadLine();

            DateTime niver;
            EscreverNaTela("Entre com a data de Nascimento no formato: DD/MM/YYYY");
            niver = DateTime.Parse(Console.ReadLine());
            DateTime dataDeCadastro = DateTime.Now;
            TimeSpan resultado;
            resultado = dataDeCadastro - niver;

            //var pessoas = Dados.BuscarPessoas();

            var pessoa = new Pessoa();
            pessoa.Nome = nome;
            pessoa.SobreNome = sobreNome;
            pessoa.Cpf = cpf;
            pessoa.DataNascimento = niver;
            pessoa.DataDeCadastro = dataDeCadastro;

            /*
            foreach (var pessoa in pessoas)
            {
                Pessoa ultimo = pessoas.Last(x => x.Id == pessoa.Id);
                pessoa.Id = ultimo.Id + 1;
            }
            */

            bdPolimorfismo.Salvar(pessoa);

            EscreverNaTela("Cadastrado com sucesso!");
            EscreverNaTela("Pressione qualquer tecla para continuar");
            Console.ReadKey();
            LimparTela();

            MenuPrincipal();
        }
        private static void OperacaoPesquisarPessoa()
        {
            LimparTela();

            Consultas();
            StatusAniversariante();
        }
        private static void OperacaoEditarPessoa()
        {
            LimparTela();

            EscreverNaTela("Entre com o CPF do Aniversariuante: ");
            string cpf = Console.ReadLine();

            var gente = bdPolimorfismo.BuscarAniversariantesPelo(cpf);
            if (gente == null)
            {
                EscreverNaTela("Cpf digitando incorretamente ou funcionario não encontrado");
                EscreverNaTela("Pressione qualquer tecla para continuar");
                Console.ReadKey();
                LimparTela();
                MenuPrincipal();
            }

            EscreverNaTela("Favor alterar o nome: ");
            string nomeAlterado = Console.ReadLine();
            gente.Nome = nomeAlterado;

            bdPolimorfismo.Editar(gente);

            MenuPrincipal();
        }
        private static void OperacaoExcluirPessoa()
        {
            LimparTela();

            EscreverNaTela("Entre com o CPF do Aniversariuante: ");
            string cpf = Console.ReadLine();

            var gente = bdPolimorfismo.BuscarAniversariantesPelo(cpf);
            if (gente == null)
            {
                EscreverNaTela("Cpf digitando incorretamente ou funcionario não encontrado");
                EscreverNaTela("Pressione qualquer tecla para continuar");
                Console.ReadKey();
                LimparTela();
                MenuPrincipal();
            }
            bdPolimorfismo.Excluir(gente);

            MenuPrincipal();
        }

        static void Consultas()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            EscreverNaTela("Opções de consultas: ");
            EscreverNaTela("1 - Consulta pelo nome:  ");
            EscreverNaTela("2 - Consulta pela data de aniversario: ");
            EscreverNaTela("3 - Consultar todos: ");

            string consultandoPessoas = Console.ReadLine();

            switch (consultandoPessoas)
            {
                case "1":
                    ConsultarPeloNome(); break;

                case "2":
                    ConsultarPelaDataNascimento(); break;

                case "3":
                    ExibirTodosOsAniversariantes(); break;
                default: EscreverNaTela("Consulta incorreta, tente novamente mais tarde! ");
                    break;
            }
            MenuPrincipal();
        }

        private static void ConsultarPeloNome()
        {
            EscreverNaTela("Digite o nome ou sobrenome que quer buscar: ");
            string nome = Console.ReadLine();

            var aniversarianteEncontrado = bdPolimorfismo
                .BuscarTodosOsAniversariantes(nome);
                
            int quantidadeDeAniversarianteEncontrado = aniversarianteEncontrado.Count();

            if (aniversarianteEncontrado.Count() > 0)
            {
                EscreverNaTela("Aniversariantes Encontrados");
                foreach (var gente in aniversarianteEncontrado)
                {
                    EscreverNaTela(gente.Nome + " " + gente.SobreNome + " - " + ListaDeAniversarianteHoje(gente));
                }
                MenuPrincipal();
            }
            else
            {
                EscreverNaTela("Nenhum Aniversariante encontrado com o nome informado: " + nome);
            }
            StatusAniversariante();
            MenuPrincipal();
        }

        private static void ConsultarPelaDataNascimento()
        {
            EscreverNaTela("Entre com a data de Nascimento: ");
            var dataNascimento = DateTime.Parse(Console.ReadLine());

            var aniversarianteFiltradosPelaDataNascimento = bdPolimorfismo.BuscarTodosOsAniversariantes(dataNascimento);
                
            EscreverNaTela("Aniversariantes Encontrados");
            foreach (var gente in aniversarianteFiltradosPelaDataNascimento)
            {
                EscreverNaTela(gente.Nome);
            }
            StatusAniversariante();
            MenuPrincipal();
        }

        private static void ExibirAniversariantesDoDia()
        {
            var aniversarianteFiltradosPelaDataNascimento = bdPolimorfismo.BuscarTodosOsAniversariantes(DateTime.Now.Date);

            if (aniversarianteFiltradosPelaDataNascimento.Count() > 0)
            {
                EscreverNaTela("Feliz aniversário!");
                foreach (var gente in aniversarianteFiltradosPelaDataNascimento)
                {
                    EscreverNaTela(gente.Nome);
                }
            }
        }
        private static void ExibirTodosOsAniversariantes()
        {
            foreach (var gente in bdPolimorfismo.BuscarTodosOsAniversariantes())
            {
                EscreverNaTela($" Nome: {gente.Nome} " +
                    $"Sobrenome: {gente.SobreNome} " +
                    $"Cpf: {gente.Cpf} " +
                    $"DataNascimento: {gente.DataNascimento}");
            }
            EscreverNaTela("Pressione qualquer tecla para continuar");
            Console.ReadKey();
            LimparTela();
            MenuPrincipal();
        }

        static void StatusAniversariante()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Os dados cadastrados estão corretos? ");
            Console.WriteLine("1 - Operação consultar pessoa mais nova");
            Console.WriteLine("2 - Operação consultar pessoa com mesmo nome");
            Console.WriteLine("3 - Operação pesquisar quantidades de pessoa");
            Console.WriteLine("4 - Consultar pela data");

            string tipoConsulta = Console.ReadLine();
            switch (tipoConsulta)
            {
                case "1":
                    OperacaoPesquisarPessoaMaisNova(); break;

                case "2":
                    OperacaoPesquisarPessoaComMesmoNome(); break;

                case "3":
                    OperacaoPesquisarQuantidadesDePessoa(); break;

                case "4":
                    ConsultarPelaData(); break;

                default:
                    Console.WriteLine("Consulta incorreta");
                    MenuPrincipal();
                    break;
            }
        }

        private static void OperacaoPesquisarPessoaMaisNova()
        {
            throw new NotImplementedException();
        }

        private static void OperacaoPesquisarPessoaComMesmoNome()
        {
            throw new NotImplementedException();
        }

        private static void OperacaoPesquisarQuantidadesDePessoa()
        {
            throw new NotImplementedException();
        }

        private static void ConsultarPelaData()
        {
            throw new NotImplementedException();
        }

        public static BancoDeDados bdPolimorfismo
        {
            get
            {
                return new BancoDeDadosDeArquivos();
            }
        }

     }
}
