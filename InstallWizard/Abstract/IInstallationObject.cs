using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstallWizard.Abstract
{
    public interface IInstallationObject
    {
        event Action<string> ProcessUpdated;

        bool Install(out string error);
    }
}
