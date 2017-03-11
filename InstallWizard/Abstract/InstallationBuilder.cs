using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstallWizard.Concrete.Console;
using InstallWizard.Concrete.Form;
using InstallWizard.StageModels;

namespace InstallWizard.Abstract
{
    /// <summary>
    /// This class provides methods to configure and create installation.
    /// To use it, you will need to instantiate any concrete implementation (<see cref="ConsoleInstallationBuilder{TInstallationObject}"/> and <see cref="FormInstallationBuilder{TInstallationObject}"/>)
    /// </summary>
    /// <typeparam name="TInstallationObject">Your custom type, providing installation details <see cref="InstallationObjectBase"/></typeparam>
    public abstract class InstallationBuilder<TInstallationObject> where TInstallationObject : InstallationObjectBase, new()
    {
        private readonly List<StageModel<TInstallationObject>> _stageModels = new List<StageModel<TInstallationObject>>();
        private string _installationName = string.Empty;

        /// <summary>
        /// Adds stage to installation process
        /// </summary>
        /// <param name="stage">The installation stage.</param>
        /// <returns>InstallationBuilder object - use it for further configuration</returns>
        public InstallationBuilder<TInstallationObject> WithStage(StageModel<TInstallationObject> stage)
        {
            _stageModels.Add(stage);

            return this;
        }

        /// <summary>
        /// Specifies installation name (name of the package to install)
        /// </summary>
        /// <param name="installationName">Name of the installation.</param>
        /// <returns>InstallationBuilder object - use it for further configuration</returns>
        public InstallationBuilder<TInstallationObject> WithName(string installationName)
        {
            _installationName = installationName;

            return this;
        }

        /// <summary>
        /// Builds installation object of appropriate type (console, gui-based and etc.)
        /// </summary>
        /// <returns>Installation object to use <see cref="Installation{TInstallationObject}"/></returns>
        /// <exception cref="System.Exception">Stages collection is empty!</exception>
        public Installation<TInstallationObject> Build()
        {
            if (!_stageModels.Any())
            {
                throw new Exception("Stages collection is empty!");
            }

            return GetInstallation(_installationName, _stageModels);
        }

        /// <summary>
        /// Each implementation of builder will override this function to provide the concrete installation object
        /// </summary>
        /// <param name="installationName">Name of the installation.</param>
        /// <param name="stages">The stages of the installation.</param>
        /// <returns></returns>
        protected abstract Installation<TInstallationObject> GetInstallation(string installationName, List<StageModel<TInstallationObject>> stages);
    }
}
