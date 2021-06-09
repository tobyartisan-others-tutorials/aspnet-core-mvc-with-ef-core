using ApplicationCore.StudentNs.Domain.Entities;
using ApplicationCore.Infrastructure.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.StudentNs.Domain.Services.Impl
{
    class GetStudentsService : IGetStudentsService
    {
        private readonly IStudentRepository _repo;

        public async Task<Student> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<IList<Student>> GetAllAsync()
        {
            return await _repo.ListAsync();
        }
    }
}
