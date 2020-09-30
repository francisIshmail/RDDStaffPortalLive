using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;

/// <summary>
/// Author : vishav Man singh 
/// </summary>
public partial class _Default : System.Web.UI.Page
{

    public Byte[] GetFileContent(System.IO.Stream inputstm)
    {
        Stream fs=inputstm;
        BinaryReader br=new BinaryReader(fs);
        Int32 lnt = Convert.ToInt32(fs.Length);
        byte[] bytes = br.ReadBytes(lnt);
        return bytes;
    }

    private void SaveFileAtWebsiteLocation(string saveFileAtWebSitePath)
    {
            String phySavePth;
            HttpPostedFile postFile;

            string ImageName=string.Empty;

            byte[] path;

            string[] keys;

            try{

            string contentType=string.Empty;

            //byte[] imgContent=null;

            string[] PhotoTitle;

            string PhotoTitlename, trimmedNameWithExt;
            int pikMaxFileName = myGlobal.trimFileLength;

            HttpFileCollection files = Request.Files;

            keys = files.AllKeys;

            for (int i = 0; i < files.Count; i++)
            {
                trimmedNameWithExt = "";
                postFile = files[i];
                if (postFile.ContentLength > 0)
                {
                    contentType = postFile.ContentType;
                    path = GetFileContent(postFile.InputStream);
                    ImageName = System.IO.Path.GetFileName(postFile.FileName);
                    PhotoTitle = ImageName.Split('.');
                    PhotoTitlename=PhotoTitle[0];
                    
                    if (PhotoTitlename.Length > pikMaxFileName)
                        PhotoTitlename = PhotoTitlename.Substring(0, pikMaxFileName);

                    trimmedNameWithExt = PhotoTitlename + "." + PhotoTitle[1];

                    phySavePth = Server.MapPath("~" + saveFileAtWebSitePath) + trimmedNameWithExt;
                    postFile.SaveAs(phySavePth);
                }
              }
            }
            catch (Exception ex)
            {
              ex.Message.ToString();
            }
      }

    protected void btnAddPhoto_Click1(object sender, EventArgs e)
    {
        SaveFileAtWebsiteLocation("/excelFileUpload/Marketing/");
    }

}
