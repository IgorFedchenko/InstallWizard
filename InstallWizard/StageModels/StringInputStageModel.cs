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
    public abstract class StringInputStageModel<TInstallationObject> : StageModel<TInstallationObject> where TInstallationObject : InstallationObjectBase, new()
    {
        protected string Input { get; private set; }

        public override ConsoleViewModel GetConsoleViewModel()
        {
            return new ConsoleViewModel()
            {
                Title = Title,
                Description = Description,
                Body = string.Empty,
                UserPrompt = "Input here",
            };
        }

        public override void ParseConsoleInput(string input)
        {
            Input = input;
        }

        public override FormViewModel GetFormViewModel()
        {
            var viewModel = new FormViewModel()
            {
                Title = Title,
                Description = Description,
            };

            var inputTextBox = new TextBox();
            viewModel.Controls = new List<Control>()
            {
               inputTextBox,
            };

            return viewModel;
        }

        public override void ParseFormInput(Control[] controls)
        {
            Input = (controls.First(c => c is TextBox) as TextBox).Text;
        }
    }
}
