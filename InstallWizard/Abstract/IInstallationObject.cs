using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstallWizard.Abstract
{
    /// <summary>
    /// You need to implement this interface to provide all required details about your installation
    /// </summary>
    public interface IInstallationObject
    {
        /// <summary>
        /// Fire this event to update installation process screen with new line
        /// </summary>
        event Action<string> ProcessUpdated;

        /// <summary>
        /// Put all your installation process here, using self properties, configured on installation stages
        /// </summary>
        /// <param name="error">The error description - if any. It is used if function returns <c>false</c></param>
        /// <returns><c>true</c>, if installation succeed, and <c>false</c> otherwise</returns>
        bool Install(out string error);
    }
}
