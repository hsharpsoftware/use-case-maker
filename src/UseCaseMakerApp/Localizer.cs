using System;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace UseCaseMaker
{
	/// <summary>
	/// Descrizione di riepilogo per Localizer.
	/// </summary>
	public class Localizer
	{
		#region Class Memebers
		private XmlDocument langDoc = null;
		#endregion

		#region Constructors
		public Localizer()
		{
		}
		#endregion

		#region Public Properties
		public XmlDocument LanguageDocument
		{
			get
			{
				return this.langDoc;
			}
		}
		#endregion

		#region Public Methods
		public void Load(string languageFilePath)
		{
			this.langDoc = new XmlDocument();
			this.langDoc.Load(languageFilePath);
		}

		public string GetValue(string section, string name)
		{
			XmlNode node = this.langDoc.SelectSingleNode("//" + section + "/" +name);
			if(node == null)
			{
				return string.Empty;
			}

			return this.FormatText(node.InnerText);
		}

		public XPathNodeIterator GetNodeSet(string parentName, string childsName)
		{
			XPathNavigator nav = this.langDoc.CreateNavigator();
			XPathNodeIterator ni = nav.Select("//" + parentName + "/" + childsName);
			return ni;
		}

		public void LocalizeControls(Form form)
		{
			XmlNode controlsNode = this.langDoc.SelectSingleNode("//" + form.Name + "/Controls");
			if(controlsNode == null)
			{
				return;
			}
			foreach(XmlNode node in controlsNode.ChildNodes)
			{
				if(node.HasChildNodes && node.FirstChild.NodeType != XmlNodeType.Text)
				{
					FieldInfo fi = form.GetType().GetField(
						node.Name,
						BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
					PropertyInfo items = fi.FieldType.GetProperty("Items");
					MethodInfo clear = items.PropertyType.GetMethod("Clear");
					clear.Invoke(items.GetValue(fi.GetValue(form),null),null);
					foreach(XmlNode itemNode in node.ChildNodes)
					{
						MethodInfo add = items.PropertyType.GetMethod("Add");
						object [] addParams = new object[1];
						addParams[0] = itemNode.InnerText;
						add.Invoke(items.GetValue(fi.GetValue(form),null),addParams);
					}
					if(node.Attributes["ToolTipText"] != null)
					{
						PropertyInfo pi = fi.FieldType.GetProperty("ToolTipText");
						pi.SetValue(
                            fi.GetValue(form),
                            this.FormatText(node.Attributes["ToolTipText"].Value),
                            null);
					}
				}
				else if(node.NodeType != XmlNodeType.Comment)
				{
					if(node.InnerText != string.Empty)
					{
						if(node.Name.ToUpper() == "SELF")
						{
							form.Text = node.InnerText;
						}
						else
						{
							FieldInfo fi = form.GetType().GetField(
								node.Name,
								BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
							PropertyInfo pi = fi.FieldType.GetProperty("Text");
							pi.SetValue(
                                fi.GetValue(form),
                                this.FormatText(node.InnerText),
                                null);
						}
					}
					if(node.Attributes["ToolTipText"] != null)
					{
						FieldInfo fi = form.GetType().GetField(
							node.Name,
							BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
						PropertyInfo pi = fi.FieldType.GetProperty("ToolTipText");
						pi.SetValue(
                            fi.GetValue(form),
                            this.FormatText(node.Attributes["ToolTipText"].Value),
                            null);
					}
                    if(node.Attributes["ref_form"] != null && node.Attributes["ref_control"] != null)
                    {
					    FieldInfo fi = form.GetType().GetField(
						    node.Name,
						    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                        this.LocalizeReferencedControl(
                            node.Attributes["ref_form"].Value,
                            node.Attributes["ref_control"].Value,
                            form,
                            fi);
                    }
				}
			}
		}

        private void LocalizeReferencedControl(String refFormName, String refControlName, Form form, FieldInfo fi)
        {
            XmlNode node = this.langDoc.SelectSingleNode("//" + refFormName + "/Controls/" + refControlName);
            if(node == null)
            {
                return;
            }
            if(node.HasChildNodes && node.FirstChild.NodeType != XmlNodeType.Text)
            {
                PropertyInfo items = fi.FieldType.GetProperty("Items");
                MethodInfo clear = items.PropertyType.GetMethod("Clear");
                clear.Invoke(items.GetValue(fi.GetValue(form), null), null);
                foreach(XmlNode itemNode in node.ChildNodes)
                {
                    MethodInfo add = items.PropertyType.GetMethod("Add");
                    object[] addParams = new object[1];
                    addParams[0] = itemNode.InnerText;
                    add.Invoke(items.GetValue(fi.GetValue(form), null), addParams);
                }
                if(node.Attributes["ToolTipText"] != null)
                {
                    PropertyInfo pi = fi.FieldType.GetProperty("ToolTipText");
                    pi.SetValue(
                        fi.GetValue(form),
                        this.FormatText(node.Attributes["ToolTipText"].Value),
                        null);
                }
            }
            else if(node.NodeType != XmlNodeType.Comment)
            {
                if(node.InnerText != string.Empty)
                {
                    if(node.Name.ToUpper() != "SELF")
                    {
                        PropertyInfo pi = fi.FieldType.GetProperty("Text");
                        pi.SetValue(fi.GetValue(form), node.InnerText, null);
                    }
                }
                if(node.Attributes["ToolTipText"] != null)
                {
                    PropertyInfo pi = fi.FieldType.GetProperty("ToolTipText");
                    pi.SetValue(
                        fi.GetValue(form),
                        this.FormatText(node.Attributes["ToolTipText"].Value),
                        null);
                }
                if(node.Attributes["ref_form"] != null && node.Attributes["ref_control"] != null)
                {
                    this.LocalizeReferencedControl(
                        node.Attributes["ref_form"].Value,
                        node.Attributes["ref_control"].Value,
                        form,
                        fi);
                }
            }
        }

        private String FormatText(String source)
        {
            // Remove new line in multi line XML node
            StringBuilder sb = new StringBuilder();
            String text;
            String[] ss;
            Char[] cs = { ' ', '\t' };

            ss = source.Split(Environment.NewLine.ToCharArray());
            foreach(String s in ss)
            {
                // Format text with new line when reqiured
                text = s.Replace("[nl]", Environment.NewLine);
                text = text.TrimStart(cs);
                if(text != String.Empty)
                {
                    sb.Append(text);
                }
            }

            return sb.ToString();
        }
		#endregion
	}
}
