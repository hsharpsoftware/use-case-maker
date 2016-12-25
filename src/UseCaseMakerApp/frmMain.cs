using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using UseCaseMakerLibrary;
using UseCaseMakerControls;
using WeifenLuo.WinFormsUI.Docking;

/**
 * @defgroup user_interface Interfaccia utente
 */

namespace UseCaseMaker
{
	/**
	 * @brief Finestra principale
	 */
	public class frmMain : System.Windows.Forms.Form
	{
		public const string defaultUCPrefix = "UC";
        public const string defaultPPrefix = "P";
        public const string defaultAPrefix = "A";
        public const string defaultMPrefix = "M";
        public const string defaultGPrefix = "G";
        public const string defaultSPrefix = "S";

        private frmModelBrowser frmModelBrowser = null;
        private frmSearchReplace frmSearchReplace = null;
        private Model model = null;
		private object currentElement = null;
		private SepararatorCollection separators = new SepararatorCollection();
		private HighLightDescriptorCollection hdc = new HighLightDescriptorCollection();
		private string modelFilePath = string.Empty;
		private string modelFileName = string.Empty;
		private bool modified = false;
		private bool modifiedLocked = false;
        private bool nodeToolTipIsUpdating = false;
		private Localizer localizer = new Localizer();
		private ApplicationSettings appSettings = new ApplicationSettings();

        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.ToolBar toolBar;
        private System.Windows.Forms.StatusBar statusBar;
		private System.Windows.Forms.MenuItem mnuFile;
		private System.Windows.Forms.MenuItem mnuFileNew;
		private System.Windows.Forms.MenuItem mnuFileOpen;
		private System.Windows.Forms.MenuItem mnuFileSep1;
		private System.Windows.Forms.MenuItem mnuFileSave;
		private System.Windows.Forms.MenuItem mnuFileSep2;
		private System.Windows.Forms.MenuItem mnuFileExit;
		private System.Windows.Forms.MenuItem mnuEdit;
		private System.Windows.Forms.MenuItem mnuHelp;
        private System.Windows.Forms.MenuItem mnuHelpAbout;
		private System.Windows.Forms.ToolBarButton tbBtnNew;
		private System.Windows.Forms.ToolBarButton tbBtnOpen;
		private System.Windows.Forms.ToolBarButton tbBtnSave;
		private System.Windows.Forms.ToolBarButton tbBtnAddActor;
		private System.Windows.Forms.ToolBarButton tbBtnRemoveActor;
		private System.Windows.Forms.ToolBarButton tbBtnAddUseCase;
		private System.Windows.Forms.ToolBarButton tbBtnRemoveUseCase;
		private System.Windows.Forms.ImageList imgListToolBar;
        private System.Windows.Forms.ImageList imgListModelBrowser;
		private System.Windows.Forms.ToolBarButton tbBtnAddPackage;
		private System.Windows.Forms.ToolBarButton tbBtnRemovePackage;
		private System.Windows.Forms.OpenFileDialog openModelFileDialog;
        private System.Windows.Forms.SaveFileDialog saveModelFileDialog;
		private System.Windows.Forms.MenuItem mnuEditRemovePackage;
		private System.Windows.Forms.MenuItem mnuEditRemoveUseCase;
		private System.Windows.Forms.MenuItem mnuFileSaveAs;
		private System.Windows.Forms.MenuItem mnuEditAddUseCase;
		private System.Windows.Forms.MenuItem mnuEditAddActor;
		private System.Windows.Forms.MenuItem mnuEditAddPackage;
        private System.Windows.Forms.MenuItem mnuEditRemoveActor;
		private System.Windows.Forms.ContextMenu modelBrowserCtxMenu;
		private System.Windows.Forms.MenuItem mnuCtxMBAddPackage;
		private System.Windows.Forms.MenuItem mnuCtxMBRemovePackage;
		private System.Windows.Forms.MenuItem mnuCtxMBAddActor;
		private System.Windows.Forms.MenuItem mnuCtxMBRemoveActor;
		private System.Windows.Forms.MenuItem mnuCtxMBAddUseCase;
        private System.Windows.Forms.MenuItem mnuCtxMBRemoveUseCase;
		private System.Windows.Forms.MenuItem mnuTools;
		private System.Windows.Forms.MenuItem mnuToolsHtmlExport;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		private System.Windows.Forms.MenuItem mnuFileRecent1;
		private System.Windows.Forms.MenuItem mnuFileRecent2;
		private System.Windows.Forms.MenuItem mnuFileRecent3;
		private System.Windows.Forms.MenuItem mnuFileRecent4;
        private System.Windows.Forms.MenuItem mnuFileSep3;
		private System.Windows.Forms.MenuItem mnuToolsSep1;
		private System.Windows.Forms.MenuItem mnuToolsOptions;
		private System.Windows.Forms.MenuItem mnuToolsSep2;
		private System.Windows.Forms.MenuItem mnuToolsXMIExport;
        private System.Windows.Forms.MenuItem mnuToolsPDFExport;
		private System.Windows.Forms.MenuItem mnuCtxETGoToDefinition;
		private System.Windows.Forms.ContextMenu elementTokenCtxMenu;
        private System.Windows.Forms.MenuItem mnuToolsRTFExport;
		private System.Windows.Forms.MenuItem mnuEditSep3;
		private System.Windows.Forms.MenuItem mnuEditSep4;
		private System.Windows.Forms.MenuItem mnuEditCut;
		private System.Windows.Forms.MenuItem mnuEditCopy;
		private System.Windows.Forms.MenuItem menuEditSep2;
		private System.Windows.Forms.MenuItem mnuEditPaste;
		private System.Windows.Forms.ToolBarButton tbBtnSep2;
		private System.Windows.Forms.ToolBarButton tbBtnSep3;
		private System.Windows.Forms.ToolBarButton tbBtnSep4;
		private System.Windows.Forms.ToolBarButton tbBtnCut;
		private System.Windows.Forms.ToolBarButton tbBtnCopy;
		private System.Windows.Forms.ToolBarButton tbBtnPaste;
		private System.Windows.Forms.ToolBarButton tbBtnSep1;
		private System.Windows.Forms.MenuItem mnuCtxMBSep2;
		private System.Windows.Forms.MenuItem mnuCtxMBSep3;
		private System.Windows.Forms.MenuItem mnuCtxMBCut;
		private System.Windows.Forms.MenuItem mnuCtxMBCopy;
		private System.Windows.Forms.MenuItem mnuCtxMBPaste;
		private System.Windows.Forms.MenuItem mnuCtxMBSep1;
		private System.Windows.Forms.MenuItem mnuCtxETSep1;
		private System.Windows.Forms.MenuItem mnuCtxETCut;
		private System.Windows.Forms.MenuItem mnuCtxETCopy;
        private System.Windows.Forms.MenuItem mnuCtxETPaste;
		private System.Windows.Forms.MenuItem mnuEditSep5;
		private System.Windows.Forms.MenuItem mnuEditReorderElements;
		private System.Windows.Forms.MenuItem mnuCtxMBSep4;
        private System.Windows.Forms.MenuItem mnuCtxMBReorderElements;
		private System.Windows.Forms.ImageList imgListSteps;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
        private MenuItem menuEditSep1;
        private MenuItem mnuEditSearchReplace;
		private System.ComponentModel.IContainer components;

		public frmMain(string openFromCmdLine)
		{
			//
			// Necessario per il supporto di Progettazione Windows Form
			//
			Application.EnableVisualStyles();
			Application.DoEvents();
			InitializeComponent();
			
			//
			// TODO: aggiungere il codice del costruttore dopo la chiamata a InitializeComponent
			//
			if(openFromCmdLine != string.Empty)
			{
				this.modelFilePath = Path.GetDirectoryName(openFromCmdLine);
				this.modelFileName = Path.GetFileName(openFromCmdLine);
			}
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Codice generato da Progettazione Windows Form
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.mnuFile = new System.Windows.Forms.MenuItem();
            this.mnuFileNew = new System.Windows.Forms.MenuItem();
            this.mnuFileOpen = new System.Windows.Forms.MenuItem();
            this.mnuFileSep1 = new System.Windows.Forms.MenuItem();
            this.mnuFileSave = new System.Windows.Forms.MenuItem();
            this.mnuFileSaveAs = new System.Windows.Forms.MenuItem();
            this.mnuFileSep2 = new System.Windows.Forms.MenuItem();
            this.mnuFileRecent1 = new System.Windows.Forms.MenuItem();
            this.mnuFileRecent2 = new System.Windows.Forms.MenuItem();
            this.mnuFileRecent3 = new System.Windows.Forms.MenuItem();
            this.mnuFileRecent4 = new System.Windows.Forms.MenuItem();
            this.mnuFileSep3 = new System.Windows.Forms.MenuItem();
            this.mnuFileExit = new System.Windows.Forms.MenuItem();
            this.mnuEdit = new System.Windows.Forms.MenuItem();
            this.mnuEditCut = new System.Windows.Forms.MenuItem();
            this.mnuEditCopy = new System.Windows.Forms.MenuItem();
            this.mnuEditPaste = new System.Windows.Forms.MenuItem();
            this.menuEditSep1 = new System.Windows.Forms.MenuItem();
            this.mnuEditSearchReplace = new System.Windows.Forms.MenuItem();
            this.menuEditSep2 = new System.Windows.Forms.MenuItem();
            this.mnuEditAddPackage = new System.Windows.Forms.MenuItem();
            this.mnuEditRemovePackage = new System.Windows.Forms.MenuItem();
            this.mnuEditSep3 = new System.Windows.Forms.MenuItem();
            this.mnuEditAddActor = new System.Windows.Forms.MenuItem();
            this.mnuEditRemoveActor = new System.Windows.Forms.MenuItem();
            this.mnuEditSep4 = new System.Windows.Forms.MenuItem();
            this.mnuEditAddUseCase = new System.Windows.Forms.MenuItem();
            this.mnuEditRemoveUseCase = new System.Windows.Forms.MenuItem();
            this.mnuEditSep5 = new System.Windows.Forms.MenuItem();
            this.mnuEditReorderElements = new System.Windows.Forms.MenuItem();
            this.mnuTools = new System.Windows.Forms.MenuItem();
            this.mnuToolsHtmlExport = new System.Windows.Forms.MenuItem();
            this.mnuToolsPDFExport = new System.Windows.Forms.MenuItem();
            this.mnuToolsRTFExport = new System.Windows.Forms.MenuItem();
            this.mnuToolsSep1 = new System.Windows.Forms.MenuItem();
            this.mnuToolsXMIExport = new System.Windows.Forms.MenuItem();
            this.mnuToolsSep2 = new System.Windows.Forms.MenuItem();
            this.mnuToolsOptions = new System.Windows.Forms.MenuItem();
            this.mnuHelp = new System.Windows.Forms.MenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.MenuItem();
            this.modelBrowserCtxMenu = new System.Windows.Forms.ContextMenu();
            this.mnuCtxMBCut = new System.Windows.Forms.MenuItem();
            this.mnuCtxMBCopy = new System.Windows.Forms.MenuItem();
            this.mnuCtxMBPaste = new System.Windows.Forms.MenuItem();
            this.mnuCtxMBSep1 = new System.Windows.Forms.MenuItem();
            this.mnuCtxMBAddPackage = new System.Windows.Forms.MenuItem();
            this.mnuCtxMBRemovePackage = new System.Windows.Forms.MenuItem();
            this.mnuCtxMBSep2 = new System.Windows.Forms.MenuItem();
            this.mnuCtxMBAddActor = new System.Windows.Forms.MenuItem();
            this.mnuCtxMBRemoveActor = new System.Windows.Forms.MenuItem();
            this.mnuCtxMBSep3 = new System.Windows.Forms.MenuItem();
            this.mnuCtxMBAddUseCase = new System.Windows.Forms.MenuItem();
            this.mnuCtxMBRemoveUseCase = new System.Windows.Forms.MenuItem();
            this.mnuCtxMBSep4 = new System.Windows.Forms.MenuItem();
            this.mnuCtxMBReorderElements = new System.Windows.Forms.MenuItem();
            this.imgListModelBrowser = new System.Windows.Forms.ImageList(this.components);
            this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.toolBar = new System.Windows.Forms.ToolBar();
            this.tbBtnNew = new System.Windows.Forms.ToolBarButton();
            this.tbBtnOpen = new System.Windows.Forms.ToolBarButton();
            this.tbBtnSave = new System.Windows.Forms.ToolBarButton();
            this.tbBtnSep1 = new System.Windows.Forms.ToolBarButton();
            this.tbBtnCut = new System.Windows.Forms.ToolBarButton();
            this.tbBtnCopy = new System.Windows.Forms.ToolBarButton();
            this.tbBtnPaste = new System.Windows.Forms.ToolBarButton();
            this.tbBtnSep2 = new System.Windows.Forms.ToolBarButton();
            this.tbBtnAddPackage = new System.Windows.Forms.ToolBarButton();
            this.tbBtnRemovePackage = new System.Windows.Forms.ToolBarButton();
            this.tbBtnSep3 = new System.Windows.Forms.ToolBarButton();
            this.tbBtnAddActor = new System.Windows.Forms.ToolBarButton();
            this.tbBtnRemoveActor = new System.Windows.Forms.ToolBarButton();
            this.tbBtnSep4 = new System.Windows.Forms.ToolBarButton();
            this.tbBtnAddUseCase = new System.Windows.Forms.ToolBarButton();
            this.tbBtnRemoveUseCase = new System.Windows.Forms.ToolBarButton();
            this.imgListToolBar = new System.Windows.Forms.ImageList(this.components);
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.openModelFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveModelFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.elementTokenCtxMenu = new System.Windows.Forms.ContextMenu();
            this.mnuCtxETGoToDefinition = new System.Windows.Forms.MenuItem();
            this.mnuCtxETSep1 = new System.Windows.Forms.MenuItem();
            this.mnuCtxETCut = new System.Windows.Forms.MenuItem();
            this.mnuCtxETCopy = new System.Windows.Forms.MenuItem();
            this.mnuCtxETPaste = new System.Windows.Forms.MenuItem();
            this.imgListSteps = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuTools,
            this.mnuHelp});
            // 
            // mnuFile
            // 
            this.mnuFile.Index = 0;
            this.mnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFileNew,
            this.mnuFileOpen,
            this.mnuFileSep1,
            this.mnuFileSave,
            this.mnuFileSaveAs,
            this.mnuFileSep2,
            this.mnuFileRecent1,
            this.mnuFileRecent2,
            this.mnuFileRecent3,
            this.mnuFileRecent4,
            this.mnuFileSep3,
            this.mnuFileExit});
            this.mnuFile.Text = "[File]";
            // 
            // mnuFileNew
            // 
            this.mnuFileNew.Index = 0;
            this.mnuFileNew.Text = "[New]";
            this.mnuFileNew.Click += new System.EventHandler(this.mnuFileNew_Click);
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Index = 1;
            this.mnuFileOpen.Text = "[Open]";
            this.mnuFileOpen.Click += new System.EventHandler(this.mnuFileOpen_Click);
            // 
            // mnuFileSep1
            // 
            this.mnuFileSep1.Index = 2;
            this.mnuFileSep1.Text = "-";
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Index = 3;
            this.mnuFileSave.Text = "[Save]";
            this.mnuFileSave.Click += new System.EventHandler(this.mnuFileSave_Click);
            // 
            // mnuFileSaveAs
            // 
            this.mnuFileSaveAs.Index = 4;
            this.mnuFileSaveAs.Text = "[Save As]";
            this.mnuFileSaveAs.Click += new System.EventHandler(this.mnuFileSaveAs_Click);
            // 
            // mnuFileSep2
            // 
            this.mnuFileSep2.Index = 5;
            this.mnuFileSep2.Text = "-";
            // 
            // mnuFileRecent1
            // 
            this.mnuFileRecent1.Index = 6;
            this.mnuFileRecent1.Text = "[Recent1]";
            this.mnuFileRecent1.Visible = false;
            this.mnuFileRecent1.Click += new System.EventHandler(this.mnuFileRecent1_Click);
            // 
            // mnuFileRecent2
            // 
            this.mnuFileRecent2.Index = 7;
            this.mnuFileRecent2.Text = "[Recent2]";
            this.mnuFileRecent2.Visible = false;
            this.mnuFileRecent2.Click += new System.EventHandler(this.mnuFileRecent2_Click);
            // 
            // mnuFileRecent3
            // 
            this.mnuFileRecent3.Index = 8;
            this.mnuFileRecent3.Text = "[Recent3]";
            this.mnuFileRecent3.Visible = false;
            this.mnuFileRecent3.Click += new System.EventHandler(this.mnuFileRecent3_Click);
            // 
            // mnuFileRecent4
            // 
            this.mnuFileRecent4.Index = 9;
            this.mnuFileRecent4.Text = "[Recent4]";
            this.mnuFileRecent4.Visible = false;
            this.mnuFileRecent4.Click += new System.EventHandler(this.mnuFileRecent4_Click);
            // 
            // mnuFileSep3
            // 
            this.mnuFileSep3.Index = 10;
            this.mnuFileSep3.Text = "-";
            this.mnuFileSep3.Visible = false;
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Index = 11;
            this.mnuFileExit.Text = "[Exit]";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.Index = 1;
            this.mnuEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuEditCut,
            this.mnuEditCopy,
            this.mnuEditPaste,
            this.menuEditSep1,
            this.mnuEditSearchReplace,
            this.menuEditSep2,
            this.mnuEditAddPackage,
            this.mnuEditRemovePackage,
            this.mnuEditSep3,
            this.mnuEditAddActor,
            this.mnuEditRemoveActor,
            this.mnuEditSep4,
            this.mnuEditAddUseCase,
            this.mnuEditRemoveUseCase,
            this.mnuEditSep5,
            this.mnuEditReorderElements});
            this.mnuEdit.Text = "[Edit]";
            // 
            // mnuEditCut
            // 
            this.mnuEditCut.Enabled = false;
            this.mnuEditCut.Index = 0;
            this.mnuEditCut.Text = "[Cut]";
            this.mnuEditCut.Click += new System.EventHandler(this.mnuEditCut_Click);
            // 
            // mnuEditCopy
            // 
            this.mnuEditCopy.Enabled = false;
            this.mnuEditCopy.Index = 1;
            this.mnuEditCopy.Text = "[Copy]";
            this.mnuEditCopy.Click += new System.EventHandler(this.mnuEditCopy_Click);
            // 
            // mnuEditPaste
            // 
            this.mnuEditPaste.Enabled = false;
            this.mnuEditPaste.Index = 2;
            this.mnuEditPaste.Text = "[Paste]";
            this.mnuEditPaste.Click += new System.EventHandler(this.mnuEditPaste_Click);
            // 
            // menuEditSep1
            // 
            this.menuEditSep1.Index = 3;
            this.menuEditSep1.Text = "-";
            // 
            // mnuEditSearchReplace
            // 
            this.mnuEditSearchReplace.Index = 4;
            this.mnuEditSearchReplace.Text = "[Search and replace]";
            this.mnuEditSearchReplace.Click += new System.EventHandler(this.mnuEditSearchReplace_Click);
            // 
            // menuEditSep2
            // 
            this.menuEditSep2.Index = 5;
            this.menuEditSep2.Text = "-";
            // 
            // mnuEditAddPackage
            // 
            this.mnuEditAddPackage.Index = 6;
            this.mnuEditAddPackage.Text = "[Add package]";
            this.mnuEditAddPackage.Click += new System.EventHandler(this.mnuEditAddPackage_Click);
            // 
            // mnuEditRemovePackage
            // 
            this.mnuEditRemovePackage.Index = 7;
            this.mnuEditRemovePackage.Text = "[Remove package]";
            this.mnuEditRemovePackage.Click += new System.EventHandler(this.mnuEditRemovePackage_Click);
            // 
            // mnuEditSep3
            // 
            this.mnuEditSep3.Index = 8;
            this.mnuEditSep3.Text = "-";
            // 
            // mnuEditAddActor
            // 
            this.mnuEditAddActor.Index = 9;
            this.mnuEditAddActor.Text = "[Add actor]";
            this.mnuEditAddActor.Click += new System.EventHandler(this.mnuEditAddActor_Click);
            // 
            // mnuEditRemoveActor
            // 
            this.mnuEditRemoveActor.Index = 10;
            this.mnuEditRemoveActor.Text = "[Remove actor]";
            this.mnuEditRemoveActor.Click += new System.EventHandler(this.mnuEditRemoveActor_Click);
            // 
            // mnuEditSep4
            // 
            this.mnuEditSep4.Index = 11;
            this.mnuEditSep4.Text = "-";
            // 
            // mnuEditAddUseCase
            // 
            this.mnuEditAddUseCase.Index = 12;
            this.mnuEditAddUseCase.Text = "[Add use case]";
            this.mnuEditAddUseCase.Click += new System.EventHandler(this.mnuEditAddUseCase_Click);
            // 
            // mnuEditRemoveUseCase
            // 
            this.mnuEditRemoveUseCase.Index = 13;
            this.mnuEditRemoveUseCase.Text = "[Remove use case]";
            this.mnuEditRemoveUseCase.Click += new System.EventHandler(this.mnuEditRemoveUseCase_Click);
            // 
            // mnuEditSep5
            // 
            this.mnuEditSep5.Index = 14;
            this.mnuEditSep5.Text = "-";
            // 
            // mnuEditReorderElements
            // 
            this.mnuEditReorderElements.Index = 15;
            this.mnuEditReorderElements.Text = "[Reorder elements]";
            this.mnuEditReorderElements.Click += new System.EventHandler(this.mnuEditReorderElements_Click);
            // 
            // mnuTools
            // 
            this.mnuTools.Index = 2;
            this.mnuTools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuToolsHtmlExport,
            this.mnuToolsPDFExport,
            this.mnuToolsRTFExport,
            this.mnuToolsSep1,
            this.mnuToolsXMIExport,
            this.mnuToolsSep2,
            this.mnuToolsOptions});
            this.mnuTools.Text = "[Tools]";
            // 
            // mnuToolsHtmlExport
            // 
            this.mnuToolsHtmlExport.Index = 0;
            this.mnuToolsHtmlExport.Text = "[HTLM Export]";
            this.mnuToolsHtmlExport.Click += new System.EventHandler(this.mnuToolsHtmlExport_Click);
            // 
            // mnuToolsPDFExport
            // 
            this.mnuToolsPDFExport.Index = 1;
            this.mnuToolsPDFExport.Text = "[PDF Export]";
            this.mnuToolsPDFExport.Click += new System.EventHandler(this.mnuToolsPDFExport_Click);
            // 
            // mnuToolsRTFExport
            // 
            this.mnuToolsRTFExport.Index = 2;
            this.mnuToolsRTFExport.Text = "[RTF Export]";
            this.mnuToolsRTFExport.Click += new System.EventHandler(this.mnuToolsRTFExport_Click);
            // 
            // mnuToolsSep1
            // 
            this.mnuToolsSep1.Index = 3;
            this.mnuToolsSep1.Text = "-";
            // 
            // mnuToolsXMIExport
            // 
            this.mnuToolsXMIExport.Index = 4;
            this.mnuToolsXMIExport.Text = "[XMI 1.1 Export]";
            this.mnuToolsXMIExport.Click += new System.EventHandler(this.mnuXMIExport_Click);
            // 
            // mnuToolsSep2
            // 
            this.mnuToolsSep2.Index = 5;
            this.mnuToolsSep2.Text = "-";
            // 
            // mnuToolsOptions
            // 
            this.mnuToolsOptions.Index = 6;
            this.mnuToolsOptions.Text = "[Options]";
            this.mnuToolsOptions.Click += new System.EventHandler(this.mnuToolsOptions_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.Index = 3;
            this.mnuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuHelpAbout});
            this.mnuHelp.Text = "[Help]";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Index = 0;
            this.mnuHelpAbout.Text = "[About]";
            this.mnuHelpAbout.Click += new System.EventHandler(this.mnuHelpAbout_Click);
            // 
            // modelBrowserCtxMenu
            // 
            this.modelBrowserCtxMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuCtxMBCut,
            this.mnuCtxMBCopy,
            this.mnuCtxMBPaste,
            this.mnuCtxMBSep1,
            this.mnuCtxMBAddPackage,
            this.mnuCtxMBRemovePackage,
            this.mnuCtxMBSep2,
            this.mnuCtxMBAddActor,
            this.mnuCtxMBRemoveActor,
            this.mnuCtxMBSep3,
            this.mnuCtxMBAddUseCase,
            this.mnuCtxMBRemoveUseCase,
            this.mnuCtxMBSep4,
            this.mnuCtxMBReorderElements});
            // 
            // mnuCtxMBCut
            // 
            this.mnuCtxMBCut.Enabled = false;
            this.mnuCtxMBCut.Index = 0;
            this.mnuCtxMBCut.Text = "[Cut]";
            this.mnuCtxMBCut.Click += new System.EventHandler(this.mnuCtxMBCut_Click);
            // 
            // mnuCtxMBCopy
            // 
            this.mnuCtxMBCopy.Enabled = false;
            this.mnuCtxMBCopy.Index = 1;
            this.mnuCtxMBCopy.Text = "[Copy]";
            this.mnuCtxMBCopy.Click += new System.EventHandler(this.mnuCtxMBCopy_Click);
            // 
            // mnuCtxMBPaste
            // 
            this.mnuCtxMBPaste.Enabled = false;
            this.mnuCtxMBPaste.Index = 2;
            this.mnuCtxMBPaste.Text = "[Paste]";
            this.mnuCtxMBPaste.Click += new System.EventHandler(this.mnuCtxMBPaste_Click);
            // 
            // mnuCtxMBSep1
            // 
            this.mnuCtxMBSep1.Index = 3;
            this.mnuCtxMBSep1.Text = "-";
            // 
            // mnuCtxMBAddPackage
            // 
            this.mnuCtxMBAddPackage.Index = 4;
            this.mnuCtxMBAddPackage.Text = "[Add package]";
            this.mnuCtxMBAddPackage.Click += new System.EventHandler(this.mnuCtxMBAddPackage_Click);
            // 
            // mnuCtxMBRemovePackage
            // 
            this.mnuCtxMBRemovePackage.Index = 5;
            this.mnuCtxMBRemovePackage.Text = "[Remove package]";
            this.mnuCtxMBRemovePackage.Click += new System.EventHandler(this.mnuCtxMBRemovePackage_Click);
            // 
            // mnuCtxMBSep2
            // 
            this.mnuCtxMBSep2.Index = 6;
            this.mnuCtxMBSep2.Text = "-";
            // 
            // mnuCtxMBAddActor
            // 
            this.mnuCtxMBAddActor.Index = 7;
            this.mnuCtxMBAddActor.Text = "[Add actor]";
            this.mnuCtxMBAddActor.Click += new System.EventHandler(this.mnuCtxMBAddActor_Click);
            // 
            // mnuCtxMBRemoveActor
            // 
            this.mnuCtxMBRemoveActor.Index = 8;
            this.mnuCtxMBRemoveActor.Text = "[Remove actor]";
            this.mnuCtxMBRemoveActor.Click += new System.EventHandler(this.mnuCtxMBRemoveActor_Click);
            // 
            // mnuCtxMBSep3
            // 
            this.mnuCtxMBSep3.Index = 9;
            this.mnuCtxMBSep3.Text = "-";
            // 
            // mnuCtxMBAddUseCase
            // 
            this.mnuCtxMBAddUseCase.Index = 10;
            this.mnuCtxMBAddUseCase.Text = "[Add use case]";
            this.mnuCtxMBAddUseCase.Click += new System.EventHandler(this.mnuCtxMBAddUseCase_Click);
            // 
            // mnuCtxMBRemoveUseCase
            // 
            this.mnuCtxMBRemoveUseCase.Index = 11;
            this.mnuCtxMBRemoveUseCase.Text = "[Remove use case]";
            this.mnuCtxMBRemoveUseCase.Click += new System.EventHandler(this.mnuCtxMBRemoveUseCase_Click);
            // 
            // mnuCtxMBSep4
            // 
            this.mnuCtxMBSep4.Index = 12;
            this.mnuCtxMBSep4.Text = "-";
            // 
            // mnuCtxMBReorderElements
            // 
            this.mnuCtxMBReorderElements.Index = 13;
            this.mnuCtxMBReorderElements.Text = "[Reorder elements]";
            this.mnuCtxMBReorderElements.Click += new System.EventHandler(this.mnuCtxMBReorderElements_Click);
            // 
            // imgListModelBrowser
            // 
            this.imgListModelBrowser.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListModelBrowser.ImageStream")));
            this.imgListModelBrowser.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListModelBrowser.Images.SetKeyName(0, "");
            this.imgListModelBrowser.Images.SetKeyName(1, "");
            this.imgListModelBrowser.Images.SetKeyName(2, "");
            this.imgListModelBrowser.Images.SetKeyName(3, "");
            this.imgListModelBrowser.Images.SetKeyName(4, "");
            // 
            // dockPanel
            // 
            this.dockPanel.ActiveAutoHideContent = null;
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.dockPanel.Location = new System.Drawing.Point(0, 28);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(792, 552);
            this.dockPanel.TabIndex = 4;
            // 
            // toolBar
            // 
            this.toolBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.tbBtnNew,
            this.tbBtnOpen,
            this.tbBtnSave,
            this.tbBtnSep1,
            this.tbBtnCut,
            this.tbBtnCopy,
            this.tbBtnPaste,
            this.tbBtnSep2,
            this.tbBtnAddPackage,
            this.tbBtnRemovePackage,
            this.tbBtnSep3,
            this.tbBtnAddActor,
            this.tbBtnRemoveActor,
            this.tbBtnSep4,
            this.tbBtnAddUseCase,
            this.tbBtnRemoveUseCase});
            this.toolBar.DropDownArrows = true;
            this.toolBar.ImageList = this.imgListToolBar;
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.ShowToolTips = true;
            this.toolBar.Size = new System.Drawing.Size(792, 28);
            this.toolBar.TabIndex = 1;
            this.toolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar_ButtonClick);
            // 
            // tbBtnNew
            // 
            this.tbBtnNew.ImageIndex = 0;
            this.tbBtnNew.Name = "tbBtnNew";
            // 
            // tbBtnOpen
            // 
            this.tbBtnOpen.ImageIndex = 1;
            this.tbBtnOpen.Name = "tbBtnOpen";
            // 
            // tbBtnSave
            // 
            this.tbBtnSave.ImageIndex = 2;
            this.tbBtnSave.Name = "tbBtnSave";
            // 
            // tbBtnSep1
            // 
            this.tbBtnSep1.Name = "tbBtnSep1";
            this.tbBtnSep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbBtnCut
            // 
            this.tbBtnCut.Enabled = false;
            this.tbBtnCut.ImageIndex = 3;
            this.tbBtnCut.Name = "tbBtnCut";
            // 
            // tbBtnCopy
            // 
            this.tbBtnCopy.Enabled = false;
            this.tbBtnCopy.ImageIndex = 4;
            this.tbBtnCopy.Name = "tbBtnCopy";
            // 
            // tbBtnPaste
            // 
            this.tbBtnPaste.Enabled = false;
            this.tbBtnPaste.ImageIndex = 5;
            this.tbBtnPaste.Name = "tbBtnPaste";
            // 
            // tbBtnSep2
            // 
            this.tbBtnSep2.Name = "tbBtnSep2";
            this.tbBtnSep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbBtnAddPackage
            // 
            this.tbBtnAddPackage.ImageIndex = 10;
            this.tbBtnAddPackage.Name = "tbBtnAddPackage";
            // 
            // tbBtnRemovePackage
            // 
            this.tbBtnRemovePackage.ImageIndex = 11;
            this.tbBtnRemovePackage.Name = "tbBtnRemovePackage";
            // 
            // tbBtnSep3
            // 
            this.tbBtnSep3.Name = "tbBtnSep3";
            this.tbBtnSep3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbBtnAddActor
            // 
            this.tbBtnAddActor.ImageIndex = 6;
            this.tbBtnAddActor.Name = "tbBtnAddActor";
            // 
            // tbBtnRemoveActor
            // 
            this.tbBtnRemoveActor.ImageIndex = 7;
            this.tbBtnRemoveActor.Name = "tbBtnRemoveActor";
            // 
            // tbBtnSep4
            // 
            this.tbBtnSep4.Name = "tbBtnSep4";
            this.tbBtnSep4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbBtnAddUseCase
            // 
            this.tbBtnAddUseCase.ImageIndex = 8;
            this.tbBtnAddUseCase.Name = "tbBtnAddUseCase";
            // 
            // tbBtnRemoveUseCase
            // 
            this.tbBtnRemoveUseCase.ImageIndex = 9;
            this.tbBtnRemoveUseCase.Name = "tbBtnRemoveUseCase";
            // 
            // imgListToolBar
            // 
            this.imgListToolBar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListToolBar.ImageStream")));
            this.imgListToolBar.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListToolBar.Images.SetKeyName(0, "");
            this.imgListToolBar.Images.SetKeyName(1, "");
            this.imgListToolBar.Images.SetKeyName(2, "");
            this.imgListToolBar.Images.SetKeyName(3, "");
            this.imgListToolBar.Images.SetKeyName(4, "");
            this.imgListToolBar.Images.SetKeyName(5, "");
            this.imgListToolBar.Images.SetKeyName(6, "");
            this.imgListToolBar.Images.SetKeyName(7, "");
            this.imgListToolBar.Images.SetKeyName(8, "");
            this.imgListToolBar.Images.SetKeyName(9, "");
            this.imgListToolBar.Images.SetKeyName(10, "");
            this.imgListToolBar.Images.SetKeyName(11, "");
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 580);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(792, 22);
            this.statusBar.TabIndex = 5;
            // 
            // openModelFileDialog
            // 
            this.openModelFileDialog.DefaultExt = "ucm";
            this.openModelFileDialog.Filter = "UseCaseMaker Model|*.ucm";
            // 
            // saveModelFileDialog
            // 
            this.saveModelFileDialog.DefaultExt = "ucm";
            this.saveModelFileDialog.Filter = "UseCaseMaker Model|*.ucm";
            // 
            // elementTokenCtxMenu
            // 
            this.elementTokenCtxMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuCtxETGoToDefinition,
            this.mnuCtxETSep1,
            this.mnuCtxETCut,
            this.mnuCtxETCopy,
            this.mnuCtxETPaste});
            // 
            // mnuCtxETGoToDefinition
            // 
            this.mnuCtxETGoToDefinition.Index = 0;
            this.mnuCtxETGoToDefinition.Text = "[Go to definition]";
            this.mnuCtxETGoToDefinition.Click += new System.EventHandler(this.mnuCtxETGoToDefinition_Click);
            // 
            // mnuCtxETSep1
            // 
            this.mnuCtxETSep1.Index = 1;
            this.mnuCtxETSep1.Text = "-";
            // 
            // mnuCtxETCut
            // 
            this.mnuCtxETCut.Enabled = false;
            this.mnuCtxETCut.Index = 2;
            this.mnuCtxETCut.Text = "[Cut]";
            this.mnuCtxETCut.Click += new System.EventHandler(this.mnuCtxETCut_Click);
            // 
            // mnuCtxETCopy
            // 
            this.mnuCtxETCopy.Enabled = false;
            this.mnuCtxETCopy.Index = 3;
            this.mnuCtxETCopy.Text = "[Copy]";
            this.mnuCtxETCopy.Click += new System.EventHandler(this.mnuCtxETCopy_Click);
            // 
            // mnuCtxETPaste
            // 
            this.mnuCtxETPaste.Enabled = false;
            this.mnuCtxETPaste.Index = 4;
            this.mnuCtxETPaste.Text = "[Paste]";
            this.mnuCtxETPaste.Click += new System.EventHandler(this.mnuCtxETPaste_Click);
            // 
            // imgListSteps
            // 
            this.imgListSteps.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListSteps.ImageStream")));
            this.imgListSteps.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListSteps.Images.SetKeyName(0, "");
            this.imgListSteps.Images.SetKeyName(1, "");
            this.imgListSteps.Images.SetKeyName(2, "");
            this.imgListSteps.Images.SetKeyName(3, "");
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(792, 602);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.toolBar);
            this.Controls.Add(this.statusBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Menu = this.mainMenu;
            this.MinimumSize = new System.Drawing.Size(800, 650);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Use Case Maker";
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.LocationChanged += new System.EventHandler(this.frmMain_LocationChanged);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/**
		 * Punto di ingresso principale dell'applicazione.
		 */
		[STAThread]
		static void Main(string [] args) 
		{
			if(args.Length > 0)
			{
				Application.Run(new frmMain(args[0]));
			}
			else
			{
				Application.Run(new frmMain(string.Empty));
			}
		}

		#region Public Properties
		/**
		 * @brief Modello in uso
		 * 
		 * Ritorna il modello correntemente in uso dall'applicazione
		 */
		public Model Model
		{
			get
			{
				return this.model;
			}
		}
		#endregion

		/**
		 * @brief Localizzazione dei controlli
		 * 
		 * Utilizza la classe Localizer per impostare il testo dei 
		 * controlli dell'applicazione nella lingua in uso dall'utente
		 * 
		 * @param languageFilePath Percorso al file xml contenente la lingua
		 */
		private void LocalizeControls(string languageFilePath)
		{
			this.localizer.Load(languageFilePath);

			this.localizer.LocalizeControls(this);
		}

		/**
		 * @brief Gestione delle modifiche utente
		 * 
		 * Traccia lo stato delle modifiche effettuate dall'utente.
		 * Attiva il comando salva (nel menu e nella toolbar) quando
		 * un elemento del business dell'applicazione è stato modificato
		 * 
		 * @param value Il comando salva è attivo (vero) o disattivo (falso)
		 */
		public void SetModified(bool value)
		{
			if(this.modifiedLocked)
			{
				return;
			}

			if(value == true)
			{
				mnuFileSave.Enabled = true;
				toolBar.Buttons[2].Enabled = true;
			}
			else
			{
				mnuFileSave.Enabled = false;
				toolBar.Buttons[2].Enabled = false;
			}
			this.modified = value;
		}

		/**
		 * @brief Gestione delle modifiche utente
		 * 
		 * Blocca le variazioni dello stato delle modifiche durante
		 * il caricamento e/o il ridisegno dei controlli dell'applicazione
		 */
		public void LockModified()
		{
			this.modifiedLocked = true;
		}

		/**
		 * @brief Gestione delle modifiche utente
		 * 
		 * Sblocca le variazioni dello stato delle modifiche
		 */
		public void UnlockModified()
		{
			this.modifiedLocked = false;
		}

		/**
		 * @brief Gestione delle modifiche utente
		 * 
		 * Ritorna lo stato di blocco delle modifiche
		 */
		public bool IsModifiedLocked()
		{
			return this.modifiedLocked;
		}

		/**
		 * @brief Gestione del modello
		 * 
		 * Chiude il modello correntemente in uso.
		 * Se gli elementi del business risultano modificati,
		 * richiede il salvataggio prima della chiusura
		 */
		private Boolean CloseModel()
		{
            Boolean saved;

			if(this.modified)
			{
				// [Save current model?]
                if(MessageBox.Show(
                    this.localizer.GetValue("UserMessages", "saveModel"),
                    Application.ProductName,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if(this.modelFilePath != string.Empty)
                    {
                        saved = this.SaveModel(false);
                    }
                    else
                    {
                        saved = this.SaveModel(true);
                    }
                    if(!saved)
                    {
                        return false;
                    }
                }
			}
            if(this.frmSearchReplace != null)
            {
                this.frmSearchReplace.Close();
            }
            this.CloseAllDocuments();
            return true;
		}

        /**
		 * @brief Visualizzazione del modello
		 * 
		 * Costruisce la vista ad albero che riflette la struttura del modello
		 * correntemente in uso
		 */
		private void BuildView(object element)
		{
			if(element.GetType() == typeof(Model))
			{
				Model model = (Model)element;
				AddElement(model,null,false);
				foreach(Actor actor in model.Actors.Sorted("ID"))
				{
					actor.Owner = model;
					AddElement(actor,actor.Owner,false);
				}
				foreach(UseCase useCase in model.UseCases.Sorted("ID"))
				{
					useCase.Owner = model;
					AddElement(useCase,useCase.Owner,false);
				}
				foreach(Package subPackage in model.Packages.Sorted("ID"))
				{
					subPackage.Owner = model;
					BuildView(subPackage);
				}
				foreach(GlossaryItem gi in model.Glossary.Sorted("Name"))
				{
					gi.Owner = model;
					string sub = "\"" + gi.Name + "\"";
					sub = sub.Replace(" ","\t");
					sub = sub.Replace(".","\v");
					HighlightDescriptor hd = new 
						HighlightDescriptor(sub,Color.Green,null,DescriptorType.Word,DescriptorRecognition.WholeWord,true);
					this.hdc.Add(hd);
				}
                foreach(Stakeholder stakeholder in model.Stakeholders.Sorted("Name"))
                {
                    stakeholder.Owner = model;
                    string sub = "\"" + stakeholder.Name + "\"";
                    sub = sub.Replace(" ", "\t");
                    sub = sub.Replace(".", "\v");
                    HighlightDescriptor hd = new
                        HighlightDescriptor(sub, Color.Blue, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true);
                    this.hdc.Add(hd);
                }
			}
			if(element.GetType() == typeof(Package))
			{
				Package package = (Package)element;
				AddElement(package,package.Owner,false);
				foreach(Actor actor in package.Actors.Sorted("ID"))
				{
					actor.Owner = package;
					AddElement(actor,actor.Owner,false);
				}
				foreach(UseCase useCase in package.UseCases.Sorted("ID"))
				{
					useCase.Owner = package;
					AddElement(useCase,useCase.Owner,false);
				}
				foreach(Package subPackage in package.Packages.Sorted("ID"))
				{
					subPackage.Owner = package;
					BuildView(subPackage);
				}
			}
		}

		/**
		 * @brief Gestione del modello
		 * 
		 * Trova il nodo selezionato dall'utente usando il valore
		 * univoco assegnato al nodo stesso dal business.
		 * Il metodo agisce in modo ricorsivo.
		 * @param parent Nodo genitore da cui avviare la ricerca
		 * @param uniqueID Identificativo univoco del nodo da ricercare
		 */
		private TreeNode FindNode(TreeNode parent, String uniqueID)
		{
			TreeNode node = null;
			TreeNode retNode = null;

			if(frmModelBrowser.tvModelBrowser.Nodes.Count == 0)
			{
				return null;
			}
			
			if(parent == null)
			{
				node = frmModelBrowser.tvModelBrowser.Nodes[0];
			}
			else
			{
				node = parent;
			}

			if((String)node.Tag == uniqueID)
			{
				return node;
			}

			foreach(TreeNode child in node.Nodes)
			{
				if((String)child.Tag == uniqueID)
				{
					retNode = child;
					break;
				}
				if(child.Nodes.Count > 0)
				{
					retNode = this.FindNode(child, uniqueID);
					if(retNode != null)
					{
						break;
					}
				}
			}

			return retNode;
		}

		/**
		 * @brief Gestione del modello
		 * 
		 * Aggiunge un elemento alla struttura del modello tramite
		 * il business dell'applicazione.
		 * @param element Elemento da aggiungere
		 * @param owner Elemento a cui appartiene l'elemento da aggiungere
		 * @addToParent Flag che stabilisce se l'elemento è gia collegato al genitore
		 */
		public void AddElement(object element, object owner, bool addToParent)
		{
			String ownerUniqueID = String.Empty;
			string sub = null;

			if(element.GetType() == typeof(Model))
			{
				Model model = (Model)element;
				frmModelBrowser.tvModelBrowser.Nodes.Clear();
				TreeNode node = new TreeNode(model.Name + " (" + model.ElementID + ")");
				node.Tag = model.UniqueID;
                node.ToolTipText = model.Attributes.Description;
				frmModelBrowser.tvModelBrowser.Nodes.Add(node);
				frmModelBrowser.tvModelBrowser.SelectedNode = node;
				TreeNode ownerNode = node;
				node = new TreeNode(this.localizer.GetValue("Globals","Actors"),1,1);
				node.Tag = model.Actors.UniqueID;
				ownerNode.Nodes.Add(node);
				node = new TreeNode(this.localizer.GetValue("Globals","UseCases"),2,2);
				node.Tag = model.UseCases.UniqueID;
				ownerNode.Nodes.Add(node);
				sub = "\"" + model.Path + "\"";
				sub = sub.Replace(" ","\t");
				sub = sub.Replace(".","\v");
                ownerNode.Expand();
				HighlightDescriptor hd = new HighlightDescriptor(sub,Color.DarkGray,null,DescriptorType.Word,DescriptorRecognition.WholeWord,true);
				this.hdc.Add(hd);
				sub = "\"" + model.Name + "\"";
				sub = sub.Replace(" ","\t");
				sub = sub.Replace(".","\v");
				hd = new HighlightDescriptor(sub,Color.Red,null,DescriptorType.Word,DescriptorRecognition.WholeWord,true);
				this.hdc.Add(hd);
			}

			if(element.GetType() == typeof(Package))
			{
				Package package = (Package)element;
				ownerUniqueID = ((Package)owner).UniqueID;
				if(addToParent)
				{
					((Package)owner).AddPackage(package);
				}
				TreeNode node = new TreeNode(package.Name + " (" + package.ElementID + ")");
				node.Tag = package.UniqueID;
                node.ToolTipText = package.Attributes.Description;
				TreeNode ownerNode = this.FindNode(null,ownerUniqueID);
				if(ownerNode != null)
				{
					ownerNode.Nodes.Add(node);
                    if(!this.IsModifiedLocked())
                    {
                        frmModelBrowser.tvModelBrowser.SelectedNode = node;
                    }
					ownerNode = node;
					node = new TreeNode(this.localizer.GetValue("Globals","Actors"),1,1);
					node.Tag = package.Actors.UniqueID;
					ownerNode.Nodes.Add(node);
					node = new TreeNode(this.localizer.GetValue("Globals","UseCases"),2,2);
					node.Tag = package.UseCases.UniqueID;
					ownerNode.Nodes.Add(node);
                    if (!this.IsModifiedLocked())
                    {
                        ownerNode.Expand();
                    }
                    else
                    {
                        ownerNode.Collapse();
                    }
				}
				sub = "\"" + package.Path + "\"";
				sub = sub.Replace(" ","\t");
				sub = sub.Replace(".","\v");
				HighlightDescriptor hd = new HighlightDescriptor(sub,Color.DarkGray,null,DescriptorType.Word,DescriptorRecognition.WholeWord,false);
				this.hdc.Add(hd);
				sub = "\"" + package.Name + "\"";
				sub = sub.Replace(" ","\t");
				sub = sub.Replace(".","\v");
				hd = new HighlightDescriptor(sub,Color.Red,null,DescriptorType.Word,DescriptorRecognition.WholeWord,true);
				this.hdc.Add(hd);
			}

			if(element.GetType() == typeof(Actor))
			{
				Actor actor = (Actor)element;
				Package package = (Package)owner;
				if(addToParent)
				{
					package.AddActor(actor);
				}
				TreeNode node = new TreeNode(actor.Name + " (" + actor.ElementID + ")",3,3);
				node.Tag = actor.UniqueID;
                node.ToolTipText = actor.Attributes.Description;
				TreeNode ownerNode = this.FindNode(null,package.UniqueID);
				if(ownerNode != null)
				{
					foreach(TreeNode subNode in ownerNode.Nodes)
					{
						if((String)subNode.Tag == package.Actors.UniqueID)
						{
							subNode.Nodes.Add(node);
                            if(!this.IsModifiedLocked())
                            {
                                frmModelBrowser.tvModelBrowser.SelectedNode = node;
                            }
                            else
                            {
                                subNode.Expand();
                            }
							break;
						}
					}
				}
				sub = "\"" + actor.Path + "\"";
				sub = sub.Replace(" ","\t");
				sub = sub.Replace(".","\v");
				HighlightDescriptor hd = new HighlightDescriptor(sub,Color.DarkGray,null,DescriptorType.Word,DescriptorRecognition.WholeWord,false);
				this.hdc.Add(hd);
				sub = "\"" + actor.Name + "\"";
				sub = sub.Replace(" ","\t");
				sub = sub.Replace(".","\v");
				hd = new HighlightDescriptor(sub,Color.Red,null,DescriptorType.Word,DescriptorRecognition.WholeWord,true);
				this.hdc.Add(hd);
			}

			if(element.GetType() == typeof(UseCase))
			{
				UseCase useCase = (UseCase)element;
				Package package = (Package)owner;
				if(addToParent)
				{
					package.AddUseCase(useCase);
				}
				TreeNode node = new TreeNode(useCase.Name + " (" + useCase.ElementID + ")",4,4);
				node.Tag = useCase.UniqueID;
                node.ToolTipText = useCase.Attributes.Description;
				TreeNode ownerNode = this.FindNode(null,package.UniqueID);
				if(ownerNode != null)
				{
					foreach(TreeNode subNode in ownerNode.Nodes)
					{
						if((String)subNode.Tag == package.UseCases.UniqueID)
						{
							subNode.Nodes.Add(node);
                            if(!this.IsModifiedLocked())
                            {
                                frmModelBrowser.tvModelBrowser.SelectedNode = node;
                            }
                            else
                            {
                                subNode.Expand();
                            }
							break;
						}
					}
				}
				sub = "\"" + useCase.Path + "\"";
				sub = sub.Replace(" ","\t");
				sub = sub.Replace(".","\v");
				HighlightDescriptor hd = new HighlightDescriptor(sub,Color.DarkGray,null,DescriptorType.Word,DescriptorRecognition.WholeWord,false);
				this.hdc.Add(hd);
				sub = "\"" + useCase.Name + "\"";
				sub = sub.Replace(" ","\t");
				sub = sub.Replace(".","\v");
				hd = new HighlightDescriptor(sub,Color.Red,null,DescriptorType.Word,DescriptorRecognition.WholeWord,true);
				this.hdc.Add(hd);
			}

			this.SetModified(true);
		}

		/**
		 * @ingroup user_interface
		 * @brief Gestione del modello
		 * 
		 * Crea un nuovo modello contenente il solo package
		 * principale modificando il business dell'applicazione.
		 * 
		 * @note Il metodo richiama frmMain::CloseModel nel caso
		 * in cui vi sia un modello già in uso che necessita
		 * di essere salvato
		 */
		private void NewModel()
		{
			this.CloseModel();

			this.modelFileName = string.Empty;

			System.Char [] separators = {' ','\r','\n',',','.','-','+','\\','\'','?','!'};
			foreach(System.Char c in separators)
			{
				this.separators.Add(c);
			}

            this.hdc.Clear();

            this.LockModified();

			model = new Model(this.localizer.GetValue("Globals","NewModel"),defaultMPrefix,1);
			this.BuildView(model);

            this.UnlockModified();

			// Cambia il titolo della finestra
			int sub = this.Text.IndexOf("-");
			if(sub != -1)
			{
				this.Text = this.Text.Substring(0,sub - 1) + " - [" + model.Name + "]";
			}
			else
			{
				this.Text += " - [" + model.Name + "]";
			}

			this.SetModified(false);
		}

		/**
		 * @ingroup user_interface
		 * @brief Gestione del modello
		 * 
		 * Apre un modello da file.
		 * 
		 * @note Il metodo richiama frmMain::CloseModel nel caso
		 * in cui vi sia un modello già in uso che necessita
		 * di essere salvato
		 */
		private void OpenModel()
		{
            UCMDocument ucmdoc = new UCMDocument();

			this.CloseModel();

			if(Directory.Exists(this.appSettings.ModelFilePath))
			{
				openModelFileDialog.InitialDirectory = this.appSettings.ModelFilePath;
			}

			// [Open Model]
			openModelFileDialog.Title = this.localizer.GetValue("UserMessages","openModel");
			openModelFileDialog.FileName = string.Empty;
			if(openModelFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				this.appSettings.ModelFilePath = Path.GetDirectoryName(openModelFileDialog.FileName);
				this.modelFilePath = Path.GetDirectoryName(openModelFileDialog.FileName);
				this.modelFileName = Path.GetFileName(openModelFileDialog.FileName);

				this.appSettings.AddToRecentFileList(openModelFileDialog.FileName);
				this.UpdateRecentFileList();

				System.Char [] separators = {' ','\r','\n',',','.','-','+','\\','\'','?','!'};
				foreach(System.Char c in separators)
				{
					this.separators.Add(c);
				}

				this.hdc.Clear();

                this.LockModified();
                Win32.SendMessage(frmModelBrowser.tvModelBrowser.Handle,Win32.WM_SETREDRAW,0,(IntPtr)0);
				this.Cursor = Cursors.WaitCursor;
				try
				{
                    XmlSerializer s = new XmlSerializer(typeof(UCMDocument));
                    TextReader r = new StreamReader(openModelFileDialog.FileName);
                    ucmdoc = (UCMDocument)s.Deserialize(r);
                    r.Close();
                    model = ucmdoc.Model;
					BuildView(model);
				}
				catch(XmlException e)
				{
					MessageBox.Show(this,e.Message,Application.ProductName);
					this.NewModel();
				}

				Win32.SendMessage(frmModelBrowser.tvModelBrowser.Handle,Win32.WM_SETREDRAW,1,(IntPtr)0);
				frmModelBrowser.tvModelBrowser.Invalidate();

                this.UnlockModified();
                this.Cursor = Cursors.Default;

				// Cambia il titolo della finestra
				int sub = this.Text.IndexOf("-");
				if(sub != -1)
				{
					this.Text = this.Text.Substring(0,sub - 1) + " - " + this.modelFileName;
				}
				else
				{
					this.Text += " - " + this.modelFileName;
				}

                this.SetModified(false);

                this.TestVersion(ucmdoc.Version);
			}
		}

		/**
		 * @brief Gestione del modello
		 * 
		 * Apre un modello da file sulla base della selezione utente
		 * dal menu File - elementi recenti
		 * 
		 * @note Il metodo richiama this::CloseModel nel caso
		 * in cui vi sia un modello già in uso che necessita
		 * di essere salvato
		 */
		private void OpenRecentModel(string modelFilePath)
		{
			UCMDocument ucmdoc = new UCMDocument();;
            
            this.CloseModel();

			if(!File.Exists(modelFilePath))
			{
				MessageBox.Show(this,this.localizer.GetValue("UserMessages","cannotOpenFile"));
				return;
			}

			this.appSettings.ModelFilePath = Path.GetDirectoryName(modelFilePath);
			this.modelFilePath = Path.GetDirectoryName(modelFilePath);
			this.modelFileName = Path.GetFileName(modelFilePath);

			System.Char [] separators = {' ','\r','\n',',','.','-','+','\\','\'','?','!'};
			foreach(System.Char c in separators)
			{
				this.separators.Add(c);
			}

			this.hdc.Clear();

            this.LockModified();
            Win32.SendMessage(frmModelBrowser.tvModelBrowser.Handle,Win32.WM_SETREDRAW,0,(IntPtr)0);
			this.Cursor = Cursors.WaitCursor;
			try
			{
                XmlSerializer s = new XmlSerializer(typeof(UCMDocument));
                TextReader r = new StreamReader(modelFilePath);
                ucmdoc = (UCMDocument)s.Deserialize(r);
                r.Close();
                model = ucmdoc.Model;
				BuildView(model);
			}
			catch(XmlException e)
			{
				MessageBox.Show(this,e.Message,Application.ProductName);
				this.NewModel();
			}

			Win32.SendMessage(frmModelBrowser.tvModelBrowser.Handle,Win32.WM_SETREDRAW,1,(IntPtr)0);
			frmModelBrowser.tvModelBrowser.Invalidate();

            this.UnlockModified();
            this.Cursor = Cursors.Default;

			// Cambia il titolo della finestra
			int sub = this.Text.IndexOf("-");
			if(sub != -1)
			{
				this.Text = this.Text.Substring(0,sub - 1) + " - " + this.modelFileName;
			}
			else
			{
				this.Text += " - " + this.modelFileName;
			}

			this.SetModified(false);

            this.TestVersion(ucmdoc.Version);
		}

		private Boolean SaveModel(bool showSaveAsDialog)
		{
            Boolean saved = false;

			if(Directory.Exists(this.appSettings.ModelFilePath))
			{
				saveModelFileDialog.InitialDirectory = this.appSettings.ModelFilePath;
			}

			if(showSaveAsDialog)
			{
				// [Save Model As]
				saveModelFileDialog.Title = this.localizer.GetValue("UserMessages","saveModelAs");
				saveModelFileDialog.FileName = ((this.modelFileName == string.Empty)
					? model.Name : Path.GetFileNameWithoutExtension(this.modelFileName));
				if(saveModelFileDialog.ShowDialog(this) == DialogResult.OK)
				{
					this.appSettings.AddToRecentFileList(saveModelFileDialog.FileName);
					this.UpdateRecentFileList();

                    UCMDocument ucmdoc = new UCMDocument();
                    ucmdoc.Model = model;
                    ucmdoc.Version = this.GetType().Assembly.GetName().Version.ToString(2);
                    XmlSerializer s = new XmlSerializer(typeof(UCMDocument));
                    TextWriter w = new StreamWriter(saveModelFileDialog.FileName);
                    s.Serialize(w, ucmdoc);
                    w.Close();

					this.appSettings.ModelFilePath = Path.GetDirectoryName(saveModelFileDialog.FileName);
					this.modelFilePath = Path.GetDirectoryName(saveModelFileDialog.FileName);
					this.modelFileName = Path.GetFileName(saveModelFileDialog.FileName);
					this.SetModified(false);
                    saved = true;
				}
			}
			else
			{
                UCMDocument ucmdoc = new UCMDocument();
                ucmdoc.Model = model;
                ucmdoc.Version = this.GetType().Assembly.GetName().Version.ToString(2);
                XmlSerializer s = new XmlSerializer(typeof(UCMDocument));
                TextWriter w = new StreamWriter(Path.Combine(this.modelFilePath, this.modelFileName));
                s.Serialize(w, ucmdoc);
                w.Close();
				this.SetModified(false);
                saved = true;
			}

			// Cambia il titolo della finestra
			int sub = this.Text.IndexOf("-");
			if(sub != -1)
			{
				this.Text = this.Text.Substring(0,sub - 1) + " - " + this.modelFileName;
			}
			else
			{
				this.Text += " - " + this.modelFileName;
			}

            return saved;
		}

        private void TestVersion(String fileVersion)
        {
			AssemblyName an = this.GetType().Assembly.GetName();
			String currentVersion = an.Version.ToString(2);
            if(currentVersion.CompareTo(fileVersion) > 0)
            {
                if(MessageBox.Show(
                    this.localizer.GetValue("UserMessages", "updateModel"),
                    Application.ProductName,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if(!this.SaveModel(true))
                    {
                        this.NewModel();
                    }
                }
                else
                {
                    this.NewModel();
                }
            }
        }

        private void CloseAllDocuments()
        {
            foreach (IDockContent content in this.dockPanel.DocumentsToArray())
            {
                content.DockHandler.Close();
            }
        }

        private IDockContent FindDocument(String tabText)
        {
            IDockContent retContent = null;

            foreach (IDockContent content in this.dockPanel.Documents)
            {
                if (content.DockHandler.TabText == tabText)
                {
                    retContent = content;
                }
            }

            return retContent;
        }

		private void UpdateRecentFileList()
		{
			mnuFileRecent1.Visible = false;
			mnuFileRecent2.Visible = false;
			mnuFileRecent3.Visible = false;
			mnuFileRecent4.Visible = false;
			mnuFileSep3.Visible = false;

			if(this.appSettings.RecentFile1 != null)
			{
				mnuFileRecent1.Text = this.appSettings.RecentFile1;
				mnuFileRecent1.Visible = true;
				mnuFileSep3.Visible = true;
			}
			if(this.appSettings.RecentFile2 != null)
			{
				mnuFileRecent2.Text = this.appSettings.RecentFile2;
				mnuFileRecent2.Visible = true;
				mnuFileSep3.Visible = true;
			}
			if(this.appSettings.RecentFile3 != null)
			{
				mnuFileRecent3.Text = this.appSettings.RecentFile3;
				mnuFileRecent3.Visible = true;
				mnuFileSep3.Visible = true;
			}
			if(this.appSettings.RecentFile4 != null)
			{
				mnuFileRecent4.Text = this.appSettings.RecentFile4;
				mnuFileRecent4.Visible = true;
				mnuFileSep3.Visible = true;
			}
		}

		private void RemoveHighlightDescriptor(string hdName, string hdUserID)
		{
			for(int i = this.hdc.Count - 1; i >= 0; i--)
			{
				HighlightDescriptor hd = this.hdc[i];
				if(hd.Token == hdName || hd.Token == hdUserID)
				{
					this.hdc.RemoveAt(i);
				}
			}
		}

        public void EnableElementTokenContextMenu(LinkEnabledRTB parent, bool isToken, Point location)
        {
            parent.ToolTip.Active = false;
            mnuCtxETGoToDefinition.Enabled = isToken;
            elementTokenCtxMenu.Show(parent, location);
            parent.ToolTip.Active = true;
        }

		public string GetElementInfo(object element)
		{
			string elementInfo = string.Empty;

			if(element.GetType() == typeof(Model))
			{
				Model root = (Model)element;

				elementInfo = this.localizer.GetValue("Globals","Model") + ": " + root.Name + "\r\n" +
					this.localizer.GetValue("Globals","Identifier") + ": " + root.Path + "\r\n" +
					this.localizer.GetValue("Globals","Owner") + ":\r\n" +
					this.localizer.GetValue("Globals","Description") + ": " + root.Attributes.Description;
			}
			if(element.GetType() == typeof(Package))
			{
				Package package = (Package)element;

				elementInfo = this.localizer.GetValue("Globals","Package") + ": " + package.Name + "\r\n" +
					this.localizer.GetValue("Globals","Identifier") + ": " + package.Path + "\r\n" +
					this.localizer.GetValue("Globals","Owner") + ": " + package.Owner.Name + "\r\n" +
					this.localizer.GetValue("Globals","Description") + ": " + package.Attributes.Description;
			}
			if(element.GetType() == typeof(Actor))
			{
				Actor actor = (Actor)element;

				elementInfo = this.localizer.GetValue("Globals","Actor") + ": " + actor.Name + "\r\n" +
					this.localizer.GetValue("Globals","Identifier") + ": " + actor.Path + "\r\n" +
					this.localizer.GetValue("Globals","Owner") + ": " + actor.Owner.Name + "\r\n" +
					this.localizer.GetValue("Globals","Description") + ": " + actor.Attributes.Description;
			}
			if(element.GetType() == typeof(UseCase))
			{
				UseCase useCase = (UseCase)element;

				elementInfo = this.localizer.GetValue("Globals","UseCase") + ": " + useCase.Name + "\r\n" +
					this.localizer.GetValue("Globals","Identifier") + ": " + useCase.Path + "\r\n" +
					this.localizer.GetValue("Globals","Owner") + ": " + useCase.Owner.Name + "\r\n" +
					this.localizer.GetValue("Globals","Description") + ": " + useCase.Attributes.Description;
			}
			if(element.GetType() == typeof(GlossaryItem))
			{
				GlossaryItem gi = (GlossaryItem)element;

				elementInfo = this.localizer.GetValue("Globals","GlossaryItem") + ": " + gi.Name + "\r\n" +
					this.localizer.GetValue("Globals","Description") + ": " + gi.Description;
			}
            if(element.GetType() == typeof(Stakeholder))
            {
                Stakeholder stakeholder = (Stakeholder)element;

                elementInfo = this.localizer.GetValue("Globals", "Stakeholder") + ": " + stakeholder.Name + "\r\n" +
                    this.localizer.GetValue("Globals", "Description") + ": " + stakeholder.Description;
            }

			return elementInfo;
		}

        public void AddGlossaryItemHD(String glossaryItem)
        {
            String token = "\"" + glossaryItem + "\"";
            token = token.Replace(" ", "\t");
            token = token.Replace(".", "\v");
            HighlightDescriptor hd = new
                HighlightDescriptor(token, Color.Green, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true);
            this.hdc.Add(hd);
        }

        public void RemoveGlossaryItemHD(String glossaryItem)
        {
            string token = "\"" + glossaryItem + "\"";
            token = token.Replace(" ", "\t");
            token = token.Replace(".", "\v");
            RemoveHighlightDescriptor(token, null);
        }

        public void AddStakeholderHD(String stakeholder)
        {
            String token = "\"" + stakeholder + "\"";
            token = token.Replace(" ", "\t");
            token = token.Replace(".", "\v");
            HighlightDescriptor hd = new
                HighlightDescriptor(token, Color.Blue, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true);
            this.hdc.Add(hd);
        }

        public void RemoveStakeholderHD(String stakeholder)
        {
            string token = "\"" + stakeholder + "\"";
            token = token.Replace(" ", "\t");
            token = token.Replace(".", "\v");
            RemoveHighlightDescriptor(token, null);
        }

		private void AddPackage()
		{
			frmCreator frm = new frmCreator(this.localizer,this.localizer.GetValue("Globals","Package"));
			if(frm.ShowDialog(this) == DialogResult.OK)
			{
				Package owner = (Package)this.currentElement;
				Package package = owner.NewPackage(frm.tbName.Text,defaultPPrefix,owner.Packages.GetNextFreeID());
				this.AddElement(package,owner,true);
			}
			frm.Dispose();
		}

		private void AddActor()
		{
			frmCreator frm = new frmCreator(this.localizer,this.localizer.GetValue("Globals","Actor"));
			if(frm.ShowDialog(this) == DialogResult.OK)
			{
				Package owner = null;
				IdentificableObjectCollection coll = (this.currentElement as IdentificableObjectCollection);
				if(coll == null)
				{
					owner = (Package)this.currentElement;
				}
				else
				{
					owner = coll.Owner;
				}
				Actor actor = owner.NewActor(frm.tbName.Text,defaultAPrefix,owner.Actors.GetNextFreeID());
				this.AddElement(actor,owner,true);
			}
			frm.Dispose();
		}

		private void AddUseCase()
		{
			frmCreator frm = new frmCreator(this.localizer,this.localizer.GetValue("Globals","UseCase"));
			if(frm.ShowDialog(this) == DialogResult.OK)
			{
				Package owner = null;
				IdentificableObjectCollection coll = (this.currentElement as IdentificableObjectCollection);
				if(coll == null)
				{
					owner = (Package)this.currentElement;
				}
				else
				{
					owner = coll.Owner;
				}
				UseCase useCase = owner.NewUseCase(frm.tbName.Text,defaultUCPrefix,owner.UseCases.GetNextFreeID());
				this.AddElement(useCase,owner,true);
			}
			frm.Dispose();
		}

        public void ElementAttributesDescriptionChanged(object element)
        {
            if(this.nodeToolTipIsUpdating)
            {
                return;
            }

            this.nodeToolTipIsUpdating = true;
            
            CommonAttributes attributes = null;
            TreeNode node = this.FindNode(null, ((IdentificableObject)element).UniqueID);
            
            if(node != null)
            {
                if(this.currentElement.GetType() == typeof(Model))
                {
                    attributes = ((Model)this.currentElement).Attributes;
                }
                if(this.currentElement.GetType() == typeof(Package))
                {
                    attributes = ((Package)this.currentElement).Attributes;
                }
                if(this.currentElement.GetType() == typeof(Actor))
                {
                    attributes = ((Actor)this.currentElement).Attributes;
                }
                if(this.currentElement.GetType() == typeof(UseCase))
                {
                    attributes = ((UseCase)this.currentElement).Attributes;
                }

                if(attributes != null)
                {
                    node.ToolTipText = attributes.Description;
                }
            }

            this.nodeToolTipIsUpdating = false;
        }

		public void ElementNameChange(Label oldNameLabel)
		{
			IdentificableObject ia = (IdentificableObject)this.model.FindElementByName(oldNameLabel.Text);
			oldNameLabel.Text = this.ElementNameChange(ia);
            TreeNode node = this.FindNode(null, ia.UniqueID);
            node.Text = ia.Name + " (" + ia.ElementID + ")";
		}

		private void ReorderElements()
		{
			frmReorder frm = new frmReorder(this.localizer,string.Empty);

			if(this.currentElement.GetType() == typeof(Actors))
			{
				frm.Prefix = defaultAPrefix;
				foreach(Actor actor in ((Actors)this.currentElement).Sorted("ID"))
				{
					frm.AddNameToList(actor.Name);
				}
			}

			if(this.currentElement.GetType() == typeof(UseCases))
			{
				frm.Prefix = defaultUCPrefix;
				foreach(UseCase useCase in ((UseCases)this.currentElement).Sorted("ID"))
				{
					frm.AddNameToList(useCase.Name);
				}
			}

            if(this.currentElement.GetType() == typeof(Package)
                || this.currentElement.GetType() == typeof(Model))
            {
                frm.Prefix = defaultPPrefix;
                foreach(Package package in ((Package)this.currentElement).Packages.Sorted("ID"))
                {
                    frm.AddNameToList(package.Name);
                }
            }

			if(frm.ShowDialog(this) == DialogResult.OK)
			{
				string [] orderedNames = frm.GetOrderedNames();
				// Step 1: marks old name with unique tag (#_n_#path#_n_#)
				for(int counter = 0; counter <= orderedNames.GetUpperBound(0); counter++)
				{
					if(this.currentElement.GetType() == typeof(Actors))
					{
						Actor actor = (Actor)((Actors)this.currentElement).FindByName(orderedNames[counter]);
						model.ReplaceElementPath(
							actor.Path,
							"\"",
							"\"",
							actor.Path,
							"#_" + counter.ToString() + "_#",
							"#_" + counter.ToString() + "_#");
					}
					if(this.currentElement.GetType() == typeof(UseCases))
					{
						UseCase useCase = (UseCase)((UseCases)this.currentElement).FindByName(orderedNames[counter]);
						model.ReplaceElementPath(
							useCase.Path,
							"\"",
							"\"",
							useCase.Path,
							"#_" + counter.ToString() + "_#",
							"#_" + counter.ToString() + "_#");
					}
                    if(this.currentElement.GetType() == typeof(Package)
                        || this.currentElement.GetType() == typeof(Model))
                    {
                        Package package = (Package)((Package)this.currentElement).Packages.FindByName(orderedNames[counter]);
                        model.ReplaceElementPath(
                            package.Path,
                            "\"",
                            "\"",
                            package.Path,
                            "#_" + counter.ToString() + "_#",
                            "#_" + counter.ToString() + "_#");
                    }
				}
				// Step 2: element reordering and marked path substitution
				for(int counter = 0; counter <= orderedNames.GetUpperBound(0); counter++)
				{
					if(this.currentElement.GetType() == typeof(Actors))
					{
						Actor actor = (Actor)((Actors)this.currentElement).FindByName(orderedNames[counter]);
						string oldPath = actor.Path;
						actor.ID = counter + 1;
						model.ReplaceElementPath(
							oldPath,
							"#_" + counter.ToString() + "_#",
							"#_" + counter.ToString() + "_#",
							actor.Path,
							"\"",
							"\"");
					}
					if(this.currentElement.GetType() == typeof(UseCases))
					{
						UseCase useCase = (UseCase)((UseCases)this.currentElement).FindByName(orderedNames[counter]);
						string oldPath = useCase.Path;
						useCase.ID = counter + 1;
						model.ReplaceElementPath(
							oldPath,
							"#_" + counter.ToString() + "_#",
							"#_" + counter.ToString() + "_#",
							useCase.Path,
							"\"",
							"\"");
					}
                    if(this.currentElement.GetType() == typeof(Package)
                        || this.currentElement.GetType() == typeof(Model))
                    {
                        Package package = (Package)((Package)this.currentElement).Packages.FindByName(orderedNames[counter]);
                        string oldPath = package.Path;
                        package.ID = counter + 1;
                        model.ReplaceElementPath(
                            oldPath,
                            "#_" + counter.ToString() + "_#",
                            "#_" + counter.ToString() + "_#",
                            package.Path,
                            "\"",
                            "\"");
                    }
				}
				Win32.SendMessage(frmModelBrowser.tvModelBrowser.Handle,Win32.WM_SETREDRAW,0,(IntPtr)0);
				BuildView(this.model);
				Win32.SendMessage(frmModelBrowser.tvModelBrowser.Handle,Win32.WM_SETREDRAW,1,(IntPtr)0);
			}
			frm.Dispose();
		}

		public string ElementNameChange(IdentificableObject ia)
		{
			string type = string.Empty;
            Color hdcolor = Color.Red;

			if(ia.GetType() == typeof(Model))
			{
				type = this.localizer.GetValue("Globals","Model");
			}
			if(ia.GetType() == typeof(Package))
			{
				type = this.localizer.GetValue("Globals","Package");
			}
			if(ia.GetType() == typeof(Actor))
			{
				type = this.localizer.GetValue("Globals","Actor");
			}
			if(ia.GetType() == typeof(UseCase))
			{
				type = this.localizer.GetValue("Globals","UseCase");
			}
			if(ia.GetType() == typeof(GlossaryItem))
			{
				type = this.localizer.GetValue("Globals","GlossaryItem");
                hdcolor = Color.Green;
			}
            if(ia.GetType() == typeof(Stakeholder))
            {
                type = this.localizer.GetValue("Globals", "Stakeholder");
                hdcolor = Color.Blue;
            }

			frmNameChanger frm = new frmNameChanger(this.localizer,type);
			frm.lblOldName.Text = ia.Name;

			if(frm.ShowDialog(this) == DialogResult.OK)
			{
				string token = "\"" + frm.lblOldName.Text + "\"";
				token = token.Replace(" ","\t");
				token = token.Replace(".","\v");

				RemoveHighlightDescriptor(token,string.Empty);

				string sub = "\"" + frm.tbNewName.Text + "\"";
				sub = sub.Replace(" ","\t");
				sub = sub.Replace(".","\v");
				HighlightDescriptor hd = new 
					HighlightDescriptor(sub,hdcolor,null,DescriptorType.Word,DescriptorRecognition.WholeWord,true);
				this.hdc.Add(hd);

				if(!frm.cbNoReplace.Checked)
				{
					model.ReplaceElementName(
						frm.lblOldName.Text,
						"\"",
						"\"",
						frm.tbNewName.Text,
						"\"",
						"\"");
                    if (dockPanel.ActiveDocument != null)
                    {
                        ((frmTabView)dockPanel.ActiveDocument.DockHandler.Form).UpdateView();
                    }
				}

				ia.Name = frm.tbNewName.Text;

                foreach (IDockContent content in this.dockPanel.Documents)
                {
                    if (content.DockHandler.TabText == frm.lblOldName.Text)
                    {
                        content.DockHandler.TabText = frm.tbNewName.Text;
                    }
                    ((frmTabView)content.DockHandler.Form).UpdateView();
                }
				
				this.SetModified(true);
			}
			frm.Dispose();
			return ia.Name;
		}

		private void ElementDelete()
		{
			string type = string.Empty;

			if(this.currentElement.GetType() == typeof(Model))
			{
				type = this.localizer.GetValue("Globals","Model");
			}
			if(this.currentElement.GetType() == typeof(Package))
			{
				type = this.localizer.GetValue("Globals","Package");
			}
			if(this.currentElement.GetType() == typeof(Actor))
			{
				type = this.localizer.GetValue("Globals","Actor");
			}
			if(this.currentElement.GetType() == typeof(UseCase))
			{
				type = this.localizer.GetValue("Globals","UseCase");
			}

			frmDeleter frm = new frmDeleter(this.localizer,type);

			if(frm.ShowDialog(this) == DialogResult.OK)
			{
				RecursiveRemoveHighligthDescriptor(this.currentElement);
				Package owner = null;
				if(this.currentElement.GetType() == typeof(Package))
				{
					owner = ((Package)this.currentElement).Owner;
					owner.RemovePackage(
						(Package)this.currentElement,
						"\"",
						"\"",
						"{",
						"}",
						frm.cbDontMark.Checked);
				}
				if(this.currentElement.GetType() == typeof(Actor))
				{
					owner = ((Actor)this.currentElement).Owner;
					owner.RemoveActor(
						(Actor)this.currentElement,
						"\"",
						"\"",
						"{",
						"}",
						frm.cbDontMark.Checked);
				}
				if(this.currentElement.GetType() == typeof(UseCase))
				{
					owner = ((UseCase)this.currentElement).Owner;
					owner.RemoveUseCase(
						(UseCase)this.currentElement,
						"\"",
						"\"",
						"{",
						"}",
						frm.cbDontMark.Checked);
				}
                IDockContent content = this.FindDocument(((IdentificableObject)this.currentElement).Name);
                if(content != null)
                {
                    content.DockHandler.Close();
                }
                if (this.dockPanel.ActiveDocument != null)
                {
                    ((frmTabView)this.dockPanel.ActiveDocument.DockHandler.Form).UpdateView();
                }
                frmModelBrowser.tvModelBrowser.SelectedNode.Remove();
			}

			frm.Dispose();
			this.SetModified(true);
		}

		private void RecursiveRemoveHighligthDescriptor(object element)
		{
			if(element.GetType() == typeof(Package))
			{
				Package package = (Package)element;

				foreach(Actor actor in package.Actors)
				{
					RecursiveRemoveHighligthDescriptor(actor);
				}
				foreach(UseCase useCase in package.UseCases)
				{
					RecursiveRemoveHighligthDescriptor(useCase);
				}
				foreach(Package subPackage in package.Packages)
				{
					RecursiveRemoveHighligthDescriptor(subPackage);
				}
				string token = "\"" + package.Name + "\"";
				token = token.Replace(" ","\t");
				token = token.Replace(".","\v");
				string userID = "\"" + package.Path + "\"";
				userID = userID.Replace(" ","\t");
				userID = userID.Replace(".","\v");
				RemoveHighlightDescriptor(token,userID);
			}
			if(element.GetType() == typeof(Actor))
			{
				Actor actor = (Actor)element;
				string token = "\"" + actor.Name + "\"";
				token = token.Replace(" ","\t");
				token = token.Replace(".","\v");
				string userID = "\"" + actor.Path + "\"";
				userID = userID.Replace(" ","\t");
				userID = userID.Replace(".","\v");
				RemoveHighlightDescriptor(token,userID);
			}
			if(element.GetType() == typeof(UseCase))
			{
				UseCase useCase = (UseCase)element;
				string token = "\"" + useCase.Name + "\"";
				token = token.Replace(" ","\t");
				token = token.Replace(".","\v");
				string userID = "\"" + useCase.Path + "\"";
				userID = userID.Replace(" ","\t");
				userID = userID.Replace(".","\v");
				RemoveHighlightDescriptor(token,userID);
			}
		}

		private void mnuFileNew_Click(object sender, System.EventArgs e)
		{
			this.NewModel();
		}

        private void mnuFileOpen_Click(object sender, System.EventArgs e)
        {
            this.OpenModel();
        }

		private void mnuFileSave_Click(object sender, System.EventArgs e)
		{
			if(this.modelFileName == string.Empty)
			{
				this.SaveModel(true);
			}
			else
			{
				this.SaveModel(false);
			}
		}

		private void mnuEditAddPackage_Click(object sender, System.EventArgs e)
		{
			this.AddPackage();
		}

		private void mnuEditRemovePackage_Click(object sender, System.EventArgs e)
		{
			this.ElementDelete();
		}

		private void mnuEditAddActor_Click(object sender, System.EventArgs e)
		{
			this.AddActor();
		}

		private void mnuEditRemoveActor_Click(object sender, System.EventArgs e)
		{
			this.ElementDelete();
		}

		private void mnuEditAddUseCase_Click(object sender, System.EventArgs e)
		{
			this.AddUseCase();
		}

		private void mnuEditRemoveUseCase_Click(object sender, System.EventArgs e)
		{
			this.ElementDelete();
		}

		private void mnuEditReorderElements_Click(object sender, System.EventArgs e)
		{
			this.ReorderElements();
		}

        private void mnuEditSearchReplace_Click(object sender, EventArgs e)
        {
            if(frmSearchReplace == null)
            {
                frmSearchReplace = new frmSearchReplace(this,this.localizer);
                frmSearchReplace.Show(this);
            }
            frmSearchReplace.Visible = true;
        }

		private void mnuFileSaveAs_Click(object sender, System.EventArgs e)
		{
			this.SaveModel(true);
		}

		private void mnuFileExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void mnuCtxMBAddPackage_Click(object sender, System.EventArgs e)
		{
			this.AddPackage();
		}

		private void mnuCtxMBRemovePackage_Click(object sender, System.EventArgs e)
		{
			this.ElementDelete();
		}

		private void mnuCtxMBAddActor_Click(object sender, System.EventArgs e)
		{
			this.AddActor();
		}

		private void mnuCtxMBRemoveActor_Click(object sender, System.EventArgs e)
		{
			this.ElementDelete();
		}

		private void mnuCtxMBAddUseCase_Click(object sender, System.EventArgs e)
		{
			this.AddUseCase();
		}

		private void mnuCtxMBRemoveUseCase_Click(object sender, System.EventArgs e)
		{
			this.ElementDelete();
		}

		private void mnuCtxMBReorderElements_Click(object sender, System.EventArgs e)
		{
			this.ReorderElements();
		}

        private void toolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {
            switch (toolBar.Buttons.IndexOf(e.Button))
            {
                // New model
                case 0:
                    NewModel();
                    break;
                // Open model
                case 1:
                    OpenModel();
                    break;
                // Save model
                case 2:
                    if (this.modelFileName == string.Empty)
                    {
                        SaveModel(true);
                    }
                    else
                    {
                        SaveModel(false);
                    }
                    break;
                // Cut
                case 4:
                    mnuEditCut_Click(mnuEditCut, new EventArgs());
                    break;
                // Copy
                case 5:
                    mnuEditCopy_Click(mnuEditCopy, new EventArgs());
                    break;
                // Paste
                case 6:
                    mnuEditPaste_Click(mnuEditPaste, new EventArgs());
                    break;
                // Add package
                case 8:
                    AddPackage();
                    break;
                // Remove package
                case 9:
                    ElementDelete();
                    break;
                // Add actor
                case 11:
                    AddActor();
                    break;
                // Remove actor
                case 12:
                    ElementDelete();
                    break;
                // Add use case
                case 14:
                    AddUseCase();
                    break;
                // Remove use case
                case 15:
                    ElementDelete();
                    break;
            }
        }

		protected override void OnClosing(CancelEventArgs e)
		{
            if(!this.CloseModel())
            {
                e.Cancel = true;
            }
            else
            {
                this.appSettings.SaveSettings();
            }
			base.OnClosing (e);
		}

		private void frmMain_Load(object sender, System.EventArgs e)
		{
			this.appSettings.LoadSettings();
			this.ImeMode = ImeMode.On;

			frmAbout frm = new frmAbout(true);
			frm.Show();
			Application.DoEvents();
			System.Threading.Thread.Sleep(3000);
			frm.Close();
			frm.Dispose();

			this.SuspendLayout();
			
			try
			{
				LocalizeControls(this.appSettings.UILanguageFilePath);
			}
			catch(Exception)
			{
				MessageBox.Show(
					this,
					"Cannot load the localization file!",
					Application.ProductName,
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);
			}

			if(this.appSettings.IsMaximized)
			{
				this.WindowState = FormWindowState.Maximized;
			}
			else
			{
				this.Location = new Point(this.appSettings.Left,this.appSettings.Top);
				this.Size = new Size(this.appSettings.Width,this.appSettings.Height);
				this.CenterToScreen();
			}
			this.UpdateRecentFileList();
            
            frmModelBrowser = new frmModelBrowser(
                this,
                this.imgListModelBrowser,
                this.modelBrowserCtxMenu,
                this.localizer);
            frmModelBrowser.Show(dockPanel, DockState.DockLeft);
			
			this.ResumeLayout();
			
			if(this.modelFilePath == string.Empty)
			{
				this.NewModel();
			}
			else
			{
				string sPath = string.Empty;
				if(this.modelFilePath.EndsWith(Path.DirectorySeparatorChar.ToString()))
				{
					sPath = this.modelFilePath + this.modelFileName;
				}
				else
				{
					sPath = this.modelFilePath + Path.DirectorySeparatorChar + this.modelFileName;
				}
				this.OpenRecentModel(sPath);
				this.appSettings.AddToRecentFileList(sPath);
				this.UpdateRecentFileList();
			}
		}

		private void frmMain_Resize(object sender, System.EventArgs e)
		{
			this.appSettings.Left = this.Left;
			this.appSettings.Top = this.Top;
			this.appSettings.Width = this.Width;
			this.appSettings.Height = this.Height;
			if(this.WindowState == FormWindowState.Maximized)
			{
				this.appSettings.IsMaximized = true;
			}
			else
			{
				this.appSettings.IsMaximized = false;
			}
		}

		private void frmMain_LocationChanged(object sender, System.EventArgs e)
		{
			this.appSettings.Left = this.Left;
			this.appSettings.Top = this.Top;
		}

		private void mnuToolsHtmlExport_Click(object sender, System.EventArgs e)
		{
			if(this.modelFilePath == string.Empty || this.modified)
			{
				// [Model must be saved before exporting...]
				MessageBox.Show(
					this,
					this.localizer.GetValue("UserMessages","saveBeforeExport"),
					Application.ProductName,
					MessageBoxButtons.OK,
					MessageBoxIcon.Hand);
				return;
			}

			if(Directory.Exists(this.appSettings.HtmlFilesPath))
			{
				folderBrowserDialog.SelectedPath = this.appSettings.HtmlFilesPath;
			}
			// [Select destination folder for generated HTML files]
			folderBrowserDialog.Description = this.localizer.GetValue("UserMessages","selectHTMLdestination");
			if(folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
			{
				try
				{
					string [] filesToCopy = Directory.GetFiles(this.appSettings.ReportsFilesPath,"*.*");
					foreach(string file in filesToCopy)
					{
						File.Copy(file,folderBrowserDialog.SelectedPath + Path.DirectorySeparatorChar + 
							file.Substring(file.LastIndexOf(Path.DirectorySeparatorChar)+1),true);
					}
				}
				catch(Exception ex)
				{
					MessageBox.Show(
						this,
						ex.GetType().Name + ":\r\n" + ex.Message,
						Application.ProductName,
						MessageBoxButtons.OK,
						MessageBoxIcon.Stop);
					return;
				}

				try
				{
					HTMLConverter hc = new HTMLConverter(
						folderBrowserDialog.SelectedPath,
						folderBrowserDialog.SelectedPath,
						this.localizer);
					this.Cursor = Cursors.WaitCursor;
					hc.BuildNavigator(this.modelFilePath + Path.DirectorySeparatorChar + this.modelFileName);
					hc.BuildPages(this.modelFilePath + Path.DirectorySeparatorChar + this.modelFileName);
					this.appSettings.HtmlFilesPath = folderBrowserDialog.SelectedPath;
				}
				catch(Exception ex)
				{
					this.Cursor = Cursors.Default;
					MessageBox.Show(
						this,
						ex.GetType().Name + ":\r\n" + ex.Message,
						Application.ProductName,
						MessageBoxButtons.OK,
						MessageBoxIcon.Stop);
					return;
				}

				this.Cursor = Cursors.Default;

				try
				{
					string [] filesToDelete = Directory.GetFiles(folderBrowserDialog.SelectedPath,"*.xsl");
					foreach(string file in filesToDelete)
					{
						File.Delete(file);
					}
				}
				catch(Exception ex)
				{
					MessageBox.Show(
						this,
						ex.GetType().Name + ":\r\n" + ex.Message,
						Application.ProductName,
						MessageBoxButtons.OK,
						MessageBoxIcon.Stop);
					return;
				}

				// [Export complete. Do you want to see the HTML files?]
				if(MessageBox.Show(
					this,
					this.localizer.GetValue("UserMessages","htmlExportCompleted"),
					Application.ProductName,
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Information) == DialogResult.Yes)
				{
					Process process = new Process();
					process.StartInfo.FileName = folderBrowserDialog.SelectedPath + Path.DirectorySeparatorChar 
						+ "index.htm";
					try
					{
						process.Start();
					}
					catch(Win32Exception ex)
					{
						// [Cannot open file!]
						MessageBox.Show(this,this.localizer.GetValue("UserMessages","cannotOpenFile") + 
							"\r\n" + ex.Message);
					}
				}
			}
		}

		private void mnuXMIExport_Click(object sender, System.EventArgs e)
		{
			if(this.modelFilePath == string.Empty || this.modified)
			{
				// [Model must be saved before exporting...]
				MessageBox.Show(
					this,
					this.localizer.GetValue("UserMessages","saveBeforeExport"),
					Application.ProductName,
					MessageBoxButtons.OK,
					MessageBoxIcon.Hand);
				return;
			}

			SaveFileDialog saveXMIFileDialog = new SaveFileDialog();
			saveXMIFileDialog.Title = this.localizer.GetValue("UserMessages","exportAsXMI");
			saveXMIFileDialog.FileName = ((this.modelFileName == string.Empty)
				? model.Name : Path.GetFileNameWithoutExtension(this.modelFileName));
			saveXMIFileDialog.DefaultExt = "xml";
			saveXMIFileDialog.Filter = "XML Files|*.xml";
			if(saveXMIFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				XMIConverter xmi = new XMIConverter(
					this.appSettings.ReportsFilesPath,
					saveXMIFileDialog.FileName);

				this.Cursor = Cursors.WaitCursor;

				try
				{
					xmi.Transform(this.modelFilePath + Path.DirectorySeparatorChar + this.modelFileName);
				}
				catch(Exception ex)
				{
					this.Cursor = Cursors.Default;
					MessageBox.Show(
						this,
						ex.GetType().Name + ":\r\n" + ex.Message,
						Application.ProductName,
						MessageBoxButtons.OK,
						MessageBoxIcon.Stop);
					return;
				}

				this.Cursor = Cursors.Default;
				
				// [Export complete.]
				MessageBox.Show(
					this,
					this.localizer.GetValue("UserMessages","xmiExportCompleted"),
					Application.ProductName,
					MessageBoxButtons.OK,
					MessageBoxIcon.Information);
			}
		}

		private void mnuToolsPDFExport_Click(object sender, System.EventArgs e)
		{
			if(this.modelFilePath == string.Empty || this.modified)
			{
				// [Model must be saved before exporting...]
				MessageBox.Show(
					this,
					this.localizer.GetValue("UserMessages","saveBeforeExport"),
					Application.ProductName,
					MessageBoxButtons.OK,
					MessageBoxIcon.Hand);
				return;
			}

			SaveFileDialog savePDFFileDialog = new SaveFileDialog();
			savePDFFileDialog.Title = this.localizer.GetValue("UserMessages","exportAsPDF");
			savePDFFileDialog.FileName = ((this.modelFileName == string.Empty)
				? model.Name : Path.GetFileNameWithoutExtension(this.modelFileName));
			savePDFFileDialog.DefaultExt = "pdf";
			savePDFFileDialog.Filter = "PDF Files|*.pdf";
			if(savePDFFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				PDFConverter pdf = new PDFConverter(
					this.appSettings.ReportsFilesPath,
					savePDFFileDialog.FileName,
					this.localizer);

				this.Cursor = Cursors.WaitCursor;

				try
				{
					pdf.Transform(this.modelFilePath + Path.DirectorySeparatorChar + this.modelFileName);
				}
				catch(Exception ex)
				{
					this.Cursor = Cursors.Default;
					MessageBox.Show(
						this,
						ex.GetType().Name + ":\r\n" + ex.Message,
						Application.ProductName,
						MessageBoxButtons.OK,
						MessageBoxIcon.Stop);
					return;
				}

				this.Cursor = Cursors.Default;

				// [Export complete. Do you want to see the PDF file?]
				if(MessageBox.Show(
					this,
					this.localizer.GetValue("UserMessages","pdfExportCompleted"),
					Application.ProductName,
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Information) == DialogResult.Yes)
				{
					Process process = new Process();
					process.StartInfo.FileName = savePDFFileDialog.FileName;
					try
					{
						process.Start();
					}
					catch(Win32Exception ex)
					{
						// [Cannot open file!]
						MessageBox.Show(this,this.localizer.GetValue("UserMessages","cannotOpenFile") + 
							"\r\n" + ex.Message);
					}
				}
			}		
		}

		private void mnuToolsRTFExport_Click(object sender, System.EventArgs e)
		{
			if(this.modelFilePath == string.Empty || this.modified)
			{
				// [Model must be saved before exporting...]
				MessageBox.Show(
					this,
					this.localizer.GetValue("UserMessages","saveBeforeExport"),
					Application.ProductName,
					MessageBoxButtons.OK,
					MessageBoxIcon.Hand);
				return;
			}

			SaveFileDialog saveRTFFileDialog = new SaveFileDialog();
			saveRTFFileDialog.Title = this.localizer.GetValue("UserMessages","exportAsRTF");
			saveRTFFileDialog.FileName = ((this.modelFileName == string.Empty)
				? model.Name : Path.GetFileNameWithoutExtension(this.modelFileName));
			saveRTFFileDialog.DefaultExt = "rtf";
			saveRTFFileDialog.Filter = "RTF Files|*.rtf";
			if(saveRTFFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				RTFConverter rtf = new RTFConverter(
					this.appSettings.ReportsFilesPath,
					saveRTFFileDialog.FileName,
					this.localizer);

				this.Cursor = Cursors.WaitCursor;

				try
				{
					rtf.Transform(this.modelFilePath + Path.DirectorySeparatorChar + this.modelFileName);
				}
				catch(Exception ex)
				{
					this.Cursor = Cursors.Default;
					MessageBox.Show(
						this,
						ex.GetType().Name + ":\r\n" + ex.Message,
						Application.ProductName,
						MessageBoxButtons.OK,
						MessageBoxIcon.Stop);
					return;
				}

				this.Cursor = Cursors.Default;

				// [Export complete. Do you want to see the PDF file?]
				if(MessageBox.Show(
					this,
					this.localizer.GetValue("UserMessages","rtfExportCompleted"),
					Application.ProductName,
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Information) == DialogResult.Yes)
				{
					Process process = new Process();
					process.StartInfo.FileName = saveRTFFileDialog.FileName;
					try
					{
						process.Start();
					}
					catch(Win32Exception ex)
					{
						// [Cannot open file!]
						MessageBox.Show(this,this.localizer.GetValue("UserMessages","cannotOpenFile") + 
							"\r\n" + ex.Message);
					}
				}
			}				
		}

		private void mnuFileRecent1_Click(object sender, System.EventArgs e)
		{
			this.OpenRecentModel(mnuFileRecent1.Text);
		}

		private void mnuFileRecent2_Click(object sender, System.EventArgs e)
		{
			this.OpenRecentModel(mnuFileRecent2.Text);
		}

		private void mnuFileRecent3_Click(object sender, System.EventArgs e)
		{
			this.OpenRecentModel(mnuFileRecent3.Text);
		}

		private void mnuFileRecent4_Click(object sender, System.EventArgs e)
		{
			this.OpenRecentModel(mnuFileRecent4.Text);
		}

		private void mnuToolsOptions_Click(object sender, System.EventArgs e)
		{
			frmOptions frm = new frmOptions(this.appSettings,this.localizer);
			if(frm.ShowDialog(this) == DialogResult.OK)
			{
				if(frm.SelectedLanguage != string.Empty
					&& frm.SelectedLanguage != this.appSettings.UILanguage)
				{
                    this.CloseAllDocuments();
					this.appSettings.UILanguage = frm.SelectedLanguage;
					this.appSettings.SaveSettings();
					this.LocalizeControls(this.appSettings.UILanguageFilePath);
				}
			}
		}

		private void mnuHelpAbout_Click(object sender, System.EventArgs e)
		{
			frmAbout frm = new frmAbout(false);
			frm.ShowDialog(this);
			frm.Dispose();
		}

		private void mnuCtxETGoToDefinition_Click(object sender, System.EventArgs e)
		{
			ContextMenu ctxMenu = (ContextMenu)((MenuItem)sender).Parent;
			LinkEnabledRTB rtb = (LinkEnabledRTB)ctxMenu.SourceControl;
			string token = rtb.LastTokenClicked;
            this.GoToDefinition(token);
		}

        public void GoToDefinition(String token)
        {
            IdentificableObject io = (IdentificableObject)model.FindElementByName(token);

            if(io == null)
            {
                return;
            }

			if(io is GlossaryItem || io is Stakeholder)
			{
				frmModelBrowser.tvModelBrowser.SelectedNode = frmModelBrowser.tvModelBrowser.Nodes[0];
				frmModelBrowser.tvModelBrowser.Nodes[0].EnsureVisible();

                frmTabView frm;
                IDockContent content = this.FindDocument(model.Name);
                if (content != null)
                {
                    frm = (frmTabView)content.DockHandler.Form;
                    frm.Activate();
                }
                else
                {
                    frm = new frmTabView(
                        this,
                        model,
                        this.separators,
                        this.hdc,
                        this.localizer);
                    frm.Show(this.dockPanel, DockState.Document);
                }
                if(io is GlossaryItem)
                {
                    frm.ShowGlossaryItem(io.Name);
                }
                else
                {
                    frm.ShowStakeholder(io.Name);
                }
			}
            else
            {
                TreeNode node = this.FindNode(null, io.UniqueID);
                node.EnsureVisible();
                frmModelBrowser.tvModelBrowser.SelectedNode = node;
            }
        }

        public void ModelBrowserCut()
        {
            Clipboard.SetDataObject(frmModelBrowser.tvModelBrowser.SelectedNode, false);
        }

        public void ModelBrowserPaste(object dstElement, object srcElement)
        {
            // Sorgente e destinazione sono lo stesso elemento
            if (((IIdentificableObject)dstElement).UniqueID ==
                ((IIdentificableObject)srcElement).UniqueID)
            {
                return;
            }

            this.hdc.Clear();

            if (dstElement.GetType() == typeof(Package) || dstElement.GetType() == typeof(Model))
            {
                if (srcElement.GetType() == typeof(Package))
                {
                    Package dst = (Package)dstElement;
                    Package src = (Package)srcElement;
                    Package owner = src.Owner;
                    owner.RemovePackage(
                        src,
                        "",
                        "",
                        "",
                        "",
                        true);
                    src.ID = dst.Packages.GetNextFreeID();
                    dst.AddPackage(src);
                    Clipboard.SetDataObject(new DataObject());
                }
            }
            else if (dstElement.GetType() == typeof(Actors))
            {
                if (srcElement.GetType() == typeof(Actors))
                {
                    Package dst = ((Actors)dstElement).Owner;
                    Actors src = (Actors)srcElement;
                    Package owner = src.Owner;
                    foreach (Actor actor in owner.Actors)
                    {
                        actor.ID = dst.Actors.GetNextFreeID();
                        dst.AddActor(actor);
                    }
                    owner.Actors.Clear();
                    Clipboard.SetDataObject(new DataObject());
                }
                if (srcElement.GetType() == typeof(Actor))
                {
                    Package dst = ((Actors)dstElement).Owner;
                    Actor src = (Actor)srcElement;
                    Package owner = src.Owner;
                    owner.RemoveActor(
                        src,
                        "",
                        "",
                        "",
                        "",
                        true);
                    src.ID = dst.Actors.GetNextFreeID();
                    dst.AddActor(src);
                    Clipboard.SetDataObject(new DataObject());
                }
            }
            else if (dstElement.GetType() == typeof(UseCases))
            {
                if (srcElement.GetType() == typeof(UseCases))
                {
                    Package dst = ((UseCases)dstElement).Owner;
                    UseCases src = (UseCases)srcElement;
                    Package owner = src.Owner;
                    foreach (UseCase useCase in owner.UseCases)
                    {
                        useCase.ID = dst.UseCases.GetNextFreeID();
                        dst.AddUseCase(useCase);
                    }
                    owner.UseCases.Clear();
                    Clipboard.SetDataObject(new DataObject());
                }
                if (srcElement.GetType() == typeof(UseCase))
                {
                    Package dst = ((UseCases)dstElement).Owner;
                    UseCase src = (UseCase)srcElement;
                    Package owner = src.Owner;
                    owner.RemoveUseCase(
                        src,
                        "",
                        "",
                        "",
                        "",
                        true);
                    src.ID = dst.UseCases.GetNextFreeID();
                    dst.AddUseCase(src);
                    Clipboard.SetDataObject(new DataObject());
                }
            }

            mnuEditPaste.Enabled = true;

            Win32.SendMessage(frmModelBrowser.tvModelBrowser.Handle, Win32.WM_SETREDRAW, 0, (IntPtr)0);
            this.BuildView(this.model);
            Win32.SendMessage(frmModelBrowser.tvModelBrowser.Handle, Win32.WM_SETREDRAW, 1, (IntPtr)0);

            if (this.dockPanel.ActiveDocument != null)
            {
                ((frmTabView)this.dockPanel.ActiveDocument.DockHandler.Form).UpdateView();
            }
        }

		private void mnuEditCut_Click(object sender, System.EventArgs e)
		{
			Control activeControl = this.ActiveControl;

			if(activeControl.GetType() == typeof(UseCaseMakerControls.LinkEnabledRTB))
			{
				LinkEnabledRTB rtb = (LinkEnabledRTB)activeControl;
				rtb.Cut();
				this.SetModified(true);
			}

			if(activeControl.GetType() == typeof(DockPane))
            {
                Form frm = (Form)((DockPane)activeControl).ActiveControl;
                if(frm.ActiveControl.GetType() == typeof(TreeView))
                {
                    ModelBrowserCut();
                }
			}
		}


		private void mnuEditCopy_Click(object sender, System.EventArgs e)
		{
			Control activeControl = this.ActiveControl;

			if(activeControl.GetType() == typeof(UseCaseMakerControls.LinkEnabledRTB))
			{
				LinkEnabledRTB rtb = (LinkEnabledRTB)activeControl;
				rtb.Copy();
				this.SetModified(true);
			}
		}

		private void mnuEditPaste_Click(object sender, System.EventArgs e)
		{
			Control activeControl = this.ActiveControl;

			if(activeControl.GetType() == typeof(UseCaseMakerControls.LinkEnabledRTB))
			{
				LinkEnabledRTB rtb = (LinkEnabledRTB)activeControl;
				rtb.Paste(DataFormats.GetFormat(DataFormats.Text));
				this.SetModified(true);
			}

            if(activeControl.GetType() == typeof(DockPane))
			{
                Form frm = (Form)((DockPane)activeControl).ActiveControl;
                if (frm.ActiveControl.GetType() == typeof(TreeView))
                {
                    if (!Clipboard.GetDataObject().GetDataPresent(typeof(TreeNode)))
                    {
                        return;
                    }

                    TreeNode srcNode, dstNode;
                    object srcElement, dstElement;

                    srcNode = (TreeNode)Clipboard.GetDataObject().GetData(typeof(TreeNode));
                    srcElement = model.FindElementByUniqueID((String)srcNode.Tag);

                    dstNode = frmModelBrowser.tvModelBrowser.SelectedNode;
                    if (dstNode != null)
                    {
                        dstElement = model.FindElementByUniqueID((String)dstNode.Tag);
                        ModelBrowserPaste(dstElement, srcElement);
                    }
                }
			}
		}

		public void OnEditableStateChanged(object sender, ItemTextChangedEventArgs e)
		{
			Control activeControl;

			if(sender.GetType() == typeof(IndexedListItem))
			{
				activeControl = (LinkEnabledRTB)e.Item;
			}
			else if(sender is Control)
			{
				activeControl = (Control)sender;
			}
			else
			{
				return;
			}

			if(activeControl.GetType() == typeof(UseCaseMakerControls.LinkEnabledRTB))
			{
				LinkEnabledRTB rtb = (LinkEnabledRTB)activeControl;
				if(!rtb.Focused)
				{
					return;
				}
				if(rtb.SelectionLength == 0)
				{
					mnuEditCut.Enabled = false;
					mnuEditCopy.Enabled = false;
				}
				else
				{
					mnuEditCut.Enabled = true;
					mnuEditCopy.Enabled = true;
				}
				if(rtb.CanPaste(DataFormats.GetFormat(DataFormats.Text)))
				{
					mnuEditPaste.Enabled = true;
				}
				else
				{
					mnuEditPaste.Enabled = false;
				}
			}
			else if(activeControl.GetType() == typeof(TreeView))
			{
                TreeNode dstNode = frmModelBrowser.tvModelBrowser.SelectedNode;
				TreeNode srcNode;
				object srcElement, dstElement;

				if(dstNode == null)
				{
					return;
				}

                this.currentElement = model.FindElementByUniqueID((String)dstNode.Tag);

                // Edit menu forced status
                if (this.currentElement.GetType() == typeof(Model))
				{
					mnuEditCut.Enabled = false;
					mnuEditCopy.Enabled = false;
				}
				else
				{
                    mnuEditCut.Enabled = true;
                    if(this.currentElement.GetType() == typeof(UseCases)
                        || this.currentElement.GetType() == typeof(Actors))
                    {
                        if(((IdentificableObjectCollection)this.currentElement).Count == 0)
                        {
                            mnuEditCut.Enabled = false;
                        }
                    }
				}

                if (this.currentElement.GetType() == typeof(Model))
                {
                    // ToolBar controls
                    tbBtnAddActor.Enabled = false;
                    tbBtnAddPackage.Enabled = true;
                    tbBtnAddUseCase.Enabled = false;
                    tbBtnRemoveActor.Enabled = false;
                    tbBtnRemovePackage.Enabled = false;
                    tbBtnRemoveUseCase.Enabled = false;

                    // Menu items
                    mnuEditAddActor.Enabled = false;
                    mnuEditAddPackage.Enabled = true;
                    mnuEditAddUseCase.Enabled = false;
                    mnuEditRemoveActor.Enabled = false;
                    mnuEditRemovePackage.Enabled = false;
                    mnuEditRemoveUseCase.Enabled = false;
                    if(((Package)this.currentElement).Packages.Count > 0)
                    {
                        mnuEditReorderElements.Enabled = true;
                    }
                    else
                    {
                        mnuEditReorderElements.Enabled = false;
                    }

                    // Model browser context menu items
                    mnuCtxMBAddActor.Enabled = false;
                    mnuCtxMBAddPackage.Enabled = true;
                    mnuCtxMBAddUseCase.Enabled = false;
                    mnuCtxMBRemoveActor.Enabled = false;
                    mnuCtxMBRemovePackage.Enabled = false;
                    mnuCtxMBRemoveUseCase.Enabled = false;
                    if(((Package)this.currentElement).Packages.Count > 0)
                    {
                        mnuCtxMBReorderElements.Enabled = true;
                    }
                    else
                    {
                        mnuCtxMBReorderElements.Enabled = false;
                    }
                }
                else if (this.currentElement.GetType() == typeof(Package))
                {
                    Package package = (Package)this.currentElement;

                    // ToolBar controls
                    tbBtnAddActor.Enabled = true;
                    tbBtnAddPackage.Enabled = true;
                    tbBtnAddUseCase.Enabled = true;
                    tbBtnRemoveActor.Enabled = false;
                    tbBtnRemovePackage.Enabled = true;
                    tbBtnRemoveUseCase.Enabled = false;

                    // Menu items
                    mnuEditAddActor.Enabled = true;
                    mnuEditAddPackage.Enabled = true;
                    mnuEditAddUseCase.Enabled = true;
                    mnuEditRemoveActor.Enabled = false;
                    mnuEditRemovePackage.Enabled = true;
                    mnuEditRemoveUseCase.Enabled = false;
                    if(((Package)this.currentElement).Packages.Count > 0)
                    {
                        mnuEditReorderElements.Enabled = true;
                    }
                    else
                    {
                        mnuEditReorderElements.Enabled = false;
                    } 

                    // Model browser context menu items
                    mnuCtxMBAddActor.Enabled = true;
                    mnuCtxMBAddPackage.Enabled = true;
                    mnuCtxMBAddUseCase.Enabled = true;
                    mnuCtxMBRemoveActor.Enabled = false;
                    mnuCtxMBRemovePackage.Enabled = true;
                    mnuCtxMBRemoveUseCase.Enabled = false;
                    if(((Package)this.currentElement).Packages.Count > 0)
                    {
                        mnuCtxMBReorderElements.Enabled = true;
                    }
                    else
                    {
                        mnuCtxMBReorderElements.Enabled = false;
                    }                    
                }
                else if (this.currentElement.GetType() == typeof(Actors))
                {
                    // ToolBar controls
                    tbBtnAddActor.Enabled = true;
                    tbBtnAddPackage.Enabled = false;
                    tbBtnAddUseCase.Enabled = false;
                    tbBtnRemoveActor.Enabled = false;
                    tbBtnRemovePackage.Enabled = false;
                    tbBtnRemoveUseCase.Enabled = false;

                    // Menu items
                    mnuEditAddActor.Enabled = true;
                    mnuEditAddPackage.Enabled = false;
                    mnuEditAddUseCase.Enabled = false;
                    mnuEditRemoveActor.Enabled = false;
                    mnuEditRemovePackage.Enabled = false;
                    mnuEditRemoveUseCase.Enabled = false;
                    if (((Actors)this.currentElement).Count > 0)
                    {
                        mnuEditReorderElements.Enabled = true;
                    }
                    else
                    {
                        mnuEditReorderElements.Enabled = false;
                    }

                    // Model browser context menu items
                    mnuCtxMBAddActor.Enabled = true;
                    mnuCtxMBAddPackage.Enabled = false;
                    mnuCtxMBAddUseCase.Enabled = false;
                    mnuCtxMBRemoveActor.Enabled = false;
                    mnuCtxMBRemovePackage.Enabled = false;
                    mnuCtxMBRemoveUseCase.Enabled = false;
                    if (((Actors)this.currentElement).Count > 0)
                    {
                        mnuCtxMBReorderElements.Enabled = true;
                    }
                    else
                    {
                        mnuCtxMBReorderElements.Enabled = false;
                    }
                }
                else if (this.currentElement.GetType() == typeof(UseCases))
                {
                    // ToolBar controls
                    tbBtnAddActor.Enabled = false;
                    tbBtnAddPackage.Enabled = false;
                    tbBtnAddUseCase.Enabled = true;
                    tbBtnRemoveActor.Enabled = false;
                    tbBtnRemovePackage.Enabled = false;
                    tbBtnRemoveUseCase.Enabled = false;

                    // Menu items
                    mnuEditAddActor.Enabled = false;
                    mnuEditAddPackage.Enabled = false;
                    mnuEditAddUseCase.Enabled = true;
                    mnuEditRemoveActor.Enabled = false;
                    mnuEditRemovePackage.Enabled = false;
                    mnuEditRemoveUseCase.Enabled = false;
                    if (((UseCases)this.currentElement).Count > 0)
                    {
                        mnuEditReorderElements.Enabled = true;
                    }
                    else
                    {
                        mnuEditReorderElements.Enabled = false;
                    }

                    // Model browser context menu items
                    mnuCtxMBAddActor.Enabled = false;
                    mnuCtxMBAddPackage.Enabled = false;
                    mnuCtxMBAddUseCase.Enabled = true;
                    mnuCtxMBRemoveActor.Enabled = false;
                    mnuCtxMBRemovePackage.Enabled = false;
                    mnuCtxMBRemoveUseCase.Enabled = false;
                    if (((UseCases)this.currentElement).Count > 0)
                    {
                        mnuCtxMBReorderElements.Enabled = true;
                    }
                    else
                    {
                        mnuCtxMBReorderElements.Enabled = false;
                    }
                }
                else if (this.currentElement.GetType() == typeof(Actor))
                {
                    // ToolBar controls
                    tbBtnAddActor.Enabled = false;
                    tbBtnAddPackage.Enabled = false;
                    tbBtnAddUseCase.Enabled = false;
                    tbBtnRemoveActor.Enabled = true;
                    tbBtnRemovePackage.Enabled = false;
                    tbBtnRemoveUseCase.Enabled = false;

                    // Menu items
                    mnuEditAddActor.Enabled = false;
                    mnuEditAddPackage.Enabled = false;
                    mnuEditAddUseCase.Enabled = false;
                    mnuEditRemoveActor.Enabled = true;
                    mnuEditRemovePackage.Enabled = false;
                    mnuEditRemoveUseCase.Enabled = false;
                    mnuEditReorderElements.Enabled = false;

                    // Model browser context menu items
                    mnuCtxMBAddActor.Enabled = false;
                    mnuCtxMBAddPackage.Enabled = false;
                    mnuCtxMBAddUseCase.Enabled = false;
                    mnuCtxMBRemoveActor.Enabled = true;
                    mnuCtxMBRemovePackage.Enabled = false;
                    mnuCtxMBRemoveUseCase.Enabled = false;
                    mnuCtxMBReorderElements.Enabled = false;
                }
                else if (this.currentElement.GetType() == typeof(UseCase))
                {
                    // ToolBar controls
                    tbBtnAddActor.Enabled = false;
                    tbBtnAddPackage.Enabled = false;
                    tbBtnAddUseCase.Enabled = false;
                    tbBtnRemoveActor.Enabled = false;
                    tbBtnRemovePackage.Enabled = false;
                    tbBtnRemoveUseCase.Enabled = true;

                    // Menu items
                    mnuEditAddActor.Enabled = false;
                    mnuEditAddPackage.Enabled = false;
                    mnuEditAddUseCase.Enabled = false;
                    mnuEditRemoveActor.Enabled = false;
                    mnuEditRemovePackage.Enabled = false;
                    mnuEditRemoveUseCase.Enabled = true;
                    mnuEditReorderElements.Enabled = false;

                    // Model browser context menu items
                    mnuCtxMBAddActor.Enabled = false;
                    mnuCtxMBAddPackage.Enabled = false;
                    mnuCtxMBAddUseCase.Enabled = false;
                    mnuCtxMBRemoveActor.Enabled = false;
                    mnuCtxMBRemovePackage.Enabled = false;
                    mnuCtxMBRemoveUseCase.Enabled = true;
                    mnuCtxMBReorderElements.Enabled = false;
                }

				// Clipboard status control
                if(Clipboard.GetDataObject().GetDataPresent(typeof(TreeNode)))
				{
					srcNode = (TreeNode)Clipboard.GetDataObject().GetData(typeof(TreeNode));
					srcElement = model.FindElementByUniqueID((String)srcNode.Tag);
					dstElement = model.FindElementByUniqueID((String)dstNode.Tag);

					// Sorgente e destinazione sono lo stesso elemento
					if(((IIdentificableObject)dstElement).UniqueID ==
						((IIdentificableObject)srcElement).UniqueID)
					{
						mnuEditPaste.Enabled = false;;
					}

					if(dstElement.GetType() == typeof(Package) || dstElement.GetType() == typeof(Model))
					{
						if(srcElement.GetType() == typeof(Package))
						{
							mnuEditPaste.Enabled = true;
						}
					}
					else if(dstElement.GetType() == typeof(Actors))
					{
						if(srcElement.GetType() == typeof(Actors))
						{
							mnuEditPaste.Enabled = true;
						}
						if(srcElement.GetType() == typeof(Actor))
						{
							mnuEditPaste.Enabled = true;
						}
					}
					else if(dstElement.GetType() == typeof(UseCases))
					{
						if(srcElement.GetType() == typeof(UseCases))
						{
							mnuEditPaste.Enabled = true;
						}
						if(srcElement.GetType() == typeof(UseCase))
						{
							mnuEditPaste.Enabled = true;
						}
					}
				}
				else
				{
					mnuEditPaste.Enabled = false;
				}		
			}

			tbBtnCut.Enabled = mnuCtxMBCut.Enabled = mnuCtxETCut.Enabled = mnuEditCut.Enabled;
			tbBtnCopy.Enabled = mnuCtxMBCopy.Enabled = mnuCtxETCopy.Enabled = mnuEditCopy.Enabled;
			tbBtnPaste.Enabled = mnuCtxMBPaste.Enabled = mnuCtxETPaste.Enabled = mnuEditPaste.Enabled;

			if(sender.GetType() == typeof(IndexedListItem))
			{
				if(((IndexedListItem)sender).ReadOnly)
				{
					tbBtnCut.Enabled = mnuCtxETCut.Enabled = mnuEditCut.Enabled = false;
				}
			}
		}

		private void mnuCtxMBCut_Click(object sender, System.EventArgs e)
		{
			mnuEditCut_Click(sender,e);
		}

		private void mnuCtxMBCopy_Click(object sender, System.EventArgs e)
		{
			mnuEditCopy_Click(sender,e);
		}

		private void mnuCtxMBPaste_Click(object sender, System.EventArgs e)
		{
			mnuEditPaste_Click(sender,e);
		}

		private void mnuCtxETCut_Click(object sender, System.EventArgs e)
		{
			mnuEditCut_Click(sender,e);
		}

		private void mnuCtxETCopy_Click(object sender, System.EventArgs e)
		{
			mnuEditCopy_Click(sender,e);
		}

		private void mnuCtxETPaste_Click(object sender, System.EventArgs e)
		{
			mnuEditPaste_Click(sender,e);
		}

        internal void OnEditElement(object sender)
        {
            Control activeControl;

            if (sender is Control)
            {
                activeControl = (Control)sender;
            }
            else
            {
                return;
            }

			if(activeControl.GetType() == typeof(TreeView))
			{
                if (!frmModelBrowser.tvModelBrowser.Focused)
                {
                    return;
                }
                
                TreeNode dstNode = frmModelBrowser.tvModelBrowser.SelectedNode;

				if(dstNode == null)
				{
					return;
				}

                object element = model.FindElementByUniqueID((String)dstNode.Tag);

                if (element is IdentificableObjectCollection)
                {
                    return;
                }

                ActivateDocument(element);
            }
        }

        internal Control ShowTarget(object element, String propertyName, Int32 index, Int32 start, Int32 length)
        {
            TabControl tabControl;
            IndexedList il;
            IndexedListItem ili;
            LinkEnabledRTB rtb;
            frmTabView frm = ActivateDocument(element);

            tabControl = (TabControl)frm.Controls["tabUseCase"];

            if(element.GetType() == typeof(Model))
            {
                if(propertyName == "Glossary")
                {
                    tabControl.SelectTab("pgGlossary");
                    il = (IndexedList)tabControl.SelectedTab.Controls["GList"];
                    ili = (IndexedListItem)il.Items[index];
                    ili.SelectText(start,length);
                    ili.Selected = true;
                    return ili;
                }
                if(propertyName == "Stakeholders")
                {
                    tabControl.SelectTab("pgStakeholders");
                    il = (IndexedList)tabControl.SelectedTab.Controls["SList"];
                    ili = (IndexedListItem)il.Items[index];
                    ili.SelectText(start, length);
                    ili.Selected = true;
                    return ili;
                }
            }

            if(element.GetType() == typeof(Model) || element.GetType() == typeof(Package))
            {
                if(propertyName == "Requirements")
                {
                    tabControl.SelectTab("pgRequirements");
                    il = (IndexedList)tabControl.SelectedTab.Controls["RList"];
                    ili = (IndexedListItem)il.Items[index];
                    ili.SelectText(start, length);
                    ili.Selected = true;
                    return ili;
                }
            }

            if(element.GetType() == typeof(Actor))
            {
                if(propertyName == "Goals")
                {
                    tabControl.SelectTab("pgAGeneral");
                    il = (IndexedList)tabControl.SelectedTab.Controls["AGList"];
                    ili = (IndexedListItem)il.Items[index];
                    ili.SelectText(start, length);
                    ili.Selected = true;
                    return ili;
                }
            }

            if(element.GetType() == typeof(UseCase))
            {
                if(propertyName == "Steps")
                {
                    tabControl.SelectTab("pgFlowOfEvents");
                    il = (IndexedList)tabControl.SelectedTab.Controls["UCList"];
                    ili = (IndexedListItem)il.Items[index];
                    ili.SelectText(start, length);
                    ili.Selected = true;
                    return ili;
                }

                if(propertyName == "OpenIssues")
                {
                    tabControl.SelectTab("pgDetails");
                    il = (IndexedList)tabControl.SelectedTab.Controls["OIList"];
                    ili = (IndexedListItem)il.Items[index];
                    ili.SelectText(start, length);
                    ili.Selected = true;
                    return ili;
                }

                if(propertyName == "Preconditions")
                {
                    tabControl.SelectTab("pgUCGeneral");
                    rtb = (LinkEnabledRTB)tabControl.SelectedTab.Controls["tbPreconditions"];
                    rtb.SelectionStart = start;
                    rtb.SelectionLength = length;
                    rtb.Focus();
                    return rtb;
                }

                if(propertyName == "Postconditions")
                {
                    tabControl.SelectTab("pgUCGeneral");
                    rtb = (LinkEnabledRTB)tabControl.SelectedTab.Controls["tbPostconditions"];
                    rtb.SelectionStart = start;
                    rtb.SelectionLength = length;
                    rtb.Focus();
                    return rtb;
                }

                if(propertyName == "Prose")
                {
                    tabControl.SelectTab("pgProse");
                    rtb = (LinkEnabledRTB)tabControl.SelectedTab.Controls["tbProse"];
                    rtb.SelectionStart = start;
                    rtb.SelectionLength = length;
                    rtb.Focus();
                    return rtb;
                }
            }            

            if(propertyName == "Attributes.Description")
            {
                tabControl.SelectTab("pgAttributes");
                rtb = (LinkEnabledRTB)tabControl.SelectedTab.Controls["tbDescription"];
                rtb.SelectionStart = start;
                rtb.SelectionLength = length;
                rtb.Focus();
                return rtb;
            }

            if(propertyName == "Attributes.Notes")
            {
                tabControl.SelectTab("pgAttributes");
                rtb = (LinkEnabledRTB)tabControl.SelectedTab.Controls["tbNotes"];
                rtb.SelectionStart = start;
                rtb.SelectionLength = length;
                rtb.Focus();
                return rtb;
            }

            return null;
        }

        internal void UpdateAllDocumentViews()
        {
            this.Cursor = Cursors.WaitCursor;

            foreach(IDockContent content in this.dockPanel.Documents)
            {
                ((frmTabView)content.DockHandler.Form).UpdateView();
            }

            this.Cursor = Cursors.Default;
        }

        internal object GetActiveWindowElement()
        {
            object element = null;

            if(this.ActiveMdiChild != null)
            {
                element = this.model.FindElementByName(((IDockContent)this.ActiveMdiChild).DockHandler.TabText);
            }

            return element;
        }

        private frmTabView ActivateDocument(object element)
        {
            this.Cursor = Cursors.WaitCursor;
            Icon icon = null;

            IDockContent content = this.FindDocument(((IdentificableObject)element).Name);
            if(content != null)
            {
                content.DockHandler.Activate();
                this.Cursor = Cursors.Default;
                return (frmTabView)content;
            }

            frmTabView frm = new frmTabView(
                this,
                element,
                this.separators,
                this.hdc,
                this.localizer);

            if(element.GetType() == typeof(Package) || element.GetType() == typeof(Model))
            {
                icon = Icon.FromHandle(((Bitmap)imgListModelBrowser.Images[0]).GetHicon());
            }
            if(element.GetType() == typeof(Actor))
            {
                icon = Icon.FromHandle(((Bitmap)imgListModelBrowser.Images[3]).GetHicon());
            }
            if(element.GetType() == typeof(UseCase))
            {
                icon = Icon.FromHandle(((Bitmap)imgListModelBrowser.Images[4]).GetHicon());
            }

            frm.Icon = (Icon)icon.Clone();
            Win32.DestroyIcon(icon.Handle);

            frm.Show(this.dockPanel, DockState.Document);
            frm.Activate();

            this.Cursor = Cursors.Default;

            return frm;
        }
    }
}
