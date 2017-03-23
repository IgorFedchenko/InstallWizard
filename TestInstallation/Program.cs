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
                                      .WithStage(new SampleInput())
                                      .WithStage(new Confirmation())
                                      .Build();

            installation.Start();
        }
    }

    class InstallationObject : InstallationObjectBase
    {
        public string Text { get; set; }

        public int Version { get; set; }

        public string[] Components { get; set; }

        protected override void Install()
        {
            NotifyOfProcessUpdate("Started");

            File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "result.txt"), JsonConvert.SerializeObject(this, Formatting.Indented));

            NotifyOfProcessUpdate("Finished");
        }
    }

    class Confirmation : ReadonlyTextStage<InstallationObject>
    {
        private string _text;

        public override string Title => "Confirmation";

        public override string Description => "confirm installation object stage";

        protected override string Text => _text;

        public override void Initialize(InstallationObject installationObject)
        {
            _text = JsonConvert.SerializeObject(installationObject);
        }
    }

    class SampleInput : StringInputStageModel<InstallationObject>
    {
        public override string Title => "Sample input";

        public override string Description => "Input some text here";

        public override void UpdateInstallationObject(InstallationObject installationObject)
        {
            installationObject.Text = Input;
        }

        public override void Validate(InstallationObject installationObject)
        {
            if (Input.Length == 0)
                throw new Exception("You need to write some text!");
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
