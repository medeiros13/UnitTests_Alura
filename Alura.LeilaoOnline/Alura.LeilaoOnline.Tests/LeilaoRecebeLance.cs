using Alura.LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoRecebeLance
    {
        [Fact]
        public void NaoAceitaProximoLanceDadoMesmoClienteRealizouUltimoLance()
        {
            //Arrange - cenário
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            leilao.IniciaPregao();
            leilao.RecebeLance(fulano, 800);

            //Act - método sob teste
            leilao.RecebeLance(fulano, 1000);

            //Assert - verificação dos critérios de aceitação (verifica se o Act foi bem sucedido)
            var quantidadeObtida = leilao.Lances.Count();
            var quantidadeEsperada = 1;
            Assert.Equal(quantidadeObtida, quantidadeEsperada);
        }
        [Theory]
        [InlineData(4, new double[] { 1000, 1200, 1400, 1300 })]
        [InlineData(2, new double[] { 800, 900 })]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(
            int quantidadeEsperada, double[] ofertas)
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

            leilao.TerminaPregao();

            //Act - método sob teste
            leilao.RecebeLance(fulano, 1000);

            //Assert - verificação dos critérios de aceitação (verifica se o Act foi bem sucedido)
            var quantidadeObtida = quantidadeEsperada;
            var valorObtido = leilao.Lances.Count();
            Assert.Equal(quantidadeObtida, valorObtido);
        }
    }
}
