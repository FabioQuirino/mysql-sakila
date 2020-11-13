using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Globalization;

namespace sakila.planilha
{
    public class Celula
    {
        private static CultureInfo cultura = new CultureInfo("en-gb");
        private Celula() { }
        public Celula(object conteudo)
        {
            Conteudo = conteudo;
        }

        public object Conteudo { get; }
        public static string ReferenciaCelula(int linha, int coluna)
        {
            return $"{DeParaColuna(coluna)}{linha}";
        }

        public static Cell GerarCelula(int linha, int coluna, object conteudo)
        {
            var celula = new Cell
            {
                CellReference = $"{DeParaColuna(coluna)}{linha}",
            };

            if (conteudo != null)
            {
                if (conteudo is short || conteudo is int)
                {
                    celula.CellValue = new CellValue(Int32Value.FromInt32(Convert.ToInt32(conteudo)));
                    celula.DataType = new EnumValue<CellValues>(CellValues.Number);
                }
                else if (conteudo is DateTime)
                {
                    celula.CellValue = new CellValue(Convert.ToDateTime(conteudo).ToOADate().ToString(cultura));
                    celula.DataType = new EnumValue<CellValues>(CellValues.Number);
                } 
                else if (conteudo is double || conteudo is decimal)
                {
                    celula.CellValue = new CellValue(DoubleValue.FromDouble(Convert.ToDouble(conteudo, cultura)));
                    celula.DataType = new EnumValue<CellValues>(CellValues.Number);
                }
                else
                {
                    celula.CellValue = new CellValue(conteudo.ToString());
                    celula.DataType = new EnumValue<CellValues>(CellValues.String);
                }
            }
            else
            {
                celula.CellValue = new CellValue();
            }

            return celula;
        }

        private static string DeParaColuna(int coluna)
        {
            var columnName = string.Empty;

            while (coluna > 0)
            {
                var modulo = (coluna - 1) % 26;
                columnName = Convert.ToChar(65 + modulo) + columnName;
                coluna = (coluna - modulo) / 26;
            }

            return columnName;
        }
    }
}
