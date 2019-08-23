using Diamtrade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diamtrade.Controllers
{

    public class servicesController : Controller
    {
        DiamtradeAdminEntities db = null;
        // GET: services
        public servicesController()
        {
            db = new DiamtradeAdminEntities();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult printing_media()
        {
            return View();
        }
        public ActionResult digital_media()
        {
            return View();
        }
        public ActionResult enterprise_system_integration()
        {
            return View();
        }
        public ActionResult erp_software_service()
        {
            return View();
        }
        public ActionResult graphics_design()
        {
            return View();
        }
        public ActionResult web_development()
        {
            return View();
        }
        public ActionResult ServicePage()
        {
            return View();
        }

        [AcceptVerbs("GET")]
        public JsonResult BindServiceData(string ServiceTypeName)
        {
            ServiceTypeClass servicetypeclass = new ServiceTypeClass();
            ServiceTypeData servicetypedata = new ServiceTypeData();
            ServiceTypeFeatureData servicetypefeaturedata = new ServiceTypeFeatureData();
            string servicename = ServiceTypeName.Replace("%20", " ");
            servicetypedata.servicetype = (from emp in db.Mst_ServicesType
                                           where emp.ServiceType == servicename

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


            servicetypefeaturedata.servicefeauretype = (from sf in db.Mst_Service_Features
                                                        join s in db.Mst_ServicesType on sf.ServiceType_Id equals s.ServiceTypeID
                                                        where s.ServiceType == servicename

                                                        select new ServiceTypeFeatureClass
                                                        {
                                                            Service_Features_Id = sf.Service_Features_Id,
                                                            Service_Feature_Desc = sf.Service_Feature_Desc,
                                                            Service_Feature_Title = sf.Service_Feature_Title,
                                                            Service_Freature_img = sf.Service_Freature_img,
                                                            ServiceFeatureName = s.ServiceType,
                                                            ServiceType_Id = sf.ServiceType_Id
                                                        }).ToList();





            if (servicetypedata.servicetype.Count > 0)
            {
                var servicetype = "";
                var servicetypefeature = "";
                servicetypedata.success = true;
                var result = new { servicetype = servicetypedata.servicetype, servicetypefeature= servicetypefeaturedata.servicefeauretype, servicetypedata.success };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                servicetypedata.success = false;
                return Json(servicetypedata, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs("GET")]
        public JsonResult BindService(int servicetypid)
        {
            ServicesData sd = new ServicesData();
            sd.services = (from s in db.Mst_Services
                           join stype in db.Mst_ServicesType on s.ServiceTypeID equals stype.ServiceTypeID
                           where stype.ServiceTypeID== servicetypid
                           select new ServicesClass
                           {
                               ServiceID = s.ServiceID,
                               ServiceTypeID = s.ServiceTypeID,
                               ServiceTypeName = stype.ServiceType,
                               ServiceName = s.ServiceName,
                               SortOrder = s.SortOrder,
                               IsActive = s.IsActive.ToString(),
                               servicetypeimage=stype.ServiceTypeImage
                           }).ToList();

            if (sd.services.Count > 0)
                sd.success = true;
            else
                sd.success = false;
            return Json(sd, JsonRequestBehavior.AllowGet);
        }
    }
}