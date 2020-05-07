using System;
using LayeredWebDemo.DAL.Interfaces;
using LayeredWebDemo.DAL.Entities;

namespace LayeredWebDemo.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private UserRepository _users;
        private MessageRepository _messages;

        public UnitOfWork(ApplicationDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("Context was not supplied");
            }

            _context = context;
        }

        #region IUnitOfWork Members

        public IRepository<ApplicationUser> Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new UserRepository(_context);
                }

                return _users;
            }
        }

        public IRepository<Message> Messages
        {
            get
            {
                if (_messages == null)
                {
                    _messages = new MessageRepository(_context);
                }

                return _messages;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

       private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}