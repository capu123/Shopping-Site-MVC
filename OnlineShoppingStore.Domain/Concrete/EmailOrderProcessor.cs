using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using OnlineShoppingStore.Domain.Abstract;
using OnlineShoppingStore.Domain.Entities;
using System.Diagnostics;
using WebApplicationSMSfree;
using IntelliSoftware;

namespace OnlineShoppingStore.Domain.Concrete
{
    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
        {
            string a = null;




            StringBuilder body = new StringBuilder()
                .AppendLine("---")
                .AppendLine("Items:");
            foreach (var line in cart.Lines)
            {
                var subtotal = line.Product.Price * line.Quantity;
                body.AppendFormat("{0} x {1} (subtotal: {2:c})\n",
                    line.Quantity,
                    line.Product.Name,
                    subtotal);
            }
            body.AppendFormat("\n \n Total order value: {0:c}",
                cart.ComputeTotalValue())
                .AppendLine("\n--- \n")
                .AppendLine("Deliver to:")


                .AppendLine(shippingInfo.HouseNo)
                .AppendLine(shippingInfo.Street ?? "")

                .AppendLine(shippingInfo.City)
                .AppendLine(shippingInfo.Postcode ?? "")
                .AppendLine(shippingInfo.PhoneNo)
                .AppendLine(shippingInfo.UserEmail)


                .AppendLine("---")
                .AppendFormat("Gift wrap: {0}",
                    shippingInfo.GiftWrap ? "Yes" : "No");



            a = body.ToString();

            string f = "saideep@saideeprajchhetri.com";  //from
            string p = "smarterasp.net4153"; //password
            string h = "mail.saideeprajchhetri.com"; //server host
            string t = shippingInfo.UserEmail; //to email

            //email to customer
            try
            {
                MailMessage m = new MailMessage();
                SmtpClient sc = new SmtpClient();
                m.From = new MailAddress(f);
                m.To.Add(t);
                m.Subject = "Thank you for the purchase";
                m.Body = "\nDear " + shippingInfo.Name +
                            ",\n\nThank you for the purchase." +
                            "\n\n Your Order Details: \n" + a +

                            ".\n\n\nYours S.Chhetri MVC App\n\nU.K.";
                sc.Host = h;
                string str1 = "gmail.com";
                string str2 = f.ToLower();
                if (str2.Contains(str1))
                {
                    try
                    {
                        sc.Port = 587;
                        sc.Credentials = new System.Net.NetworkCredential(f, p);
                        sc.EnableSsl = true;
                        sc.Send(m);

                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    try
                    {
                        sc.Port = 25;
                        sc.Credentials = new System.Net.NetworkCredential(f, p);
                        sc.EnableSsl = false;
                        sc.Send(m);

                    }
                    catch (Exception ex)
                    {

                    }
                }


            }//End try

            catch
            {
                //MessageLabel.Text = "Server authentication is failed, please check your email";
                //MessageLabel.ForeColor = System.Drawing.Color.Red;
            }

            //email to me
            try
            {
                MailMessage m = new MailMessage();
                SmtpClient sc = new SmtpClient();
                m.From = new MailAddress(f);
                m.To.Add("saideep_chhetri@yahoo.com");
                m.Subject = "Someone used your MVC app";
                m.Body = "\nName: " + shippingInfo.Name +
                            "\n" +
                            "\n Order Details: \n" + a +

                            ".\n\n\n MVC App\n\nU.K.";
                sc.Host = h;
                string str1 = "gmail.com";
                string str2 = f.ToLower();
                if (str2.Contains(str1))
                {
                    try
                    {
                        sc.Port = 587;
                        sc.Credentials = new System.Net.NetworkCredential(f, p);
                        sc.EnableSsl = true;
                        sc.Send(m);

                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    try
                    {
                        sc.Port = 25;
                        sc.Credentials = new System.Net.NetworkCredential(f, p);
                        sc.EnableSsl = false;
                        sc.Send(m);

                    }
                    catch (Exception ex)
                    {

                    }
                }


            }//End try

            catch
            {
                //MessageLabel.Text = "Server authentication is failed, please check your email";
                //MessageLabel.ForeColor = System.Drawing.Color.Red;
            }
//.................................................SMS new.......................................................

            IntelliSMS objIntelliSMS = new IntelliSMS();

            objIntelliSMS.Username = "saideep";
            objIntelliSMS.Password = "password7";

            string message1 = "Hello " + shippingInfo.Name+ "!\n\n" + "Thank you for the purchase. Your order has been processed! \n\nORDER DETAILS: \n" + a + "\n\nSaideep's MVCapp";

            String MessageId = objIntelliSMS.SendMsg
                (shippingInfo.PhoneNo, message1, "447429075603");


//..............................................................SMS old.......................................................
            //string username = "saideep_chhetri@yahoo.com";
            //string password = "b2vac";
            //string msgsender = "4700000000";
            //string destinationaddr = shippingInfo.PhoneNo;
            //string message = "\n \n Your Total order value of {0:c} has been processed. \n\n Saideep";

            //// Create ViaNettSMS object with username and password
            //ViaNettSMS s = new ViaNettSMS(username, password);
            //// Declare Result object returned by the SendSMS function
            //ViaNettSMS.Result result;
            //try
            //{
            //    // Send SMS through HTTP API
            //    result = s.sendSMS(msgsender, destinationaddr, message);
            //    // Show Send SMS response

            //}
            //catch (System.Net.WebException ex)
            //{
            //    //Catch error occurred while connecting to server.
            //    Debug.WriteLine(ex.Message);

            //}









        }
    }
}
