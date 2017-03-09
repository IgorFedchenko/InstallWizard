using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstallWizard.StageModels;

namespace InstallWizard.Abstract
{
    public abstract class InstallationBuilder<TInstallationObject> where TInstallationObject : IInstallationObject, new()
    {
        private readonly List<StageModel<TInstallationObject>> _stageModels = new List<StageModel<TInstallationObject>>();
        private string _installationName = string.Empty;
         
        public InstallationBuilder<TInstallationObject> WithStage(StageModel<TInstallationObject> stage)
        {
            _stageModels.Add(stage);

            return this;
        }

        public InstallationBuilder<TInstallationObject> WithName(string installationName)
        {
            _installationName = installationName;

            return this;
        }

        public Installation<TInstallationObject> Build()
        {
            if (!_stageModels.Any())
            {
                throw new Exception("Stages collection is empty!");
            }

            return GetInstallation(_installationName, _stageModels);
        }

        protected abstract Installation<TInstallationObject> GetInstallation(string installationName, List<StageModel<TInstallationObject>> stages);
    }
}
