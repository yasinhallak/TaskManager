using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using TaskManager.Enums;
using TaskManager.Models;
using TaskManager.Repository;

namespace TaskManager.Data.ViewModel
{

    public static class NotificationVMExtensions
    {
        public static NotificationVm ToModel(this Notfication entity, AdamUnit adamUnit)
        {
           var NofiVM=new NotificationVm();

           NofiVM.Id = entity.Id;
           NofiVM.Context = entity.Context;
           NofiVM.NameOfSend = adamUnit.AppUserRepository.SingleOrDefault(x => x.Id == entity.OwnerUserId).FirstName;
           var userIdAdmin = adamUnit.RoleRepository.GetUserIdOfRole();
           if (userIdAdmin == entity.OwnerUserId)
           {
               NofiVM.filename = adamUnit.AttachmentRepository.FirstOrDefault(x => x.OwnerId == userIdAdmin.ToString() && x.AttachmentOwner == AttachmentOwner.Employees).FileName;
           }
           else
           {
               var employeeId = adamUnit.EmployeeRepository.SingleOrDefault(x => x.UserId == entity.OwnerUserId).Id;
               NofiVM.filename = adamUnit.AttachmentRepository.SingleOrDefault(x => x.OwnerId == employeeId.ToString() && x.AttachmentOwner == AttachmentOwner.Employees).FileName;
           }
       
           NofiVM.TableName = entity.TableName.GetDisplayName();
           NofiVM.TablePkId = entity.TablePkId;
           NofiVM.IsRead = entity.IsRead;
           NofiVM.Date = entity.CreationDate.ToShortTimeString();
           return NofiVM;
        }


    }
    public class NotificationVm
    {
        public long  Id { get; set; }

        public string  Context { get; set; }

        public string NameOfSend { get; set; }

        public string TableName { get; set; }

        public  string filename { get; set; }

        public long TablePkId { get; set; }

        public string  Date { get; set; }

        public bool IsRead { get; set; }
    }
}
