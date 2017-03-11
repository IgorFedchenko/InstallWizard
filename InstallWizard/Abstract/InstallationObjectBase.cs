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
    public abstract class InstallationObjectBase
    {
        /// <summary>
        /// Fire this event to update installation process screen with new line
        /// </summary>
        public event Action<string> ProcessUpdated;

        /// <summary>
        /// Describes installation procedure, i.e. running scripts, configuring system and etc.
        /// </summary>
        /// <param name="error">The error description - if any. It is used if function returns <c>false</c></param>
        /// <returns><c>true</c>, if installation succeed, and <c>false</c> otherwise</returns>
        public bool Install(out string error)
        {
            error = string.Empty;

            try
            {
                Install();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Put all your installation process here, using self properties, configured on installation stages.
        /// Ti indicate error, throw an exception
        /// </summary>
        protected abstract void Install();

        /// <summary>
        /// Notifies the of installation process update.
        /// </summary>
        /// <param name="text">The text.</param>
        protected void NotifyOfProcessUpdate(string text)
        {
            ProcessUpdated?.Invoke(text);
        }
    }
}
