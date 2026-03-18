using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parte1
{
    public class Jogador
    {
        public string Nome { get; private set; }
        public int Vitorias { get; set; }
        public int Derrotas { get; set; }
        public int Empates { get; set; }

        public Jogador(string nome)
        {
            Nome = nome;
        }

        public char ObterOpcao()
        {
            Console.WriteLine($"{Nome}, faça sua escolha:");
            return ValidarEntrada(new char[] { '0', '1', '2' });
        }

        public static char ValidarEntrada(char[] opcoesValidas)
        {
            char entrada;

            do
            {
                entrada = Console.ReadKey().KeyChar;

                if (!Array.Exists(opcoesValidas, x => x == entrada))
                {
                    Console.WriteLine("\nOpção inválida. Tente novamente:");
                }

            } while (!Array.Exists(opcoesValidas, x => x == entrada));

            return entrada;
        }
    }
}
