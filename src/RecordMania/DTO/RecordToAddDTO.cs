using System.ComponentModel.DataAnnotations;

namespace RecordMania.DTO;

public class RecordToAddDTO
{
    [Required]
    public int LanguageId { get; set; }
    
    [Required]
    public int StudentId { get; set; }
    
    public TaskToAddDTO? Task { get; set; }
    
    [Required]
    public int ExecutionTime { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; }
}