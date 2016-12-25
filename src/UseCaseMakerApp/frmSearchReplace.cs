using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UseCaseMakerControls;
using UseCaseMakerLibrary;
using UseCaseMakerLibrary.Support;

namespace UseCaseMaker
{
    public partial class frmSearchReplace : Form
    {
        private frmMain parent = null;
        private Control ctrl = null;
        private SearchBookmark bookmark = null;
        private Localizer localizer = null;
        private SearchBookmarkQueue bookmarks = new SearchBookmarkQueue();

        public frmSearchReplace(frmMain parent, Localizer localizer)
        {
            this.parent = parent;

            InitializeComponent();

            this.Reset();

            this.localizer = localizer;

            this.localizer.LocalizeControls(this);

            this.ImeMode = ImeMode.On;
        }

        private void Reset()
        {
            tbSearch.Text = String.Empty;
            tbReplace.Text = String.Empty;
            btnSearch.Enabled = false;
            btnSearchAgain.Enabled = false;
            btnReplace.Enabled = false;
            btnReplaceAll.Enabled = false;
            cbCaseSensitivity.Checked = false;
            cbWholeWords.Checked = false;
            cbElementOnly.Checked = false;
        }

        private void SetState()
        {
            if(bookmark != null)
            {
                btnSearch.Enabled = false;
                btnSearchAgain.Enabled = true;
                btnReplace.Enabled = true;
                btnReplaceAll.Enabled = true;
            }
            else
            {
                btnSearch.Enabled = true;
                btnSearchAgain.Enabled = false;
                btnReplace.Enabled = false;
                btnReplaceAll.Enabled = false;
            }

            if(tbSearch.Text.Length == 0)
            {
                btnSearch.Enabled = false;
            }
        }

        private void NextBookmark()
        {
            if(this.bookmarks.Count > 0)
            {
                bookmark = bookmarks.Dequeue();
                ctrl = parent.ShowTarget(
                    bookmark.Element,
                    bookmark.PropertyName,
                    bookmark.Index,
                    bookmark.Start,
                    bookmark.Length);
            }
            else
            {
                MessageBox.Show(
                    this.localizer.GetValue("UserMessages", "searchCompleted"),
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                bookmark = null;
                this.SetState();
            }
        }

        private void Execute(Boolean replace)
        {
            bookmarks.Clear();
            object element = null;

            if(cbElementOnly.Checked)
            {
                element = parent.GetActiveWindowElement();
            }

            if(element == null)
            {
                element = parent.Model;
            }

            if(element.GetType() == typeof(Model))
            {
                ((Model)element).TextSearch(
                    bookmarks,
                    tbSearch.Text,
                    tbReplace.Text,
                    cbWholeWords.Checked,
                    cbCaseSensitivity.Checked,
                    replace);
            }
            if(element.GetType() == typeof(Package))
            {
                ((Package)element).TextSearch(
                    bookmarks,
                    tbSearch.Text,
                    tbReplace.Text,
                    cbWholeWords.Checked,
                    cbCaseSensitivity.Checked,
                    replace);
            }
            if(element.GetType() == typeof(Actor))
            {
                ((Actor)element).TextSearch(
                    bookmarks,
                    tbSearch.Text,
                    tbReplace.Text,
                    cbWholeWords.Checked,
                    cbCaseSensitivity.Checked,
                    replace);
            }
            if(element.GetType() == typeof(UseCase))
            {
                ((UseCase)element).TextSearch(
                    bookmarks,
                    tbSearch.Text,
                    tbReplace.Text,
                    cbWholeWords.Checked,
                    cbCaseSensitivity.Checked,
                    replace);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.Hide();
            this.Reset();
            this.bookmarks.Clear();
            e.Cancel = true;
            base.OnClosing(e);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.Execute(false);

            this.NextBookmark();

            this.SetState();
        }

        private void btnSearchAgain_Click(object sender, EventArgs e)
        {
            this.NextBookmark();
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            String what;

            if(ctrl != null && bookmark != null)
            {
                if(ctrl is IndexedListItem)
                {
                    what = ((IndexedListItem)ctrl).SelectedText;
                }
                else
                {
                    what = ((LinkEnabledRTB)ctrl).SelectedText;
                }

                if(what.Equals(
                    tbSearch.Text,
                    ((cbCaseSensitivity.Checked)
                        ? StringComparison.CurrentCulture
                        : StringComparison.CurrentCultureIgnoreCase)) == true)
                {
                    if(ctrl is IndexedListItem)
                    {
                        ((IndexedListItem)ctrl).ReplaceText(tbReplace.Text);
                    }
                    else
                    {
                        ((LinkEnabledRTB)ctrl).SelectedText = tbReplace.Text;
                    }

                    // TextChanged event raised by controls already set this flag
                    // parent.SetModified(true);
                }
            }
            this.NextBookmark();
        }

        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            parent.Cursor = Cursors.WaitCursor;

            this.Execute(true);

            parent.UpdateAllDocumentViews();

            parent.Cursor = Cursors.Default;

            // Replace operation is executed at business level.
            // Controls are not involved in replacement,
            // so we need to set manually this flag.
            parent.SetModified(true);

            MessageBox.Show(
                this.localizer.GetValue("UserMessages", "replaceCompleted"),
                Application.ProductName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            bookmark = null;
            this.SetState();
        }

        private void cbWholeWords_CheckedChanged(object sender, EventArgs e)
        {
            bookmark = null;
            this.SetState();
        }

        private void cbCaseSensitivity_CheckedChanged(object sender, EventArgs e)
        {
            bookmark = null;
            this.SetState();
        }

        private void cbElementOnly_CheckedChanged(object sender, EventArgs e)
        {
            bookmark = null;
            this.SetState();
        }
    }
}