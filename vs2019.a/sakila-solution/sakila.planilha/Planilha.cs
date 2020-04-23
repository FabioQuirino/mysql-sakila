using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.IO;
using System.Linq;

namespace sakila.planilha
{
    public class Planilha
    {
        private Planilha()
        {
        }

        public Planilha(Aba aba)
        {
            Abas = new Aba[] { aba };
        }

        public Planilha(Aba[] abas)
        {
            if (abas.Select(x => x.Nome).Distinct().Count() != abas.Count())
            {
                throw new Exception("Os nomes das abas não podem ser repetidos.");
            }

            Abas = abas;
        }

        private Aba[] Abas { get; }

        public void GerarArquivo(string caminho, string arquivo)
        {
            File.WriteAllBytes($@"{caminho}\{arquivo}", Arquivo);
        }

        public byte[] Arquivo
        {
            get
            {
                if (arquivo == null)
                {
                    arquivo = GerarPlanilha();
                }

                return arquivo;
            }
        }
        private byte[] arquivo;

        private byte[] GerarPlanilha()
        {
            var fileArray = new MemoryStream();

            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(fileArray, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                for (int i = 0; i < Abas.Length; i++)
                {
                    WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                    SheetData sheetData = new SheetData();
                    worksheetPart.Worksheet = new Worksheet(sheetData);

                    Sheet sheet = new Sheet()
                    {
                        Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                        SheetId = (uint)(i + 1),
                        Name = Abas[i].Nome
                    };

                    foreach (var elemento in Abas[i].Dados)
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

                    sheets.Append(sheet);
                }

                workbookpart.Workbook.Save();
                spreadsheetDocument.Close();
            }

            return fileArray.ToArray();
        }
    }
}
