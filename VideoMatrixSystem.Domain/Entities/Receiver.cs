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

        public string ImageUrl { get; private set; }

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

        public Receiver(string name, string ip, string imageUrl, DeviceState state)
        {
            UpdateName(name);
            UpdateIP(ip);
            UpdateImage(imageUrl);
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
        /// Updates the transmitter of the receiver
        /// </summary>
        /// <param name="transmitter">New transmitter for the receiver</param>
        /// <returns>True</returns>
        public bool UpdateTransmitter(Transmitter? transmitter)
        {
            Transmitter = transmitter;
            SetChanges(OpResul.Page);
            return true;
        }

        /// <summary>
        /// Updates the image of the receiver
        /// </summary>
        /// <param name="imageUrl">New image URL for the receiver</param>
        /// <returns>True</returns>
        public bool UpdateImage(string? imageUrl)
        {
            if (ImageUrl == imageUrl)
            {
                SetChanges(OpResul.Cancel);
                return false;
            }

            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                //Change to empty route
            }

            ImageUrl = imageUrl;
            SetChanges(OpResul.Line);
            return true;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}";
        }
    }
}
