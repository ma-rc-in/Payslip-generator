using System;
using System.Drawing;
using System.Drawing.Printing;

using System.Windows.Forms;


namespace payrollSystem
{
    public partial class payrollSystem : Form
    {
        private bool btnOpenWasClicked = false;

        string username;

        public payrollSystem()
        {
            InitializeComponent();
            Shown += Form1_Shown;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //btnOpenWasClicked = true;           

            if (txtLogin.Text == "" && txtPass.Text == "")
            {
                tabControl1.TabPages.Add(tbpDetails);
                tabControl1.SelectedIndex = 2;
                tabControl1.TabPages.Remove(tbpMain);

                username = txtLogin.Text;
                lblUsername.Text = "Your login is: " + username;

                txtID.Focus();
            }

            else
            {
                MessageBox.Show("Unknown user or password", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLogin.Focus();
                txtLogin.Text = "";
                txtPass.Text = "";
            }

        }

        private void Form1_Shown(Object sender, EventArgs e)
        {
            txtLogin.Focus();

            tabControl1.TabPages.Remove(tbpDetails);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to log out?", "Are you sure?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                tabControl1.TabPages.Add(tbpMain);
                tabControl1.SelectedIndex = 2;
                tabControl1.TabPages.Remove(tbpDetails);
                txtLogin.Text = "";
                txtPass.Text = "";
            }
            else
            {
                btnOpenWasClicked = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close the program?", "Are you sure?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                string ID;
                string name;
                string surname;
                double hours = 0.0;
                double payRate = 13.45;
                double overtime = 1.5;
                double gross = 0.0;
                double tax = 0.0;
                double insurance = 0.0;
                double grossDeductions = 0.0;

                ID = txtID.Text;
                name = txtName.Text;
                surname = txtSurname.Text;
                hours = double.Parse(txtHours.Text);

                tax = gross * 0.2;
                gross = payRate * hours;

                if (gross > 157)
                {
                    insurance = gross * 0.12;
                }

                if (hours > 37)
                {
                    gross = gross * overtime;
                }

                grossDeductions = gross - (insurance + tax);

                rtxSummary.Text = "Employee ID: " + ID + "\n" + "Forename: " + name + "\n" + "Surname: " + surname + "\n" + "Income:" + "\n" + "Gross: " + "£" + gross + "\n" + "Gross + deductions (TAX + NI): " + "£" + grossDeductions;
            }
            catch
            {
                MessageBox.Show("Error! Check if all details are properly filled in!");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear the form?", "Are you sure?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                txtID.Text = "";
                txtName.Text = "";
                txtSurname.Text = "";
                txtHours.Text = "";
                rtxSummary.Text = "";
                txtID.Focus();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            // Confirm user wants to close
            switch (MessageBox.Show(this, "Are you sure you want to close the program?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                case DialogResult.No:
                    e.Cancel = true;
                    break;
                default:
                    break;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileSaving = new SaveFileDialog();
            fileSaving.FileName = "EmployeeSummary.txt";
            fileSaving.DefaultExt = "*.txt";
            fileSaving.Filter = "Text File|*.txt";
            if (fileSaving.ShowDialog() == DialogResult.OK && fileSaving.FileName.Length > 0)
                rtxSummary.SaveFile(fileSaving.FileName, RichTextBoxStreamType.PlainText);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DialogResult printResponse;
            printResponse = printDialog1.ShowDialog();
            if (printResponse == DialogResult.OK)
                printDocument1.Print(); //needs fixing
        }

        private void printPreviewDialog1_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font myFont = new Font("Arial", 20, FontStyle.Italic);
            Brush myBrush = new SolidBrush(Color.Black);

            e.Graphics.DrawString(rtxSummary.Text, myFont, myBrush, 20, 20);

            //or e.Graphics.DrawString(rtxSummary.Text, new Font("Arial", 20, FontStyle.Regular), Brushes.Black, 20, 20);
        }
    }
}



