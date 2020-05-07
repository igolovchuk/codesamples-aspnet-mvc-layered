using LayeredWebDemo.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using LayeredWebDemo.BLL.DTO;
using AutoMapper;
using LayeredWebDemo.DAL.Entities;

namespace LayeredWebDemo.BLL.Services
{
    class MessageService
    {
        #region fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        public MessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Comment Description: 
        /// </summary>
        /// <returns>{Type of Request that has used this method}/{Controller}/{Action Name}</returns>   
        //
        // GET: /Controller/Action - not used yet
        public IEnumerable<NotificationDTO> GetMessages() => Mapper.Map<IEnumerable<Message>, List<NotificationDTO>>(_unitOfWork.Messages.GetAll());



        // Dispose Database
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
