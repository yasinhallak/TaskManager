using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using TaskManager.Controllers;
using TaskManager.Models;

namespace TaskManager.hubs
{
    public class chatHub:BaseController
    {

        [HttpPost]
        [Route("/chatHub/saveToken")]
        public async Task<JsonResult> saveToken(string currentToken)
        {
            var UserName = User.Identity.Name;
            var UserId = adamUnit.AppUserRepository.SingleOrDefault(x => x.Email == UserName).Id;
            if (!adamUnit.TokenFireBaseRepository.Exists(x => x.token==currentToken && x.UserId==UserId))
            {
                var token = new TokenFireBase();
                token.UserId = UserId;
                token.token = currentToken;
                await adamUnit.TokenFireBaseRepository.InserAsync(token);
            }
            return Json("ok");
        }

        //public static void SendPushNotification(string fcmToken, string body, string title = "")
        //{

        //    var data = new
        //    {
        //        to = fcmToken,
        //        data = new
        //        {
        //            message = body,
        //            name = "Yasin",
        //            tag = "1",
        //        }
        //    };
        //    var json = JsonConvert.SerializeObject(data);
        //    Byte[] byteArray = Encoding.UTF8.GetBytes(json);
        //    try
        //    {
        //        string SERVER_API_KEY = "AIzaSyAwASfjWUoAJ0gE4k9HhLmBnKZUVWA__XY";
        //        var SENDER_ID = "550075842685";
        //        // Create Request
        //        WebRequest tRequest;
        //        tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");     // FCM link
        //        tRequest.Method = "Post";
        //        tRequest.ContentType = "application / json";
        //        tRequest.Headers.Add(string.Format("Authorization: key ={ 0}", SERVER_API_KEY));     //Server Api Key Header
        //        tRequest.Headers.Add(string.Format("Sender: id ={ 0}", SENDER_ID));
        //        tRequest.ContentLength = byteArray.Length;
        //        Stream dataStream = tRequest.GetRequestStream();
        //        dataStream.Write(byteArray, 0, byteArray.Length);
        //        WebResponse tResponse = tRequest.GetResponse();
        //        dataStream = tResponse.GetResponseStream();
        //        StreamReader tReader = new StreamReader(dataStream);
        //        String sResponseFromServer = tReader.ReadToEnd();
        //        tReader.Close();
        //        dataStream.Close();
        //        tResponse.Close();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
      
    }
}
