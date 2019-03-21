using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TaskManager.Enums;
using TaskManager.Models;
using TaskManager.Utility;

namespace TaskManager.Extensions
{
    public static class AppHelper
    {
          //static Name
        public const string Users= "Customer";
        public const string SuperAdminEndUser = "Admin";
        public const string UploadEmp = "UploadEm";
        public const string UploadCut = "UploadCus";
        public const string UploadPro = "UploadProj";
        public const string UploadTask = "UploadTask";
        public const string UploadDefault = "UploadDefault";

        public static string GetRelativePath(this Attachment target)
        {

            if (target.AttachmentOwner == AttachmentOwner.Customers)
            {
                return $"{AppHelper.UploadCut}/{target.FileName}";
            }
            else if (target.AttachmentOwner == AttachmentOwner.Employees)
                return $"{AppHelper.UploadEmp}/{target.FileName}";
            else if (target.AttachmentOwner == AttachmentOwner.Projects)
                return $"{AppHelper.UploadPro}/{target.FileName}";
            else if (target.AttachmentOwner == AttachmentOwner.Tasks)
                return $"{AppHelper.UploadTask}/{target.FileName}";
            else
            {
                return $"{AppHelper.UploadDefault}/{target.FileName}";
            }


        }
        public static Attachment ToAttachment(this IFormFile source, string ownerId, AttachmentOwner ownerType,
            AttachmentType attachmentType, string fileName)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new Attachment()
            {
                FileName = fileName,
                FileExtenstion = Path.GetExtension(fileName),
                OwnerId = ownerId,
                AttachmentOwner = ownerType,
                CreationDate = DateTime.Now
            };
        }

        public class FileUploadHelper
        {
            public static bool ContainsFile(string AttachmentName, AttachmentOwner attachmentOwner, string WebRootPath)
            {
                if (attachmentOwner == AttachmentOwner.Tasks)
                {
                    string folderName = AppHelper.UploadTask;
                    string webRootPath = WebRootPath;
                    string newPath = Path.Combine(webRootPath, folderName);
                    var tempFolderInfolang = $"{newPath}/{AttachmentName}";
                    if (File.Exists(tempFolderInfolang))
                    {
                        File.Delete(tempFolderInfolang);
                        return true;
                    }

                    return false;
                }
                if (attachmentOwner == AttachmentOwner.Employees)
                {
                    string folderName = AppHelper.UploadEmp;
                    string webRootPath = WebRootPath;
                    string newPath = Path.Combine(webRootPath, folderName);
                    var tempFolderInfolang = $"{newPath}/{AttachmentName}";
                    if (File.Exists(tempFolderInfolang))
                    {
                        File.Delete(tempFolderInfolang);
                        return true;
                    }

                    return false;
                }
                if (attachmentOwner == AttachmentOwner.Customers)
                {
                    string folderName = AppHelper.UploadCut;
                    string webRootPath = WebRootPath;
                    string newPath = Path.Combine(webRootPath, folderName);
                    var tempFolderInfolang = $"{newPath}/{AttachmentName}";
                    if (File.Exists(tempFolderInfolang))
                    {
                        File.Delete(tempFolderInfolang);
                        return true;
                    }

                    return false;
                }
                if (attachmentOwner == AttachmentOwner.Projects)
                {
                    string folderName = AppHelper.UploadPro;
                    string webRootPath = WebRootPath;
                    string newPath = Path.Combine(webRootPath, folderName);
                    var tempFolderInfolang = $"{newPath}/{AttachmentName}";
                    if (File.Exists(tempFolderInfolang))
                    {
                        File.Delete(tempFolderInfolang);
                        return true;
                    }

                    return false;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
