namespace GraduateWork.Server.Common.Models
{
    public class JsonOutputWrapper<T>
    {
        public T Result { get; set; }

        public Error Error { get; set; }
    }
}
