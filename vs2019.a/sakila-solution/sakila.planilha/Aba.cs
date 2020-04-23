using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
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

        private static int InsertSharedStringItem(object conteudo, SharedStringTablePart shareStringPart)
        {
            string text = conteudo == null ? string.Empty : conteudo.ToString();

            // If the part does not contain a SharedStringTable, create one.
            if (shareStringPart.SharedStringTable == null)
            {
                shareStringPart.SharedStringTable = new SharedStringTable();
            }

            int i = 0;

            // Iterate through all the items in the SharedStringTable. If the text already exists, return its index.
            foreach (SharedStringItem item in shareStringPart.SharedStringTable.Elements<SharedStringItem>())
            {
                if (item.InnerText == text)
                {
                    return i;
                }

                i++;
            }

            // The text does not exist in the part. Create the SharedStringItem and return its index.
            shareStringPart.SharedStringTable.AppendChild(new SharedStringItem(new DocumentFormat.OpenXml.Spreadsheet.Text(text)));
            shareStringPart.SharedStringTable.Save();

            return i;
        }

        public static Sheet GerarPlanilha(SpreadsheetDocument spreadsheetDocument, WorkbookPart workbookpart, Aba aba)
        {
            WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            SheetData sheetData = new SheetData();
            worksheetPart.Worksheet = new Worksheet(sheetData);
            var tot = spreadsheetDocument.WorkbookPart.Workbook.Sheets.Count();
            Sheet sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                SheetId = (uint)(tot + 1),
                Name = aba.Nome
            };

            foreach (var elemento in aba.Dados)
            {
                var linha = elemento.Key;
                Row row = new Row()
                {
                    RowIndex = (uint)linha
                };

                var coluna = 1;
                foreach (var celula in elemento.Value)
                {
                    object conteudo = celula.Conteudo;
                    var cell = Celula.GerarCelula(linha, coluna, conteudo);
                    row.AppendChild(cell);
                    coluna++;
                }

                sheetData.AppendChild(row);
            }

            return sheet;
        }
    }
}
