using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InstallWizard.Concrete.Console;

namespace InstallWizard.StageModels
{
    abstract class StageModel<TInstallationObject>
    {
        public abstract string Title { get; }

        public abstract string Description { get; }



        public abstract void UpdateInstallationObject(TInstallationObject installationObject);
    }
}
