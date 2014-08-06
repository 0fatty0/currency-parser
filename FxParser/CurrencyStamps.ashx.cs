using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace FxParser
{
    /// <summary>
    /// Summary description for CurrencyStamps
    /// </summary>
    public class CurrencyStamps : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            DateTime displayDate = GetDisplayDate(context.Request.Params);
            List<CurrencyStampEntity> stampsAll = GetCurrencyStampEntities(displayDate);
            string stampsJson = ConvertStampsToJson(stampsAll);

            context.Response.Write(stampsJson);
        }

        private DateTime GetDisplayDate(NameValueCollection requestParams)
        {
            string displayDateString = requestParams["displayDate"];
            return DateTime.Parse(displayDateString);
        }

        private List<CurrencyStamp> GetCurrencyStamps()
        {
            List<CurrencyStamp> stampsAll = null;

            using (CurrencyDbContext context = new CurrencyDbContext())
            {
                stampsAll = context.CurrencyStamps.ToList();
            }

            return stampsAll;
        }

        private List<CurrencyStampEntity> GetCurrencyStampEntities(DateTime displayDate)
        {
            
            List<CurrencyStamp> stampsAll = GetCurrencyStamps();
            List<CurrencyStampEntity> stampEntitiesAll = new List<CurrencyStampEntity>();

            foreach(CurrencyStamp stamp in stampsAll)
            {
                if (stamp.Time.Date == displayDate)
                {
                    stampEntitiesAll.Add(new CurrencyStampEntity(stamp));
                }
                
            }

            return stampEntitiesAll;
        }

        private string ConvertStampsToJson(List<CurrencyStampEntity> stamps)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string stampsJson = jss.Serialize(stamps);

            return stampsJson;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}