using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Diamtrade.Models;
using Diamtrade.Common;

namespace Diamtrade.Controllers
{

    
    public class DiamtradeController : Controller
    {
        DiamtradeAdminEntities db = null;

        //string hostemailid = System.Configuration.ConfigurationManager.AppSettings["hostemail"];
        //string networkemail = System.Configuration.ConfigurationManager.AppSettings["networkemail"];
        //string networkemailpass = System.Configuration.ConfigurationManager.AppSettings["networkemailpass"];
        //string inqiuryemail = System.Configuration.ConfigurationManager.AppSettings["inqiuryemail"];
        //string careeremail = System.Configuration.ConfigurationManager.AppSettings["careeremail"];
        //string contactusemail = System.Configuration.ConfigurationManager.AppSettings["contactus"];
        //string networkemailport = System.Configuration.ConfigurationManager.AppSettings["networkemailport"];
        //string networkemailssl = System.Configuration.ConfigurationManager.AppSettings["networkemailssl"];

        public DiamtradeController()
        {
            db = new DiamtradeAdminEntities();
        }
        // GET: Diamtrade
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult about()
        {
            return View();
        }
        public ActionResult testimonials()
        {
            return View();
        }
        public ActionResult support()
        {
            return View();
        }
        public ActionResult contactus()
        {
            return View();
        }
        public ActionResult sign_in()
        {
            return View();
        }
        public ActionResult diamtrade()
        {
            return View();
        }

        [Route("contactus123")]
        public ActionResult inquiry()
        {
            return View();
        }
        public ActionResult careers()
        {
            return View();
        }
      
        
        public ActionResult compare()
        {
            return View();
        }
        public ActionResult downloads()
        {
            return View();
        }

        #region InquiryMessage
        public JsonResult InsertInquiryMessage(Mst_InquiryMessage brc)
        {
           
            try
            {
                var msg = "";
                var success = "";
                MailMessage mail = new MailMessage();
                mail.To.Add(Helper.inqiuryemail);
                mail.From = new MailAddress(Helper.networkemail);
                mail.Subject = "New Inquiry For Diamtrade";
                string Body = "Dear Sir/Mam <br /><br />" +
                    "Hello,<br />" +
                    "<b>Person Name :</b>" + brc.FullName + "<br />" +
                    "<b>Mobile Number :</b>" + brc.MobileNo + "<br />" +
                    "<b>Email-ID :</b>" + brc.EmailID + "<br />" +
                    "<b>Inquiry Type :</b>" + brc.InquiryType + "<br />" +
                    "<b>Message :</b>" + brc.InquiryMessage + "<br />";
                    
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = Helper.hostemailid;
                smtp.Port = Convert.ToInt32(Helper.networkemailport);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(Helper.networkemail,Helper.networkemailpass); // Enter seders User name and password  
                smtp.EnableSsl = Convert.ToBoolean(Helper.networkemailssl);

                try
                {
                    smtp.Send(mail);
                    db.Mst_InquiryMessage.Add(brc);
                    db.SaveChanges();
                    msg = "Inquiry Send Successfully..";
                    success = "true";
                    var resultm = new { msg, success };
                }
                catch
                {
                    msg = "Inquiry Not Send..";
                    success = "false";
                    var resultm = new { msg, success };
                }

                 return Json(success, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region InquiryMessage
        public JsonResult ContactUsMessage(Mst_CotactMessage brc)
        {
            try
            {
                var msg = "";
                var success = "";
                MailMessage mail = new MailMessage();
                mail.To.Add(Helper.contactus);
                mail.From = new MailAddress(Helper.networkemail);
                mail.Subject = "New Contact For Diamtrade";
                string Body = "Dear Sir/Mam <br /><br />" +
                    "Hello,<br />" +
                    "<b>Person Name :</b>" + brc.FirstName + " "  + brc.LastName +"<br />" +
                    "<b>Mobile Number :</b>" + brc.MobileNo + "<br />" +
                    "<b>Email-ID :</b>" + brc.EmailID + "<br />" +
                    "<b>Message :</b>" + brc.MessageContent + "<br />";

                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = Helper.hostemailid;
                smtp.Port = Convert.ToInt32(Helper.networkemailport);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(Helper.networkemail,Helper.networkemailpass); // Enter seders User name and password  
                smtp.EnableSsl = Convert.ToBoolean(Helper.networkemailssl);

                try
                {
                    smtp.Send(mail);
                    db.Mst_CotactMessage.Add(brc);
                    db.SaveChanges();
                    msg = "Contact Message Send Successfully..";
                    success = "true";
                    var resultm = new { msg, success };
                }
                catch
                {
                    msg = "Contact Message Not Send..";
                    success = "false";
                    var resultm = new { msg, success };
                }

                return Json(success, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Gallery
        public ActionResult Gallery()
        {
            return View();
        }
        #endregion

        #region GalleryPhoto
        public ActionResult GalleryPhoto()
        {
            return View();
        }
        #endregion

       
    }
}