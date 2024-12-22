using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class FileUploadEmployeeMapping
    {
        [Key]
        public int FileUploadEmployeeMappingId { get; set; }
        public int EmployeeId { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.UtcNow;
        public string FileType { get; set; } // Type of file (PDF)
    }
}
