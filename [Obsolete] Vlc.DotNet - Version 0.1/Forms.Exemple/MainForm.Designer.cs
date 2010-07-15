namespace Vlc.DotNet.Forms.Exemple
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.myControlsGroupBox = new System.Windows.Forms.GroupBox();
            this.myStartButton = new System.Windows.Forms.Button();
            this.myStopButton = new System.Windows.Forms.Button();
            this.myMediasListBox = new System.Windows.Forms.ListBox();
            this.myVlcpathGroupBox = new System.Windows.Forms.GroupBox();
            this.myVlcPathButton = new System.Windows.Forms.Button();
            this.myVlcPathTextBox = new System.Windows.Forms.TextBox();
            this.myVlcPathFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.myMediasControlsGroupBox = new System.Windows.Forms.GroupBox();
            this.myRemoveMediaButton = new System.Windows.Forms.Button();
            this.myAddMediaButton = new System.Windows.Forms.Button();
            this.myAddMediaOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.myVlcControl = new Vlc.DotNet.Forms.VlcControl();
            this.myPositionTrackBar = new System.Windows.Forms.TrackBar();
            this.myControlsGroupBox.SuspendLayout();
            this.myVlcpathGroupBox.SuspendLayout();
            this.myMediasControlsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myPositionTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // myControlsGroupBox
            // 
            this.myControlsGroupBox.Controls.Add(this.myStartButton);
            this.myControlsGroupBox.Controls.Add(this.myStopButton);
            this.myControlsGroupBox.Location = new System.Drawing.Point(378, 64);
            this.myControlsGroupBox.Name = "myControlsGroupBox";
            this.myControlsGroupBox.Size = new System.Drawing.Size(219, 240);
            this.myControlsGroupBox.TabIndex = 1;
            this.myControlsGroupBox.TabStop = false;
            this.myControlsGroupBox.Text = "Media Controls";
            // 
            // myStartButton
            // 
            this.myStartButton.Enabled = false;
            this.myStartButton.Location = new System.Drawing.Point(6, 48);
            this.myStartButton.Name = "myStartButton";
            this.myStartButton.Size = new System.Drawing.Size(207, 23);
            this.myStartButton.TabIndex = 1;
            this.myStartButton.Text = "Start";
            this.myStartButton.UseVisualStyleBackColor = true;
            this.myStartButton.Click += new System.EventHandler(this.OnStartButtonClick);
            // 
            // myStopButton
            // 
            this.myStopButton.Enabled = false;
            this.myStopButton.Location = new System.Drawing.Point(6, 19);
            this.myStopButton.Name = "myStopButton";
            this.myStopButton.Size = new System.Drawing.Size(207, 23);
            this.myStopButton.TabIndex = 0;
            this.myStopButton.Text = "Stop";
            this.myStopButton.UseVisualStyleBackColor = true;
            this.myStopButton.Click += new System.EventHandler(this.OnStopButtonClick);
            // 
            // myMediasListBox
            // 
            this.myMediasListBox.FormattingEnabled = true;
            this.myMediasListBox.Location = new System.Drawing.Point(12, 336);
            this.myMediasListBox.Name = "myMediasListBox";
            this.myMediasListBox.Size = new System.Drawing.Size(360, 160);
            this.myMediasListBox.TabIndex = 2;
            this.myMediasListBox.SelectedIndexChanged += new System.EventHandler(this.OnMediasListBoxSelectedIndexChanged);
            // 
            // myVlcpathGroupBox
            // 
            this.myVlcpathGroupBox.Controls.Add(this.myVlcPathButton);
            this.myVlcpathGroupBox.Controls.Add(this.myVlcPathTextBox);
            this.myVlcpathGroupBox.Location = new System.Drawing.Point(12, 12);
            this.myVlcpathGroupBox.Name = "myVlcpathGroupBox";
            this.myVlcpathGroupBox.Size = new System.Drawing.Size(585, 46);
            this.myVlcpathGroupBox.TabIndex = 3;
            this.myVlcpathGroupBox.TabStop = false;
            this.myVlcpathGroupBox.Text = "VideoLan Client path";
            // 
            // myVlcPathButton
            // 
            this.myVlcPathButton.Location = new System.Drawing.Point(545, 17);
            this.myVlcPathButton.Name = "myVlcPathButton";
            this.myVlcPathButton.Size = new System.Drawing.Size(34, 23);
            this.myVlcPathButton.TabIndex = 6;
            this.myVlcPathButton.Text = "...";
            this.myVlcPathButton.UseVisualStyleBackColor = true;
            this.myVlcPathButton.Click += new System.EventHandler(this.OnVlcPathButtonClick);
            // 
            // myVlcPathTextBox
            // 
            this.myVlcPathTextBox.Enabled = false;
            this.myVlcPathTextBox.Location = new System.Drawing.Point(6, 19);
            this.myVlcPathTextBox.Name = "myVlcPathTextBox";
            this.myVlcPathTextBox.Size = new System.Drawing.Size(533, 20);
            this.myVlcPathTextBox.TabIndex = 5;
            // 
            // myVlcPathFolderBrowserDialog
            // 
            this.myVlcPathFolderBrowserDialog.RootFolder = System.Environment.SpecialFolder.ProgramFiles;
            this.myVlcPathFolderBrowserDialog.ShowNewFolderButton = false;
            // 
            // myMediasControlsGroupBox
            // 
            this.myMediasControlsGroupBox.Controls.Add(this.myRemoveMediaButton);
            this.myMediasControlsGroupBox.Controls.Add(this.myAddMediaButton);
            this.myMediasControlsGroupBox.Location = new System.Drawing.Point(378, 310);
            this.myMediasControlsGroupBox.Name = "myMediasControlsGroupBox";
            this.myMediasControlsGroupBox.Size = new System.Drawing.Size(219, 187);
            this.myMediasControlsGroupBox.TabIndex = 2;
            this.myMediasControlsGroupBox.TabStop = false;
            this.myMediasControlsGroupBox.Text = "Media Librairy Controls";
            // 
            // myRemoveMediaButton
            // 
            this.myRemoveMediaButton.Enabled = false;
            this.myRemoveMediaButton.Location = new System.Drawing.Point(6, 48);
            this.myRemoveMediaButton.Name = "myRemoveMediaButton";
            this.myRemoveMediaButton.Size = new System.Drawing.Size(207, 23);
            this.myRemoveMediaButton.TabIndex = 1;
            this.myRemoveMediaButton.Text = "Remove Selected Media";
            this.myRemoveMediaButton.UseVisualStyleBackColor = true;
            this.myRemoveMediaButton.Click += new System.EventHandler(this.OnRemoveMediaButtonClick);
            // 
            // myAddMediaButton
            // 
            this.myAddMediaButton.Location = new System.Drawing.Point(6, 19);
            this.myAddMediaButton.Name = "myAddMediaButton";
            this.myAddMediaButton.Size = new System.Drawing.Size(207, 23);
            this.myAddMediaButton.TabIndex = 0;
            this.myAddMediaButton.Text = "Add Media";
            this.myAddMediaButton.UseVisualStyleBackColor = true;
            this.myAddMediaButton.Click += new System.EventHandler(this.OnAddMediaButtonClick);
            // 
            // myAddMediaOpenFileDialog
            // 
            this.myAddMediaOpenFileDialog.Filter = "Video files|*.avi|Audio files|*.mp3|All files|*.*";
            this.myAddMediaOpenFileDialog.ReadOnlyChecked = true;
            this.myAddMediaOpenFileDialog.SupportMultiDottedExtensions = true;
            this.myAddMediaOpenFileDialog.Title = "Add media to media librairy";
            // 
            // myVlcControl
            // 
            this.myVlcControl.BackColor = System.Drawing.Color.Black;
            this.myVlcControl.Location = new System.Drawing.Point(12, 64);
            this.myVlcControl.Manager.Position = 0F;
            this.myVlcControl.Manager.VlcLibPath = "C:\\Program Files\\VideoLAN\\VLC\\";
            this.myVlcControl.Name = "myVlcControl";
            this.myVlcControl.Size = new System.Drawing.Size(360, 240);
            this.myVlcControl.TabIndex = 0;
            this.myVlcControl.Text = "vlcControl1";
            // 
            // myPositionTrackBar
            // 
            this.myPositionTrackBar.Enabled = false;
            this.myPositionTrackBar.Location = new System.Drawing.Point(12, 310);
            this.myPositionTrackBar.Maximum = 100;
            this.myPositionTrackBar.Name = "myPositionTrackBar";
            this.myPositionTrackBar.Size = new System.Drawing.Size(360, 45);
            this.myPositionTrackBar.TabIndex = 4;
            this.myPositionTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 509);
            this.Controls.Add(this.myMediasListBox);
            this.Controls.Add(this.myPositionTrackBar);
            this.Controls.Add(this.myMediasControlsGroupBox);
            this.Controls.Add(this.myVlcpathGroupBox);
            this.Controls.Add(this.myControlsGroupBox);
            this.Controls.Add(this.myVlcControl);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Vlc.DotNet";
            this.myControlsGroupBox.ResumeLayout(false);
            this.myVlcpathGroupBox.ResumeLayout(false);
            this.myVlcpathGroupBox.PerformLayout();
            this.myMediasControlsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.myPositionTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private VlcControl myVlcControl;
        private System.Windows.Forms.GroupBox myControlsGroupBox;
        private System.Windows.Forms.Button myStopButton;
        private System.Windows.Forms.ListBox myMediasListBox;
        private System.Windows.Forms.Button myStartButton;
        private System.Windows.Forms.GroupBox myVlcpathGroupBox;
        private System.Windows.Forms.Button myVlcPathButton;
        private System.Windows.Forms.TextBox myVlcPathTextBox;
        private System.Windows.Forms.FolderBrowserDialog myVlcPathFolderBrowserDialog;
        private System.Windows.Forms.GroupBox myMediasControlsGroupBox;
        private System.Windows.Forms.Button myRemoveMediaButton;
        private System.Windows.Forms.Button myAddMediaButton;
        private System.Windows.Forms.OpenFileDialog myAddMediaOpenFileDialog;
        private System.Windows.Forms.TrackBar myPositionTrackBar;
    }
}

