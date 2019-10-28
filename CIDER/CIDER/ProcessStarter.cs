using System.Diagnostics;

namespace CIDER
{
    public interface IProcessStarter
    ///Summary
    ///This interface is used as an abstraction. This is done so the classes using this class can be unit tested. It is a seam.
    {
        void Start(ProcessStartInfo info);
    }
}