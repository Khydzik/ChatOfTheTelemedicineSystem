using System;

namespace GraduateWork.Server.Data.Models
{
    public class FileEntity
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public long Size { get; set; }
    }
}
