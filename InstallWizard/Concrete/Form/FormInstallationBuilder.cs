using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstallWizard.Abstract;
using InstallWizard.StageModels;

namespace InstallWizard.Concrete.Form
{
    public class FormInstallationBuilder<TInstallationObject> : InstallationBuilder<TInstallationObject> where TInstallationObject : IInstallationObject, new()
    {
        protected override Installation<TInstallationObject> GetInstallation(string installationName, List<StageModel<TInstallationObject>> stages)
        {
            return new FormInstallation<TInstallationObject>(installationName, stages);
        }
    }
}
