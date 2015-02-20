namespace PANE.Framework.NHibernateManager
{
    using NHibernate;
    using NHibernate.Cfg;
    using System.Runtime.Remoting.Messaging;
    using System.Web;
    using System.IO;
    //using NHibernate.Mapping.Attributes;
    using System.Reflection;
    using FluentNHibernate.Cfg;
    using PANE.Framework.ExceptionHandling;
    using System;
    using PANE.Framework.AuditTrail;

    public sealed class NHibernateSessionManager
    {
        private const string SESSION_KEY = "::NHIBERNATE_SESSION_KEY::";
        private ISessionFactory sessionFactory;

        private NHibernateSessionManager()
        {
            try
            {

                this.InitSessionFactory();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw;
            }
        }

        public void RollbackSession()
        {
            ISession contextSession = this.ContextSession;
            if (contextSession != null)
            {
                if (contextSession.Transaction != null && contextSession.Transaction.IsActive)
                {
                    contextSession.Transaction.Rollback();
                }
            }
        }

        public void CloseSession()
        {
            ISession contextSession = this.ContextSession;
            if ((contextSession != null) && contextSession.IsOpen)
            {
                try
                {
                    if (HttpContext.Current == null || HttpContext.Current.Error == null)
                    {
                        if (contextSession.Transaction != null && contextSession.Transaction.IsActive)
                        {
                            contextSession.Transaction.Commit();
                            PANE.Framework.Managed.NHibernateManager.NHibernateSessionManager.Instance.GetSession(PortalUtility.MfbCode, Managed.NHibernateManager.Configuration.DatabaseSource.Local).Transaction.Commit();
                        }
                        contextSession.Flush();
                    }
                    else
                    {
                        try
                        {
                            if (contextSession.Transaction != null && contextSession.Transaction.IsActive)
                            {
                                contextSession.Transaction.Rollback();
                                PANE.Framework.Managed.NHibernateManager.NHibernateSessionManager.Instance.GetSession(PortalUtility.MfbCode, Managed.NHibernateManager.Configuration.DatabaseSource.Local).Transaction.Rollback();
                            }
                        }
                        catch {  }
                    }
                }
                catch (Exception ex)
                {
                    new global::PANE.ERRORLOG.Error().LogToFile(ex);
                    try
                    {
                        if (contextSession.Transaction != null && contextSession.Transaction.IsActive)
                        {
                            contextSession.Transaction.Rollback();
                        }
                    }
                    catch (Exception e) { new global::PANE.ERRORLOG.Error().LogToFile(e); }
                }
                finally
                {
                    try
                    {
                        contextSession.Close();
                        contextSession.Dispose();
                    }
                    catch { }
                }
            }
            this.ContextSession = null;
        }


        public ISession GetSession()
        {
            ISession contextSession = this.ContextSession;
            if (contextSession == null)
            {
                //ISession auditTrailSession = PANE.Framework.Managed.NHibernateManager.NHibernateSessionManager.Instance.GetSession(PortalUtility.MfbCode); 
                
                //ISession auditTrailSession = PANE.Framework.Managed.NHibernateManager.NHibernateSessionManager.Instance.GetSession(PortalUtility.MfbCode, Managed.NHibernateManager.Configuration.DatabaseSource.Local);
                //contextSession = this.sessionFactory.OpenSession(new AuditTrailInterceptor(auditTrailSession));
                contextSession = this.sessionFactory.OpenSession();
                //if (contextSession.Transaction == null || !contextSession.Transaction.IsActive)
                //{
                //    contextSession.BeginTransaction();
                //}
                this.ContextSession = contextSession;
            }

            return contextSession;
        }

        private class PortalUtility
        {
            private const string SS_MFBCODE = "::SS_INSTITUTION_CODE::";
            public static string MfbCode
            {
                get
                {
                    if (System.Web.HttpContext.Current != null)
                    {
                        try
                        {
                            PANE.Framework.Managed.Functions.DTO.FunctionsMembershipUser user = System.Web.Security.Membership.GetUser() as PANE.Framework.Managed.Functions.DTO.FunctionsMembershipUser;
                            if (user != null)
                            {
                                return user.UserName.Split(':')[1];
                            }
                            else
                            {
                                return "";
                            }
                        }
                        catch
                        {
                            return "";
                        }
                    }
                    else
                    {
                        return "";
                    }
                }
            }
        }

        private void InitSessionFactory()
        {
           // HibernatingRhinos.NHibernate.Profiler.Appender.NHibernateProfiler.Initialize();
            Configuration cfg = new Configuration().Configure();

            FluentConfiguration fluentCfg = Fluently.Configure(cfg);

             NHibernate.Cfg.ConfigurationSchema.HibernateConfiguration hc = System.Configuration.ConfigurationManager.GetSection(NHibernate.Cfg.ConfigurationSchema.CfgXmlHelper.CfgSectionName) as NHibernate.Cfg.ConfigurationSchema.HibernateConfiguration;
             if (hc == null) throw new HibernateConfigException("Cannot process Hibernate Section in config file");
             if (hc.SessionFactory != null)
             {
                 foreach (NHibernate.Cfg.ConfigurationSchema.MappingConfiguration mappingAssemblyCfg in hc.SessionFactory.Mappings)
                 {
                     //NOTE: Nhibernate.Mapping.attributes plugin code
                     /*MemoryStream stream = new MemoryStream();
                     try
                     {
                         HbmSerializer.Default.Serialize(stream, Assembly.Load(mappingAssemblyCfg.Assembly));
                         stream.Position = 0;
                         cfg = cfg.AddInputStream(stream);
                     }
                     catch { throw; }
                     finally
                     {
                         stream.Close();
                     }*/

                     //fluent nHibernate
                      //fluentCfg.Mappings( m =>
                      //    m.AutoMappings.Add(
                      //     new AutoPersistenceModel(Assembly.Load(mappingAssemblyCfg.Assembly))                         
                      //     )); 
                     fluentCfg.Mappings(m =>
                                 m.FluentMappings.AddFromAssembly(Assembly.Load(mappingAssemblyCfg.Assembly))
                                 .Conventions.Setup(c =>
                                 {
                                     c.Add<Fluent.ClassMappingConvention>();
                                     c.Add<Fluent.ReferencesConvention>();
                                     c.Add<Fluent.HasManyConvention>();
                                     // c.Add<Fluent.EnumMappingConvention>();
                                 }
                                 ));//.ExportTo(@".\"));
                 }
             }

             
            //new NHibernate.Cfg.ConfigurationSchema.SessionFactoryConfiguration
             Configuration conf = fluentCfg.BuildConfiguration();

            AuditEventListener eventListener = new AuditEventListener();
            //conf.SetListener(NHibernate.Event.ListenerType.PreInsert, eventListener);
            //conf.SetListener(NHibernate.Event.ListenerType.PreUpdate, eventListener);
            //conf.SetListener(NHibernate.Event.ListenerType.PreDelete, eventListener);
           conf.SetListener(NHibernate.Event.ListenerType.Merge, eventListener);
           conf.SetListener(NHibernate.Event.ListenerType.PostInsert, eventListener);
           conf.SetListener(NHibernate.Event.ListenerType.PostUpdate, eventListener);
           conf.SetListener(NHibernate.Event.ListenerType.PostDelete, eventListener);
           //conf.SetListener(NHibernate.Event.ListenerType, eventListener);

          //   new NHibernate.Tool.hbm2ddl.SchemaExport(conf).Execute(false, true, false);
             //TODO: Add

            new NHibernate.Tool.hbm2ddl.SchemaUpdate(conf).Execute(false, true);

            this.sessionFactory = conf.BuildSessionFactory();
            
        }

        private bool IsInWebContext()
        {
            return (HttpContext.Current != null);
        }

        private ISession ContextSession
        {
            get
            {
               if (this.IsInWebContext())
                {
                    return (ISession) HttpContext.Current.Items["::NHIBERNATE_SESSION_KEY::"];
                }
                return (ISession) CallContext.GetData("::NHIBERNATE_SESSION_KEY::");
            }
            set
            {
                if (this.IsInWebContext())
                {
                    HttpContext.Current.Items["::NHIBERNATE_SESSION_KEY::"] = value;
                }
                else
                {
                    CallContext.SetData("::NHIBERNATE_SESSION_KEY::", value);
                }
            }
        }

        public static NHibernateSessionManager Instance
        {
            get
            {
                return Nested.NHibernateSessionManager;
            }
        }

        private class Nested
        {
            internal static readonly NHibernateSessionManager NHibernateSessionManager = new NHibernateSessionManager();
        }
    }
}

