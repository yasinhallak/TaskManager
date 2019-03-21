using TaskManager.Data;
using TaskManager.Data.ViewModel;
using TaskManager.Model;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Utility
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Employee, EmployeeVM>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email)).
                ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber)).
                ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName)).
                ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName)).
                ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User.Gender));
            CreateMap<EmployeeVM, Employee>();
         
            CreateMap<Customer, CustomerVM>();
            CreateMap<CustomerVM, Customer>();
            CreateMap<City, CityVM>();
            CreateMap<CityVM, City>();
            CreateMap<Project, ProjectVM>()
                /*.ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FirstName))*/;
            CreateMap<ProjectVM, Project>();
            CreateMap<task, TaskVM>();
            CreateMap<TaskVM, task>();
            //CreateMap<BoardTask, BoardTaskVM>();
            //CreateMap<BoardTaskVM, BoardTask>();

            //CreateMap<DelayReport, DelayReportVM>();
            //CreateMap<DelayReportVM, DelayReport>();
            //CreateMap<Files, FileVM>();
            //CreateMap<FileVM, Files>();
            //CreateMap<User, AppUserVM>();
            //CreateMap<AppUserVM, User>();
            //CreateMap<JobTitle, JobTitleVM>();
            //CreateMap<JobTitleVM, JobTitle>();
            //CreateMap<ProjectType, PojectTypeVM>();
            //CreateMap<PojectTypeVM, ProjectType>();
            //CreateMap<CompanyType, CompanyTypeVM>();
            //CreateMap<CompanyTypeVM, CompanyType>();
            //CreateMap<Company, CompanyVM>();
            //CreateMap<CompanyVM, Company>();
            //CreateMap<Comment, CommentVM>();
            //CreateMap<CommentVM, Comment>();
        }
        public static void Run()
        {
            Mapper.Initialize(a =>
            {
                a.AddProfile<MappingProfile>();
            });
        }
    }
}
