using VideoMatrixSystem.Domain.Entities;
using VideoMatrixSystem.Domain.Repository;
using VideoMatrixSystem.Domain.Context;

namespace VideoMatrixSystem.Infraestructure.Repository
{
    public class RepositoryManager
    {
        private readonly AppDbContext _context;

        public IRepository<Transmitter> TransmittersRepository { get; }
        public IRepository<Receiver> ReceiverRepository { get; }

        public RepositoryManager(AppDbContext context) 
        {
            _context = context;

            TransmittersRepository = new TransmitterRepository(context);
            ReceiverRepository = new ReceiverRepository(context);
        }
    }
}
