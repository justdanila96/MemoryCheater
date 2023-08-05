using OpenCvSharp;
using OpenCvSharp.Extensions;
using CVSize = OpenCvSharp.Size;

namespace MemoryCheater {
    public partial class Form1 : Form {

        private readonly HashSet<Rectangle> rectangles;
        private readonly Mat template;

        public Form1() {

            InitializeComponent();
            using var templateRaw = Properties.Resources._default.ToMat();
            template = templateRaw.CvtColor(ColorConversionCodes.BGR2GRAY);
            rectangles = new();
        }

        private void Form1_Load(object sender, EventArgs e) {

            rectangles.Clear();
            WindowsComboBox.Items.Clear();
            List<WindowData> windows = ScreenCapture.GetOpenWindows();
            foreach (WindowData window in windows) {

                WindowsComboBox.Items.Add(window);
            }

            infoLbl.Text = "Выберите окно для начала захвата";
        }

        private void RefreshButton_Click(object sender, EventArgs e) {

            Form1_Load(null, null);
            pictureBox1.Image?.Dispose();
            pictureBox1.Image = null;
        }

        private void CaptureButton_Click(object sender, EventArgs e) {

            if (timer1.Enabled) {
                timer1.Stop();
                infoLbl.Text = "Захват остановлен";
            }
            else {
                timer1.Start();
                rectangles.Clear();
                pictureBox1.Image?.Dispose();
                pictureBox1.Image = null;
                infoLbl.Text = "Идет захват";
            }
        }

        private HashSet<Rectangle> FindMatches(Bitmap src, float threshold = 0.85f) {

            using Mat matSrcRaw = src.ToMat();
            using Mat matSrc = matSrcRaw.CvtColor(ColorConversionCodes.BGR2GRAY);

            using var matMatches = new Mat();
            Cv2.MatchTemplate(matSrc, template, matMatches, TemplateMatchModes.CCoeffNormed);
            matMatches.GetArray(out float[] matches);

            var globalResult = new HashSet<Rectangle>();
            var locker = new object();

            Parallel.For(0, matches.Length,

                () => new Lazy<HashSet<Rectangle>>(),

                (i, _, localResult) => {

                    if (matches[i] >= threshold) {

                        localResult.Value.Add(new Rectangle {
                            X = i % matMatches.Width,
                            Y = i / matMatches.Width,
                            Width = template.Width,
                            Height = template.Height
                        });
                    }
                    return localResult;
                },

                localResult => {

                    if (localResult.IsValueCreated) {
                        lock (locker) {
                            globalResult.UnionWith(localResult.Value);
                        }
                    }
                });

            return globalResult;
        }

        private void timer1_Tick(object sender, EventArgs e) {

            var selectedWindow = WindowsComboBox.SelectedItem as WindowData;
            if (selectedWindow == null) {
                infoLbl.Text = "Не выбрано окно для захвата";
                timer1.Stop();
                return;
            }

            Bitmap screenshot = ScreenCapture.CaptureWindow(selectedWindow.Ptr);
            HashSet<Rectangle> newRectangles = FindMatches(screenshot);

            if (newRectangles.Count == 0) {

                infoLbl.Text = "Не вижу карточки!";
                screenshot.Dispose();
                return;
            }

            if (rectangles.Count == 0) {
                rectangles.UnionWith(newRectangles);
                pictureBox1.Image?.Dispose();
                pictureBox1.Image = screenshot;
                infoLbl.Text = "Нашел карточки!";
                return;
            }

            if (newRectangles.Count < rectangles.Count) {

                IEnumerable<Rectangle> delta = rectangles.Except(newRectangles);
                using (var G = Graphics.FromImage(pictureBox1.Image)) {
                    foreach (Rectangle rect in delta) {

                        G.DrawImage(screenshot, rect, rect, GraphicsUnit.Pixel);
                    }
                }

                infoLbl.Text = "Подглядел!";
            }

            screenshot.Dispose();
            pictureBox1.Invalidate();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            timer1.Stop();
            template.Dispose();
        }

        private void trackBar1_Scroll(object sender, EventArgs e) {

            using Mat templateRaw = Properties.Resources._default.ToMat();
            using Mat templateOriginal = templateRaw.CvtColor(ColorConversionCodes.BGR2GRAY);
            double ratio = (double)trackBar1.Value / trackBar1.Maximum;
            var sz = new CVSize(templateOriginal.Width * ratio, templateOriginal.Height * ratio);
            Cv2.Resize(templateOriginal, template, sz);
        }
    }
}