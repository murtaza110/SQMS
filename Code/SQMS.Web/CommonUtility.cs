using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Configuration;

namespace SQMS.Web
{
    public class CommonUtility
    {
        public const string AdminRole = "1";
        public const string SubAdminRole = "1, 2";
        public const string MoallimRole = "1, 2, 3";
        public const string MemberRole = "1, 2, 3, 5";

        private static CommonUtility objCommonUtility = null;

        private CommonUtility()
        {
            int nWidth = Convert.ToInt32(ConfigurationManager.AppSettings["CardImageSizeWidth"]);
            int nHeight = Convert.ToInt32(ConfigurationManager.AppSettings["CardImageSizeHeigth"]);
            CardImageSize = new Size(nWidth, nHeight);
        }

        public static CommonUtility Instance
        {
            get
            {
                if (objCommonUtility == null)
                    objCommonUtility = new CommonUtility();

                return objCommonUtility;
            }
        }

        public Size CardImageSize { get; set; }
    }

    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            ViewResult result = new ViewResult();
            result.ViewName = "~/Views/Shared/Error.cshtml";
            result.ViewBag.ErrorMessage = "Access Denied...!!";
            filterContext.Result = result;
        }
    }

    public class ImageResult : ActionResult
    {
        private readonly System.Drawing.Image _image;
        public ImageResult(System.Drawing.Image image)
        {
            _image = image;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            int nWidth = Convert.ToInt32(ConfigurationManager.AppSettings["CardImageSizeWidth"]);
            int nHeight = Convert.ToInt32(ConfigurationManager.AppSettings["CardImageSizeHeigth"]);
            Bitmap bmp = new Bitmap(_image, new Size(nWidth, nHeight));
            context.HttpContext.Response.ContentType = "image/jpg";
            bmp.Save(context.HttpContext.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
    }
}