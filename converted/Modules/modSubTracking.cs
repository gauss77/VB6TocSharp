using VB6 = Microsoft.VisualBasic.Compatibility.VB6;
using System.Runtime.InteropServices;
using static VBExtension;
using static VBConstants;
using Microsoft.VisualBasic;
using System;
using System.Windows;
using System.Windows.Controls;
using static System.DateTime;
using static System.Math;
using static Microsoft.VisualBasic.Globals;
using static Microsoft.VisualBasic.Collection;
using static Microsoft.VisualBasic.Constants;
using static Microsoft.VisualBasic.Conversion;
using static Microsoft.VisualBasic.DateAndTime;
using static Microsoft.VisualBasic.ErrObject;
using static Microsoft.VisualBasic.FileSystem;
using static Microsoft.VisualBasic.Financial;
using static Microsoft.VisualBasic.Information;
using static Microsoft.VisualBasic.Interaction;
using static Microsoft.VisualBasic.Strings;
using static Microsoft.VisualBasic.VBMath;
using System.Collections.Generic;
using static Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6.ColorConstants;
using static Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6.DrawStyleConstants;
using static Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6.FillStyleConstants;
using static Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6.GlobalModule;
using static Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6.Printer;
using static Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6.PrinterCollection;
using static Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6.PrinterObjectConstants;
using static Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6.ScaleModeConstants;
using static Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6.SystemColorConstants;
using ADODB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using VB2CS.Forms;
using static modUtils;
using static modConvert;
using static modProjectFiles;
using static modTextFiles;
using static modRegEx;
using static frmTest;
using static modConvertForm;
using static modSubTracking;
using static modVB6ToCS;
using static modUsingEverything;
using static modSupportFiles;
using static modConfig;
using static modRefScan;
using static modConvertUtils;
using static modControlProperties;
using static modProjectSpecific;
using static modINI;
using static modLinter;
using static modGit;
using static modDirStack;
using static modShell;
using static VB2CS.Forms.frm;
using static VB2CS.Forms.frmConfig;


static class modSubTracking {
// Option Explicit
public class Variable {
 public string Name = "";
 public string asType = "";
 public string asArray = "";
 public bool Param = false;
 public bool RetVal = false;
 public bool Assigned = false;
 public bool Used = false;
 public bool AssignedBeforeUsed = false;
 public bool UsedBeforeAssigned = false;
}
public class Property {
 public string Name = "";
 public bool asPublic = false;
 public string asType = "";
 public bool asFunc = false;
 public string Getter = "";
 public string Setter = "";
 public string origArgName = "";
 public string funcArgs = "";
 public string origProto = "";
}
private static bool Lockout = false;
private static List<Variable> Vars = new List<Variable> {}; // TODO - Specified Minimum Array Boundary Not Supported: Private Vars() As Variable
private static List<Property> Props = new List<Property> {}; // TODO - Specified Minimum Array Boundary Not Supported: Private Props() As Property


public static bool Analyze {
  get {
    bool Analyze;
    Analyze = Lockout;

  return Analyze;
  }
}


public static void SubBegin(bool setLockout= false) {
  if (!setLockout) {
    List<Variable> nVars = new List<Variable> {}; // TODO - Specified Minimum Array Boundary Not Supported:     Dim nVars() As Variable

    Vars = nVars;
  }

  Lockout = Lockout;
}

private static int SubParamIndex(string P) {
  int SubParamIndex = 0;
  // TODO (not supported):   On Error GoTo NoEntries
  for(SubParamIndex=LBound(Vars); SubParamIndex<UBound(Vars); SubParamIndex++) {
    if (Vars(SubParamIndex).Name == P()) {
      return SubParamIndex;

    }
  }
NoEntries:;
  SubParamIndex = -1;
  return SubParamIndex;
}

public static Variable SubParam(string P) {
  Variable SubParam = null;
  // TODO (not supported): On Error Resume Next
  SubParam = Vars(SubParamIndex(P()));
  return SubParam;
}

public static void SubParamDecl(string P, string asType, string asArray, bool isParam, bool isReturn) {
  if (Lockout) {
return;

  }

  Variable K = null;
  int N = 0;

  K.Name = P();
  K.Param = isParam;
  // TODO (not supported): On Error Resume Next
  N = 0;
  N = UBound(Vars) + 1;
  List<> Vars_4617_tmp = new List<>();
for (int redim_iter_1306=0;i<0;redim_iter_1306++) {Vars.Add(redim_iter_1306<Vars.Count ? Vars(redim_iter_1306) : null);}
  dynamic _WithVar_4682;
  _WithVar_4682 = Vars(N);
    _WithVar_4682.Name = P();
    _WithVar_4682.asType = asType;
    _WithVar_4682.Param = isParam;
    _WithVar_4682.RetVal = isReturn;
    _WithVar_4682.asArray = asArray;
}

public static void SubParamAssign(string P) {
  if (Lockout) {
return;

  }

  int K = 0;

  K = SubParamIndex(P());
  if (K >= 0) {
    dynamic _WithVar_2088;
    _WithVar_2088 = Vars(K);
      _WithVar_2088.Assigned = true;
      if (!_WithVar_2088.Used) {
        _WithVar_2088.AssignedBeforeUsed = true;
      }
  }
}

public static void SubParamUsed(string P) {
  if (Lockout) {
return;

  }

  int K = 0;

  K = SubParamIndex(P());
  if (K >= 0) {
    Vars(K).Used = true;
    if (!Vars(K).Assigned) {
      Vars(K).UsedBeforeAssigned = true;
    }
  }
}

public static void SubParamUsedList(string S) {
  dynamic Sp = null;
  dynamic L = null;

  if (Lockout) {
return;

  }

  Sp = Split(S, ",");
  foreach(var iterL in Sp) {
L = iterL;
    if (L != "") {
      SubParamUsed(L);
    }
  }
}

public static void ClearProperties() {
  List<Property> nProps = new List<Property> {}; // TODO - Specified Minimum Array Boundary Not Supported:   Dim nProps() As Property

  Props = nProps;
}

private static int PropIndex(string P) {
  int PropIndex = 0;
  // TODO (not supported):   On Error GoTo NoEntries
  for(PropIndex=LBound(Props); PropIndex<UBound(Props); PropIndex++) {
    if (Props(PropIndex).Name == P()) {
      return PropIndex;

    }
  }
NoEntries:;
  PropIndex = -1;
  return PropIndex;
}

public static void AddProperty(string S) {
  int X = 0;
  Property PP = null;

  string Pro = "";
  string origProto = "";
  bool asPublic = false;

  bool asFunc = false;

  string GSL = "";
  string pName = "";
  string pArgs = "";
  string pArgName = "";
  string pType = "";


  Pro = SplitWord(S, 1, vbCr);
  origProto = Pro;

  S = nlTrim(Replace(S, Pro, ""));
  if (Right(S, 12) == "End Property") {
    S = nlTrim(Left(S, Len(S) - 12));
  }


  if (LMatch(Pro, "Public ")) {
    Pro = Mid(Pro, 8); // if one is public, both are...
    asPublic = true;
  }
  if (LMatch(Pro, "Private ")) {
    Pro = Mid(Pro, 9);
  }
  if (LMatch(Pro, "Friend ")) {
    Pro = Mid(Pro, 8);
  }
  if (LMatch(Pro, "Property ")) {
    Pro = Mid(Pro, 10);
  }

  if (LMatch(Pro, "Get ")) {
    Pro = Mid(Pro, 5);
    GSL = "get";
  }
  if (LMatch(Pro, "Let ")) {
    Pro = Mid(Pro, 5);
    GSL = "let";
  }
  if (LMatch(Pro, "Set ")) {
    Pro = Mid(Pro, 5);
    GSL = "set";
  }
  pName = RegExNMatch(Pro, patToken);
  Pro = Mid(Pro, Len(pName) + 1);
  if (LMatch(Pro, "(")) {
    Pro = Mid(Pro, 2);
  }
  pArgs = nextBy(Pro, ")");
  if ((GSL == "get" && pArgs != "") || (GSL != "get" && InStr(pArgs, ",") > 0)) {
    asFunc = true;
  }
  if (GSL == "set" || GSL == "let") {
    string fArg = "";

    fArg = Trim(SplitWord(pArgs, -1, ","));
    if (LMatch(fArg, "ByVal ")) {
      fArg = Mid(fArg, 7);
    }
    if (LMatch(fArg, "ByRef ")) {
      fArg = Mid(fArg, 7);
    }
    pArgName = SplitWord(fArg, 1);
    if (SplitWord(fArg, 2, " ") == "As") {
      pType = SplitWord(fArg, 3, " ");
    } else {
      pType = "Variant";
    }
  }
  Pro = Mid(Pro, Len(pArgs) + 1);
  if (LMatch(Pro, ")")) {
    Pro = Trim(Mid(Pro, 2));
  }
  if (LMatch(Pro, "As ")) {
    Pro = Mid(Pro, 4);
    pType = Pro;
  }

  if (pType == "") {
    pType = "Variant";
  }


  X = PropIndex(pName);
  if (X == -1) {
    X = 0;
    // TODO (not supported): On Error Resume Next
    X = UBound(Props) + 1;
    // TODO (not supported): On Error GoTo 0
    List<> Props_7671_tmp = new List<>();
for (int redim_iter_422=0;i<0;redim_iter_422++) {Props.Add(redim_iter_422<Props.Count ? Props(redim_iter_422) : null);}
  }

  Props(X).Name = pName;
  Props(X).origProto = origProto;
  if (asPublic) {
    Props(X).asPublic = true; // if one is public, both are...
  }
  switch(GSL) {
    case "get":
      Props(X).Getter = ConvertSub(S, false, vbTriState.vbFalse);
      Props(X).asType = ConvertDataType(pType);
      Props(X).asFunc = asFunc;
      Props(X).funcArgs = pArgs;
      break;
    case "set":
      Props(X).Setter = ConvertSub(S, false, vbTriState.vbFalse);
      Props(X).origArgName = pArgName;
      if (pType != "") {
        Props(X).asType = ConvertDataType(pType);
      }
      if (asFunc) {
        Props(X).asFunc = true;
      }
      if (pArgs != "") {
        Props(X).funcArgs = pArgs;
      }
break;
}
}

public static string ReadOutProperties(bool asModule= false) {
  string ReadOutProperties = "";
  // TODO (not supported): On Error Resume Next
  int I = 0;
  string R = "";
  Property P = null;

  string N = "";
  string M = "";

  string T = "";

  R = "";
  M = "";
  N = vbCrLf;
  I = -1;
  for(I=LBound(Props); I<UBound(Props); I++) {
    if (I == -1) {
goto NoItems;
    }
    dynamic _WithVar_5223;
    _WithVar_5223 = Props(I);
      if (_WithVar_5223.Name != "" && !(_WithVar_5223.Getter == "" && _WithVar_5223.Setter == "")) {
        if (_WithVar_5223.asPublic) {
          R = R + "public ";
        }
        if (asModule) {
          R = R + "static ";
        }
//          If .Getter = "" Then R = R & "writeonly "
//          If .Setter = "" Then R = R & "readonly "
        if (_WithVar_5223.asFunc) {
          R = R + " // TODO: Arguments not allowed on properties: " + _WithVar_5223.funcArgs + vbCrLf;
          R = R + " //       " + _WithVar_5223.origProto + vbCrLf;
        }
        R = R + M + _WithVar_5223.asType + " " + _WithVar_5223.Name;
        R = R + " {";

        if (_WithVar_5223.Getter != "") {
          R = R + N + "  get {";
          R = R + N + "    " + _WithVar_5223.asType + " " + _WithVar_5223.Name + ";";
          T = _WithVar_5223.Getter;
          T = Replace(T, "Exit(Property)", "return " + _WithVar_5223.Name + ";");
          R = R + N + "    " + T;
          R = R + N + "  return " + _WithVar_5223.Name + ";";
          R = R + N + "  }";
        }
        if (_WithVar_5223.Setter != "") {
          R = R + N + "  set {";
          T = _WithVar_5223.Setter;
          T = ReplaceToken(T, "value", "valueOrig");
          T = Replace(T, _WithVar_5223.origArgName, "value");
          T = Replace(T, "Exit Property", "return;");
          R = R + N + "    " + T;
          R = R + N + "  }";
        }
        R = R + N + "}";
        R = R + N;
      }
  }
NoItems:;

  ReadOutProperties = R;
  return ReadOutProperties;
}
}
