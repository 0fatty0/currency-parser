using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace FxParser
{
    public class Global : System.Web.HttpApplication
    {
        private CurrencyDbContext _currencyDbContext;
        private bool _applicationClosing = false;
        private string CurrencyPair = "EUR/USD";
        private int IntervalInSeconds = 5;
        private string _sessionId = null;
        private string _sessionQualifierRandom = null;
        protected void Application_Start(object sender, EventArgs e)
        {
            InitDatabase();
            StartDataParser();
        }


        private void InitDatabase()
        {
            _currencyDbContext = new CurrencyDbContext();

        }

        private void StartDataParser()
        {
            InitParser();
            StartParserRoutine();
            
        }

        private void InitParser()
        {
            // Create an HttpClient instance 
            HttpClient client = new HttpClient();

            _sessionQualifierRandom = "test";
            string requestUrl = String.Format("http://webrates.truefx.com/rates/connect.html?u=0fatty0&p=31121992&q={0}", _sessionQualifierRandom);
            // Send a request asynchronously continue when complete 
            client.GetAsync(requestUrl).ContinueWith(
                (requestTask) =>
                {
                    // Get HTTP response from completed task. 
                    HttpResponseMessage response = requestTask.Result;

                    // Check that response was successful or throw exception 
                    response.EnsureSuccessStatusCode();

                    // Read response asynchronously as JsonValue
                    response.Content
                        .ReadAsStringAsync()
                        .ContinueWith((sessionIdContent) =>
                        {
                            _sessionId = sessionIdContent.Result.Replace("\r\n", "");
                        });

                }); 
        }

        private void StartParserRoutine()
        {
            ThreadPool.QueueUserWorkItem((Object state) =>
            {
                EasyTimer.SetInterval(() =>
                {
                    GetCurrentPairData();
                }, IntervalInSeconds * 1000);
                //while (!_applicationClosing)
                //{
                    
                //}
            });
        }


        private void GetCurrentPairData()
        {
            // Create an HttpClient instance 
            HttpClient client = new HttpClient();

            string requestUrl = String.Format("http://webrates.truefx.com/rates/connect.html?u=0fatty0&p=31121992&id={0}&q={1}&c={2}&f=csv", _sessionId, _sessionQualifierRandom, CurrencyPair);
            // Send a request asynchronously continue when complete 
            client.GetAsync(requestUrl).ContinueWith( 
                (requestTask) => 
                { 
                    // Get HTTP response from completed task. 
                    HttpResponseMessage response = requestTask.Result; 

                   // Check that response was successful or throw exception 
                    response.EnsureSuccessStatusCode(); 

                    // Read response asynchronously as JsonValue
                    response.Content
                        .ReadAsStringAsync()
                        .ContinueWith((readTask) => {
                            CurrencyStamp stampEntity = ParseCurrencyStamp(readTask.Result);
                            if (stampEntity != null)
                            {
                                SaveToDatabase(stampEntity);
                            }
                        });
                    
                }); 
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        private CurrencyStamp ParseCurrencyStamp(string stampString)
        {
            string[] parsedStamp = stampString.Split(',');
            if (parsedStamp.Length != 9)
            {
                return null;
            }
            CurrencyStamp newStamp = new CurrencyStamp();

            

            newStamp.CurrencyPair = parsedStamp[0];
            newStamp.StartPrice = Double.Parse(parsedStamp[6]);
            newStamp.EndPrice = Double.Parse(parsedStamp[7]);
            newStamp.MiddlePrice = Double.Parse(parsedStamp[8]);

            //double unixTimestamp =  Double.Parse(parsedStamp[1]);
            //newStamp.Time = UnixTimeStampToDateTime(unixTimestamp);
            newStamp.Time = DateTime.Now;

            return newStamp;
        }
        private void SaveToDatabase(CurrencyStamp newStamp)//Task<string> readTask)
        {
            _currencyDbContext.CurrencyStamps.Add(newStamp);
            _currencyDbContext.SaveChanges();
        }
    }
}