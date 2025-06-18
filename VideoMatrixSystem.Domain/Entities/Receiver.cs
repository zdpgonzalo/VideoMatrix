using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using VideoMatrixSystem.Domain.Common;

namespace VideoMatrixSystem.Domain.Entities
{
    /// <summary>
    /// Represents a receiver device within the video matrix system
    /// </summary>
    public class Receiver : MyEntity
    {
        [Required]
        public string Name { get; private set; } = string.Empty;

        [Required]
        public string IP { get; private set; } = string.Empty;

        public int? TransmitterId { get; private set; }

        [ForeignKey(nameof(TransmitterId))]
        public Transmitter? Transmitter { get; private set; }

        public DeviceState State { get; private set; } = DeviceState.Offline;

        public Receiver()
        {
            Name = string.Empty;
            IP = string.Empty;
            State = DeviceState.Offline;
            Transmitter = null;
        }

        public Receiver(string name, string ip, DeviceState state)
        {
            UpdateName(name);
            UpdateIP(ip);
            UpdateState(state);
        }

        /// <summary>
        /// Updates the receiver's name
        /// </summary>
        /// <param name="name">New name value</param>
        /// <returns>True if the name was updated successfully, otherwise false</returns>
        /// <exception cref="ArgumentNullException">Thrown when name is null, empty or whitespace</exception>
        [MemberNotNull(nameof(Name))]
        public bool UpdateName(string name)
        {
            if(string.IsNullOrWhiteSpace(name)) 
                throw new ArgumentNullException("Name can't be null or empty");

            if(name == Name)
            {
                SetChanges(OpResul.Cancel);
                return false;
            }

            Name = name;
            SetChanges(OpResul.Line);
            return true;
        }

        /// <summary>
        /// Updates the ip of the receiver.
        /// </summary>
        /// <param name="ip">New IP value</param>
        /// <returns>True if the IP was updated successfully, otherwise false</returns>
        /// <exception cref="ArgumentNullException">Thrown when IP is null, empty or whitespace</exception>
        [MemberNotNull(nameof(IP))]
        public bool UpdateIP(string ip)
        {
            if(string.IsNullOrWhiteSpace(ip)) 
                throw new ArgumentNullException("IP can't be null or empty");

            if(ip == IP)
            {
                SetChanges(OpResul.Cancel);
                return false;
            }

            IP = ip;
            SetChanges(OpResul.Line);
            return true;
        }

        /// <summary>
        /// Updates the device state of the receiver
        /// </summary>
        /// <param name="state">New state value</param>
        /// <returns>True if the state was updated successfully, otherwise false</returns>
        public bool UpdateState(DeviceState state)
        {
            if(state == State)
            {
                SetChanges(OpResul.Cancel);
                return false;
            }

            State = state;
            SetChanges(OpResul.Line);
            return true;
        }

		/// <summary>
		/// Turns the receiver on or off depending on its current state and transmitter presence.
		/// </summary>
		/// <returns>True if the state was updated successfully; otherwise, false.</returns>
		public bool SwitchState()
        {
            if(State != DeviceState.Offline)
            {
                return UpdateState(DeviceState.Offline);
            }

            if(Transmitter != null)
            {
                return UpdateState(DeviceState.Active);
            }
            else
            {
				return UpdateState(DeviceState.StandBy);
			}
        }

        /// <summary>
        /// Updates the transmitter of the receiver
        /// </summary>
        /// <param name="transmitter">New transmitter for the receiver</param>
        /// <returns>True</returns>
        public bool UpdateTransmitter(Transmitter? transmitter)
        {
            if(Transmitter != null)
            {
                Transmitter.RemoveReceiver(this);
            }
            
            if(transmitter == null && State == DeviceState.Active)
            {
                UpdateState(DeviceState.StandBy);
            }
            else if(transmitter != null && State == DeviceState.StandBy) 
            {
				UpdateState(DeviceState.Active);
			}

            Transmitter = transmitter;
            Transmitter?.AddReceiver(this);

            SetChanges(OpResul.Page);
            return true;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}";
        }
    }
}
