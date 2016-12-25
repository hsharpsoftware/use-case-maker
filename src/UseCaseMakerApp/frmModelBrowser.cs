using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UseCaseMakerLibrary;
using UseCaseMakerControls;
using WeifenLuo.WinFormsUI.Docking;

namespace UseCaseMaker
{
    public partial class frmModelBrowser : DockContent
    {
        private frmMain parent;
        private Timer tmrNodeAutoExpand = new Timer();
        private Point mouseOverNodePoint;

        private Boolean nodeDoubleClicked;

        public frmModelBrowser(
            frmMain parent,
            ImageList imgList,
            ContextMenu ctxMenu,
            Localizer localizer)
        {
            this.parent = parent;
            InitializeComponent();

            tvModelBrowser.ImageList = imgList;
            tvModelBrowser.ContextMenu = ctxMenu;

            // Create handler for treeview node auto expand
            tmrNodeAutoExpand.Tick += new EventHandler(OnNodeAutoExpandTest);
            tmrNodeAutoExpand.Interval = 750;
            tmrNodeAutoExpand.Enabled = false;

            localizer.LocalizeControls(this);

            this.TabText = this.Text;

            this.ImeMode = ImeMode.On;
        }

        /**
         * @brief Gestore del timer per l'espansione automatica di
         * un nodo nella TreeView durante il drag and drop
         */
        private void OnNodeAutoExpandTest(object source, System.EventArgs e)
        {
            TreeNode node = null;

            this.mouseOverNodePoint = tvModelBrowser.PointToClient(this.mouseOverNodePoint);
            node = tvModelBrowser.GetNodeAt(this.mouseOverNodePoint);
            if (node != null)
            {
                if (!node.IsExpanded)
                {
                    node.Expand();
                }
            }

            tmrNodeAutoExpand.Stop();
        }

        private void tvModelBrowser_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            parent.OnEditableStateChanged(sender, new ItemTextChangedEventArgs(null));
        }

        private void tvModelBrowser_ItemDrag(object sender, ItemDragEventArgs e)
        {
            TreeNode node = (TreeNode)e.Item;

            if(e.Button != MouseButtons.Left)
            {
                return;
            }

            if(parent.Model.FindElementByUniqueID((String)node.Tag).GetType() == typeof(Model))
            {
                ((TreeView)sender).DoDragDrop(e.Item, DragDropEffects.None);
                return;
            }
            ((TreeView)sender).SelectedNode = node;
            ((TreeView)sender).DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void tvModelBrowser_KeyDown(object sender, KeyEventArgs e)
        {
            TreeNode dstNode = tvModelBrowser.SelectedNode;

            if((e.Control && e.KeyCode == Keys.X) || (e.Shift && e.KeyCode == Keys.Delete))
            {
                if (parent.Model.FindElementByUniqueID((String)dstNode.Tag).GetType() == typeof(Model))
                {
                    return;
                }

                parent.ModelBrowserCut();
            }
            if((e.Control && e.KeyCode == Keys.V) || (e.Shift && e.KeyCode == Keys.Insert))
            {
                TreeNode srcNode;
                object srcElement, dstElement;

                if (Clipboard.GetDataObject().GetDataPresent(typeof(TreeNode)))
                {
                    srcNode = (TreeNode)Clipboard.GetDataObject().GetData(typeof(TreeNode));
                    srcElement = parent.Model.FindElementByUniqueID((String)srcNode.Tag);
                    dstElement = parent.Model.FindElementByUniqueID((String)dstNode.Tag);

                    parent.ModelBrowserPaste(dstElement, srcElement);
                }
            }
            if(e.KeyCode == Keys.Enter)
            {
                parent.OnEditElement(sender);
                e.SuppressKeyPress = true;
            }
            if(e.KeyCode == Keys.Add && !dstNode.IsExpanded)
            {
                dstNode.Expand();
                e.SuppressKeyPress = true;
            }
            if(e.KeyCode == Keys.Subtract && dstNode.IsExpanded)
            {
                dstNode.Collapse();
                e.SuppressKeyPress = true;
            }
        }

        // TODO: should this be transformed into a mouse double click event?
        private void tvModelBrowser_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode tv = (TreeNode)tvModelBrowser.GetNodeAt(e.X, e.Y);
                if (tv != null)
                {
                    tvModelBrowser.SelectedNode = tv;
                }
            }

            if (e.Clicks == 2)
            {
                nodeDoubleClicked = true;
            }
        }

        private void tvModelBrowser_Enter(object sender, EventArgs e)
        {
            parent.OnEditableStateChanged(sender, new ItemTextChangedEventArgs(null));
        }

        private void tvModelBrowser_DragDrop(object sender, DragEventArgs e)
        {
			if(!e.Data.GetDataPresent(typeof(TreeNode)))
			{
				return;
			}

			if(e.Effect == DragDropEffects.None)
			{
				return;
			}

			TreeNode srcNode, dstNode;
			object srcElement, dstElement;

			srcNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
			srcElement = parent.Model.FindElementByUniqueID((String)srcNode.Tag);

			Point pt = new Point(e.X,e.Y);
			pt = tvModelBrowser.PointToClient(pt);
			dstNode = tvModelBrowser.GetNodeAt(pt);
			if(dstNode != null)
			{
                dstElement = parent.Model.FindElementByUniqueID((String)dstNode.Tag);
                parent.ModelBrowserPaste(dstElement, srcElement);
			}
        }

        private void tvModelBrowser_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(TreeNode)))
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            TreeNode srcNode, dstNode;
            Type srcType, dstType;

            srcNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
            srcType = parent.Model.FindElementByUniqueID((String)srcNode.Tag).GetType();

            Point pt = new Point(e.X, e.Y);
            pt = tvModelBrowser.PointToClient(pt);
            dstNode = tvModelBrowser.GetNodeAt(pt);
            if (dstNode != null)
            {
                dstType = parent.Model.FindElementByUniqueID((String)dstNode.Tag).GetType();
                if (dstType == typeof(Package) || dstType == typeof(Model))
                {
                    if(srcType == typeof(Package))
                    {
                        e.Effect = DragDropEffects.Move;
                    }
                    else
                    {
                        e.Effect = DragDropEffects.None;
                    }
                    tmrNodeAutoExpand.Start();
                    this.mouseOverNodePoint = new Point(e.X, e.Y);
                }
                else if(dstType == typeof(Actors))
                {
                    if (srcType == typeof(Actors)
                        || srcType == typeof(Actor))
                    {
                        e.Effect = DragDropEffects.Move;
                    }
                    else
                    {
                        e.Effect = DragDropEffects.None;
                    }
                }
                else if(dstType == typeof(UseCases))
                {
                    if (srcType == typeof(UseCases)
                        || srcType == typeof(UseCase))
                    {
                        e.Effect = DragDropEffects.Move;
                    }
                    else
                    {
                        e.Effect = DragDropEffects.None;
                    }
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
        }

        private void tvModelBrowser_DoubleClick(object sender, EventArgs e)
        {
            parent.OnEditElement(sender);
        }

        private void tvModelBrowser_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if(nodeDoubleClicked)
            {
                nodeDoubleClicked = false;
                e.Cancel = true;
            }
        }

        private void tvModelBrowser_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if(nodeDoubleClicked)
            {
                nodeDoubleClicked = false;
                e.Cancel = true;
            }
        }
    }
}