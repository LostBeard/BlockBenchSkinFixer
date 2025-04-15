using System.Drawing.Imaging;

namespace BlockBenchSkinFixer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private static Bitmap? LoadBitmapUnlocked(string fileName, out PixelFormat pixelFormat)
        {
            try
            {
                using (var bm = new Bitmap(fileName))
                {
                    pixelFormat = bm.PixelFormat;
                    return new Bitmap(bm);
                }
            }
            catch
            {
                pixelFormat = default;
                return null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowFixPicker();
        }
        void ShowFixPicker()
        {
            var dialog = new OpenFileDialog()
            {
                Title = "Select a Blockbench skin file",
                Filter = "PNG files (*.png)|*.png",
                Multiselect = true,
            };
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK && dialog.FileNames.Any())
            {
                var fixedCount = 0;
                var skippedCount = 0;
                var failedCount = 0;
                var files = dialog.FileNames;
                foreach (var file in files)
                {
                    try
                    {
                        using var bitmap = LoadBitmapUnlocked(file, out var pixelFormat);
                        if (bitmap == null)
                        {
                            failedCount++;
                            continue;
                        }
                        switch (pixelFormat)
                        {
                            case PixelFormat.Format24bppRgb:
                                {
                                    //using var newBitmap = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppArgb);
                                    //using (var graphics = Graphics.FromImage(newBitmap))
                                    //{
                                    //    graphics.DrawImage(bitmap, 0, 0);
                                    //}
                                    bitmap!.Save(file, ImageFormat.Png);
                                    fixedCount++;
                                }
                                break;
                            default:
                                skippedCount++;
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        failedCount++;
                    }
                }
                MessageBox.Show($"Fixed {fixedCount}, skipped {skippedCount}, failed {failedCount}.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
