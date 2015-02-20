using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOA.Framework.FS;
namespace SOA.Framework.ApprovalConfigHelper
{
    
    public class AppFunctions
    {
       
            public AppFunctions()
            {
            }


            public AppFunctions(string appName, FunctionItem functionItem )
            {
                this.AppName = appName;
                this.FunctionItem = functionItem;
            }
            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            /// <value>The name.</value>
            /// 

           public virtual FunctionItem FunctionItem { get; set; }

            public virtual string AppName { get; set; }
            
            public virtual string Name {


                get{
                    if (FunctionItem != null)
                    {

                        return this.FunctionItem.Name;
                    }
                    else {

                        return "";
                    
                    }
                }
                
                
                }

            /// <summary>
            /// Gets or sets the description.
            /// </summary>
            /// <value>The description.</value>

            public virtual string Description
            {
                get
                {
                    if (FunctionItem != null)
                    {

                        return this.FunctionItem.Description;
                    }
                    else
                    {

                        return "";

                    }
                }
            }


            /// <summary>
            /// Gets or sets the parent function item.
            /// </summary>
            /// <value>The parent function.</value>

            public virtual FunctionItem ParentFunctionItem
            {
                get
                {
                    if (FunctionItem != null)
                    {

                        return this.FunctionItem.ParentFunctionItem;
                    }
                    else
                    {

                        return null;

                    }
                }
            }


            /// <summary>
            /// Gets or sets the name of the role.
            /// </summary>
            /// <value>The name of the role.</value>

            public virtual string RoleName
            {
                get
                {
                    if (FunctionItem != null)
                    {

                        return this.FunctionItem.RoleName;
                    }
                    else
                    {

                        return "";

                    }
                }
            }

            /// <summary>
            /// Gets or sets a value indicating whether this instance has sub roles.
            /// </summary>
            /// <value>
            /// 	<c>true</c> if this instance has sub roles; otherwise, <c>false</c>.
            /// </value>

            public virtual Boolean HasSubRoles
            {
                get
                {
                    if (FunctionItem != null)
                    {

                        return this.FunctionItem.HasSubRoles;
                    }
                    else
                    {

                        return false;

                    }
                }
            }

            public virtual IList<ApprovalConfig> TheApprovalConfigurations
            {
                get
                {
                    if (FunctionItem != null)
                    {

                        return this.TheApprovalConfigurations;
                    }
                    else
                    {

                        return null;

                    }
                }
            }

            public virtual bool IsApprovable
            {
                get
                {
                    if (FunctionItem != null)
                    {

                        return this.FunctionItem.IsApprovable;
                    }
                    else
                    {

                        return false;

                    }
                }
            }
        
    }
}
