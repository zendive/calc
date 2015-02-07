using System;
using System.Windows.Forms;
using MasCalc;

class Program
{
  [STAThread]
  static void Main()
  {
    bool bContinue = false;
    do
    {
      try
      {
        bContinue = false;
        Form form = new CalcForm();
        Program.Prepare();
        Application.Run(form);
      }
      catch (Exception xcp)
      {
        DEBUG.DUMP_EXCEPTION(xcp);
        DialogResult r = MessageBox.Show(
          "Programm was wondered by underestimated but handled exception."
          +"\nWTF are you doing there?\n\n- Restart the programm?"+xcp.ToString()
          , "What a wonderful world :)"
          , MessageBoxButtons.RetryCancel, MessageBoxIcon.Question
          , MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        bContinue = (r == DialogResult.Retry);
      }
    } while (bContinue);
  }

  private static void Prepare()
  {
    // ensure quick response on following exceptions
    try
    {
      throw new ApplicationException();
    }
    catch (ApplicationException)
    {
    }

    Application.EnableVisualStyles();
    
    // wake up GC
    for (byte c = 255; c > 0; c--) { GC.Collect(); }
  }
}

