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

            foreach (var valor in ofertas)
            {
                leilao.RecebeLance(fulano, valor);
            }

            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert - verificação dos critérios de aceitação (verifica se o Act foi bem sucedido)
            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);

        }
                                                                                                    
        [Fact]
        public void RetornaZeroDadoLeilaoSemLances()
        {
            //Arrange - cenário
            var leilao = new Leilao("Van Gogh");

            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert - verificação dos critérios de aceitação (verifica se o Act foi bem sucedido)
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }
    }
}
