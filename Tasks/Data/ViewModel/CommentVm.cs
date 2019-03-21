using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Enums;
using TaskManager.Models;
using TaskManager.Repository;

namespace TaskManager.Data.ViewModel
{
    public static class CommentVMExtensions
    {
        public static CommentVm ToModel(this Comment entity, AdamUnit adamUnit)
        {
            var comment = new CommentVm();
            comment.id = entity.Id;
            comment.context = entity.context;
            comment.firstName = adamUnit.AppUserRepository.SingleOrDefault(x => x.Id == entity.UserId).FirstName;
            comment.lastName = adamUnit.AppUserRepository.SingleOrDefault(x => x.Id == entity.UserId).LastName;
            comment.date = entity.CreationDate.ToShortTimeString();
            comment.tableId = entity.TableId;
            var userIdAdmin = adamUnit.RoleRepository.GetUserIdOfRole();
            if (userIdAdmin == entity.UserId)
            {
                comment.attachment = adamUnit.AttachmentRepository.FirstOrDefault(x => x.OwnerId == userIdAdmin.ToString() && x.AttachmentOwner == AttachmentOwner.Employees).FileName;
            }
            else
            {
                var employeeId = adamUnit.EmployeeRepository.SingleOrDefault(x => x.UserId == entity.UserId).Id;
                comment.attachment = adamUnit.AttachmentRepository.SingleOrDefault(x => x.OwnerId == employeeId.ToString()).FileName;
            }
            return comment;
        }
    }
    public class CommentVm
    {
        public long id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string attachment { get; set; }
        public string context { get; set; }
        public long  tableId { get; set; }
        public string  date { get; set; }
    }
}
