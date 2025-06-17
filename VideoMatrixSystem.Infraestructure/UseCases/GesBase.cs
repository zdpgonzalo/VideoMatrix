using VideoMatrixSystem.Domain.Common;
using VideoMatrixSystem.Domain.General;

namespace VideoMatrixSystem.Infraestructure.UseCases
{
    public class GesBase<TGest, TEvent, TAction, TName, TTable> : IAction
                 where TEvent : Enum where TAction : Enum
                 where TName : Enum where TTable : Enum
    {
        protected IAction Parent { get; set; }

        public GesBase(IAction parent)
        {
            Parent = parent;
        }

        public GesBase()
        {
        }

        #region IAction Interface for Data Binding

        /// <summary>
        /// Decomposes the identifier into an Action-Table instruction and 
        /// executes the specified action.
        /// </summary>
        /// <param name="ident">Name of the Action-Table instruction</param>
        /// <param name="info">Additional information</param>
        /// <returns>Result of the executed operation</returns>
        public Task<object> Action(string ident, params object[] info)
        {
            ResetChanges();

            if (ident != null)
            {
                string cTable = Data.GetTable(ident);
                string cOper = Data.GetName(ident);

                if (Data.IsDefined<TTable>(cTable) &&
                    Data.IsDefined<TAction>(cOper))
                {
                    var action = Data.GetEnum<TAction>(cOper);
                    var table = Data.GetEnum<TTable>(cTable);

                    return Action(action, table, info);
                }
            }

            return null;
        }

        /// <summary>
        /// Performs a Set operation on a specified item's property or value.
        /// </summary>
        /// <param name="ident">Instruction with Name-Table to change</param>
        /// <param name="item">Object whose value will be modified</param>
        /// <param name="value">New value for the property</param>
        /// <returns>True if the change was made; False otherwise</returns>
        public bool SetData(string ident, object item, object value)
        {
            ResetChanges();
            bool result = false;

            if (ident != null)
            {
                string cTable = Data.GetTable(ident);
                string cName = Data.GetName(ident);

                if (Data.IsDefined<TTable>(cTable) &&
                    Data.IsDefined<TName>(cName))
                {
                    var name = Data.GetEnum<TName>(cName);
                    var table = Data.GetEnum<TTable>(cTable);

                    result = SetData(name, table, item, value);
                }
            }

            return result;
        }

        /// <summary>
        /// Executes a get instruction from the domain layer.
        /// </summary>
        /// <param name="ident">Instruction with Name-Table to perform</param>
        /// <param name="item">Item to process</param>
        /// <returns>Value of the property or field</returns>
        public object GetData(string ident, object item)
        {
            SetChanges(OpResul.Cancel);
            object value = null;

            if (ident != null)
            {
                string cTable = Data.GetTable(ident);
                string cName = Data.GetName(ident);

                if (Data.IsDefined<TTable>(cTable) &&
                    Data.IsDefined<TName>(cName))
                {
                    var name = Data.GetEnum<TName>(cName);
                    var table = Data.GetEnum<TTable>(cTable);

                    value = GetData(name, table, item);
                }
            }

            return value;
        }

        /// <summary>
        /// Changes the update level of the application.
        /// </summary>
        /// <param name="opResul">Update level</param>
        public static void SetChanges(OpResul opResul)
        {
            AppChanges.SetChanges(opResul);
        }

        /// <summary>
        /// Resets the update level to the lowest level.
        /// </summary>
        public void ResetChanges()
        {
            AppChanges.ResetChanges();
        }

        /// <summary>
        /// Returns the update level.
        /// </summary>
        /// <returns>Update level</returns>
        public OpResul GetDataResul()
        {
            return AppChanges.DataResul;
        }
        #endregion

        #region Metodos sobrecargables para enlace a datos

        protected virtual Task<object> Action(TAction oper, TTable table, params object[] info)
        {
            return Task.FromResult<object>(null);
        }

        protected virtual object GetData(TName name, TTable table, object item)
        {
            return null;
        }

        protected virtual bool SetData(TName name, TTable table, object item, object value)
        {
            return false;
        }

        #endregion

    }

}
