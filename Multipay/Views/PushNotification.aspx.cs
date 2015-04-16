using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.Text;
using System.IO;

namespace Multipay
{
    public partial class PushNotification : System.Web.UI.Page
    {
        //PushBAL bal = new PushBAL();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            DataSet dsregID = new DataSet();
            //dsregID = bal.GetAndroidRegIds();

            //You can send upto 1000 regid's to GCM server once.
            //write loop here for multiple ID's.
            //string regId = dsregID.Tables[0].Columns["RegID"].ToString();

            // string regId = "";
            string regId = "APA91bHntTZ4_Tft21x2qfOEcGja0zV6FzpaWk4G01J0_81IqYc7kin2Bex0a4W-QdHsu4bUFvYlzqTlQ9-WSmxpSngc7lnFOR0UmOoGTYP6dx9IfZadHbyIggsXdcWW7GvIxhLARnVauy8Uv48xxxHsmkurPNZlbw";
            var applicationID = "AIzaSyDyn65Ul3fCpSY7rcOpi8AW5ve-ZFRK0CU";



            var SENDER_ID = "437797444824";
            var value = Text1.Text;
            WebRequest tRequest;
            tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));

            tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

            //Data_Post Format
            // string postData = "{'collapse_key' : 'demo', 'registration_id': [ '" + regId + "' ], 
            //'data': {'message': '" + Label1.Text + "'},'time_to_live' : '3' }";


            string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=0&data.message="
                + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + regId + "";


            Console.WriteLine(postData);
            Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            tRequest.ContentLength = byteArray.Length;

            Stream dataStream = tRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse tResponse = tRequest.GetResponse();

            dataStream = tResponse.GetResponseStream();

            StreamReader tReader = new StreamReader(dataStream);

            String sResponseFromServer = tReader.ReadToEnd();

            Label3.Text = sResponseFromServer; //printing response from GCM server.
            tReader.Close();
            dataStream.Close();
            tResponse.Close();



        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }



    }
}
