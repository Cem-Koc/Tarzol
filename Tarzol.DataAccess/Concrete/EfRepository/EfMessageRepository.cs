using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.DataAccess.Abstract;
using Tarzol.DataAccess.Context;
using Tarzol.DataAccess.Repositories;
using Tarzol.Entity;

namespace Tarzol.DataAccess.Concrete.EfRepository
{
   public class EfMessageRepository : BaseRepository<Message>, IMessageRepository
    {
        private readonly TarzolDbContext _tarzolDbContext;

        public EfMessageRepository(TarzolDbContext tarzolDbContext) : base(tarzolDbContext)
        {
            _tarzolDbContext = tarzolDbContext;
        }
    }
}
