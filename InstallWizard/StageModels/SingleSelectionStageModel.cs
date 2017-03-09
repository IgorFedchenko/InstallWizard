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
    public abstract class SingleSelectionStageModel<TInstallationObject> 
        : StageModel<TInstallationObject> where TInstallationObject : IInstallationObject, new()
    {
        public abstract string[] Options { get; }

        protected int SelectedIndex { get; private set; }

        public override ConsoleViewModel GetConsoleViewModel()
        {
            var viewModel = new ConsoleViewModel()
            {
                Title = Title,
                Description = Description,
            };

            var bodyBuilder = new StringBuilder();
            for (int i = 0; i < Options.Length; ++i)
            {
                bodyBuilder.AppendLine($"{i + 1}. {Options[i]}");
            }

            viewModel.Body = bodyBuilder.ToString();

            viewModel.UserPrompt = $"Selected option number ({1}-{Options.Length})";

            return viewModel;
        }

        public override void ParseConsoleInput(string input)
        {
            SelectedIndex = int.Parse(input) - 1;
        }

        public override FormViewModel GetFormViewModel()
        {
            var viewModel = new FormViewModel()
            {
                Title = Title,
                Description = Description,
            };

            var selectionControls = new List<Control>();
            bool first = true;
            foreach (var option in Options)
            {
                selectionControls.Add(new RadioButton()
                {
                    Text = option,
                    Checked = first,
                });

                first = false;
            }

            viewModel.Controls = selectionControls;

            return viewModel;
        }

        public override void ParseFormInput(Control[] controls)
        {
            var selected = controls.Where(c => c is RadioButton).Cast<RadioButton>().First(b => b.Checked);
            SelectedIndex = Options.ToList().IndexOf(selected.Text);
        }
    }
}
