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
    class FormInstallation<TInstallationObject> : Installation<TInstallationObject> where TInstallationObject : IInstallationObject, new()
    {
        private readonly List<StageModel<TInstallationObject>> _stages;
        private bool _formIsClosed = false;

        public FormInstallation(string installationName, List<StageModel<TInstallationObject>> stages)
            : base(installationName)
        {
            _stages = stages;
        }

        /// <summary>
        /// Starts installation process
        /// </summary>
        public override void Start()
        {
            var form = CreateForm();
            ShowForm(form);

            ConsoleWindowManager.HideConsoleWindow();
            
            foreach (var stageModel in _stages)
            {
                if (!DisplayStage(form, stageModel))
                    return;
            }

            MakeInstallation(form);
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

        private bool DisplayStage(InstallationForm form, StageModel<TInstallationObject> stageModel)
        {
            var viewModel = stageModel.GetFormViewModel();
            var view = new FormStageView(viewModel);
            form.BeginInvoke((Action)(() => view.Draw(form.InstallatoinPanelObject)));
            form.StageFinished.WaitOne();

            if (_formIsClosed)
                return true;

            stageModel.ParseFormInput(viewModel.Controls.ToArray());
            stageModel.UpdateInstallationObject(InstallationObject);

            return false;
        }

        private void MakeInstallation(InstallationForm form)
        {
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
