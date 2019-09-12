using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.HSSF;
using NPOI.POIFS.FileSystem;
using System.Data;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;

namespace WebApplication1
{
    
    public partial class SUSESS : System.Web.UI.Page
    {
        public static DataTable tmpdb;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
       
        protected void Button1_Click(object sender, EventArgs e)
        {
            string savePath = @"C:\Users\Ni\Desktop";
            if (FileUpload1.HasFile)
            {
                string filename = FileUpload1.FileName;
                savePath += filename;
                FileUpload1.SaveAs(savePath);
                Label1.Text = "上傳成功";
                Label2.Visible = true;
                TextBox_name.Visible = true;
                Label3.Visible = true;
                TextBox_test1.Visible = true;
                Label4.Visible = true;
                TextBox_test2.Visible = true;
                Label5.Visible = true;
                TextBox_test3.Visible = true;
                Label6.Visible = true;
                TextBox_exam.Visible = true;
                Label7.Visible = true;
                TextBox_grade.Visible = true;
                Button2.Visible = true;

                IWorkbook myWorkbook;
                myWorkbook = new XSSFWorkbook(FileUpload1.FileContent);
                ISheet sheet = myWorkbook.GetSheetAt(0);
                //建立DATATABLE

                DataTable myDT = new DataTable();
                XSSFRow headerRow = sheet.GetRow(0) as XSSFRow;
                for (int i = headerRow.FirstCellNum; i < headerRow.LastCellNum; i++)
                {
                    if (headerRow.GetCell(i) != null)
                    {
                        DataColumn myColumn = new DataColumn(headerRow.GetCell(i).StringCellValue);
                        myDT.Columns.Add(myColumn);

                    }

                }
                //抓取MYSHEET工作表中的標題欄位，並存入DATATABLE

                for (int i = sheet.FirstRowNum + 1; i < sheet.LastRowNum; i++)
                {
                    XSSFRow row = sheet.GetRow(i) as XSSFRow;
                    DataRow myrow = myDT.NewRow();
                    for (int j = row.FirstCellNum; j < row.LastCellNum; j++)
                    {
                        if (row.GetCell(j) != null)
                        {
                            myrow[j] = row.GetCell(j).ToString().Trim();
                        }
                    }
                    myDT.Rows.Add(myrow);
                }
                tmpdb = myDT;
                GridView1.DataSource = myDT;
                GridView1.DataBind();
                GridView1.Visible = true;
                var x = tmpdb;
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable data;
            data = tmpdb;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataSource = data;
            GridView1.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            DataTable data;
            data = tmpdb;
           
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i].ItemArray[0].ToString().Trim() == TextBox_name.Text.Trim())
                {
                    data.Rows[i].ItemArray[1] = TextBox_test1.Text.Trim();
                    data.Rows[i].ItemArray[2] = TextBox_test2.Text.Trim();
                    data.Rows[i].ItemArray[3] = TextBox_test3.Text.Trim();
                    data.Rows[i].ItemArray[4] = TextBox_exam.Text.Trim();
                    data.Rows[i].ItemArray[5] = TextBox_grade.Text.Trim();
                    tmpdb = data;
                    GridView1.DataSource = data;
                    GridView1.DataBind();
                    return;
                }
            }
            if (TextBox_name.Text.Trim() != "")
            {
                data.Rows.Add(new object [] {TextBox_name.Text.Trim() ,
               TextBox_test1.Text.Trim(),TextBox_test2.Text.Trim(),TextBox_test3.Text.Trim(),
                TextBox_exam.Text.Trim(),TextBox_grade.Text.Trim()});
                tmpdb = data;
                GridView1.DataSource = data;
                GridView1.DataBind();
                return;
            }
        }

      
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            DataTable data;
            data = tmpdb;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable data;
            data = tmpdb;
            TextBox_name.Text = data.Rows[GridView1.SelectedIndex].ItemArray[0].ToString().Trim();
            TextBox_test1.Text = data.Rows[GridView1.SelectedIndex].ItemArray[1].ToString().Trim();
            TextBox_test2.Text = data.Rows[GridView1.SelectedIndex].ItemArray[2].ToString().Trim();
            TextBox_test3.Text = data.Rows[GridView1.SelectedIndex].ItemArray[3].ToString().Trim();
            TextBox_exam.Text = data.Rows[GridView1.SelectedIndex].ItemArray[4].ToString().Trim();
            TextBox_grade.Text = data.Rows[GridView1.SelectedIndex].ItemArray[5].ToString().Trim();
            Button2.Text = "更新資料";
        }

        protected void TextBox_name_TextChanged(object sender, EventArgs e)
        {
            if (TextBox_name.Text.Trim() == "")
            {
                Button2.Text = "新增資料";
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = 0;
           DataTable data;
            data = tmpdb;
            for (int i = 0; i < data.Rows.Count; i++)
                if (data.Rows[i].ItemArray[0].ToString().Trim() == e.Values[0].ToString())
                    data.Rows.RemoveAt(i);

            tmpdb = data;
            GridView1.DataSource = data;
            GridView1.DataBind();
            return;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            IWorkbook wb = new XSSFWorkbook();
            ISheet ws = wb.CreateSheet("Class");

            ws.CreateRow(0);
            for (int i = 0; i < tmpdb.Columns.Count; i++)
                ws.GetRow(0).CreateCell(i).SetCellValue(tmpdb.Columns[i].ColumnName.Trim());

            for (int i = 0; i < tmpdb.Rows.Count; i++)
            {
                ws.CreateRow(i+1);
                for (int j = 0; j < tmpdb.Columns.Count; j++)
                {
                    ws.GetRow(i+1).CreateCell(j).SetCellValue(tmpdb.Rows[i].ItemArray[j].ToString().Trim());
                }
            }
            FileStream file = new FileStream(@"C:\Users\Ni\Desktop\handsome.xlsx", FileMode.Create);//產生檔案
            wb.Write(file);
            file.Close();
        }
    }
}