using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Diamtrade.Common
{
    public static class Helper
    {
        public static string hostemailid
        {
            get { return ConfigurationManager.AppSettings["hostemail"]; }
        }
        public static string networkemail
        {
            get { return ConfigurationManager.AppSettings["networkemail"]; }
        }
        public static string networkemailpass
        {
            get { return ConfigurationManager.AppSettings["networkemailpass"]; }
        }
        public static string inqiuryemail
        {
            get { return ConfigurationManager.AppSettings["inqiuryemail"]; }
        }

        public static string careeremail
        {
            get { return ConfigurationManager.AppSettings["careeremail"]; }
        }
        public static string contactus
        {
            get { return ConfigurationManager.AppSettings["contactus"]; }
        }
        public static string networkemailport
        {
            get { return ConfigurationManager.AppSettings["networkemailport"]; }
        }
        public static string networkemailssl
        {
            get { return ConfigurationManager.AppSettings["networkemailssl"]; }
        }
        public static string demorequest
        {
            get { return ConfigurationManager.AppSettings["demorequest"]; }
        }
    }
}