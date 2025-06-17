using VideoMatrixSystem.Domain.Common;

namespace VideoMatrixSystem.Infraestructure.UseCases
{
    public interface IAction
    {
        /// <summary>
        /// Performs an application action
        /// </summary>
        /// <param name="oper">Operation to perform</param>
        /// <param name="table">Table to operate on</param>
        /// <param name="info">Additional information</param>
        /// <returns>Result of the executed operation</returns>
        public Task<object> Action(string oper, object[] values);

        /// <summary>
        /// Sets a property or value of the specified item
        /// </summary>
        /// <param name="name">Field or property to modify</param>
        /// <param name="table">Table from which to change the value</param>
        /// <param name="item">Object whose value will be modified</param>
        /// <param name="value">New value for the property</param>
        /// <returns>True if the change is made; false otherwise</returns>
        public object GetData(string name, object item);

        /// <summary>
        /// Returns data from lower layers
        /// </summary>
        /// <param name="name">Name of the action</param>
        /// <param name="table">Table to process</param>
        /// <param name="item">Additional information</param>
        /// <returns>Returned information</returns>
        public bool SetData(string name, object item, object value);

        public OpResul GetDataResul();
    }
}
