using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace MyEntityFrameWork.TypeHelperFactorys
{
    internal static class TypeHelperFactory
    {
        #region GetAllPropertyList

        public static List<string> GetAllPropertyList(Type type)
        {
            var Propertys = BaseTypeHelper
                          .GetAllMembers(type)
                          .ToList()
                          .FindAll(member => member.MemberType == MemberTypes.Property);

            #region 这里进行了修改 时间：2017-5-17 17：47 测试已通过
            //这是原来的代码
            //var PropertyList = new List<string>();
            //foreach (var item in Propertys)
            //{
            //    PropertyList.Add(item.Name);
            //}
            // return PropertyList;
            //这是新的代码
            var PropertyList = from Property in Propertys
                               select Property.Name;

            return PropertyList.ToList();
            #endregion

        }

        #endregion

        #region GetAllPropertyNameAndValueDictionary 有改进的空间
        //添加到字典中的时候已经去除掉空的值
        public static Dictionary<string, object> GetAllPropertyNameAndValueDictionary(object obj)
        {
            Type type = obj.GetType();
            var PropertyList = GetAllPropertyList(type);
            var PropertyValueList = new Dictionary<string, object>();
            foreach (var Property in PropertyList)
            {
                var value = BaseTypeHelper.GetValue(obj, Property);
                if (value == null) continue;
                PropertyValueList.Add(Property, value);
            }
            return PropertyValueList;
        }

        #endregion

        #region GetTableName
        /// <summary>
        ///简单获取类的名称
        /// 未查找特性[table(Name="")]的标注
        /// 2017-5-9 18：00 
        /// Author ：曲
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetTableName(Type type)
        {
            return type.Name;
        }

        #endregion

        #region GetPrimaryKey
        /// <summary>
        /// 获取主键 可以识别特性 key  或者默认的ID/Id/id 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetPrimaryKey(Type type)
        {
            //1.查找特性标注为key的
            //2.如果不存在 查找 类型为int和名称为ID/Id/id 的
            var memberNameList = GetAllPropertyList(type);
            var attrribute = new KeyAttribute();
            #region 这里进行了修改 时间:2017-5-17 18:19 测试已通过
            //这是原来的代码


            //foreach (var item in memberNameList)
            //{
            //    if (BaseTypeHelper.CustomAttributeExist(item, type, attrribute))
            //    {
            //        return item;
            //    }
            //}
            //return memberNameList.FirstOrDefault(
            //                                       key => key.ToLower() == "id"
            //                                       && BaseTypeHelper
            //                                          .GetPropertyType(key, type)
            //                                          .Contains("Int")
            //                                       );
            //这是新的代码
            var KeyAttribute = from item in memberNameList
                               where BaseTypeHelper.CustomAttributeExist(item, type, attrribute)
                               select item;
          //  if(KeyAttribute.Count()>0)
            return KeyAttribute.FirstOrDefault()  ?? FindPrimaryKeyHasID(memberNameList, type);
       
            #endregion


        }
        /// <summary>
        /// 查找list中是否存在属性名称为ID/Id/id且数据类型为int的元素
        /// 查找不到返回为空值
        /// </summary>
        /// <param name="memberNameList">成员名称list</param>
        /// <param name="type">所在类的类型</param>
        /// <returns></returns>
        private static  string FindPrimaryKeyHasID(List<string> memberNameList,Type type)
        {
          return  memberNameList.FirstOrDefault(
                                                    key => key.ToLower() == "id"
                                                    && BaseTypeHelper
                                                       .GetPropertyType(key, type)
                                                       .Contains("Int")
                                                    );
        }
    
        #endregion

        #region GetPrimaryKeyValue

        public static object GetPrimaryKeyValue(object obj, string PrimaryKeyName)
        {
            return BaseTypeHelper.GetValue(obj, PrimaryKeyName);
        }

        #endregion

        #region GetPrimaryKeyType

        public static string GetPrimaryKeyType(Type type, string PrimaryKey)
        {
            return BaseTypeHelper.GetPropertyType(PrimaryKey, type);
        }


        #endregion

        #region SetPropertyValue

        public static void SetPropertyValue(object obj, string MemberName, object value)
        {
            BaseTypeHelper.SetValue(obj, MemberName, value);
        }

        #endregion
    }
}
