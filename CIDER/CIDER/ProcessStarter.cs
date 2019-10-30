using System.Diagnostics;

namespace CIDER
{
    /// <summary>
    /// This interface should be implemented by classes being used to start processes
    /// </summary>
    public interface IProcessStarter
    {
        /// <summary>
        /// This function should start the specified process
        /// </summary>
        /// <param name="info">Information on the process to be started</param>
        void Start(ProcessStartInfo info);
    }
}