using System;
using System.IO;
using Microsoft.Win32;

namespace Installer
{

    /// <summary>
    /// Entry point for the installer.
    /// </summary>
    class Program {

		const string URI_SCHEME = "myscheme";
		const string URI_KEY = "URL:MyScheme Protocol";

		static void RegisterUriScheme(string appPath) {
			// HKEY_CLASSES_ROOT\myscheme
			using (RegistryKey hkcrClass = Registry.ClassesRoot.CreateSubKey(URI_SCHEME)) {
				hkcrClass.SetValue(null, URI_KEY);
				hkcrClass.SetValue("URL Protocol", String.Empty, RegistryValueKind.String);

				// use the application's icon as the URI scheme icon
				using (RegistryKey defaultIcon = hkcrClass.CreateSubKey("DefaultIcon")) {
					string iconValue = String.Format("\"{0}\",0", appPath);
					defaultIcon.SetValue(null, iconValue);
				}

				// open the application and pass the URI to the command-line
				using (RegistryKey shell = hkcrClass.CreateSubKey("shell")) {
					using (RegistryKey open = shell.CreateSubKey("open")) {
						using (RegistryKey command = open.CreateSubKey("command")) {
							string cmdValue = String.Format("\"{0}\" \"%1\"", appPath);
							command.SetValue(null, cmdValue);
						}
					}
				}
			}
		}

		static void UnregisterUriScheme() {
			Registry.ClassesRoot.DeleteSubKeyTree(URI_SCHEME);
		}

		static void Main(string[] args) {
			if ((args.Length > 0) && (args[0].Equals("/u") || args[0].Equals("-u"))) {
				// uninstall
				Console.Write("Attempting to unregister URI scheme...");

				try {
					UnregisterUriScheme();
					Console.WriteLine(" Success.");
				}
				catch (Exception ex) {
					Console.WriteLine(" Failed!");
					Console.WriteLine("{0}: {1}", ex.GetType().Name, ex.Message);
				}
			}
			else {
				// install
				string appPath = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), "Application.exe");

				Console.Write("Attempting to register URI scheme...");

				try {
					if (!File.Exists(appPath)) {
						throw new InvalidOperationException(String.Format("Application not found at: {0}", appPath));
					}

					RegisterUriScheme(appPath);
					Console.WriteLine(" Success.");
				}
				catch (Exception ex) {
					Console.WriteLine(" Failed!");
					Console.WriteLine("{0}: {1}", ex.GetType().Name, ex.Message);
				}
			}

			Console.WriteLine();
			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
		}
	}
}
