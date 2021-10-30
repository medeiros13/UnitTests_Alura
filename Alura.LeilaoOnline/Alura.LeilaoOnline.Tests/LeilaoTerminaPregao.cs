using Alura.LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoTerminaPregao
    {

        [Theory]
        [InlineData(1200, new double[] { 800, 900, 1000, 1200 })] //Teste com os lances ordenados com valor
        [InlineData(1000, new double[] { 800, 900, 1000, 990 })] //Teste com os lances desordenados por valor
        [InlineData(800, new double[] { 800 })] //Teste com apenas um lance
        public void RetornaMaiorValorDadoLeilaoComPeloMenosUmLance(
            double valorEsperado,
            double[] ofertas)
        {
            //Arrange - cenário
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();
            for (int i = 0; i < ofertas.Length; i++)
            {
                var oferta = ofertas[i];

                if ((i % 2) == 0)
                {
                    leilao.RecebeLance(fulano, oferta);
                }
                else
                {
                    leilao.RecebeLance(maria, oferta);
                }
            }

            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert - verificação dos critérios de aceitação (verifica se o Act foi bem sucedido)
            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);

        }

        [Fact]
        public void LancaInvalidOperationExceptionDadoPregaoNaoIniciado()
        {
            //Arrange - cenário
            var leilao = new Leilao("Van Gogh");

            var excecaoObtida = Assert.Throws<InvalidOperationException>(
                //Act - método sob teste
                () => leilao.TerminaPregao()
            );

            var msgEsperada = "Não é possível terminar o pregão sem que ele tenha começado. Para isso, utilize o método IniciaPregao()";
            Assert.Equal(msgEsperada, excecaoObtida.Message);
        }

        [Fact]
        public void RetornaZeroDadoLeilaoSemLances()
        {
            //Arrange - cenário
            var leilao = new Leilao("Van Gogh");
            leilao.IniciaPregao();

            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert - verificação dos critérios de aceitação (verifica se o Act foi bem sucedido)
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }
    }
}
