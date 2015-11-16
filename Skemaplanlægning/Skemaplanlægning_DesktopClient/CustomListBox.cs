using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Skemaplanlægning_DesktopClient
{
    /// <summary>
    /// well... this class was supposed to add 2-way binding support for listbox selecteditems... used
    /// 2 days on it without getting it to work so i kindda stopped... 
    /// </summary>
    /* custom listbox solution i made, works 1-ways
    public class CustomListBox : ListBox
    {

        public CustomListBox()
        {
            this.SelectionChanged += CustomListBox_SelectionChanged;
        }

        private void CustomListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.SelectedItemsList = this.SelectedItems;
        }
        #region SelectedItemsList

        public IList SelectedItemsList
        {
            get { return (IList)GetValue(SelectedItemsListProperty); }
            set { SetValue(SelectedItemsListProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemsListProperty =
                DependencyProperty.Register("SelectedItemsList", typeof(IList), typeof(CustomListBox), new PropertyMetadata(null));

        #endregion
    }*/
    /* behavior solution...ish
    public class MultiSelectionBehavior : Behavior<ListBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            if (SelectedItems != null)
            {
                AssociatedObject.SelectedItems.Clear();
                foreach (var item in SelectedItems)
                {
                    AssociatedObject.SelectedItems.Add(item);
                }
            }
        }

        public IList SelectedItems
        {
            get { return (IList)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register("SelectedItems", typeof(IList), typeof(MultiSelectionBehavior), new UIPropertyMetadata(null, SelectedItemsChanged));

        private static void SelectedItemsChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var behavior = o as MultiSelectionBehavior;
            if (behavior == null)
                return;

            var oldValue = e.OldValue as INotifyCollectionChanged;
            var newValue = e.NewValue as INotifyCollectionChanged;

            if (oldValue != null)
            {
                oldValue.CollectionChanged -= behavior.SourceCollectionChanged;
                behavior.AssociatedObject.SelectionChanged -= behavior.ListBoxSelectionChanged;
            }
            if (newValue != null)
            {
                behavior.AssociatedObject.SelectedItems.Clear();
                foreach (var item in (IEnumerable)newValue)
                {
                    behavior.AssociatedObject.SelectedItems.Add(item);
                }

                behavior.AssociatedObject.SelectionChanged += behavior.ListBoxSelectionChanged;
                newValue.CollectionChanged += behavior.SourceCollectionChanged;
            }
        }

        private bool _isUpdatingTarget;
        private bool _isUpdatingSource;

        void SourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_isUpdatingSource)
                return;

            try
            {
                _isUpdatingTarget = true;

                if (e.OldItems != null)
                {
                    foreach (var item in e.OldItems)
                    {
                        AssociatedObject.SelectedItems.Remove(item);
                    }
                }

                if (e.NewItems != null)
                {
                    foreach (var item in e.NewItems)
                    {
                        AssociatedObject.SelectedItems.Add(item);
                    }
                }
            }
            finally
            {
                _isUpdatingTarget = false;
            }
        }

        private void ListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isUpdatingTarget)
                return;

            var selectedItems = this.SelectedItems;
            if (selectedItems == null)
                return;

            try
            {
                _isUpdatingSource = true;

                foreach (var item in e.RemovedItems)
                {
                    selectedItems.Remove(item);
                }

                foreach (var item in e.AddedItems)
                {
                    selectedItems.Add(item);
                }
            }
            finally
            {
                _isUpdatingSource = false;
            }
        }

    }*/
    /* fuck it... not even this worked...
    public class SmartListBox : ListBox
    {
        #region Properties 

        public static readonly DependencyProperty SmartSelectedItemsProperty =
          DependencyProperty.Register("SmartSelectedItems", typeof(INotifyCollectionChanged), typeof(SmartListBox), new PropertyMetadata(OnSmartSelectedItemsPropertyChanged));

        public INotifyCollectionChanged SmartSelectedItems
        {
            get { return (INotifyCollectionChanged)GetValue(SmartSelectedItemsProperty); }
            set { SetValue(SmartSelectedItemsProperty, value); }
        }

        #endregion

        public SmartListBox()
        {
            SelectionChanged += new SelectionChangedEventHandler(BaseListBoxSelectionChanged);
        }

        private static void OnSmartSelectedItemsPropertyChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            var collection = args.NewValue as INotifyCollectionChanged;
            if (collection != null)
            {
                // unsubscribe, before subscribe to make sure not to have multiple subscription
                collection.CollectionChanged -= ((SmartListBox)target).SmartSelectedItemsCollectionChanged;
                collection.CollectionChanged += ((SmartListBox)target).SmartSelectedItemsCollectionChanged;
            }
        }

        void SmartSelectedItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //Need to unsubscribe from the events so we don't override the transfer
            UnsubscribeFromEvents();

            //Move items from the selected items list to the list box selection
            Transfer(SmartSelectedItems as IList, base.SelectedItems);

            //subscribe to the events again so we know when changes are made
            SubscribeToEvents();
        }

        void BaseListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Need to unsubscribe from the events so we don't override the transfer
            UnsubscribeFromEvents();

            //Move items from the selected items list to the list box selection
            Transfer(base.SelectedItems, SmartSelectedItems as IList);

            //subscribe to the events again so we know when changes are made
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            SelectionChanged += BaseListBoxSelectionChanged;

            if (SmartSelectedItems != null)
            {
                SmartSelectedItems.CollectionChanged += SmartSelectedItemsCollectionChanged;
            }
        }

        private void Transfer(System.Collections.IList source, IList target)
        {
            if (source == null || target == null)
            {
                return;
            }

            target.Clear();

            foreach (var o in source)
            {
                target.Add(o);
            }
        }

        private void UnsubscribeFromEvents()
        {
            SelectionChanged -= BaseListBoxSelectionChanged;

            if (SmartSelectedItems != null)
            {
                SmartSelectedItems.CollectionChanged -= SmartSelectedItemsCollectionChanged;
            }
        }
    }*/
}