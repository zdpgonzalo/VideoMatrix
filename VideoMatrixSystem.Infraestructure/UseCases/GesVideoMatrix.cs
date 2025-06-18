using VideoMatrixSystem.Domain.Common;
using VideoMatrixSystem.Domain.Context;
using VideoMatrixSystem.Domain.Entities;
using VideoMatrixSystem.Infraestructure.Repository;

namespace VideoMatrixSystem.Infraestructure.UseCases
{
    public class GesVideoMatrix : GesBase<GesVideoMatrix, GesVideoMatrix.Events, GesVideoMatrix.Actions,
                                                GesVideoMatrix.Names, GesVideoMatrix.Tables>
    {
        private RepositoryManager repositoryManager;
        private readonly AppDbContext context;
        private OpResul dataResul;

        private int SelectedTransmitterId { get; set; }
        public Transmitter? SelectedTransmitter
        {
            get => repositoryManager.TransmittersRepository.GetById(SelectedTransmitterId);
        }

        private int SelectedReceiverId { get; set; }
        public Receiver? SelectedReceiver
        {
            get => repositoryManager.ReceiverRepository.GetById(SelectedReceiverId);
        }

        public GesVideoMatrix(RepositoryManager repositoryManager, AppDbContext context) : base()
        {
            this.context = context;
            this.repositoryManager = repositoryManager;
        }

        public enum Tables
        {
            None,
            Transmitter,
            Receiver,
        }

        public enum Names
        {
            None,

            GetTransmitters,
            GetReceivers,
            GetSelectedTransmitter,
            GetSelectedReceiver,
        }


        public enum Actions
        {
            None,
            SwitchState,
            SelectTransmitter,
            SelectReceiver,
            ChangeReceiverTransmitter
        }

        public enum Events
        {
            None,
        }

        /// <summary>
        /// Performs an application action
        /// </summary>
        /// <param name="oper">Operation to perform</param>
        /// <param name="table">Table to operate on</param>
        /// <param name="info">Additional information</param>
        /// <returns>Result of the executed operation</returns>
        protected override async Task<object> Action(Actions oper, Tables table, object[] info)
        {
            try
            {
                ResetChanges();
                switch (table)
                {
                    case Tables.None:
                        switch (oper)
                        {
                            default:
                                return -1;
                        }

                    case Tables.Transmitter:
                        switch (oper)
                        {
                            case Actions.SelectTransmitter:
                                SelectedTransmitterId = Convert.ToInt32(info[0]);
                                return true;

                            case Actions.SwitchState:
                                if (SelectedTransmitter == null)
                                    return false;
                                return SelectedTransmitter.SwitchState();

                            default:
                                return -1;
                        }

                    case Tables.Receiver:
                        SelectedReceiverId = Convert.ToInt32(info[0]);
                        Receiver? receiver = GetReceiver(SelectedReceiverId);
						try
						{
							var usuarios = context.Transmitters.ToList();

						}
						catch (Exception ex)
						{
							Console.WriteLine("❌ Error de conexión: " + ex.Message);
						}
						if (receiver == null)
                        {
                            return false;
                        }

                        switch (oper)
                        {
							case Actions.SelectReceiver:
								SelectedReceiverId = Convert.ToInt32(info[0]);
								return true;

							case Actions.ChangeReceiverTransmitter:
                                Transmitter? transmitter = GetTransmitter(Convert.ToInt32(info[1]));
                                return receiver.UpdateTransmitter(transmitter);

							case Actions.SwitchState:
								if (receiver == null)
									return false;
								return receiver.SwitchState();

							default:
                                return -1;
                        }

                    default:
                        return -1;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Sets a property or value of the specified item
        /// </summary>
        /// <param name="name">Field or property to modify</param>
        /// <param name="table">Table from which to change the value</param>
        /// <param name="item">Object whose value will be modified</param>
        /// <param name="value">New value for the property</param>
        /// <returns>True if the change is made; false otherwise</returns>
        protected override bool SetData(Names name, Tables table, object item, object value)
        {
            try
            {
                ResetChanges();
                switch (table)
                {
                    case Tables.Transmitter:
                        switch (name)
                        {
                            default:
                                return false;
                        }

                    case Tables.Receiver:
                        switch (name)
                        {
                            default:
                                return false;
                        }

                    default:
                        return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Returns data from lower layers
        /// </summary>
        /// <param name="name">Name of the action</param>
        /// <param name="table">Table to process</param>
        /// <param name="item">Additional information</param>
        /// <returns>Returned information</returns>
        protected override object GetData(Names name, Tables table, object item)
        {
            switch (table)
            {
                case Tables.Transmitter:
                    switch (name)
                    {
                        case Names.GetTransmitters:
                            return repositoryManager.TransmittersRepository.GetAll();

                        case Names.GetSelectedTransmitter:
                            return SelectedTransmitter;

                        default:
                            break;
                    }
                    break;

                case Tables.Receiver:
                    switch (name)
                    {
                        case Names.GetReceivers:
                            return repositoryManager.ReceiverRepository.GetAll();

						case Names.GetSelectedReceiver:
							return SelectedReceiver;

						default:
                            break;
                    }
                    break;

                default:
                    break;
            }

            return null;
        }

        #region GETTERS

        /// <summary>
        /// Retrieves a receiver by its unique identifier.
        /// </summary>
        /// <param name="Id">The ID of the receiver to retrieve.</param>
        /// <returns>The matching <see cref="Receiver"/> instance if found; otherwise, <c>null</c>.</returns>
        private Receiver? GetReceiver(int Id)
        {
            return repositoryManager.ReceiverRepository.GetById(Id);
        }

        /// <summary>
        /// Retrieves a transmitter by its unique identifier.
        /// </summary>
        /// <param name="Id">The ID of the transmitter to retrieve.</param>
        /// <returns>The matching <see cref="Transmitter"/> instance if found; otherwise, <c>null</c>.</returns>
        private Transmitter? GetTransmitter(int Id)
        {
            return repositoryManager.TransmittersRepository.GetById(Id);
        }

        #endregion

    }
}
