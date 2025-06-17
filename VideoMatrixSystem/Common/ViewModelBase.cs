using VideoMatrixSystem.Domain.Common;
using VideoMatrixSystem.Infraestructure.UseCases;

namespace VideoMatrixSystem.Common
{
    public partial class ViewModelBase
    {
        public ViewModelBase(IAction gesVideoMatrix)
        {
            DataRefer = gesVideoMatrix;
        }

        internal IAction DataRefer { get; set; }

        /// <summary>
        /// Calls the Set method of the management layer
        /// </summary>
        public virtual bool SetData(string name, object value, object item = null)
        {
            bool resul = false;
            OpResul dataResul = new();

            try
            {
                if (DataRefer == null) return false;

                resul = DataRefer.SetData(name, item, value);

                dataResul = DataRefer.GetDataResul();

                object[] values = [item, value];

                UpdateModel(dataResul);
            }
            catch (Exception ex)
            {
                return false;
            }

            return resul;
        }

        /// <summary>
        /// Calls the Get method of the management layer
        /// </summary>
        public virtual object GetData(string name, object item = null)
        {
            try
            {
                if (DataRefer == null) return false;

                return DataRefer.GetData(name, item);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool inRefresh = false;

        /// <summary>
        /// Calls the Action method of the management layer
        /// </summary>
        public virtual async Task<object> Action(string oper, object value, params object[] info)
        {
            if (inRefresh)
            {
                return false;
            }

            inRefresh = true;

            object resul = null;

            try
            {
                object[] values = [.. (new object[] { value }), .. info];

                if (DataRefer != null)
                {
                    resul = await DataRefer.Action(oper, values);

                    if (resul != null)
                    {
                        OpResul dataResul = DataRefer.GetDataResul();
                        UpdateModel(dataResul);
                    }
                }
            }
            catch (Exception exc)
            {
                return false;
            }
            finally
            {
                inRefresh = false;
            }

            return resul;
        }

        /// <summary>
        /// Updates the ViewModel data from the management class
        /// </summary>
        public virtual void UpdateModel(OpResul dataResul) { }
    }
}
