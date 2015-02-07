/**
 * Author: Alexander Block
 * Is a part of Calc application
 * developed for C# study goals and is an intelectual
 * property of his author.
*/

namespace Calc
{
  /// <summary>
  /// CCalculator exception, rised when synrax error catched.
  /// Syntax error description passed via exception message.</summary>
  public class SyntaxException: System.ApplicationException
  {
    private string m_strError;

    public string Error
    {
      get { return m_strError; }
    }

    public SyntaxException()
      : base("")
    {
      m_strError = "";
    }

    public SyntaxException(string _strError)
      :base(_strError)
    {
      m_strError = _strError;
    }
  };

  
  /// <summary>
  /// CCalculator exception, rised on defined but not implemented method.
  /// Error description passed via exception message.</summary>
  public class EngineException: System.ApplicationException
  {
    public EngineException()
      : base("")
    {
    }

    public EngineException(string _sMessage)
      : base(_sMessage)
    {
    }
  };


  /// <summary>
  /// CCalculator exception, rised with request to client-application
  /// to execute command passed via exception message.
  /// List of this commands can be found in
  /// `CCalculator.ExitWithCommand` method.</summary>
  public class CommandException: System.ApplicationException
  {
    private Calc.AppCommand m_AppCommand;
    private int m_iArg;

    public Calc.AppCommand Command
    {
      get { return m_AppCommand; }
    }

    public int Argument
    {
      get { return m_iArg; }
    }

    public CommandException()
      : base("")
    {
      m_AppCommand = Calc.AppCommand.Standby;
      m_iArg = 0;
    }

    public CommandException(Calc.AppCommand _AppCommand)
      : base("")
    {
      m_AppCommand = _AppCommand;
      m_iArg = 0;
    }

    public CommandException(Calc.AppCommand _AppCommand, int _iArg)
      : base("")
    {
      m_AppCommand = _AppCommand;
      m_iArg = _iArg;
    }
  };


  /// <summary>
  /// CCalculator exception NOT FOR APPLICATION CATCHES.
  /// It's used inside CCalculator algorythm.</summary>
  public class ServiceException: System.ApplicationException
  {
    public ServiceException()
      : base("")
    {
    }

    public ServiceException(string _sMessage)
      : base(_sMessage)
    {
    }
  };
}
