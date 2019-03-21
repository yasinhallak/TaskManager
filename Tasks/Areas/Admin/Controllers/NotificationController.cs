using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TaskManager.Controllers;
using TaskManager.Data.ViewModel;
using TaskManager.Models;
using WebApiContrib.Formatting;

namespace TaskManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]

    public class NotificationController : BaseController
    {

        public static void SendPushNotification(string fcmToken, string body, string fromName, string Url,string attachment)
        {
            var data = new
            {
                to = fcmToken,
                data = new
                {
                    message = body,
                    name = fromName,
                    tag = Url,
                    filename=attachment
                }
            };
            var json = JsonConvert.SerializeObject(data);
            Byte[] byteArray = Encoding.UTF8.GetBytes(json);
            try
            {
                string SERVER_API_KEY = "AAAAgBMTQH0:APA91bGR9NR-NP0Vw7_N07VVeLC-TyEHRMHk1IzxspuR6Gyd8E-Bc25Cetc4ch7uHsPcZ-WSXYe5n2FaMhXrQXGBfCs2djYghnzt1FyOeWabiRcEyj_G3MUm9CseSOC5TgCRKU9kXIU1";
                var SENDER_ID = "550075842685";
                // Create Request
                WebRequest tRequest;
                tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");     // FCM link
                tRequest.Method = "Post";
                tRequest.ContentType = "application/json";
                tRequest.Headers.Add(string.Format("Authorization: key ={0}", SERVER_API_KEY));     //Server Api Key Header
                tRequest.Headers.Add(string.Format("Sender: id ={0}", SENDER_ID));
                tRequest.ContentLength = byteArray.Length;
                Stream dataStream = tRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                WebResponse tResponse = tRequest.GetResponse();
                dataStream = tResponse.GetResponseStream();
                StreamReader tReader = new StreamReader(dataStream);
                String sResponseFromServer = tReader.ReadToEnd();
                tReader.Close();
                dataStream.Close();
                tResponse.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public JsonResult GetAllNotification()
        {
            var UserName = User.Identity.Name;
            var userId = adamUnit.AppUserRepository.SingleOrDefault(x => x.UserName == UserName).Id;
            var list = adamUnit.NotificationRepository.GetAll(x => x.ToUserId == userId).Select(x => x.ToModel(adamUnit)).Take(5).Reverse().ToList();
            //var count = 0;
            //foreach (var item in list)
            //{
            //    if (item.IsRead == false)
            //    {
            //        count += 1;
            //    }
            //}
            return Json(list);

        }
        [HttpPost]
        public JsonResult GetAllCount()
        {
            var UserName = User.Identity.Name;
            var userId = adamUnit.AppUserRepository.SingleOrDefault(x => x.UserName == UserName).Id;
            var list = adamUnit.NotificationRepository.GetAll(x => x.ToUserId == userId).ToList();
            var count = 0;
            foreach (var item in list)
            {
                if (item.IsRead == false)
                {
                    count += 1;
                }
            }
            return Json(count);
        }
        //Get GetNotification
        [HttpGet]
        public JsonResult GetNotification()
        {
            var UserName = User.Identity.Name;
            var userId = adamUnit.AppUserRepository.SingleOrDefault(x => x.UserName == UserName).Id;
            var list = adamUnit.NotificationRepository.GetAll(x => x.ToUserId == userId).ToList();
            var count = 0;
            foreach (var item in list)
            {
                if (item.IsRead == false)
                {
                    count += 1;
                }
            }
            return Json(count);

        }

        // Reset Notification
        [HttpGet]
        public async Task<ActionResult> ResetNotification()
        {
            var UserName = User.Identity.Name;
            var UserId = adamUnit.AppUserRepository.SingleOrDefault(x => x.UserName == UserName).Id;
            var readnoty = adamUnit.NotificationRepository.GetAll(x => x.ToUserId == UserId).ToList();
            foreach (var item in readnoty)
            {
                item.IsRead = true;
            }
            await adamUnit.NotificationRepository.TrushAsync();
            return Json("ok");
        }
        // show All Notification
        [HttpGet]
        public IActionResult NotifyShowMore()
        {
            var UserName = User.Identity.Name;
            var userId = adamUnit.AppUserRepository.SingleOrDefault(x => x.UserName == UserName).Id;
            var list = adamUnit.NotificationRepository.GetAll(x => x.ToUserId == userId).Select(x => x.ToModel(adamUnit)).ToList();
            return View(list);
        }
    }
}