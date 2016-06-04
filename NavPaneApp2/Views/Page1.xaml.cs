using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace NavPaneApp2.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page1 : Page
    {
        private byte[] downloadedData;
        public Page1()
        {
            this.InitializeComponent();
            var data = los();
        }

        private async Task<byte[]> los()
        {

            try
            {
                string reqUriString = "https://github.com/MicrosoftEdge/MicrosoftEdge-Documentation/archive/master.zip";
                WebRequest req = WebRequest.Create(new Uri(reqUriString));
                WebResponse response = await req.GetResponseAsync();

                Stream stream = response.GetResponseStream();
                // Download in chunks
                byte[] buffer = new byte[1024];

                int dataLength = (int)response.ContentLength;

                // Download to memory
                MemoryStream memStream = new MemoryStream();
                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                    {
                        break;
                    }
                    else
                    {
                        tb_FileSizeDownloaded.Text = bytesRead.ToString();
                        memStream.Write(buffer, 0, bytesRead);
                    }


                }

                byte[] downloadedData = memStream.ToArray();

                stream.Dispose();
                memStream.Dispose();
            }
            catch (Exception)
            {
                throw;
            }

            return this.downloadedData;
        }


    }

}
