using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.IO;

namespace sakila.planilha
{
    public class Planilha
    {
        public Planilha()
        {

        }

        public string ReferenciaCelula(int j, int k)
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

        private string DeParaColuna(int dividend)
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

                for (int i = 1; i < 4; i++)
                {
                    WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                    SheetData sheetData = new SheetData();
                    worksheetPart.Worksheet = new Worksheet(sheetData);

                    Sheet sheet = new Sheet()
                    {
                        Id = spreadsheetDocument.WorkbookPart.
                        GetIdOfPart(worksheetPart),
                        SheetId = (uint)i,
                        Name = $"mySheet-{i}"
                    };

                    for (int j = 1; j < 5; j++)
                    {
                        Row row = new Row()
                        {
                            RowIndex = (uint)j
                        };

                        for (int k = 1; k < 8; k++)
                        {
                            string celula = ReferenciaCelula(j, k);

                            Cell cell = new Cell()
                            {
                                CellReference = new StringValue(celula),
                                DataType = CellValues.String,
                                CellValue = new CellValue($"conteudo-{celula}-{(i * j * k).ToString()}")
                            };

                            row.AppendChild(cell);
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
