using Newtonsoft.Json;

namespace GraduateWork.Server.Common.Models
{
    public class ResponseError<T>
    {
        public T Result { get; set; }
        public Error Error { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
