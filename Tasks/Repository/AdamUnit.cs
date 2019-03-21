using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data;

namespace TaskManager.Repository
{
    public class AdamUnit
    {
        private bool _disposed;
        private readonly ApplicationDbContext _context=new ApplicationDbContext();

        #region Repository
        //City
        private CityRepository cityRepository;
        public CityRepository CityRepository => cityRepository ?? (cityRepository = new CityRepository(_context));

        //Employee
        private EmployeeRepository employeeRepository;
        public EmployeeRepository EmployeeRepository => employeeRepository ?? (employeeRepository = new EmployeeRepository(_context));

        //AppUser
        private AppUserRepository appUserRepository;
        public AppUserRepository AppUserRepository => appUserRepository ?? (appUserRepository = new AppUserRepository(_context));


        //IdentityUser
        private IdentityUserRepository identityUserRepository;
        public IdentityUserRepository IdentityUserRepository => identityUserRepository ?? (identityUserRepository = new IdentityUserRepository(_context));
        //Customer
        private CustomerRepository customerRepository;
        public CustomerRepository CustomerRepository => customerRepository ?? (customerRepository = new CustomerRepository(_context));

        //Project
        private ProjectRepository projectRepository;
        public ProjectRepository ProjectRepository => projectRepository ?? (projectRepository = new ProjectRepository(_context));

        //ProjectUser
        private ProjectUserRepository projectUserRepository;
        public ProjectUserRepository ProjectUserRepository => projectUserRepository ?? (projectUserRepository = new ProjectUserRepository(_context));

        //task
        private TaskRepository taskRepository;
        public TaskRepository TaskRepository => taskRepository ?? (taskRepository = new TaskRepository(_context));

        //Attachment
        private AttachmentRepository attachmentRepository;
        public AttachmentRepository AttachmentRepository => attachmentRepository ?? (attachmentRepository = new AttachmentRepository(_context));
        //Role
        private RoleRepository roleRepository;
        public RoleRepository RoleRepository => roleRepository ?? (roleRepository = new RoleRepository(_context));

        //Notification
        private NotificationRepository notificationRepository;
        public NotificationRepository NotificationRepository => notificationRepository ?? (notificationRepository = new NotificationRepository(_context));
        //TokenFireBase
        private TokenFireBaseRepository tokenFireBaseRepository;
        public TokenFireBaseRepository TokenFireBaseRepository => tokenFireBaseRepository ?? (tokenFireBaseRepository = new TokenFireBaseRepository(_context));

        //Comment
        private CommentRepository commentRepository;
        public CommentRepository CommentRepository => commentRepository ?? (commentRepository = new CommentRepository(_context));
        #endregion

    }
}
