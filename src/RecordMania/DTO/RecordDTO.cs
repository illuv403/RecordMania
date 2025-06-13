namespace RecordMania.DTO;

public class RecordDTO
{
    public int Id { get; set; }
    
    public LanguageDTO Language { get; set; }
    
    public TaskDTO Task { get; set; }
    
    public StudentDTO Student { get; set; }
    
    public double ExecutionTime { get; set; }
    
    public string Created { get; set; }
}