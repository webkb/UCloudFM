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

        private string ucloud_put_auth(string method, string query, string mime)
        {
            string options = method + "\n"
                + "" + "\n"
                + mime + "\n"
                + "" + "\n"
                + "/" + APP_ID + "/" + query;
            return APP_KEY + ":" + Convert.ToBase64String(new HMACSHA1(Encoding.UTF8.GetBytes(APP_SECRET)).ComputeHash(Encoding.UTF8.GetBytes(options)));
        }
        private string ucloud_list_auth(string method, string query)
        {
            string options = method + "\n"
                + "" + "\n"
                + "" + "\n"
                + "" + "\n"
                + "/" + APP_ID + "/";
            return APP_KEY + ":" + Convert.ToBase64String(new HMACSHA1(Encoding.UTF8.GetBytes(APP_SECRET)).ComputeHash(Encoding.UTF8.GetBytes(options)));
        }
        private string ucloud_delete_auth(string method, string query)
        {
            string options = method + "\n"
                + "" + "\n"
                + "" + "\n"
                + "" + "\n"
                + "/" + APP_ID + "/" + query;
            return APP_KEY + ":" + Convert.ToBase64String(new HMACSHA1(Encoding.UTF8.GetBytes(APP_SECRET)).ComputeHash(Encoding.UTF8.GetBytes(options)));
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
            string test = "";
            try
            {
                client.DefaultRequestHeaders.Add("Authorization", "UCloud " + ucloud_put_auth("PUT", FileName, FileMime));
                Task<HttpResponseMessage> task = client.PutAsync(APP_HOST + FileName, content);

                test = task.Result.ReasonPhrase.ToString();

                richTextBox1.Text = test;

            }
            catch (Exception yc)
            {
                test = yc.Message;
            }
        }
        private string mime_content_type(string str)
        {
            switch (str)
            {
                case ".323": return "text/h323";
                case ".html": return "text/html";
                case ".txt": return "text/plain";
                case ".bmp": return "image/bmp";
                case ".gif": return "image/gif";
                case ".jpeg": return "image/jpeg";
                case ".jpg": return "image/jpeg";
                case ".png": return "image/png";
                case ".ico": return "image/x-ico";
                default: return "application/octet-stream";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            HttpClient client = new HttpClient();
            string test = "";
            try
            {
                client.DefaultRequestHeaders.Add("Authorization", "UCloud " + ucloud_list_auth("GET", "?list"));
                Task<string> task = client.GetStringAsync(APP_HOST + "?list");

                test = task.Result;
                var stream = new MemoryStream(Encoding.UTF8.GetBytes(test));
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
                test = yc.Message;
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
            string test = "";
            try
            {
                client.DefaultRequestHeaders.Add("Authorization", "UCloud " + ucloud_delete_auth("DELETE", FileName));
                Task<HttpResponseMessage> task = client.DeleteAsync(APP_HOST + FileName);

                test = task.Result.ReasonPhrase.ToString();

                richTextBox1.Text = test;

            }
            catch (Exception yc)
            {
                test = yc.Message;
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