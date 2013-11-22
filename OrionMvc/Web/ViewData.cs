using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Web.UI;

namespace OrionMvc.Web
{
    public class ViewData : DynamicObject, IDictionary, ICollection, IEnumerable//Dictionary<string, object>, IViewData
    {
        Dictionary<string, object> viewBag;

        public ViewData()
        {
            viewBag = new Dictionary<string, object>();
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            viewBag[binder.Name] = value;
            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;
            if (viewBag.Keys.Contains(binder.Name))
            {

                result = viewBag[binder.Name];
                return true;
            }
           
            result = null;
            return false;
        }

        public override System.Collections.Generic.IEnumerable<string> GetDynamicMemberNames()
        {
            return Keys.Cast<string>().AsEnumerable();
        }

        public object this[string key]
        {
            get {
                try
                {
                    return viewBag[key];
                }catch(KeyNotFoundException ex)
                {
                    object result = null;
                    GetMember(this,key, out result);
                    throw;
                }
            }
            set { viewBag[key] = value; }
        }

        private bool GetMember(ViewData viewData, string key, out object result)
        {
            var miArray = this.GetType().GetMember(key, BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Instance);
            if (miArray != null && miArray.Length > 0)
            {
                var mi = miArray[0];
                if (mi.MemberType == MemberTypes.Property)
                {
                    result = ((PropertyInfo)mi).GetValue(this, null);
                    return true;
                }
            }

            result = null;
            return false;        
        }
        protected bool SetMember(ViewData viewData, string name, object value)
        {
           

            var miArray = this.GetType().GetMember(name, BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.Instance);
            if (miArray != null && miArray.Length > 0)
            {
                var mi = miArray[0];
                if (mi.MemberType == MemberTypes.Property)
                {
                    ((PropertyInfo)mi).SetValue(this, value, null);
                    return true;
                }
            }
            return false;
        }

        #region StateBag Methods



        #endregion

        #region Implementation of IStateManager



        #endregion

        #region Implementation of IDictionary

        void IDictionary.Add(object key, object value)
        {
            ((IDictionary)viewBag).Add(key, value);
        }

        public void Clear()
        {
            viewBag.Clear();
        }

        bool IDictionary.Contains(object key)
        {
            return ((IDictionary)viewBag).Contains(key);
        }

        public IDictionaryEnumerator GetEnumerator()
        {
            return viewBag.GetEnumerator();
        }

        bool IDictionary.IsFixedSize
        {
            get { return ((IDictionary)viewBag).IsFixedSize; }
        }

        bool IDictionary.IsReadOnly
        {
            get { return ((IDictionary)viewBag).IsReadOnly; }
        }

        public ICollection Keys
        {
            get { return viewBag.Keys; }
        }

        void IDictionary.Remove(object key)
        {
            ((IDictionary)viewBag).Remove(key);
        }

        public ICollection Values
        {
            get { return viewBag.Values; }
        }

        object IDictionary.this[object key]
        {
            get
            {
                return ((IDictionary)viewBag)[key];
            }
            set
            {
                ((IDictionary)viewBag)[key] = value;
            }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            ((IDictionary)viewBag).CopyTo(array, index);
        }

        public int Count
        {
            get { return viewBag.Count; }
        }

        bool ICollection.IsSynchronized
        {
            get { return ((IDictionary)viewBag).IsSynchronized; }
        }

        object ICollection.SyncRoot
        {
            get { return ((IDictionary)viewBag).SyncRoot; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IDictionary)viewBag).GetEnumerator();
        }

        #endregion
    }
}
