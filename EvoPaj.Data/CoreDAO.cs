using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using System.Collections;
using NHibernate.Criterion;
using PANE.Framework.NHibernateManager;
using PANE.Framework.DTO;
using System.Web.Security;
using System.Web;

namespace EvoPaj.Data
{
    public class CoreDAO<T, idT>
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
        public virtual void Update(T obj)
        {
            ISession session = BuildSession();
            ITransaction tran = BuildTransaction(session);
            try
            {
               
                session.Merge(obj);

            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }

        }
        public virtual void ExplicitUpdate(T obj)
        {
            ISession session = BuildSession();
            ITransaction tran = BuildTransaction(session);
            try
            {
                //session.Clear();
                if (HttpContext.Current != null)
                {
                    if (!(obj as IAuditable).IsActionInitiatorSet)
                    {
                        try
                        {
                            MembershipUser theUser = Membership.GetUser();
                            PANE.Framework.Managed.Functions.DTO.FunctionsMembershipUser memUser = (theUser as PANE.Framework.Managed.Functions.DTO.FunctionsMembershipUser);
                            if (memUser != null)
                            {
                                (obj as IAuditable).ActionInitiatorUserID = memUser.UserDetails.ID;
                                (obj as IAuditable).ActionInitiatorUserName = memUser.UserDetails.UserName;
                            }
                        }
                        catch { }
                    }
                }
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
        public virtual void Save(T obj)
        {
            ISession session = BuildSession();
            ITransaction tran = BuildTransaction(session);
            try
            {               
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
        public virtual void Delete(T obj)
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


        public virtual List<T> RetrieveUsingPaging<T>(ICriteria theCriteria, int startIndex, int maxRows, out int totalCount)
        {
            ISession session = BuildSession();
            ICriteria countCriteria = CriteriaTransformer.Clone(theCriteria).SetProjection(Projections.RowCount());
            ICriteria listCriteria = CriteriaTransformer.Clone(theCriteria).SetFirstResult(startIndex);
            if (maxRows > 0)
            {
                listCriteria.SetMaxResults(maxRows);
            }
            IList allResults = session.CreateMultiCriteria().Add<T>(listCriteria).Add(countCriteria).List();
            totalCount = Convert.ToInt32(((IList)allResults[1])[0]);
            return allResults[0] as List<T>;
        }


        /// <summary>
        /// Retrieves the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public virtual T Retrieve(idT id)
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
        public virtual List<T> RetrieveAll()
        {
            List<T> results = new List<T>();
            ISession session = BuildSession();
            try
            {
                results = session.CreateCriteria(typeof(T)).List<T>() as List<T>;
            }
            catch
            {
                throw;
            }
            return results;
        }
        public virtual List<T> RetrieveAllSorted(string ColumnName)
        {
            List<T> results = new List<T>();
            ISession session = BuildSession();
            try
            {
                ICriteria criteria = session.CreateCriteria(typeof(T));
                results = criteria.AddOrder(Order.Asc("ColumnName")).List<T>() as List<T>;
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
        public virtual List<T> RetrieveAll(bool withDeleted)
        {
            List<T> results = new List<T>();
            ISession session = BuildSession();
            try
            {
                if (withDeleted)
                {
                    results = session.CreateCriteria(typeof(T)).List<T>() as List<T>;
                }
                else
                {
                    results = session.CreateCriteria(typeof(T)).Add(Expression.Eq("IsDeleted", false)).List<T>() as List<T>;
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

            return NHibernateSessionManager.Instance.GetSession(); //PANE.Framework.DAO.CoreDAO<T, idT>.BuildSession();//
        }


        /// <summary>
        /// Builds the transaction.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <returns></returns>
        public static ITransaction BuildTransaction(ISession session)
        {
            if (session.Transaction == null || !session.Transaction.IsActive)
            {
                return session.BeginTransaction();
            }
            return session.Transaction;
        }

        public virtual T Merge(T entity)
        {
            ISession session = BuildSession();
            if (HttpContext.Current != null)
            {
                if (!(entity as IAuditable).IsActionInitiatorSet)
                {
                    try
                    {
                        MembershipUser theUser = Membership.GetUser();
                        PANE.Framework.Managed.Functions.DTO.FunctionsMembershipUser memUser = (theUser as PANE.Framework.Managed.Functions.DTO.FunctionsMembershipUser);
                        if (memUser != null)
                        {
                            (entity as IAuditable).ActionInitiatorUserID = memUser.UserDetails.ID;
                            (entity as IAuditable).ActionInitiatorUserName = memUser.UserDetails.UserName;
                        }
                    }
                    catch { }
                }
            }
            return (T)session.Merge(entity);
        }

        /// <summary>
        /// Commits the changes.
        /// </summary>
        public virtual void CommitChanges()
        {
            ISession session = BuildSession();
            if (session.Transaction != null && session.Transaction.IsActive)
            {
                session.Transaction.Commit();
            }
        }

        /// <summary>
        /// Rollbacks the changes.
        /// </summary>
        public virtual void RollbackChanges()
        {
            try
            {
                ISession session = BuildSession();
                if (session.Transaction != null && session.Transaction.IsActive)
                {
                    session.Transaction.Rollback();
                }
            }
            catch (Exception ex) { PANE.ERRORLOG.ErrorLogger.Log(ex); }
        }

        /// <summary>
        /// Clears the current session.
        /// </summary>
        public virtual void ClearCurrentSession()
        {
            BuildSession().Clear();
        }
        public void RunQuery(string SQLquery)
        {
            ISession session = BuildSession();
            IQuery query = session.CreateSQLQuery(SQLquery);
            query.ExecuteUpdate();
        }

        //I just created this modification to d one above
        public void RunDeleteQuery(string SQLquery)
        {
            ISession session = BuildSession();
            IQuery query = session.CreateQuery(SQLquery);
            query.ExecuteUpdate();
        }
    }
}
