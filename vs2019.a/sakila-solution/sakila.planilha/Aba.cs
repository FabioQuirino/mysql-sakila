using System;
using System.Collections.Generic;
using System.Linq;

namespace sakila.planilha
{
    public class Aba
    {
        private Aba()
        {
        }

        public Aba(string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new Exception("É necessário definir um nome para a aba");
            }

            Nome = nome;
            dados = new Dictionary<int, Celula[]>();
        }

        public string Nome { get; }

        private Dictionary<int, Celula[]> dados;

        public Dictionary<int, Celula[]> Dados
        {
            get
            {
                return dados;
            }
        }

        public void AdicionarDados(params Celula[] lista)
        {
            var indice = Dados.Any() ? Dados.Max(x => x.Key) : 0;
            Dados.Add(indice + 1, lista.Select(x => x).ToArray());
        }

        public void AdicionarDados(params object[] lista)
        {
            AdicionarDados(lista.Select(x => new Celula(x)).ToArray());
        }

        //public void AdicionarDados(Celula celula)
        //{
        //    AdicionarDados(new Celula[] { celula });
        //}

        //public void AdicionarDados(object conteudo)
        //{
        //    AdicionarDados(new Celula[] { new Celula(conteudo) });
        //}

        //public void AdicionarDados(object[] celulas)
        //{
        //    AdicionarDados(celulas.Select(x => new Celula(x)).ToArray());
        //}

        //public void AdicionarDados(Celula[] celulas)
        //{
        //    var indice = dados.Max(x => x.Key);
        //    dados.Add(indice + 1, celulas);
        //}
    }
}
