﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NHibernate;
using T034.Api.Tools;

namespace T034.Api.DataAccess
{
    public class NhBaseDb : IBaseDb
    {
        protected readonly ISessionFactory SessionFactory;

        public NhBaseDb(ISessionFactory sessionFactory)
        {
            SessionFactory = sessionFactory;
        }

        public T Get<T>(int id) where T : Entity.Entity
        {
            using (var session = SessionFactory.OpenSession())
            {
                try
                {
                    return session.Get<T>(id);
                }
                catch (Exception ex)
                {
                    var methodName = string.Format("{0}<{1}>", System.Reflection.MethodBase.GetCurrentMethod().Name, typeof(T));
                    MonitorLog.WriteLog(FormatLogMessage(methodName, ex), MonitorLog.typelog.Error, true);
                    throw;
                }
            }
        }

        public List<T> Select<T>() where T : Entity.Entity
        {
            List<T> result;
            using (var session = SessionFactory.OpenSession())
            {
                try
                {
                    var query = session.QueryOver<T>();

                    result = (List<T>) query
                        .OrderBy(p => p.Id).Asc
                        .List<T>();
                }
                catch (Exception ex)
                {
                    var methodName = string.Format("{0}<{1}>", System.Reflection.MethodBase.GetCurrentMethod().Name, typeof(T));
                    MonitorLog.WriteLog(FormatLogMessage(methodName, ex), MonitorLog.typelog.Error, true);
                    throw;
                }
            }
            return result;
        }

        public List<T> Where<T>(Expression<Func<T, bool>> expression) where T : Entity.Entity
        {
            List<T> result;
            using (var session = SessionFactory.OpenSession())
            {
                try
                {
                    var query = session.QueryOver<T>().Where(expression);
                    result = (List<T>)query
                        .OrderBy(p => p.Id).Asc
                        .List<T>();
                }
                catch (Exception ex)
                {
                    var methodName = string.Format("{0}<{1}>", System.Reflection.MethodBase.GetCurrentMethod().Name, typeof(T));
                    MonitorLog.WriteLog(FormatLogMessage(methodName, ex), MonitorLog.typelog.Error, true);
                    throw;
                }
            }
            return result;
        }

        public T SingleOrDefault<T>(Expression<Func<T, bool>> expression) where T : Entity.Entity
        {
            T result;
            using (var session = SessionFactory.OpenSession())
            {
                try
                {
                    var query = session.QueryOver<T>().Where(expression).SingleOrDefault();
                    result = query;
                }
                catch (Exception ex)
                {
                    var methodName = string.Format("{0}<{1}>", System.Reflection.MethodBase.GetCurrentMethod().Name, typeof(T));
                    MonitorLog.WriteLog(FormatLogMessage(methodName, ex), MonitorLog.typelog.Error, true);
                    throw;
                }
            }
            return result;
        }

        public int Save<T>(T entity) where T : Entity.Entity
        {
            int result;
            using (var session = SessionFactory.OpenSession())
            {
                using (var tran = session.BeginTransaction())
                {
                    try
                    {
                        session.SaveOrUpdate(entity);

                        tran.Commit();
                        result = entity.Id;
                    }
                    catch (Exception ex)
                    {
                        var methodName = string.Format("{0}<{1}>", System.Reflection.MethodBase.GetCurrentMethod().Name, typeof(T));
                        MonitorLog.WriteLog(FormatLogMessage(methodName, ex), MonitorLog.typelog.Error, true);
                        tran.Rollback();
                        result = 0;
                    }
                }
            }
            return result;
        }

        public void Save<T>(List<T> list) where T : Entity.Entity
        {
            using (var session = SessionFactory.OpenSession())
            {
                using (var tran = session.BeginTransaction())
                {
                    try
                    {
                        foreach (var entity in list)
                        {
                            session.SaveOrUpdate(entity);                           
                        }

                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        var methodName = string.Format("{0}<{1}>", System.Reflection.MethodBase.GetCurrentMethod().Name, typeof(T));
                        MonitorLog.WriteLog(FormatLogMessage(methodName, ex), MonitorLog.typelog.Error, true);
                        tran.Rollback();
                    }
                }
            }
        }

        public int SaveOrUpdate<T>(T entity) where T : Entity.Entity
        {
            int result;
            using (var session = SessionFactory.OpenSession())
            {
                using (var tran = session.BeginTransaction())
                {
                    try
                    {
                        session.SaveOrUpdate(entity);

                        tran.Commit();
                        result = entity.Id;
                    }
                    catch (Exception ex)
                    {
                        var methodName = string.Format("{0}<{1}>", System.Reflection.MethodBase.GetCurrentMethod().Name, typeof(T));
                        MonitorLog.WriteLog(FormatLogMessage(methodName, ex), MonitorLog.typelog.Error, true);
                        tran.Rollback();
                        result = 0;
                    }
                }
            }
            return result;
        }

        public bool Delete<T>(T entity) where T : Entity.Entity
        {
            bool result;
            using (var session = SessionFactory.OpenSession())
            {
                using (var tran = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(entity);

                        tran.Commit();
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        var methodName = string.Format("{0}<{1}>", System.Reflection.MethodBase.GetCurrentMethod().Name, typeof(T));
                        MonitorLog.WriteLog(FormatLogMessage(methodName, ex), MonitorLog.typelog.Error, true);
                        tran.Rollback();
                        result = false;
                    }
                }
            }
            return result;
        }

        private static string FormatLogMessage(string methodName, Exception ex)
        {
            return string.Format("Ошибка выполнения процедуры {0} : {1}. \nInnerException : {2}", methodName, ex.Message, ex.InnerException);
        }
    }
}
