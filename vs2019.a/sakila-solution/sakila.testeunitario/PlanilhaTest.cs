using Microsoft.VisualStudio.TestTools.UnitTesting;
using sakila.planilha;
using sakila.repositorio.servico;
using System;

namespace sakila.testeunitario
{
    [TestClass]
    public class PlanilhaTest
    {
        [TestMethod]
        public void GerarPlanilha()
        {
            var actors = new ServicoActor().Listar();
            var planilha = new Planilha();
            var arquivo = planilha.Arquivo;
            planilha.GerarArquivo(@"d:\temp", $"teste-{DateTime.Now:yyyyMMddHHmmss}.xlsx");
            Assert.IsTrue(arquivo != null, "Os dados não foram gerados.");
        }

        [TestMethod]
        public void CalcularReferenciaCelula()
        {
            var planilha = new Planilha();
            var referencia = planilha.ReferenciaCelula(1, 1);

            Assert.IsTrue(referencia == "A1", "Erro ao gerar referencia célula");
        }
    }
}
