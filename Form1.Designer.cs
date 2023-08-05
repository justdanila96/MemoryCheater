namespace MemoryCheater {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            components = new System.ComponentModel.Container();
            RefreshButton = new Button();
            WindowsComboBox = new ComboBox();
            pictureBox1 = new PictureBox();
            CaptureButton = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            statusStrip1 = new StatusStrip();
            infoLbl = new ToolStripStatusLabel();
            trackBar1 = new TrackBar();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            SuspendLayout();
            // 
            // RefreshButton
            // 
            RefreshButton.Location = new Point(12, 12);
            RefreshButton.Name = "RefreshButton";
            RefreshButton.Size = new Size(75, 23);
            RefreshButton.TabIndex = 0;
            RefreshButton.Text = "Обновить";
            RefreshButton.UseVisualStyleBackColor = true;
            RefreshButton.Click += RefreshButton_Click;
            // 
            // WindowsComboBox
            // 
            WindowsComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            WindowsComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            WindowsComboBox.FormattingEnabled = true;
            WindowsComboBox.Location = new Point(93, 12);
            WindowsComboBox.Name = "WindowsComboBox";
            WindowsComboBox.Size = new Size(498, 23);
            WindowsComboBox.TabIndex = 1;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.Location = new Point(12, 41);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(660, 450);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // CaptureButton
            // 
            CaptureButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CaptureButton.Location = new Point(597, 12);
            CaptureButton.Name = "CaptureButton";
            CaptureButton.Size = new Size(75, 23);
            CaptureButton.TabIndex = 3;
            CaptureButton.Text = "Запуск";
            CaptureButton.UseVisualStyleBackColor = true;
            CaptureButton.Click += CaptureButton_Click;
            // 
            // timer1
            // 
            timer1.Interval = 500;
            timer1.Tick += timer1_Tick;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { infoLbl });
            statusStrip1.Location = new Point(0, 545);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(684, 22);
            statusStrip1.TabIndex = 4;
            statusStrip1.Text = "statusStrip1";
            // 
            // infoLbl
            // 
            infoLbl.Name = "infoLbl";
            infoLbl.Size = new Size(112, 17);
            infoLbl.Text = "Добро пожаловать";
            // 
            // trackBar1
            // 
            trackBar1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            trackBar1.Location = new Point(12, 497);
            trackBar1.Maximum = 100;
            trackBar1.Minimum = 1;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(660, 45);
            trackBar1.TabIndex = 5;
            trackBar1.Value = 100;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 567);
            Controls.Add(trackBar1);
            Controls.Add(statusStrip1);
            Controls.Add(CaptureButton);
            Controls.Add(pictureBox1);
            Controls.Add(WindowsComboBox);
            Controls.Add(RefreshButton);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button RefreshButton;
        private ComboBox WindowsComboBox;
        private PictureBox pictureBox1;
        private Button CaptureButton;
        private System.Windows.Forms.Timer timer1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel infoLbl;
        private TrackBar trackBar1;
    }
}