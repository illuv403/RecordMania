using System.ComponentModel.DataAnnotations;

namespace RecordMania.DTO;

public class TaskToAddDTO
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [Required]
    [MaxLength(2000)]
    public string Description { get; set; }
}