using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace FooFoo
{
    public class DataTableReader
    {
        public DataTable GetDataTable()
        {
            string strConn = ConfigurationManager.ConnectionStrings["FooFooConnectionString"].ToString();
            SqlConnection conn = new SqlConnection(strConn);
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM [FooFoo] ORDER BY id ASC", conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "FooFoo");
            DataTable dt = ds.Tables["FooFoo"];
            return dt;
        }
    }

    public class DataTableToCSVMapper
    {
        private readonly DataTableReader _dataTableReader = new DataTableReader();

        public System.IO.MemoryStream CreateMemoryFile()
        {
            MemoryStream ReturnStream = new MemoryStream();

            try
            {
                var dt = _dataTableReader.GetDataTable();

                //Create a streamwriter to write to the memory stream
                StreamWriter sw = new StreamWriter(ReturnStream);
                WriteColumns(dt, sw);
                WriteRows(dt, sw);

                sw.Flush();
                sw.Close();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return ReturnStream;
        }

        private void WriteRows(DataTable dt, StreamWriter sw)
        {
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    WriteRow(dr[i], sw);

                    HandleSeparator(dt, i, sw);
                }
                sw.WriteLine();
            }
        }

        private void WriteRow(object dataCell, StreamWriter sw)
        {
            if (!Convert.IsDBNull(dataCell))
            {
                string str = String.Format("\"{0:c}\"", dataCell.ToString()).Replace("\r\n", " ");
                sw.Write(str);
            }
            else
            {
                sw.Write("");
            }
        }

        private void HandleSeparator(DataTable dt, int i, StreamWriter sw)
        {
            if (i < dt.Columns.Count - 1)
            {
                sw.Write(",");
            }
        }

        private void WriteColumns(DataTable dt, StreamWriter sw)
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                sw.Write(dt.Columns[i]);
                if (i < dt.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }

            sw.WriteLine();
        }
    }

    public partial class Download : System.Web.UI.Page
    {
        private readonly DataTableToCSVMapper _dataTableToCsvMapper = new DataTableToCSVMapper();

        protected void Page_Load(object sender, EventArgs e)
        {
            System.IO.MemoryStream ms = _dataTableToCsvMapper.CreateMemoryFile();

            byte[] byteArray = ms.ToArray();
            ms.Flush();
            ms.Close();
            ResponseClear();
            ResponseCachebility();
            WriteContent(byteArray);
        }

        private void WriteContent(byte[] byteArray)
        {
            Response.Charset = System.Text.UTF8Encoding.UTF8.WebName;
            Response.ContentEncoding = System.Text.UTF8Encoding.UTF8;
            Response.ContentType = "text/comma-separated-values";
            Response.AddHeader("Content-Disposition", "attachment; filename=FooFoo.csv");
            Response.AddHeader("Content-Length", byteArray.Length.ToString());
            Response.BinaryWrite(byteArray);
        }

        private void ResponseCachebility()
        {
            Response.Cache.SetCacheability(HttpCacheability.Private);
            Response.CacheControl = "private";
            Response.AppendHeader("Pragma", "cache");
            Response.AppendHeader("Expires", "60");
        }

        private void ResponseClear()
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Cookies.Clear();
        }
    }
}