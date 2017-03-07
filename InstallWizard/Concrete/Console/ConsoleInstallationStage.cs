using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstallWizard.Abstract;

namespace InstallWizard.Concrete.Console
{
    class ConsoleInstallationStage<TInstallationObject>
    {
        private readonly string _text;
        private readonly string _userPrompt;

        public ConsoleInstallationStage(string body, string userPrompt)
        {
            _text = text;
            _userPrompt = userPrompt;
        }

        public override string ToString()
        {
            return $"***************************\n" +
                   $"{Title}\n" +
                   $"***************************\n\n" +
                   $"{Description}\n\n" +
                   $"{_text}\n" +
                   $"{_userPrompt}";
        }

        protected override void UpdateInstallationObject()
        {
            throw new NotImplementedException();
        }
    }
}
