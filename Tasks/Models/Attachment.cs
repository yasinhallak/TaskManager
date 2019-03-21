using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Enums;
using TaskManager.Extensions;
using TaskManager.Model;

namespace TaskManager.Models
{
    public class Attachment:BaseEntity
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }

        public string  FileExtenstion { get; set; }

        public string  OwnerId { get; set; }

        public AttachmentOwner  AttachmentOwner { get; set; }

        public AttachmentType AttachmentType { get; set; }
  
        [NotMapped]
        public string FilePath => this.GetRelativePath();

    }
}
