using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InstallWizard.Concrete.Form
{
    public class FormViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public List<Control> Controls { get; set; }
    }
}
