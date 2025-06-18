using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using VideoMatrixSystem.Domain.Common;

namespace VideoMatrixSystem.Domain.Entities
{
    /// <summary>
    /// Represents a transmitting device within the video matrix system
    /// </summary>
    public class Transmitter : MyEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string IP { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public DeviceState State { get; set; } = DeviceState.Offline;

        public List<Receiver> Receivers { get; set; } = new List<Receiver>();

        public Transmitter()
        {
            Name        = string.Empty;
            IP          = string.Empty;
            ImageUrl    = string.Empty;
            State       = DeviceState.Offline;
        }

        public Transmitter(string name, string ip, string imageUrl, DeviceState state)
        {
            UpdateName(name);
            UpdateIP(ip);
            UpdateImage(imageUrl);
            UpdateState(state);
        }

        /// <summary>
        /// Updates the transmitter's name
        /// </summary>
        /// <param name="name">New name value</param>
        /// <returns>True if the name was updated successfully, otherwise false</returns>
        /// <exception cref="ArgumentNullException">Thrown when name is null, empty or whitespace</exception>
        [MemberNotNull(nameof(Name))]
        public bool UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Name can't be null or empty");

            if (name == Name)
            {
                SetChanges(OpResul.Cancel);
                return false;
            }

            Name = name;
            SetChanges(OpResul.Line);
            return true;
        }

        /// <summary>
        /// Updates the ip of the transmitter.
        /// </summary>
        /// <param name="ip">New IP value</param>
        /// <returns>True if the IP was updated successfully, otherwise false</returns>
        /// <exception cref="ArgumentNullException">Thrown when IP is null, empty or whitespace</exception>
        [MemberNotNull(nameof(IP))]
        public bool UpdateIP(string ip)
        {
            if (string.IsNullOrWhiteSpace(ip))
                throw new ArgumentNullException("IP can't be null or empty");

            if (ip == IP)
            {
                SetChanges(OpResul.Cancel);
                return false;
            }

            IP = ip;
            SetChanges(OpResul.Line);
            return true;
        }

        /// <summary>
        /// Updates the device state of the transmitter
        /// </summary>
        /// <param name="state">New state value</param>
        /// <returns>True if the state was updated successfully, otherwise false</returns>
        public bool UpdateState(DeviceState state)
        {
            if (state == State)
            {
                SetChanges(OpResul.Cancel);
                return false;
            }

            State = state;
            UpdateTransmitter();
            SetChanges(OpResul.Line);
            return true;
        }

		/// <summary>
		/// Toggles the device state between Active and Offline.
		/// If the current state is not Offline, it switches to Offline; otherwise, it switches to Active.
		/// </summary>
		/// <returns>True if the state was successfully updated; otherwise, false.</returns>
		public bool SwitchState()
        {
            if(State != DeviceState.Offline)
            {
                return UpdateState(DeviceState.Offline);
            }
            else
            {
                return UpdateState(DeviceState.Active);
            }

        }

        /// <summary>
        /// Updates the image of the receiver
        /// </summary>
        /// <param name="imageUrl">New image URL for the receiver</param>
        /// <returns>True</returns>
        public bool UpdateImage(string imageUrl)
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

        /// <summary>
        /// Clears the receivers list of the transmitter
        /// </summary>
        /// <returns>True</returns>
        public bool ClearReceivers()
        {
            Receivers.Clear();
            UpdateTransmitter();
            SetChanges(OpResul.Page);
            return true;

        }

        /// <summary>
        /// Adds the receiver list of the transmitter
        /// </summary>
        /// <returns>True if adds the receiver successfully, otherwise false</returns>
        public bool AddReceiver(Receiver receiver)
        {
            if (Receivers.Contains(receiver))
            {
                return false;
            }

            Receivers.Add(receiver);
            UpdateTransmitter();
            SetChanges(OpResul.Page);
            return true;

        }

        /// <summary>
        /// Removes the receiver from the list of the transmitter
        /// </summary>
        /// <returns>True if removes the receiver successfully, otherwise false</returns>
        public bool RemoveReceiver(Receiver receiver)
        {
            if (Receivers.Contains(receiver))
            {
                return false;
            }

            Receivers.Remove(receiver);
            UpdateTransmitter();
            SetChanges(OpResul.Page);
            return true;

        }

		/// <summary>
		/// Updates the transmitter state based on the number of connected receivers.
		/// Sets the state to StandBy if there are no receivers; otherwise, sets it to Active.
		/// Returns false if the transmitter is offline and cannot be updated.
		/// </summary>
		/// <returns>True if the state was updated; otherwise, false.</returns>
		public bool UpdateTransmitter()
        {
            if(State == DeviceState.Offline)
            {
                return false;
            }

            if(Receivers.Count == 0)
            {
                UpdateState(DeviceState.StandBy);
            }
            else
            {
				UpdateState(DeviceState.Active);
			}
            return true;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}";
        }
    }
}
