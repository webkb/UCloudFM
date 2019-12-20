using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string APP_KEY = "";
        private string APP_SECRET = "";
        private string APP_ID = "";
        private string APP_HOST = "";

        private string ucloud_auth(string method, string filename = "", string mime = "")
        {
            string options = $"{method}\n\n{mime}\n\n/{APP_ID}/{filename}";

            string optionsHash = Convert.ToBase64String(new HMACSHA1(Encoding.UTF8.GetBytes(APP_SECRET)).ComputeHash(Encoding.UTF8.GetBytes(options)));

            return "UCloud " + APP_KEY + ":" + optionsHash;
        }
        private string mime_content_type(string str)
        {
            switch (str)
            {
                case ".323": return "text/h323";
                case ".aaf": return "application/octet-stream";
                case ".aca": return "application/octet-stream";
                case ".accdb": return "application/msaccess";
                case ".accde": return "application/msaccess";
                case ".accdt": return "application/msaccess";
                case ".acx": return "application/internet-property-stream";
                case ".afm": return "application/octet-stream";
                case ".ai": return "application/postscript";
                case ".aif": return "audio/x-aiff";
                case ".aifc": return "audio/aiff";
                case ".aiff": return "audio/aiff";
                case ".application": return "application/x-ms-application";
                case ".art": return "image/x-jg";
                case ".asd": return "application/octet-stream";
                case ".asf": return "video/x-ms-asf";
                case ".asi": return "application/octet-stream";
                case ".asm": return "text/plain";
                case ".asr": return "video/x-ms-asf";
                case ".asx": return "video/x-ms-asf";
                case ".atom": return "application/atom+xml";
                case ".au": return "audio/basic";
                case ".avi": return "video/x-msvideo";
                case ".axs": return "application/olescript";
                case ".bas": return "text/plain";
                case ".bcpio": return "application/x-bcpio";
                case ".bin": return "application/octet-stream";
                case ".bmp": return "image/bmp";
                case ".c": return "text/plain";
                case ".cab": return "application/octet-stream";
                case ".calx": return "application/vnd.ms-office.calx";
                case ".cat": return "application/vnd.ms-pki.seccat";
                case ".cdf": return "application/x-cdf";
                case ".chm": return "application/octet-stream";
                case ".class": return "application/x-java-applet";
                case ".clp": return "application/x-msclip";
                case ".cmx": return "image/x-cmx";
                case ".cnf": return "text/plain";
                case ".cod": return "image/cis-cod";
                case ".cpio": return "application/x-cpio";
                case ".cpp": return "text/plain";
                case ".crd": return "application/x-mscardfile";
                case ".crl": return "application/pkix-crl";
                case ".crt": return "application/x-x509-ca-cert";
                case ".csh": return "application/x-csh";
                case ".css": return "text/css";
                case ".csv": return "application/octet-stream";
                case ".cur": return "application/octet-stream";
                case ".dcr": return "application/x-director";
                case ".deploy": return "application/octet-stream";
                case ".der": return "application/x-x509-ca-cert";
                case ".dib": return "image/bmp";
                case ".dir": return "application/x-director";
                case ".disco": return "text/xml";
                case ".dll": return "application/x-msdownload";
                case ".dll.config": return "text/xml";
                case ".dlm": return "text/dlm";
                case ".doc": return "application/msword";
                case ".docm": return "application/vnd.ms-word.document.macroEnabled.12";
                case ".docx": return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case ".dot": return "application/msword";
                case ".dotm": return "application/vnd.ms-word.template.macroEnabled.12";
                case ".dotx": return "application/vnd.openxmlformats-officedocument.wordprocessingml.template";
                case ".dsp": return "application/octet-stream";
                case ".dtd": return "text/xml";
                case ".dvi": return "application/x-dvi";
                case ".dwf": return "drawing/x-dwf";
                case ".dwp": return "application/octet-stream";
                case ".dxr": return "application/x-director";
                case ".eml": return "message/rfc822";
                case ".emz": return "application/octet-stream";
                case ".eot": return "application/octet-stream";
                case ".eps": return "application/postscript";
                case ".etx": return "text/x-setext";
                case ".evy": return "application/envoy";
                case ".exe": return "application/octet-stream";
                case ".exe.config": return "text/xml";
                case ".fdf": return "application/vnd.fdf";
                case ".fif": return "application/fractals";
                case ".fla": return "application/octet-stream";
                case ".flr": return "x-world/x-vrml";
                case ".flv": return "video/x-flv";
                case ".gif": return "image/gif";
                case ".gtar": return "application/x-gtar";
                case ".gz": return "application/x-gzip";
                case ".h": return "text/plain";
                case ".hdf": return "application/x-hdf";
                case ".hdml": return "text/x-hdml";
                case ".hhc": return "application/x-oleobject";
                case ".hhk": return "application/octet-stream";
                case ".hhp": return "application/octet-stream";
                case ".hlp": return "application/winhlp";
                case ".hqx": return "application/mac-binhex40";
                case ".hta": return "application/hta";
                case ".htc": return "text/x-component";
                case ".htm": return "text/html";
                case ".html": return "text/html";
                case ".htt": return "text/webviewhtml";
                case ".hxt": return "text/html";
                case ".ico": return "image/x-icon";
                case ".ics": return "application/octet-stream";
                case ".ief": return "image/ief";
                case ".iii": return "application/x-iphone";
                case ".inf": return "application/octet-stream";
                case ".ins": return "application/x-internet-signup";
                case ".isp": return "application/x-internet-signup";
                case ".IVF": return "video/x-ivf";
                case ".jar": return "application/java-archive";
                case ".java": return "application/octet-stream";
                case ".jck": return "application/liquidmotion";
                case ".jcz": return "application/liquidmotion";
                case ".jfif": return "image/pjpeg";
                case ".jpb": return "application/octet-stream";
                case ".jpe": return "image/jpeg";
                case ".jpeg": return "image/jpeg";
                case ".jpg": return "image/jpeg";
                case ".js": return "application/x-javascript";
                case ".jsx": return "text/jscript";
                case ".latex": return "application/x-latex";
                case ".lit": return "application/x-ms-reader";
                case ".lpk": return "application/octet-stream";
                case ".lsf": return "video/x-la-asf";
                case ".lsx": return "video/x-la-asf";
                case ".lzh": return "application/octet-stream";
                case ".m13": return "application/x-msmediaview";
                case ".m14": return "application/x-msmediaview";
                case ".m1v": return "video/mpeg";
                case ".m3u": return "audio/x-mpegurl";
                case ".man": return "application/x-troff-man";
                case ".manifest": return "application/x-ms-manifest";
                case ".map": return "text/plain";
                case ".mdb": return "application/x-msaccess";
                case ".mdp": return "application/octet-stream";
                case ".me": return "application/x-troff-me";
                case ".mht": return "message/rfc822";
                case ".mhtml": return "message/rfc822";
                case ".mid": return "audio/mid";
                case ".midi": return "audio/mid";
                case ".mix": return "application/octet-stream";
                case ".mmf": return "application/x-smaf";
                case ".mno": return "text/xml";
                case ".mny": return "application/x-msmoney";
                case ".mov": return "video/quicktime";
                case ".movie": return "video/x-sgi-movie";
                case ".mp2": return "video/mpeg";
                case ".mp3": return "audio/mpeg";
                case ".mpa": return "video/mpeg";
                case ".mpe": return "video/mpeg";
                case ".mpeg": return "video/mpeg";
                case ".mpg": return "video/mpeg";
                case ".mpp": return "application/vnd.ms-project";
                case ".mpv2": return "video/mpeg";
                case ".ms": return "application/x-troff-ms";
                case ".msi": return "application/octet-stream";
                case ".mso": return "application/octet-stream";
                case ".mvb": return "application/x-msmediaview";
                case ".mvc": return "application/x-miva-compiled";
                case ".nc": return "application/x-netcdf";
                case ".nsc": return "video/x-ms-asf";
                case ".nws": return "message/rfc822";
                case ".ocx": return "application/octet-stream";
                case ".oda": return "application/oda";
                case ".odc": return "text/x-ms-odc";
                case ".ods": return "application/oleobject";
                case ".one": return "application/onenote";
                case ".onea": return "application/onenote";
                case ".onetoc": return "application/onenote";
                case ".onetoc2": return "application/onenote";
                case ".onetmp": return "application/onenote";
                case ".onepkg": return "application/onenote";
                case ".osdx": return "application/opensearchdescription+xml";
                case ".p10": return "application/pkcs10";
                case ".p12": return "application/x-pkcs12";
                case ".p7b": return "application/x-pkcs7-certificates";
                case ".p7c": return "application/pkcs7-mime";
                case ".p7m": return "application/pkcs7-mime";
                case ".p7r": return "application/x-pkcs7-certreqresp";
                case ".p7s": return "application/pkcs7-signature";
                case ".pbm": return "image/x-portable-bitmap";
                case ".pcx": return "application/octet-stream";
                case ".pcz": return "application/octet-stream";
                case ".pdf": return "application/pdf";
                case ".pfb": return "application/octet-stream";
                case ".pfm": return "application/octet-stream";
                case ".pfx": return "application/x-pkcs12";
                case ".pgm": return "image/x-portable-graymap";
                case ".pko": return "application/vnd.ms-pki.pko";
                case ".pma": return "application/x-perfmon";
                case ".pmc": return "application/x-perfmon";
                case ".pml": return "application/x-perfmon";
                case ".pmr": return "application/x-perfmon";
                case ".pmw": return "application/x-perfmon";
                case ".png": return "image/png";
                case ".pnm": return "image/x-portable-anymap";
                case ".pnz": return "image/png";
                case ".pot": return "application/vnd.ms-powerpoint";
                case ".potm": return "application/vnd.ms-powerpoint.template.macroEnabled.12";
                case ".potx": return "application/vnd.openxmlformats-officedocument.presentationml.template";
                case ".ppam": return "application/vnd.ms-powerpoint.addin.macroEnabled.12";
                case ".ppm": return "image/x-portable-pixmap";
                case ".pps": return "application/vnd.ms-powerpoint";
                case ".ppsm": return "application/vnd.ms-powerpoint.slideshow.macroEnabled.12";
                case ".ppsx": return "application/vnd.openxmlformats-officedocument.presentationml.slideshow";
                case ".ppt": return "application/vnd.ms-powerpoint";
                case ".pptm": return "application/vnd.ms-powerpoint.presentation.macroEnabled.12";
                case ".pptx": return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                case ".prf": return "application/pics-rules";
                case ".prm": return "application/octet-stream";
                case ".prx": return "application/octet-stream";
                case ".ps": return "application/postscript";
                case ".psd": return "application/octet-stream";
                case ".psm": return "application/octet-stream";
                case ".psp": return "application/octet-stream";
                case ".pub": return "application/x-mspublisher";
                case ".qt": return "video/quicktime";
                case ".qtl": return "application/x-quicktimeplayer";
                case ".qxd": return "application/octet-stream";
                case ".ra": return "audio/x-pn-realaudio";
                case ".ram": return "audio/x-pn-realaudio";
                case ".rar": return "application/octet-stream";
                case ".ras": return "image/x-cmu-raster";
                case ".rf": return "image/vnd.rn-realflash";
                case ".rgb": return "image/x-rgb";
                case ".rm": return "application/vnd.rn-realmedia";
                case ".rmi": return "audio/mid";
                case ".roff": return "application/x-troff";
                case ".rpm": return "audio/x-pn-realaudio-plugin";
                case ".rtf": return "application/rtf";
                case ".rtx": return "text/richtext";
                case ".scd": return "application/x-msschedule";
                case ".sct": return "text/scriptlet";
                case ".sea": return "application/octet-stream";
                case ".setpay": return "application/set-payment-initiation";
                case ".setreg": return "application/set-registration-initiation";
                case ".sgml": return "text/sgml";
                case ".sh": return "application/x-sh";
                case ".shar": return "application/x-shar";
                case ".sit": return "application/x-stuffit";
                case ".sldm": return "application/vnd.ms-powerpoint.slide.macroEnabled.12";
                case ".sldx": return "application/vnd.openxmlformats-officedocument.presentationml.slide";
                case ".smd": return "audio/x-smd";
                case ".smi": return "application/octet-stream";
                case ".smx": return "audio/x-smd";
                case ".smz": return "audio/x-smd";
                case ".snd": return "audio/basic";
                case ".snp": return "application/octet-stream";
                case ".spc": return "application/x-pkcs7-certificates";
                case ".spl": return "application/futuresplash";
                case ".src": return "application/x-wais-source";
                case ".ssm": return "application/streamingmedia";
                case ".sst": return "application/vnd.ms-pki.certstore";
                case ".stl": return "application/vnd.ms-pki.stl";
                case ".sv4cpio": return "application/x-sv4cpio";
                case ".sv4crc": return "application/x-sv4crc";
                case ".swf": return "application/x-shockwave-flash";
                case ".t": return "application/x-troff";
                case ".tar": return "application/x-tar";
                case ".tcl": return "application/x-tcl";
                case ".tex": return "application/x-tex";
                case ".texi": return "application/x-texinfo";
                case ".texinfo": return "application/x-texinfo";
                case ".tgz": return "application/x-compressed";
                case ".thmx": return "application/vnd.ms-officetheme";
                case ".thn": return "application/octet-stream";
                case ".tif": return "image/tiff";
                case ".tiff": return "image/tiff";
                case ".toc": return "application/octet-stream";
                case ".tr": return "application/x-troff";
                case ".trm": return "application/x-msterminal";
                case ".tsv": return "text/tab-separated-values";
                case ".ttf": return "application/octet-stream";
                case ".txt": return "text/plain";
                case ".u32": return "application/octet-stream";
                case ".uls": return "text/iuls";
                case ".ustar": return "application/x-ustar";
                case ".vbs": return "text/vbscript";
                case ".vcf": return "text/x-vcard";
                case ".vcs": return "text/plain";
                case ".vdx": return "application/vnd.ms-visio.viewer";
                case ".vml": return "text/xml";
                case ".vsd": return "application/vnd.visio";
                case ".vss": return "application/vnd.visio";
                case ".vst": return "application/vnd.visio";
                case ".vsto": return "application/x-ms-vsto";
                case ".vsw": return "application/vnd.visio";
                case ".vsx": return "application/vnd.visio";
                case ".vtx": return "application/vnd.visio";
                case ".wav": return "audio/wav";
                case ".wax": return "audio/x-ms-wax";
                case ".wbmp": return "image/vnd.wap.wbmp";
                case ".wcm": return "application/vnd.ms-works";
                case ".wdb": return "application/vnd.ms-works";
                case ".wks": return "application/vnd.ms-works";
                case ".wm": return "video/x-ms-wm";
                case ".wma": return "audio/x-ms-wma";
                case ".wmd": return "application/x-ms-wmd";
                case ".wmf": return "application/x-msmetafile";
                case ".wml": return "text/vnd.wap.wml";
                case ".wmlc": return "application/vnd.wap.wmlc";
                case ".wmls": return "text/vnd.wap.wmlscript";
                case ".wmlsc": return "application/vnd.wap.wmlscriptc";
                case ".wmp": return "video/x-ms-wmp";
                case ".wmv": return "video/x-ms-wmv";
                case ".wmx": return "video/x-ms-wmx";
                case ".wmz": return "application/x-ms-wmz";
                case ".wps": return "application/vnd.ms-works";
                case ".wri": return "application/x-mswrite";
                case ".wrl": return "x-world/x-vrml";
                case ".wrz": return "x-world/x-vrml";
                case ".wsdl": return "text/xml";
                case ".wvx": return "video/x-ms-wvx";
                case ".x": return "application/directx";
                case ".xaf": return "x-world/x-vrml";
                case ".xaml": return "application/xaml+xml";
                case ".xap": return "application/x-silverlight-app";
                case ".xbap": return "application/x-ms-xbap";
                case ".xbm": return "image/x-xbitmap";
                case ".xdr": return "text/plain";
                case ".xla": return "application/vnd.ms-excel";
                case ".xlam": return "application/vnd.ms-excel.addin.macroEnabled.12";
                case ".xlc": return "application/vnd.ms-excel";
                case ".xlm": return "application/vnd.ms-excel";
                case ".xls": return "application/vnd.ms-excel";
                case ".xlsb": return "application/vnd.ms-excel.sheet.binary.macroEnabled.12";
                case ".xlsm": return "application/vnd.ms-excel.sheet.macroEnabled.12";
                case ".xlsx": return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case ".xlt": return "application/vnd.ms-excel";
                case ".xltm": return "application/vnd.ms-excel.template.macroEnabled.12";
                case ".xltx": return "application/vnd.openxmlformats-officedocument.spreadsheetml.template";
                case ".xlw": return "application/vnd.ms-excel";
                case ".xml": return "text/xml";
                case ".xof": return "x-world/x-vrml";
                case ".xpm": return "image/x-xpixmap";
                case ".xps": return "application/vnd.ms-xpsdocument";
                case ".xsd": return "text/xml";
                case ".xsf": return "text/xml";
                case ".xsl": return "text/xml";
                case ".xslt": return "text/xml";
                case ".xsn": return "application/octet-stream";
                case ".xtp": return "application/octet-stream";
                case ".xwd": return "image/x-xwindowdump";
                case ".z": return "application/x-compress";
                case ".zip": return "application/x-zip-compressed";
                default: return "application/octet-stream";
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog FileDialog = new OpenFileDialog();
            FileDialog.Multiselect = false;
            FileDialog.Title = "请选择文件";
            FileDialog.Filter = "所有文件(*.*)|*.*";
            if (FileDialog.ShowDialog() == DialogResult.OK)
            {
                upload_file(FileDialog.FileName);
            }
        }
        private void upload_file(string FilePath)
        {
            FileStream fs = File.OpenRead(@FilePath);
            FileInfo fi = new FileInfo(@FilePath);
            string FileName = fi.Name;
            string FileMime = mime_content_type(fi.Extension);

            HttpContent content = new StreamContent(fs);
            content.Headers.ContentType = new MediaTypeHeaderValue(FileMime);

            HttpClient client = new HttpClient();
            try
            {
                client.DefaultRequestHeaders.Add("Authorization", ucloud_auth("PUT", FileName, FileMime));
                Task<HttpResponseMessage> task = client.PutAsync(APP_HOST + FileName, content);

                richTextBox1.Text = task.Result.ReasonPhrase.ToString();

            }
            catch (Exception yc)
            {
                richTextBox1.Text = yc.Message;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            HttpClient client = new HttpClient();
            try
            {
                client.DefaultRequestHeaders.Add("Authorization", ucloud_auth("GET"));
                Task<string> task = client.GetStringAsync(APP_HOST + "?list");

                var stream = new MemoryStream(Encoding.UTF8.GetBytes(task.Result));
                var serializer = new DataContractJsonSerializer(typeof(UcloudList));
                var rootObject = (UcloudList)serializer.ReadObject(stream);
                stream.Close();
                foreach (DataSet item in rootObject.DataSet)
                {
                    string FileName = item.FileName;
                    string Size = item.Size.ToString();
                    ListViewItem lv_item = new ListViewItem(FileName);
                    lv_item.SubItems.Add(Size);
                    listView1.Items.Add(lv_item);
                }
                richTextBox1.Text = task.Result.ToString();

            }
            catch (Exception yc)
            {
                richTextBox1.Text = yc.Message;
            }
        }
        [DataContract]
        public class UcloudList
        {
            [DataMember(Name = "BucketName")]
            public string BucketName { get; set; }
            [DataMember(Name = "BucketId")]
            public string BucketId { get; set; }
            [DataMember(Name = "NextMarker")]
            public string NextMarker { get; set; }
            [DataMember(Name = "DataSet")]
            public List<DataSet> DataSet { get; set; }
        }
        [DataContract]
        public class DataSet

        {
            [DataMember(Name = "BucketName")]
            public string BucketName { get; set; }
            [DataMember(Name = "FileName")]
            public string FileName { get; set; }
            [DataMember(Name = "Hash")]
            public string Hash { get; set; }
            [DataMember(Name = "MimeType")]
            public string MimeType { get; set; }
            [DataMember(Name = "first_object")]
            public string first_object { get; set; }
            [DataMember(Name = "Size")]
            public int Size { get; set; }
            [DataMember(Name = "CreateTime")]
            public int CreateTime { get; set; }
            [DataMember(Name = "ModifyTime")]
            public int ModifyTime { get; set; }
            [DataMember(Name = "StorageClass")]
            public string StorageClass { get; set; }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (dynamic item in listView1.SelectedItems)
            {
                string FileName = item.SubItems[0].Text;
                delete_file(FileName);
                listView1.Items.Remove(item);
            }
        }
        private void delete_file(string FileName)
        {

            HttpClient client = new HttpClient();

            try
            {
                client.DefaultRequestHeaders.Add("Authorization", ucloud_auth("DELETE", FileName));
                Task<HttpResponseMessage> task = client.DeleteAsync(APP_HOST + FileName);

                richTextBox1.Text = task.Result.ReasonPhrase.ToString();
            }
            catch (Exception yc)
            {
                richTextBox1.Text = yc.Message;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string cfg_path = Directory.GetCurrentDirectory() + "\\UCloudFM.cfg";
            if (File.Exists(cfg_path))
            {
                string[] FileText = File.ReadAllLines(cfg_path);
                APP_KEY = FileText[0].Replace("APP_KEY=", "");
                APP_SECRET = FileText[1].Replace("APP_SECRET=", "");
                APP_ID = FileText[2].Replace("APP_ID=", "");
                APP_HOST = FileText[3].Replace("APP_HOST=", "");
            }
            else
            {
                richTextBox1.Text = "需要配置文件";
            }
        }


    }


}