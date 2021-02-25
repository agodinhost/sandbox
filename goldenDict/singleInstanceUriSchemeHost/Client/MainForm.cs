using System;
using System.Windows.Forms;

namespace Application
{

    public partial class MainForm : Form {

		public MainForm() {
			InitializeComponent();
		}

		/// <summary>
		/// Adds the specified URI to the text area on the form.
		/// </summary>
		/// <param name="uri"></param>
		public void AddUri(Uri uri) {
			if (InvokeRequired) {
				BeginInvoke(new Action<Uri>(AddUri), uri);
			}
			else {
				txtOutput.Text += uri.ToString() + Environment.NewLine;
			}
		}
	}
}
