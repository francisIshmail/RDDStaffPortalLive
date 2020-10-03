using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

public partial class CreateCaptcha : System.Web.UI.Page
{
    private Random rand = new Random();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CreateCaptchaImage();
        }
    }
    /// <summary>
    /// method for create captcha image
    /// </summary>
    private void CreateCaptchaImage()
    {
        string code = string.Empty;
        if (Request.QueryString["New"].ToString() == "1" && Session["RDDCaptchaCode"] == null)
        {
            code = GetRandomText();
        }
        else
        {
            code = Session["RDDCaptchaCode"].ToString();
        }

        Bitmap bitmap = new Bitmap(200, 60, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        Graphics g = Graphics.FromImage(bitmap);
        Pen pen = new Pen(Color.Yellow);
        Rectangle rect = new Rectangle(0, 0, 200, 60);

        //SolidBrush blue = new SolidBrush(Color.CornflowerBlue);
        //SolidBrush blue = new SolidBrush(Color.BurlyWood);
        SolidBrush blue = new SolidBrush(Color.LavenderBlush);
        //SolidBrush blue = new SolidBrush(Color.LightSlateGray);
        SolidBrush black = new SolidBrush(Color.Black);

        int counter = 0;

        g.DrawRectangle(pen, rect);
        g.FillRectangle(blue, rect);

        for (int i = 0; i < code.Length; i++)
        {
            g.DrawString(code[i].ToString(), new Font("Tahoma", 10 + rand.Next(15, 20), FontStyle.Italic), black, new PointF(10 + counter, 10));
            counter += 28;
        }

        DrawRandomLines(g);
        bitmap.Save(Response.OutputStream, ImageFormat.Gif);

        g.Dispose();
        bitmap.Dispose();

    }
    /// <summary>
    /// Method for drawing lines
    /// </summary>
    /// <param name="g"></param>
    private void DrawRandomLines(Graphics g)
    {
        SolidBrush yellow = new SolidBrush(Color.Yellow);
        for (int i = 0; i < 20; i++)
        {
            g.DrawLines(new Pen(yellow, 1), GetRandomPoints());
        }

    }

    /// <summary>
    /// method for gettting random point position
    /// </summary>
    /// <returns></returns>
    private Point[] GetRandomPoints()
    {
        Point[] points = { new Point(rand.Next(0, 150), rand.Next(1, 150)), new Point(rand.Next(0, 200), rand.Next(1, 190)) };
        return points;
    }
    /// <summary>
    /// Method for generating random text of 5 cahrecters as captcha code
    /// </summary>
    /// <returns></returns>
    private string GetRandomText()
    {
        StringBuilder randomText = new StringBuilder();
        string alphabets = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        Random r = new Random();
        for (int j = 0; j <= 4; j++)
        {
            randomText.Append(alphabets[r.Next(alphabets.Length)]);
        }
        Session["RDDCaptchaCode"] = randomText.ToString();
        return Session["RDDCaptchaCode"] as String;
    }
}