using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InstallWizard.Abstract;
using InstallWizard.Helpers;
using InstallWizard.StageModels;

namespace InstallWizard.Concrete.Form
{
    class FormInstallation<TInstallationObject> : Installation<TInstallationObject> where TInstallationObject : IInstallationObject, new()
    {
        private readonly List<StageModel<TInstallationObject>> _stages;
        private bool _formIsClosed = false;

        public FormInstallation(List<StageModel<TInstallationObject>> stages)
        {
            _stages = stages;
        }

        public override void Start()
        {
            var form = new InstallationForm();
            form.FormClosing += (s, e) => HandleFormClosed();
            Task.Run(() => ShowForm(form));
            while (Application.OpenForms.Count == 0) { }

            ConsoleWindowManager.HideConsoleWindow();
            
            foreach (var stageModel in _stages)
            {
                var viewModel = stageModel.GetFormViewModel();
                var view = new FormStageView(viewModel);
                form.BeginInvoke((Action) (() => view.Draw(form.InstallatoinPanelObject)));
                form.StageFinished.WaitOne();

                if (_formIsClosed)
                    return;

                stageModel.ParseFormInput(viewModel.Controls.ToArray());
                stageModel.UpdateInstallationObject(InstallationObject);
            }

            InvokeAction(form, () => SetLabel(form.InstallatoinPanelObject, "Installing components..."));
            InvokeAction(form, () =>
            {
                form.NextButton.Text = "Close";
                form.NextButton.Enabled = false;
            });

            InstallationObject.ProcessUpdated += state =>
            {
                form.Invoke((Action)(() => AddText(form.InstallatoinPanelObject, state)));
            };

            string error;
            var success = InstallationObject.Install(out error);

            if (success)
                form.Invoke((Action)(() => AddText(form.InstallatoinPanelObject, "Installation finished")));
            else
                form.Invoke((Action)(() => AddText(form.InstallatoinPanelObject, "Installation failed: " + error)));

            InvokeAction(form, () => form.NextButton.Enabled = true);
            form.StageFinished.WaitOne();
        }

        private void ShowForm(System.Windows.Forms.Form form)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(form);
        }

        private void SetLabel(Panel installationPanel, string text)
        {
            installationPanel.Controls.Clear();

            installationPanel.Controls.Add(new Label()
            {
                Text = text,
                Name = "TextLabel",
                Width = installationPanel.Width,
                Height = installationPanel.Height,
            });
        }

        private void AddText(Panel installationPanel, string text)
        {
            var label = installationPanel.Controls.Find("TextLabel", true).First();
            label.Text += "\n" + text;
        }

        private void InvokeAction(System.Windows.Forms.Form form, Action action, bool async = false)
        {
            if (async)
                form.BeginInvoke(action);
            else
                form.Invoke(action);
        }

        private void HandleFormClosed()
        {
            _formIsClosed = true;
        }
    }
}
