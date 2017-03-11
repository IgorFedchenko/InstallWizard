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
    /// <summary>
    /// Concrete implementation of installation for GUI (WinForms) <see cref="Installation{TInstallationObject}"/>
    /// </summary>
    /// <typeparam name="TInstallationObject">The type of the installation object.</typeparam>
    /// <seealso cref="InstallWizard.Abstract.Installation{TInstallationObject}" />
    class FormInstallation<TInstallationObject> : Installation<TInstallationObject> where TInstallationObject : InstallationObjectBase, new()
    {
        private InstallationForm _installationForm;
        private bool _formIsClosed = false;

        public FormInstallation(string installationName, List<StageModel<TInstallationObject>> stages)
            : base(installationName, stages)
        {
        }

        protected override void OnStart()
        {
            _installationForm = CreateForm();
            ShowForm(_installationForm);

            ConsoleWindowManager.HideConsoleWindow();
        }

        protected override bool HandleStage(StageModel<TInstallationObject> stage)
        {
            var viewModel = stage.GetFormViewModel();
            var view = new FormStageView(viewModel);
            _installationForm.BeginInvoke((Action)(() => view.Draw(_installationForm.InstallatoinPanelObject)));
            _installationForm.StageFinished.WaitOne();

            if (_formIsClosed)
                return false;

            stage.ParseFormInput(viewModel.Controls.ToArray());
            stage.UpdateInstallationObject(InstallationObject);

            return true;
        }

        protected override void HandleInstallationProcessStarted()
        {
            InvokeAction(_installationForm, () => SetLabel(_installationForm.InstallatoinPanelObject, "Installing components..."));
            InvokeAction(_installationForm, () =>
            {
                _installationForm.NextButton.Text = "Close";
                _installationForm.NextButton.Enabled = false;
            });
        }

        protected override void HandleInstallationProcessUpdated(string updateMessage)
        {
            _installationForm.Invoke((Action)(() => AddText(_installationForm.InstallatoinPanelObject, updateMessage)));
        }

        protected override void HandleInstallationProcessSucceed()
        {
            _installationForm.Invoke((Action)(() => AddText(_installationForm.InstallatoinPanelObject, "Installation finished")));
        }

        protected override void HandleInstallationProcessFailed(Exception error)
        {
            _installationForm.Invoke((Action)(() => AddText(_installationForm.InstallatoinPanelObject, "Installation failed: " + error.Message)));
        }

        protected override void HandleInstallationProcessFinished()
        {
            InvokeAction(_installationForm, () => _installationForm.NextButton.Enabled = true);
            _installationForm.StageFinished.WaitOne();
        }

        private InstallationForm CreateForm()
        {
            var form = new InstallationForm
            {
                Text = InstallationName
            };
            form.FormClosing += (s, e) => HandleFormClosed();

            return form;
        }

        private void ShowForm(InstallationForm form)
        {
            Task.Run(() =>
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(form);
            });

            form.Opened.WaitOne();
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
            var label = installationPanel.Controls.Find("TextLabel", true).FirstOrDefault();
            if (label == null)
                SetLabel(installationPanel, text);
            else
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
