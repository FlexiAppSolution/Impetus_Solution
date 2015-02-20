using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace PANE.Framework.Utility
{
    public class WindowsImpersonation
    {
       // Declare signatures for Win32 LogonUser and CloseHandle APIs
       [DllImport("advapi32.dll", SetLastError = true)]
       static extern bool LogonUser(
       string principal,
       string authority,
       string password,
       LogonSessionType logonType,
       LogonProvider logonProvider,
       out IntPtr token);
       [DllImport("kernel32.dll", SetLastError = true)]
       static extern bool CloseHandle(IntPtr handle);
       enum LogonSessionType : uint
       {
           Interactive = 2,
           Network,
           Batch,
           Service,
           NetworkCleartext = 8,
           NewCredentials
       }
       enum LogonProvider : uint
       {
           Default = 0, // default for platform (use this!)
           WinNT35, // sends smoke signals to authority
           WinNT40, // uses NTLM
           WinNT50 // negotiates Kerb or NTLM
       }



       static WindowsImpersonationContext wic;

       public WindowsImpersonation() { }
       private static WindowsImpersonationContext impersonatedUser = null;
       private static IntPtr token = new IntPtr(0);


       public void ImpersonateUser(string domain,string username,string password)
       {
           //IntPtr token = IntPtr.Zero;
           token = IntPtr.Zero;


           try
           {
               // Create a token for DomainName\Bob
               // Note: Credentials should be encrypted in configuration file
               bool result = LogonUser(username,domain ,
               password,
               LogonSessionType.NetworkCleartext,
               LogonProvider.Default,
               out token);
               if (result)
               {
                   WindowsIdentity id = new WindowsIdentity(token);

                   // Begin impersonation
                   impersonatedUser = id.Impersonate();
                   // Log the new identity 

               }
               else
               {
                   //Email.sendError("adeel@xxx.com", "Error (I/O) impersonate failed: " + username + " ", Marshal.GetLastWin32Error().ToString());
               }
           }
           catch (Exception ex)
           {
               //Email.sendError("adeel@xxx.com", "Error (I/O) impersonate: " + username + " ", ex.Message);
               // Prevent any exceptions that occur while the thread is 
               // impersonating from propagating
           }
       }

       // Stops impersonation
       public void Undo()
       {
           if (impersonatedUser != null)
           {
               impersonatedUser.Undo();
               // Free the tokens.
               if (token != IntPtr.Zero)
                   CloseHandle(token);

               // Email.sendError("adeel@xxx.com", "impersonate Undo: ", "impersonate Undo: ");
           }
       }
    }
}
