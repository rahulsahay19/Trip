using System;

namespace Trip.Services
{
  public interface IMailService
  {
      bool SendMail(string To, string From, string Subject, string Body);
  }
}
