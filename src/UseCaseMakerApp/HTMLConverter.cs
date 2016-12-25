using System;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Net;
using System.Reflection;
using System.Globalization;

namespace UseCaseMaker
{
	/// <summary>
	/// Descrizione di riepilogo per HTMLConverter.
	/// </summary>
	public class HTMLConverter
	{
		private string stylesheetFilesPath = string.Empty;
		private string htmlFilesPath = string.Empty;
		private Localizer localizer = null;

		public HTMLConverter(
			string stylesheetFilesPath,
			string htmlFilesPath,
			Localizer localizer)
		{
			this.stylesheetFilesPath = stylesheetFilesPath;
			this.htmlFilesPath = htmlFilesPath;
			this.localizer = localizer;
		}

		public void BuildNavigator(string modelFilePath)
		{
			XmlResolver resolver = new XmlUrlResolver();
			resolver.Credentials = CredentialCache.DefaultCredentials;
			XmlTextReader tr = new XmlTextReader(modelFilePath);
			XslCompiledTransform transform = new XslCompiledTransform();
			transform.Load(this.stylesheetFilesPath + Path.DirectorySeparatorChar + "ModelTree.xsl",null,resolver);
            XmlTextWriter tw = new XmlTextWriter(this.htmlFilesPath + Path.DirectorySeparatorChar + "ModelTree.htm", Encoding.UTF8);
			XsltArgumentList al = new XsltArgumentList();
			al.AddParam("modelBrowser","",this.localizer.GetValue("Globals","ModelBrowser"));
			al.AddParam("glossary","",this.localizer.GetValue("Globals","Glossary"));
            al.AddParam("stakeholders", "", this.localizer.GetValue("Globals", "Stakeholders"));
			transform.Transform(tr,al,tw,null);
			tw.Close();
            tr.Close();

            tr = new XmlTextReader(modelFilePath);
            transform.Load(this.stylesheetFilesPath + Path.DirectorySeparatorChar + "HomePage.xsl",null,resolver);
			tw = new XmlTextWriter(this.htmlFilesPath + Path.DirectorySeparatorChar + "main.htm",Encoding.UTF8);
			al = new XsltArgumentList();
			AssemblyName an = this.GetType().Assembly.GetName();
			al.AddParam("version","",an.Version.ToString(3));
            al.AddParam("model", "", this.localizer.GetValue("Globals", "Model"));
            al.AddParam("author", "", this.localizer.GetValue("Globals", "Author"));
            al.AddParam("company", "", this.localizer.GetValue("Globals", "Company"));
            al.AddParam("creationDate", "", this.localizer.GetValue("Globals", "CreationDate"));
            al.AddParam("exportPrintDate", "", this.localizer.GetValue("Globals", "ExportPrintDate"));
            al.AddParam("now", "", Convert.ToString(DateTime.Now, DateTimeFormatInfo.InvariantInfo));
            al.AddParam("release", "", this.localizer.GetValue("Globals", "Release"));
            transform.Transform(tr,al,tw,null);
			tw.Close();
            tr.Close();
		}

		public void BuildPages(string modelFilePath)
		{
			XmlResolver resolver = new XmlUrlResolver();
			resolver.Credentials = CredentialCache.DefaultCredentials;
			XmlDocument doc = new XmlDocument();
			doc.XmlResolver = resolver;
			doc.Load(modelFilePath);
			XmlNode modelNode = doc.SelectSingleNode("//Model");
			this.RecurseNode(modelFilePath, resolver, modelNode,"Package.xsl");
		}

        private void RecurseNode(string modelFilePath, XmlResolver resolver, XmlNode elementNode, string xsltName)
		{
            this.ElementToHTMLPage(modelFilePath, resolver, elementNode, xsltName);

			foreach(XmlNode childNode in elementNode)
			{
				if(childNode.Name == "Glossary")
				{
                    this.ElementToHTMLPage(modelFilePath, resolver, childNode, "Glossary.xsl");
				}
                if(childNode.Name == "Stakeholders")
                {
                    this.ElementToHTMLPage(modelFilePath, resolver, childNode, "Stakeholders.xsl");
                }
                if(childNode.Name == "Requirements")
                {
                    this.ElementToHTMLPage(modelFilePath, resolver, childNode, "Requirements.xsl");
                }
				if(childNode.Name == "Packages")
				{
					foreach(XmlNode packageNode in childNode.ChildNodes)
					{
                        this.RecurseNode(modelFilePath, resolver, packageNode, "Package.xsl");
					}
				}
				if(childNode.Name == "Actors")
				{
					foreach(XmlNode actorNode in childNode.ChildNodes)
					{
                        this.RecurseNode(modelFilePath, resolver, actorNode, "Actor.xsl");
					}
				}
				if(childNode.Name == "UseCases")
				{
					foreach(XmlNode useCaseNode in childNode.ChildNodes)
					{
                        this.RecurseNode(modelFilePath, resolver, useCaseNode, "UseCase.xsl");
					}
				}
			}
		}

		private void ElementToHTMLPage(
            string modelFilePath,
			XmlResolver resolver,
			XmlNode currentNode,
			string xslFileName
			)
		{
            String HTMLFileName = String.Empty;

			XsltArgumentList al = new XsltArgumentList();
			if(currentNode.Name == "Glossary")
			{
                HTMLFileName = "Glossary";
				al.AddParam("glossary","",this.localizer.GetValue("Globals","Glossary"));
				al.AddParam("glossaryItem","",this.localizer.GetValue("Globals","GlossaryItem"));
				al.AddParam("description","",this.localizer.GetValue("Globals","Description"));
			}
            if(currentNode.Name == "Stakeholders")
            {
                HTMLFileName = "Stakeholders";
                al.AddParam("stakeholders", "", this.localizer.GetValue("Globals", "Stakeholders"));
                al.AddParam("stakeholder", "", this.localizer.GetValue("Globals", "Stakeholder"));
                al.AddParam("description", "", this.localizer.GetValue("Globals", "Description"));
            }
            if(currentNode.Name == "Requirements")
            {
                HTMLFileName = "Req_" + currentNode.ParentNode.Attributes["UniqueID"].Value;
                al.AddParam("parentUniqueID", "", currentNode.ParentNode.Attributes["UniqueID"].Value);

                al.AddParam("categoryNodeSet", "", this.localizer.GetNodeSet("cmbCategory", "Item"));
                al.AddParam("importanceNodeSet", "", this.localizer.GetNodeSet("cmbImportance", "Item"));
                al.AddParam("acceptanceNodeSet", "", this.localizer.GetNodeSet("cmbAcceptanceStatus", "Item"));
                al.AddParam("historyTypeNodeSet", "", this.localizer.GetNodeSet("HistoryType", "Item"));
                al.AddParam("statusNodeSet", "", this.localizer.GetNodeSet("cmbStatus", "Item"));

                al.AddParam("proposedBy", "", this.localizer.GetValue("Globals", "ProposedBy"));
                al.AddParam("benefitTo", "", this.localizer.GetValue("Globals", "BenefitTo"));
                al.AddParam("category", "", this.localizer.GetValue("Globals", "Category"));
                al.AddParam("importance", "", this.localizer.GetValue("Globals", "Importance"));
                al.AddParam("status", "", this.localizer.GetValue("Globals", "Status"));
                al.AddParam("acceptance", "", this.localizer.GetValue("Globals", "Acceptance"));
                al.AddParam("mappedUCs", "", this.localizer.GetValue("Globals", "MappedUCs"));
                al.AddParam("history", "", this.localizer.GetValue("Globals", "History"));
                al.AddParam("requirements", "", this.localizer.GetValue("Globals", "Requirements"));
            }
			if(currentNode.Name == "Model" || currentNode.Name == "Package")
			{
                HTMLFileName = currentNode.Attributes["UniqueID"].Value;
                al.AddParam("elementUniqueID", "", currentNode.Attributes["UniqueID"].InnerText);
				if(currentNode.Name == "Model")
				{
					al.AddParam("elementType","",this.localizer.GetValue("Globals","Model"));
				}
				else
				{
					al.AddParam("elementType","",this.localizer.GetValue("Globals","Package"));
				}
				al.AddParam("actors","",this.localizer.GetValue("Globals","Actors"));
				al.AddParam("useCases","",this.localizer.GetValue("Globals","UseCases"));
				al.AddParam("packages","",this.localizer.GetValue("Globals","Packages"));
				al.AddParam("description","",this.localizer.GetValue("Globals","Description"));
				al.AddParam("notes","",this.localizer.GetValue("Globals","Notes"));
				al.AddParam("relatedDocs","",this.localizer.GetValue("Globals","RelatedDocuments"));
				al.AddParam("requirements","",this.localizer.GetValue("Globals","Requirements"));
			}
			if(currentNode.Name == "Actor")
			{
                HTMLFileName = currentNode.Attributes["UniqueID"].Value;
                al.AddParam("elementUniqueID", "", currentNode.Attributes["UniqueID"].InnerText);
				al.AddParam("elementType","",this.localizer.GetValue("Globals","Actor"));
				al.AddParam("description","",this.localizer.GetValue("Globals","Description"));
				al.AddParam("notes","",this.localizer.GetValue("Globals","Notes"));
				al.AddParam("relatedDocs","",this.localizer.GetValue("Globals","RelatedDocuments"));
				al.AddParam("goals","",this.localizer.GetValue("Globals","Goals"));
			}
			if(currentNode.Name == "UseCase")
			{
                HTMLFileName = currentNode.Attributes["UniqueID"].Value;
                al.AddParam("elementUniqueID", "", currentNode.Attributes["UniqueID"].InnerText);
				al.AddParam("statusNodeSet","",this.localizer.GetNodeSet("cmbStatus","Item"));
				al.AddParam("levelNodeSet","",this.localizer.GetNodeSet("cmbLevel","Item"));
				al.AddParam("complexityNodeSet","",this.localizer.GetNodeSet("cmbComplexity","Item"));
				al.AddParam("implementationNodeSet","",this.localizer.GetNodeSet("cmbImplementation","Item"));
				al.AddParam("historyTypeNodeSet","",this.localizer.GetNodeSet("HistoryType","Item"));
                al.AddParam("triggerEventNodeSet", "", this.localizer.GetNodeSet("cmbTriggerEvent", "Item"));

				al.AddParam("elementType","",this.localizer.GetValue("Globals","UseCase"));
				al.AddParam("preconditions","",this.localizer.GetValue("Globals","Preconditions"));
				al.AddParam("postconditions","",this.localizer.GetValue("Globals","Postconditions"));
				al.AddParam("openIssues","",this.localizer.GetValue("Globals","OpenIssues"));
				al.AddParam("flowOfEvents","",this.localizer.GetValue("Globals","FlowOfEvents"));
				al.AddParam("prose","",this.localizer.GetValue("Globals","Prose"));
				al.AddParam("details","",this.localizer.GetValue("Globals","Details"));
				al.AddParam("priority","",this.localizer.GetValue("Globals","Priority"));
				al.AddParam("status","",this.localizer.GetValue("Globals","Status"));
				al.AddParam("level","",this.localizer.GetValue("Globals","Level"));
				al.AddParam("complexity","",this.localizer.GetValue("Globals","Complexity"));
				al.AddParam("implementation","",this.localizer.GetValue("Globals","Implementation"));
				al.AddParam("assignedTo","",this.localizer.GetValue("Globals","AssignedTo"));
				al.AddParam("release","",this.localizer.GetValue("Globals","Release"));
				al.AddParam("activeActors","",this.localizer.GetValue("Globals","ActiveActors"));
				al.AddParam("primary","",this.localizer.GetValue("Globals","Primary"));
				al.AddParam("history","",this.localizer.GetValue("Globals","History"));
				al.AddParam("description","",this.localizer.GetValue("Globals","Description"));
				al.AddParam("notes","",this.localizer.GetValue("Globals","Notes"));
				al.AddParam("relatedDocs","",this.localizer.GetValue("Globals","RelatedDocuments"));
                al.AddParam("triggerEvent", "", this.localizer.GetValue("Globals", "TriggerEvent"));
                al.AddParam("triggerDescription", "", this.localizer.GetValue("Globals", "TriggerDescription"));
			}

			XslCompiledTransform transform = new XslCompiledTransform();
			transform.Load(this.stylesheetFilesPath + Path.DirectorySeparatorChar + xslFileName,null,resolver);
			XmlTextWriter tw = new XmlTextWriter(htmlFilesPath + Path.DirectorySeparatorChar + HTMLFileName + ".htm",Encoding.UTF8);
            XmlTextReader tr = new XmlTextReader(modelFilePath);
			transform.Transform(tr,al,tw,null);
            tr.Close();
			tw.Close();
		}
	}
}
