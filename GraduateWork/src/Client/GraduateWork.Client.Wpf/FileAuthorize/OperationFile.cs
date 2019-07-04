using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GraduateWork.Client.Wpf.FileAuthorize
{
    public class OperationFile
    {
        private static OperationFile _instance;
        private const string rootFile = "token_file.txt";
        private byte[] array;

        public static OperationFile GetInstance()
        {
            if (_instance == null)
                _instance = new OperationFile();
            return _instance;
        }
        
        public async Task<string> ReadTokenFromFile()
        {
            try
            {
                var combineFileRoot = Path.Combine(Directory.GetCurrentDirectory(), rootFile);

                using (var fileStream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), rootFile), FileMode.Create))
                {                    
                    await fileStream.ReadAsync(array, 0, array.Length);
                    return Encoding.Default.GetString(array);                    
                }
            }
            catch (Exception ex) { return null; }
        }

        public async Task WriteTokenToFileAsync(string token)
        {
            try
            {
                var combineFileRoot = Path.Combine(Directory.GetCurrentDirectory(), rootFile);
                using (var fileStream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), rootFile), FileMode.Create))
                {
                    array = Encoding.Default.GetBytes(token);
                    await fileStream.WriteAsync(array, 0, array.Length);
                }
            }
            catch(Exception)
            {
                return;
            }
        }
    }
}
