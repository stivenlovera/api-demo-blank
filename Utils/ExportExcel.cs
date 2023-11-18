using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using GrapeCity.Documents.Excel;

namespace api_guardian.Utils
{
    public class ExportExcel
    {
        private readonly ILogger<ExportExcel> logger;

        public ExportExcel(ILogger<ExportExcel> logger)
        {
            this.logger = logger;
        }
        public Workbook ConvertExcel()
        {
            this.GetBidTracker();
            return new Workbook();
        }

        private Workbook GetBidTracker()
        {
            Workbook workbook = new Workbook();
            IWorksheet worksheet = workbook.Worksheets[0];

            //***********************Set RowHeight & ColumnWidth***************
            worksheet.StandardHeight = 30;
            worksheet.Range["1:1"].RowHeight = 57.75;
            worksheet.Range["2:9"].RowHeight = 30;
            worksheet.Range["A:A"].ColumnWidth = 2.71;
            worksheet.Range["B:B"].ColumnWidth = 11.71;
            worksheet.Range["C:C"].ColumnWidth = 28;
            worksheet.Range["D:D"].ColumnWidth = 22.425;
            worksheet.Range["E:E"].ColumnWidth = 16.71;
            worksheet.Range["F:F"].ColumnWidth = 28;
            worksheet.Range["G:H"].ColumnWidth = 16.71;
            worksheet.Range["I:I"].ColumnWidth = 2.71;

            //**************************Set Table Value & Formulas*********************
            ITable table = worksheet.Tables.Add(worksheet.Range["B2:H9"], true);
            worksheet.Range["B2:H9"].Value = new object[,]
            {
            { "BID #", "DESCRIPTION", "DATE RECEIVED", "AMOUNT", "PERCENT COMPLETE", "DEADLINE", "DAYS LEFT" },
            { 1, "New Emergency care facility", null, 2000, 0.5, null, null },
            { 2, "Service Contract Extension", null, 3500, 0.75, null, null },
            { 3, "Preventive Maintenance Agreement", null, 5000, 0.8, null, null },
            { 4, "Full restoration contract", null, 4000, 0.2, null, null },
            { 5, "Hampton Inn, Burlington", null, 4000, 1.00, null , null },
            { 6, "New invitation to bid", null, 1500, 0.0, null , null },
            { 7, "Children's hospital - new admin building", null, 5000, 0.65, null, null },
            };
            worksheet.Range["B1"].Value = "Bid Details";
            worksheet.Range["D3"].Formula = "=TODAY()-10";
            worksheet.Range["D4:D5"].Formula = "=TODAY()-20";
            worksheet.Range["D6"].Formula = "=TODAY()-10";
            worksheet.Range["D7"].Formula = "=TODAY()-28";
            worksheet.Range["D8"].Formula = "=TODAY()-17";
            worksheet.Range["D9"].Formula = "=TODAY()-15";
            worksheet.Range["G3:G9"].Formula = "=[@[DATE RECEIVED]]+30";
            worksheet.Range["H3:H9"].Formula = "=[@DEADLINE]-TODAY()";

            //****************************Set Table Style********************************

            //****************************Set Table Style********************************
            ITableStyle tableStyle = workbook.TableStyles.Add("Bid Tracker");
            workbook.DefaultTableStyle = "Bid Tracker";

            var wholeTableStyle = tableStyle.TableStyleElements[TableStyleElementType.WholeTable];

            //Set WholeTable element style.
            wholeTableStyle.Font.Color = Color.FromArgb(89, 89, 89);
            wholeTableStyle.Borders.Color = Color.FromArgb(89, 89, 89);
            wholeTableStyle.Borders[BordersIndex.EdgeLeft].LineStyle = BorderLineStyle.Thin;
            wholeTableStyle.Borders[BordersIndex.EdgeRight].LineStyle = BorderLineStyle.Thin;
            wholeTableStyle.Borders[BordersIndex.EdgeTop].LineStyle = BorderLineStyle.Thin;
            wholeTableStyle.Borders[BordersIndex.EdgeBottom].LineStyle = BorderLineStyle.Thin;
            wholeTableStyle.Borders[BordersIndex.InsideVertical].LineStyle = BorderLineStyle.Thin;
            wholeTableStyle.Borders[BordersIndex.InsideHorizontal].LineStyle = BorderLineStyle.Thin;

            var headerRowStyle = tableStyle.TableStyleElements[TableStyleElementType.HeaderRow];

            //Set HeaderRow element style.
            headerRowStyle.Borders.Color = Color.FromArgb(89, 89, 89);
            headerRowStyle.Borders[BordersIndex.EdgeLeft].LineStyle = BorderLineStyle.Thin;
            headerRowStyle.Borders[BordersIndex.EdgeRight].LineStyle = BorderLineStyle.Thin;
            headerRowStyle.Borders[BordersIndex.EdgeTop].LineStyle = BorderLineStyle.Thin;
            headerRowStyle.Borders[BordersIndex.EdgeBottom].LineStyle = BorderLineStyle.Thin;
            headerRowStyle.Borders[BordersIndex.InsideVertical].LineStyle = BorderLineStyle.Thin;
            headerRowStyle.Borders[BordersIndex.InsideHorizontal].LineStyle = BorderLineStyle.Thin;
            headerRowStyle.Interior.Color = Color.FromArgb(131, 95, 1);
            headerRowStyle.Interior.PatternColor = Color.FromArgb(254, 184, 10);


            var totalRowStyle = tableStyle.TableStyleElements[TableStyleElementType.TotalRow];

            //Set TotalRow element style.
            totalRowStyle.Borders.Color = Color.White;
            totalRowStyle.Borders[BordersIndex.EdgeLeft].LineStyle = BorderLineStyle.Thin;
            totalRowStyle.Borders[BordersIndex.EdgeRight].LineStyle = BorderLineStyle.Thin;
            totalRowStyle.Borders[BordersIndex.EdgeTop].LineStyle = BorderLineStyle.Thin;
            totalRowStyle.Borders[BordersIndex.EdgeBottom].LineStyle = BorderLineStyle.Thin;
            totalRowStyle.Borders[BordersIndex.InsideVertical].LineStyle = BorderLineStyle.Thin;
            totalRowStyle.Borders[BordersIndex.InsideHorizontal].LineStyle = BorderLineStyle.Thin;
            totalRowStyle.Interior.Color = Color.FromArgb(131, 95, 1);


            //***********************************Set Named Styles*****************************
            IStyle titleStyle = workbook.Styles["Title"];
            titleStyle.Font.Name = "Calibri";
            titleStyle.Font.Size = 36;
            titleStyle.Font.Color = Color.FromArgb(0, 0, 0);
            titleStyle.IncludeAlignment = true;
            titleStyle.VerticalAlignment = VerticalAlignment.Center;

            IStyle heading1Style = workbook.Styles["Heading 1"];
            heading1Style.IncludeAlignment = true;
            heading1Style.HorizontalAlignment = HorizontalAlignment.Right;
            heading1Style.VerticalAlignment = VerticalAlignment.Bottom;
            heading1Style.Borders[BordersIndex.EdgeBottom].LineStyle = BorderLineStyle.None;
            heading1Style.Font.Size = 14;
            heading1Style.Font.Color = Color.Black;
            heading1Style.Font.Bold = false;
            heading1Style.IncludePatterns = true;
            heading1Style.Interior.Color = Color.FromArgb(255, 255, 255);

            IStyle dateStyle = workbook.Styles.Add("Date");
            dateStyle.IncludeNumber = true;
            dateStyle.NumberFormat = "m/d/yyyy";
            dateStyle.IncludeAlignment = true;
            dateStyle.HorizontalAlignment = HorizontalAlignment.Right;
            dateStyle.VerticalAlignment = VerticalAlignment.Center;
            dateStyle.IncludeFont = false;
            dateStyle.IncludeBorder = false;
            dateStyle.IncludePatterns = false;

            IStyle commaStyle = workbook.Styles["Comma"];
            commaStyle.IncludeNumber = true;
            commaStyle.NumberFormat = "#,##0_);(#,##0)";
            commaStyle.IncludeAlignment = true;
            commaStyle.HorizontalAlignment = HorizontalAlignment.Right;
            commaStyle.VerticalAlignment = VerticalAlignment.Center;

            IStyle normalStyle = workbook.Styles["Normal"];
            normalStyle.HorizontalAlignment = HorizontalAlignment.Right;
            normalStyle.VerticalAlignment = VerticalAlignment.Center;
            normalStyle.WrapText = true;
            normalStyle.Font.Color = Color.FromArgb(89, 89, 89);

            IStyle currencyStyle = workbook.Styles["Currency"];
            currencyStyle.NumberFormat = "$#,##0.00";
            currencyStyle.IncludeAlignment = true;
            currencyStyle.HorizontalAlignment = HorizontalAlignment.Right;
            currencyStyle.VerticalAlignment = VerticalAlignment.Center;

            IStyle percentStyle = workbook.Styles["Percent"];
            percentStyle.IncludeAlignment = true;
            percentStyle.HorizontalAlignment = HorizontalAlignment.Right;
            percentStyle.VerticalAlignment = VerticalAlignment.Center;
            percentStyle.IncludeFont = true;
            percentStyle.Font.Name = "Calibri";
            percentStyle.Font.Size = 14;
            percentStyle.Font.Bold = true;
            percentStyle.Font.Color = Color.FromArgb(89, 89, 89);

            IStyle comma0Style = workbook.Styles["Comma [0]"];
            comma0Style.NumberFormat = "#,##0_);(#,##0)";
            comma0Style.IncludeAlignment = true;

            comma0Style.VerticalAlignment = VerticalAlignment.Center;

            //************************************Add Conditional Formatting****************
            IDataBar dataBar = worksheet.Range["F3:F9"].FormatConditions.AddDatabar();
            dataBar.MinPoint.Type = ConditionValueTypes.Number;
            dataBar.MinPoint.Value = 1;
            dataBar.MaxPoint.Type = ConditionValueTypes.Number;
            dataBar.MaxPoint.Value = 0;

            dataBar.BarFillType = DataBarFillType.Gradient;
            dataBar.BarColor.Color = Color.FromArgb(126, 194, 211);
            dataBar.Direction = DataBarDirection.Context;

            dataBar.AxisColor.Color = Color.Black;
            dataBar.AxisPosition = DataBarAxisPosition.Automatic;

            dataBar.NegativeBarFormat.ColorType = DataBarNegativeColorType.Color;
            dataBar.NegativeBarFormat.Color.Color = Color.Red;
            dataBar.ShowValue = true;

            //****************************************Use NamedStyle**************************
            worksheet.SheetView.DisplayGridlines = false;
            table.TableStyle = tableStyle;
            worksheet.Range["B1"].Style = titleStyle;
            worksheet.Range["B1"].WrapText = false;
            worksheet.Range["B2:H2"].Style = heading1Style;
            worksheet.Range["B3:B9"].Style = commaStyle;
            worksheet.Range["C3:C9"].Style = normalStyle;
            worksheet.Range["D3:D9"].Style = dateStyle;
            worksheet.Range["E3:E9"].Style = currencyStyle;
            worksheet.Range["F3:F9"].Style = percentStyle;
            worksheet.Range["G3:G9"].Style = dateStyle;
            worksheet.Range["H3:H9"].Style = comma0Style;

            return workbook;
        }
    }
}