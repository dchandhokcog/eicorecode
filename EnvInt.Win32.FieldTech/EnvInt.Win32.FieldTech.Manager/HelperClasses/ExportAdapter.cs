using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Text;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace EnvInt.Win32.FieldTech.Migrate
{

    public class ExportAdapter
    {
        //This class reads from a datatable object and exports to various formats

        public bool DataTable2Excel(DataTable dt, string strExcelFile)
        {
            bool functionReturnValue = false;

            StringBuilder myString = new StringBuilder();
            bool bFirstRecord = true;
            int rowIndex = 0;

            functionReturnValue = true;


            try
            {
                //write the header row
                bFirstRecord = true;
                foreach (DataColumn column in dt.Columns)
                {
                    if (!bFirstRecord)
                    {
                        myString.Append('\t');
                    }
                    myString.Append(column.ColumnName);
                    bFirstRecord = false;
                }
                myString.Append(Environment.NewLine);


                foreach (DataRow dr in dt.Rows)
                {
                    rowIndex += 1;
                    bFirstRecord = true;
                    foreach (object field in dr.ItemArray)
                    {
                        if (!bFirstRecord)
                        {
                            myString.Append('\t');
                        }
                        myString.Append(field.ToString());
                        bFirstRecord = false;
                    }
                    //New Line to differentiate next row
                    myString.Append(Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                functionReturnValue = false;
                return functionReturnValue;
            }

            try
            {
                System.IO.File.WriteAllText(strExcelFile, myString.ToString(), Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                functionReturnValue = false;
            }
            return functionReturnValue;


        }

        public bool DataTable2CSV(DataTable dt, string fileName)
        {
            bool functionReturnValue = false;

            StringBuilder myString = new StringBuilder();
            bool bFirstRecord = true;
            int rowIndex = 0;

            functionReturnValue = true;


            try
            {
                //write the header row
                bFirstRecord = true;
                foreach (DataColumn column in dt.Columns)
                {
                    if (!bFirstRecord)
                    {
                        myString.Append(",");
                    }
                    myString.Append(column.ColumnName);
                    bFirstRecord = false;
                }
                myString.Append(Environment.NewLine);


                foreach (DataRow dr in dt.Rows)
                {
                    rowIndex += 1;
                    bFirstRecord = true;
                    foreach (object field in dr.ItemArray)
                    {
                        if (!bFirstRecord)
                        {
                            myString.Append(",");
                        }
                        myString.Append(field.ToString());
                        bFirstRecord = false;
                    }
                    //New Line to differentiate next row
                    myString.Append(Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                functionReturnValue = false;
                return functionReturnValue;
            }

            try
            {
                System.IO.File.WriteAllText(fileName, myString.ToString(), Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                functionReturnValue = false;
            }
            return functionReturnValue;


        }

        public bool DataTable2txt(DataTable dt, string fileName, char delimiter)
        {
            bool functionReturnValue = false;

            StringBuilder myString = new StringBuilder();
            bool bFirstRecord = true;
            int rowIndex = 0;

            functionReturnValue = true;


            try
            {
                //write the header row
                bFirstRecord = true;
                foreach (DataColumn column in dt.Columns)
                {
                    if (!bFirstRecord)
                    {
                        myString.Append(delimiter);
                    }
                    myString.Append(column.ColumnName);
                    bFirstRecord = false;
                }
                myString.Append(Environment.NewLine);


                foreach (DataRow dr in dt.Rows)
                {
                    rowIndex += 1;
                    bFirstRecord = true;
                    foreach (object field in dr.ItemArray)
                    {
                        if (!bFirstRecord)
                        {
                            myString.Append(delimiter);
                        }
                        myString.Append(field.ToString());
                        bFirstRecord = false;
                    }
                    //New Line to differentiate next row
                    myString.Append(Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                functionReturnValue = false;
                return functionReturnValue;
            }

            try
            {
                System.IO.File.WriteAllText(fileName, myString.ToString(), Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                functionReturnValue = false;
            }
            return functionReturnValue;


        }

        public string DataTable2HTMLReport(DataTable targetTable, string ReportTemplateName, string ReportTemplatePath, string ReportTitle)
        {

            string[] sourceHTML = File.ReadAllLines(ReportTemplatePath + "\\" + ReportTemplateName + ".htm");
            StringBuilder destinationHTML = new StringBuilder();
            StringBuilder tableHTML = new StringBuilder();

            if (targetTable == null) return string.Empty;

            tableHTML.Append("<table id=\"MainTable\">");
            tableHTML.Append("<thead>");

            //append header values
            foreach (DataColumn dc in targetTable.Columns)
            {
                tableHTML.Append("<th>" + dc.ColumnName + "</th>");
            }

            tableHTML.Append("</thead>");
            tableHTML.Append("</tbody>");

            //Add the data rows.
            foreach (DataRow myRow in targetTable.Rows)
            {
                tableHTML.Append("<tr>");

                foreach (DataColumn myColumn in targetTable.Columns)
                {
                    tableHTML.Append("<td>");
                    if (myRow[myColumn.ColumnName].ToString() == null)
                    {
                        tableHTML.Append("&nbsp;");
                    }
                    else
                    {
                        tableHTML.Append(myRow[myColumn.ColumnName].ToString());
                    }
                    tableHTML.Append("</td>");
                }


                tableHTML.Append("</tr>");
            }

            tableHTML.Append("</tbody>");
            tableHTML.Append("</table>");

            //finish building the report based on the template
            foreach (string htmlRow in sourceHTML)
            {
                bool skipLine = false;
                if (htmlRow.Contains("<div id=\"Title\">ReportTitle</div>"))
                {
                    destinationHTML.Append("<div id=\"Title\">" + ReportTitle + "</div>");
                    skipLine = true;
                }
                if (htmlRow.Contains("<table id=\"MainTable\"></table>"))
                {
                    destinationHTML.Append(tableHTML.ToString());
                    skipLine = true;
                }

                if (!skipLine)
                {
                    destinationHTML.Append(htmlRow);
                }
            }

            return destinationHTML.ToString(); 
        }


        public string ConvertToHtmlFile(DataTable targetTable, string fileName)
        {
            string myHtmlFile = "";

            if ((targetTable == null))
            {
                throw new System.ArgumentNullException("targetTable");
            }
            else
            {
                //Continue.
            }

            //Get a worker object.
            System.Text.StringBuilder myBuilder = new System.Text.StringBuilder();

            //Open tags and write the top portion.
            myBuilder.Append("<html xmlns='http://www.w3.org/1999/xhtml'>");
            myBuilder.Append("<head>");
            myBuilder.Append("<title>");
            myBuilder.Append("Page-");
            myBuilder.Append(Guid.NewGuid().ToString());
            myBuilder.Append("</title>");
            myBuilder.Append("</head>");
            myBuilder.Append("<body>");
            myBuilder.Append("<table border='1px' cellpadding='5' cellspacing='0' ");
            myBuilder.Append("style='border: solid 1px Silver; font-size: x-small;'>");

            //Add the headings row.

            myBuilder.Append("<tr align='left' valign='top'>");

            foreach (DataColumn myColumn in targetTable.Columns)
            {
                myBuilder.Append("<td align='left' valign='top'>");
                myBuilder.Append(myColumn.ColumnName);
                myBuilder.Append("</td>");
            }

            myBuilder.Append("</tr>");


            //Add the data rows.
            foreach (DataRow myRow in targetTable.Rows)
            {
                myBuilder.Append("<tr align='left' valign='top'>");

                foreach (DataColumn myColumn in targetTable.Columns)
                {
                    myBuilder.Append("<td align='left' valign='top'>");
                    if (myRow[myColumn.ColumnName].ToString() == null)
                    {
                        myBuilder.Append("&nbsp;");
                    }
                    else
                    {
                        myBuilder.Append(myRow[myColumn.ColumnName].ToString());
                    }
                    //myBuilder.Append(myRow(myColumn.ColumnName).ToString())
                    myBuilder.Append("</td>");
                }


                myBuilder.Append("</tr>");
            }

            //Close tags.
            myBuilder.Append("</table>");
            myBuilder.Append("</body>");
            myBuilder.Append("</html>");

            //Get the string for return.
            myHtmlFile = myBuilder.ToString();

            try
            {
                System.IO.File.WriteAllText(fileName, myHtmlFile.ToString(), Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return myHtmlFile;
        }

    }
}