using System;
using System.Windows.Forms;

using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibRoslynDocument;

namespace RoslynDoc
{
	/// <summary>
	///		Clase de configuración de la aplicación
	/// </summary>
	public static class Configuration
	{
		/// <summary>
		///		Carga los parámetros de documentación
		/// </summary>
		internal static void Load()
		{ // Carga los datos básicos
				SolutionFileName = Properties.Settings.Default.Solution;
				OutputPath = Properties.Settings.Default.Output;
			// Carga los parámetros
				Parameters = new DocumentationParameters();
				Parameters.TemplateFileName = Properties.Settings.Default.Template;
				Parameters.IDType = (DocumentationParameters.DocumentationType) Properties.Settings.Default.IDType;
				Parameters.ShowPublic = Properties.Settings.Default.WithPublic;
				Parameters.ShowInternal = Properties.Settings.Default.WithInternal;
				Parameters.ShowProtected = Properties.Settings.Default.WithProtected;
				Parameters.ShowPrivate = Properties.Settings.Default.WithPrivate;
			// Normaliza los datos
				if (OutputPath.IsEmpty())
					OutputPath = System.IO.Path.Combine(Application.StartupPath, "Data\\Documents");
			// Plantilla
				if (Parameters.TemplateFileName.IsEmpty())
					Parameters.TemplateFileName = System.IO.Path.Combine(Application.StartupPath, "Data\\Templates\\Templates.tpt");
		}

		/// <summary>
		///		Graba los parámetros de configuración
		/// </summary>
		internal static void Save(string strSolutionFileName, string strOutputPath, DocumentationParameters objParameters)
		{ // Ajusta los datos en memoria
				SolutionFileName = strSolutionFileName;
				OutputPath = strOutputPath;
				Parameters = objParameters;
			// Ajusta los datos de las propiedades
				Properties.Settings.Default.Solution = strSolutionFileName;
				Properties.Settings.Default.Output = strOutputPath;
				Properties.Settings.Default.Template = Parameters.TemplateFileName;
				Properties.Settings.Default.IDType = (int) Parameters.IDType;
				Properties.Settings.Default.WithPublic = Parameters.ShowPublic;
				Properties.Settings.Default.WithInternal = Parameters.ShowInternal;
				Properties.Settings.Default.WithProtected = Parameters.ShowProtected;
				Properties.Settings.Default.WithPrivate = Parameters.ShowPrivate;
			// Graba la configuración
				Properties.Settings.Default.Save();
		}

		/// <summary>
		///		Nombre del archivo de solución
		/// </summary>
		internal static string SolutionFileName { get; private set; }

		/// <summary>
		///		Nombre del archivo de salida
		/// </summary>
		internal static string OutputPath { get; private set; }

		/// <summary>
		///		Parámetros de configuración
		/// </summary>
		internal static DocumentationParameters Parameters { get; private set; }
	}
}
