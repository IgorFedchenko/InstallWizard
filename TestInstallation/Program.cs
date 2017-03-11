using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstallWizard.Abstract;
using InstallWizard.Concrete.Console;
using InstallWizard.Concrete.Form;
using InstallWizard.StageModels;
using Newtonsoft.Json;

namespace TestInstallation
{
    class Program
    {
        static void Main(string[] args)
        {
            InstallationBuilder<InstallationObject> builder;

            if (args.Contains("--console") || args.Contains("-c"))
                builder = new ConsoleInstallationBuilder<InstallationObject>();
            else
                builder = new FormInstallationBuilder<InstallationObject>();

            var installation = builder.WithStage(new VersionSelection())
                                      .WithStage(new ComponentsSelection())
                                      .Build();

            installation.Start();
        }
    }

    class InstallationObject : InstallationObjectBase
    {
        public int Version { get; set; }

        public string[] Components { get; set; }

        protected override void Install()
        {
            NotifyOfProcessUpdate("Started");

            File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "result.txt"), JsonConvert.SerializeObject(this, Formatting.Indented));

            NotifyOfProcessUpdate("Finished");
        }
    }

    class VersionSelection : SingleSelectionStageModel<InstallationObject>
    {
        public override string Title => "Sample single seletion";

        public override string Description => "Select version to install";

        public override void UpdateInstallationObject(InstallationObject installationObjectBase)
        {
            installationObjectBase.Version = SelectedIndex == 0 ? 1 : 2;
        }

        public override string[] Options => new string[]
        {
            "Version 1.0",
            "Version 1.5",
        };
    }

    class ComponentsSelection :  MultiSelectionStageModel<InstallationObject>
    {
        public override string Title => "Sample multiple seletion";

        public override string Description => "Select components to install";

        public override void UpdateInstallationObject(InstallationObject installationObjectBase)
        {
            installationObjectBase.Components = SelectedIndexes.Select(index => Options[index]).ToArray();
        }

        public override string[] Options => new string[]
        {
            "Component 1",
            "Component 2",
            "Component 3",
        };
    }
}
