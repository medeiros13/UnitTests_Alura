using Alura.LeilaoOnline.Core;
using System;

namespace Alura.LeilaoOnline.ConsoleApp
{
    class Program
    {
        private static void Verifica(double valorEsperado, double valorObtido)
        {
            if (valorObtido == valorEsperado)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("TESTE OK");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"TESTE FALHOU! Valor Esperado: {valorEsperado}, Valor obtido: {valorObtido}");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void LeilaoComVariosLances()
        {
            //Arrange - cenário
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.RecebeLance(fulano, 800);
            leilao.RecebeLance(maria, 900);
            leilao.RecebeLance(fulano, 1000);
            leilao.RecebeLance(maria, 990);

            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert - verificação dos critérios de aceitação (verifica se o Act foi bem sucedido)
            var valorEsperado = 1000;
            var valorObtido = leilao.Ganhador.Valor;

            Console.ForegroundColor = ConsoleColor.White;

            Verifica(valorEsperado, valorObtido);

        }
        private static void LeilaoComApenasUmLance()
        {
            //Arrange - cenário
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);

            leilao.RecebeLance(fulano, 800);

            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert - verificação dos critérios de aceitação (verifica se o Act foi bem sucedido)
            var valorEsperado = 800;
            var valorObtido = leilao.Ganhador.Valor;

            Verifica(valorEsperado, valorObtido);
        }
        static void Main(string[] args)
        {
            LeilaoComVariosLances();
            LeilaoComApenasUmLance();
        }
    }
}
