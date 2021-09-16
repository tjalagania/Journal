using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
namespace Journal.src
{
    public class CreateEXCEL
    {
        public string FilePath
        {
            get;set;
        }
        public string[] Headers { get; set; }
        public ObservableCollection<JournalItem>Objects { get; set; }
        public CreateEXCEL(string flpath,ObservableCollection<JournalItem>objects)
        {
            FilePath = flpath;
            Headers = new[] {"გატარების თარიღი","ავტორი","სახელი","ადრესატი","კოლეგია","შენიშვნა" };
            Objects = objects;
            CreateExcel();
        }
        private void CreateExcel()
        {
            SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.
             Create(FilePath, SpreadsheetDocumentType.Workbook);

            // Add a WorkbookPart to the document.
            WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            // Add a WorksheetPart to the WorkbookPart.
            WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());
            

            // Add Sheets to the Workbook.
            Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.
                AppendChild<Sheets>(new Sheets());

            // Append a new worksheet and associate it with the workbook.
            Sheet sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.
                GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = "Sheet 1"
            };
            sheets.Append(sheet);

            Row row = new Row();
            
            SheetData sheetdata = worksheetPart.Worksheet.GetFirstChild<SheetData>();
            
            sheetdata.Append(row);
            foreach(string s in Headers)
            {
                Cell cell = new Cell
                {
                    CellValue = new CellValue(s),
                    DataType = CellValues.String,
                   
                };
                
                row.Append(cell);
            }
            foreach(var i in Objects)
            {
                Row rw = new Row();
                sheetdata.Append(rw);

                JournalItem jr = (JournalItem)i;

                Cell date = new Cell
                {
                    CellValue = new CellValue(jr.DateOfRec.ToString()),
                    DataType = CellValues.String,
                    
                    
                };
                Cell author = new Cell
                {
                    CellValue = new CellValue(jr.Author),
                    DataType = CellValues.String
                };
                Cell name = new Cell
                {
                    CellValue = new CellValue(jr.Name),
                    DataType = CellValues.String
                };
                Cell Adresses = new Cell
                {
                    CellValue = new CellValue(jr.OwnAdressee.Name),
                    DataType = CellValues.String
                };
                Cell ownboard = new Cell
                {
                    CellValue = new CellValue(jr.OwnBoard.Name),
                    DataType = CellValues.String
                };
                Cell note = new Cell
                {
                    CellValue = new CellValue(jr.Note),
                    DataType = CellValues.String
                };
                rw.Append(date);
                rw.Append(author);
                rw.Append(name);
                rw.Append(Adresses);
                rw.Append(ownboard);
                rw.Append(note);
                
            }
            
            workbookpart.Workbook.Save();

            // Close the document.
            spreadsheetDocument.Close();
        }
    }
}
