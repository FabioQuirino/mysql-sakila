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
        }
    }
}
