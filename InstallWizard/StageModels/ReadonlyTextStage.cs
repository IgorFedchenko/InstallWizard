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
    public abstract class ReadonlyTextStage<TInstallationObject>
        : StageModel<TInstallationObject> where TInstallationObject : InstallationObjectBase, new()
    {
        protected abstract string Text { get; }

        public override ConsoleViewModel GetConsoleViewModel()
        {
            return new ConsoleViewModel()
            {
                Title = Title,
                Description = Description,
                Body = Text,
                UserPrompt = "Press Enter to continue"
            };
        }

        public override void ParseConsoleInput(string input)
        {
        }

        public override FormViewModel GetFormViewModel()
        {
            var viewModel = new FormViewModel()
            {
                Title = Title,
                Description = Description,
            };

            var textLabel = new Label
            {
                Text = Text
            };
            viewModel.Controls = new List<Control>()
            {
                textLabel
            };

            return viewModel;
        }

        public override void ParseFormInput(Control[] controls)
        {
        }

        public override void UpdateInstallationObject(TInstallationObject installationObject)
        {
        }
    }
}
