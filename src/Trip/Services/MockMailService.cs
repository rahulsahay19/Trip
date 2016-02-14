using System.Diagnostics;

namespace Trip.Services
{
    public class MockMailService : IMailService
    {
        public bool SendMail(string To, string From, string Subject, string Body)
        {
            //Below Using C# 6 Feature, called String Interpolation
            Debug.WriteLine($"Sending Mail: To :{To}, Subject: {Subject}");
            return true;
        }
    }
}
