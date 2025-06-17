using VideoMatrixSystem.Domain.Context;
using VideoMatrixSystem.Domain.Entities;

namespace VideoMatrixSystem.Domain.Repository
{
    internal class ReceiverRepository : IRepository<Receiver>
    {
        private readonly AppDbContext _context;

        public ReceiverRepository(AppDbContext context) 
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> AddAsync(Receiver entity)
        {
            if (entity == null)
                return false;

            if (!IsValid(entity))
                return false;

            try
            {
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                return false;
            }

            return true;
        }

        public async Task<bool> AddRangeAsync(List<Receiver> transmitters)
        {
            if (transmitters == null)
                return false;

            transmitters = transmitters.Where(IsValid).ToList();

            try
            {
                await _context.AddRangeAsync(transmitters);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteAsync(Receiver entity)
        {
            if (entity == null)
                return false;

            try
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteRangeAsync(List<Receiver> entities)
        {
            if (entities == null)
                return false;

            try
            {
                _context.RemoveRange(entities);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public List<Receiver> GetAll()
        {
            return _context.Receivers.ToList();
        }

        public Receiver? GetById(int id)
        {
            return _context.Receivers.Find(id);
        }

        public bool IsValid(Receiver entity)
        {
            return !string.IsNullOrEmpty(entity?.Name);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Receiver entity)
        {
            try
            {
                _context.Update(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }

        public async Task<bool> UpdateRangeAsync(List<Receiver> entities)
        {
            try
            {
                _context.UpdateRange(entities);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
