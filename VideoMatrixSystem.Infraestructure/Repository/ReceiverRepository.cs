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
            InitializeDatabase();

            return _context.Receivers.ToList();
        }

        private async void InitializeDatabase()
        {
            try
            {
                if (_context.Receivers.Count() > 0)
                {
                    return;
                }

                List<Receiver> receivers = new List<Receiver>()
                {
                    new Receiver("R01", "1.2.3.4", "https://www.shutterstock.com/image-vector/default-ui-image-placeholder-wireframes-600nw-1037719192.jpg", Common.DeviceState.Offline),
                    new Receiver("R02", "432.23.2", "https://www.shutterstock.com/image-vector/default-ui-image-placeholder-wireframes-600nw-1037719192.jpg", Common.DeviceState.StandBy),
                    new Receiver("R03", "123.54.6", "https://www.shutterstock.com/image-vector/default-ui-image-placeholder-wireframes-600nw-1037719192.jpg", Common.DeviceState.Active),
                    new Receiver("R04", "532.63.3", "https://www.shutterstock.com/image-vector/default-ui-image-placeholder-wireframes-600nw-1037719192.jpg", Common.DeviceState.StandBy),
                    new Receiver("R05", "16.53.12", "https://www.shutterstock.com/image-vector/default-ui-image-placeholder-wireframes-600nw-1037719192.jpg", Common.DeviceState.Offline),
                    new Receiver("R06", "192.168.1.10", "https://www.shutterstock.com/image-vector/default-ui-image-placeholder-wireframes-600nw-1037719192.jpg", Common.DeviceState.Offline),
                    new Receiver("R07", "10.0.0.23", "https://www.shutterstock.com/image-vector/default-ui-image-placeholder-wireframes-600nw-1037719192.jpg", Common.DeviceState.StandBy),
                    new Receiver("R08", "172.16.5.6", "https://www.shutterstock.com/image-vector/default-ui-image-placeholder-wireframes-600nw-1037719192.jpg", Common.DeviceState.Active),
                    new Receiver("R09", "192.168.0.33", "https://www.shutterstock.com/image-vector/default-ui-image-placeholder-wireframes-600nw-1037719192.jpg", Common.DeviceState.StandBy),
                    new Receiver("R10", "10.10.10.12", "https://www.shutterstock.com/image-vector/default-ui-image-placeholder-wireframes-600nw-1037719192.jpg", Common.DeviceState.Offline),
                    new Receiver("R11", "192.168.1.11", "https://www.shutterstock.com/image-vector/default-ui-image-placeholder-wireframes-600nw-1037719192.jpg", Common.DeviceState.Offline),
                    new Receiver("R12", "10.0.0.24", "https://www.shutterstock.com/image-vector/default-ui-image-placeholder-wireframes-600nw-1037719192.jpg", Common.DeviceState.StandBy),
                    new Receiver("R13", "172.16.5.7", "https://www.shutterstock.com/image-vector/default-ui-image-placeholder-wireframes-600nw-1037719192.jpg", Common.DeviceState.Active),
                    new Receiver("R14", "192.168.0.34", "https://www.shutterstock.com/image-vector/default-ui-image-placeholder-wireframes-600nw-1037719192.jpg", Common.DeviceState.StandBy),
                    new Receiver("R15", "10.10.10.13", "https://www.shutterstock.com/image-vector/default-ui-image-placeholder-wireframes-600nw-1037719192.jpg", Common.DeviceState.Offline),
                };

                await AddRangeAsync(receivers);

                return;
            }
            catch (Exception ex)
            {
                return;
            }
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
