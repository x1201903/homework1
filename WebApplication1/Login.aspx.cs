using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //List<string> account = new List<string> { "Test001", "Test002" };
            //List<string> password = new List<string> { "123", "456" };

            Dictionary<string, string> account = new Dictionary<string, string>()
            {
                { "Test001","123"},
                { "Test002","456"}
            };
            for (int i = 0; i < account.Count; i++)
            {
                if (TextBox2.Text == account[TextBox1.Text])
                {
                    Server.Transfer("SUSESS.aspx");
                }
            }
            Response.Write("<Script language='Javascript'>");
            Response.Write("alert('錯誤! 無法登入!')");
            Response.Write("</" + "Script>");
        }
       
    }
}