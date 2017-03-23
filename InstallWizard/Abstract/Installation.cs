using System;
using System.Collections.Generic;
using System.Linq;
using InstallWizard.StageModels;

namespace InstallWizard.Abstract
{
    /// <summary>
    /// This class performs all required actions to install components using your InstallationObject
    /// </summary>
    /// <typeparam name="TInstallationObject">The type of the installation object.</typeparam>
    public abstract class Installation<TInstallationObject> where TInstallationObject : InstallationObjectBase, new()
    {
        private readonly List<StageModel<TInstallationObject>> _stages;

        /// <summary>
        /// Name of the installation (usually name of the package).
        /// </summary>
        protected string InstallationName { get; }

        /// <summary>
        /// Generates installation stages sequence depending on installation object stage
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<StageModel<TInstallationObject>> InstallationStages()
        {
            StageModel<TInstallationObject> nextStage;

            do
            {
                nextStage = _stages.FirstOrDefault(s => s.Required(InstallationObject) && !s.IsHandled);

                if (nextStage != null)
                    yield return nextStage;

            } while (nextStage != null);
        }

        /// <summary>
        /// Installation object to use
        /// </summary>
        protected TInstallationObject InstallationObject { get; } = Activator.CreateInstance<TInstallationObject>();

        protected Installation(string installationName, List<StageModel<TInstallationObject>> stages)
        {
            InstallationName = installationName;
            _stages = stages;
        }

        /// <summary>
        /// Starts installation process
        /// </summary>
        public void Start()
        {
            OnStart();

            foreach (var installationStage in InstallationStages())
            {
                installationStage.Initialize(InstallationObject);

                if (!HandleStage(installationStage))
                    return;

                try
                {
                    installationStage.Validate(InstallationObject);
                }
                catch (Exception ex)
                {
                    ShowValidationErrorMessage(ex.Message);
                    continue;
                }

                installationStage.UpdateInstallationObject(InstallationObject);
                installationStage.IsHandled = true;
            }

            InstallationObject.ProcessUpdated += HandleInstallationProcessUpdated;

            HandleInstallationProcessStarted();

            Exception error;
            var success = InstallationObject.Install(out error);

            if (success)
                HandleInstallationProcessSucceed();
            else
                HandleInstallationProcessFailed(error);

            HandleInstallationProcessFinished();
        }

        /// <summary>
        /// Called when installation stages processing starts, before any installation stage is processed
        /// </summary>
        protected abstract void OnStart();

        /// <summary>
        /// Handles installation stage: rendering, getting user input should me implemented here
        /// </summary>
        /// <param name="stage">The stage to handle</param>
        /// <returns><c>true</c>, if stage is handled, and <c>false</c> if installation should be canceled</returns>
        protected abstract bool HandleStage(StageModel<TInstallationObject> stage);

        /// <summary>
        /// Shows the validation error message to the user
        /// </summary>
        /// <param name="message">The message to show.</param>
        protected abstract void ShowValidationErrorMessage(string message);

        /// <summary>
        /// Called before the installation process, defined by user in installation object, starts
        /// </summary>
        protected abstract void HandleInstallationProcessStarted();

        /// <summary>
        /// Called when installation object fires update <see cref="InstallationObjectBase.NotifyOfProcessUpdate"/>
        /// </summary>
        /// <param name="updateMessage">The update message.</param>
        protected abstract void HandleInstallationProcessUpdated(string updateMessage);

        /// <summary>
        /// Called when installation process is finished successfully
        /// </summary>
        protected abstract void HandleInstallationProcessSucceed();

        /// <summary>
        /// Handles the installation process failed.
        /// </summary>
        /// <param name="error">Error description, which caused installation to fail</param>
        protected abstract void HandleInstallationProcessFailed(Exception error);

        /// <summary>
        /// Called after <see cref="HandleInstallationProcessSucceed"/> or <see cref="HandleInstallationProcessFailed"/>
        /// </summary>
        protected virtual void HandleInstallationProcessFinished()
        {
        }
    }
}
