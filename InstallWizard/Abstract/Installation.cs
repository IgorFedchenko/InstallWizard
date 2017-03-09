using System;
using System.Collections.Generic;
using InstallWizard.StageModels;

namespace InstallWizard.Abstract
{
    public abstract class Installation<TInstallationObject> where TInstallationObject : IInstallationObject, new()
    {
        protected TInstallationObject InstallationObject { get; } = Activator.CreateInstance<TInstallationObject>();

        public abstract void Start(); 
    }
}
