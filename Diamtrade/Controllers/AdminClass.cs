using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diamtrade.Controllers
{
    public class AdminClass
    {
    }
    #region LoginData
    public class Credential
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class LoginClass
    {
        public int? UserID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string EmailID { get; set; }
        public string ContactNo { get; set; }
        public string IsActive { get; set; }
        public string CreateDate { get; set; }
        public string CreateBy { get; set; }
    }
    public class LoginData
    {
        public bool success { get; set; }
        public string msg { get; set; }
        public List<LoginClass> userlogin { get; set; }
    }
    #endregion
    #region AboutUs
    public class AboutUsClass
    {
        public int? AboutID { get; set; }
        public string AboutTitle { get; set; }
        public string AboutContent { get; set; }
        public string AboutTitle1 { get; set; }
        public string AboutContent1 { get; set; }
        public string AboutTitle2 { get; set; }
        public string AboutContent2 { get; set; }
        public string AboutTitle3 { get; set; }
        public string AboutContent3 { get; set; }
        public string AboutTitle4 { get; set; }
        public string AboutContent4 { get; set; }
        public int CreatedBy { get; set; }
        public string CreateDate { get; set; }
        public bool IsActive { get; set; }
        public string Image_Banner { get; set; }
        public string Image_Business { get; set; }
        public string Image_Access { get; set; }
        public string Image_Safest_Fastest { get; set; }
    }
    public class AboutUsData
    {
        public bool success { get; set; }
        public string msg { get; set; }
        public List<AboutUsClass> data { get; set; }
    }
    #endregion

    #region Slider
    public class SliderClass
    {
        public int? SliderID { get; set; }
        public string Title1 { get; set; }
        public string Content1 { get; set; }
        public string Title2 { get; set; }
        public string Content2 { get; set; }
        public string Title3 { get; set; }
        public string Content3 { get; set; }
        public int CreateBy { get; set; }
        public string CreateDate { get; set; }
        public bool IsActive { get; set; }

        public string Image_1 { get; set; }
        public string Image_2 { get; set; }
        public string Image_3 { get; set; }
        public string Image_4 { get; set; }

        public string Title4 { get; set; }
        public string Content4 { get; set; }
    }
    public class SliderData
    {
        public bool success { get; set; }
        public string msg { get; set; }
        public List<SliderClass> sliders { get; set; }
    }
    #endregion

    #region HomePage
    public class HomePagerClass
    {
        public int? HomePageID { get; set; }
        public string Title1 { get; set; }
        public string SubTitle1 { get; set; }
        public string Content1 { get; set; }
        public string Title2 { get; set; }
        public string Content2 { get; set; }
        public string Title3 { get; set; }
        public string Content3 { get; set; }
        public int CreateBy { get; set; }
        public string CreateDate { get; set; }
        public bool IsActive { get; set; }

        public string Image_1 { get; set; }
        public string Image_2 { get; set; }


        public string Title4 { get; set; }
        public string Content4 { get; set; }
        public string Image_4 { get; set; }
    }
    public class HomePageData
    {
        public bool success { get; set; }
        public string msg { get; set; }
        public List<HomePagerClass> homepage { get; set; }
    }
    #endregion

    #region ServiceType
    public class ServiceTypeClass
    {
        public int? ServiceTypeID { get; set; }
        public string ServiceType { get; set; }
        public string ServiceTypeImage { get; set; }
        public int? SortOrder { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateBy { get; set; }
        public string IsActive { get; set; }

        public string ServiceBanner { get; set; }
        public string Service_Heading { get; set; }
        public string Service_Desc1 { get; set; }
        public string Service_Desc2 { get; set; }
        public string Service_img1 { get; set; }
        public string Service_img2 { get; set; }
        public string Service_ing3 { get; set; }
        public string img1_Content1 { get; set; }
        public string img2_Conrent2 { get; set; }
        public string img3_Content3 { get; set; }
        public string Service_title1 { get; set; }
        public string Service_title2 { get; set; }
        public string Service_title3 { get; set; }
    }
    public class ServiceTypeData
    {
        public bool success { get; set; }
        public string msg { get; set; }
        public List<ServiceTypeClass> servicetype { get; set; }
    }
    public class ServiceTypeFeatureData
    {
        public bool success { get; set; }
        public string msg { get; set; }
        public List<ServiceTypeFeatureClass> servicefeauretype { get; set; }
    }

    public class ServiceTypeFeatureClass
    {
        public int Service_Features_Id { get; set; }
        public Nullable<int> ServiceType_Id { get; set; }
        public string Service_Feature_Title { get; set; }
        public string Service_Freature_img { get; set; }
        public string Service_Feature_Desc { get; set; }
        public string ServiceFeatureName { get; set; }

    }
    #endregion

    #region Services
    public class ServicesClass
    {
        public int? ServiceID { get; set; }
        public Nullable<int> ServiceTypeID { get; set; }
        public string ServiceTypeName { get; set; }
        public string ServiceName { get; set; }
        public int? SortOrder { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateBy { get; set; }
        public string IsActive { get; set; }
        public string servicetypeimage { get; set; }
    }
    public class ServicesData
    {
        public bool success { get; set; }
        public string msg { get; set; }
        public List<ServicesClass> services { get; set; }
    }
    #endregion
    #region Testimonial
    public class TestimonialClass
    {
        public int? TestimonialID { get; set; }
        public string TestimonialMainTitle { get; set; }
        public string TestiTitle { get; set; }
        public string TestimComments { get; set; }
        public string PersonImage { get; set; }
        public string PersonName { get; set; }
        public int? SortOrder { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateBy { get; set; }
        public string IsActive { get; set; }
    }
    public class TestimonialData
    {
        public bool success { get; set; }
        public string msg { get; set; }
        public List<TestimonialClass> testimoni { get; set; }
    }
    #endregion
    #region Products
    public class ProductsClass
    {
        public int? ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductLogo { get; set; }
        public string ProductImage { get; set; }
        public string ShortContent { get; set; }
        public int? SortOrder { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateBy { get; set; }
        public string IsActive { get; set; }
    }
    public class ProductsData
    {
        public bool success { get; set; }
        public string msg { get; set; }
        public List<ProductsClass> products { get; set; }
    }
    #endregion

    #region Inquiry
    public class InquiryClass
    {
        public int? InquiryID { get; set; }
        public string InquiryDesc { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateBy { get; set; }
        public string IsActive { get; set; }
    }
    public class InquiryData
    {
        public bool success { get; set; }
        public string msg { get; set; }
        public List<InquiryClass> inquiries { get; set; }
    }
    #endregion

    #region InquiryMessage
    public class InquiryMessageClass
    {
        public int? InquiryMessageID { get; set; }
        public string FullName { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public string InquiryType { get; set; }
        public string InquiryMessage { get; set; }
        public string CreateDate { get; set; }
        public int CreateBy { get; set; }
        public string IsActive { get; set; }
    }
    public class InquiryMessageData
    {
        public bool success { get; set; }
        public string msg { get; set; }
        public List<InquiryMessageClass> inquirymsg { get; set; }
    }
    #endregion

    #region Contact
    public class ContactClass
    {
        public int? ContactusID { get; set; }
        public string CotactUsDesc { get; set; }
        public string CotactTitle { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateBy { get; set; }
        public string IsActive { get; set; }
        public string SupportName { get; set; }
        public string TeamViewerName { get; set; }
        public string Image_Banner { get; set; }


    }
    public class ContactData
    {
        public bool success { get; set; }
        public string msg { get; set; }
        public List<ContactClass> contacts { get; set; }
        public List<CareerMeaageClass> career { get; set; }
    }
    #endregion

    #region ContactMessage
    public class ContactMessageClass
    {
        public int? ContactMessageID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public string ContactMessage { get; set; }
        public string CreateDate { get; set; }
        public int CreateBy { get; set; }
        public string IsActive { get; set; }
    }

    public class CareerMeaageClass
    {
        public int SendCarrerID { get; set; }
        public string Prefix { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Educations { get; set; }
        public string Designation { get; set; }
        public string CoreExpert { get; set; }
        public string Exper { get; set; }
        public string PrevCompany { get; set; }
        public string CurrentCompany { get; set; }
        public string CurrentSalary { get; set; }
        public string ExpectedSalary { get; set; }
        public string Reference { get; set; }
        public string AttachDoc { get; set; }
        public string Address { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string AttachPhoto { get; set; }
        public string date { get; set; }
    }
    public class ContactMessageData
    {
        public bool success { get; set; }
        public string msg { get; set; }
        public List<ContactMessageClass> contactmsg { get; set; }
    }
    #endregion

    #region Career
    public class CareerInfoClass
    {
        public int? CarrersID { get; set; }
        public string CarrersTitle { get; set; }
        public string SubTitle { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateBy { get; set; }
        public string IsActive { get; set; }

        public string Image_Banner { get; set; }
    }
    public class CareerInfoData
    {
        public bool success { get; set; }
        public string msg { get; set; }
        public List<CareerInfoClass> careerinfo { get; set; }
    }
    public class CareersClass
    {
        public int? CareerMultiID { get; set; }
        public string Subject { get; set; }
        public string Expe { get; set; }
        public string Location { get; set; }
        public string CareerDesc { get; set; }
        public int? SortOrder { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateBy { get; set; }
        public string IsActive { get; set; }
    }
    public class CareersData
    {
        public bool success { get; set; }
        public string msg { get; set; }
        public List<CareersClass> careers { get; set; }
    }
    #endregion

    #region FooterBlock
    public class FooterBlockClass
    {
        public int? FooterID { get; set; }
        public string FooterAddress { get; set; }
        public string PhoneNo { get; set; }
        public string SupportNo { get; set; }
    }
    public class FooterBlockData
    {
        public bool success { get; set; }
        public string msg { get; set; }
        public List<FooterBlockClass> footerblock { get; set; }
    }
    #endregion

    #region Compare
    public class CompareClass
    {
        public int? CompareID { get; set; }
        public string CompareDesc { get; set; }
        public string Product1Desc { get; set; }
        public string Product2Desc { get; set; }
    }
    public class CompareData
    {
        public bool success { get; set; }
        public string msg { get; set; }
        public List<CompareClass> compare { get; set; }
    }
    #endregion

    #region Desktop
    public class DesktopClass
    {
        public int? DesktopID { get; set; }
        public string Content1 { get; set; }
        public string Content2 { get; set; }
        public string Content3 { get; set; }
        public string Content4 { get; set; }

        public string ProductLogo { get; set; }
        public string ProductImage { get; set; }
        public string Content5 { get; set; }
    }
    public class DesktopData
    {
        public bool success { get; set; }
        public string msg { get; set; }
        public List<DesktopClass> desktop { get; set; }
    }
    #endregion

    #region Cloud
    public class CloudClass
    {
        public int CloudID { get; set; }
        public string Content1 { get; set; }
        public string Content2 { get; set; }
        public string Content3 { get; set; }
        public string Content4 { get; set; }
        public string ProductLogo { get; set; }
        public string ProductImage { get; set; }

        public string Content5 { get; set; }
    }
    public class CloudData
    {
        public bool success { get; set; }
        public string msg { get; set; }
        public List<CloudClass> cloud { get; set; }
    }
    #endregion

    #region Diamx
    public class DiamxClass
    {
        public int DiamxID { get; set; }
        public string Content1 { get; set; }
        public string Content2 { get; set; }
        public string Content3 { get; set; }
        public string Content4 { get; set; }
        public string ProductLogo { get; set; }
        public string ProductImage { get; set; }
        public string Content5 { get; set; }

    }
    public class DiamxData
    {
        public bool success { get; set; }
        public string msg { get; set; }
        public List<DiamxClass> diamx { get; set; }
    }
    #endregion

    #region Support
    public class SupportClass
    {
        public int? SupportID { get; set; }
        public string Content1 { get; set; }
        public string Content2 { get; set; }
        public string Title1 { get; set; }
        public string SubTitle { get; set; }
        public string MobileNo { get; set; }
        public string SupportName { get; set; }
        public string EmailID { get; set; }
        public string Content3 { get; set; }
        public string Title2 { get; set; }
        public string Content4 { get; set; }

        public string Image_Banner { get; set; }
    }
    public class SupportData
    {
        public bool success { get; set; }
        public string msg { get; set; }
        public List<SupportClass> support { get; set; }
    }
    #endregion

    #region Features
    public class CloudFeatClass
    {
        public int CloudFeatID { get; set; }
        public Nullable<int> ProductTypeID { get; set; }
        public string Feature1 { get; set; }
        public string Feature2 { get; set; }
        public string Feature3 { get; set; }
        public string Feature4 { get; set; }
        public string Feature5 { get; set; }
        public string Feature6 { get; set; }
        public string Feature7 { get; set; }
        public string Feature8 { get; set; }
        public string Feature9 { get; set; }
        public string Img_1 { get; set; }
        public string Img_2 { get; set; }
        public string Img_3 { get; set; }
        public string Img_4 { get; set; }
        public string Img_5 { get; set; }
    }
    public class CloudFeatData
    {
        public bool success { get; set; }
        public string msg { get; set; }
        public List<CloudFeatClass> cloudfeat { get; set; }
    }
    public class DeskFeatClass
    {
        public int DeskFeatureID { get; set; }
        public Nullable<int> ProductTypeID { get; set; }
        public string Feature1 { get; set; }
        public string Feature2 { get; set; }
        public string Feature3 { get; set; }
        public string Feature4 { get; set; }
        public string Feature5 { get; set; }
        public string Feature6 { get; set; }
        public string Feature7 { get; set; }
        public string Feature8 { get; set; }
        public string Img_1 { get; set; }
        public string Img_2 { get; set; }
        public string Img_3 { get; set; }
        public string Img_4 { get; set; }
        public string Img_5 { get; set; }
        public string Img_6 { get; set; }
        public string Img_7 { get; set; }
        public string Img_8 { get; set; }
    }
    public class DeskFeatData
    {
        public bool success { get; set; }
        public string msg { get; set; }
        public List<DeskFeatClass> deskfeat { get; set; }
    }

    public class GalleryAlbum
    {
        public int Tab_Id { get; set; }
        public string TabName { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public string IsActive { get; set; }

        public int countimages { get; set; }

        public string displayimage { get; set; }
    }
    #endregion
    #region OurClient
    public class OurClientClass
    {
        public int? ClientImageID { get; set; }
        public string ClientImage { get; set; }
        public int? SortOrder { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateBy { get; set; }
        public string IsActive { get; set; }
    }
    public class OurClientData
    {
        public bool success { get; set; }
        public string msg { get; set; }
        public List<OurClientClass> ourclient { get; set; }
    }
    #endregion
}