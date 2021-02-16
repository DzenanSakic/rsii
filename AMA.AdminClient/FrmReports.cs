using System;
using System.Windows.Forms;

namespace AMA.AdminClient
{
    public partial class FrmReports : Form
    {
        public FrmReports()
        {
            InitializeComponent();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmUsersActivityReport frmUsersActivityReport = new FrmUsersActivityReport();
            frmUsersActivityReport.Show();
            Hide();
            frmUsersActivityReport.FormClosing += FrmUsersActivityReport_Closing;
        }

        private void FrmUsersActivityReport_Closing(object sender, FormClosingEventArgs e)
        {
            Show();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmUsersPerformanceReport frmUsersPerformanceReport = new FrmUsersPerformanceReport();
            frmUsersPerformanceReport.Show();
            Hide();
            frmUsersPerformanceReport.FormClosing += FrmUsersPerformanceReport_Closing;
        }

        private void FrmUsersPerformanceReport_Closing(object sender, FormClosingEventArgs e)
        {
            Show();
        }
    }
}
