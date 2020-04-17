namespace sakila.planilha
{
    public class Celula
    {
        private Celula() { }
        public Celula(object conteudo)
        {
            Conteudo = conteudo;
        }

        public object Conteudo { get; }
    }
}
