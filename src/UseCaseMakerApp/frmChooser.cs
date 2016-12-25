using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UseCaseMakerLibrary;

namespace UseCaseMaker
{
    public partial class frmChooser : Form
    {
        private Model model;
        private object selected;
        private Boolean showActors = false;
        private Boolean showUseCases = false;
        private Boolean packageSelectionIsValid = false;
        private Boolean actorSelectionIsValid = false;
        private Boolean useCaseSelectionIsValid = false;
        private Localizer localizer;

        public frmChooser(
            Model model,
            Localizer localizer,
            String elementType)
        {
            InitializeComponent();

			this.model = model;
			this.localizer = localizer;
			this.localizer.LocalizeControls(this);
            this.Text += ": " + elementType;
			
            this.selected = null;
			this.btnOK.Enabled = false;
			this.tvModelBrowser.SelectedNode = null;
        }

        public object SelectedElement
        {
            get
            {
                return this.selected;
            }
        }

        public Boolean ShowActors
        {
            get
            {
                return this.showActors;
            }
            set
            {
                this.showActors = value;
            }
        }

        public Boolean ShowUseCases
        {
            get
            {
                return this.showUseCases;
            }
            set
            {
                this.showUseCases = value;
            }
        }

        public Boolean PackageSelectionIsValid
        {
            get
            {
                return this.packageSelectionIsValid;
            }
            set
            {
                this.packageSelectionIsValid = value;
            }
        }

        public Boolean ActorSelectionIsValid
        {
            get
            {
                return this.actorSelectionIsValid;
            }
            set
            {
                this.actorSelectionIsValid = value;
            }
        }

        public Boolean UseCaseSelectionIsValid
        {
            get
            {
                return this.useCaseSelectionIsValid;
            }
            set
            {
                this.useCaseSelectionIsValid = value;
            }
        }

        public void Initialize()
        {
            BuildView(this.model);
            this.tvModelBrowser.SelectedNode = this.tvModelBrowser.Nodes[0];
        }

        private void BuildView(object element)
        {
            Package package = (Package)element;
            if(element.GetType() == typeof(Model))
            {
                AddElement(package, null);
            }
            else
            {
                AddElement(package, package.Owner);
            }
            if(this.showUseCases)
            {
                foreach(UseCase useCase in package.UseCases.Sorted("ID"))
                {
                    useCase.Owner = package;
                    AddElement(useCase, useCase.Owner);
                }
            }
            if(this.showActors)
            {
                foreach(Actor actor in package.Actors.Sorted("ID"))
                {
                    actor.Owner = package;
                    AddElement(actor, actor.Owner);
                }
            }
            foreach(Package subPackage in package.Packages.Sorted("ID"))
            {
                subPackage.Owner = package;
                BuildView(subPackage);
            }
        }

        private void AddElement(object element, object owner)
        {
            String ownerUniqueID = String.Empty;

            if(element.GetType() == typeof(Model))
            {
                Model model = (Model)element;
                tvModelBrowser.Nodes.Clear();
                TreeNode node = new TreeNode(model.Name + " (" + model.ElementID + ")");
                node.Tag = model.UniqueID;
                node.ToolTipText = model.Attributes.Description;
                tvModelBrowser.Nodes.Add(node);
                tvModelBrowser.SelectedNode = node;
                TreeNode ownerNode = node;
                if(this.showUseCases)
                {
                    node = new TreeNode(this.localizer.GetValue("Globals", "UseCases"), 1, 1);
                    node.Tag = model.UseCases.UniqueID;
                    ownerNode.Nodes.Add(node);
                }
                if(this.showActors)
                {
                    node = new TreeNode(this.localizer.GetValue("Globals", "Actors"), 3, 3);
                    node.Tag = model.Actors.UniqueID;
                    ownerNode.Nodes.Add(node);
                }
            }

            if(element.GetType() == typeof(Package))
            {
                Package package = (Package)element;
                ownerUniqueID = ((Package)owner).UniqueID;
                TreeNode node = new TreeNode(package.Name + " (" + package.ElementID + ")");
                node.Tag = package.UniqueID;
                node.ToolTipText = package.Attributes.Description;
                TreeNode ownerNode = this.FindNode(null, ownerUniqueID);
                if(ownerNode != null)
                {
                    ownerNode.Nodes.Add(node);
                    tvModelBrowser.SelectedNode = node;
                    ownerNode = node;
                    if(this.showUseCases)
                    {
                        node = new TreeNode(this.localizer.GetValue("Globals", "UseCases"), 1, 1);
                        node.Tag = package.UseCases.UniqueID;
                        ownerNode.Nodes.Add(node);
                    }
                    if(this.showActors)
                    {
                        node = new TreeNode(this.localizer.GetValue("Globals", "Actors"), 3, 3);
                        node.Tag = package.Actors.UniqueID;
                        ownerNode.Nodes.Add(node);
                    }
                }
            }
            if(element.GetType() == typeof(UseCase))
            {
                UseCase useCase = (UseCase)element;
                Package package = (Package)owner;
                TreeNode node = new TreeNode(useCase.Name + " (" + useCase.ElementID + ")", 2, 2);
                node.Tag = useCase.UniqueID;
                node.ToolTipText = useCase.Attributes.Description;
                TreeNode ownerNode = this.FindNode(null, package.UniqueID);
                if(ownerNode != null)
                {
                    foreach(TreeNode subNode in ownerNode.Nodes)
                    {
                        if((String)subNode.Tag == package.UseCases.UniqueID)
                        {
                            subNode.Nodes.Add(node);
                            tvModelBrowser.SelectedNode = node;
                            break;
                        }
                    }
                }
            }
            if(element.GetType() == typeof(Actor))
            {
                Actor actor = (Actor)element;
                Package package = (Package)owner;
                TreeNode node = new TreeNode(actor.Name + " (" + actor.ElementID + ")", 4, 4);
                node.Tag = actor.UniqueID;
                node.ToolTipText = actor.Attributes.Description;
                TreeNode ownerNode = this.FindNode(null, package.UniqueID);
                if(ownerNode != null)
                {
                    foreach(TreeNode subNode in ownerNode.Nodes)
                    {
                        if((String)subNode.Tag == package.Actors.UniqueID)
                        {
                            subNode.Nodes.Add(node);
                            tvModelBrowser.SelectedNode = node;
                            break;
                        }
                    }
                }
            }
        }

        private TreeNode FindNode(TreeNode parent, String parentUniqueID)
        {
            TreeNode node = null;
            TreeNode retNode = null;

            if(tvModelBrowser.Nodes.Count == 0)
            {
                return null;
            }

            if(parent == null)
            {
                node = tvModelBrowser.Nodes[0];
            }
            else
            {
                node = parent;
            }

            if((String)node.Tag == parentUniqueID)
            {
                return node;
            }

            foreach(TreeNode child in node.Nodes)
            {
                if((String)child.Tag == parentUniqueID)
                {
                    retNode = child;
                    break;
                }
                if(child.Nodes.Count > 0)
                {
                    retNode = this.FindNode(child, parentUniqueID);
                    if(retNode != null)
                    {
                        break;
                    }
                }
            }

            return retNode;
        }

        private void tvModelBrowser_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = tvModelBrowser.SelectedNode;
            object element = model.FindElementByUniqueID((String)node.Tag);

            btnOK.Enabled = false;
            this.selected = null;

            if(element.GetType() == typeof(Package) || element.GetType() == typeof(Model))
            {
                if(this.packageSelectionIsValid)
                {
                    selected = element;
                    this.btnOK.Enabled = true;
                }
            }
            if(element.GetType() == typeof(UseCase))
            {
                if(this.useCaseSelectionIsValid)
                {
                    selected = element;
                    this.btnOK.Enabled = true;
                }
            }
            if(element.GetType() == typeof(Actor))
            {
                if(this.actorSelectionIsValid)
                {
                    selected = element;
                    this.btnOK.Enabled = true;
                }
            }
        }
    }
}