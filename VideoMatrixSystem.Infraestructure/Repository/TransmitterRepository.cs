using Microsoft.EntityFrameworkCore;
using VideoMatrixSystem.Domain.Context;
using VideoMatrixSystem.Domain.Entities;

namespace VideoMatrixSystem.Domain.Repository
{
    internal class TransmitterRepository : IRepository<Transmitter>
    {
        private readonly AppDbContext _context;

        public TransmitterRepository(AppDbContext context) 
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> AddAsync(Transmitter entity)
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

        public async Task<bool> AddRangeAsync(List<Transmitter> transmitters)
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

        public async Task<bool> DeleteAsync(Transmitter entity)
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

        public async Task<bool> DeleteRangeAsync(List<Transmitter> entities)
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

        public List<Transmitter> GetAll()
        {
            InitializeDatabase();

            return _context.Transmitters.Include(t => t.Receivers).ToList();
        }

        private async void InitializeDatabase()
        {
            try
            {
                if (_context.Transmitters.Count() > 0)
                {
                    return;
                }

                List<Transmitter> transmitters = new List<Transmitter>()
            {
                new Transmitter("T01", "1.2.3.4", "www.image.es", Common.DeviceState.Offline),
                new Transmitter("T02", "432.23.2", "www.aaa.es", Common.DeviceState.StandBy),
                new Transmitter("T03", "123.54.6", "www.bbb.es", Common.DeviceState.Active),
                new Transmitter("T04", "532.63.3", "www.ccc.es", Common.DeviceState.StandBy),
                new Transmitter("T05", "16.53.12", "www.ddd.es", Common.DeviceState.Offline),
            };

                await AddRangeAsync(transmitters);

                return;
            }
            catch (Exception ex) 
            {
                return;
            }
        }

        public Transmitter? GetById(int id)
        {
            return _context.Transmitters.Find(id);
        }

        public bool IsValid(Transmitter entity)
        {
            return !string.IsNullOrEmpty(entity?.Name);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Transmitter entity)
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

        public async Task<bool> UpdateRangeAsync(List<Transmitter> entities)
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
