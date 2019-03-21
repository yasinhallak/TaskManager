using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TaskManager.Repository;

namespace TaskManager.Controllers
{
    public class BaseController : Controller
    {
        public AdamUnit adamUnit = new AdamUnit();


        public BaseController()
        {

            //Response.Cookies.Append(
            //    CookieRequestCultureProvider.DefaultCookieName,
            //    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(LanguageCode)),
            //    new CookieOptions {Expires = DateTimeOffset.UtcNow.AddYears(1)});
        }
        //protected string LanguageCode => RouteData.Values["language"]?.ToString() ;

     
      
       
        //public IActionResult SetLanguage([FromBody] string culture)
        //{

        //    System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
        //    System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);

        //    _localizer.WithCulture(new CultureInfo(culture));

        //}

    }
}