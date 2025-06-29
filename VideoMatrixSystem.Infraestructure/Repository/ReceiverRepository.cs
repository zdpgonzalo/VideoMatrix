﻿using Microsoft.EntityFrameworkCore;
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

        public  bool Add(Receiver entity)
        {
            if (entity == null)
                return false;

            if (!IsValid(entity))
                return false;

            try
            {
                 _context.Add(entity);
                 _context.SaveChanges();
            }
            catch (Exception ex) 
            {
                return false;
            }

            return true;
        }

        public  bool AddRange(List<Receiver> transmitters)
        {
            if (transmitters == null)
                return false;

            transmitters = transmitters.Where(IsValid).ToList();

            try
            {
                 _context.AddRange(transmitters);
                 _context.SaveChanges();
            }
            catch (Exception ex) 
            {
                return false;
            }

            return true;
        }

        public  bool Delete(Receiver entity)
        {
            if (entity == null)
                return false;

            try
            {
                _context.Remove(entity);
                 _context.SaveChanges();
            }
            catch (Exception ex) 
            {
                return false;
            }

            return true;
        }

        public  bool DeleteRange(List<Receiver> entities)
        {
            if (entities == null)
                return false;

            try
            {
                _context.RemoveRange(entities);
                 _context.SaveChanges();
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

			return  _context.Receivers.Include(r => r.Transmitter).ToList();
		}

        private void InitializeDatabase()
        {
			try
			{
				if (_context.Receivers.Count() > 0)
				{
					return;
				}

				List<Receiver> receivers = new List<Receiver>()
				{
					new Receiver("R01", "1.2.3.4", Common.DeviceState.Offline),
					new Receiver("R02", "432.23.2", Common.DeviceState.StandBy),
					new Receiver("R03", "123.54.6", Common.DeviceState.StandBy),
					new Receiver("R04", "532.63.3", Common.DeviceState.StandBy),
					new Receiver("R05", "16.53.12", Common.DeviceState.Offline),
					new Receiver("R06", "192.168.1.10", Common.DeviceState.Offline),
					new Receiver("R07", "10.0.0.23", Common.DeviceState.StandBy),
					new Receiver("R08", "172.16.5.6", Common.DeviceState.StandBy),
					new Receiver("R09", "192.168.0.33", Common.DeviceState.StandBy),
					new Receiver("R10", "10.10.10.12", Common.DeviceState.Offline),
					new Receiver("R11", "192.168.1.11", Common.DeviceState.Offline),
					new Receiver("R12", "10.0.0.24", Common.DeviceState.StandBy),
					new Receiver("R13", "172.16.5.7", Common.DeviceState.StandBy),
					new Receiver("R14", "192.168.0.34", Common.DeviceState.StandBy),
					new Receiver("R15", "10.10.10.13", Common.DeviceState.Offline),

				};

				 AddRange(receivers);

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

        public void SaveChanges()
        {
             _context.SaveChanges();
        }

        public  bool Update(Receiver entity)
        {
            try
            {
                _context.Update(entity);
                 _context.SaveChanges();
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }

        public  bool UpdateRange(List<Receiver> entities)
        {
            try
            {
                _context.UpdateRange(entities);
                 _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
