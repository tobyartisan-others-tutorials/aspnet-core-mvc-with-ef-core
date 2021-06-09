using ApplicationCore.StudentNs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.StudentNs.Domain.Services
{
    public interface IGetStudentsService
    {
        Task<Student> GetByIdAsync(int id);
        Task<IList<Student>> GetAllAsync();
    }
}
