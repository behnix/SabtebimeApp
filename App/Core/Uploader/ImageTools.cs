using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using App.Core.Generators;
using App.Core.Securities;
using Microsoft.AspNetCore.Http;

namespace App.Core.Uploader
{
    public class ImageTools
    {
        public static string UploadImageNormal(string oldImage, IFormFile newImage, string defaultImage, string imagePath, bool createThumb, string thumbDir,int sizeWidth)
        {
            if (newImage == null) return oldImage;
            var image = "";
            if (newImage.FileName != defaultImage && oldImage != defaultImage)
            {
                if (newImage.IsImage())
                {
                    image = Path.Combine(Directory.GetCurrentDirectory(), imagePath,
                        oldImage);

                    if (File.Exists(image))
                    {
                        File.Delete(image);
                    }
                }
            }

            if (!newImage.IsImage()) return oldImage;
            oldImage = StringGenerator.GenerateUniqCode() + Path.GetExtension(newImage.FileName);
            image = Path.Combine(Directory.GetCurrentDirectory(), imagePath, oldImage);
            using (var stream = new FileStream(image, FileMode.Create,FileAccess.ReadWrite))
            {
                newImage.CopyTo(stream);
            }

            if (!createThumb) return oldImage;

            var imageResize = new ImageTools();
            var thumbDirection = Path.Combine(Directory.GetCurrentDirectory(), thumbDir, oldImage);
            imageResize.Image_resize(image, thumbDirection, sizeWidth);
            return oldImage;


        }
        public void Image_resize(string image, string outputImagePath, int newWidth)
        {

            const long quality = 50L;

            var sourceBitmap = new Bitmap(image);



            double dblWidthOrigial = sourceBitmap.Width;

            double dblHeigthOrigial = sourceBitmap.Height;

            var relationHeigthWidth = dblHeigthOrigial / dblWidthOrigial;

            var newHeight = (int)(newWidth * relationHeigthWidth);



            //< create Empty Drawarea >

            var new_DrawArea = new Bitmap(newWidth, newHeight);

            //</ create Empty Drawarea >



            using (var graphic_of_DrawArea = Graphics.FromImage(new_DrawArea))

            {

                //< setup >

                graphic_of_DrawArea.CompositingQuality = CompositingQuality.HighSpeed;

                graphic_of_DrawArea.InterpolationMode = InterpolationMode.HighQualityBicubic;

                graphic_of_DrawArea.CompositingMode = CompositingMode.SourceCopy;

                //</ setup >



                //< draw into placeholder >

                //*imports the image into the drawarea

                graphic_of_DrawArea.DrawImage(sourceBitmap, 0, 0, newWidth, newHeight);

                //</ draw into placeholder >



                //--< Output as .Jpg >--

                using (var output = System.IO.File.Open(outputImagePath, FileMode.Create))

                {

                    //< setup jpg >

                    var qualityParamId = System.Drawing.Imaging.Encoder.Quality;

                    var encoderParameters = new EncoderParameters(1);

                    encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);

                    //</ setup jpg >



                    //< save Bitmap as Jpg >

                    var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);

                    new_DrawArea.Save(output, codec, encoderParameters);

                    //resized_Bitmap.Dispose ();

                    output.Close();

                    //</ save Bitmap as Jpg >

                }

                //--</ Output as .Jpg >--

                graphic_of_DrawArea.Dispose();

            }

            sourceBitmap.Dispose();

            //---------------</ Image_resize() >---------------

        }
    }
}
