using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.IO;

namespace CardGenerationModule
{
    public class CardGenerationModule
    {
        //private static CardGenerationModule objCardGenerationModule = null;
        private Size TemplateImage { get; set; }
        //private Rectangle DivName { get; set; }
        //private Rectangle DivPhone { get; set; }
        //private Rectangle DivSabaqDay { get; set; }
        //private Rectangle DivHalqa { get; set; }
        //private Rectangle DivEjamaat { get; set; }
        //private Rectangle DivBarCode { get; set; }
        //private Size ImageBarCodeSize { get; set; }
        private String DisplayFontName { get; set; }
        private PrivateFontCollection objPrivateFontCollection = null;

        private static CardGenerationModule objCardGenerationModule = null;

        private CardGenerationModule()
        {
            TemplateImage = new Size(1020, 645);


            objPrivateFontCollection = new PrivateFontCollection();

            Stream fontStream = this.GetType().Assembly.GetManifestResourceStream("CardGenerationModule.free3of9.ttf");

            byte[] fontdata = new byte[fontStream.Length];
            fontStream.Read(fontdata, 0, (int)fontStream.Length);
            fontStream.Close();
            unsafe
            {
                fixed (byte* pFontData = fontdata)
                {
                    objPrivateFontCollection.AddMemoryFont((System.IntPtr)pFontData, fontdata.Length);
                }
            }

            DisplayFontName = "Tahoma";
        }

        public static CardGenerationModule Instance
        {
            get
            {
                if (objCardGenerationModule == null)
                    objCardGenerationModule = new CardGenerationModule();

                return objCardGenerationModule;
            }
        }

        public Image GetCardImage(string memberId, string memberName, string halqaname, string sabaqday, string phone)
        {
            //string imgFormatSuffix = imgFormat.ToString().ToLower();
            //string templateImagePath = Server.MapPath("~/sabaq-Card-template.jpg");

            Bitmap myBitmap = Resources.sabaq_Card_template;
            myBitmap = new Bitmap(myBitmap, TemplateImage);
            Graphics g = Graphics.FromImage(myBitmap);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            Rectangle DivName = new Rectangle(110, 310, 440, 50);
            Rectangle DivPhone = new Rectangle(110, 365, 440, 50);
            Rectangle DivSabaqDay = new Rectangle(110, 470, 440, 50);

            Rectangle DivHalqa = new Rectangle(555, 310, 365, 50);
            Rectangle DivEjamaat = new Rectangle(555, 365, 365, 50);
            Rectangle DivBarCode = new Rectangle(555, 435, 365, 100);

            Size ImageBarCodeSize = new Size(DivBarCode.Width, DivBarCode.Height);

            if (!string.IsNullOrEmpty(memberName))
            {
                string _memberName = memberName;
                Font font = new Font(DisplayFontName, 32, FontStyle.Bold);

                if (IsStringSizeGreater(_memberName, font, g, DivName.Size.Width))
                {
                    font = new Font(DisplayFontName, 27, FontStyle.Bold);
                    int nNoOfLines = 0;
                    _memberName = GetFormattedString(_memberName, font, g, DivName.Size.Width, out nNoOfLines);

                    DivName.Height = DivName.Height + (30 * nNoOfLines);
                    DivPhone.Y = DivPhone.Y + (30 * nNoOfLines);
                }

                g.DrawString(_memberName, font, Brushes.Black, DivName);
            }

            if (!string.IsNullOrEmpty(phone))
            {
                g.DrawString(phone, new Font(DisplayFontName, 25), Brushes.Black, DivPhone);
            }

            if (!string.IsNullOrEmpty(sabaqday))
            {
                string _sabaqday = sabaqday.ToUpper();
                Font font = new Font(DisplayFontName, 32);

                if (IsStringSizeGreater(_sabaqday, font, g, DivSabaqDay.Size.Width))
                {
                    font = new Font(DisplayFontName, 27);
                    int nNoOfLines = 0;
                    _sabaqday = GetFormattedString(_sabaqday, font, g, DivSabaqDay.Size.Width, out nNoOfLines);

                    DivSabaqDay.Height = DivSabaqDay.Height + (30 * nNoOfLines);
                }

                g.DrawString(_sabaqday, font, Brushes.Black, DivSabaqDay);
            }
            
            if (!string.IsNullOrEmpty(halqaname))
            {
                String _halqaname = halqaname.ToUpper();
                Font font = new Font(DisplayFontName, 32, FontStyle.Bold);
                DivHalqa.X = DivHalqa.X + GetCenterAllignedX(_halqaname, font, g, DivHalqa.Width);
                g.DrawString(_halqaname, font, Brushes.Black, DivHalqa);
            }

            if (!string.IsNullOrEmpty(memberId))
            {
                String _memberId = memberId;
                Font font = new Font(DisplayFontName, 25, FontStyle.Bold);
                DivEjamaat.X = DivEjamaat.X + GetCenterAllignedX(_memberId, font, g, DivEjamaat.Width);
                g.DrawString(_memberId, font, Brushes.Black, DivEjamaat);
            }

            Bitmap barCode = CreateBarcode("*" + memberId + "*");
            System.Drawing.Image barcodeImage = Image.FromHbitmap(barCode.GetHbitmap());
            g.DrawImage(barcodeImage, DivBarCode);
            return Image.FromHbitmap(myBitmap.GetHbitmap());

            //myBitmap.Save(serverMapPathCards + "\\" + txtBoxEJamaat.Text + "-card." + imgFormatSuffix, imgFormat);
        }

        private string GetFormattedString(string data, Font font, Graphics g, int nWidth, out int noOfLines)
        {
            string _data = data;
            noOfLines = 0;
            if (!IsStringSizeGreater(data, font, g, nWidth))
                return data;

            string[] memberNames = _data.Split(" ".ToArray());

            _data = "";
            foreach (string st in memberNames)
            {
                if (string.IsNullOrEmpty(st.Trim()))
                    continue;

                if (string.IsNullOrEmpty(_data))
                {
                    _data = st;
                    continue;
                }

                if (IsStringSizeGreater(_data + " " + st, font, g, nWidth))
                {
                    _data = _data + "\n\r" + st;
                    noOfLines++;
                }
                else
                {
                    _data = _data + " " + st;
                }
            }

            return _data;
        }

        private int GetCenterAllignedX(string data, Font font, Graphics g, int nWidth)
        {
            int nDiv = 0;
            SizeF fontSize = g.MeasureString(data, font);
            if ((int)fontSize.Width < nWidth)
            {
                int nDifference = nWidth - (int)fontSize.Width;
                nDiv = nDifference / 2;
            }

            return nDiv;
        }

        private bool IsStringSizeGreater(string data, Font font, Graphics g, int nWidth)
        {
            if (g == null) return false;

            SizeF textSize = g.MeasureString(data, font);
            if ((int)textSize.Width > nWidth)
                return true;

            return false;
        }

        public Bitmap CreateBarcode(string data)
        {
            //Create the Bitmap
            Bitmap barcode = new Bitmap(1, 1);

            FontFamily fontFamily = objPrivateFontCollection.Families[0];
            Font threeOfNine = new Font(fontFamily, 60, FontStyle.Regular, GraphicsUnit.Pixel);
            Graphics graphics = Graphics.FromImage(barcode);
            SizeF dataSize = graphics.MeasureString(data, threeOfNine);
            barcode = new Bitmap(barcode, dataSize.ToSize());
            graphics = Graphics.FromImage(barcode);

            graphics.Clear(Color.White);
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;
            graphics.DrawString(data, threeOfNine, new SolidBrush(Color.Black), 0, 0);
            graphics.Flush();
            threeOfNine.Dispose();

            graphics.Dispose();

            return barcode;
        }
    }
}
