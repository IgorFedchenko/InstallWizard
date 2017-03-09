using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InstallWizard.Abstract;
using InstallWizard.Concrete.Console;
using InstallWizard.Concrete.Form;

namespace InstallWizard.StageModels
{
    public abstract class StageModel<TInstallationObject> where TInstallationObject : IInstallationObject, new()
    {
        public abstract string Title { get; }

        public abstract string Description { get; }

        public abstract ConsoleViewModel GetConsoleViewModel();

        public abstract void ParseConsoleInput(string input);

        public abstract FormViewModel GetFormViewModel();

        public abstract void ParseFormInput(Control[] controls);

        public abstract void UpdateInstallationObject(TInstallationObject installationObject);
    }
}
