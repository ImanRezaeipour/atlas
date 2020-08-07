using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jSignature.Tools;
using System.Text;
using System.IO;
using System.Drawing.Imaging;
using Svg;
using GTS.Clock.Business.UI;
using System.Drawing;


public partial class DigitalSignature : GTSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page DigitalSignaturePage = this;
        Ajax.Utility.GenerateMethodScripts(DigitalSignaturePage);
    }

    [Ajax.AjaxMethod("SaveImagePage", "SaveImagePage_OnCallback", null, null)]
    public string SaveImagePage(string data)
    {
        try
        {
            Base30Converter conv = new Base30Converter();
            int[][][] bytearray = conv.GetData(data);
            string actual = jSignature.Tools.SVGConverter.ToSVG(bytearray);
            var byteArray = Encoding.ASCII.GetBytes(actual);
            using (var stream = new MemoryStream(byteArray))
            {
                SvgDocument svgDocument = SvgDocument.Open(stream);
                Bitmap bitmap = svgDocument.Draw();
                bitmap.Save(AppDomain.CurrentDomain.BaseDirectory + "DigitalSignatureImages\\" + Guid.NewGuid() + ".png", ImageFormat.Png);
            }
            return "Saved";
        }
        catch (Exception)
        {
            return "Not Saved!";
        }               
    }
}