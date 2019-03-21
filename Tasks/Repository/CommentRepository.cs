using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Repository
{
    public class CommentRepository:BaseRepository<Comment>
    {
        public CommentRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
