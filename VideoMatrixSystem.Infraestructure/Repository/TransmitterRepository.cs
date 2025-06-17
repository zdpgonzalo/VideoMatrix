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
                    new Transmitter("T01", "1.2.3.4", "https://azimutmarine.es/media/upload/image/tut-carto-005.jpg", Common.DeviceState.Offline),
                    new Transmitter("T02", "432.23.2", "https://www.boletinpatron.com/es/media/58e285db820/carta-electr%C3%B3nica.jpg", Common.DeviceState.StandBy),
                    new Transmitter("T03", "123.54.6", "https://marinos.es/wp-content/uploads/Carta-105-e1459785606979.jpg", Common.DeviceState.Active),
                    new Transmitter("T04", "532.63.3", "https://nauticaprofesional.com/wp-content/uploads/2023/10/shipfinder-aplicacion-buscar-barcos-1024x538.png", Common.DeviceState.StandBy),
                    new Transmitter("T05", "16.53.12", "https://www.rubicon3adventure.com/wp-content/uploads/2024/04/idenitification-ais.webp", Common.DeviceState.Offline),
                    new Transmitter("T06", "192.168.1.10", "https://media.barcosnews.es/boatsnews-es/26162/ais-trafico-maritimo-1.jpg", Common.DeviceState.Offline),
                    new Transmitter("T07", "10.0.0.23", "https://blog.nautia.net/wp-content/uploads/2017/11/automatic-identification-system.jpg", Common.DeviceState.StandBy),
                    new Transmitter("T08", "172.16.5.6", "https://www.aresashipyard.com/media/barcos/2500-sfv/1.png", Common.DeviceState.Active),
                    new Transmitter("T09", "192.168.0.33", "https://www.galiciaconfidencial.com/imgpipe/2024_3_14_128960s740x.jpg", Common.DeviceState.StandBy),
                    new Transmitter("T10", "10.10.10.12", "https://azimutmarine.es/media/upload/image/CMS/NAUTICA/Electronica/AIS/superposicion-ais-radar.jpg", Common.DeviceState.Offline),
                    new Transmitter("T11", "192.168.1.11", "https://media.barcosnews.es/boatsnews-es/23699/seguridad-maritima-electronica-ais-4.jpg", Common.DeviceState.Offline),
                    new Transmitter("T12", "10.0.0.24", "https://www.aresashipyard.com/media/barcos/2500-s/imagenes/012.jpg", Common.DeviceState.StandBy),
                    new Transmitter("T13", "172.16.5.7", "https://www.gpsnautico.com/img/cms/Dual-Radar-Support.png", Common.DeviceState.Active),
					new Transmitter("T14", "192.168.0.34", "https://www.simrad-yachting.com/globalassets/simrad/uns/sportfishing-small/sportsfishing-sm-v2.png", Common.DeviceState.StandBy),
                    new Transmitter("T15", "10.10.10.13", "https://anave.es/wp-content/uploads/2024/08/fo-bajamar-express.jpg", Common.DeviceState.Offline),
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
