using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Model
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreationDate = DateTime.Now;
            LastModifiedDate = DateTime.Now;
            IsDeleted = false;
        }


       
        [Column(TypeName = "datetime2")]
        public DateTime CreationDate { get; set; }


        public string CreatedBy { get; set; }

    
        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }


        public string ModifiedBy { get; set; }


        [Column(TypeName = "datetime2")]
        public DateTime DeletionDate { get; set; }



        public string DeletedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}
