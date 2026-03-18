using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parte1
{
    public class Jogo
    {
        private List<Jogador> jogadores = new List<Jogador>();
        private Jogador jogadorAtual;
        private Random random = new Random();

        public void Iniciar()
        {
            Console.WriteLine("Olá! Vamos jogar Jokempo?");
            CriarOuSelecionarJogador();

            char opcaoMenu;

            do
            {
                opcaoMenu = ExibirMenuPrincipal();

                switch (opcaoMenu)
                {
                    case '1':
                        JogarRodada();
                        break;

                    case '2':
                        ExibirEstatisticasTodos();
                        break;

                    case '3':
                        CriarOuSelecionarJogador();
                        break;

                    case '0':
                        Console.WriteLine("Saindo do jogo...");
                        break;
                }

            } while (opcaoMenu != '0');

            Console.WriteLine("Tchau! Até a próxima.");
        }

        private char ExibirMenuPrincipal()
        {
            Console.WriteLine("\n=== MENU ===");
            Console.WriteLine("1 - Jogar");
            Console.WriteLine("2 - Ver estatísticas");
            Console.WriteLine("3 - Trocar jogador");
            Console.WriteLine("0 - Sair");

            return Jogador.ValidarEntrada(new char[] { '0', '1', '2', '3' });
        }

        private void CriarOuSelecionarJogador()
        {
            Console.Write("\nDigite o nome do jogador: ");
            string nome = Console.ReadLine();

            jogadorAtual = jogadores.Find(j => j.Nome == nome);

            if (jogadorAtual == null)
            {
                jogadorAtual = new Jogador(nome);
                jogadores.Add(jogadorAtual);
                Console.WriteLine("Novo jogador criado.");
            }
            else
            {
                Console.WriteLine("Jogador existente selecionado.");
            }
        }

        private void JogarRodada()
        {
            ExibirMenuJogada();

            char opcao = jogadorAtual.ObterOpcao();
            int opcaoPC = random.Next(3);

            bool vitoria = VerificarVitoria(opcao, opcaoPC);

            ExibirEscolhas(opcao, opcaoPC);
            ExibirResultado(opcao, opcaoPC, vitoria);
            AtualizarEstatisticas(opcao, opcaoPC, vitoria);
        }

        private void ExibirMenuJogada()
        {
            Console.WriteLine("\nEscolha uma opção:");
            Console.WriteLine("0 - Pedra");
            Console.WriteLine("1 - Papel");
            Console.WriteLine("2 - Tesoura");
        }

        private bool VerificarVitoria(char opcao, int opcaoPC)
        {
            return (opcao == '0' && opcaoPC == 2) ||
                   (opcao == '1' && opcaoPC == 0) ||
                   (opcao == '2' && opcaoPC == 1);
        }

        private void ExibirEscolhas(char opcao, int opcaoPC)
        {
            Console.WriteLine($"\nVocê escolheu: {ConverterOpcao(opcao)}");
            Console.WriteLine($"Computador escolheu: {ConverterOpcao(opcaoPC)}");
        }

        private void ExibirResultado(char opcao, int opcaoPC, bool vitoria)
        {
            if ((int)char.GetNumericValue(opcao) == opcaoPC)
            {
                Console.WriteLine("Empate!");
            }
            else if (vitoria)
            {
                Console.WriteLine("Parabéns! Você venceu.");
            }
            else
            {
                Console.WriteLine("Computador venceu.");
            }
        }

        private void AtualizarEstatisticas(char opcao, int opcaoPC, bool vitoria)
        {
            if ((int)char.GetNumericValue(opcao) == opcaoPC)
                jogadorAtual.Empates++;
            else if (vitoria)
                jogadorAtual.Vitorias++;
            else
                jogadorAtual.Derrotas++;
        }

        private void ExibirEstatisticasTodos()
        {
            Console.WriteLine("\n=== ESTATÍSTICAS ===");

            if (jogadores.Count == 0)
            {
                Console.WriteLine("Nenhum jogador registrado.");
                return;
            }

            foreach (var jogador in jogadores)
            {
                Console.WriteLine("\n----------------------");
                Console.WriteLine($"Nome: {jogador.Nome}");
                Console.WriteLine($"Vitórias: {jogador.Vitorias}");
                Console.WriteLine($"Derrotas: {jogador.Derrotas}");
                Console.WriteLine($"Empates: {jogador.Empates}");
            }

            Console.WriteLine("\n----------------------");
        }

        private string ConverterOpcao(int opcao)
        {
            return opcao switch
            {
                0 => "Pedra",
                1 => "Papel",
                2 => "Tesoura",
                _ => "Desconhecido"
            };
        }

        private string ConverterOpcao(char opcao)
        {
            return ConverterOpcao((int)char.GetNumericValue(opcao));
        }
    }
}
