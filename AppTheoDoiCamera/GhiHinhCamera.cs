using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using OpenCvSharp;

namespace AppTheoDoiCamera
{
    public partial class GhiHinhCamera : Form
    {
        private FilterInfoCollection videoDevices; // List of video devices
        private VideoCaptureDevice videoSource; // Video capturing device
        private VideoWriter videoWriter; // Video writer object
        private bool isRecording = false; // Flag to check recording status
        private int fps;
        OpenCvSharp.Size dsize = new OpenCvSharp.Size(640, 480);

        public GhiHinhCamera()
        {
            InitializeComponent();
            cbDevices.DropDownStyle = ComboBoxStyle.DropDownList; // Only allow selection from the list
        }

        // Scan and populate available camera devices
        private void Function_ScanCameraIput()
        {
            cbDevices.Items.Clear(); // Clear the current list in the combo box

            // Get the list of video devices
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in videoDevices)
            {
                cbDevices.Items.Add(device.Name); // Add device names to the combo box
            }

            if (cbDevices.Items.Count == 0)
            {
                MessageBox.Show("Không tìm thấy thiết bị camera nào.");
            }
            else
            {
                cbDevices.SelectedIndex = 0; // Select the first device in the list
            }
        }

        // Search for video devices
        private void buttonSearchDevices_Click(object sender, EventArgs e)
        {
            Function_ScanCameraIput(); // Use the scanning method for consistency
        }

        // Open the selected camera
        private void buttonOpenCamera_Click(object sender, EventArgs e)
        {
            if (cbDevices.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn một thiết bị camera trước.");
                return;
            }

            int deviceIndex = cbDevices.SelectedIndex;

            try
            {
                // Initialize the video source with the selected camera
                videoSource = new VideoCaptureDevice(videoDevices[deviceIndex].MonikerString);
                videoSource.NewFrame += new NewFrameEventHandler(Video_NewFrame);
                videoSource.Start();

                System.Threading.Thread.Sleep(500);

                if (videoSource.IsRunning)
                {
                    MessageBox.Show("Camera is running.");
                    Debug.WriteLine("Camera is running.");
                    ShowCameraInfo();
                }
                else
                {
                    MessageBox.Show("Camera failed to start.");
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Lỗi: Không thể kết nối với thiết bị camera đã chọn. Vui lòng thử lại.");
                Debug.WriteLine($"Camera connection error: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi không xác định khi khởi động camera.");
                Debug.WriteLine($"Unknown error when starting camera: {ex.Message}");
            }
        }

        // Show camera format information
        private void ShowCameraInfo()
        {
            if (cbDevices.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn một thiết bị camera trước.");
                return;
            }

            int deviceIndex = cbDevices.SelectedIndex;
            videoSource = new VideoCaptureDevice(videoDevices[deviceIndex].MonikerString);

            var capabilities = videoSource.VideoCapabilities;
            if (capabilities.Length > 0)
            {
                var defaultFormat = capabilities[0];
                label1.Text = $"Độ phân giải: {defaultFormat.FrameSize.Width} x {defaultFormat.FrameSize.Height}\n";
                label2.Text = $"Tốc độ khung hình: {defaultFormat.AverageFrameRate} FPS\n";
                label3.Text = $"Tốc độ khung hình tối đa: {defaultFormat.MaximumFrameRate} FPS\n";
                label4.Text = $"Số bit trên pixel: {defaultFormat.BitCount}\n";
                fps = defaultFormat.MaximumFrameRate;
            }
            else
            {
                MessageBox.Show("Không tìm thấy định dạng video cho camera.");
                Debug.WriteLine("No video format found for the camera.");
            }
        }

        // Handle new frame from the camera
        private void Video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap frame = eventArgs.Frame;

            using (var mat = OpenCvSharp.Extensions.BitmapConverter.ToMat(frame))
            {
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Cv2.PutText(mat, timestamp, new OpenCvSharp.Point(100, 120), HersheyFonts.HersheySimplex, 5.0, new Scalar(0, 255, 0), 5);
                Cv2.Resize(mat, mat, dsize);

                if (isRecording && videoWriter != null)
                {
                    videoWriter.Write(mat);
                    Debug.WriteLine("Frame with timestamp written to video at: " + DateTime.Now);
                }

                // Check if PictureBox is not disposed before updating its image
                if (!pbCam.IsDisposed)
                {
                    pbCam.Invoke(new Action(() =>
                    {
                        pbCam.Image?.Dispose();
                        pbCam.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(mat);
                    }));
                }
            }
        }


        // Start video recording
        public const string strDefineMode = "MP4_MODE";
        private void buttonStartRecording_Click(object sender, EventArgs e)
        {
            if (!isRecording)
            {
                string outputFileName = (strDefineMode == "AVI_MODE") ? "output_video.avi" : "output_video.mp4";
                FourCC codec = (strDefineMode == "AVI_MODE") ? FourCC.MJPG : FourCC.H264;

                videoWriter = new VideoWriter(outputFileName, codec, fps, dsize, true);

                if (!videoWriter.IsOpened())
                {
                    MessageBox.Show("Không thể mở video writer.");
                    Debug.WriteLine("Video writer could not be opened.");
                    return;
                }

                buttonRecord.Text = "Dừng Ghi";
                buttonRecord.BackColor = Color.Red;
                isRecording = true;
                MessageBox.Show("Video recording started at: " + DateTime.Now);
            }
            else
            {
                buttonStopRecording_Click(sender, e);
            }
        }

        // Stop video recording
        private void buttonStopRecording_Click(object sender, EventArgs e)
        {
            if (isRecording)
            {
                isRecording = false;
                videoWriter?.Release();
                videoWriter = null;

                buttonRecord.Text = "Ghi Hình";
                buttonRecord.BackColor = Color.Green;
                MessageBox.Show("Đã dừng ghi video.");
                Debug.WriteLine("Video recording stopped at: " + DateTime.Now);
            }
            else
            {
                MessageBox.Show("Không có video nào đang được ghi.");
                Debug.WriteLine("Attempted to stop recording, but no recording was in progress.");
            }
        }

        // Open the recorded video file
        private void buttonOpenVideo_Click(object sender, EventArgs e)
        {
            string videoFilePath = "output_video.mp4";
            if (System.IO.File.Exists(videoFilePath))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = videoFilePath,
                    UseShellExecute = true
                });
            }
            else
            {
                MessageBox.Show("Không tìm thấy tệp video!");
                Debug.WriteLine("Video file not found.");
            }
        }

        // Stop the camera and release resources when the form closes
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Dừng và giải phóng video source nếu đang chạy
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
                videoSource = null; // Giải phóng tham chiếu
            }

            // Giải phóng video writer nếu tồn tại
            videoWriter?.Release();
            videoWriter = null;

            // Giải phóng PictureBox nếu cần
            pbCam?.Dispose();
            pbCam = null;

            Debug.WriteLine("Form đã đóng. Tài nguyên đã được giải phóng.");

            // Thoát ứng dụng
            Environment.Exit(0);
        }



        private void Form1_Load(object sender, EventArgs e)
        {
          
        }
    }
}
