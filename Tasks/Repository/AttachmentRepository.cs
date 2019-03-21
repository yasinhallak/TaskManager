using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TaskManager.Data;
using TaskManager.Enums;
using TaskManager.Extensions;
using TaskManager.Models;

namespace TaskManager.Repository
{
    public class AttachmentRepository:BaseRepository<Attachment>
    {
        private readonly ApplicationDbContext _context;
        public AttachmentRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        public List<Attachment> GetPhoto(string ownerId, AttachmentOwner ownerType)
        {

            var file = _context.Attachments.Where(x => x.OwnerId == ownerId &&
                                            x.AttachmentOwner == ownerType).ToList();
            return file;
        }

        public String GetFileName(IFormFile file, AttachmentOwner ownerType)
        {
            if (ownerType == AttachmentOwner.Employees)
            {
                return file.FileName;
            }
            else
            {
                return file.FileName;
            }
        }

        public String GetFullPath(AttachmentOwner ownerType)
        {
            if (ownerType == AttachmentOwner.Employees)
            {
                return "UploadEm";
            }

            if (ownerType == AttachmentOwner.Customers)
            {
                return "UploadCus";
            }

            if (ownerType == AttachmentOwner.Projects)
            {
                return "UploadProj";
            }

            if (ownerType == AttachmentOwner.Tasks)
            {
                return "UploadTask";
            }
            else
            {
                //var year = AppHelper.ThisYear;
                //var Month = AppHelper.ThisMonth;
                //return AppHelper.UploadIconLanguages;

                return "UploadDefault";

            }
        }

        public bool OnPostUpload(List<IFormFile> files, string recordId, AttachmentOwner ownerType,
            AttachmentType attachmentType, string WebRootPath)
        {
            if (files != null && files.Count > 0)
            {
                string folderName = GetFullPath(ownerType);
                string webRootPath = WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                 
                foreach (IFormFile item in files)
                {
                    if (item.Length > 0)
                    {
                        string fileName = GetFileName(item, ownerType);
                        string fullPath = Path.Combine(newPath, fileName);
                        using (var stream=new FileStream(fullPath,FileMode.Create))
                        {
                            item.CopyTo(stream);
                        }

                        var attachment = item.ToAttachment(recordId, ownerType, attachmentType, fileName);
                        if (attachment != null)
                        {
                            AddToDb(attachment);
                        }
                    }
                }

                return true;
            }

            return false;
        }
        
    }
}
