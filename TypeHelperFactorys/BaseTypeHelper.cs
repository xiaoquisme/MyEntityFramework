using System;
using System.Linq;
using System.Reflection;

namespace MyEntityFrameWork.TypeHelperFactorys
{
    internal static class BaseTypeHelper
    {
        #region 获取单个成员
        private static MemberInfo GetOneMember(Type t, string MemberName)
        {
            return GetAllMembers(t).FirstOrDefault(m => m.Name == MemberName);
        }

        #endregion

        #region 获取所有成员
        public  static MemberInfo[] GetAllMembers(Type t)
        {
            return t.GetMembers();
        }

        #endregion

        #region 获取成员的属性

        /// <summary>
        /// 获取成员的属性
        /// </summary>
        /// <param name="obj">目标类</param>
        /// <param name="MemberName">成员名称</param>
        /// <returns></returns>
        private static PropertyInfo GetProperty(object obj, string MemberName)
        {
            var type = obj.GetType(); 
            var member = GetOneMember(type, MemberName);
            return type.GetProperty(member.Name);

        }
        #endregion

        #region 执行法并返回结果    
        /// <summary>
        /// 获取方法的返回值
        /// </summary>
        /// <param name="MethodName">方法的名称</param>
        /// <param name="instance">实例</param>
        /// <param name="param">参数列表，如果没有参数则置为null</param>
        /// <returns></returns>
        public static object GetMethodValue(string MethodName, object instance, params object[] param)
        {
            Type t = instance.GetType();
            try
            {
                MethodInfo info = t.GetMethod(MethodName);
                return info.Invoke(instance, param);
            }
            catch 
            {
                throw new Exception("方法没有找到");
            }

        }
        #endregion

        #region 获取声明成员的类型

        /// <summary>
        /// 获取声明成员的类型
        /// 说明：返回若为空则 没有找到
        /// 若不为空，则查找正常
        /// </summary>
        /// <param name="MemberName">成员的名称</param>
        /// <param name="t">所在类的类型</param>
        /// <returns></returns>
        public static string GetPropertyType(string MemberName, Type t)
        {
            MemberInfo member = GetOneMember(t, MemberName);
            if (member != null)
            {
                PropertyInfo property = t.GetProperty(member.Name);
                return property.PropertyType.Name;
            }
            return null;

        }
        #endregion

        #region 获取单个成员是否含有某个属性


        /// <summary>
        /// 获取单个成员是否含有某个特性
        /// </summary>
        /// <param name="MemberName">成员的名称</param>
        /// <param name="t">所在类的类型</param>
        /// <param name="attribute">要获取的特性</param>
        /// <returns></returns>
        public static bool CustomAttributeExist(string MemberName, Type t, Attribute attribute)
        {

            var Member = GetOneMember(t, MemberName);
            var My_customAttribute = Member.CustomAttributes.FirstOrDefault(
                              a => a.AttributeType == attribute.GetType());
            return My_customAttribute != null;
        }
        #endregion

        #region 通过SetValue给成员设值
  
        /// <summary>
        /// 给成员设值
        /// </summary>
        /// <param name="obj">目标类</param>
        /// <param name="MemberName">类内属性名称</param>
        /// <param name="value">设置的值</param>
        public static void SetValue(object obj, string MemberName, object value)
        {
            var Property = GetProperty(obj, MemberName);
            Property.SetValue(obj, value);
        }
        #endregion

        #region 通过GetValue给成员取值
    
        /// <summary>
        /// 取成员的值
        /// </summary>
        /// <param name="obj">目标类</param>
        /// <param name="MemberName">成员的名称</param>
        /// <returns></returns>
        public static object GetValue(object obj, string MemberName)
        {
            var Property = GetProperty(obj, MemberName);
            return Property.GetValue(obj);
        }
        #endregion
        
        
    }
}
