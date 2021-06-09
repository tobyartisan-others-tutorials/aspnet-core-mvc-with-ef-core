using ApplicationCore.StudentNs.Domain.Entities;
using ApplicationCore.Infrastructure.Interfaces.Repositories;

namespace ApplicationCore.StudentNs.Domain.Services.Impl
{
    class UpdateStudentService : IUpdateStudentService
    {
        private readonly IStudentRepository _repo;

        public void Update(Student student)
        {
            _repo.Update(student);
        }
    }
}
