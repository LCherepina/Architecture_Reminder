using System;
using System.Collections.Generic;
using System.ServiceModel;
using Architecture_Reminder.DBModels;

namespace Architecture_Reminder.ServiceInterface
{
    [ServiceContract]
    public interface IReminderContract
    {
        [OperationContract]
        bool UserExists(string login);
        [OperationContract]
        User GetUserByLogin(string login);
        [OperationContract]
        User GetUserByGuid(Guid guid);
        [OperationContract]
        List<User> GetAllUsers(Guid walletGuid);
        [OperationContract]
        void AddUser(User user);
        [OperationContract]
        void AddReminder(Reminder reminder);
        [OperationContract]
        void SaveReminder(Reminder reminder);
        [OperationContract]
        void DeleteReminder(Reminder selectedReminder);
    }

}
