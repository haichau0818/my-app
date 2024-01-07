using Chat.API.Data;
using Chat.API.Data.Entities;
using Chat.API.DesignPattern.Abstract;
using Microsoft.EntityFrameworkCore;
using System;

namespace Chat.API.DesignPattern.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChatContext _chatContext;
        private GenericRepository<User> _userRepository;
        public UnitOfWork(
            ChatContext chatContext,
            GenericRepository<User> userRepository

            )
        {
            _chatContext = chatContext;
            _userRepository = userRepository;
        }

        public GenericRepository<User> UserRepository
        {
            get
            {
                if (this._userRepository == null)
                {
                    this._userRepository = new GenericRepository<User>(_chatContext);
                }
                return _userRepository;
            }
        }

        public async Task CompleteAsync()
        {
            await _chatContext.SaveChangesAsync();
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _chatContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
