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
    /// <summary>
    /// This is a base class for all kinds of stages of installation process
    /// </summary>
    /// <typeparam name="TInstallationObject">The type of the installation object.</typeparam>
    public abstract class StageModel<TInstallationObject> where TInstallationObject : InstallationObjectBase, new()
    {
        /// <summary>
        /// Title of installation stage
        /// </summary>
        public abstract string Title { get; }

        /// <summary>
        /// Description of installation stage
        /// </summary>
        public abstract string Description { get; }

        // TODO: Move this view-related functions to external converter

        public abstract ConsoleViewModel GetConsoleViewModel();

        public abstract void ParseConsoleInput(string input);

        public abstract FormViewModel GetFormViewModel();

        public abstract void ParseFormInput(Control[] controls);

        /// <summary>
        /// Updates the installation object with new data from this stage.
        /// </summary>
        /// <param name="installationObject">The installation object.</param>
        public abstract void UpdateInstallationObject(TInstallationObject installationObject);

        /// <summary>
        /// Checks if this stage is required depending on installation object stage. Defaults to true/
        /// </summary>
        /// <param name="installationObject">The installation object.</param>
        /// <returns>Return true, if this stage is needed, and false otherwise</returns>
        /// <remarks>Override this function to skip stage in some conditions</remarks>
        public virtual bool Required(TInstallationObject installationObject)
        {
            return true;
        }

        /// <summary>
        /// Override this to initialize stage stage with data from installation object
        /// </summary>
        /// <param name="installationObject">The installation object.</param>
        public virtual void Initialize(TInstallationObject installationObject)
        {
        }
    }
}
