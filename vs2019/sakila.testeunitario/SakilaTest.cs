using Microsoft.VisualStudio.TestTools.UnitTesting;
using sakila.repositorio.servico;

namespace sakila.testeunitario
{
    [TestClass]
    public class SakilaTest
    {
        [TestMethod]
        public void TestarAcessoBd()
        {
            var teste = new servicoActor().Listar();
            Assert.IsTrue(teste.Count > 1000, $"erro de teste. Encontrados: {teste.Count}");
        }
    }
}
