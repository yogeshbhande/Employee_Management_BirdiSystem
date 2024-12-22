namespace EmployeeManagement.Services
{
    public interface IFileUploadService
    {
        Task<string> SaveFileAsync(IFormFile file, int employeeId);
    }
    public class FileUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly string _uploadPath;


        public FileUploadService(IWebHostEnvironment enviromnment)
        {
            _environment = enviromnment;
            _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

        }

        public async Task<string> SaveFileAsync(IFormFile file, int employeeId)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("No file selected.");

            // Check file type (ensure it's a PDF)
            if (file.ContentType != "application/pdf")
                throw new ArgumentException("Only PDF files are allowed.");

            // Create the employee folder if it doesn't exist
            string folderPath = Path.Combine(_uploadPath, employeeId.ToString());
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Generate unique file name (timestamp + original filename)
            string uniqueFileName = $"{DateTime.Now:yyyyMMddHHmmss}_{file.FileName}";
            string filePath = Path.Combine(folderPath, uniqueFileName);

            // Save the file to the folder
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return $"/uploads/{employeeId}/{uniqueFileName}"; // Return the relative file path
        }
    }
}
