using Microsoft.VisualStudio.TestTools.UnitTesting;
using sakila.repositorio.servico;

namespace sakila.testeunitario
{
    [TestClass]
    public class SakilaTest
    {
        [TestMethod]
        public void TestarListarAtores()
        {
            var actors = new ServicoActor().Listar();
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestarListarFilmes()
        {
            var films = new ServicoFilm().Listar();
            Assert.IsTrue(true);
        }
    }
}
