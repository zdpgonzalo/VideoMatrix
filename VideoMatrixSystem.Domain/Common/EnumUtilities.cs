namespace VideoMatrixSystem.Domain.Common
{
    /// <summary>
    /// Represents the possible states of a device.
    /// </summary>
    public enum DeviceState
    {
        None,
        Offline,
        StandBy,
        Active
    }

    /// <summary>
    /// Represents the level of update for the user interface.
    /// </summary>
    public enum OpResul
    {
        None,
        Cancel,
        Data,
        Line,
        Page,
        Docum,
        Range
    }
}
