using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace PANE.Framework.Utility
{
    /// <summary>
    /// This indicates that a class can be approved.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
    public sealed class  ApprovableClassAttribute :  Attribute
    {
        /// <summary>
        /// Constructor for specifying the type this attribute should be applied to.
        /// </summary>
        /// <param name="name">A friendly name of this class.</param>
        /// <param name="type">The type of the class.</param>
        public ApprovableClassAttribute(string name, Type type)
        {
            this.Name = name;
            this.Type = type;
        }

        /// <summary>
        /// Constructor for specifying the type this attribute should be applied to.
        /// </summary>
        /// <param name="type">The type of the class.</param>
        public ApprovableClassAttribute(Type type)
            : this(type.Name, type)
        {
        }

        public string Name { get; set; }

        public Type Type { get; set; }

    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class XMLNameAttribute : Attribute
    {
        /// <summary>
        /// Constructor for specifying the type this attribute should be applied to.
        /// </summary>
        /// <param name="name">A friendly name of this class.</param>
        public XMLNameAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }


    }

    /// <summary>
    /// Used by every method to specify the action to be taken on that method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple=false)]
    public sealed class ApprovableMethodAttribute: Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">The name of the method.</param>
        /// <param name="action">The <see cref="Action"/> to be taken.</param>
        public ApprovableMethodAttribute(string name, Action action)
            : this(action)
        {
            this.Name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to be taken.</param>
        public ApprovableMethodAttribute(Action action)
        {
            this.Action = action;
        }

        public string Name { get; set; }

        public Action Action { get; set; }
    }

    public enum Action
    {
        INSERT,
        UPDATE,
        DELETE,
        ENABLEDISABLE
      
    }

    public class Approver
    {
        /// <summary>
        /// Invokes methods that are attributed with <see cref="Action"/>.Update
        /// </summary>
        /// <param name="assembly">The assembly to search for the method.</param>
        /// <param name="obj">The object to be updated.</param>
        /// <returns>An <see cref="object"/></returns>
        public static object Update(Assembly assembly, object obj)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException("Assembly");
            }
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }


            //Iterate thru all types in the assembly.
            foreach(Type type in assembly.GetTypes())
            {
                //Compare the type of the object with the type specified in the custom attribute.
                ApprovableClassAttribute[] customClassAttr = (ApprovableClassAttribute[]) type.GetCustomAttributes(typeof(ApprovableClassAttribute), false);
                if (customClassAttr.Any(c => c != null && c.Type == obj.GetType()))
                {
                    //Check that there is an update method specified.
                    bool hasUpdateMethod = false;

                    //First iterate thru all the methods.
                    try
                    {
                            foreach (MethodInfo methodInfo in type.GetMethods())
                            {
                                //Get out all the possible custom attributes.
                                ApprovableMethodAttribute[] customMethodAttr = (ApprovableMethodAttribute[])methodInfo.GetCustomAttributes(typeof(ApprovableMethodAttribute), false);

                                //Do the check here.
                                hasUpdateMethod = customMethodAttr.Any(c => c != null && c.Action == Action.UPDATE);

                                if (hasUpdateMethod)
                                {
                                    //Call the update method.
                                    return methodInfo.Invoke(Activator.CreateInstance(type, null), new object[] { obj });
                                }

                            }
                    }
                    catch {  }
                    //If there is no update mehtod in the class that is meant to have it, then alert the user
                    
                    if (!hasUpdateMethod)
                    {
                        throw new Exception(String.Format("No update method specified in {0}." +
                                   "Please check your methods and try again.", type.FullName));
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Invokes methods that are attributed with <see cref="Action"/>.Insert.
        /// </summary>
        /// <param name="assembly">The assembly to search for the method.</param>
        /// <param name="obj">The object to be inserted.</param>
        /// <returns>An <see cref="object"/></returns>
        public static object Insert(Assembly assembly, object obj)
        {

            if (assembly == null)
            {
                throw new ArgumentNullException("Assembly");
            }
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            //Iterate thru all types in the assembly.
            foreach (Type type in assembly.GetExportedTypes())
            {
                //Compare the type of the object with the type specified in the custom attribute.
                ApprovableClassAttribute[] customClassAttr = (ApprovableClassAttribute[])type.GetCustomAttributes(typeof(ApprovableClassAttribute), false);
                if (customClassAttr.Any(c => c != null && c.Type == obj.GetType()))
                {
                    //Check that there is an insert method specified.
                    bool hasInsertMethod = false;

                    //First iterate thru all the methods.
                    try
                    {
                        foreach (MethodInfo methodInfo in type.GetMethods())
                        {
                            //Get out all the possible custom attributes.
                            ApprovableMethodAttribute[] customMethodAttr = (ApprovableMethodAttribute[])methodInfo.GetCustomAttributes(typeof(ApprovableMethodAttribute), false);
                            //Do the check here.
                            hasInsertMethod = customMethodAttr.Any(c => c != null && c.Action == Action.INSERT);

                            if (hasInsertMethod)
                            {
                                //Call the insert method.
                                return methodInfo.Invoke(Activator.CreateInstance(type, null), new object[] { obj });
                            }

                        }
                    }
                    catch { throw; }
                    //If there is no insert mehtod in the class that is meant to have it, then alert the user
                    if (!hasInsertMethod)
                    {
                        throw new Exception(String.Format("No insert method with appropriate attributes specified in {0}." +
                                   "Please check your methods and try again.", type.FullName));
                    }
                }
            }
            throw new Exception(String.Format("No class in {0} exists with the needed 'ApprovalAttribute'" +
                        ". Please check your classes and try again.", assembly.FullName));
            //return null;
        }

        public static object ApproveAction(Action theAction, object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            Assembly assembly = PANE.Framework.Approval.Configuration.ConfigurationManager.ServiceAssembly;
            //Iterate thru all types in the assembly.
            foreach (Type type in assembly.GetTypes())
            {
                //Compare the type of the object with the type specified in the custom attribute.
                ApprovableClassAttribute[] customClassAttr = (ApprovableClassAttribute[])type.GetCustomAttributes(typeof(ApprovableClassAttribute), false);
                if (customClassAttr.Any(c => c != null && c.Type == obj.GetType()))
                {
                    //Check that there is an update method specified.
                    bool hasMethod = false;

                    //First iterate thru all the methods.
                    try
                    {
                        foreach (MethodInfo methodInfo in type.GetMethods())
                        {
                            //Get out all the possible custom attributes.
                            ApprovableMethodAttribute[] customMethodAttr = (ApprovableMethodAttribute[])methodInfo.GetCustomAttributes(typeof(ApprovableMethodAttribute), false);
                            //Do the check here.
                            hasMethod = customMethodAttr.Any(c => c != null && c.Action == theAction);

                            if (hasMethod)
                            {
                                //Call the update method.
                                return methodInfo.Invoke(Activator.CreateInstance(type, null), new object[] { obj });
                            }

                        }
                    }
                    catch { throw; }
                    //If there is no update mehtod in the class that is meant to have it, then alert the user
                    if (!hasMethod)
                    {
                        throw new Exception(String.Format("No method specified in {0}." +
                                   "Please check your methods and try again.", type.FullName));
                    }
                }
            }
            return null;
        }
    }
}
