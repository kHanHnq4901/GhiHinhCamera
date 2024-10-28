namespace AppTheoDoiCamera
{
    partial class GhiHinhCamera
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GhiHinhCamera));
            button1 = new Button();
            pbCam = new PictureBox();
            cbDevices = new ComboBox();
            buttonCheckDevices = new Button();
            buttonRecord = new Button();
            button4 = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)pbCam).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(31, 11);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Mở cam";
            button1.UseVisualStyleBackColor = true;
            button1.Click += buttonOpenCamera_Click;
            // 
            // pbCam
            // 
            pbCam.Location = new Point(3, 88);
            pbCam.Name = "pbCam";
            pbCam.Size = new Size(640, 480);
            pbCam.TabIndex = 1;
            pbCam.TabStop = false;
            // 
            // cbDevices
            // 
            cbDevices.FormattingEnabled = true;
            cbDevices.Location = new Point(205, 12);
            cbDevices.Name = "cbDevices";
            cbDevices.Size = new Size(121, 23);
            cbDevices.TabIndex = 2;
            // 
            // buttonCheckDevices
            // 
            buttonCheckDevices.Location = new Point(332, 11);
            buttonCheckDevices.Name = "buttonCheckDevices";
            buttonCheckDevices.Size = new Size(75, 23);
            buttonCheckDevices.TabIndex = 0;
            buttonCheckDevices.Text = "Tìm kiếm ";
            buttonCheckDevices.Click += buttonSearchDevices_Click;
            // 
            // buttonRecord
            // 
            buttonRecord.BackColor = Color.Lime;
            buttonRecord.Location = new Point(413, 11);
            buttonRecord.Name = "buttonRecord";
            buttonRecord.Size = new Size(75, 23);
            buttonRecord.TabIndex = 3;
            buttonRecord.Text = "Ghi hình";
            buttonRecord.UseVisualStyleBackColor = false;
            buttonRecord.Click += buttonStartRecording_Click;
            // 
            // button4
            // 
            button4.Location = new Point(494, 11);
            button4.Name = "button4";
            button4.Size = new Size(75, 23);
            button4.TabIndex = 5;
            button4.Text = "Mở file ";
            button4.Click += buttonOpenVideo_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(31, 38);
            label1.Name = "label1";
            label1.Size = new Size(83, 15);
            label1.TabIndex = 6;
            label1.Text = "Độ phân giải : ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(31, 53);
            label2.Name = "label2";
            label2.Size = new Size(113, 15);
            label2.TabIndex = 7;
            label2.Text = "Tốc độ khung hình :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(180, 38);
            label3.Name = "label3";
            label3.Size = new Size(146, 15);
            label3.TabIndex = 8;
            label3.Text = "Tốc độ khung hình tối đa :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(180, 53);
            label4.Name = "label4";
            label4.Size = new Size(95, 15);
            label4.TabIndex = 9;
            label4.Text = "Số bit trên pixel :";
            // 
            // GhiHinhCamera
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(658, 570);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button4);
            Controls.Add(buttonRecord);
            Controls.Add(buttonCheckDevices);
            Controls.Add(cbDevices);
            Controls.Add(pbCam);
            Controls.Add(button1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "GhiHinhCamera";
            Text = "GhiHinhCamera";
            Load += GhiHinhCamera_Load;
            ((System.ComponentModel.ISupportInitialize)pbCam).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private PictureBox pbCam;
        private ComboBox cbDevices;
        private Button buttonCheckDevices;
        private Button buttonRecord;
        private Button button4;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}