using ApplicationCore.StudentNs.Domain.Entities;
using ApplicationCore.Infrastructure.Interfaces.Repositories;

namespace ApplicationCore.StudentNs.Domain.Services.Impl
{
    class DeleteStudentService : IDeleteStudentService
    {
        private readonly IStudentRepository _repo;

        public void Delete(Student student)
        {
            _repo.Delete(student);
        }
    }
}
