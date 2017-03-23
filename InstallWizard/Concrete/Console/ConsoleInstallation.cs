using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstallWizard.Abstract;
using InstallWizard.StageModels;

namespace InstallWizard.Concrete.Console
{
    /// <summary>
    /// Concrete implementation of installation for console <see cref="Installation{TInstallationObject}"/>
    /// </summary>
    /// <typeparam name="TInstallationObject">The type of the installation object.</typeparam>
    /// <seealso cref="InstallWizard.Abstract.Installation{TInstallationObject}" />
    class ConsoleInstallation<TInstallationObject> : Installation<TInstallationObject> where TInstallationObject : InstallationObjectBase, new()
    {
        public ConsoleInstallation(string installationName, List<StageModel<TInstallationObject>> stages)
            : base(installationName, stages)
        {
        }

        protected override void OnStart()
        {
            System.Console.WriteLine("Starting installation");
        }

        protected override bool HandleStage(StageModel<TInstallationObject> stage)
        {
            System.Console.Write(GetView(stage));

            var input = System.Console.ReadLine();

            stage.ParseConsoleInput(input);

            return true;
        }

        protected override void ShowValidationErrorMessage(string message)
        {
            System.Console.WriteLine("Wrong input: " + message + " (Press Enter to continue)\n");
            System.Console.ReadLine();
        }

        protected override void HandleInstallationProcessStarted()
        {
            System.Console.WriteLine("\nInstalling components...\n");
        }

        protected override void HandleInstallationProcessUpdated(string updateMessage)
        {
            System.Console.WriteLine("Installation: " + updateMessage);
        }

        protected override void HandleInstallationProcessSucceed()
        {
            System.Console.WriteLine("\nInstallation finished");
        }

        protected override void HandleInstallationProcessFailed(Exception error)
        {
            System.Console.WriteLine("\nInstallation failed: " + error);
        }

        protected override void HandleInstallationProcessFinished()
        {
            System.Console.ReadLine();
        }

        private ConsoleStageView GetView(StageModel<TInstallationObject> model)
        {
            return new ConsoleStageView(model.GetConsoleViewModel());
        }
    }
}
