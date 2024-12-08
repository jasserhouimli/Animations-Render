using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Animation_Render
{
    public partial class Form1 : Form
    {
        private TabControl tabControl;
        private TabPage tabPage1;
        private PictureBox pictureBox;
        private Button loadImageButton;
        private OpenFileDialog openFileDialog;
        private List<Image> frames;
        private int currentFrameIndex;

        public Form1()
        {
            InitializeComponent();
            InitializeUI();
            InitializeAnimation();
        }

        private void InitializeUI()
        {
            tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;

            tabPage1 = new TabPage("Animation");
            tabPage1.AutoScroll = true;

            pictureBox = new PictureBox();
            pictureBox.Location = new Point(200, 0);
            pictureBox.Size = new Size(500, 500);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            tabPage1.Controls.Add(pictureBox);

            loadImageButton = new Button();
            loadImageButton.Text = "Load Images";
            loadImageButton.Click += LoadImageButton_Click;
            loadImageButton.Location = new Point(0, 0);
            tabPage1.Controls.Add(loadImageButton);

            tabControl.TabPages.Add(tabPage1);

            Controls.Add(tabControl);
        }

        private void InitializeAnimation()
        {
            frames = new List<Image>();
            LoadDefaultFrames();
            currentFrameIndex = 0;
            timer1.Start();
        }

        private void LoadDefaultFrames()
        {
            frames.Clear();
        }

        private void LoadFramesFromImages(string[] imagePaths)
        {
            frames.Clear();

            foreach (string path in imagePaths)
            {
                Image frame = Image.FromFile(path);
                frames.Add(frame);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            currentFrameIndex++;
            if (currentFrameIndex >= frames.Count)
                currentFrameIndex = 0;
            if (frames.Count > 0)
            {
                pictureBox.Image = frames[currentFrameIndex];
            }
        }

        private void LoadImageButton_Click(object sender, EventArgs e)
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] selectedFiles = openFileDialog.FileNames;
                LoadFramesFromImages(selectedFiles);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "Speed : " + trackBar1.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] selectedFiles = openFileDialog.FileNames;
                LoadFramesFromImages(selectedFiles);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = "Speed : " + trackBar1.Value;
            timer1.Interval = trackBar1.Value;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            trackBar1.Maximum = int.Parse(textBox1.Text);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            currentFrameIndex++;
            if (currentFrameIndex >= frames.Count)
                currentFrameIndex = 0;
            if (frames.Count > 0)
            {
                pictureBox.Image = frames[currentFrameIndex];
            }
        }
    }
}
