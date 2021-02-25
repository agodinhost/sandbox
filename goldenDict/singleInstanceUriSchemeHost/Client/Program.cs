using System;

namespace Application
{

    static class Program {

		/// <summary>
		/// Gets the main form in the application.
		/// </summary>
		internal static MainForm MainForm { get; private set; }

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args) {
			System.Windows.Forms.Application.EnableVisualStyles();
			System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

			Uri uri = null;
			if (args.Length > 0) {
				// a URI was passed and needs to be handled
				try {
					uri = new Uri(args[0].Trim());
				}
				catch (UriFormatException) {
					Console.WriteLine("Invalid URI.");
				}
			}

			IUriHandler handler = UriHandler.GetHandler();
			if (handler != null) {
				// the singular instance of the application is already running
				if (uri != null) handler.HandleUri(uri);

				// the process will now exit without displaying the main form
				//...
			}
			else {
				// this must become the singular instance of the application
				UriHandler.Register();

				MainForm = new MainForm();

				if (uri != null) {
					// a URI was passed, handle it locally
					MainForm.Shown += (o, e) => new UriHandler().HandleUri(uri);
				}

				// load and display the main form
				System.Windows.Forms.Application.Run(MainForm);
			}
		}
	}
}