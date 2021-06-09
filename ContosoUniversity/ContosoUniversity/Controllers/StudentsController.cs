using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContosoUniversity.ViewModels;
using Microsoft.Extensions.Logging;
using ApplicationCore.StudentNs.Domain.Services;
using AutoMapper;
using Entites = ApplicationCore.StudentNs.Domain.Entities;

namespace ContosoUniversity.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ICreateStudentService _createStudentService;
        private readonly IDeleteStudentService _deleteStudentService;
        private readonly IGetStudentsService _getStudentsService;
        private readonly IUpdateStudentService _updateStudentService;

        private readonly ILogger<StudentsController> _logger;
        private readonly IMapper _mapper;

        public StudentsController(ICreateStudentService createStudentService, 
            ILogger<StudentsController> logger,
            IMapper mapper)
        {
            _createStudentService = createStudentService;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Loading students for index view.");
            return View(await _getStudentsService.GetAllAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = (await _getStudentsService.GetAllAsync())
                .FirstOrDefault(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LastName,FirstMidName,EnrollmentDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                // TODO It might be more appropriate to map the ViewModel to a Model instead of an Entity, since an Entity might not have all of its properties as public.
                var studentEntity = _mapper.Map<Entites.Student>(student);
                _createStudentService.Create(studentEntity);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentEntity = await _getStudentsService.GetByIdAsync(id.Value);
            if (studentEntity == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<Student>(studentEntity));
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LastName,FirstMidName,EnrollmentDate")] Student student)
        {
            if (id != student.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var studentEntity = _mapper.Map<Entites.Student>(student);
                    await _updateStudentService.Update(studentEntity);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.ID == id);
        }
    }
}
