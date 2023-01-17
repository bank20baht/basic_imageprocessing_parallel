using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace prog2017_019_try_load_image04
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string newLine = Environment.NewLine;

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Click on the link below to continue learning how to build a desktop app using WinForms!
            System.Diagnostics.Process.Start("http://aka.ms/dotnet-get-started-desktop");

        }

        Stopwatch stopWatch1 = new Stopwatch();

        Stopwatch stopWatch3 = new Stopwatch();

        Bitmap image1, image2_clone;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------
            //using System.Drawing.Imaging;
            stopWatch1.Reset();
            stopWatch1.Start();
            BitmapData dstData = image1.LockBits(new Rectangle(0, 0, image1.Width, image1.Height), ImageLockMode.ReadWrite, image1.PixelFormat);

            IntPtr ptr = dstData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(dstData.Stride) * image1.Height;

            byte[] rgbValues = new byte[bytes];

            textBox1.AppendText(newLine + newLine + "image width = " + image1.Width.ToString() + "\r\n");
            textBox1.AppendText("image height = " + image1.Height.ToString() + "\r\n");
            textBox1.AppendText("bytes = " + bytes.ToString() + "\r\n");
            textBox1.AppendText("image width*3 = " + (image1.Width * 3).ToString() + "\r\n");
            textBox1.AppendText("dstData.Stride = " + dstData.Stride.ToString() + "\r\n");



            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            for (int y = 0; y < image1.Height; y++)
            {
                int start_line = y * dstData.Stride;
                for (int x = 0; x < image1.Width * 3; x += 3)
                {
                    rgbValues[x + start_line] = 0;  //blue
                    //rgbValues[x + 1 + start_line] = 0;//green
                    rgbValues[x + 2 + start_line] = 0;//red
                }
            }
            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            image1.UnlockBits(dstData);
            stopWatch1.Stop();
            pictureBox1.Image = image1;
            textBox1.AppendText(newLine + newLine + "Time for extracting green component using LockBit = " + stopWatch1.ElapsedMilliseconds.ToString() + " mS\r\n");
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------
            // แบบนี้ใช้ไม่ได้ กับ parallel.for เนื่องจาก GetPixel และ SetPixel จะไปล๊อกหน่วยความจำ ทำให้ไม่สามารถเข้าถึงพร้อมกันได้หลายเธรด
            /*            stopWatch1.Reset();
                        stopWatch1.Start();
                        Parallel.For(0, image1.Height, y =>            
                        {
                            Color pixelColor;
                            Color newColor;                
                            for (int x = 0; x < image1.Width; x++)
                            {
                                pixelColor = image2_clone.GetPixel(x, y);
                                newColor = Color.FromArgb(pixelColor.R, 0, 0);
                                image1.SetPixel(x, y, newColor);
                            }
                        });
                        stopWatch1.Stop();
                        pictureBox1.Image = image1;
                        textBox1.AppendText("Time for extracting Red component(Parallel) = " + stopWatch1.ElapsedMilliseconds.ToString() + " mS\r\n");
                        */
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int x, y;
            //extract only red component

            stopWatch1.Reset();
            stopWatch1.Start();

            for (x = 0; x < image1.Width; x++)
            {
                for (y = 0; y < image1.Height; y++)
                {
                    Color pixelColor = image1.GetPixel(x, y);
                    Color newColor = Color.FromArgb(pixelColor.R, 0, 0);
                    image1.SetPixel(x, y, newColor);
                }
            }
            stopWatch1.Stop();
            pictureBox1.Image = image1;

            textBox1.AppendText(newLine+newLine+"Time for extracting Red component = " + stopWatch1.ElapsedMilliseconds.ToString() + " mS\r\n");

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int x, y;

            //shows only Green component

            stopWatch1.Reset();
            stopWatch1.Start();
            for (x = 0; x < image1.Width; x++)
            {
                for (y = 0; y < image1.Height; y++)
                {
                    Color pixelColor = image1.GetPixel(x, y);
                    Color newColor = Color.FromArgb(0, pixelColor.G, 0);
                    image1.SetPixel(x, y, newColor);
                }
            }

            stopWatch1.Stop();
            pictureBox1.Image = image1;
            textBox1.AppendText(newLine + newLine + "Time for extracting Green component = " + stopWatch1.ElapsedMilliseconds.ToString() + " mS\r\n");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int x, y;

            //shows only Blue component
            stopWatch1.Reset();
            stopWatch1.Start();
            for (x = 0; x < image1.Width; x++)
            {
                for (y = 0; y < image1.Height; y++)
                {
                    Color pixelColor = image1.GetPixel(x, y);
                    Color newColor = Color.FromArgb(0, 0, pixelColor.B);
                    image1.SetPixel(x, y, newColor);
                }
            }

            stopWatch1.Stop();
            pictureBox1.Image = image1;
            textBox1.AppendText(newLine + newLine + "Time for extracting Blue component = " + stopWatch1.ElapsedMilliseconds.ToString() + " mS\r\n");

        }

        


        private void button5_Click(object sender, EventArgs e)
        {
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------
            //using System.Drawing.Imaging;
            stopWatch1.Reset();
            stopWatch1.Start();
            BitmapData dstData = image1.LockBits(new Rectangle(0, 0, image1.Width, image1.Height), ImageLockMode.ReadWrite, image1.PixelFormat);

            IntPtr ptr = dstData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(dstData.Stride) * image1.Height;
            
            byte[] rgbValues = new byte[bytes];            

            textBox1.AppendText(newLine + newLine + "image width = " + image1.Width.ToString() + "\r\n");
            textBox1.AppendText("image height = " + image1.Height.ToString() + "\r\n");
            textBox1.AppendText("bytes = " + bytes.ToString() + "\r\n");
            textBox1.AppendText("image width*3 = " + (image1.Width*3).ToString() + "\r\n");
            textBox1.AppendText("dstData.Stride = " + dstData.Stride.ToString() + "\r\n");



            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            for (int y=0; y<image1.Height; y++)
            {
                int start_line = y * dstData.Stride;
                for (int x = 0; x < image1.Width * 3; x += 3)
                {
                    rgbValues[x + start_line] = 0;  //blue
                    rgbValues[x+1 + start_line] = 0;//green
                    //rgbValues[x+2 + start_line] = 255;//red
                }
            }
            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            image1.UnlockBits(dstData);
            stopWatch1.Stop();
            pictureBox1.Image = image1;
            textBox1.AppendText(newLine + newLine + "Time for extracting Red component using LockBit = " + stopWatch1.ElapsedMilliseconds.ToString() + " mS\r\n");
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------
            // แบบนี้ใช้ไม่ได้ กับ parallel.for เนื่องจาก GetPixel และ SetPixel จะไปล๊อกหน่วยความจำ ทำให้ไม่สามารถเข้าถึงพร้อมกันได้หลายเธรด
            /*            stopWatch1.Reset();
                        stopWatch1.Start();
                        Parallel.For(0, image1.Height, y =>            
                        {
                            Color pixelColor;
                            Color newColor;                
                            for (int x = 0; x < image1.Width; x++)
                            {
                                pixelColor = image2_clone.GetPixel(x, y);
                                newColor = Color.FromArgb(pixelColor.R, 0, 0);
                                image1.SetPixel(x, y, newColor);
                            }
                        });
                        stopWatch1.Stop();
                        pictureBox1.Image = image1;
                        textBox1.AppendText("Time for extracting Red component(Parallel) = " + stopWatch1.ElapsedMilliseconds.ToString() + " mS\r\n");
                        */
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------
            //using System.Drawing.Imaging;
            stopWatch1.Reset();
            stopWatch1.Start();
            BitmapData dstData = image1.LockBits(new Rectangle(0, 0, image1.Width, image1.Height), ImageLockMode.ReadWrite, image1.PixelFormat);

            IntPtr ptr = dstData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(dstData.Stride) * image1.Height;

            byte[] rgbValues = new byte[bytes];

            textBox1.AppendText(newLine + newLine + "image width = " + image1.Width.ToString() + "\r\n");
            textBox1.AppendText("image height = " + image1.Height.ToString() + "\r\n");
            textBox1.AppendText("bytes = " + bytes.ToString() + "\r\n");
            textBox1.AppendText("image width*3 = " + (image1.Width * 3).ToString() + "\r\n");
            textBox1.AppendText("dstData.Stride = " + dstData.Stride.ToString() + "\r\n");



            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            for (int y = 0; y < image1.Height; y++)
            {
                int start_line = y * dstData.Stride;
                for (int x = 0; x < image1.Width * 3; x += 3)
                {
                    //rgbValues[x + start_line] = 0;  //blue
                    rgbValues[x + 1 + start_line] = 0;//green
                    rgbValues[x+2 + start_line] = 0;//red
                }
            }
            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            image1.UnlockBits(dstData);
            stopWatch1.Stop();
            pictureBox1.Image = image1;
            textBox1.AppendText(newLine + newLine + "Time for extracting Blue component using LockBit = " + stopWatch1.ElapsedMilliseconds.ToString() + " mS\r\n");
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------
            // แบบนี้ใช้ไม่ได้ กับ parallel.for เนื่องจาก GetPixel และ SetPixel จะไปล๊อกหน่วยความจำ ทำให้ไม่สามารถเข้าถึงพร้อมกันได้หลายเธรด
            /*            stopWatch1.Reset();
                        stopWatch1.Start();
                        Parallel.For(0, image1.Height, y =>            
                        {
                            Color pixelColor;
                            Color newColor;                
                            for (int x = 0; x < image1.Width; x++)
                            {
                                pixelColor = image2_clone.GetPixel(x, y);
                                newColor = Color.FromArgb(pixelColor.R, 0, 0);
                                image1.SetPixel(x, y, newColor);
                            }
                        });
                        stopWatch1.Stop();
                        pictureBox1.Image = image1;
                        textBox1.AppendText("Time for extracting Red component(Parallel) = " + stopWatch1.ElapsedMilliseconds.ToString() + " mS\r\n");
                        */
        }

        private void button8_Click(object sender, EventArgs e)
        {//extract red using lockbit and parallel.for
        }

        private void button9_Click(object sender, EventArgs e)
        {
        }

        private void button10_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve the image.
                stopWatch1.Reset();
                stopWatch1.Start();

                image1 = new Bitmap(@"lotus.jpg", true);

                stopWatch1.Stop();
                textBox1.AppendText(newLine + newLine + "Time for reading the image from file = " + stopWatch1.ElapsedMilliseconds.ToString() + " mS\r\n");

                RectangleF cloneRect = new RectangleF(0, 0, image1.Width, image1.Height);
                System.Drawing.Imaging.PixelFormat format = image1.PixelFormat;
                image2_clone = image1.Clone(cloneRect, format);

                textBox1.AppendText("Successfully load the image\r\n");
                // Set the PictureBox to display the image.
                pictureBox1.Image = image1;

            }
            catch (ArgumentException)
            {
                MessageBox.Show("There was an error." +
                    "Check the path to the image file.");
            }

        }


    }
}
