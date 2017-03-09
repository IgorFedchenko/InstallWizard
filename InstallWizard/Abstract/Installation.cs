using System;
using System.Collections.Generic;
using InstallWizard.StageModels;

namespace InstallWizard.Abstract
{
    public abstract class Installation<TInstallationObject> where TInstallationObject : IInstallationObject, new()
    {
        protected string InstallationName { get; }

        protected TInstallationObject InstallationObject { get; } = Activator.CreateInstance<TInstallationObject>();

        protected Installation(string installationName)
        {
            InstallationName = installationName;
        }

        public abstract void Start(); 
    }
}
