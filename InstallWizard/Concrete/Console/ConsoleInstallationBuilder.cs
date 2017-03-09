using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstallWizard.Abstract;
using InstallWizard.StageModels;

namespace InstallWizard.Concrete.Console
{
    public class ConsoleInstallationBuilder<TInstallationObject> : InstallationBuilder<TInstallationObject> where TInstallationObject : IInstallationObject, new()
    {
        protected override Installation<TInstallationObject> GetInstallation(List<StageModel<TInstallationObject>> stages)
        {
            return new ConsoleInstallation<TInstallationObject>(stages);
        }
    }
}
