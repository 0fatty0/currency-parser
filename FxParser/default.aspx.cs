using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Threading.Tasks;

namespace FxParser
{
    public partial class _default : System.Web.UI.Page
    {
        private string CurrencyPair = "EUR/USD";
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
        }

        protected void RefreshButton_Click(object sender, EventArgs e)
        {
            using (CurrencyDbContext context = new CurrencyDbContext())
            {
                List<CurrencyStamp> stamps = context.CurrencyStamps.ToList();

                if (!IsPostBack)
                {
                    StampDataRepeater.DataSource = stamps;
                    StampDataRepeater.DataBind();
                }
            }
        }

        
    }
}