using Microsoft.VisualStudio.TestTools.UnitTesting;
using sakila.planilha;
using sakila.repositorio.servico;
using System;
using System.Collections.Generic;
using System.Linq;

namespace sakila.testeunitario
{
    [TestClass]
    public class PlanilhaTest
    {
        [TestMethod]
        public void GerarPlanilha()
        {
            var actors = new ServicoActor().Listar();

            int? intNull = null;
            decimal? decimalNull = null;
            DateTime? dateNull = null;

            var aba1 = new Aba("fabio");
            aba1.AdicionarDados(1, intNull, decimalNull, dateNull);
            aba1.AdicionarDados(1, "quirino", DateTime.Now, decimal.Zero);

            var aba2 = new Aba("atores");
            actors.ForEach(x => { aba2.AdicionarDados(x.actor_id, x.first_name, x.last_name, x.last_update); });

            var abas = new Aba[] {
                aba1,
                aba2
            };

            var planilha = new Planilha(abas);
            var arquivo = planilha.Arquivo;
            planilha.GerarArquivo(@"d:\temp", $"teste-{DateTime.Now:yyyyMMddHHmmss}.xlsx");
            Assert.IsTrue(arquivo != null, "Os dados não foram gerados.");
        }

        [TestMethod]
        public void CalcularReferenciaCelula()
        {
            var referencia = Planilha.ReferenciaCelula(3, 1);

            Assert.IsTrue(referencia == "A3", "Erro ao gerar referencia célula");
        }
    }
}
