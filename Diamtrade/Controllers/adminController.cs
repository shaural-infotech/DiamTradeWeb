using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Diamtrade.Models;
using Diamtrade.Common;

namespace Diamtrade.Controllers
{
    public class adminController : Controller
    {
        DiamtradeAdminEntities db = null;
        static int OurClientIDNew = 0;
        //string hostemailid = System.Configuration.ConfigurationManager.AppSettings["hostemail"];
        //string networkemail = System.Configuration.ConfigurationManager.AppSettings["networkemail"];
        //string networkemailpass = System.Configuration.ConfigurationManager.AppSettings["networkemailpass"];
        //string inqiuryemail = System.Configuration.ConfigurationManager.AppSettings["inqiuryemail"];
        //string demorequest = System.Configuration.ConfigurationManager.AppSettings["demorequest"];
        //string careeremail = System.Configuration.ConfigurationManager.AppSettings["careeremail"];
        //string contactus = System.Configuration.ConfigurationManager.AppSettings["contactus"];
        //string networkemailport = System.Configuration.ConfigurationManager.AppSettings["networkemailport"];
        //string networkemailssl = System.Configuration.ConfigurationManager.AppSettings["networkemailssl"];

        public adminController()
        {
            db = new DiamtradeAdminEntities();
        }

        #region ViewPages
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult abc()
        {
            return View();
        }
        public ActionResult dashboard()
        {
            return View();
        }
        public ActionResult about()
        {
            return View();
        }
        public ActionResult slider()
        {
            return View();
        }
        public ActionResult home()
        {
            return View();
        }
        public ActionResult ServiceType()
        {
            return View();
        }
        public ActionResult Service()
        {
            return View();
        }
        public ActionResult Testimonial()
        {
            return View();
        }
        public ActionResult Product()
        {
            return View();
        }
        public ActionResult Inquiry()
        {
            return View();
        }
        public ActionResult InquiryMessage()
        {
            return View();
        }
        public ActionResult Contactus()
        {
            return View();
        }
        public ActionResult ContactMessage()
        {
            return View();
        }
        public ActionResult Careers()
        {
            return View();
        }
        public ActionResult FooterBlock()
        {
            return View();
        }
        public ActionResult Compare()
        {
            return View();
        }
        public ActionResult Desktop()
        {
            return View();
        }
        public ActionResult Cloud()
        {
            return View();
        }
        public ActionResult Diamx()
        {
            return View();
        }
        public ActionResult Support()
        {
            return View();
        }


        public ActionResult DesktopFeature()
        {
            return View();
        }
        public ActionResult CloudFeature()
        {
            return View();
        }
        public ActionResult OurClient()
        {
            return View();
        }
        #endregion

        #region LoginUser
        public JsonResult UserLogin(Credential cr)
        {
            LoginClass loginclass = new LoginClass();
            LoginData loginData = new LoginData();

            if ((cr.UserName == null || cr.UserName == "") && (cr.Password == null || cr.Password == ""))
            {
                loginData.success = false;
                loginData.msg = "Please Enter UserName & Password!";
                return Json(loginData, JsonRequestBehavior.AllowGet);
            }
            else if (cr.UserName == null || cr.UserName == "")
            {
                loginData.success = false;
                loginData.msg = "Please Enter UserName!";
                return Json(loginData, JsonRequestBehavior.AllowGet);
            }
            else if (cr.Password == null || cr.Password == "")
            {
                loginData.success = false;
                loginData.msg = "Please Enter Password.!";
                return Json(loginData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                loginclass = (from emp in db.Mst_User
                              where emp.IsActive == true && emp.UserName.Trim() == cr.UserName && emp.Password == cr.Password
                              select new LoginClass
                              {
                                  UserID = emp.UserID,
                                  UserName = emp.UserName


                              }).SingleOrDefault();

                if (loginclass == null)
                {
                    loginData.success = false;
                    loginData.msg = "Invalid username password.";
                    return Json(loginData, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    HttpCookie usercookie = new HttpCookie("username");
                    usercookie.Value = loginclass.UserName;
                    usercookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(usercookie);


                    loginData.success = true;
                    loginData.msg = "Login successfully.";
                    return Json(loginData, JsonRequestBehavior.AllowGet);
                }
            }

        }
      
        public  JsonResult LogOut()
        {
            string msg = "";
            if(Request.Cookies["username"].Value!=null)
            {
                var c = new HttpCookie("username");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
                msg = "Success";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            {
                msg = "Fail";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region AboutUS
        [AcceptVerbs("GET")]
        public JsonResult EditAboutUs()
        {
            AboutUsClass aboutclass = new AboutUsClass();
            AboutUsData aboutdata = new AboutUsData();

            aboutdata.data = (from abt in db.Mst_AboutUS
                              where abt.IsActive == true
                              select new AboutUsClass
                              {
                                  AboutID = abt.AboutID,
                                  AboutTitle = abt.AboutTitle,
                                  AboutContent = abt.AboutContent,
                                  AboutTitle1 = abt.AboutTitle1,
                                  AboutContent1 = abt.AboutContent1,
                                  AboutTitle2 = abt.AboutTitle2,
                                  AboutContent2 = abt.AboutContent2,
                                  AboutTitle3 = abt.AboutTitle3,
                                  AboutContent3 = abt.AboutContent3,
                                  AboutTitle4 = abt.AboutTitle4,
                                  AboutContent4 = abt.AboutContent4,
                                  Image_Banner = abt.Image_Banner,
                                  Image_Business = abt.Image_Business,
                                  Image_Access = abt.Image_Access,
                                  Image_Safest_Fastest =abt.Image_Safest_Fastest
                              }).ToList();


            if (aboutdata.data.Count > 0)
            {
                var aboutus = "";
                aboutdata.success = true;
                var result = new { aboutus = aboutdata.data, aboutdata.success };
                //return Request.CreateResponse(HttpStatusCode.OK, result);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                aboutdata.success = false;
                //return Request.CreateResponse(HttpStatusCode.OK, aboutdata);
                return Json(aboutdata, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UpdateAboutUs(AboutUsClass abtus)
        {
            try
            {
                int AboutID = Convert.ToInt32(abtus.AboutID);
                Mst_AboutUS bmst = new Mst_AboutUS();
                bmst = db.Mst_AboutUS.Where(a => a.AboutID == AboutID).SingleOrDefault();
                bmst.AboutTitle = abtus.AboutTitle;
                bmst.AboutContent = abtus.AboutContent;
                bmst.AboutTitle1 = abtus.AboutTitle1;
                bmst.AboutContent1 = abtus.AboutContent1;
                bmst.AboutTitle2 = abtus.AboutTitle2;
                bmst.AboutContent2 = abtus.AboutContent2;
                bmst.AboutTitle3 = abtus.AboutTitle3;
                bmst.AboutContent3 = abtus.AboutContent3;
                bmst.AboutTitle4 = abtus.AboutTitle4;
                bmst.AboutContent4 = abtus.AboutContent4;
                bmst.CreateDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                db.SaveChanges();
                return Json("Success");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult UploadAboutBanner_image(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_AboutUS pm = new Mst_AboutUS();
                    pm = (db.Mst_AboutUS.Where(r => r.AboutID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Image_Banner = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult UploadAboutBusiness_image(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_AboutUS pm = new Mst_AboutUS();
                    pm = (db.Mst_AboutUS.Where(r => r.AboutID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Image_Business = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult UploadAboutAccess_image(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_AboutUS pm = new Mst_AboutUS();
                    pm = (db.Mst_AboutUS.Where(r => r.AboutID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Image_Access = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult UploadAboutSafest_image(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_AboutUS pm = new Mst_AboutUS();
                    pm = (db.Mst_AboutUS.Where(r => r.AboutID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Image_Safest_Fastest = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        #endregion

        #region Slider
        [AcceptVerbs("GET")]
        public JsonResult EditSlider()
        {
            SliderClass sliderclass = new SliderClass();
            SliderData sliderdata = new SliderData();

            sliderdata.sliders = (from abt in db.Mst_Slider
                                  where abt.IsActive == true
                                  select new SliderClass
                                  {
                                      SliderID = abt.SliderID,
                                      Title1 = abt.Title1,
                                      Content1 = abt.Content1,
                                      Title2 = abt.Title2,
                                      Content2 = abt.Content2,
                                      Title3 = abt.Title3,
                                      Content3 = abt.Content3,
                                      Title4 = abt.Title4,
                                      Content4 = abt.Content4,
                                      Image_1 = abt.Image_1,
                                      Image_2 = abt.Image_2,
                                      Image_3 = abt.Image_3,
                                      Image_4 = abt.Image_4

                                  }).ToList();


            if (sliderdata.sliders.Count > 0)
            {
                var aboutus = "";
                sliderdata.success = true;
                var result = new { aboutus = sliderdata.sliders, sliderdata.success };
                //return Request.CreateResponse(HttpStatusCode.OK, result);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                sliderdata.success = false;
                //return Request.CreateResponse(HttpStatusCode.OK, aboutdata);
                return Json(sliderdata, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UpdateSlider(SliderClass abtus)
        {
            try
            {
                int SliderID = Convert.ToInt32(abtus.SliderID);
                Mst_Slider bmst = new Mst_Slider();
                bmst = db.Mst_Slider.Where(a => a.SliderID == SliderID).SingleOrDefault();
                bmst.Title1 = abtus.Title1;
                bmst.Title2 = abtus.Title2;
                bmst.Title3 = abtus.Title3;
                bmst.Content1 = abtus.Content1;
                bmst.Content2 = abtus.Content2;
                bmst.Content3 = abtus.Content3;
                bmst.Title4 = abtus.Title4;
                bmst.Content4 = abtus.Content4;

                bmst.CreateDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                db.SaveChanges();
                return Json("Success");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult UploadSlider1_image(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_Slider pm = new Mst_Slider();
                    pm = (db.Mst_Slider.Where(r => r.SliderID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Image_1 = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult UploadSlider2_image(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_Slider pm = new Mst_Slider();
                    pm = (db.Mst_Slider.Where(r => r.SliderID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Image_2 = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult UploadSlider3_image(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_Slider pm = new Mst_Slider();
                    pm = (db.Mst_Slider.Where(r => r.SliderID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Image_3 = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult UploadSlider4_image(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_Slider pm = new Mst_Slider();
                    pm = (db.Mst_Slider.Where(r => r.SliderID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Image_4 = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        #endregion

        #region HomePageInfo
        [AcceptVerbs("GET")]
        public JsonResult EditHomePageInfo()
        {
            HomePagerClass homepageclass = new HomePagerClass();
            HomePageData homepagedata = new HomePageData();

            homepagedata.homepage = (from abt in db.Mst_Home
                                     where abt.IsActive == true
                                     select new HomePagerClass
                                     {
                                         HomePageID = abt.HomePageID,
                                         Title1 = abt.Title1,
                                         SubTitle1 = abt.SubTitle1,
                                         Content1 = abt.Content1,
                                         Title2 = abt.Title2,
                                         Content2 = abt.Content2,
                                         Title3 = abt.Title3,
                                         Content3 = abt.Content3,
                                         Title4 = abt.Title4,
                                         Content4 = abt.Content4,
                                         Image_1 = abt.Image_1,
                                         Image_2 = abt.Image_2,
                                         Image_4 = abt.Image_4
                                     }).ToList();


            if (homepagedata.homepage.Count > 0)
            {
                var aboutus = "";
                homepagedata.success = true;
                var result = new { aboutus = homepagedata.homepage, homepagedata.success };
                //return Request.CreateResponse(HttpStatusCode.OK, result);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                homepagedata.success = false;
                //return Request.CreateResponse(HttpStatusCode.OK, aboutdata);
                return Json(homepagedata, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UpdateHomePageInfo(HomePagerClass abtus)
        {
            try
            {
                int HomePageID = Convert.ToInt32(abtus.HomePageID);
                Mst_Home bmst = new Mst_Home();
                bmst = db.Mst_Home.Where(a => a.HomePageID == HomePageID).SingleOrDefault();
                bmst.Title1 = abtus.Title1;
                bmst.SubTitle1 = abtus.SubTitle1;
                bmst.Title2 = abtus.Title2;
                bmst.Title3 = abtus.Title3;
                bmst.Title4 = abtus.Title4;
                bmst.Content1 = abtus.Content1;
                bmst.Content2 = abtus.Content2;
                bmst.Content3 = abtus.Content3;
                bmst.Content4 = abtus.Content4;
                bmst.CreateDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                db.SaveChanges();
                return Json("Success");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult UploadHomeImage_1(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string homeid = collection["Id"];
                    int tid = Convert.ToInt32(homeid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_Home pm = new Mst_Home();
                    pm = (db.Mst_Home.Where(r => r.HomePageID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Image_1 = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Image_1 Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult UploadHomeImage_2(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string homeid = collection["Id"];
                    int tid = Convert.ToInt32(homeid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_Home pm = new Mst_Home();
                    pm = (db.Mst_Home.Where(r => r.HomePageID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Image_2 = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Image_1 Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        public ActionResult UploadHomeImage_3(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string homeid = collection["Id"];
                    int tid = Convert.ToInt32(homeid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_Home pm = new Mst_Home();
                    pm = (db.Mst_Home.Where(r => r.HomePageID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Image_4 = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Image_1 Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        #endregion

        #region ServicesType
        [AcceptVerbs("GET")]
        public JsonResult BindServiceType()
        {
            ServiceTypeData sd = new ServiceTypeData();
            sd.servicetype = (from s in db.Mst_ServicesType
                              orderby s.SortOrder
                              select new ServiceTypeClass
                              {
                                  ServiceTypeID = s.ServiceTypeID,
                                  ServiceType = s.ServiceType,
                                  SortOrder = s.SortOrder,
                                  IsActive = s.IsActive.ToString()
                              }).ToList();


            if (sd.servicetype.Count > 0)
                sd.success = true;
            else
                sd.success = false;
            return Json(sd, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertServiceType(Mst_ServicesType brc)
        {
            try
            {
                var msg = "";
                var success = "";
                int id = 0;
                var result = db.Mst_ServicesType.Where(a => a.ServiceType == brc.ServiceType).ToList();
                if (result != null && result.Count > 0)
                {
                    msg = "Service Type Already Exists..";
                    success = "false";
                }
                else
                {
                    //brc.IsActive = true;
                    db.Mst_ServicesType.Add(brc);
                    db.SaveChanges();
                    id = brc.ServiceTypeID;
                    msg = "Service Type Inserted Successfully..";
                    success = "true";
                }
                var resultm = new { msg, success, id };
                return Json(resultm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AcceptVerbs("GET")]
        public JsonResult EditServiceType(int ServiceTypeID)
        {
            ServiceTypeClass servicetypeclass = new ServiceTypeClass();
            ServiceTypeData servicetypedata = new ServiceTypeData();

            servicetypedata.servicetype = (from emp in db.Mst_ServicesType
                                           where emp.ServiceTypeID == ServiceTypeID

                                           select new ServiceTypeClass
                                           {
                                               ServiceTypeID = emp.ServiceTypeID,
                                               ServiceType = emp.ServiceType,
                                               SortOrder = emp.SortOrder,
                                               IsActive = emp.IsActive.ToString(),
                                               Service_Heading = emp.Service_Heading,
                                               Service_Desc1 = emp.Service_Desc1,
                                               Service_Desc2 = emp.Service_Desc2,
                                               img1_Content1 = emp.img1_Content1,
                                               img2_Conrent2 = emp.img2_Conrent2,
                                               img3_Content3 = emp.img3_Content3,
                                               Service_img1 = emp.Service_img1,
                                               Service_img2 = emp.Service_img2,
                                               Service_ing3 = emp.Service_ing3,
                                               Service_title1 = emp.Service_title1,
                                               Service_title2 = emp.Service_title2,
                                               Service_title3 = emp.Service_title3,
                                               ServiceTypeImage = emp.ServiceTypeImage,
                                               ServiceBanner = emp.ServiceBanner
                                           }).ToList();



            if (servicetypedata.servicetype.Count > 0)
            {
                var servicetype = "";
                servicetypedata.success = true;
                var result = new { servicetype = servicetypedata.servicetype, servicetypedata.success };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                servicetypedata.success = false;
                return Json(servicetypedata, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UpdateServiceType(Mst_ServicesType brc)
        {
            try
            {
                var msg = "";
                var success = "";
                int id = 0;
                var result = db.Mst_ServicesType.Where(a => a.ServiceTypeID == brc.ServiceTypeID).SingleOrDefault();
                if (result == null)
                {
                    id = 0;
                    msg = "Service Type Already Exists..";
                    success = "false";
                }
                else
                {
                    int ServiceTypeID = brc.ServiceTypeID;
                    Mst_ServicesType emp = new Mst_ServicesType();
                    emp = db.Mst_ServicesType.Where(empp => empp.ServiceTypeID == ServiceTypeID).SingleOrDefault();
                    emp.ServiceType = brc.ServiceType;
                    emp.Service_Heading = brc.Service_Heading;
                    emp.Service_Desc1 = brc.Service_Desc1;
                    emp.Service_Desc2 = brc.Service_Desc2;
                    emp.img1_Content1 = brc.img1_Content1;
                    emp.img2_Conrent2 = brc.img2_Conrent2;
                    emp.img3_Content3 = brc.img3_Content3;
                    emp.Service_title1 = brc.Service_title1;
                    emp.Service_title2 = brc.Service_title2;
                    emp.Service_title3 = brc.Service_title3;

                    emp.SortOrder = brc.SortOrder;
                    emp.IsActive = brc.IsActive;

                    db.SaveChanges();
                    id = emp.ServiceTypeID;
                    msg = "Service Type Updated Successfully..";
                    success = "true";
                }
                var resultm = new { msg, success, id };
                return Json(resultm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AcceptVerbs("GET")]
        public JsonResult BindServiceTypeD()
        {
            ServiceTypeData sd = new ServiceTypeData();
            sd.servicetype = (from s in db.Mst_ServicesType
                              where s.IsActive == true
                              select new ServiceTypeClass
                              {
                                  ServiceTypeID = s.ServiceTypeID,
                                  ServiceType = s.ServiceType
                              }).ToList();

            if (sd.servicetype.Count > 0)
                sd.success = true;
            else
                sd.success = false;

            return Json(sd, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteServiceType(Mst_ServicesType brc)
        {
            try
            {
                var msg = "";
                var success = "";
                int ServiceTypeID = brc.ServiceTypeID;
                Mst_ServicesType emp = new Mst_ServicesType();
                emp = db.Mst_ServicesType.Where(empp => empp.ServiceTypeID == ServiceTypeID).SingleOrDefault();
                db.Mst_ServicesType.Remove(emp);
                db.SaveChanges();
                msg = "Service Type Deleted Successfully..";
                success = "true";
                var resultm = new { msg, success };
                return Json(resultm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult UploadServiceMenuImage(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string serviceid = collection["Id"];
                    int tid = Convert.ToInt32(serviceid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_ServicesType pm = new Mst_ServicesType();
                    pm = (db.Mst_ServicesType.Where(r => r.ServiceTypeID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.ServiceTypeImage = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult UploadServiceBannerImage(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string serviceid = collection["Id"];
                    int tid = Convert.ToInt32(serviceid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_ServicesType pm = new Mst_ServicesType();
                    pm = (db.Mst_ServicesType.Where(r => r.ServiceTypeID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.ServiceBanner = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult UploadServiceImage_1Image(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string serviceid = collection["Id"];
                    int tid = Convert.ToInt32(serviceid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_ServicesType pm = new Mst_ServicesType();
                    pm = (db.Mst_ServicesType.Where(r => r.ServiceTypeID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Service_img1 = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult UploadServiceImage_2Image(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string serviceid = collection["Id"];
                    int tid = Convert.ToInt32(serviceid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_ServicesType pm = new Mst_ServicesType();
                    pm = (db.Mst_ServicesType.Where(r => r.ServiceTypeID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Service_img2 = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult UploadServiceImage_3Image(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string serviceid = collection["Id"];
                    int tid = Convert.ToInt32(serviceid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_ServicesType pm = new Mst_ServicesType();
                    pm = (db.Mst_ServicesType.Where(r => r.ServiceTypeID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Service_ing3 = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        public ActionResult UploadServiceImage(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                    //string filename = Path.GetFileName(Request.Files[i].FileName);  

                    HttpPostedFileBase file = files[0];
                    HttpPostedFileBase file1 = files[1];
                    HttpPostedFileBase file2 = files[2];
                    HttpPostedFileBase file3 = files[3];
                    HttpPostedFileBase file4 = files[4];

                    string fname;
                    string fname1;
                    string fname2;
                    string fname3;
                    string fname4;


                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];

                        string[] testfiles1 = file1.FileName.Split(new char[] { '\\' });
                        fname1 = testfiles1[testfiles1.Length - 1];

                        string[] testfiles2 = file2.FileName.Split(new char[] { '\\' });
                        fname2 = testfiles2[testfiles2.Length - 1];

                        string[] testfiles3 = file3.FileName.Split(new char[] { '\\' });
                        fname3 = testfiles3[testfiles3.Length - 1];

                        string[] testfiles4 = file4.FileName.Split(new char[] { '\\' });
                        fname4 = testfiles4[testfiles4.Length - 1];



                    }
                    else
                    {
                        fname = file.FileName;
                        fname1 = file1.FileName;
                        fname2 = file2.FileName;
                        fname3 = file3.FileName;
                        fname4 = file4.FileName;

                    }

                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    fname1 = Path.Combine(Server.MapPath("~/images/"), fname1);
                    fname2 = Path.Combine(Server.MapPath("~/images/"), fname2);
                    fname3 = Path.Combine(Server.MapPath("~/images/"), fname3);
                    fname4 = Path.Combine(Server.MapPath("~/images/"), fname4);

                    file.SaveAs(fname);
                    file1.SaveAs(fname1);
                    file1.SaveAs(fname2);
                    file1.SaveAs(fname3);
                    file1.SaveAs(fname4);

                    string physicalpath = "images/Products/" + file.FileName;
                    string physicalpath1 = "images/Products/" + file1.FileName;
                    string physicalpath2 = "images/Products/" + file2.FileName;
                    string physicalpath3 = "images/Products/" + file3.FileName;
                    string physicalpath4 = "images/Products/" + file4.FileName;

                    //string physicalpath = "http://diamtrade.cricketclubs.in/images/Products/" + file.FileName;


                    Mst_ServicesType pm = new Mst_ServicesType();
                    pm = (db.Mst_ServicesType.Where(r => r.ServiceTypeID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.ServiceTypeImage = physicalpath;
                        pm.ServiceBanner = physicalpath1;
                        pm.Service_img1 = physicalpath2;
                        pm.Service_img2 = physicalpath3;
                        pm.Service_ing3 = physicalpath4;
                        db.SaveChanges();
                    }

                    // Returns message that successfully uploaded  
                    return Json("Product Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }


        public ActionResult InsertServiceTypeFeature(Mst_Service_Features sf)
        {
            try
            {
                db.Mst_Service_Features.Add(sf);
                db.SaveChanges();
                int i = 0;
                i = sf.Service_Features_Id;
                return Json(i, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public ActionResult UploadServiceFeatureImage(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                    //string filename = Path.GetFileName(Request.Files[i].FileName);  

                    HttpPostedFileBase file = files[0];


                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }

                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);

                    file.SaveAs(fname);


                    string physicalpath = "images/" + file.FileName;

                    //string physicalpath = "http://diamtrade.cricketclubs.in/images/Products/" + file.FileName;


                    Mst_Service_Features pm = new Mst_Service_Features();
                    pm = (db.Mst_Service_Features.Where(r => r.Service_Features_Id == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Service_Freature_img = physicalpath;
                        db.SaveChanges();
                    }

                    // Returns message that successfully uploaded  
                    return Json("service feature Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        public ActionResult BindServiceTypeFeature(int id)
        {
            try
            {
                ServiceTypeFeatureData msf = new ServiceTypeFeatureData();
                msf.servicefeauretype = (from sf in db.Mst_Service_Features
                                         join s in db.Mst_ServicesType on sf.ServiceType_Id equals s.ServiceTypeID
                                         where sf.ServiceType_Id == id
                                         select new ServiceTypeFeatureClass
                                         {
                                             ServiceType_Id = sf.ServiceType_Id,
                                             Service_Features_Id = sf.Service_Features_Id,
                                             Service_Feature_Title = sf.Service_Feature_Title,
                                             Service_Feature_Desc = sf.Service_Feature_Desc,
                                             ServiceFeatureName = s.ServiceType
                                         }).ToList();
                if (msf.servicefeauretype.Count > 0)
                    msf.success = true;
                else
                    msf.success = false;
                return Json(msf, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [AcceptVerbs("GET")]
        public JsonResult EditServiceTypeFeature(int ServiceTypeFeatureID)
        {
            ServiceTypeFeatureClass servicetypefeatureclass = new ServiceTypeFeatureClass();
            ServiceTypeFeatureData servicetypefeaturedata = new ServiceTypeFeatureData();

            servicetypefeaturedata.servicefeauretype = (from sf in db.Mst_Service_Features
                                                        join s in db.Mst_ServicesType on sf.ServiceType_Id equals s.ServiceTypeID
                                                        where sf.Service_Features_Id == ServiceTypeFeatureID

                                                        select new ServiceTypeFeatureClass
                                                        {
                                                            Service_Features_Id = sf.Service_Features_Id,
                                                            Service_Feature_Desc = sf.Service_Feature_Desc,
                                                            Service_Feature_Title = sf.Service_Feature_Title,
                                                            Service_Freature_img = sf.Service_Freature_img,
                                                            ServiceFeatureName = s.ServiceType,
                                                            ServiceType_Id = sf.ServiceType_Id
                                                        }).ToList();



            if (servicetypefeaturedata.servicefeauretype.Count > 0)
            {
                var servicetype = "";
                servicetypefeaturedata.success = true;
                var result = new { servicetype = servicetypefeaturedata.servicefeauretype, servicetypefeaturedata.success };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                servicetypefeaturedata.success = false;
                return Json(servicetypefeaturedata, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UpdateServiceTypeFeature(Mst_Service_Features sf)
        {
            try
            {

                int i = 0;
                var result = db.Mst_Service_Features.Where(a => a.Service_Features_Id == sf.Service_Features_Id).SingleOrDefault();
                if (result == null)
                {
                    i = 0;
                }
                else
                {

                    Mst_Service_Features emp = new Mst_Service_Features();
                    emp = db.Mst_Service_Features.Where(empp => empp.Service_Features_Id == sf.Service_Features_Id).SingleOrDefault();
                    emp.Service_Feature_Desc = sf.Service_Feature_Desc;
                    emp.Service_Feature_Title = sf.Service_Feature_Title;
                    emp.ServiceType_Id = sf.ServiceType_Id;
                    db.SaveChanges();
                    i = emp.Service_Features_Id;
                }

                return Json(i, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        public JsonResult DeleteServiceTypeFeature(Mst_Service_Features brc)
        {
            try
            {
                var msg = "";
                var success = "";
                int servicefeatureid = brc.Service_Features_Id;
                Mst_Service_Features emp = new Mst_Service_Features();
                emp = db.Mst_Service_Features.Where(empp => empp.Service_Features_Id == servicefeatureid).SingleOrDefault();
                db.Mst_Service_Features.Remove(emp);
                db.SaveChanges();
                msg = "Service Type Feature Deleted Successfully..";
                success = "true";
                var resultm = new { msg, success };
                return Json(resultm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Services
        [AcceptVerbs("GET")]
        public JsonResult BindService()
        {
            ServicesData sd = new ServicesData();
            sd.services = (from s in db.Mst_Services
                           join stype in db.Mst_ServicesType on s.ServiceTypeID equals stype.ServiceTypeID into fstype
                           from stype in fstype.DefaultIfEmpty()
                           select new ServicesClass
                           {
                               ServiceID = s.ServiceID,
                               ServiceTypeID = s.ServiceTypeID,
                               ServiceTypeName = stype.ServiceType,
                               ServiceName = s.ServiceName,
                               SortOrder = s.SortOrder,
                               IsActive = s.IsActive.ToString()
                           }).ToList();

            if (sd.services.Count > 0)
                sd.success = true;
            else
                sd.success = false;
            return Json(sd, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertServices(Mst_Services brc)
        {
            try
            {
                var msg = "";
                var success = "";
                var result = db.Mst_Services.Where(a => a.ServiceName == brc.ServiceName && a.ServiceTypeID == brc.ServiceTypeID).ToList();
                if (result != null && result.Count > 0)
                {
                    msg = "Service Already Exists..";
                    success = "false";
                }
                else
                {
                    //brc.IsActive = true;
                    db.Mst_Services.Add(brc);
                    db.SaveChanges();
                    msg = "Service Inserted Successfully..";
                    success = "true";
                }
                var resultm = new { msg, success };
                return Json(resultm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AcceptVerbs("GET")]
        public JsonResult EditService(int ServiceID)
        {
            ServicesClass serviceclass = new ServicesClass();
            ServicesData servicedata = new ServicesData();

            servicedata.services = (from emp in db.Mst_Services
                                    join stype in db.Mst_ServicesType on emp.ServiceTypeID equals stype.ServiceTypeID into fstype
                                    from stype in fstype.DefaultIfEmpty()
                                    where emp.ServiceID == ServiceID
                                    select new ServicesClass
                                    {
                                        ServiceID = emp.ServiceID,
                                        ServiceTypeID = emp.ServiceTypeID,
                                        ServiceTypeName = stype.ServiceType,
                                        ServiceName = emp.ServiceName,
                                        SortOrder = emp.SortOrder,
                                        IsActive = emp.IsActive.ToString()
                                    }).ToList();

            if (servicedata.services.Count > 0)
            {
                var servicetype = "";
                servicedata.success = true;
                var result = new { servicetype = servicedata.services, servicedata.success };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                servicedata.success = false;
                return Json(servicedata, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UpdateServices(Mst_Services brc)
        {
            try
            {
                var msg = "";
                var success = "";
                var result = db.Mst_Services.Where(a => a.ServiceName == brc.ServiceName && a.ServiceTypeID == brc.ServiceTypeID && a.ServiceID != brc.ServiceID).ToList();
                if (result != null && result.Count > 0)
                {
                    msg = "Service Already Exists..";
                    success = "false";
                }
                else
                {
                    int ServiceID = brc.ServiceID;
                    Mst_Services emp = new Mst_Services();
                    emp = db.Mst_Services.Where(empp => empp.ServiceID == ServiceID).SingleOrDefault();
                    emp.ServiceTypeID = brc.ServiceTypeID;
                    emp.ServiceName = brc.ServiceName;
                    emp.SortOrder = brc.SortOrder;
                    emp.IsActive = brc.IsActive;

                    db.SaveChanges();
                    msg = "Service Updated Successfully..";
                    success = "true";
                }
                var resultm = new { msg, success };
                return Json(resultm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult DeleteService(Mst_Services brc)
        {
            try
            {
                var msg = "";
                var success = "";
                int ServiceID = brc.ServiceID;
                Mst_Services emp = new Mst_Services();
                emp = db.Mst_Services.Where(empp => empp.ServiceID == ServiceID).SingleOrDefault();
                db.Mst_Services.Remove(emp);
                db.SaveChanges();
                msg = "Service Deleted Successfully..";
                success = "true";
                var resultm = new { msg, success };
                return Json(resultm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Testimonials
        //[AcceptVerbs("GET")]
        //public JsonResult BindTestimonialMain()
        //{
        //    TestimonialData sd = new TestimonialData();
        //    sd.testimoni = (from s in db.Mst_TestimonialMain
        //                    select new TestimonialClass
        //                    {
        //                        TestimonialID = s.TestMID,
        //                        TestiTitle = s.TestMTitle
        //                    }).ToList();
        //    if (sd.testimoni.Count > 0)
        //        sd.success = true;
        //    else
        //        sd.success = false;
        //    return Json(sd, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult UpdateTestimonialMain(Mst_TestimonialMain brc)
        //{
        //    try
        //    {
        //        var msg = "";
        //        var success = "";
        //        int TestMID = brc.TestMID;
        //        Mst_TestimonialMain emp = new Mst_TestimonialMain();
        //        emp = db.Mst_TestimonialMain.Where(empp => empp.TestMID == TestMID).SingleOrDefault();
        //        emp.TestMTitle = brc.TestMTitle;
        //        db.SaveChanges();
        //        msg = "Testimonial Description Updated Successfully..";
        //        success = "true";
        //        var resultm = new { msg, success };
        //        return Json(resultm, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        [AcceptVerbs("GET")]
        public JsonResult BindTestimonial()
        {
            TestimonialData sd = new TestimonialData();
            sd.testimoni = (from s in db.Mst_Testimonial
                            where s.IsActive == true
                            orderby  s.SortOrder ascending
                            select new TestimonialClass
                            {
                                TestimonialID = s.TestimonialID,
                                TestiTitle = s.TestiTitle,
                                TestimComments = s.TestimComments,
                                PersonName = s.PersonName,
                                SortOrder = s.SortOrder,
                                IsActive = s.IsActive.ToString(),
                                TestimonialMainTitle = s.TestimonialMainTitle,
                                PersonImage = s.PersonImage
                            }).ToList();


            if (sd.testimoni.Count > 0)
                sd.success = true;
            else
                sd.success = false;
            return Json(sd, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditDynamicTestimonial(int testiid)
        {
            var data = (db.Mst_Testimonial.Where(r => r.TestimonialID == testiid)).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertTestimonial(Mst_Testimonial brc)
        {
            try
            {
                var msg = "";
                var success = "";
                db.Mst_Testimonial.Add(brc);
                db.SaveChanges();
                msg = "Testimonial Inserted Successfully..";
                success = "true";
                int id = brc.TestimonialID;
                var resultm = new { msg, success, id };
                return Json(resultm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult UploadFile(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //Get all files from Request object
                    HttpFileCollectionBase files = Request.Files;
                    string testimonialid = collection["Id"];
                    int tid = Convert.ToInt32(testimonialid);
                    for (int i = 0; i < files.Count; i++)
                    {
                        string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";
                        string filename = Path.GetFileName(Request.Files[i].FileName);

                        HttpPostedFileBase file = files[i];
                        string fname;

                        //Checking for Internet Explorer
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        // Get the complete folder path and store the file inside it.
                        fname = Path.Combine(Server.MapPath("~/images/Testimonial/"), fname);
                        file.SaveAs(fname);
                        string physicalpath = "images/Testimonial/" + file.FileName;
                        // string physicalpath = "http://18.218.220.245:8080/DiamTrade.com/images/Testimonial/" + file.FileName;


                        Mst_Testimonial tm = new Mst_Testimonial();
                        tm = (db.Mst_Testimonial.Where(r => r.TestimonialID == tid)).SingleOrDefault();
                        if (tm != null)
                        {
                            tm.PersonImage = physicalpath;
                            db.SaveChanges();
                        }
                    }
                    //Returns message that successfully uploaded
                    return Json("Testimonial Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }


        [AcceptVerbs("GET")]
        public JsonResult EditTestimonial(int TestimonialID)
        {
            TestimonialClass servicetypeclass = new TestimonialClass();
            TestimonialData servicetypedata = new TestimonialData();

            servicetypedata.testimoni = (from emp in db.Mst_Testimonial
                                         where emp.TestimonialID == TestimonialID
                                         select new TestimonialClass
                                         {
                                             TestimonialID = emp.TestimonialID,
                                             TestiTitle = emp.TestiTitle,
                                             PersonName = emp.PersonName,
                                             SortOrder = emp.SortOrder,
                                             IsActive = emp.IsActive.ToString(),
                                             TestimComments = emp.TestimComments,
                                             PersonImage = emp.PersonImage
                                         }).ToList();



            if (servicetypedata.testimoni.Count > 0)
            {
                var Testimoni = "";
                servicetypedata.success = true;
                var result = new { Testimoni = servicetypedata.testimoni, servicetypedata.success };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                servicetypedata.success = false;
                return Json(servicetypedata, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UpdateTestimonial(Mst_Testimonial brc)
        {
            try
            {
                var msg = "";
                var success = "";

                int TestimonialID = brc.TestimonialID;
                Mst_Testimonial emp = new Mst_Testimonial();
                emp = db.Mst_Testimonial.Where(empp => empp.TestimonialID == TestimonialID).SingleOrDefault();
                emp.TestimComments = brc.TestimComments;
                emp.PersonName = brc.PersonName;
                emp.TestiTitle = brc.TestiTitle;
                emp.SortOrder = brc.SortOrder;
                emp.IsActive = brc.IsActive;
                db.SaveChanges();
                int id = emp.TestimonialID;
                msg = "Testimonial Updated Successfully..";
                success = "true";

                var resultm = new { msg, success, TestimonialID };
                return Json(resultm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult DeleteTestimonial(Mst_Testimonial brc)
        {
            try
            {
                var msg = "";
                var success = "";
                int TestimonialID = brc.TestimonialID;
                Mst_Testimonial emp = new Mst_Testimonial();
                emp = db.Mst_Testimonial.Where(empp => empp.TestimonialID == TestimonialID).SingleOrDefault();
                db.Mst_Testimonial.Remove(emp);
                db.SaveChanges();
                msg = "Testimonial Deleted Successfully..";
                success = "true";
                var resultm = new { msg, success };
                return Json(resultm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Products
        [AcceptVerbs("GET")]
        public JsonResult BindProducts()
        {
            ProductsData sd = new ProductsData();
            sd.products = (from s in db.Mst_Product
                           select new ProductsClass
                           {
                               ProductID = s.ProductID,
                               ProductName = s.ProductName,
                               SortOrder = s.SortOrder,
                               IsActive = s.IsActive.ToString()
                           }).ToList();


            if (sd.products.Count > 0)
                sd.success = true;
            else
                sd.success = false;
            return Json(sd, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Insertproducts(Mst_Product brc)
        {
            try
            {
                var msg = "";
                var success = "";
                int id = 0;
                var result = db.Mst_Product.Where(a => a.ProductName == brc.ProductName).ToList();
                if (result != null && result.Count > 0)
                {
                    msg = "Product Name Already Exists..";
                    success = "false";
                }
                else
                {
                    //brc.IsActive = true;
                    brc.CreateDate = DateTime.UtcNow;
                    db.Mst_Product.Add(brc);
                    db.SaveChanges();
                    id = brc.ProductID;
                    msg = "Product Inserted Successfully..";
                    success = "true";
                }
                var resultm = new { msg, success, id };
                return Json(resultm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AcceptVerbs("GET")]
        public JsonResult EditProducts(int ProductID)
        {
            ProductsClass productclass = new ProductsClass();
            ProductsData productdata = new ProductsData();

            productdata.products = (from emp in db.Mst_Product
                                    where emp.ProductID == ProductID

                                    select new ProductsClass
                                    {
                                        ProductID = emp.ProductID,
                                        ProductName = emp.ProductName,
                                        SortOrder = emp.SortOrder,
                                        ShortContent = emp.ShortContent,
                                        IsActive = emp.IsActive.ToString(),
                                        ProductLogo = emp.ProductLogo,
                                        ProductImage = emp.ProductImage
                                    }).ToList();

            if (productdata.products.Count > 0)
            {
                var products = "";
                productdata.success = true;
                var result = new { products = productdata.products, productdata.success };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                productdata.success = false;
                return Json(productdata, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UpdateProducts(Mst_Product brc)
        {
            try
            {
                var msg = "";
                var success = "";
                int id = 0;
                var result = db.Mst_Product.Where(a => a.ProductName == brc.ProductName && a.ProductID != brc.ProductID).ToList();
                if (result != null && result.Count > 0)
                {
                    msg = "Product Name Already Exists..";
                    success = "false";
                }
                else
                {
                    int ProductID = brc.ProductID;
                    Mst_Product emp = new Mst_Product();
                    emp = db.Mst_Product.Where(empp => empp.ProductID == ProductID).SingleOrDefault();
                    emp.ProductName = brc.ProductName;
                    emp.SortOrder = brc.SortOrder;
                    emp.ShortContent = brc.ShortContent;
                    emp.IsActive = brc.IsActive;
                    db.SaveChanges();
                    id = emp.ProductID;
                    msg = "Product Updated Successfully..";
                    success = "true";
                }
                var resultm = new { msg, success, id };
                return Json(resultm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult DeleteProducts(Mst_Product brc)
        {
            try
            {
                var msg = "";
                var success = "";
                int ProductsID = brc.ProductID;
                Mst_Product emp = new Mst_Product();
                emp = db.Mst_Product.Where(empp => empp.ProductID == ProductsID).SingleOrDefault();
                db.Mst_Product.Remove(emp);
                db.SaveChanges();
                msg = "Products Deleted Successfully..";
                success = "true";
                var resultm = new { msg, success };
                return Json(resultm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult UploadProductImage(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/images/Products/"), fname);
                        file.SaveAs(fname);
                        string physicalpath = "images/Products/" + file.FileName;
                        //string physicalpath = "http://diamtrade.cricketclubs.in/images/Products/" + file.FileName;


                        Mst_Product pm = new Mst_Product();
                        pm = (db.Mst_Product.Where(r => r.ProductID == tid)).SingleOrDefault();
                        if (pm != null)
                        {
                            pm.ProductImage = physicalpath;
                            db.SaveChanges();
                        }
                    }
                    // Returns message that successfully uploaded  
                    return Json("Product Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult UploadProductLogo(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/images/Products/"), fname);
                        file.SaveAs(fname);
                        string physicalpath = "images/Products/" + file.FileName;
                        //string physicalpath = "http://diamtrade.cricketclubs.in/images/Products/" + file.FileName;


                        Mst_Product pm = new Mst_Product();
                        pm = (db.Mst_Product.Where(r => r.ProductID == tid)).SingleOrDefault();
                        if (pm != null)
                        {
                            pm.ProductLogo = physicalpath;
                            db.SaveChanges();
                        }
                    }
                    // Returns message that successfully uploaded  
                    return Json("Product logo Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }


        #endregion

        #region Inquiry
        [AcceptVerbs("GET")]
        public JsonResult EditInquiry()
        {
            InquiryClass inquiryclass = new InquiryClass();
            InquiryData inquirydata = new InquiryData();

            inquirydata.inquiries = (from abt in db.Mst_Inquiry
                                     where abt.IsActive == true
                                     select new InquiryClass
                                     {
                                         InquiryID = abt.InquiryID,
                                         InquiryDesc = abt.InquiryDesc,
                                         Address = abt.Address,
                                         EmailID = abt.EmailID,
                                         MobileNo = abt.MobileNo
                                     }).ToList();


            if (inquirydata.inquiries.Count > 0)
            {
                var inquiries = "";
                inquirydata.success = true;
                var result = new { inquiries = inquirydata.inquiries, inquirydata.success };
                //return Request.CreateResponse(HttpStatusCode.OK, result);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                inquirydata.success = false;
                //return Request.CreateResponse(HttpStatusCode.OK, aboutdata);
                return Json(inquirydata, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UpdateInquiry(InquiryClass abtus)
        {
            try
            {
                int InquiryID = Convert.ToInt32(abtus.InquiryID);
                Mst_Inquiry bmst = new Mst_Inquiry();
                bmst = db.Mst_Inquiry.Where(a => a.InquiryID == InquiryID).SingleOrDefault();
                bmst.InquiryDesc = abtus.InquiryDesc;
                bmst.Address = abtus.Address;
                bmst.MobileNo = abtus.MobileNo;
                bmst.EmailID = abtus.EmailID;
                bmst.CreateDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                db.SaveChanges();
                return Json("Success");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region InquiryMessages
        [AcceptVerbs("GET")]
        public JsonResult BindInquiryMessages()
        {
            InquiryMessageData sd = new InquiryMessageData();
            sd.inquirymsg = (from s in db.Mst_InquiryMessage
                             select new InquiryMessageClass
                             {
                                 InquiryMessageID = s.InquiryMessageID,
                                 InquiryType = s.InquiryType,
                                 FullName = s.FullName,
                                 MobileNo = s.MobileNo,
                                 EmailID = s.EmailID,
                                 InquiryMessage = s.InquiryMessage,
                                 CreateDate = s.CreateDate.ToString(),
                                 IsActive = s.IsActive.ToString()
                             }).ToList();

            if (sd.inquirymsg.Count > 0)
                sd.success = true;
            else
                sd.success = false;
            return Json(sd, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ViewInquiryMessages(int InquiryMessageID)
        {
            InquiryMessageData sd = new InquiryMessageData();
            sd.inquirymsg = (from s in db.Mst_InquiryMessage
                             where s.InquiryMessageID == InquiryMessageID
                             select new InquiryMessageClass
                             {
                                 InquiryMessage = s.InquiryMessage
                             }).ToList();

            if (sd.inquirymsg.Count > 0)
                sd.success = true;
            else
                sd.success = false;
            return Json(sd, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DelInquiryMsg(Mst_InquiryMessage brc)
        {
            try
            {
                var msg = "";
                var success = "";
                int InquiryMessageID = brc.InquiryMessageID;
                Mst_InquiryMessage emp = new Mst_InquiryMessage();
                emp = db.Mst_InquiryMessage.Where(empp => empp.InquiryMessageID == InquiryMessageID).SingleOrDefault();
                db.Mst_InquiryMessage.Remove(emp);
                db.SaveChanges();
                msg = "Inquiry Message Deleted Successfully..";
                success = "true";
                var resultm = new { msg, success };
                return Json(resultm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult InsertInquiry(Mst_InquiryMessage mi)
        {
            try
            {
                int i = 0;
                Mst_InquiryMessage im = new Mst_InquiryMessage();
                im.FullName = mi.FullName;
                im.MobileNo = mi.MobileNo;
                im.EmailID = mi.EmailID;
                im.InquiryMessage = mi.InquiryMessage;
                im.InquiryType = mi.InquiryType;
                im.IsActive = true;
                im.CreateDate = DateTime.UtcNow;
                db.Mst_InquiryMessage.Add(im);
                db.SaveChanges();
                i = im.InquiryMessageID;
                if (i > 0)
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(Helper.inqiuryemail);
                    mail.From = new MailAddress(Helper.networkemail);
                    mail.Subject = "New Inquiry For Diamtrade";
                    string Body = "Dear Sir/Mam <br /><br />" +
                        "Hello,<br />" +
                        "<b>Person Name :</b>" + mi.FullName + "<br />" +
                        "<b>Mobile Number :</b>" + mi.MobileNo + "<br />" +
                        "<b>Email-ID :</b>" + mi.EmailID + "<br />" +
                         "<b>Inquiry Type :</b>" + mi.InquiryType + "<br />" +
                        "<b>Message :</b>" + mi.InquiryMessage + "<br />";

                    mail.Body = Body;
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = Helper.hostemailid;
                    smtp.Port = Convert.ToInt32(Helper.networkemailport);
                    //smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential(Helper.networkemail,Helper.networkemailpass); // Enter seders User name and password  
                    smtp.EnableSsl = Convert.ToBoolean(Helper.networkemailssl);
                    smtp.Send(mail);
                    //MailMessage mail = new MailMessage();
                    //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    //mail.From = new MailAddress("your_email_address@gmail.com");
                    //mail.To.Add("to_address");
                    //mail.Subject = "Test Mail";
                    //mail.Body = "This is for testing SMTP mail from GMAIL";

                    //SmtpServer.Port = 587;
                    //SmtpServer.Credentials = new System.Net.NetworkCredential("username", "password");
                    //SmtpServer.EnableSsl = true;

                    //SmtpServer.Send(mail);
                    //MessageBox.Show("mail Send");
                    return Json(i, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(i, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult InsertDemoReq(string strEmail)
        {
            try
            {
                int i = 0;
                Mst_DemoRequest im = new Mst_DemoRequest();
                im.EmailID = strEmail;
                im.IsActive = true;
                im.CreateDate = DateTime.UtcNow;
                db.Mst_DemoRequest.Add(im);
                db.SaveChanges();
                i = im.DemoRequestID;
                if (i > 0)
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(Helper.demorequest);
                    mail.From = new MailAddress(Helper.networkemail);
                    mail.Subject = "New Request for Demo of Diamtrade";
                    string Body = "Dear Sir/Mam <br /><br />" +
                        "Hello,<br />" +
                        "<b>Email-ID :</b>" + strEmail + "<br />" +
                         "<b>Inquiry Type : Request for Demo</b><br />";

                    mail.Body = Body;
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = Helper.hostemailid;
                    smtp.Port = Convert.ToInt32(Helper.networkemailport);
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential(Helper.networkemail, Helper.networkemailpass); // Enter seders User name and password  
                    smtp.EnableSsl = Convert.ToBoolean(Helper.networkemailssl);
                    smtp.Send(mail);
                    return Json(i, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(i, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        #endregion

        #region ContactUs
        [AcceptVerbs("GET")]
        public JsonResult EditContactUs()
        {
            ContactClass contactclass = new ContactClass();
            ContactData contactdata = new ContactData();

            contactdata.contacts = (from abt in db.Mst_ContactUs
                                    where abt.IsActive == true
                                    select new ContactClass
                                    {
                                        ContactusID = abt.ContactusID,
                                        CotactUsDesc = abt.CotactUsDesc,
                                        CotactTitle = abt.CotactTitle,
                                        Address = abt.Address,
                                        EmailID = abt.EmailID,
                                        MobileNo = abt.MobileNo,
                                        SupportName = abt.SupportName,
                                        TeamViewerName = abt.TeamViewerName,
                                        Image_Banner = abt.Image_Banner
                                    }).ToList();


            if (contactdata.contacts.Count > 0)
            {
                var contacts = "";
                contactdata.success = true;
                var result = new { contacts = contactdata.contacts, contactdata.success };
                //return Request.CreateResponse(HttpStatusCode.OK, result);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                contactdata.success = false;
                //return Request.CreateResponse(HttpStatusCode.OK, aboutdata);
                return Json(contactdata, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UpdateContactUs(ContactClass abtus)
        {
            try
            {
                int ContactusID = Convert.ToInt32(abtus.ContactusID);
                Mst_ContactUs bmst = new Mst_ContactUs();
                bmst = db.Mst_ContactUs.Where(a => a.ContactusID == ContactusID).SingleOrDefault();
                bmst.CotactTitle = abtus.CotactTitle;
                bmst.CotactUsDesc = abtus.CotactUsDesc;
                bmst.Address = abtus.Address;
                bmst.MobileNo = abtus.MobileNo;
                bmst.EmailID = abtus.EmailID;
                bmst.TeamViewerName = abtus.TeamViewerName;
                bmst.SupportName = abtus.SupportName;
                bmst.CreateDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                db.SaveChanges();
                return Json("Success");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult UploadContactUsBanner_image(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_ContactUs pm = new Mst_ContactUs();
                    pm = (db.Mst_ContactUs.Where(r => r.ContactusID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Image_Banner = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        #endregion

        #region ContactMessages
        public JsonResult InsertContact(Mst_CotactMessage mi)
        {
            try
            {
                int i = 0;
                Mst_CotactMessage im = new Mst_CotactMessage();
                im.FirstName = mi.FirstName;
                im.MobileNo = mi.MobileNo;
                im.EmailID = mi.EmailID;
                im.LastName = mi.LastName;
                im.MessageContent = mi.MessageContent;
                im.IsActive = true;
                im.CreateDate = DateTime.UtcNow;
                db.Mst_CotactMessage.Add(im);
                db.SaveChanges();
                i = im.ContactMessageID;
                if (i > 0)
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(Helper.contactus);
                    mail.From = new MailAddress(Helper.networkemail);
                    mail.Subject = "New Contact For Diamtrade";
                    string Body = "Dear Sir/Mam <br /><br />" +
                        "Hello,<br />" +
                        "<b>Person Name :</b>" + im.FirstName + "" + im.LastName + "<br />" +
                        "<b>Mobile Number :</b>" + mi.MobileNo + "<br />" +
                        "<b>Email-ID :</b>" + mi.EmailID + "<br />" +
                        "<b>Message :</b>" + mi.MessageContent + "<br />";

                    mail.Body = Body;
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = Helper.hostemailid;
                    smtp.Port = Convert.ToInt32(Helper.networkemailport);
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential(Helper.networkemail, Helper.networkemailpass); // Enter seders User name and password  
                    smtp.EnableSsl = Convert.ToBoolean(Helper.networkemailssl);
                    smtp.Send(mail);
                    //MailMessage mail = new MailMessage();
                    //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    //mail.From = new MailAddress("your_email_address@gmail.com");
                    //mail.To.Add("to_address");
                    //mail.Subject = "Test Mail";
                    //mail.Body = "This is for testing SMTP mail from GMAIL";

                    //SmtpServer.Port = 587;
                    //SmtpServer.Credentials = new System.Net.NetworkCredential("username", "password");
                    //SmtpServer.EnableSsl = true;

                    //SmtpServer.Send(mail);
                    //MessageBox.Show("mail Send");
                    return Json(i, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(i, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [AcceptVerbs("GET")]
        public JsonResult BindContactMessages()
        {
            ContactMessageData sd = new ContactMessageData();
            sd.contactmsg = (from s in db.Mst_CotactMessage
                             select new ContactMessageClass
                             {
                                 ContactMessageID = s.ContactMessageID,
                                 ContactMessage = s.MessageContent,
                                 FirstName = s.FirstName,
                                 LastName = s.LastName,
                                 MobileNo = s.MobileNo,
                                 EmailID = s.EmailID,
                                 CreateDate = s.CreateDate.ToString(),
                                 IsActive = s.IsActive.ToString()
                             }).ToList();

            if (sd.contactmsg.Count > 0)
                sd.success = true;
            else
                sd.success = false;
            return Json(sd, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ViewContactMessages(int ContactMessageID)
        {
            ContactMessageData sd = new ContactMessageData();
            sd.contactmsg = (from s in db.Mst_CotactMessage
                             where s.ContactMessageID == ContactMessageID
                             select new ContactMessageClass
                             {
                                 ContactMessage = s.MessageContent
                             }).ToList();

            if (sd.contactmsg.Count > 0)
                sd.success = true;
            else
                sd.success = false;
            return Json(sd, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DelContactMsg(Mst_CotactMessage brc)
        {
            try
            {
                var msg = "";
                var success = "";
                int ContactMessageID = brc.ContactMessageID;
                Mst_CotactMessage emp = new Mst_CotactMessage();
                emp = db.Mst_CotactMessage.Where(empp => empp.ContactMessageID == ContactMessageID).SingleOrDefault();
                db.Mst_CotactMessage.Remove(emp);
                db.SaveChanges();
                msg = "Contact Message Deleted Successfully..";
                success = "true";
                var resultm = new { msg, success };
                return Json(resultm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Career
        [AcceptVerbs("GET")]
        public JsonResult EditCareerInfo()
        {
            CareerInfoClass careerclass = new CareerInfoClass();
            CareerInfoData careerdata = new CareerInfoData();

            careerdata.careerinfo = (from abt in db.Mst_Career
                                         //where abt.IsActive == "True"
                                     select new CareerInfoClass
                                     {
                                         CarrersID = abt.CarrersID,
                                         CarrersTitle = abt.CarrersTitle,
                                         SubTitle = abt.SubTitle,
                                         Image_Banner = abt.Image_Banner

                                     }).ToList();


            if (careerdata.careerinfo.Count > 0)
            {
                var careerinfo = "";
                careerdata.success = true;
                var result = new { careerinfo = careerdata.careerinfo, careerdata.success };
                //return Request.CreateResponse(HttpStatusCode.OK, result);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                careerdata.success = false;
                //return Request.CreateResponse(HttpStatusCode.OK, aboutdata);
                return Json(careerdata, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UpdateCareerInfo(CareerInfoClass abtus)
        {
            try
            {
                int CarrersID = Convert.ToInt32(abtus.CarrersID);
                Mst_Career bmst = new Mst_Career();
                bmst = db.Mst_Career.Where(a => a.CarrersID == CarrersID).SingleOrDefault();
                bmst.CarrersTitle = abtus.CarrersTitle;
                bmst.SubTitle = abtus.SubTitle;
                bmst.CreateDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                db.SaveChanges();
                return Json("Success");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult UploadCarrerBanner_image(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_Career pm = new Mst_Career();
                    pm = (db.Mst_Career.Where(r => r.CarrersID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Image_Banner = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public JsonResult BindCareer()
        {
            CareersData sd = new CareersData();
            sd.careers = (from s in db.Mst_CarrerMulti
                          orderby  s.SortOrder ascending
                          select new CareersClass
                          {
                              CareerMultiID = s.CareerMultiID,
                              CareerDesc = s.CareerDesc,
                              Subject = s.Subject,
                              Expe = s.Expe,
                              Location = s.JobLocation,
                              SortOrder = s.SortOrder,
                              IsActive = s.IsActive.ToString(),
                          }).ToList();


            if (sd.careers.Count > 0)
                sd.success = true;
            else
                sd.success = false;
            return Json(sd, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertCareerMulti(Mst_CarrerMulti brc)
        {
            try
            {
                var msg = "";
                var success = "";
                //brc.IsActive = true;
                brc.CreateDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                db.Mst_CarrerMulti.Add(brc);
                db.SaveChanges();
                msg = "Career Inserted Successfully..";
                success = "true";
                var resultm = new { msg, success };
                return Json(resultm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AcceptVerbs("GET")]
        public JsonResult EditCareers(int CareerMultiID)
        {
            CareersClass servicetypeclass = new CareersClass();
            CareersData servicetypedata = new CareersData();

            servicetypedata.careers = (from emp in db.Mst_CarrerMulti
                                       where emp.CareerMultiID == CareerMultiID
                                       select new CareersClass
                                       {
                                           CareerMultiID = emp.CareerMultiID,
                                           CareerDesc = emp.CareerDesc,
                                           Subject = emp.Subject,
                                           Location = emp.JobLocation,
                                           Expe = emp.Expe,
                                           SortOrder = emp.SortOrder,
                                           IsActive = emp.IsActive.ToString(),

                                       }).ToList();



            if (servicetypedata.careers.Count > 0)
            {
                var careers = "";
                servicetypedata.success = true;
                var result = new { careers = servicetypedata.careers, servicetypedata.success };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                servicetypedata.success = false;
                return Json(servicetypedata, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UpdateCareers(Mst_CarrerMulti brc)
        {
            try
            {
                var msg = "";
                var success = "";

                int CareerMultiID = brc.CareerMultiID;
                Mst_CarrerMulti emp = new Mst_CarrerMulti();
                emp = db.Mst_CarrerMulti.Where(empp => empp.CareerMultiID == CareerMultiID).SingleOrDefault();
                emp.CareerDesc = brc.CareerDesc;
                emp.Subject = brc.Subject;
                emp.JobLocation = brc.JobLocation;
                emp.Expe = brc.Expe;
                emp.SortOrder = brc.SortOrder;
                emp.IsActive = brc.IsActive;
                db.SaveChanges();
                msg = "Career Updated Successfully..";
                success = "true";

                var resultm = new { msg, success };
                return Json(resultm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult DeleteCareers(Mst_CarrerMulti brc)
        {
            try
            {
                var msg = "";
                var success = "";
                int CareerIDMulti = brc.CareerMultiID;
                Mst_CarrerMulti emp = new Mst_CarrerMulti();
                emp = db.Mst_CarrerMulti.Where(empp => empp.CareerMultiID == CareerIDMulti).SingleOrDefault();
                db.Mst_CarrerMulti.Remove(emp);
                db.SaveChanges();
                msg = "Career Deleted Successfully..";
                success = "true";
                var resultm = new { msg, success };
                return Json(resultm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult InsertCareerSend(Mst_CareersSend cs)
        {
            try
            {

                Mst_CareersSend c = new Mst_CareersSend();
                c.Prefix = cs.Prefix;
                c.Name = cs.Name;
                c.Email = cs.Email;
                c.MobileNo = cs.MobileNo;
                c.Educations = cs.Educations;
                c.Designation = cs.Designation;
                c.CoreExpert = cs.CoreExpert;
                c.Exper = cs.Exper;
                c.PrevCompany = cs.PrevCompany;
                c.CurrentCompany = cs.CurrentCompany;
                c.CurrentSalary = cs.CurrentSalary;
                c.ExpectedSalary = cs.ExpectedSalary;
                c.Reference = cs.Reference;
                c.Address = cs.Address;
                c.CreateDate = DateTime.UtcNow;
                db.Mst_CareersSend.Add(c);
                db.SaveChanges();
                int i = c.SendCarrerID;
                return Json(i, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult UploadCareer(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                    //string filename = Path.GetFileName(Request.Files[i].FileName);  

                    HttpPostedFileBase file = files[0];
                    HttpPostedFileBase file1 = files[1];

                    string fname;
                    string fname1;


                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];

                        string[] testfiles1 = file1.FileName.Split(new char[] { '\\' });
                        fname1 = testfiles1[testfiles1.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                        fname1 = file1.FileName;
                    }

                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    fname1 = Path.Combine(Server.MapPath("~/images/"), fname1);


                    file.SaveAs(fname);
                    file1.SaveAs(fname1);

                    string physicalpath = "images/" + file.FileName;
                    string physicalpath1 = "images/" + file1.FileName;



                    Mst_CareersSend pm = new Mst_CareersSend();
                    pm = (db.Mst_CareersSend.Where(r => r.SendCarrerID == tid)).SingleOrDefault();
                    if (pm != null)
                    {

                        MailMessage mail = new MailMessage();
                        mail.To.Add(Helper.careeremail);
                        mail.From = new MailAddress(Helper.networkemail);
                        mail.Subject = "New Resume Uploaded";
                        string Body = "Dear Sir/Mam <br /><br />" +
                            "Hello,<br />" +
                            "<b>Person Name :</b>" + pm.Name + "<br />" +
                            "<b>Mobile Number :</b>" + pm.MobileNo + "<br />" +
                            "<b>Email-ID :</b>" + pm.Email + "<br />" +
                             "<b>Current Company :</b>" + pm.CurrentCompany + "<br />" +
                             "<b>Current Salary :</b>" + pm.CurrentSalary + "<br />" +
                             "<b>Experience :</b>" + pm.Exper + "<br />" +
                             "<b>Expected Salary :</b>" + pm.ExpectedSalary + "<br />" +
                            "<b>Address :</b>" + pm.Address + "<br />";

                        mail.Body = Body;
                        mail.IsBodyHtml = true;
                        mail.Attachments.Add(new Attachment(fname));
                        mail.Attachments.Add(new Attachment(fname1));
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = Helper.hostemailid;
                        smtp.Port = Convert.ToInt32(Helper.networkemailport);
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential(Helper.networkemail, Helper.networkemailpass); // Enter seders User name and password  
                        smtp.EnableSsl = Convert.ToBoolean(Helper.networkemailssl);
                        smtp.Send(mail);


                        pm.AttachDoc = physicalpath;
                        pm.AttachPhoto = physicalpath1;

                        db.SaveChanges();
                    }

                    // Returns message that successfully uploaded  
                    return Json("Career Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        public JsonResult BindCareerSend()
        {
            try
            {
                ContactData sd = new ContactData();
                sd.career = (from s in db.Mst_CareersSend
                             select new CareerMeaageClass
                             {
                                 SendCarrerID = s.SendCarrerID,
                                 Name = s.Name,
                                 AttachDoc = s.AttachDoc,
                                 AttachPhoto = s.AttachPhoto,
                                 Email = s.Email,
                                 MobileNo = s.MobileNo,
                                 date = s.CreateDate.ToString(),
                                 Designation = s.Designation,

                             }).ToList();

                if (sd.career.Count > 0)
                    sd.success = true;
                else
                    sd.success = false;
                return Json(sd, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult DelCareer(Mst_CareersSend brc)
        {
            try
            {
                var msg = "";
                var success = "";
                int careerid = brc.SendCarrerID;
                Mst_CareersSend emp = new Mst_CareersSend();
                emp = db.Mst_CareersSend.Where(empp => empp.SendCarrerID == careerid).SingleOrDefault();
                db.Mst_CareersSend.Remove(emp);
                db.SaveChanges();
                msg = "Career Deleted Successfully..";
                success = "true";
                var resultm = new { msg, success };
                return Json(resultm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult CareerMessage()
        {
            return View();
        }
        #endregion

        #region FooterBlock
        [AcceptVerbs("GET")]
        public JsonResult EditFooterBlock()
        {
            FooterBlockClass footerclass = new FooterBlockClass();
            FooterBlockData footerdata = new FooterBlockData();

            footerdata.footerblock = (from abt in db.Mst_Footerblock
                                      select new FooterBlockClass
                                      {
                                          FooterID = abt.FooterID,
                                          FooterAddress = abt.FooterAddress,
                                          PhoneNo = abt.PhoneNo,
                                          SupportNo = abt.SupportNo
                                      }).ToList();

            if (footerdata.footerblock.Count > 0)
            {
                var footerblock = "";
                footerdata.success = true;
                var result = new { footerblock = footerdata.footerblock, footerdata.success };
                //return Request.CreateResponse(HttpStatusCode.OK, result);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                footerdata.success = false;
                //return Request.CreateResponse(HttpStatusCode.OK, aboutdata);
                return Json(footerdata, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UpdateFooterBlock(FooterBlockClass abtus)
        {
            try
            {
                int FooterID = Convert.ToInt32(abtus.FooterID);
                Mst_Footerblock bmst = new Mst_Footerblock();
                bmst = db.Mst_Footerblock.Where(a => a.FooterID == FooterID).SingleOrDefault();
                bmst.FooterAddress = abtus.FooterAddress;
                bmst.PhoneNo = abtus.PhoneNo;
                bmst.SupportNo = abtus.SupportNo;
                db.SaveChanges();
                return Json("Success");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Compare
        [AcceptVerbs("GET")]
        public JsonResult EditCompare()
        {
            CompareClass compareclass = new CompareClass();
            CompareData comparedata = new CompareData();

            comparedata.compare = (from abt in db.Mst_Compare
                                   select new CompareClass
                                   {
                                       CompareID = abt.CompareID,
                                       CompareDesc = abt.CompareDesc,
                                       Product1Desc = abt.Product1Desc,
                                       Product2Desc = abt.Product2Desc
                                   }).ToList();

            if (comparedata.compare.Count > 0)
            {
                var compare = "";
                comparedata.success = true;
                var result = new { compare = comparedata.compare, comparedata.success };
                //return Request.CreateResponse(HttpStatusCode.OK, result);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                comparedata.success = false;
                //return Request.CreateResponse(HttpStatusCode.OK, aboutdata);
                return Json(comparedata, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UpdateCompare(CompareClass abtus)
        {
            try
            {
                int CompareID = Convert.ToInt32(abtus.CompareID);
                Mst_Compare bmst = new Mst_Compare();
                bmst = db.Mst_Compare.Where(a => a.CompareID == CompareID).SingleOrDefault();
                bmst.CompareDesc = abtus.CompareDesc;
                bmst.Product1Desc = abtus.Product1Desc;
                bmst.Product2Desc = abtus.Product2Desc;
                db.SaveChanges();
                return Json("Success");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult SaveComparison(Mst_CompareMaster cm)
        {
            try
            {
                db.Mst_CompareMaster.Add(cm);
                db.SaveChanges();
                int i = cm.Compare_Id;
                return Json(i, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult BindComparison()
        {
            try
            {
                var sm = (db.Mst_CompareMaster.ToList());
                return Json(sm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult EditComparison(int CompareId)
        {
            try
            {
                Mst_CompareMaster df = new Mst_CompareMaster();
                df = (db.Mst_CompareMaster.Where(r => r.Compare_Id == CompareId)).SingleOrDefault();
                return Json(df, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult UpdateComparison(Mst_CompareMaster cm)
        {
            try
            {
                Mst_CompareMaster m = new Mst_CompareMaster();
                m = (db.Mst_CompareMaster.Where(r => r.Compare_Id == cm.Compare_Id)).SingleOrDefault();
                m.FeatureName = cm.FeatureName;
                m.Desktop = cm.Desktop;
                m.Cloud = cm.Cloud;
                db.SaveChanges();
                int i = m.Compare_Id;
                return Json(i, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult DeleteComparison(Mst_CompareMaster brc)
        {
            try
            {
                var msg = "";
                var success = "";

                Mst_CompareMaster emp = new Mst_CompareMaster();
                emp = db.Mst_CompareMaster.Where(empp => empp.Compare_Id == brc.Compare_Id).SingleOrDefault();
                db.Mst_CompareMaster.Remove(emp);
                db.SaveChanges();
                msg = "Compare Feature Deleted Successfully..";
                success = "true";
                var resultm = new { msg, success };
                return Json(resultm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult SaveComparisonFeature(Mst_Compare_KeyFeature cm)
        {
            try
            {
                db.Mst_Compare_KeyFeature.Add(cm);
                db.SaveChanges();
                int i = cm.Id;
                return Json(i, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult BindComparisonFeature()
        {
            try
            {
                var sm = (db.Mst_Compare_KeyFeature.ToList());
                return Json(sm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult EditComparisonFeature(int id)
        {
            try
            {
                Mst_Compare_KeyFeature df = new Mst_Compare_KeyFeature();
                df = (db.Mst_Compare_KeyFeature.Where(r => r.Id == id)).SingleOrDefault();
                return Json(df, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult UpdateComparisonFeature(Mst_Compare_KeyFeature cm)
        {
            try
            {
                Mst_Compare_KeyFeature m = new Mst_Compare_KeyFeature();
                m = (db.Mst_Compare_KeyFeature.Where(r => r.Id == cm.Id)).SingleOrDefault();
                m.Key_FeatureDesc = cm.Key_FeatureDesc;
                db.SaveChanges();
                int i = m.Id;
                return Json(i, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult DeleteComparisonFeature(Mst_Compare_KeyFeature brc)
        {
            try
            {
                var msg = "";
                var success = "";

                Mst_Compare_KeyFeature emp = new Mst_Compare_KeyFeature();
                emp = db.Mst_Compare_KeyFeature.Where(empp => empp.Id == brc.Id).SingleOrDefault();
                db.Mst_Compare_KeyFeature.Remove(emp);
                db.SaveChanges();
                msg = "Compare Key Feature Deleted Successfully..";
                success = "true";
                var resultm = new { msg, success };
                return Json(resultm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region Desktop
        [AcceptVerbs("GET")]
        public JsonResult EditDesktop()
        {
            DesktopClass desktopclass = new DesktopClass();
            DesktopData desktopdata = new DesktopData();

            desktopdata.desktop = (from abt in db.Mst_Desktop
                                   select new DesktopClass
                                   {
                                       DesktopID = abt.DesktopID,
                                       Content1 = abt.Content1,
                                       Content2 = abt.Content2,
                                       Content3 = abt.Content3,
                                       Content4 = abt.Content4,
                                       Content5 = abt.Content5,
                                       ProductImage = abt.ProductImage,
                                       ProductLogo = abt.ProductLogo
                                   }).ToList();

            if (desktopdata.desktop.Count > 0)
            {
                var desktop = "";
                desktopdata.success = true;
                var result = new { desktop = desktopdata.desktop, desktopdata.success };
                //return Request.CreateResponse(HttpStatusCode.OK, result);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                desktopdata.success = false;
                //return Request.CreateResponse(HttpStatusCode.OK, aboutdata);
                return Json(desktopdata, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UpdateDesktop(DesktopClass abtus)
        {
            try
            {
                int DesktopID = Convert.ToInt32(abtus.DesktopID);
                Mst_Desktop bmst = new Mst_Desktop();
                bmst = db.Mst_Desktop.Where(a => a.DesktopID == DesktopID).SingleOrDefault();
                bmst.Content1 = abtus.Content1;
                bmst.Content2 = abtus.Content2;
                bmst.Content3 = abtus.Content3;
                bmst.Content4 = abtus.Content4;
                bmst.Content5 = abtus.Content5;
                db.SaveChanges();
                int id = bmst.DesktopID;
                var msg = "";
                if (id > 0)
                {
                    msg = "Success";
                }
                else
                {
                    msg = "Fail";
                }
                var resultm = new { msg, id };
                return Json(resultm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult UploadDeskProductImage(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                    //string filename = Path.GetFileName(Request.Files[i].FileName);  

                    HttpPostedFileBase file = files[0];

                    string fname;


                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];


                    }
                    else
                    {
                        fname = file.FileName;

                    }

                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/Products/"), fname);

                    file.SaveAs(fname);

                    string physicalpath = "images/Products/" + file.FileName;

                    //string physicalpath = "http://diamtrade.cricketclubs.in/images/Products/" + file.FileName;


                    Mst_Desktop pm = new Mst_Desktop();
                    pm = (db.Mst_Desktop.Where(r => r.DesktopID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.ProductImage = physicalpath;
                        db.SaveChanges();
                    }

                    // Returns message that successfully uploaded  
                    return Json("Product Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult UploadDeskProductLogo(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                    //string filename = Path.GetFileName(Request.Files[i].FileName);  

                    HttpPostedFileBase file = files[0];

                    string fname;


                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];


                    }
                    else
                    {
                        fname = file.FileName;

                    }

                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/Products/"), fname);

                    file.SaveAs(fname);

                    string physicalpath = "images/Products/" + file.FileName;

                    //string physicalpath = "http://diamtrade.cricketclubs.in/images/Products/" + file.FileName;


                    Mst_Desktop pm = new Mst_Desktop();
                    pm = (db.Mst_Desktop.Where(r => r.DesktopID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.ProductLogo = physicalpath;
                        db.SaveChanges();
                    }

                    // Returns message that successfully uploaded  
                    return Json("Product Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        #region ProductFeature
        public ActionResult InsertDesktopProductFeature(Mst_Desktop_Features sf)
        {
            try
            {
                db.Mst_Desktop_Features.Add(sf);
                db.SaveChanges();
                int i = 0;
                i = sf.Id;
                return Json(i, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult UploadDesktopProductFeatureImage(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                    //string filename = Path.GetFileName(Request.Files[i].FileName);  

                    HttpPostedFileBase file = files[0];


                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }

                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);

                    file.SaveAs(fname);


                    string physicalpath = "images/" + file.FileName;

                    //string physicalpath = "http://diamtrade.cricketclubs.in/images/" + file.FileName;


                    Mst_Desktop_Features pm = new Mst_Desktop_Features();
                    pm = (db.Mst_Desktop_Features.Where(r => r.Id == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Image = physicalpath;
                        db.SaveChanges();
                    }

                    // Returns message that successfully uploaded  
                    return Json("product feature Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        public JsonResult BindDesktopProductFeature()
        {
            try
            {
                var md = db.Mst_Desktop_Features.ToList();
                return Json(md, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult EditDesktopFeature(int Desktopfeatureid)
        {
            try
            {
                Mst_Desktop_Features df = new Mst_Desktop_Features();
                df = (db.Mst_Desktop_Features.Where(r => r.Id == Desktopfeatureid)).SingleOrDefault();
                return Json(df, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult UpdateDesktopProductFeature(Mst_Desktop_Features sf)
        {
            try
            {
                Mst_Desktop_Features df = new Mst_Desktop_Features();
                df = (db.Mst_Desktop_Features.Where(r => r.Id == sf.Id)).SingleOrDefault();
                df.ProductName = sf.ProductName;
                df.Feature_Name = sf.Feature_Name;
                db.SaveChanges();
                int i = 0;
                i = df.Id;
                return Json(i, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult DeleteDesktopProductFeature(Mst_Desktop_Features brc)
        {
            try
            {
                var msg = "";
                var success = "";

                Mst_Desktop_Features emp = new Mst_Desktop_Features();
                emp = db.Mst_Desktop_Features.Where(empp => empp.Id == brc.Id).SingleOrDefault();
                db.Mst_Desktop_Features.Remove(emp);
                db.SaveChanges();
                msg = "Desktop Type Feature Deleted Successfully..";
                success = "true";
                var resultm = new { msg, success };
                return Json(resultm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region KeyFeature
        public ActionResult InsertDesktopkeyFeature(Mst_Desktop_KeyFeature sf)
        {
            try
            {
                int i = 0;
                if (sf.ProductName == "Desktop")
                {
                    Mst_Desktop_KeyFeature K = new Mst_Desktop_KeyFeature();
                    K.ProductName = sf.ProductName;
                    db.Mst_Desktop_KeyFeature.Add(K);
                    db.SaveChanges();
                    i = K.Id;
                }
                else if (sf.ProductName == "Cloud")
                {
                    Mst_Cloud_KetFeature c = new Mst_Cloud_KetFeature();
                    c.ProductName = sf.ProductName;
                    db.Mst_Cloud_KetFeature.Add(c);
                    db.SaveChanges();
                    i = c.Id;
                }
                else
                {
                    Mst_Diamx_KeyFeature d = new Mst_Diamx_KeyFeature();
                    d.ProductName = sf.ProductName;
                    db.Mst_Diamx_KeyFeature.Add(d);
                    db.SaveChanges();
                    i = d.Id;
                }

                return Json(i, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult UploadDesktopkeyFeatureImage(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    string productname = collection["ProductName"];
                    int tid = Convert.ToInt32(productid);

                    //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                    //string filename = Path.GetFileName(Request.Files[i].FileName);  

                    HttpPostedFileBase file = files[0];


                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }

                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);

                    file.SaveAs(fname);


                    string physicalpath = "images/" + file.FileName;

                    //string physicalpath = "http://diamtrade.cricketclubs.in/images/" + file.FileName;

                    if (productname == "Desktop")
                    {
                        Mst_Desktop_KeyFeature pm = new Mst_Desktop_KeyFeature();
                        pm = (db.Mst_Desktop_KeyFeature.Where(r => r.Id == tid)).SingleOrDefault();
                        if (pm != null)
                        {
                            pm.Image = physicalpath;
                            db.SaveChanges();
                        }
                    }
                    else if (productname == "Cloud")
                    {
                        Mst_Cloud_KetFeature pm = new Mst_Cloud_KetFeature();
                        pm = (db.Mst_Cloud_KetFeature.Where(r => r.Id == tid)).SingleOrDefault();
                        if (pm != null)
                        {
                            pm.Image = physicalpath;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        Mst_Diamx_KeyFeature pm = new Mst_Diamx_KeyFeature();
                        pm = (db.Mst_Diamx_KeyFeature.Where(r => r.Id == tid)).SingleOrDefault();
                        if (pm != null)
                        {
                            pm.Image = physicalpath;
                            db.SaveChanges();
                        }
                    }
                    // Returns message that successfully uploaded  
                    return Json("product feature Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        public JsonResult BindDesktopkeyFeature(string productname)
        {
            try
            {
                if (productname == "Desktop")
                {
                    var md = db.Mst_Desktop_KeyFeature.ToList();
                    return Json(md, JsonRequestBehavior.AllowGet);
                }
                else if (productname == "Cloud")
                {
                    var md = db.Mst_Cloud_KetFeature.ToList();
                    return Json(md, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var md = db.Mst_Diamx_KeyFeature.ToList();
                    return Json(md, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult EditDesktopkeyFeature(int Desktopfeatureid, string productname)
        {
            try
            {
                if (productname == "Desktop")
                {
                    Mst_Desktop_KeyFeature df = new Mst_Desktop_KeyFeature();
                    df = (db.Mst_Desktop_KeyFeature.Where(r => r.Id == Desktopfeatureid)).SingleOrDefault();
                    return Json(df, JsonRequestBehavior.AllowGet);
                }
                else if (productname == "Cloud")
                {
                    Mst_Cloud_KetFeature df = new Mst_Cloud_KetFeature();
                    df = (db.Mst_Cloud_KetFeature.Where(r => r.Id == Desktopfeatureid)).SingleOrDefault();
                    return Json(df, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Mst_Diamx_KeyFeature df = new Mst_Diamx_KeyFeature();
                    df = (db.Mst_Diamx_KeyFeature.Where(r => r.Id == Desktopfeatureid)).SingleOrDefault();
                    return Json(df, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult UpdateDesktopkeyFeature(Mst_Desktop_Features sf)
        {
            try
            {
                int i = 0;
                if (sf.ProductName == "Desktop")
                {
                    Mst_Desktop_KeyFeature df = new Mst_Desktop_KeyFeature();
                    df = (db.Mst_Desktop_KeyFeature.Where(r => r.Id == sf.Id)).SingleOrDefault();
                    df.ProductName = sf.ProductName;
                    df.Feature_Name = sf.Feature_Name;
                    db.SaveChanges();
                    i = df.Id;
                }
                else if (sf.ProductName == "Cloud")
                {
                    Mst_Cloud_KetFeature df = new Mst_Cloud_KetFeature();
                    df = (db.Mst_Cloud_KetFeature.Where(r => r.Id == sf.Id)).SingleOrDefault();
                    df.ProductName = sf.ProductName;
                    df.Feature_Name = sf.Feature_Name;
                    db.SaveChanges();
                    i = df.Id;
                }
                else
                {
                    Mst_Diamx_KeyFeature df = new Mst_Diamx_KeyFeature();
                    df = (db.Mst_Diamx_KeyFeature.Where(r => r.Id == sf.Id)).SingleOrDefault();
                    df.ProductName = sf.ProductName;
                    df.Feature_Name = sf.Feature_Name;
                    db.SaveChanges();
                    i = df.Id;
                }
                return Json(i, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult DeleteDesktopkeyFeature(Mst_Desktop_Features brc)
        {
            try
            {
                var msg = "";
                var success = "";
                if (brc.ProductName == "Desktop")
                {
                    Mst_Desktop_KeyFeature emp = new Mst_Desktop_KeyFeature();
                    emp = db.Mst_Desktop_KeyFeature.Where(empp => empp.Id == brc.Id).SingleOrDefault();
                    db.Mst_Desktop_KeyFeature.Remove(emp);
                    db.SaveChanges();
                    msg = "Desktop Type Key Feature Deleted Successfully..";
                    success = "true";
                    var resultm = new { msg, success };
                    return Json(resultm, JsonRequestBehavior.AllowGet);
                }
                else if (brc.ProductName == "Cloud")
                {
                    Mst_Cloud_KetFeature emp = new Mst_Cloud_KetFeature();
                    emp = db.Mst_Cloud_KetFeature.Where(empp => empp.Id == brc.Id).SingleOrDefault();
                    db.Mst_Cloud_KetFeature.Remove(emp);
                    db.SaveChanges();
                    msg = "Cloud Type Key Feature Deleted Successfully..";
                    success = "true";
                    var resultm = new { msg, success };
                    return Json(resultm, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Mst_Diamx_KeyFeature emp = new Mst_Diamx_KeyFeature();
                    emp = db.Mst_Diamx_KeyFeature.Where(empp => empp.Id == brc.Id).SingleOrDefault();
                    db.Mst_Diamx_KeyFeature.Remove(emp);
                    db.SaveChanges();
                    msg = "Diamx Type Key Feature Deleted Successfully..";
                    success = "true";
                    var resultm = new { msg, success };
                    return Json(resultm, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region spclFeature
        public ActionResult InsertDesktopSpclInveFeature(Mst_Desktop_KeyFeature sf)
        {
            try
            {
                int i = 0;
                if (sf.ProductName == "Desktop")
                {
                    Mst_Desktop_Inventories K = new Mst_Desktop_Inventories();
                    K.ProductName = sf.ProductName;
                    db.Mst_Desktop_Inventories.Add(K);
                    db.SaveChanges();
                    i = K.Id;
                }
                else if (sf.ProductName == "Cloud")
                {
                    Mst_Cloud_Inventories c = new Mst_Cloud_Inventories();
                    c.ProductName = sf.ProductName;
                    db.Mst_Cloud_Inventories.Add(c);
                    db.SaveChanges();
                    i = c.Id;
                }
                else
                {
                    Mst_Diamx_Inventories d = new Mst_Diamx_Inventories();
                    d.ProductName = sf.ProductName;
                    db.Mst_Diamx_Inventories.Add(d);
                    db.SaveChanges();
                    i = d.Id;
                }

                return Json(i, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult UploadDesktopSpclInveFeatureImage(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    string productname = collection["ProductName"];
                    int tid = Convert.ToInt32(productid);

                    //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                    //string filename = Path.GetFileName(Request.Files[i].FileName);  

                    HttpPostedFileBase file = files[0];


                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }

                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);

                    file.SaveAs(fname);


                    string physicalpath = "images/" + file.FileName;

                    //string physicalpath = "http://diamtrade.cricketclubs.in/images/" + file.FileName;

                    if (productname == "Desktop")
                    {
                        Mst_Desktop_Inventories pm = new Mst_Desktop_Inventories();
                        pm = (db.Mst_Desktop_Inventories.Where(r => r.Id == tid)).SingleOrDefault();
                        if (pm != null)
                        {
                            pm.Image = physicalpath;
                            db.SaveChanges();
                        }
                    }
                    else if (productname == "Cloud")
                    {
                        Mst_Cloud_Inventories pm = new Mst_Cloud_Inventories();
                        pm = (db.Mst_Cloud_Inventories.Where(r => r.Id == tid)).SingleOrDefault();
                        if (pm != null)
                        {
                            pm.Image = physicalpath;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        Mst_Diamx_Inventories pm = new Mst_Diamx_Inventories();
                        pm = (db.Mst_Diamx_Inventories.Where(r => r.Id == tid)).SingleOrDefault();
                        if (pm != null)
                        {
                            pm.Image = physicalpath;
                            db.SaveChanges();
                        }
                    }
                    // Returns message that successfully uploaded  
                    return Json("product feature Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        public JsonResult BindDesktopSpclInveFeature(string productname)
        {
            try
            {
                if (productname == "Desktop")
                {
                    var md = db.Mst_Desktop_Inventories.ToList();
                    return Json(md, JsonRequestBehavior.AllowGet);
                }
                else if (productname == "Cloud")
                {
                    var md = db.Mst_Cloud_Inventories.ToList();
                    return Json(md, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var md = db.Mst_Diamx_Inventories.ToList();
                    return Json(md, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult EditDesktopSpclInveFeature(int Desktopfeatureid, string productname)
        {
            try
            {
                if (productname == "Desktop")
                {
                    Mst_Desktop_Inventories df = new Mst_Desktop_Inventories();
                    df = (db.Mst_Desktop_Inventories.Where(r => r.Id == Desktopfeatureid)).SingleOrDefault();
                    return Json(df, JsonRequestBehavior.AllowGet);
                }
                else if (productname == "Cloud")
                {
                    Mst_Cloud_Inventories df = new Mst_Cloud_Inventories();
                    df = (db.Mst_Cloud_Inventories.Where(r => r.Id == Desktopfeatureid)).SingleOrDefault();
                    return Json(df, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Mst_Diamx_Inventories df = new Mst_Diamx_Inventories();
                    df = (db.Mst_Diamx_Inventories.Where(r => r.Id == Desktopfeatureid)).SingleOrDefault();
                    return Json(df, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult UpdateDesktopSpclInveFeature(Mst_Desktop_Features sf)
        {
            try
            {
                int i = 0;
                if (sf.ProductName == "Desktop")
                {
                    Mst_Desktop_Inventories df = new Mst_Desktop_Inventories();
                    df = (db.Mst_Desktop_Inventories.Where(r => r.Id == sf.Id)).SingleOrDefault();
                    df.ProductName = sf.ProductName;
                    df.Feature_Name = sf.Feature_Name;
                    db.SaveChanges();
                    i = df.Id;
                }
                else if (sf.ProductName == "Cloud")
                {
                    Mst_Cloud_Inventories df = new Mst_Cloud_Inventories();
                    df = (db.Mst_Cloud_Inventories.Where(r => r.Id == sf.Id)).SingleOrDefault();
                    df.ProductName = sf.ProductName;
                    df.Feature_Name = sf.Feature_Name;
                    db.SaveChanges();
                    i = df.Id;
                }
                else
                {
                    Mst_Diamx_Inventories df = new Mst_Diamx_Inventories();
                    df = (db.Mst_Diamx_Inventories.Where(r => r.Id == sf.Id)).SingleOrDefault();
                    df.ProductName = sf.ProductName;
                    df.Feature_Name = sf.Feature_Name;
                    db.SaveChanges();
                    i = df.Id;
                }
                return Json(i, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult DeleteDesktopSpclInveFeature(Mst_Desktop_Features brc)
        {
            try
            {
                var msg = "";
                var success = "";
                if (brc.ProductName == "Desktop")
                {
                    Mst_Desktop_Inventories emp = new Mst_Desktop_Inventories();
                    emp = db.Mst_Desktop_Inventories.Where(empp => empp.Id == brc.Id).SingleOrDefault();
                    db.Mst_Desktop_Inventories.Remove(emp);
                    db.SaveChanges();
                    msg = "Desktop Type Key Feature Deleted Successfully..";
                    success = "true";
                    var resultm = new { msg, success };
                    return Json(resultm, JsonRequestBehavior.AllowGet);
                }
                else if (brc.ProductName == "Cloud")
                {
                    Mst_Cloud_Inventories emp = new Mst_Cloud_Inventories();
                    emp = db.Mst_Cloud_Inventories.Where(empp => empp.Id == brc.Id).SingleOrDefault();
                    db.Mst_Cloud_Inventories.Remove(emp);
                    db.SaveChanges();
                    msg = "Cloud Type Key Feature Deleted Successfully..";
                    success = "true";
                    var resultm = new { msg, success };
                    return Json(resultm, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Mst_Diamx_Inventories emp = new Mst_Diamx_Inventories();
                    emp = db.Mst_Diamx_Inventories.Where(empp => empp.Id == brc.Id).SingleOrDefault();
                    db.Mst_Diamx_Inventories.Remove(emp);
                    db.SaveChanges();
                    msg = "Diamx Type Key Feature Deleted Successfully..";
                    success = "true";
                    var resultm = new { msg, success };
                    return Json(resultm, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region productgallery
        public ActionResult InsertDesktopGallery(Mst_Desktop_KeyFeature sf)
        {
            try
            {
                int i = 0;
                if (sf.ProductName == "Desktop")
                {
                    Mst_Desktop_Gallery K = new Mst_Desktop_Gallery();
                    K.ProductName = sf.ProductName;
                    db.Mst_Desktop_Gallery.Add(K);
                    db.SaveChanges();
                    i = K.Id;
                }
                else if (sf.ProductName == "Cloud")
                {
                    Mst_Cloud_Gallery c = new Mst_Cloud_Gallery();
                    c.ProductName = sf.ProductName;
                    db.Mst_Cloud_Gallery.Add(c);
                    db.SaveChanges();
                    i = c.Id;
                }
                else
                {
                    Mst_Diamx_Gallery d = new Mst_Diamx_Gallery();
                    d.ProductName = sf.ProductName;
                    db.Mst_Diamx_Gallery.Add(d);
                    db.SaveChanges();
                    i = d.Id;
                }

                return Json(i, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult UploadDesktopGalleryImage(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    string productname = collection["ProductName"];
                    int tid = Convert.ToInt32(productid);

                    //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                    //string filename = Path.GetFileName(Request.Files[i].FileName);  

                    HttpPostedFileBase file = files[0];


                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }

                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);

                    file.SaveAs(fname);


                    string physicalpath = "images/" + file.FileName;

                    //string physicalpath = "http://diamtrade.cricketclubs.in/images/" + file.FileName;

                    if (productname == "Desktop")
                    {
                        Mst_Desktop_Gallery pm = new Mst_Desktop_Gallery();
                        pm = (db.Mst_Desktop_Gallery.Where(r => r.Id == tid)).SingleOrDefault();
                        if (pm != null)
                        {
                            pm.Image = physicalpath;
                            db.SaveChanges();
                        }
                    }
                    else if (productname == "Cloud")
                    {
                        Mst_Cloud_Gallery pm = new Mst_Cloud_Gallery();
                        pm = (db.Mst_Cloud_Gallery.Where(r => r.Id == tid)).SingleOrDefault();
                        if (pm != null)
                        {
                            pm.Image = physicalpath;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        Mst_Diamx_Gallery pm = new Mst_Diamx_Gallery();
                        pm = (db.Mst_Diamx_Gallery.Where(r => r.Id == tid)).SingleOrDefault();
                        if (pm != null)
                        {
                            pm.Image = physicalpath;
                            db.SaveChanges();
                        }
                    }
                    // Returns message that successfully uploaded  
                    return Json("product feature Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        public JsonResult BindDesktopGallery(string productname)
        {
            try
            {
                if (productname == "Desktop")
                {
                    var md = db.Mst_Desktop_Gallery.ToList();
                    return Json(md, JsonRequestBehavior.AllowGet);
                }
                else if (productname == "Cloud")
                {
                    var md = db.Mst_Cloud_Gallery.ToList();
                    return Json(md, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var md = db.Mst_Diamx_Gallery.ToList();
                    return Json(md, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult EditDesktopGallery(int Desktopfeatureid, string productname)
        {
            try
            {
                if (productname == "Desktop")
                {
                    Mst_Desktop_Gallery df = new Mst_Desktop_Gallery();
                    df = (db.Mst_Desktop_Gallery.Where(r => r.Id == Desktopfeatureid)).SingleOrDefault();
                    return Json(df, JsonRequestBehavior.AllowGet);
                }
                else if (productname == "Cloud")
                {
                    Mst_Cloud_Gallery df = new Mst_Cloud_Gallery();
                    df = (db.Mst_Cloud_Gallery.Where(r => r.Id == Desktopfeatureid)).SingleOrDefault();
                    return Json(df, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Mst_Diamx_Gallery df = new Mst_Diamx_Gallery();
                    df = (db.Mst_Diamx_Gallery.Where(r => r.Id == Desktopfeatureid)).SingleOrDefault();
                    return Json(df, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult UpdateDesktopGallery(Mst_Desktop_Features sf)
        {
            try
            {
                int i = 0;
                if (sf.ProductName == "Desktop")
                {
                    Mst_Desktop_Gallery df = new Mst_Desktop_Gallery();
                    df = (db.Mst_Desktop_Gallery.Where(r => r.Id == sf.Id)).SingleOrDefault();
                    df.ProductName = sf.ProductName;
                    db.SaveChanges();
                    i = df.Id;
                }
                else if (sf.ProductName == "Cloud")
                {
                    Mst_Cloud_Gallery df = new Mst_Cloud_Gallery();
                    df = (db.Mst_Cloud_Gallery.Where(r => r.Id == sf.Id)).SingleOrDefault();
                    df.ProductName = sf.ProductName;
                    db.SaveChanges();
                    i = df.Id;
                }
                else
                {
                    Mst_Diamx_Gallery df = new Mst_Diamx_Gallery();
                    df = (db.Mst_Diamx_Gallery.Where(r => r.Id == sf.Id)).SingleOrDefault();
                    df.ProductName = sf.ProductName;
                    db.SaveChanges();
                    i = df.Id;
                }
                return Json(i, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult DeleteDesktopGallery(Mst_Desktop_Features brc)
        {
            try
            {
                var msg = "";
                var success = "";
                if (brc.ProductName == "Desktop")
                {
                    Mst_Desktop_Gallery emp = new Mst_Desktop_Gallery();
                    emp = db.Mst_Desktop_Gallery.Where(empp => empp.Id == brc.Id).SingleOrDefault();
                    db.Mst_Desktop_Gallery.Remove(emp);
                    db.SaveChanges();
                    msg = "Desktop Type Key Feature Deleted Successfully..";
                    success = "true";
                    var resultm = new { msg, success };
                    return Json(resultm, JsonRequestBehavior.AllowGet);
                }
                else if (brc.ProductName == "Cloud")
                {
                    Mst_Cloud_Gallery emp = new Mst_Cloud_Gallery();
                    emp = db.Mst_Cloud_Gallery.Where(empp => empp.Id == brc.Id).SingleOrDefault();
                    db.Mst_Cloud_Gallery.Remove(emp);
                    db.SaveChanges();
                    msg = "Cloud Type Key Feature Deleted Successfully..";
                    success = "true";
                    var resultm = new { msg, success };
                    return Json(resultm, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Mst_Diamx_Gallery emp = new Mst_Diamx_Gallery();
                    emp = db.Mst_Diamx_Gallery.Where(empp => empp.Id == brc.Id).SingleOrDefault();
                    db.Mst_Diamx_Gallery.Remove(emp);
                    db.SaveChanges();
                    msg = "Diamx Type Key Feature Deleted Successfully..";
                    success = "true";
                    var resultm = new { msg, success };
                    return Json(resultm, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion

        #region Cloud
        [AcceptVerbs("GET")]
        public JsonResult EditCloud()
        {
            CloudClass cloudclass = new CloudClass();
            CloudData clouddata = new CloudData();

            clouddata.cloud = (from abt in db.Mst_Cloud
                               select new CloudClass
                               {
                                   CloudID = abt.CloudID,
                                   Content1 = abt.Content1,
                                   Content2 = abt.Content2,
                                   Content3 = abt.Content3,
                                   Content4 = abt.Content4,
                                   Content5 = abt.Content5,
                                   ProductImage = abt.ProductImage,
                                   ProductLogo = abt.ProductLogo
                               }).ToList();

            if (clouddata.cloud.Count > 0)
            {
                var cloud = "";
                clouddata.success = true;
                var result = new { cloud = clouddata.cloud, clouddata.success };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                clouddata.success = false;
                //return Request.CreateResponse(HttpStatusCode.OK, aboutdata);
                return Json(clouddata, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UpdateCloud(CloudClass abtus)
        {
            try
            {
                int CloudID = Convert.ToInt32(abtus.CloudID);
                Mst_Cloud bmst = new Mst_Cloud();
                bmst = db.Mst_Cloud.Where(a => a.CloudID == CloudID).SingleOrDefault();
                bmst.Content1 = abtus.Content1;
                bmst.Content2 = abtus.Content2;
                bmst.Content3 = abtus.Content3;
                bmst.Content4 = abtus.Content4;
                bmst.Content5 = abtus.Content5;
                db.SaveChanges();
                return Json("Success");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult UploadCloudImage(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                    //string filename = Path.GetFileName(Request.Files[i].FileName);  

                    HttpPostedFileBase file = files[0];

                    string fname;


                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];


                    }
                    else
                    {
                        fname = file.FileName;

                    }

                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/Products/"), fname);

                    file.SaveAs(fname);

                    string physicalpath = "images/Products/" + file.FileName;

                    //string physicalpath = "http://diamtrade.cricketclubs.in/images/Products/" + file.FileName;


                    Mst_Cloud pm = new Mst_Cloud();
                    pm = (db.Mst_Cloud.Where(r => r.CloudID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.ProductImage = physicalpath;
                        db.SaveChanges();
                    }

                    // Returns message that successfully uploaded  
                    return Json("Product Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult UploadCloudLogo(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                    //string filename = Path.GetFileName(Request.Files[i].FileName);  

                    HttpPostedFileBase file = files[0];

                    string fname;


                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];


                    }
                    else
                    {
                        fname = file.FileName;

                    }

                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/Products/"), fname);

                    file.SaveAs(fname);

                    string physicalpath = "images/Products/" + file.FileName;

                    //string physicalpath = "http://diamtrade.cricketclubs.in/images/Products/" + file.FileName;


                    Mst_Cloud pm = new Mst_Cloud();
                    pm = (db.Mst_Cloud.Where(r => r.CloudID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.ProductLogo = physicalpath;
                        db.SaveChanges();
                    }

                    // Returns message that successfully uploaded  
                    return Json("Product Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        public ActionResult InsertCloudProductFeature(Mst_Cloud_Features sf)
        {
            try
            {
                db.Mst_Cloud_Features.Add(sf);
                db.SaveChanges();
                int i = 0;
                i = sf.Id;
                return Json(i, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult UploadCloudProductFeatureImage(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                    //string filename = Path.GetFileName(Request.Files[i].FileName);  

                    HttpPostedFileBase file = files[0];


                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }

                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);

                    file.SaveAs(fname);


                    string physicalpath = "images/" + file.FileName;

                    //string physicalpath = "http://diamtrade.cricketclubs.in/images/" + file.FileName;


                    Mst_Cloud_Features pm = new Mst_Cloud_Features();
                    pm = (db.Mst_Cloud_Features.Where(r => r.Id == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Image = physicalpath;
                        db.SaveChanges();
                    }

                    // Returns message that successfully uploaded  
                    return Json("product feature Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        public JsonResult BindCloudProductFeature()
        {
            try
            {
                var md = db.Mst_Cloud_Features.ToList();
                return Json(md, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult EditCloudFeature_new(int Desktopfeatureid)
        {
            try
            {
                Mst_Cloud_Features df = new Mst_Cloud_Features();
                df = (db.Mst_Cloud_Features.Where(r => r.Id == Desktopfeatureid)).SingleOrDefault();
                return Json(df, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult UpdateCloudProductFeature(Mst_Desktop_Features sf)
        {
            try
            {
                Mst_Cloud_Features df = new Mst_Cloud_Features();
                df = (db.Mst_Cloud_Features.Where(r => r.Id == sf.Id)).SingleOrDefault();
                df.ProductName = sf.ProductName;
                df.Feature_Name = sf.Feature_Name;
                db.SaveChanges();
                int i = 0;
                i = df.Id;
                return Json(i, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult DeleteCloudProductFeature(Mst_Desktop_Features brc)
        {
            try
            {
                var msg = "";
                var success = "";

                Mst_Cloud_Features emp = new Mst_Cloud_Features();
                emp = db.Mst_Cloud_Features.Where(empp => empp.Id == brc.Id).SingleOrDefault();
                db.Mst_Cloud_Features.Remove(emp);
                db.SaveChanges();
                msg = "Cloud Type Feature Deleted Successfully..";
                success = "true";
                var resultm = new { msg, success };
                return Json(resultm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Diamx
        [AcceptVerbs("GET")]
        public JsonResult EditDiamx()
        {
            DiamxClass diamxclass = new DiamxClass();
            DiamxData diamxdata = new DiamxData();

            diamxdata.diamx = (from abt in db.Mst_Diamx
                               select new DiamxClass
                               {
                                   DiamxID = abt.DiamxID,
                                   Content1 = abt.Content1,
                                   Content2 = abt.Content2,
                                   Content3 = abt.Content3,
                                   Content4 = abt.Content4,
                                   Content5 = abt.Content5,
                                   ProductImage = abt.ProductImage,
                                   ProductLogo = abt.ProductLogo
                               }).ToList();

            if (diamxdata.diamx.Count > 0)
            {
                var diamx = "";
                diamxdata.success = true;
                var result = new { diamx = diamxdata.diamx, diamxdata.success };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                diamxdata.success = false;
                //return Request.CreateResponse(HttpStatusCode.OK, aboutdata);
                return Json(diamxdata, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UpdateDiamx(DiamxClass abtus)
        {
            try
            {
                int DiamxID = Convert.ToInt32(abtus.DiamxID);
                Mst_Diamx bmst = new Mst_Diamx();
                bmst = db.Mst_Diamx.Where(a => a.DiamxID == DiamxID).SingleOrDefault();
                bmst.Content1 = abtus.Content1;
                bmst.Content2 = abtus.Content2;
                bmst.Content3 = abtus.Content3;
                bmst.Content4 = abtus.Content4;
                bmst.Content5 = abtus.Content5;

                db.SaveChanges();
                return Json("Success");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult UploadDiamxImage(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                    //string filename = Path.GetFileName(Request.Files[i].FileName);  

                    HttpPostedFileBase file = files[0];

                    string fname;


                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];


                    }
                    else
                    {
                        fname = file.FileName;

                    }

                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/Products/"), fname);

                    file.SaveAs(fname);

                    string physicalpath = "images/Products/" + file.FileName;

                    //string physicalpath = "http://diamtrade.cricketclubs.in/images/Products/" + file.FileName;


                    Mst_Diamx pm = new Mst_Diamx();
                    pm = (db.Mst_Diamx.Where(r => r.DiamxID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.ProductImage = physicalpath;
                        db.SaveChanges();
                    }

                    // Returns message that successfully uploaded  
                    return Json("Product Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult UploadDiamxLogo(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                    //string filename = Path.GetFileName(Request.Files[i].FileName);  

                    HttpPostedFileBase file = files[0];

                    string fname;


                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];


                    }
                    else
                    {
                        fname = file.FileName;

                    }

                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/Products/"), fname);

                    file.SaveAs(fname);

                    string physicalpath = "images/Products/" + file.FileName;

                    //string physicalpath = "http://diamtrade.cricketclubs.in/images/Products/" + file.FileName;



                    Mst_Diamx pm = new Mst_Diamx();
                    pm = (db.Mst_Diamx.Where(r => r.DiamxID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.ProductLogo = physicalpath;
                        db.SaveChanges();
                    }

                    // Returns message that successfully uploaded  
                    return Json("Product Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        public ActionResult InsertDiamxProductFeature(Mst_DiamX_Features sf)
        {
            try
            {
                db.Mst_DiamX_Features.Add(sf);
                db.SaveChanges();
                int i = 0;
                i = sf.Id;
                return Json(i, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult UploadDiamxProductFeatureImage(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                    //string filename = Path.GetFileName(Request.Files[i].FileName);  

                    HttpPostedFileBase file = files[0];


                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }

                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);

                    file.SaveAs(fname);


                    string physicalpath = "images/" + file.FileName;

                    //string physicalpath = "http://diamtrade.cricketclubs.in/images/" + file.FileName;


                    Mst_DiamX_Features pm = new Mst_DiamX_Features();
                    pm = (db.Mst_DiamX_Features.Where(r => r.Id == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Image = physicalpath;
                        db.SaveChanges();
                    }

                    // Returns message that successfully uploaded  
                    return Json("product feature Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        public JsonResult BindDiamxProductFeature()
        {
            try
            {
                var md = db.Mst_DiamX_Features.ToList();
                return Json(md, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult EditDiamxFeature_new(int Desktopfeatureid)
        {
            try
            {
                Mst_DiamX_Features df = new Mst_DiamX_Features();
                df = (db.Mst_DiamX_Features.Where(r => r.Id == Desktopfeatureid)).SingleOrDefault();
                return Json(df, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult UpdateDiamxProductFeature(Mst_DiamX_Features sf)
        {
            try
            {
                Mst_DiamX_Features df = new Mst_DiamX_Features();
                df = (db.Mst_DiamX_Features.Where(r => r.Id == sf.Id)).SingleOrDefault();
                df.ProductName = sf.ProductName;
                df.Feature_Name = sf.Feature_Name;
                db.SaveChanges();
                int i = 0;
                i = df.Id;
                return Json(i, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult DeleteDiamxProductFeature(Mst_DiamX_Features brc)
        {
            try
            {
                var msg = "";
                var success = "";

                Mst_DiamX_Features emp = new Mst_DiamX_Features();
                emp = db.Mst_DiamX_Features.Where(empp => empp.Id == brc.Id).SingleOrDefault();
                db.Mst_DiamX_Features.Remove(emp);
                db.SaveChanges();
                msg = "Diamx Type Feature Deleted Successfully..";
                success = "true";
                var resultm = new { msg, success };
                return Json(resultm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Support
        [AcceptVerbs("GET")]
        public JsonResult EditSupport()
        {
            SupportClass supportclass = new SupportClass();
            SupportData supportdata = new SupportData();

            supportdata.support = (from abt in db.Mst_Support
                                   select new SupportClass
                                   {
                                       SupportID = abt.SupportID,
                                       Content1 = abt.Content1,
                                       Content2 = abt.Content2,
                                       Content3 = abt.Content3,
                                       Content4 = abt.Content4,
                                       Title1 = abt.Title1,
                                       Title2 = abt.Title2,
                                       SubTitle = abt.SubTitle,
                                       MobileNo = abt.mobileNo,
                                       SupportName = abt.SupportName,
                                       EmailID = abt.EmailID,
                                       Image_Banner = abt.Image_Banner

                                   }).ToList();

            if (supportdata.support.Count > 0)
            {
                var support = "";
                supportdata.success = true;
                var result = new { support = supportdata.support, supportdata.success };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                supportdata.success = false;
                //return Request.CreateResponse(HttpStatusCode.OK, aboutdata);
                return Json(supportdata, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UpdateSupport(SupportClass abtus)
        {
            try
            {
                int SupportID = Convert.ToInt32(abtus.SupportID);
                Mst_Support bmst = new Mst_Support();
                bmst = db.Mst_Support.Where(a => a.SupportID == SupportID).SingleOrDefault();
                bmst.Content1 = abtus.Content1;
                bmst.Content2 = abtus.Content2;
                bmst.Title1 = abtus.Title1;
                bmst.Title2 = abtus.Title2;
                bmst.mobileNo = abtus.MobileNo;
                bmst.SupportName = abtus.SupportName;
                bmst.EmailID = abtus.EmailID;
                bmst.SubTitle = abtus.SubTitle;
                bmst.Content3 = abtus.Content3;
                bmst.Content4 = abtus.Content4;
                db.SaveChanges();
                return Json("Success");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult UploadSupportBanner_image(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_Support pm = new Mst_Support();
                    pm = (db.Mst_Support.Where(r => r.SupportID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Image_Banner = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }



        #endregion

        #region Features
        [AcceptVerbs("GET")]
        public JsonResult EditCloudFeature(int producttypeid)
        {
            CloudFeatClass cloudclass = new CloudFeatClass();
            CloudFeatData clouddata = new CloudFeatData();

            clouddata.cloudfeat = (from abt in db.Mst_CloudFeature
                                   where abt.ProductTypeID == producttypeid
                                   select new CloudFeatClass
                                   {
                                       CloudFeatID = abt.CloudFeatID,
                                       ProductTypeID = abt.ProductTypeID,
                                       Feature1 = abt.Feature1,
                                       Feature2 = abt.Feature2,
                                       Feature3 = abt.Feature3,
                                       Feature4 = abt.Feature4,
                                       Feature5 = abt.Feature5,
                                       Feature6 = abt.Feature6,
                                       Feature7 = abt.Feature7,
                                       Feature8 = abt.Feature8,
                                       Feature9 = abt.Feature9,
                                       Img_1 = abt.Img_1,
                                       Img_2 = abt.Img_2,
                                       Img_3 = abt.Img_3,
                                       Img_4 = abt.Img_4,
                                       Img_5 = abt.Img_5

                                   }).ToList();

            if (clouddata.cloudfeat.Count > 0)
            {
                var cloudfeat = "";
                clouddata.success = true;
                var result = new { cloudfeat = clouddata.cloudfeat, clouddata.success };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                clouddata.success = false;
                //return Request.CreateResponse(HttpStatusCode.OK, aboutdata);
                return Json(clouddata, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UpdateCloudFeat(CloudFeatClass abtus)
        {
            try
            {
                int ProductId = Convert.ToInt32(abtus.ProductTypeID);
                Mst_CloudFeature bmst = new Mst_CloudFeature();
                bmst = db.Mst_CloudFeature.Where(a => a.ProductTypeID == ProductId).SingleOrDefault();
                bmst.Feature1 = abtus.Feature1;
                bmst.Feature2 = abtus.Feature2;
                bmst.Feature3 = abtus.Feature3;
                bmst.Feature4 = abtus.Feature4;
                bmst.Feature5 = abtus.Feature5;
                bmst.Feature6 = abtus.Feature6;
                bmst.Feature7 = abtus.Feature7;
                bmst.Feature8 = abtus.Feature8;
                bmst.Feature9 = abtus.Feature9;
                db.SaveChanges();
                return Json("Success");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult UploadCloudImage_1(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_CloudFeature pm = new Mst_CloudFeature();
                    pm = (db.Mst_CloudFeature.Where(r => r.ProductTypeID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Img_1 = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult UploadCloudImage_2(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_CloudFeature pm = new Mst_CloudFeature();
                    pm = (db.Mst_CloudFeature.Where(r => r.ProductTypeID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Img_2 = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult UploadCloudImage_3(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_CloudFeature pm = new Mst_CloudFeature();
                    pm = (db.Mst_CloudFeature.Where(r => r.ProductTypeID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Img_3 = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult UploadCloudImage_4(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_CloudFeature pm = new Mst_CloudFeature();
                    pm = (db.Mst_CloudFeature.Where(r => r.ProductTypeID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Img_4 = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult UploadCloudImage_5(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_CloudFeature pm = new Mst_CloudFeature();
                    pm = (db.Mst_CloudFeature.Where(r => r.ProductTypeID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Img_5 = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        [AcceptVerbs("GET")]
        public JsonResult EditDeskFeature(int producttypeid)
        {
            DeskFeatClass deskclass = new DeskFeatClass();
            DeskFeatData deskdata = new DeskFeatData();

            deskdata.deskfeat = (from abt in db.Mst_DeskFeature
                                 where abt.ProductTypeID == producttypeid
                                 select new DeskFeatClass

                                 {
                                     DeskFeatureID = abt.DeskFeatureID,
                                     ProductTypeID = abt.ProductTypeID,
                                     Feature1 = abt.Feature1,
                                     Feature2 = abt.Feature2,
                                     Feature3 = abt.Feature3,
                                     Feature4 = abt.Feature4,
                                     Feature5 = abt.Feature5,
                                     Feature6 = abt.Feature6,
                                     Feature7 = abt.Feature7,
                                     Feature8 = abt.Feature8,
                                     Img_1 = abt.Img_1,
                                     Img_2 = abt.Img_2,
                                     Img_3 = abt.Img_3,
                                     Img_4 = abt.Img_4,
                                     Img_5 = abt.Img_5,
                                     Img_6 = abt.Img_6,
                                     Img_7 = abt.Img_7,
                                     Img_8 = abt.Img_8

                                 }).ToList();

            if (deskdata.deskfeat.Count > 0)
            {
                var deskfeat = "";
                deskdata.success = true;
                var result = new { deskfeat = deskdata.deskfeat, deskdata.success };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                deskdata.success = false;
                //return Request.CreateResponse(HttpStatusCode.OK, aboutdata);
                return Json(deskdata, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UpdateDeskFeat(DeskFeatClass abtus)
        {
            try
            {
                int producttypeid = Convert.ToInt32(abtus.ProductTypeID);
                Mst_DeskFeature bmst = new Mst_DeskFeature();
                bmst = db.Mst_DeskFeature.Where(a => a.ProductTypeID == producttypeid).SingleOrDefault();
                bmst.Feature1 = abtus.Feature1;
                bmst.Feature2 = abtus.Feature2;
                bmst.Feature3 = abtus.Feature3;
                bmst.Feature4 = abtus.Feature4;
                bmst.Feature5 = abtus.Feature5;
                bmst.Feature6 = abtus.Feature6;
                bmst.Feature7 = abtus.Feature7;
                bmst.Feature8 = abtus.Feature8;
                db.SaveChanges();
                return Json("Success");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult UploadDesktopImage_1(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_DeskFeature pm = new Mst_DeskFeature();
                    pm = (db.Mst_DeskFeature.Where(r => r.ProductTypeID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Img_1 = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult UploadDesktopImage_2(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_DeskFeature pm = new Mst_DeskFeature();
                    pm = (db.Mst_DeskFeature.Where(r => r.ProductTypeID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Img_2 = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult UploadDesktopImage_3(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_DeskFeature pm = new Mst_DeskFeature();
                    pm = (db.Mst_DeskFeature.Where(r => r.ProductTypeID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Img_3 = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult UploadDesktopImage_4(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_DeskFeature pm = new Mst_DeskFeature();
                    pm = (db.Mst_DeskFeature.Where(r => r.ProductTypeID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Img_4 = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult UploadDesktopImage_5(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_DeskFeature pm = new Mst_DeskFeature();
                    pm = (db.Mst_DeskFeature.Where(r => r.ProductTypeID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Img_5 = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult UploadDesktopImage_6(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_DeskFeature pm = new Mst_DeskFeature();
                    pm = (db.Mst_DeskFeature.Where(r => r.ProductTypeID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Img_6 = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult UploadDesktopImage_7(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_DeskFeature pm = new Mst_DeskFeature();
                    pm = (db.Mst_DeskFeature.Where(r => r.ProductTypeID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Img_7 = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult UploadDesktopImage_8(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_DeskFeature pm = new Mst_DeskFeature();
                    pm = (db.Mst_DeskFeature.Where(r => r.ProductTypeID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Img_8 = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        #endregion

        #region Products
        [AcceptVerbs("GET")]
        public JsonResult BindClientImage()
        {
            OurClientData sd = new OurClientData();
            sd.ourclient = (from s in db.Mst_OurClientImages
                            select new OurClientClass
                            {
                                ClientImageID = s.ClientImageID,
                                ClientImage = s.ClientImage,
                                SortOrder = s.SortOrder,
                                IsActive = s.IsActive.ToString()
                            }).ToList();

            if (sd.ourclient.Count > 0)
                sd.success = true;
            else
                sd.success = false;
            return Json(sd, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertClientImage(Mst_OurClientImages brc)
        {
            try
            {
                var msg = "";
                var success = "";
                var result = db.Mst_OurClientImages.Where(a => a.ClientImage == brc.ClientImage).ToList();
                if (result != null && result.Count > 0)
                {
                    msg = "Client Image Already Exists..";
                    success = "false";
                }
                else
                {
                    //brc.IsActive = true;
                    db.Mst_OurClientImages.Add(brc);
                    db.SaveChanges();
                    OurClientIDNew = brc.ClientImageID;
                    msg = "Client Image Inserted Successfully..";
                    success = "true";
                }
                var resultm = new { msg, success };
                return Json(resultm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AcceptVerbs("GET")]
        public JsonResult EditClientImage(int ClientImageID)
        {
            OurClientClass clientimgclass = new OurClientClass();
            OurClientData clientimgdata = new OurClientData();

            clientimgdata.ourclient = (from emp in db.Mst_OurClientImages
                                       where emp.ClientImageID == ClientImageID
                                       select new OurClientClass
                                       {
                                           ClientImageID = emp.ClientImageID,
                                           ClientImage = emp.ClientImage,
                                           SortOrder = emp.SortOrder,
                                           IsActive = emp.IsActive.ToString()
                                       }).ToList();

            if (clientimgdata.ourclient.Count > 0)
            {
                var ourclient = "";
                clientimgdata.success = true;
                var result = new { ourclient = clientimgdata.ourclient, clientimgdata.success };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                clientimgdata.success = false;
                return Json(clientimgdata, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UpdateClientImage(Mst_OurClientImages brc)
        {
            try
            {
                var msg = "";
                var success = "";
                var result = db.Mst_OurClientImages.Where(a => a.ClientImage == brc.ClientImage && a.ClientImageID != brc.ClientImageID).ToList();
                if (result != null && result.Count > 0)
                {
                    msg = "Client Image Already Exists..";
                    success = "false";
                }
                else
                {
                    int ClientImageID = brc.ClientImageID;
                    Mst_OurClientImages emp = new Mst_OurClientImages();
                    emp = db.Mst_OurClientImages.Where(empp => empp.ClientImageID == ClientImageID).SingleOrDefault();
                    emp.ClientImage = brc.ClientImage;
                    emp.SortOrder = brc.SortOrder;
                    emp.IsActive = brc.IsActive;
                    db.SaveChanges();
                    OurClientIDNew = ClientImageID;
                    msg = "Client Image Updated Successfully..";
                    success = "true";
                }
                var resultm = new { msg, success };
                return Json(resultm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult DeleteClientImage(Mst_OurClientImages brc)
        {
            try
            {
                var msg = "";
                var success = "";
                int ClientImageID = brc.ClientImageID;
                Mst_OurClientImages emp = new Mst_OurClientImages();
                emp = db.Mst_OurClientImages.Where(empp => empp.ClientImageID == ClientImageID).SingleOrDefault();
                db.Mst_OurClientImages.Remove(emp);
                db.SaveChanges();
                msg = "Client Image Deleted Successfully..";
                success = "true";
                var resultm = new { msg, success };
                return Json(resultm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public ActionResult FileUpload(HttpPostedFileBase file)
        {

            if (file != null)
            {
                int OurClientID = OurClientIDNew;
                DiamtradeAdminEntities db = new DiamtradeAdminEntities();
                string ImageName = System.IO.Path.GetFileName(file.FileName);
                string physicalPath = Server.MapPath("~/images/OurClient/" + ImageName);

                // save image in folder
                file.SaveAs(physicalPath);

                //Uri baseuri = new Uri(Request.PhysicalPath.Replace(Request.PhysicalPath.PathAndQuery, string.Empty));
                //string fileRelativePath = "~/Upload/" + ImageName;
                //Uri fileFullPath = new Uri(baseuri, VirtualPathUtility.ToAbsolute(fileRelativePath));
                //savedFilePath.Add(fileFullPath.ToString());

                //save new record in database
                //Mst_OurClientImages newRecord = new Mst_OurClientImages();
                //newRecord.ClientImage = ImageName;
                //db.Mst_OurClientImages.Add(newRecord);
                //db.SaveChanges();
                //var filepathn = "http://localhost:65144/images/OurClient/" + ImageName;
                var filepathn = "images/OurClient/" + ImageName;
                Mst_OurClientImages emp = new Mst_OurClientImages();
                emp = db.Mst_OurClientImages.Where(empp => empp.ClientImageID == OurClientID).SingleOrDefault();
                //emp.ClientImage = ImageName;
                //emp.ClientImage = physicalPath;
                emp.ClientImage = filepathn;
                db.SaveChanges();

            }
            //Display records
            return RedirectToAction("../home/Display/");
        }

        #region FrontEndData
        [AcceptVerbs("GET")]
        public JsonResult BindProductsMenu()
        {
            ProductsData sd = new ProductsData();
            sd.products = (from s in db.Mst_Product
                           where s.IsActive == true
                           orderby s.SortOrder ascending
                           select new ProductsClass
                           {
                               ProductID = s.ProductID,
                               ProductName = s.ProductName,
                               SortOrder = s.SortOrder,
                               ShortContent = s.ShortContent,
                               ProductLogo = s.ProductLogo,
                               ProductImage = s.ProductImage,
                               IsActive = s.IsActive.ToString()
                           }).ToList();


            if (sd.products.Count > 0)
                sd.success = true;
            else
                sd.success = false;
            return Json(sd, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs("GET")]
        public JsonResult BindProductsDesc(ProductsClass pr)
        {
            ProductsData sd = new ProductsData();
            sd.products = (from s in db.Mst_Product
                           where s.IsActive == true && s.ProductID == pr.ProductID
                           orderby s.SortOrder ascending
                           select new ProductsClass
                           {
                               ProductID = s.ProductID,
                               ProductName = s.ProductName,
                               SortOrder = s.SortOrder,
                               ShortContent = s.ShortContent,
                               ProductLogo = s.ProductLogo,
                               ProductImage = s.ProductImage,
                               IsActive = s.IsActive.ToString()
                           }).ToList();


            if (sd.products.Count > 0)
                sd.success = true;
            else
                sd.success = false;
            return Json(sd, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region StaticTestimonialNew
        public ActionResult Testimonial_New()
        {
            return View();
        }

        public JsonResult EditStaticTestimonial()
        {
            Mst_TestimonialMain ms = new Mst_TestimonialMain();
            ms = (db.Mst_TestimonialMain.Where(r => r.TestMID == 1)).SingleOrDefault();
            return Json(ms, JsonRequestBehavior.AllowGet);

        }

        public JsonResult UpdateStaticTestimonial(Mst_TestimonialMain tm)
        {
            string msg = "";
            try
            {
                Mst_TestimonialMain st = new Mst_TestimonialMain();
                st = (db.Mst_TestimonialMain.Where(r => r.TestMID == tm.TestMID)).FirstOrDefault();
                st.name_1 = tm.name_1;
                st.name_2 = tm.name_2;
                st.name_3 = tm.name_3;
                st.name_4 = tm.name_4;
                st.name_5 = tm.name_5;
                st.name_6 = tm.name_6;
                st.name_7 = tm.name_7;
                st.name_8 = tm.name_8;

                st.Content_1 = tm.Content_1;
                st.Content_2 = tm.Content_2;
                st.Content_3 = tm.Content_3;
                st.Content_4 = tm.Content_4;
                st.Content_5 = tm.Content_5;
                st.Content_6 = tm.Content_6;
                st.Content_7 = tm.Content_7;
                st.Content_8 = tm.Content_8;

                db.SaveChanges();
                msg = "success";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch
            {

                msg = "false";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UploadTestimonialImage_1(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_TestimonialMain pm = new Mst_TestimonialMain();
                    pm = (db.Mst_TestimonialMain.Where(r => r.TestMID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.img_1 = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult UploadTestimonialImage_2(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_TestimonialMain pm = new Mst_TestimonialMain();
                    pm = (db.Mst_TestimonialMain.Where(r => r.TestMID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.img_2 = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult UploadTestimonialImage_3(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_TestimonialMain pm = new Mst_TestimonialMain();
                    pm = (db.Mst_TestimonialMain.Where(r => r.TestMID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.img_3 = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult UploadTestimonialImage_4(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_TestimonialMain pm = new Mst_TestimonialMain();
                    pm = (db.Mst_TestimonialMain.Where(r => r.TestMID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.img_4 = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult UploadTestimonialImage_5(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_TestimonialMain pm = new Mst_TestimonialMain();
                    pm = (db.Mst_TestimonialMain.Where(r => r.TestMID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.img_5 = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult UploadTestimonialImage_6(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_TestimonialMain pm = new Mst_TestimonialMain();
                    pm = (db.Mst_TestimonialMain.Where(r => r.TestMID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.img_6 = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult UploadTestimonialImage_7(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_TestimonialMain pm = new Mst_TestimonialMain();
                    pm = (db.Mst_TestimonialMain.Where(r => r.TestMID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.img_7 = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult UploadTestimonialImage_8(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);
                    file.SaveAs(fname);

                    string locallink = ConfigurationManager.AppSettings["localimagelink"];
                    string onlinelink = ConfigurationManager.AppSettings["onlineimagelink"];
                    string physicalpath = "images/" + file.FileName;
                    //string physicalpath = onlinelink + file.FileName;
                    Mst_TestimonialMain pm = new Mst_TestimonialMain();
                    pm = (db.Mst_TestimonialMain.Where(r => r.TestMID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.img_8 = physicalpath;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("Service Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        #endregion

        #region gallary
        public ActionResult GalleryTab()
        {
            return View();
        }
        public JsonResult InsertGalleryTab(Mst_TabMaster tb)
        {
            try
            {
                db.Mst_TabMaster.Add(tb);
                db.SaveChanges();
                return Json(tb.Tab_Id, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public JsonResult UpdateGalleryTab(Mst_TabMaster tb)
        {
            try
            {
                Mst_TabMaster tm = new Mst_TabMaster();
                tm = db.Mst_TabMaster.Where(r => r.Tab_Id == tb.Tab_Id).FirstOrDefault();
                tm.TabName = tb.TabName;
                tm.SortOrder = tb.SortOrder;
                db.SaveChanges();
                return Json(tb.Tab_Id, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public JsonResult BindGalleryTab()
        {
            try
            {
                var data = db.Mst_TabMaster.Where(r => r.TabName != "").OrderBy(r => r.SortOrder).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public JsonResult EditTab(int Tab_Id)
        {
            try
            {
                Mst_TabMaster tm = new Mst_TabMaster();
                tm = db.Mst_TabMaster.Where(r => r.Tab_Id == Tab_Id).FirstOrDefault();
                return Json(tm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public JsonResult DeleteTab(Mst_TabMaster brc)
        {
            try
            {
                var msg = "";
                var success = "";
                int tabid = brc.Tab_Id;
                Mst_TabMaster emp = new Mst_TabMaster();
                emp = db.Mst_TabMaster.Where(empp => empp.Tab_Id == tabid).SingleOrDefault();
                db.Mst_TabMaster.Remove(emp);
                db.SaveChanges();
                msg = "Tab Deleted Successfully..";
                success = "true";
                var resultm = new { msg, success };
                return Json(resultm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GalleryTabImage
        public ActionResult GalleryImage()
        {
            return View();
        }

        public JsonResult InsertGalleryImage(Mst_TabImage tb)
        {
            try
            {
                Mst_TabImage tm = new Mst_TabImage();
                tm.Tab_Id = tb.Tab_Id;
                tm.CreateBy = 1;
                tm.CreateDate = DateTime.UtcNow;
                db.Mst_TabImage.Add(tm);
                db.SaveChanges();
                int i = tm.Img_Id;
                return Json(i, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public JsonResult UpdateGalleryImage(Mst_TabImage tb)
        {
            try
            {
                Mst_TabImage tm = new Mst_TabImage();
                tm = (db.Mst_TabImage.Where(r => r.Img_Id == tb.Img_Id)).SingleOrDefault();
                tm.Tab_Id = tb.Tab_Id;
                tm.CreateBy = 1;
                tm.CreateDate = DateTime.UtcNow;
                db.SaveChanges();
                return Json(tb.Img_Id, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult UploadGalleryimage(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string productid = collection["Id"];
                    int tid = Convert.ToInt32(productid);

                    //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                    //string filename = Path.GetFileName(Request.Files[i].FileName);  

                    HttpPostedFileBase file = files[0];


                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }

                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);

                    file.SaveAs(fname);


                    string physicalpath = "images/" + file.FileName;

                    //string physicalpath = "http://diamtrade.cricketclubs.in/images/Products/" + file.FileName;


                    Mst_TabImage pm = new Mst_TabImage();
                    pm = (db.Mst_TabImage.Where(r => r.Img_Id == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Tab_Image = physicalpath;
                        db.SaveChanges();
                    }

                    // Returns message that successfully uploaded  
                    return Json("image feature Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        public JsonResult EditImage(int Img_Id)
        {
            try
            {
                Mst_TabImage tm = new Mst_TabImage();
                tm = db.Mst_TabImage.Where(r => r.Img_Id == Img_Id).FirstOrDefault();
                return Json(tm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult BindImages()
        {
            try
            {
                var data = (from i in db.Mst_TabImage
                            join t in db.Mst_TabMaster on i.Tab_Id equals t.Tab_Id
                            select new { i, t }).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult DeleteTabImage(Mst_TabImage brc)
        {
            try
            {
                var msg = "";
                var success = "";
                int imgid = brc.Img_Id;
                Mst_TabImage emp = new Mst_TabImage();
                emp = db.Mst_TabImage.Where(empp => empp.Img_Id == imgid).SingleOrDefault();
                db.Mst_TabImage.Remove(emp);
                db.SaveChanges();
                msg = "Tab image Deleted Successfully..";
                success = "true";
                var resultm = new { msg, success };
                return Json(resultm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public JsonResult BindAlbumImage(int tabid, string image)
        {
            try
            {
                var data = db.Mst_TabImage.Where(r => r.Tab_Id == tabid && r.Tab_Image != image).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion



        #region newslider
        public ActionResult SliderNew()
        {
            return View();
        }
        public JsonResult InsertSlider()
        {
            Mst_Slider sm = new Mst_Slider();
            sm.IsActive = true;
            db.Mst_Slider.Add(sm);
            db.SaveChanges();
            return Json(sm.SliderID, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UploadSlider(FormCollection collection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string sliderid = collection["Id"];
                    int tid = Convert.ToInt32(sliderid);

                    //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                    //string filename = Path.GetFileName(Request.Files[i].FileName);  

                    HttpPostedFileBase file = files[0];


                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }

                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/images/"), fname);

                    file.SaveAs(fname);


                    string physicalpath = "http://18.218.220.245:8080/DiamTrade.com/images/" + file.FileName;

                    //string physicalpath = "http://diamtrade.cricketclubs.in/images/Products/" + file.FileName;


                    Mst_Slider pm = new Mst_Slider();
                    pm = (db.Mst_Slider.Where(r => r.SliderID == tid)).SingleOrDefault();
                    if (pm != null)
                    {
                        pm.Image_1 = physicalpath;
                        db.SaveChanges();
                    }

                    // Returns message that successfully uploaded  
                    return Json("image feature Uploaded!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        public JsonResult UpdateSlider_New(int SliderID)
        {
            Mst_Slider sm = new Mst_Slider();
            sm = db.Mst_Slider.Where(r => r.SliderID == SliderID).FirstOrDefault();
            sm.IsActive = true;
            db.SaveChanges();
            return Json(sm.SliderID, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditSlider_New(int Sliderid)
        {
            try
            {
                Mst_Slider tm = new Mst_Slider();
                tm = db.Mst_Slider.Where(r => r.SliderID == Sliderid).FirstOrDefault();
                return Json(tm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public JsonResult BindSlider()
        {
            try
            {
                var data = db.Mst_Slider.ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult DeleteSlider(Mst_Slider brc)
        {
            try
            {
                var msg = "";
                var success = "";
                int SliderID = brc.SliderID;
                Mst_Slider emp = new Mst_Slider();
                emp = db.Mst_Slider.Where(empp => empp.SliderID == SliderID).SingleOrDefault();
                db.Mst_Slider.Remove(emp);
                db.SaveChanges();
                msg = "Slider image Deleted Successfully..";
                success = "true";
                var resultm = new { msg, success };
                return Json(resultm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Galleryforlayout
        public JsonResult BindGallery()
        {
            try
            {
                var data = (from i in db.Mst_TabMaster
                            where i.TabName != ""
                            orderby i.SortOrder
                            select new GalleryAlbum
                            {
                                Tab_Id = i.Tab_Id,
                                TabName = i.TabName,
                                countimages = (db.Mst_TabImage.Where(r => r.Tab_Id == i.Tab_Id)).Count(),
                                displayimage = (db.Mst_TabImage.Where(r => r.Tab_Id == i.Tab_Id).Select(r => r.Tab_Image).FirstOrDefault())
                            }).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion
    }
}