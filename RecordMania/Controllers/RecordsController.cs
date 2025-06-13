using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecordMania.DAL;
using RecordMania.DTO;
using RecordMania.Models;
using Task = RecordMania.Models.Task;

namespace RecordMania.Controllers;

[Route("api/records")]
[ApiController]
public class RecordsController : ControllerBase
{
    private readonly RecordManiaDBContext _context;

    public RecordsController(RecordManiaDBContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetRecords([FromQuery] DateTime? createdAt, [FromQuery] int? languageId,
        [FromQuery] int? taskId, CancellationToken token)
    {
        var recordsQuery = _context.Record
            .Include(r => r.Student)
            .Include(r => r.Language)
            .Include(r => r.Task)
            .AsQueryable();

        if (createdAt != null)
        {
            recordsQuery = recordsQuery.Where(r => r.CreatedAt.Date == createdAt.Value.Date);
        }

        if (languageId != null)
        {
            recordsQuery = recordsQuery.Where(r => r.LanguageId == languageId.Value);
        }

        if (taskId != null)
        {
            recordsQuery = recordsQuery.Where(r => r.TaskId == taskId.Value);
        }

        recordsQuery = recordsQuery.OrderByDescending(r => r.CreatedAt);

        recordsQuery = recordsQuery.OrderBy(r => r.Student.LastName);

        var records = await recordsQuery.Select(r => new RecordDTO
        {
            Id = r.Id,
            Language = new LanguageDTO
            {
                Id = r.LanguageId,
                Name = r.Language.Name
            },
            Task = new TaskDTO
            {
                Id = r.TaskId,
                Name = r.Task.Name,
                Description = r.Task.Description
            },
            Student = new StudentDTO
            {
                Id = r.StudentId,
                FirstName = r.Student.FirstName,
                LastName = r.Student.LastName,
                Email = r.Student.Email
            },
            ExecutionTime = r.ExecutionTime,
            Created = r.CreatedAt.ToString("MM/dd/yyyy hh:mm:ss"),
        }).ToListAsync(token);

        return Ok(records);
    }

    [HttpPost]
    public async Task<IActionResult> AddRecord([FromBody] RecordToAddDTO record, CancellationToken token)
    {
        var language = await _context.Language.FirstOrDefaultAsync(l => l.Id == record.LanguageId, token);

        if (language == null)
        {
            return NotFound();
        }

        var student = await _context.Student.FirstOrDefaultAsync(s => s.Id == record.StudentId, token);

        if (student == null)
        {
            return NotFound();
        }

        var task = await _context.Task.FirstOrDefaultAsync(t => t.Id == record.Task.Id, token);

        int taskId;
        if (task == null)
        {
            if (string.IsNullOrEmpty(record.Task.Name) || string.IsNullOrEmpty(record.Task.Description))
            {
                return BadRequest();
            }

            var newTask = new Task
            {
                Name = record.Task.Name,
                Description = record.Task.Description
            };

            _context.Task.Add(newTask);
            await _context.SaveChangesAsync(token);

            taskId = newTask.Id;
        }
        else
        {
            taskId = task.Id;
        }

        var newRecord = new Record
        {
            LanguageId = record.LanguageId,
            StudentId = record.StudentId,
            TaskId = taskId,
            ExecutionTime = record.ExecutionTime,
            CreatedAt = record.CreatedAt
        };

        _context.Record.Add(newRecord);
        await _context.SaveChangesAsync(token);

        return Created();
    }
}