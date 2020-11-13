using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sakila.model;
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

        [TestMethod]
        public void TestarIncluirFilme()
        {
            var filme = new film()
            {
                title = "O PISTOLEIRO SEM DEDO",
                language_id = 1
            };

            new ServicoFilm().Incluir(filme);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestarIncluirFilmeAtor()
        {
            var filme = new film()
            {
                title = "A VOLTA DOS QUE N�O FORAM",
                language_id = 1
            };

            var ator = new actor()
            {
                first_name = "FABIO",
                last_name = "QUIRINO",
                last_update = DateTime.Now
            };

            var film_actor = new film_actor()
            {
                actor = ator,
                film = filme,
                last_update = DateTime.Now
            };

            filme.films_actors = new List<film_actor>(){ film_actor };

            new ServicoFilm().Incluir(filme);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestarObterAtor()
        {
            actor ator = new ServicoActor().ObterPorSobrenome("QUIRINO");
            Assert.IsTrue(ator != null && ator.last_name == "QUIRINO");
        }
    }
}
