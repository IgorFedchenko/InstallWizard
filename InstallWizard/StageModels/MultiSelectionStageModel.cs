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
    public abstract class MultiSelectionStageModel<TInstallationObject>
        : StageModel<TInstallationObject> where TInstallationObject : IInstallationObject, new()
    {
        public abstract string[] Options { get; }

        protected int[] SelectedIndexes { get; private set; }

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

            viewModel.UserPrompt = $"Selected numbers, separated by spaces and/or commas (i.e. '1, 3')";

            return viewModel;
        }

        public override void ParseConsoleInput(string input)
        {
            SelectedIndexes = Array.ConvertAll(input.Split(',', ' ').Where(s => s.Length > 0).ToArray(), int.Parse);
        }

        public override FormViewModel GetFormViewModel()
        {
            var viewModel = new FormViewModel()
            {
                Title = Title,
                Description = Description,
            };

            var selectionControls = new List<Control>();
            foreach (var option in Options)
            {
                selectionControls.Add(new CheckBox()
                {
                    Text = option,
                });
            }

            viewModel.Controls = selectionControls;

            return viewModel;
        }

        public override void ParseFormInput(Control[] controls)
        {
            var selected = controls.Where(c => c is CheckBox).Cast<CheckBox>().Where(b => b.Checked);
            SelectedIndexes = selected.Select(option => Options.ToList().IndexOf(option.Text)).ToArray();
        }
    }
}
