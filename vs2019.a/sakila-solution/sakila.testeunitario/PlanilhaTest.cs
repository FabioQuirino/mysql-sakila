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
            var filmes = new ServicoFilm().Listar().ToList();

            var aba1 = new Aba("filmes");
            aba1.AdicionarDados("film_id", "title", "description", "release_year", "language_id", "original_language_id", "rental_duration", "rental_rate", "length", "replacement_cost", "rating", "special_features", "last_update");
            filmes.ForEach(f =>
            {
                aba1.AdicionarDados(f.film_id, f.title, f.description, f.release_year, f.language_id, f.original_language_id, f.rental_duration, f.rental_rate, f.length, f.replacement_cost, f.rating, f.special_features, f.last_update);
            });

            var aba2 = new Aba("fabio");
            aba2.AdicionarDados("film_id", "title", "description", "release_year", "language_id", "original_language_id", "rental_duration", "rental_rate", "length", "replacement_cost", "rating", "special_features", "last_update");
            filmes.ForEach(f =>
            {
                aba2.AdicionarDados(f.film_id, f.title, f.description, f.release_year, f.language_id, f.original_language_id, f.rental_duration, f.rental_rate, f.length, f.replacement_cost, f.rating, f.special_features, f.last_update);
            });

            var abas = new Aba[] {
                aba1, aba2
            };

            var planilha = new Planilha(abas);
            var arquivo = planilha.Arquivo;
            planilha.GerarArquivo(@"d:\temp", $"teste-{DateTime.Now:yyyyMMddHHmmss}.xlsx");
            Assert.IsTrue(arquivo != null, "Os dados não foram gerados.");
        }

        [TestMethod]
        public void CalcularReferenciaCelula()
        {
            var referencia = Celula.ReferenciaCelula(3, 1);

            Assert.IsTrue(referencia == "A3", "Erro ao gerar referencia célula");
        }
    }
}
