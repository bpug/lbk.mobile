//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ObservableDictionary.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;

    public class ObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>,
        INotifyCollectionChanged,
        INotifyPropertyChanged
    {
        private const string CountString = "Count";

        private const string IndexerName = "Item[]";

        private const string KeysName = "Keys";

        private const string ValuesName = "Values";

        private IDictionary<TKey, TValue> _dictionary;

        public ObservableDictionary()
        {
            this._dictionary = new Dictionary<TKey, TValue>();
        }

        public ObservableDictionary(IDictionary<TKey, TValue> dictionary)
        {
            this._dictionary = new Dictionary<TKey, TValue>(dictionary);
        }

        public ObservableDictionary(IEqualityComparer<TKey> comparer)
        {
            this._dictionary = new Dictionary<TKey, TValue>(comparer);
        }

        public ObservableDictionary(int capacity)
        {
            this._dictionary = new Dictionary<TKey, TValue>(capacity);
        }

        public ObservableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
        {
            this._dictionary = new Dictionary<TKey, TValue>(dictionary, comparer);
        }

        public ObservableDictionary(int capacity, IEqualityComparer<TKey> comparer)
        {
            this._dictionary = new Dictionary<TKey, TValue>(capacity, comparer);
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Count
        {
            get
            {
                return this.Dictionary.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return this.Dictionary.IsReadOnly;
            }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                return this.Dictionary.Keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                return this.Dictionary.Values;
            }
        }

        protected IDictionary<TKey, TValue> Dictionary
        {
            get
            {
                return this._dictionary;
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                return this.Dictionary[key];
            }
            set
            {
                this.Insert(key, value, false);
            }
        }

        public void Add(TKey key, TValue value)
        {
            this.Insert(key, value, true);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            this.Insert(item.Key, item.Value, true);
        }

        public void AddRange(IDictionary<TKey, TValue> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            if (items.Count > 0)
            {
                if (this.Dictionary.Count > 0)
                {
                    if (items.Keys.Any((k) => this.Dictionary.ContainsKey(k)))
                    {
                        throw new ArgumentException("An item with the same key has already been added.");
                    }
                    else
                    {
                        foreach (var item in items)
                        {
                            this.Dictionary.Add(item);
                        }
                    }
                }
                else
                {
                    this._dictionary = new Dictionary<TKey, TValue>(items);
                }

                OnCollectionChanged(NotifyCollectionChangedAction.Add, items.ToArray());
            }
        }

        public void Clear()
        {
            if (this.Dictionary.Count > 0)
            {
                this.Dictionary.Clear();
                this.OnCollectionChanged();
            }
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return this.Dictionary.Contains(item);
        }

        public bool ContainsKey(TKey key)
        {
            return this.Dictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            this.Dictionary.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this.Dictionary.GetEnumerator();
        }

        public bool Remove(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            TValue value;
            this.Dictionary.TryGetValue(key, out value);
            bool removed = this.Dictionary.Remove(key);
            if (removed)
            {
                //OnCollectionChanged(NotifyCollectionChangedAction.Remove, new KeyValuePair<TKey, TValue>(key, value));
                this.OnCollectionChanged();
            }

            return removed;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return this.Dictionary.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.Dictionary).GetEnumerator();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void Insert(TKey key, TValue value, bool add)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            TValue item;
            if (this.Dictionary.TryGetValue(key, out item))
            {
                if (add)
                {
                    throw new ArgumentException("An item with the same key has already been added.");
                }
                if (Equals(item, value))
                {
                    return;
                }
                this.Dictionary[key] = value;

                this.OnCollectionChanged(
                    NotifyCollectionChangedAction.Replace,
                    new KeyValuePair<TKey, TValue>(key, value),
                    new KeyValuePair<TKey, TValue>(key, item));
            }
            else
            {
                this.Dictionary[key] = value;

                this.OnCollectionChanged(NotifyCollectionChangedAction.Add, new KeyValuePair<TKey, TValue>(key, value));
            }
        }

        private void OnCollectionChanged()
        {
            this.OnPropertyChanged();
            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, KeyValuePair<TKey, TValue> changedItem)
        {
            this.OnPropertyChanged();
            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        private void OnCollectionChanged(
            NotifyCollectionChangedAction action,
            KeyValuePair<TKey, TValue> newItem,
            KeyValuePair<TKey, TValue> oldItem)
        {
            this.OnPropertyChanged();
            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, IList newItems)
        {
            this.OnPropertyChanged();
            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        private void OnPropertyChanged()
        {
            this.OnPropertyChanged(CountString);
            this.OnPropertyChanged(IndexerName);
            this.OnPropertyChanged(KeysName);
            this.OnPropertyChanged(ValuesName);
        }
    }
}