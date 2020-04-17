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

        public static string ReferenciaCelula(int j, int k)
        {
            return $"{DeParaColuna(k)}{j}";
        }

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

        private static string DeParaColuna(int dividend)
        {
            var columnName = string.Empty;

            while (dividend > 0)
            {
                var modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo) + columnName;
                dividend = (dividend - modulo) / 26;
            }

            return columnName;
        }

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
                            string referencia = ReferenciaCelula(linha, coluna);
                            object conteudo = celula.Conteudo;

                            Cell cell = new Cell()
                            {
                                CellReference = new StringValue(referencia),
                                DataType = CellValues.String
                            };

                            try
                            {
                                if (conteudo != null)
                                {
                                    cell.CellValue = new CellValue($"{conteudo.ToString()}");
                                }
                                else
                                {
                                    cell.CellValue = new CellValue();
                                }
                            }
                            catch (Exception erro)
                            {
                                cell.CellValue = new CellValue($"{erro.Message}");
                            }

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
