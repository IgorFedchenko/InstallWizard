using System;
using System.Collections.Generic;
using InstallWizard.StageModels;

namespace InstallWizard.Abstract
{
    /// <summary>
    /// This class performs all required actions to install components using your InstallationObject
    /// </summary>
    /// <typeparam name="TInstallationObject">The type of the installation object.</typeparam>
    public abstract class Installation<TInstallationObject> where TInstallationObject : InstallationObjectBase, new()
    {
        /// <summary>
        /// Name of the installation (usually name of the package).
        /// </summary>
        protected string InstallationName { get; }

        /// <summary>
        /// Installation object to use
        /// </summary>
        protected TInstallationObject InstallationObject { get; } = Activator.CreateInstance<TInstallationObject>();

        protected Installation(string installationName)
        {
            InstallationName = installationName;
        }

        /// <summary>
        /// Starts installation process
        /// </summary>
        public abstract void Start(); 
    }
}
