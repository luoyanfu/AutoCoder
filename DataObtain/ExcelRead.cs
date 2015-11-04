using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace DataObtain
{
    /// <summary>
    /// read excel
    /// </summary>
    public class ExcelRead
    {
        /// <summary>
        /// read excel
        /// </summary>
        /// <param name="path"></param>
        /// <param name="sheetName"></param>
        /// <returns>datatable</returns>
        public static DataTable ReadExcel(string path, string sheetName)
        {
            var dt = XlsDataByCom(path, sheetName);
            return dt;
        }

        /// <summary>
        /// OleDB
        /// </summary>
        private static DataTable XlsDataByOleDb(string path, string sheetName)
        {
            var dt = new DataTable();

            try
            {
                var connectString =
                    string.Format(
                        "Provider=Microsoft.ACE.OLEDB.12.0;Data Source = {0};Extended Properties ='Excel 12.0;HDR=YES;IMEX=1'",
                        path);
                using (var con = new OleDbConnection(connectString))
                {
                    con.Open();
                    var sql = "select * from [" + sheetName + "$]";
                    var cmd = new OleDbCommand(sql, con);
                    var dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    dr.Close();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                dt = null;
            }
            return dt;
        }

        /// <summary>
        /// COM
        /// </summary>
        private static DataTable XlsDataByCom(string path, string sheetName)
        {
            var application = new Excel.Application();
            Excel.Workbook workbook = null;
            Excel.Sheets sheets = null;
            Excel.Worksheet worksheet = null;
            var oMissiong = Type.Missing;
            var dt = new DataTable();

            try
            {
                workbook = application.Workbooks.Open(path, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong,
                    oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong);

                sheets = workbook.Sheets;
                worksheet = (Excel.Worksheet)sheets[sheetName]; 

                var iRowCount = worksheet.UsedRange.Rows.Count;
                var iColCount = worksheet.UsedRange.Columns.Count;

                var columnID = 1;
                while (columnID <= iColCount)
                {
                    var dc = new DataColumn
                    {
                        DataType = Type.GetType("System.String"),
                        ColumnName = Convert.ToString(columnID)
                    };
                    dt.Columns.Add(dc);
                    columnID++;
                }

                for (var iRow = 2; iRow <= iRowCount; iRow++)
                {
                    var dr = dt.NewRow();
                    for (var iCol = 1; iCol <= iColCount; iCol++)
                    {
                        var range = (Excel.Range)worksheet.Cells[iRow, iCol];
                        if ((iCol == 2 && string.IsNullOrWhiteSpace(range.Text))
                            || ((iCol == 4 || iCol == 5) && range.Text == "obligate"))
                        {
                            dr = null;
                            break;
                        }

                        string cellContent = (range.Value2 == null) ? "" : range.Text;
                        dr[iCol - 1] = cellContent;
                        Release(range);
                    }

                    if (dr == null)
                    {
                        continue;
                    }

                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                dt = null;
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                Release(worksheet);
                Release(sheets);
                if (workbook != null)
                {
                    workbook.Close(true, oMissiong, oMissiong);
                    Release(workbook);
                }
                application.Workbooks.Close();
                application.Quit();
                Release(application);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            return dt;
        }

        private static void Release(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                obj = null;
            }
        }
    }
}
