using System.Collections.Generic;
using InstallWizard.StageModels;

namespace InstallWizard.Abstract
{
    abstract class Installation<TInstallationObject>
    {
        protected TInstallationObject InstallationObject { get; }

        public string InstallationName { get; }

        protected Installation(string installationName)
        {
            InstallationName = installationName;

            InstallationObject = default(TInstallationObject);
        }

        public abstract void Start();
    }
}
