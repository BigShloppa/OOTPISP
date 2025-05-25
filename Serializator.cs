using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    internal class Serializator
    {
        public List<Bitmap>? states;

        public void Serialize(string filePath) {
            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
               
                writer.Write(states.Count);

                foreach (var bmp in states)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bmp.Save(ms, ImageFormat.Png); 
                        byte[] data = ms.ToArray();

                        writer.Write(data.Length);
                        writer.Write(data);
                    }
                }
            }
        }

        public void Deserialize(string filePath)
        {
            List<Bitmap> bitmaps = new List<Bitmap>();

            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                int count = reader.ReadInt32();

                for (int i = 0; i < count; i++)
                {
                    int length = reader.ReadInt32();
                    byte[] data = reader.ReadBytes(length);

                    using (MemoryStream ms = new MemoryStream(data))
                    {
                        bitmaps.Add(new Bitmap(ms));
                    }
                }
            }

            for (int i = 0; i < bitmaps.Count; i++)
            {
                Bitmap source = bitmaps[i];
                Bitmap compatible = new Bitmap(source.Width, source.Height, PixelFormat.Format32bppArgb);

                using (Graphics g = Graphics.FromImage(compatible))
                {
                    g.DrawImage(source, 0, 0);
                }
                bitmaps[i] = compatible;
            }
            states = [.. bitmaps.ToArray()];
        }
    }
}
