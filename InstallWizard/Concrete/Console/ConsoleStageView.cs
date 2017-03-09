using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstallWizard.Abstract;

namespace InstallWizard.Concrete.Console
{
    class ConsoleStageView
    {
        private readonly ConsoleViewModel _viewModel;

        public ConsoleStageView(ConsoleViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override string ToString()
        {
            return $"***************************\n" +
                   $"{_viewModel.Title}\n" +
                   $"***************************\n\n" +
                   $"{_viewModel.Description}\n\n" +
                   $"{_viewModel.Body}\n" +
                   $"{_viewModel.UserPrompt}: ";
        }
    }
}
