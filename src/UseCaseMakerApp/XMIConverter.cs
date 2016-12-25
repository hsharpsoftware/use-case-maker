using System;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Net;

namespace UseCaseMaker
{
	/**
	 * @brief Descrizione di riepilogo per XMIConverter.
	 */
	public class XMIConverter
	{
		#region Enumerators and Constants
		// Public
		// Private
		// Protected
		#endregion

		#region Class Members
		// Public
		// Private
		private string stylesheetFilesPath = string.Empty;
		private string xmiFilesPath = string.Empty;
		// Protected
		#endregion

		#region Constructors
		/**
		 * @brief Costruttore di default per XMIConverter
		 */
		public XMIConverter(
			string stylesheetFilesPath,
			string xmiFilesPath)
		{
			this.stylesheetFilesPath = stylesheetFilesPath;
			this.xmiFilesPath = xmiFilesPath;
		}
		#endregion

		#region Public Properties
		#endregion

		#region Private Properties
		#endregion

		#region Protected Properties
		#endregion

		#region Public Methods
		public void Transform(string modelFilePath)
		{
			XmlResolver resolver = new XmlUrlResolver();
			resolver.Credentials = CredentialCache.DefaultCredentials;
			XmlTextReader doc = new XmlTextReader(modelFilePath);
			XslCompiledTransform transform = new XslCompiledTransform();
			transform.Load(this.stylesheetFilesPath + Path.DirectorySeparatorChar + "XMI11Export.xsl",null,resolver);
			transform.Transform(doc,null,new XmlTextWriter(this.xmiFilesPath,Encoding.UTF8),resolver);
		}
		#endregion

		#region Private Methods
		#endregion

		#region Protected Methods
		#endregion
	}
}
