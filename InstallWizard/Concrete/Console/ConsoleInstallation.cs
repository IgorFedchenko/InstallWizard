using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstallWizard.Abstract;
using InstallWizard.StageModels;

namespace InstallWizard.Concrete.Console
{
    class ConsoleInstallation<TInstallationObject> : Installation<TInstallationObject>
    {
        private readonly List<StageModel<TInstallationObject>> _stages;

        public ConsoleInstallation(string installationName, List<StageModel<TInstallationObject>> stages)
            : base(installationName)
        {
            _stages = stages;
        }

        public override void Start()
        {
            System.Console.WriteLine("Starting installation");

            foreach (var stageModel in _stages)
            {
                System.Console.WriteLine(stageModel);
                var input = System.Console.ReadLine();
                stageModel.UpdateInstallationObject(InstallationObject);
            }
        }
    }
}
