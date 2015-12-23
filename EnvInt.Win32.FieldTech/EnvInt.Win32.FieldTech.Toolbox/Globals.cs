using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Reflection;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic.FileIO;
using System.Drawing.Imaging;
using System.Globalization;
using System.Threading;
using Community.CsharpSqlite.SQLiteClient;

using EnvInt.Win32.FieldTech.Data;
using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.FieldTech
{
    public static class Globals
    {
        public static string LastLocation;
        public static List<string> ProjectComponents = new List<string>();
        public static List<string> ProjectComponentTypes = new List<string>();
        public static Guid ApplicationGuid = new Guid("4C915D8D-3EE6-4083-ADDA-A1A297CFEC18");
        public static Dictionary<string, Point> DialogLocations = new Dictionary<string, Point>();
        public static ProjectData CurrentProjectData;
        public static bool CurrentProjectDataDirty;
        public static bool isProductChinese = false;
        public static TaggedComponent LastComponentValues = new TaggedComponent();
       
        public static void setCurrentCulture()
        {
            if (isProductChinese)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CHS");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CHS");
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            }
        }

        public static string WorkingFolder
        {
            get
            {
                string wFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                //string exeFolder = Environment.CurrentDirectory;
                return wFolder + "\\FieldTech\\Working";
            }
        }

        public static string BackupFolder
        {
            get
            {
                string bFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                return bFolder + "\\FieldTech\\Backups";
            }
        }

        public static string UserFolder
        {
            get
            {
                string bFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                return bFolder + "\\FieldTech";
            }
        }

        public static string ErrorLog
        {
            get
            {
                string bFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                return bFolder + "\\FieldTech\\errorLog.txt";
            }
        }

        public static string ExeDirectory
        {
            get
            {
                return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
        }

        public static void cleanWorkingDirectory()
        {
            //delete it to make sure we are clean...
            try
            {
                if (System.IO.Directory.Exists(Globals.WorkingFolder))
                {
                    //System.IO.Directory.Delete(Globals.WorkingFolder, true);

                    //Move to recycle bin
                    FileSystem.DeleteDirectory(Globals.WorkingFolder, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                }

                System.IO.Directory.CreateDirectory(Globals.WorkingFolder);
                System.IO.Directory.CreateDirectory(Globals.WorkingFolder + "\\Images");
            }
            catch (Exception e) { }

        }

        //public static SqliteConnection GetDbConnection()
        //{
        //    SqliteConnection conn = new SqliteConnection();

        //    string dbFilename = CurrentProjectData.ProjectName + ".db";
        //    string cs = string.Format("Version=3,uri=file:{0}", dbFilename);
        //    Console.WriteLine("Set connection String: {0}", cs);
        //    conn.ConnectionString = cs;
        //    if (!System.IO.File.Exists(dbFilename))
        //    {
        //        //doesn't exist... we have to copy our version there...

        //    }
        //    return conn;
        //}

        //public static DataTable QueryDb(SqliteConnection conn, string queryString)
        //{
        //    SqliteCommand command = new SqliteCommand(queryString, conn);
        //    DataTable dataTable = new DataTable();
        //    SqliteDataAdapter dataAdapter = new SqliteDataAdapter();
        //    dataAdapter.SelectCommand = command;
        //    dataAdapter.Fill(dataTable);
        //    return dataTable;
        //}


        public static void LogError(string msg, string source, string stackTrace)
        {
            //EiServiceReference.MainServiceClient client = new EiServiceReference.MainServiceClient();
           // client.SendErrorAsync(msg, source, stackTrace, System.Environment.UserName, System.Environment.MachineName, String.Empty, GetWindowsVersion(), String.Empty, "FieldTechToolbox", GetCurrentAssemblyVersion(), String.Empty);
            truncateErrorLog();
            
            try
            {
                System.IO.File.AppendAllText(ErrorLog, DateTime.Now.ToString() + " - " + Environment.OSVersion + " - " + Environment.MachineName + " - " + source + Environment.NewLine);
                System.IO.File.AppendAllText(ErrorLog, stackTrace + Environment.NewLine + Environment.NewLine);
            }
            catch (Exception ex)
            { }
        }

        private static void truncateErrorLog()
        {
            if (System.IO.File.Exists(ErrorLog))
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(ErrorLog);
                if (fi.Length > 1000000)
                {
                    //if the log file gets too big for it's britches, make a copy and truncate
                    try
                    {
                        string oldErrorLogPath = ErrorLog + DateTime.Now.ToString("ddMMyyy_Hmmss");
                        System.IO.File.Copy(ErrorLog, oldErrorLogPath);
                        string oldLog = System.IO.File.ReadAllText(ErrorLog);
                        if (oldLog.Length > 10000)
                        {
                            //save last bit of stuff
                            string newLog = "<Log truncated from " + oldErrorLogPath + ">" + oldLog.Substring(oldLog.Length - 10000, 10000);
                            System.IO.File.WriteAllText(ErrorLog, newLog);
                        }
                    }
                    catch { }
                }
            }
        }

        private static string GetWindowsVersion()
        {
            return Environment.OSVersion.Version.ToString();
        }

        public static string GetCurrentAssemblyVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public static bool isDevelopmentVersion()
        {
            bool devVersion;
            try
            {
                if (System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.MinorRevision > 0)
                    devVersion = true;
                else
                    devVersion = false;
            }
            catch
            {
                devVersion = false;                
            }

            return devVersion;

        }

        public static string textCleaner(string str)
        {
            //this function makes sure certain characters aren't fouling up our csv
            string tmpString = string.Empty;

            if (string.IsNullOrEmpty(str)) return "";

            foreach (char c in str)
            {
                if (c != '"' && c != '\\')
                {
                    tmpString += c;
                }
            }

            return tmpString;
        }

        public static double getSizeFromString(string str)
        {
            //sizes have to be positive, so -1 indicates error
            double size = 0;
            double wholeNo = 0;

            if (str != null)
            {

                string rmdc = string.Empty;
                foreach (char c in str)
                {
                    if (c == '/') rmdc += c;
                    if (c == '.') rmdc += c;
                    if (c == ' ') rmdc += c;
                    if (char.IsNumber(c)) rmdc += c;
                }
                str = rmdc;

                if (str.Contains("/"))
                {
                    try
                    {
                        if (str.Contains(" "))
                        {
                            string[] wn = str.Split(' ');
                            wholeNo = double.Parse(wn[0]);
                            str = wn[1];
                        }
                        string[] frac = str.Split('/');
                        size = double.Parse(frac[0]) / double.Parse(frac[1]);
                    }
                    catch
                    {
                        size = 0;
                    }
                }
                else
                {
                    if (!double.TryParse(str, out size)) size = 0;
                }
            }

            return size + wholeNo;
        }

        public static string getTagPoint(int pointNumber, int decimalPoints = 1)
        {
            string pt = string.Empty;

            pt = pointNumber.ToString();

            while (pt.Length < decimalPoints)
            {
                pt = "0" + pt;
            }

            return pt;
        }

        public static string getTagPoint(string LDARTag)
        {
            string pt = string.Empty;

            if (LDARTag.Contains('.'))
            {
                return LDARTag.Split('.')[1];
            }
            else
            {
                return string.Empty;
            }

            return pt;
        }

        public static string getPointFromExistingTag(string TagID)
        {
            string pt = string.Empty;

            if (!string.IsNullOrEmpty(TagID))
            {
                if (TagID.Contains("."))
                {
                    List<string> splitTag = TagID.Split('.').ToList<string>();
                    pt = splitTag.LastOrDefault();
                }
            }

            return pt;
        }

        public static string getBaseTag(string TagID)
        {
            if (TagID.Contains("."))
            {
                return TagID.Split('.')[0];
            }
            else
            {
                return TagID;
            }
        }

        public static string getNextTagWithPoint(string TagID, int startAt, int paddedZeros)
        {

            string tmpPoint = getPointFromExistingTag(TagID);

            if (!string.IsNullOrEmpty(tmpPoint))
            {
                int tmpPointNo = 0;
                if (int.TryParse(tmpPoint, out tmpPointNo))
                {
                    tmpPoint = (tmpPointNo + 1).ToString();
                }
                else
                {
                    tmpPoint = startAt.ToString();
                }
            }
            else
            {
                tmpPoint = startAt.ToString();
            }

            while (tmpPoint.Length < paddedZeros)
            {
                tmpPoint = "0" + tmpPoint;
            }

            return getBaseTag(TagID) + "." + tmpPoint;
        }

        public static string getNextExtension(string Extension, int startAt, int paddedZeros)
        {

            int tmpPointNo = startAt;
            string tmpPoint = string.Empty;

            if (int.TryParse(Extension, out tmpPointNo))
            {
                tmpPoint = (tmpPointNo + 1).ToString();
            }
            else
            {
                tmpPoint = startAt.ToString();
            }

            while (tmpPoint.Length < paddedZeros)
            {
                tmpPoint = "0" + tmpPoint;
            }

            return tmpPoint;
        }

        public static Bitmap GetScreenImage(Point location)
        {
            
            Bitmap screenCap = new Bitmap(140,60);
            Graphics g = Graphics.FromImage(screenCap);
            g.CopyFromScreen(location, Point.Empty, new Size(140, 60));
            return screenCap;

        }

        public static Bitmap GetWholeScreenImage(Point startLocation, Size formSize)
        {

            Bitmap screenCap = new Bitmap(formSize.Width, formSize.Height);
            Graphics g = Graphics.FromImage(screenCap);
            g.CopyFromScreen(startLocation, new Point(0, 0), formSize);
            return screenCap;

        }

        public static List<Rectangle> searchBitmapAll(Bitmap smallBmp, Bitmap bigBmp, double tolerance)
        {
            List<Rectangle> foundRectangles = new List<Rectangle>();
            
            BitmapData smallData =
              smallBmp.LockBits(new Rectangle(0, 0, smallBmp.Width, smallBmp.Height),
                       System.Drawing.Imaging.ImageLockMode.ReadOnly,
                       System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            BitmapData bigData =
              bigBmp.LockBits(new Rectangle(0, 0, bigBmp.Width, bigBmp.Height),
                       System.Drawing.Imaging.ImageLockMode.ReadOnly,
                       System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            int smallStride = smallData.Stride;
            int bigStride = bigData.Stride;

            int bigWidth = bigBmp.Width;
            int bigHeight = bigBmp.Height - smallBmp.Height + 1;
            int smallWidth = smallBmp.Width * 3;
            int smallHeight = smallBmp.Height;

            Rectangle location = Rectangle.Empty;
            int margin = Convert.ToInt32(255.0 * tolerance);

            unsafe
            {
                byte* pSmall = (byte*)(void*)smallData.Scan0;
                byte* pBig = (byte*)(void*)bigData.Scan0;

                int smallOffset = smallStride - smallBmp.Width * 3;
                int bigOffset = bigStride - bigBmp.Width * 3;

                bool matchFound = true;

                for (int y = 0; y < bigHeight; y++)
                {
                    for (int x = 0; x < bigWidth; x++)
                    {
                        byte* pBigBackup = pBig;
                        byte* pSmallBackup = pSmall;

                        //Look for the small picture.
                        for (int i = 0; i < smallHeight; i++)
                        {
                            int j = 0;
                            matchFound = true;
                            for (j = 0; j < smallWidth; j++)
                            {
                                //With tolerance: pSmall value should be between margins.
                                int inf = pBig[0] - margin;
                                int sup = pBig[0] + margin;
                                if (sup < pSmall[0] || inf > pSmall[0])
                                {
                                    matchFound = false;
                                    break;
                                }

                                pBig++;
                                pSmall++;
                            }

                            if (!matchFound) break;

                            //We restore the pointers.
                            pSmall = pSmallBackup;
                            pBig = pBigBackup;

                            //Next rows of the small and big pictures.
                            pSmall += smallStride * (1 + i);
                            pBig += bigStride * (1 + i);
                        }

                        //If match found, we return.
                        if (matchFound)
                        {
                            location.X = x;
                            location.Y = y;
                            location.Width = smallBmp.Width;
                            location.Height = smallBmp.Height;
                            foundRectangles.Add(new Rectangle(location.X, location.Y, location.Width, location.Height));
                            //break;
                        }
                        pBig = pBigBackup;
                        pSmall = pSmallBackup;
                        pBig += 3;
                    }

                    //if (matchFound) break;

                    pBig += bigOffset;
                }
            }

            bigBmp.UnlockBits(bigData);
            smallBmp.UnlockBits(smallData);

            return foundRectangles;
        }


        public static Rectangle searchBitmap(Bitmap smallBmp, Bitmap bigBmp, double tolerance)
        {
            BitmapData smallData =
              smallBmp.LockBits(new Rectangle(0, 0, smallBmp.Width, smallBmp.Height),
                       System.Drawing.Imaging.ImageLockMode.ReadOnly,
                       System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            BitmapData bigData =
              bigBmp.LockBits(new Rectangle(0, 0, bigBmp.Width, bigBmp.Height),
                       System.Drawing.Imaging.ImageLockMode.ReadOnly,
                       System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            int smallStride = smallData.Stride;
            int bigStride = bigData.Stride;

            int bigWidth = bigBmp.Width;
            int bigHeight = bigBmp.Height - smallBmp.Height + 1;
            int smallWidth = smallBmp.Width * 3;
            int smallHeight = smallBmp.Height;

            Rectangle location = Rectangle.Empty;
            int margin = Convert.ToInt32(255.0 * tolerance);

            unsafe
            {
                byte* pSmall = (byte*)(void*)smallData.Scan0;
                byte* pBig = (byte*)(void*)bigData.Scan0;

                int smallOffset = smallStride - smallBmp.Width * 3;
                int bigOffset = bigStride - bigBmp.Width * 3;

                bool matchFound = true;

                for (int y = 0; y < bigHeight; y++)
                {
                    for (int x = 0; x < bigWidth; x++)
                    {
                        byte* pBigBackup = pBig;
                        byte* pSmallBackup = pSmall;

                        //Look for the small picture.
                        for (int i = 0; i < smallHeight; i++)
                        {
                            int j = 0;
                            matchFound = true;
                            for (j = 0; j < smallWidth; j++)
                            {
                                //With tolerance: pSmall value should be between margins.
                                int inf = pBig[0] - margin;
                                int sup = pBig[0] + margin;
                                if (sup < pSmall[0] || inf > pSmall[0])
                                {
                                    matchFound = false;
                                    break;
                                }

                                pBig++;
                                pSmall++;
                            }

                            if (!matchFound) break;

                            //We restore the pointers.
                            pSmall = pSmallBackup;
                            pBig = pBigBackup;

                            //Next rows of the small and big pictures.
                            pSmall += smallStride * (1 + i);
                            pBig += bigStride * (1 + i);
                        }

                        //If match found, we return.
                        if (matchFound)
                        {
                            location.X = x;
                            location.Y = y;
                            location.Width = smallBmp.Width;
                            location.Height = smallBmp.Height;
                            break;
                        }
                        //If no match found, we restore the pointers and continue.
                        else
                        {
                            pBig = pBigBackup;
                            pSmall = pSmallBackup;
                            pBig += 3;
                        }
                    }

                    if (matchFound) break;

                    pBig += bigOffset;
                }
            }

            bigBmp.UnlockBits(bigData);
            smallBmp.UnlockBits(smallData);

            return location;
        }

        public static Rectangle findLeftExclusionZone(Bitmap screenCapture)
        {

            Rectangle location = Rectangle.Empty;

            bool matchFound = true;

            for (int y = 80; y < screenCapture.Height - 20; y++)
            {
                Color pixColor = screenCapture.GetPixel(177, y);
                bool rMatch = pixColor.R > 145 && pixColor.R < 200;
                bool gMatch = pixColor.G > 175 && pixColor.G < 210;
                bool bMatch = pixColor.B > 200 && pixColor.B < 230;
                //if (y == 219)
                //{
                //    MessageBox.Show(pixColor.ToString());
                //}
                if (rMatch && gMatch && bMatch)
                {
                    location.Y = y;
                    break;
                }
            }

            if (location.Y < screenCapture.Height - 20 && location.Y > 80)
            {

                for (int x = 177; x < screenCapture.Width; x++)
                {
                    Color pixColor = screenCapture.GetPixel(x, location.Y);
                    bool rMatch = pixColor.R < 145 || pixColor.R > 200;
                    bool gMatch = pixColor.G < 175 || pixColor.G > 210;
                    bool bMatch = pixColor.B < 200 || pixColor.B > 230;
                    if (rMatch && gMatch && bMatch)
                    {
                        location.X = x;
                        break;
                    }
                }

                if (location.X > 177 && location.X < screenCapture.Width)
                {
                    location.X = location.X - 65;
                    location.Height = 1000;
                    location.Width = 65;
                }
            }
                
            return location;
        }



        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;
            form.TopMost = true;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        public static DataTable GetObjectListAsTable<T>(List<T> stuffList)
        {
            DataTable dt = new DataTable();

            if (stuffList == null) return dt;

            List<PropertyInfo> propList;

            if (stuffList.Count > 0)
            {
                propList = stuffList[0].GetType().GetProperties().ToList();
            }
            else
            {
                return dt;
            }

            foreach (PropertyInfo propinfo in propList)
            {
                if (propinfo.PropertyType.ToString().Contains("Nullable"))
                {
                    if (propinfo.PropertyType.ToString().Contains("DateTime")) dt.Columns.Add(propinfo.Name, typeof(System.Data.SqlTypes.SqlDateTime));
                    if (propinfo.PropertyType.ToString().Contains("String")) dt.Columns.Add(propinfo.Name, typeof(System.Data.SqlTypes.SqlString));
                    if (propinfo.PropertyType.ToString().Contains("Int")) dt.Columns.Add(propinfo.Name, typeof(System.Data.SqlTypes.SqlInt32));
                    if (propinfo.PropertyType.ToString().Contains("Double")) dt.Columns.Add(propinfo.Name, typeof(System.Data.SqlTypes.SqlDouble));
                    if (propinfo.PropertyType.ToString().Contains("Boolean")) dt.Columns.Add(propinfo.Name, typeof(System.Data.SqlTypes.SqlBoolean));
                }
                else
                {
                    dt.Columns.Add(propinfo.Name, propinfo.PropertyType);
                }
            }

            
            foreach (T o in stuffList)
            {
                DataRow dr = dt.NewRow(); 
                foreach (PropertyInfo pi in o.GetType().GetProperties().ToList())
                {
                    if (o.GetType().GetProperty(pi.Name).GetValue(o, null) == null)
                    {
                        dr[pi.Name] = DBNull.Value;
                    }
                    else
                    {
                        dr[pi.Name] = o.GetType().GetProperty(pi.Name).GetValue(o, null);
                    }
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }
    }
}
