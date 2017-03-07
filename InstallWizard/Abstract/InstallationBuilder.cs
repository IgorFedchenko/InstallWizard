using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstallWizard.StageModels;

namespace InstallWizard.Abstract
{
    abstract class InstallationBuilder<TInstallationObject>
    {
        private readonly List<StageModel<TInstallationObject>> _stageModels = new List<StageModel<TInstallationObject>>();
         
        public InstallationBuilder<TInstallationObject> WithStage(StageModel<TInstallationObject> stage)
        {
            _stageModels.Add(stage);

            return this;
        }

        public abstract Installation<TInstallationObject> Build();
    }
}
