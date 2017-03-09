using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InstallWizard
{
    public partial class InstallationForm : Form
    {
        public Panel InstallatoinPanelObject => InstallatoinPanel;

        public Button NextButton => NextBtn;

        public AutoResetEvent StageFinished = new AutoResetEvent(false);

        public InstallationForm()
        {
            InitializeComponent();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            StageFinished.Set();
        }

        private void Next_Click(object sender, EventArgs e)
        {
            StageFinished.Set();
        }
    }
}
