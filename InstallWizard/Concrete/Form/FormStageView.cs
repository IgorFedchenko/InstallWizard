using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InstallWizard.Concrete.Form
{
    public class FormStageView
    {
        private readonly FormViewModel _viewModel;

        public FormStageView(FormViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Draw(Panel rootPanel)
        {
            rootPanel.Controls.Clear();

            var controls = new List<Control>();

            var titleLabel = new Label()
            {
                Text = _viewModel.Title,
            };
            titleLabel.Width = rootPanel.Width;
            titleLabel.Font = new Font(titleLabel.Font.FontFamily, titleLabel.Font.Size * 1.5f);
            controls.Add(titleLabel);

            controls.Add(new Label()
            {
                Text = _viewModel.Description,
                Width = rootPanel.Width,
            });

            var table = new TableLayoutPanel()
            {
                RowCount = controls.Count,
                ColumnCount = 1,
                Dock = DockStyle.Fill
            };

            foreach (var control in _viewModel.Controls)
            {
                control.Width = table.Width;
            }

            controls.AddRange(_viewModel.Controls.ToArray());

            table.Controls.AddRange(controls.ToArray());

            rootPanel.Controls.Add(table);
        }
    }
}
