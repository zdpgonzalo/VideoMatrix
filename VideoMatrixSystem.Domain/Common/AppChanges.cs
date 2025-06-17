namespace VideoMatrixSystem.Domain.Common
{
    /// <summary>
    /// Class that manages the update level state for the user interface.
    /// </summary>
    public class AppChanges
    {
        private static OpResul dataResul = OpResul.Cancel;

        public static OpResul DataResul
        {
            get => dataResul;
        }

        /// <summary>
        /// Updates the update level for the UI.
        /// The update level is only changed if the new value is higher 
        /// than the current one or if it is Cancel.
        /// </summary>
        /// <param name="opResul">The UI update level</param>
        public static void SetChanges(OpResul opResul)
        {
            if (opResul == DataResul)
                return;

            if (opResul == OpResul.Cancel || opResul > DataResul)
            {
                dataResul = opResul;
            }
        }

        /// <summary>
        /// Resets the update level to the lowest level.
        /// </summary>
        public static void ResetChanges()
        {
            dataResul = OpResul.Cancel;
        }
        
    }
}
