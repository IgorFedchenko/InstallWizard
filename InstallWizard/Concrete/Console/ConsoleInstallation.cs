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
    class ConsoleInstallation<TInstallationObject> : Installation<TInstallationObject> where TInstallationObject : IInstallationObject, new()
    {
        private readonly List<StageModel<TInstallationObject>> _stages;

        public ConsoleInstallation(string installationName, List<StageModel<TInstallationObject>> stages) 
            : base(installationName)
        {
            _stages = stages;
        }

        /// <summary>
        /// Starts installation process
        /// </summary>
        public override void Start()
        {
            System.Console.WriteLine("Starting installation");

            foreach (var stageModel in _stages)
            {
                System.Console.Write(GetView(stageModel));

                var input = System.Console.ReadLine();

                stageModel.ParseConsoleInput(input);

                stageModel.UpdateInstallationObject(InstallationObject);
            }

            System.Console.WriteLine("\nInstalling components...\n");

            InstallationObject.ProcessUpdated += state => System.Console.WriteLine("Installation: " + state);

            string error;
            var success = InstallationObject.Install(out error);

            if (success)
                System.Console.WriteLine("\nInstallation finished");
            else
                System.Console.WriteLine("\nInstallation failed: " + error);

            System.Console.ReadLine();
        }

        private ConsoleStageView GetView(StageModel<TInstallationObject> model)
        {
            return new ConsoleStageView(model.GetConsoleViewModel());
        }
    }
}
