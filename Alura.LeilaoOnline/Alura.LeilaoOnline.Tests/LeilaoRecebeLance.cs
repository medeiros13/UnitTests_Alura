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
        [Theory]
        [InlineData(4, new double[] { 1000, 1200, 1400, 1300 })]
        [InlineData(2, new double[] { 800, 900 })]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(
            int quantidadeEsperada, double[] ofertas)
        {
            //Arrange - cenário
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);

            leilao.IniciaPregao();

            foreach (var oferta in ofertas)
            {
                leilao.RecebeLance(fulano, oferta);
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
