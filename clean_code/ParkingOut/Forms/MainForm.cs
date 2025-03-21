using System;
using System.Windows.Forms;
using SimpleParkingAdmin.Utils;
using SimpleParkingAdmin.Models;
using SimpleParkingAdmin.Forms;

namespace SimpleParkingAdmin.Forms
{
    public partial class MainForm : Form
    {
        private readonly User _currentUser;
        private readonly IAppLogger _logger;

        public MainForm(User currentUser)
        {
            _currentUser = currentUser;
            _logger = new FileLogger();
            InitializeComponent();
        }

        private void ShowEntryForm()
        {
            using (var entryForm = new EntryForm(_currentUser))
            {
                entryForm.ShowDialog();
            }
        }

        private void ShowExitForm()
        {
            using (var exitForm = new ExitForm(_currentUser))
            {
                exitForm.ShowDialog();
            }
        }

        private void ShowReportForm()
        {
            using (var reportForm = new ReportForm(_currentUser))
            {
                reportForm.ShowDialog();
            }
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            // Designer code will be here
        }
        #endregion
    }
} 