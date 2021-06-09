using AutoMapper;
using Entites = ApplicationCore.StudentNs.Domain.Entities;

namespace ContosoUniversity.ViewModels
{
    public class ViewModelToEntityMapperProfile : Profile
    {
        public ViewModelToEntityMapperProfile()
        {
            CreateMap<Student, Entites.Student>();
            CreateMap<Course, Entites.Course>();
            CreateMap<Enrollment, Entites.Enrollment>();
        }
    }
}
