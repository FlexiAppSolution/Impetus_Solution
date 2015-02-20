using System;
using System.Collections.Generic;
using NHibernate;
using System.Collections;
using NHibernate.Criterion;
using PANE.Framework.NHibernateManager;
using PANE.Framework.DTO;
using System.Web;
using System.Web.Security;

namespace PANE.Framework.DAO
{
    public class CoreDAO<T, idT> where T:class
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreDAO&lt;T, idT&gt;"/> class.
        /// </summary>
        public CoreDAO()
        {
            
        }

        /// <summary>
        /// Updates the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        public static  void Update(T obj)
        {
            ISession session = BuildSession();
            ITransaction tran = BuildTransaction(session);
            try
            {
                
                if (HttpContext.Current != null)
                {
                    if (!(obj as IAuditable).IsActionInitiatorSet)
                    {
                        MembershipUser theUser = Membership.GetUser();
                        PANE.Framework.Managed.Functions.DTO.FunctionsMembershipUser memUser = (theUser as PANE.Framework.Managed.Functions.DTO.FunctionsMembershipUser);
                    
                        //MembershipUser theUser = Membership.GetUser();
                        if (theUser != null)
                        {
                            (obj as IAuditable).ActionInitiatorUserID = memUser.UserDetails.ID;
                            (obj as IAuditable).ActionInitiatorUserName = memUser.UserDetails.UserName;
                        }
                    }
                }
                //session.Clear();
                session.Update(obj);
              
                
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }

        }
        public static void MergeObect(T obj)
        {
            ISession session = BuildSession();
            ITransaction tran = BuildTransaction(session);
            try
            {

                if (HttpContext.Current != null)
                {
                    if (!(obj as IAuditable).IsActionInitiatorSet)
                    {
                        MembershipUser theUser = Membership.GetUser();
                        if (theUser != null)
                        {
                            (obj as IAuditable).ActionInitiatorUserID = Convert.ToInt64(theUser.ProviderUserKey);
                        }
                    }
                }
                session.Clear();
                session.Update(obj);


            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }

        }

        /// <summary>
        /// Saves the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        public static  void Save(T obj)
        {
            ISession session = BuildSession();
            ITransaction tran = BuildTransaction(session);
            try
            {
                if (HttpContext.Current != null)
                {
                    if (!(obj as IAuditable).IsActionInitiatorSet)
                    {
                        MembershipUser theUser = Membership.GetUser();
                        PANE.Framework.Managed.Functions.DTO.FunctionsMembershipUser memUser = (theUser as PANE.Framework.Managed.Functions.DTO.FunctionsMembershipUser);
                         
                        if (theUser != null)
                        {
                            (obj as IAuditable).ActionInitiatorUserID = memUser.UserDetails.ID;
                            (obj as IAuditable).ActionInitiatorUserName = memUser.UserDetails.UserName;
                        }
                    }
                }
                session.Save(obj);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
        }




        /// <summary>
        /// Deletes the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        public static void Delete(T obj)
        {
            ISession session = BuildSession();
            ITransaction tran = BuildTransaction(session);
            try
            {
                session.Delete(obj);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
        }


        public static List<T> RetrieveUsingPaging<T>(ICriteria theCriteria, int startIndex, int maxRows, out int totalCount)
        {
            ISession session = BuildSession();
            ICriteria countCriteria = CriteriaTransformer.Clone(theCriteria).SetProjection(Projections.RowCount());
            ICriteria listCriteria = CriteriaTransformer.Clone(theCriteria).SetFirstResult(startIndex).SetMaxResults(maxRows);
            IList allResults = session.CreateMultiCriteria().Add<T>(listCriteria).Add(countCriteria).List();
            totalCount = Convert.ToInt32(((IList)allResults[1])[0]);
            return allResults[0] as List<T>;
        }


        /// <summary>
        /// Retrieves the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static T Retrieve(idT id)
        {
            T result = default(T);
            ISession session = BuildSession();
            try
            {
                result = session.Get<T>(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// Retrieves all.
        /// </summary>
        /// <returns></returns>
        public static List<T> RetrieveAll()
        {
            List<T> results = new List<T>();
            ISession session = BuildSession();
            try
            {
                results = session.CreateCriteria<T>().List<T>() as List<T>;
            }
            catch
            {
                throw;
            }
            return results;
        }

        public static List<T> RetrieveAllSorted(string ColumnName)
        {
            List<T> results = new List<T>();
            ISession session = BuildSession();
            try
            {
                ICriteria criteria = session.CreateCriteria<T>();
                results = criteria.AddOrder(Order.Asc(ColumnName)).List<T>() as List<T>;
            }
            catch
            {
                throw;
            }
            return results;
        }

        /// <summary>
        /// Retrieves all.
        /// </summary>
        /// <param name="withDeleted">if set to <c>true</c> [with deleted].</param>
        /// <returns></returns>
        public static List<T> RetrieveAll(bool withDeleted)
        {
            List<T> results = new List<T>();
            ISession session = BuildSession();
            try
            {
                if (withDeleted)
                {
                    results = session.CreateCriteria<T>().List<T>() as List<T>;
                }
                else
                {
                    results = session.CreateCriteria<T>()
                        .Add(Expression.Eq("IsDeleted", false)).List<T>() as List<T>;
                }
            }
            catch
            {
                throw;
            }

            return results;
        }

        /// <summary>
        /// Builds the session.
        /// </summary>
        /// <returns></returns>
        public static ISession BuildSession()
        {

            return NHibernateSessionManager.Instance.GetSession();
        }

        /// <summary>
        /// Builds the transaction.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <returns></returns>
        protected static ITransaction BuildTransaction(ISession session)
        {
            if (session.Transaction == null || !session.Transaction.IsActive)
            {
                return session.BeginTransaction();
            }
            return session.Transaction;
        }


        public static T Merge(T entity)
        {
            ISession session = BuildSession();
            
            T obj = (T)session.Merge(entity);

            if (HttpContext.Current != null)
            {
                if (!(obj as IAuditable).IsActionInitiatorSet)
                {

                    MembershipUser theUser = Membership.GetUser();
                    PANE.Framework.Managed.Functions.DTO.FunctionsMembershipUser memUser = (theUser as PANE.Framework.Managed.Functions.DTO.FunctionsMembershipUser);
                         
                    if (theUser != null)
                    {
                        (obj as IAuditable).ActionInitiatorUserID = memUser.UserDetails.ID;
                        (obj as IAuditable).ActionInitiatorUserName = memUser.UserDetails.UserName;
                    }
                }
            }

            return obj;
        }

        /// <summary>
        /// Commits the changes.
        /// </summary>
        public static void CommitChanges()
        {
            ISession session = BuildSession();
            if (session.Transaction != null && session.Transaction.IsActive)
            {
                session.Transaction.Commit();
            }
        }

        public static void Evict(T entity)
        {
            ISession session = BuildSession();
            session.Evict(entity);
        }

        /// <summary>
        /// Rollbacks the changes.
        /// </summary>
        public static void RollbackChanges()
        {
            ISession session = BuildSession();
            if (session.Transaction != null && session.Transaction.IsActive)
            {
                session.Transaction.Rollback();
            }
        }

        /// <summary>
        /// Clears the current session.
        /// </summary>
        public static void ClearCurrentSession()
        {
            BuildSession().Clear();
        }

    }

}
