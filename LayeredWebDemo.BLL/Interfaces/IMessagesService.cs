using LayeredWebDemo.BLL.DTO;
using System.Collections.Generic;

namespace LayeredWebDemo.BLL.Interfaces
{
    public interface IMessagesService
    {
        IEnumerable<NotificationDTO> GetMessages();
        void Dispose();
    }
}
